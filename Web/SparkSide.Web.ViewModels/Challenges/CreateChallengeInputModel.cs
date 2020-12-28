namespace SparkSide.Web.ViewModels.Challenges
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using SparkSide.Services.Data.Models;

    public class CreateChallengeInputModel
    {
        public CreateChallengeInputModel(ChallengeDTO challenge)
        {
            this.Title = challenge.Title;
            this.Description = challenge.Description;
            this.DurationDays = challenge.DurationDays;
            this.Tags = challenge.Tags;
            this.Tasks = new HashSet<ChallengeTaskInputModel>();
        }
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
