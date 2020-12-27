namespace SparkSide.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SparkSide.Common;
    using SparkSide.Data.Models;
    using SparkSide.Services.Data.Contracts;
    using SparkSide.Services.Data.Models;
    using SparkSide.Web.ViewModels.Challenges;

    public class ChallengesController : Controller
    {
        private readonly IChallengesService challengesService;
        private readonly UserManager<ApplicationUser> userManager;


        public ChallengesController(
            IChallengesService challengesService,
            UserManager<ApplicationUser> userManager)
        {
            this.challengesService = challengesService;
            this.userManager = userManager;
        }

        public IActionResult All()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Saved()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Followed()
        {
            return this.View();
        }

        public async Task<IActionResult> Details(int id)
        {
            ChallengeDTO challenge = await this.challengesService.GetById(id);

            if (challenge == null)
            {
                return this.NotFound();
            }

            return this.View(challenge);
        }
    }
}
