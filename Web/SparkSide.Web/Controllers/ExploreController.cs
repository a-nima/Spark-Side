namespace SparkSide.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SparkSide.Services.Data.Contracts;
    using SparkSide.Services.Data.Models;

    public class ExploreController : Controller
    {
        private readonly IChallengesService challengesService;

        public ExploreController(IChallengesService challengesService)
        {
            this.challengesService = challengesService;
        }

        [AllowAnonymous]
        public IActionResult AllChallenges()
        {
            ICollection<ChallengeDTO> challenges = this.challengesService.GetAll();
            return this.View(challenges);
        }
    }
}
