namespace SparkSide.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using SparkSide.Services.Data.Models;
    using SparkSide.Web.ViewModels.Users;

    public interface IUsersService
    {
        UserDTO GetByLoginName(string username);

        Task FollowAsync(string currentUserId, string followedUserId);

        bool IsFollowing(string currentUserId, string otherUserId);

        Task UnfollowAsync(string currentUserId, string otherUserId);

        Task<bool> IsInRoleAsync(string id, string trainerRoleName);

        Task UpdateUserSettingsAsync(UserSettingsInputModel input, string path);
    }
}
