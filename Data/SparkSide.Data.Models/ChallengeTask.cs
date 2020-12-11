namespace SparkSide.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using SparkSide.Data.Common.Models;

    public class ChallengeTask : BaseDeletableModel<int>
    {
        public ChallengeTask()
        {
            this.UserChallengeTasks = new HashSet<UserChallengeTask>();
        }

        [Required]
        public int Day { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [MaxLength(350)]
        public string Description { get; set; }

        public string PictureUrl { get; set; }

        public int ChallengeId { get; set; }

        public virtual Challenge Challenge { get; set; }

        public virtual ICollection<UserChallengeTask> UserChallengeTasks { get; set; }
    }
}
