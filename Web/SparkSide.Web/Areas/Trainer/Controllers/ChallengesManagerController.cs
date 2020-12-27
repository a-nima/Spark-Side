namespace SparkSide.Web.Areas.Trainer.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using SparkSide.Data.Models;
    using SparkSide.Services.Data.Contracts;
    using SparkSide.Services.Data.Models;

    public class ChallengesManagerController : TrainersController
    {
        private readonly IChallengesService challengesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ChallengesManagerController(
            IChallengesService challengesService,
            UserManager<ApplicationUser> userManager)
        {
            this.challengesService = challengesService;
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
            return View();
        }

        // POST: ChallengesManagerController/Create
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        //[HttpPost]
        //[Authorize(Roles = GlobalConstants.TrainerRoleName)]
        //public IActionResult Create([FromBody] CreateChallengeInputModel model)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.BadRequest();
        //    }

        //    return this.RedirectToAction("MyChallenges", "Challenges");
        //}


        // GET: ChallengesManagerController/Edit/5
        public ActionResult Edit(int id)
        {
            // edit challenge if not published
            // else redirect
            return View();
        }

        // POST: ChallengesManagerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ChallengesManagerController/Delete/5
        public ActionResult Delete(int id)
        {

            return View();
        }

        // POST: ChallengesManagerController/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
