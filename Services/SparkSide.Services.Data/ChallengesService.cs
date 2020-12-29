namespace SparkSide.Services.Data
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
    using SparkSide.Services.DTOs;
    using SparkSide.Web.ViewModels.Challenges;

    public class ChallengesService : IChallengesService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };

        private readonly IDeletableEntityRepository<Challenge> challengesRepository;
        private readonly IDeletableEntityRepository<UserChallengeActive> startedChallengesRepository;
        private readonly IDeletableEntityRepository<UserChallengeFavourite> savedChallengesRepository;
        private readonly IDeletableEntityRepository<UserChallengeTask> userChallengeTasksRepository;
        private readonly IDeletableEntityRepository<ChallengeTask> challengeTasksRepository;


        private readonly ITagsService tagsService;
        public ChallengesService(
            IDeletableEntityRepository<Challenge> challengesRepository,
            IDeletableEntityRepository<UserChallengeActive> startedChallengesRepository,
            IDeletableEntityRepository<UserChallengeFavourite> savedChallengesRepository,
            IDeletableEntityRepository<ChallengeTask> challengeTasksRepository,
            IDeletableEntityRepository<UserChallengeTask> userChallengeTasksRepository,
            ITagsService tagsService)
        {
            this.challengesRepository = challengesRepository;
            this.savedChallengesRepository = savedChallengesRepository;
            this.startedChallengesRepository = startedChallengesRepository;
            this.challengeTasksRepository = challengeTasksRepository;
            this.userChallengeTasksRepository = userChallengeTasksRepository;

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

        public ICollection<ActiveChallengeDTO> GetUserFollowedChallenges(string userId)
        {
            ICollection<ActiveChallengeDTO> challenges = this.challengesRepository
                .All()
                .Where(c => c.UsersWithActiveChallenge.Any(u => u.UserId == userId))
                .Select(c => new ActiveChallengeDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    Progress = 0,
                })
                .ToList();

            foreach (ActiveChallengeDTO challenge in challenges)
            {
                ICollection<UserChallengeTask> userChallengeTasks = this.GetUserChallengeTasks(userId, challenge.Id);
                ICollection<ChallengeTask> challengeTasks = this.GetChallengeTasks(challenge.Id);

                UserChallengeTask firstTask = userChallengeTasks.OrderBy(t => t.CompletedOn).FirstOrDefault();
                if (userChallengeTasks.Count != 0 && challengeTasks.Count() != 0)
                {
                    int percentComplete = (userChallengeTasks.Count() / challengeTasks.Count()) * 100;

                    challenge.Progress = percentComplete;
                    challenge.StartedOn = firstTask?.CompletedOn;
                }
            }

            return challenges;
        }

        private ICollection<ChallengeTask> GetChallengeTasks(int challengeId)
        {
            return this.challengeTasksRepository
                .All()
                .Where(c => c.ChallengeId == challengeId)
                .ToList();
        }

        private ICollection<UserChallengeTask> GetUserChallengeTasks(string userId, int challengeId)
        {
            return this.userChallengeTasksRepository
                .All()
                .Include(c => c.ChallengeTask)
                .Where(c => c.UserID == userId && c.ChallengeTask.ChallengeId == challengeId)
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

        public async Task RemoveFromSavedAsync(string userId, int challengeId)
        {
            UserChallengeFavourite current = this.savedChallengesRepository
                .All()
                .Where(c => c.UserId == userId && c.ChallengeId == challengeId)
                .FirstOrDefault();

            if (current != null)
            {
                this.savedChallengesRepository.HardDelete(current);
                await this.savedChallengesRepository.SaveChangesAsync();
            }
        }

        public async Task AddToStartedAsync(string userId, int challengeId)
        {
            UserChallengeActive entity = new UserChallengeActive();
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

            await this.startedChallengesRepository.AddAsync(entity);
            await this.challengesRepository.SaveChangesAsync();
        }

        public async Task RemoveFromStartedAsync(string userId, int challengeId)
        {
            UserChallengeActive current = this.startedChallengesRepository
                .All()
                .Where(c => c.UserId == userId && c.ChallengeId == challengeId)
                .FirstOrDefault();

            if (current != null)
            {
                this.startedChallengesRepository.HardDelete(current);
                await this.startedChallengesRepository.SaveChangesAsync();
            }
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

                challenge.PictureLink = $"~/images/challenges/{challenge.Id}.{extension}";
                await this.challengesRepository.SaveChangesAsync();
            }

            return challenge.Id;
        }

        public async Task UpdateAsync(int id, EditChallengeInputModel input)
        {
            Challenge challenge = this.challengesRepository
                .All()
                .Include(c => c.Tags)
                    .ThenInclude(t => t.Tag)
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (challenge == null)
            {
                throw new ArgumentException("Can't find challenge with id " + id);
            }

            challenge.Title = input.Title;
            challenge.Description = input.Description;
            challenge.DurationDays = input.DurationDays;

            await this.challengesRepository.SaveChangesAsync();

            await this.tagsService.UpdateTagsAsync(challenge, input.Tags);
        }
    }
}