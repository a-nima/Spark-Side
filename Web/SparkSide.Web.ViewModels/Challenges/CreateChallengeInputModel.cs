namespace SparkSide.Web.ViewModels.Challenges
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateChallengeInputModel
    {
        public CreateChallengeInputModel()
        {
            this.Tags = new HashSet<string>();
        }

        [Required]
        [MaxLength(120)]
        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<string> Tags { get; set; }
    }
}
