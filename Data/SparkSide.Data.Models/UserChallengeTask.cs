namespace SparkSide.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using SparkSide.Data.Common.Models;

    public class UserChallengeTask : BaseDeletableModel<int>
    {
        [Required]
        public int ChallengeTaskId { get; set; }

        public virtual ChallengeTask ChallengeTask { get; set; }

        [Required]
        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public DateTime CompletedOn { get; set; }
    }
}
