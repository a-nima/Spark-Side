namespace SparkSide.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using SparkSide.Data.Common.Models;

    public class Challenge : BaseDeletableModel<int>
    {
        public Challenge()
        {
            this.Tags = new HashSet<ChallengeTag>();
            this.UsersWithActiveChallenge = new HashSet<UserChallengeActive>();
            this.UsersWithFavouriteChallenge = new HashSet<UserChallengeFavourite>();
            this.ChallengeTasks = new HashSet<ChallengeTask>();
            this.Comments = new HashSet<Comment>();
        }

        [Required]
        [MaxLength(120)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [Required]
        public int DurationDays { get; set; }

        public bool IsPublished { get; set; }

        public virtual ICollection<ChallengeTag> Tags { get; set; }

        public virtual ICollection<UserChallengeActive> UsersWithActiveChallenge { get; set; }

        public virtual ICollection<UserChallengeFavourite> UsersWithFavouriteChallenge { get; set; }

        public virtual ICollection<ChallengeTask> ChallengeTasks { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

    }
}
