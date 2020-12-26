namespace SparkSide.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using SparkSide.Data.Common.Repositories;
    using SparkSide.Data.Models;
    using SparkSide.Services.Data.Contracts;
    using SparkSide.Services.Data.Models;

    public class ChallengesService : IChallengesService
    {
        private readonly IDeletableEntityRepository<Challenge> challengesRepository;

        public ChallengesService(IDeletableEntityRepository<Challenge> challengesRepository)
        {
            this.challengesRepository = challengesRepository;
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
    }
}
