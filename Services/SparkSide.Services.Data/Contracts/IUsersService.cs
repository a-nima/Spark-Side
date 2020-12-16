﻿using SparkSide.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparkSide.Services.Data.Contracts
{
    public interface IUsersService
    {
        UserDTO GetByLoginName(string username);

        void FollowAsync(string currentUserId, string followedUserId);

        bool IsFollowing(string currentUserId, string otherUserId);

    }
}
