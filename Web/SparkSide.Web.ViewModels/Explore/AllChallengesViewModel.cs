namespace SparkSide.Web.ViewModels.Explore
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SparkSide.Services.Data.Models;

    public class AllChallengesViewModel
    {
        public IEnumerable<ChallengeDTO> Challenges { get; set; }
    }
}
