namespace SparkSide.Web.ViewModels.Challenges
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SparkSide.Services.Data.Models;

    public class ChallengeDetailsViewModel
    {
        public ChallengeDTO Challenge { get; set; }

        public bool IsSaveButtonDisabled { get; set; }

        public bool IsStartButtonDisabled { get; set; }
    }
}
