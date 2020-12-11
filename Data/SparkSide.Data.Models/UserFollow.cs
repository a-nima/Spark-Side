namespace SparkSide.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SparkSide.Data.Common.Models;

    public class UserFollow : BaseDeletableModel<int>
    {
        public string FollowerId { get; set; }

        public virtual ApplicationUser Follower { get; set; }

        public string FollowedId { get; set; }

        public virtual ApplicationUser Followed { get; set; }
    }
}
