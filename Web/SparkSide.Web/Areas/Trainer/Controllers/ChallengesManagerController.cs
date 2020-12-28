namespace SparkSide.Web.Areas.Trainer.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SparkSide.Data.Models;
    using SparkSide.Services.Data.Contracts;
    using SparkSide.Services.Data.Models;
    using SparkSide.Web.ViewModels.Challenges;

    public class ChallengesManagerController : TrainersController
    {
        private readonly IChallengesService challengesService;
        private readonly IWebHostEnvironment environment;
        private readonly UserManager<ApplicationUser> userManager;

        public ChallengesManagerController(
            IChallengesService challengesService,
            IWebHostEnvironment environment,
            UserManager<ApplicationUser> userManager)
        {
            this.challengesService = challengesService;
            this.environment = environment;
            this.userManager = userManager;
        }

        // GET: ChallengesManagerController
        public ActionResult Index()
        {
            // return only current trainer's challenges
            string trainerId = this.userManager.GetUserId(this.User);
            ICollection<ChallengeDTO> trainerChallenges = this.challengesService.GetUserCreatedChallenges(trainerId);

            return this.View(trainerChallenges);
        }

        // GET: ChallengesManagerController/Create
        public ActionResult Create()
        {
            return this.View(new CreateChallengeInputModel());
        }

        // POST: ChallengesManagerController/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateChallengeInputModel input)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View();
                };

                string userId = this.userManager.GetUserId(this.User);

                int challengeId = await this.challengesService.CreateAsync(input, userId, $"{this.environment.WebRootPath}/images");

                return this.RedirectToAction("Details", "Challenges", new { area = "", id = challengeId });
            }
            catch
            {
                return this.View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            ChallengeDTO challenge = await this.challengesService.GetByIdAsync(id);

            if (challenge != null && !challenge.IsPublished)
            {
                EditChallengeInputModel inputModel = new EditChallengeInputModel(challenge);

                return this.View(inputModel);
            }
            else
            {
                return this.NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditChallengeInputModel collection)
        {
            try
            {
                ChallengeDTO challenge = await this.challengesService.GetByIdAsync(id);

                if (challenge != null && !challenge.IsPublished)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Publish(int id)
        {
            try
            {
                await this.challengesService.PublishAsync(id);
                return this.RedirectToAction(nameof(this.Index));
            }
            catch
            {
                return this.NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.challengesService.DeleteByIdAsync(id);
                return this.RedirectToAction(nameof(this.Index));
            }
            catch
            {
                return this.NotFound();
            }
        }
    }
}
