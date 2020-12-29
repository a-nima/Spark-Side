namespace SparkSide.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using SparkSide.Data.Models;
    using SparkSide.Services.Data.Models;
    using SparkSide.Services.DTOs;
    using SparkSide.Web.ViewModels.Challenges;

    public interface IChallengesService
    {
        ICollection<ChallengeDTO> GetAll();

        ICollection<ChallengeDTO> GetUserSavedChallenges(string userId);

        ICollection<ActiveChallengeDTO> GetUserStartedChallengesStats(string userId);

        ICollection<ChallengeDTO> GetUserStartedChallenges(string userId);

        ICollection<ChallengeDTO> GetUserCreatedChallenges(string userId);

        ICollection<UserChallengeTask> GetUserChallengeTasks(string userId, int challengeId);

        ICollection<ChallengeTask> GetChallengeTasks(int challengeId);

        Task<ChallengeDTO> GetByIdAsync(int id);

        Task DeleteByIdAsync(int id);

        Task PublishAsync(int id);

        bool IsChallengeStarted(string userId, int id);

        bool IsChallengeSaved(string userId, int id);

        Task AddToSavedAsync(string userId, int challengeId);
        
        Task RemoveFromSavedAsync(string userId, int challengeId);

        Task AddToStartedAsync(string userId, int challengeId);


        Task RemoveFromStartedAsync(string userId, int challengeId);

        Task<int> CreateAsync(CreateChallengeInputModel input, string userId, string path);

        Task UpdateAsync(int id, EditChallengeInputModel input);
    }
}
