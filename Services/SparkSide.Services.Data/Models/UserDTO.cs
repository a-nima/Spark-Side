using System;
using System.Collections.Generic;
using System.Text;

namespace SparkSide.Services.Data.Models
{
    public class UserDTO
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int FollowersCount { get; set; }

        public int FollowingCount { get; set; }

        public bool IsCurrentUser { get; set; }

        public bool IsFollowing { get; set; }

        public string ProfilePictureLink { get; set; }

        public string ProfileDescription { get; set; }
    }
}
