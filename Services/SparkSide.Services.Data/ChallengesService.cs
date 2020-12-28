namespace SparkSide.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SparkSide.Data.Common.Repositories;
    using SparkSide.Data.Models;
    using SparkSide.Services.Data.Contracts;
    using SparkSide.Services.Data.Models;

    public class ChallengesService : IChallengesService
    {
        private readonly IDeletableEntityRepository<Challenge> challengesRepository;
        private readonly IDeletableEntityRepository<UserChallengeActive> startedChallengesRepository;
        private readonly IDeletableEntityRepository<UserChallengeFavourite> savedChallengesRepository;


        public ChallengesService(IDeletableEntityRepository<Challenge> challengesRepository,
            IDeletableEntityRepository<UserChallengeActive> startedChallengesRepository,
            IDeletableEntityRepository<UserChallengeFavourite> savedChallengesRepository)
        {
            this.challengesRepository = challengesRepository;
            this.savedChallengesRepository = savedChallengesRepository;
            this.startedChallengesRepository = startedChallengesRepository;
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
            };

            challenge.UsersWithFavouriteChallenge.Add(entity);
            await this.savedChallengesRepository.SaveChangesAsync();
        }
    }
}
