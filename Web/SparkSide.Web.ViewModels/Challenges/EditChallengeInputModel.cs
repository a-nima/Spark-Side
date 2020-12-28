namespace SparkSide.Web.ViewModels.Challenges
{
    using SparkSide.Services.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class EditChallengeInputModel : CreateChallengeInputModel
    {

        public EditChallengeInputModel(ChallengeDTO challenge)
            : base(challenge)
        {
            this.Id = challenge.Id;
        }

        public int Id { get; set; }
    }
}
