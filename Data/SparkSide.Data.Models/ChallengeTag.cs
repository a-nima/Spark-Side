namespace SparkSide.Data.Models
{
    using System;

    using SparkSide.Data.Common.Models;

    public class ChallengeTag : BaseDeletableModel<int>
    {
        public int ChallengeId { get; set; }

        public virtual Challenge Challenge { get; set; }

        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
