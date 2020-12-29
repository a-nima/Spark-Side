namespace SparkSide.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using SparkSide.Services.Data.Models;
    using SparkSide.Web.ViewModels.Challenges;

    public interface IChallengesService
    {
        ICollection<ChallengeDTO> GetAll();

        ICollection<ChallengeDTO> GetUserSavedChallenges(string userId);

        ICollection<ChallengeDTO> GetUserFollowedChallenges(string userId);

        ICollection<ChallengeDTO> GetUserCreatedChallenges(string userId);

        Task<ChallengeDTO> GetByIdAsync(int id);

        Task DeleteByIdAsync(int id);

        Task PublishAsync(int id);

        bool IsChallengeStarted(string userId, int id);

        bool IsChallengeSaved(string userId, int id);

        Task AddToSavedAsync(string userId, int challengeId);

        Task<int> CreateAsync(CreateChallengeInputModel input, string userId, string path);

        Task UpdateAsync(int id, EditChallengeInputModel input);
    }
}
