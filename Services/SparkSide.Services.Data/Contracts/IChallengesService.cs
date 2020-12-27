namespace SparkSide.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using SparkSide.Services.Data.Models;

    public interface IChallengesService
    {
        ICollection<ChallengeDTO> GetAll();

        ICollection<ChallengeDTO> GetUserSavedChallenges(string userId);

        ICollection<ChallengeDTO> GetUserFollowedChallenges(string userId);

        ICollection<ChallengeDTO> GetUserCreatedChallenges(string userId);

        Task<ChallengeDTO> GetById(int id);
    }
}
