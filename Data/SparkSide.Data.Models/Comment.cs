namespace SparkSide.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SparkSide.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(240)]
        public string Content { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        [Required]
        public int ChallengeId { get; set; }

        public virtual Challenge Challenge { get; set; }
    }
}
