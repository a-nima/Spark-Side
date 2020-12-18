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

        public IActionResult Details([FromQuery] string id)
        {
            return this.View();
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.TrainerRoleName)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.TrainerRoleName)]
        public IActionResult Create([FromBody] CreateChallengeInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction("MyChallenges", "Challenges");
        }

        [Authorize(Roles = GlobalConstants.TrainerRoleName)]
        public IActionResult MyChallenges()
        {
            return this.View();
        }
    }
}
