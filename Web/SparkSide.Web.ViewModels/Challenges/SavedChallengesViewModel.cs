﻿namespace SparkSide.Web.ViewModels.Challenges
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SparkSide.Services.Data.Models;

    public class SavedChallengesViewModel
    {
        public ICollection<ChallengeDTO> Challenges { get; set; }
    }
}
