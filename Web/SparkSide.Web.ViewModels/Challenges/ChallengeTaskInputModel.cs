namespace SparkSide.Web.ViewModels.Challenges
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class ChallengeTaskInputModel
    {
        [Required]
        public string Title { get; set; }

        public int Day { get; set; }
    }
}
