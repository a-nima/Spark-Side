namespace SparkSide.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using SparkSide.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        public Tag()
        {
            this.Challenges = new HashSet<ChallengeTag>();
        }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public virtual ICollection<ChallengeTag> Challenges { get; set; }

    }
}
