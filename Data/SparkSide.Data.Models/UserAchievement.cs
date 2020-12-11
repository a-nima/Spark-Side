namespace SparkSide.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using SparkSide.Data.Common.Models;

    public class UserAchievement : BaseDeletableModel<int>
    {
        public UserAchievement()
        {
            this.UserAchievementLikes = new HashSet<Like>();
        }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public int AchievementId { get; set; }

        public virtual Achievement Achievement { get; set; }

        public virtual ICollection<Like> UserAchievementLikes { get; set; }

    }
}
