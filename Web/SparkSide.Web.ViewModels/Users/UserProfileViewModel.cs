namespace SparkSide.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SparkSide.Services.Data.Models;

   public class UserProfileViewModel
    {
        public UserDTO User { get; set; }

        public bool IsCurrentUser { get; set; }

        public bool IsFollowing { get; set; }
    }
}
