﻿namespace SparkSide.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SparkSide.Data.Common.Repositories;
    using SparkSide.Data.Models;
    using SparkSide.Services.Data.Contracts;
    using SparkSide.Services.Data.Models;
    using SparkSide.Web.ViewModels.Challenges;

    public class ChallengesService : IChallengesService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };

        private readonly IDeletableEntityRepository<Challenge> challengesRepository;
        private readonly IDeletableEntityRepository<UserChallengeActive> startedChallengesRepository;
        private readonly IDeletableEntityRepository<UserChallengeFavourite> savedChallengesRepository;
        private readonly ITagsService tagsService;
        public ChallengesService(
            IDeletableEntityRepository<Challenge> challengesRepository,
            IDeletableEntityRepository<UserChallengeActive> startedChallengesRepository,
            IDeletableEntityRepository<UserChallengeFavourite> savedChallengesRepository,
            ITagsService tagsService)
        {
            this.challengesRepository = challengesRepository;
            this.savedChallengesRepository = savedChallengesRepository;
            this.startedChallengesRepository = startedChallengesRepository;
            this.tagsService = tagsService;
        }

        public ICollection<ChallengeDTO> GetAll()
        {
            return this.challengesRepository
                .All()
                .Include(c => c.Author)
                .Where(c => c.IsPublished)
                .OrderByDescending(c => c.CreatedOn)
                .Select(c => new ChallengeDTO(c))
                .ToList();
        }

        public ICollection<ChallengeDTO> GetUserCreatedChallenges(string userId)
        {
            return this.challengesRepository
                .All()
                .Include(c => c.Author)
                .Where(c => c.AuthorId == userId)
                .Select(c => new ChallengeDTO(c))
                .ToList();
        }

        public ICollection<ChallengeDTO> GetUserFollowedChallenges(string userId)
        {
            return this.challengesRepository
                .All()
                .Include(c => c.Author)
                .Where(c => c.UsersWithActiveChallenge.Any(u => u.UserId == userId))
                .Select(c => new ChallengeDTO(c))
                .ToList();
        }

        public ICollection<ChallengeDTO> GetUserSavedChallenges(string userId)
        {
            return this.challengesRepository
               .All()
               .Include(c => c.Author)
               .Where(c => c.UsersWithFavouriteChallenge.Any(u => u.UserId == userId))
               .Select(c => new ChallengeDTO(c))
               .ToList();
        }

        public Task<ChallengeDTO> GetByIdAsync(int id)
        {
            return this.challengesRepository
                .All()
                .Include(c => c.Author)
                .Include(c => c.Tags)
                    .ThenInclude(t => t.Tag)
                .Where(c => c.Id == id)
                .Select(c => new ChallengeDTO(c))
                .FirstOrDefaultAsync();
        }

        public Task DeleteByIdAsync(int id)
        {
            Challenge challenge = this.challengesRepository
                .All()
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (challenge == null)
            {
                throw new ArgumentException("No challenge found with id " + id);
            }

            this.challengesRepository.Delete(challenge);

            return this.challengesRepository.SaveChangesAsync();
        }

        public Task PublishAsync(int id)
        {
            Challenge challenge = this.challengesRepository
             .All()
             .Where(c => c.Id == id)
             .FirstOrDefault();

            if (challenge == null)
            {
                throw new ArgumentException("No challenge found with id " + id);
            }

            challenge.IsPublished = true;

            return this.challengesRepository.SaveChangesAsync();
        }

        public bool IsChallengeStarted(string userId, int challengeId)
        {
            return this.startedChallengesRepository
                .All()
                .Any(c => c.UserId == userId && challengeId == c.ChallengeId);
        }

        public bool IsChallengeSaved(string userId, int challengeId)
        {
            return this.savedChallengesRepository
                .All()
                .Any(c => c.UserId == userId && challengeId == c.ChallengeId);
        }

        public async Task AddToSavedAsync(string userId, int challengeId)
        {
            UserChallengeFavourite entity = new UserChallengeFavourite();
            entity.ChallengeId = challengeId;
            entity.UserId = userId;

            Challenge challenge = this.challengesRepository
                .All()
                .Where(c => c.Id == challengeId)
                .FirstOrDefault();
            if (challenge == null)
            {
                throw new ArgumentException("Can't find challenge with id " + challengeId);
            }

            challenge.UsersWithFavouriteChallenge.Add(entity);
            await this.savedChallengesRepository.SaveChangesAsync();
        }

        public async Task<int> CreateAsync(CreateChallengeInputModel input, string userId, string path)
        {
            //TODO: Add days count
            Challenge challenge = new Challenge
            {
                Title = input.Title,
                Description = input.Description,
                AuthorId = userId,
                IsPublished = false,
                DurationDays = input.DurationDays,
            };

            await this.challengesRepository.AddAsync(challenge);
            await this.challengesRepository.SaveChangesAsync();

            for (int i = 0; i < input.Tags.Count; i++)
            {
                await this.tagsService.AddTag(challenge.Id, input.Tags.ElementAt(i));
            }

            if (input.Image != null)
            {

                // /wwwroot/images/recipes/jhdsi-343g3h453-=g34g.jpg
                Directory.CreateDirectory($"{path}/challenges/");

                var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var physicalPath = $"{path}/challenges/{challenge.Id}.{extension}";

                using (Stream fileStream = new FileStream(physicalPath, FileMode.Create))
                {
                    await input.Image.CopyToAsync(fileStream);
                }

                challenge.PictureLink = physicalPath;
                await this.challengesRepository.SaveChangesAsync();
            }

            return challenge.Id;
        }
    }
}