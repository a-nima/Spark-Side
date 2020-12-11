using SparkSide.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SparkSide.Data.Models
{
    public class Like : BaseDeletableModel<int>
    {
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public int UserAchievementId { get; set; }

        public virtual UserAchievement UserAchievement { get; set; } 
    }
}
