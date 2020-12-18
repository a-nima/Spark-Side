using SparkSide.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SparkSide.Services.Data.Contracts
{
    public interface IUsersService
    {
        UserDTO GetByLoginName(string username);

        Task FollowAsync(string currentUserId, string followedUserId);

        bool IsFollowing(string currentUserId, string otherUserId);

        Task UnfollowAsync(string currentUserId, string otherUserId);
    }
}
