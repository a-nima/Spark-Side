namespace SparkSide.Web.ViewModels.Challenges
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class CreateChallengeInputModel
    {
        public CreateChallengeInputModel()
        {
            this.Tags = new HashSet<string>();
            this.Tasks = new HashSet<ChallengeTaskInputModel>();
        }

        [Required]
        [MaxLength(120)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int DurationDays { get; set; }

        public IFormFile Image { get; set; }

        public ICollection<ChallengeTaskInputModel> Tasks { get; set; }

        public ICollection<string> Tags { get; set; }
    }
}
