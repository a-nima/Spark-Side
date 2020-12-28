namespace SparkSide.Web.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SparkSide.Data.Models;
    using SparkSide.Services.Data.Contracts;
    using SparkSide.Web.ViewModels.Challenges;
    using SparkSide.Web.ViewModels.Users;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserActionsController : ControllerBase
    {
        private readonly IUsersService usersService;
        private readonly IChallengesService challengesService;

        private readonly UserManager<ApplicationUser> userManager;

        public UserActionsController(
            IUsersService usersService,
            IChallengesService challengesService,
            UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
            this.challengesService = challengesService;
        }

        [HttpPost]
        [Route(nameof(Follow))]
        public async Task<IActionResult> Follow([FromBody] UserFollowInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == model.UserId)
            {
                return this.BadRequest("You cannot follow yourself.");
            }

            await this.usersService.FollowAsync(currentUserId, model.UserId);
            return this.Ok();
        }

        [HttpPost]
        [Route(nameof(Unfollow))]
        public async Task<IActionResult> Unfollow([FromBody] UserFollowInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == model.UserId)
            {
                return this.BadRequest("You cannot unfollow yourself.");
            }

            await this.usersService.UnfollowAsync(currentUserId, model.UserId);

            return this.Ok();
        }

        [HttpPost]
        [Route(nameof(SaveChallenge))]
        public async Task<IActionResult> SaveChallenge([FromBody] ChallengeIdInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                await this.challengesService.AddToSavedAsync(userId, input.ChallengeId);

                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.BadRequest();
            }

        }
    }
}
