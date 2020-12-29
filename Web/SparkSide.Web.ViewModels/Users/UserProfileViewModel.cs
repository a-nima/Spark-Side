namespace SparkSide.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SparkSide.Services.Data.Models;

    public class UserProfileViewModel
    {
        public UserProfileViewModel()
        {
            this.Challenges = new HashSet<ChallengeDTO>();
            this.StartedChallenges = new HashSet<ChallengeDTO>();
        }

        public UserDTO User { get; set; }

        public bool IsCurrentUser { get; set; }

        public bool IsFollowing { get; set; }

        public bool IsTrainer { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<ChallengeDTO> Challenges { get; set; }

        public ICollection<ChallengeDTO> StartedChallenges { get; set; }
    }
}
