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
    using SparkSide.Services.DTOs;
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
        public async Task<IActionResult> CancelChallenge(int id)
        {
            try
            {
                string userId = this.userManager.GetUserId(this.User);
                await this.challengesService.RemoveFromStartedAsync(userId, id);
                return this.RedirectToAction("Saved");
            }
            catch
            {
                return this.NotFound();
            }

        }

        [Authorize]
        public IActionResult Saved()
        {
            string userId = this.userManager.GetUserId(this.User);
            ICollection<ChallengeDTO> challenges = this.challengesService.GetUserSavedChallenges(userId);

            return this.View(challenges);
        }

        [Authorize]
        public IActionResult Started()
        {
            string userId = this.userManager.GetUserId(this.User);
            ICollection<ActiveChallengeDTO> challenges = this.challengesService.GetUserStartedChallenges(userId);

            return this.View(challenges);
        }

        public async Task<IActionResult> Details(int id)
        {
            ChallengeDetailsViewModel details = new ChallengeDetailsViewModel();
            details.Challenge = await this.challengesService.GetByIdAsync(id);

            if (details.Challenge == null)
            {
                return this.NotFound();
            }

            if (!details.Challenge.IsPublished && details.Challenge.Author.LoginName != this.User.Identity.Name)
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);

            details.IsStartButtonDisabled = this.challengesService.IsChallengeStarted(userId, details.Challenge.Id);
            details.IsSaveButtonDisabled = this.challengesService.IsChallengeSaved(userId, details.Challenge.Id);

            return this.View(details);
        }
    }
}
