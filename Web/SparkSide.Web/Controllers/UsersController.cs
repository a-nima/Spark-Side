namespace SparkSide.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SparkSide.Data.Models;
    using SparkSide.Services.Data.Contracts;
    using SparkSide.Services.Data.Models;
    using SparkSide.Web.ViewModels.Users;
    using SparkSide.Common;
    using Microsoft.AspNetCore.Hosting;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IChallengesService challengesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public UsersController(
            IUsersService usersService,
            IChallengesService challengesService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.usersService = usersService;
            this.userManager = userManager;
            this.challengesService = challengesService;
            this.environment = environment;
        }

        [Authorize]
        [Route("Users/Settings")]
        public async Task<IActionResult> Settings()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            UserSettingsInputModel model = new UserSettingsInputModel(user);

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        [Route("Users/Settings")]
        public async Task<IActionResult> Settings(UserSettingsInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            string currentUserId = this.userManager.GetUserId(this.User);

            if (currentUserId != input.UserId)
            {
                return this.View();
            }

            try
            {
                await this.usersService.UpdateUserSettingsAsync(input, $"{this.environment.WebRootPath}/images");
                return this.RedirectToAction("AllChallenges", "Explore");
            }
            catch
            {
                return this.View();
            }
        }

        [Authorize]
        [Route("Users/{loginName}")]
        public async Task<IActionResult> Index(string loginName)
        {
            bool isCurrentUser = false;
            bool isFollowing = false;
            UserDTO targetUser = this.usersService.GetByLoginName(loginName);
            if (loginName.ToLower() == this.User.Identity.Name.ToLower())
            {
                isCurrentUser = true;
            }
            else
            {
                ApplicationUser currentUser = await this.userManager.GetUserAsync(this.User);
                isFollowing = this.usersService.IsFollowing(currentUser.Id, targetUser.Id);
            }

            if (targetUser.ProfilePictureLink == string.Empty)
            {
                targetUser.ProfilePictureLink = "../img/" + GlobalConstants.DefaultProfilePictureName;
            }

            UserProfileViewModel model = new UserProfileViewModel();
            model.User = targetUser;
            model.IsCurrentUser = isCurrentUser;
            model.IsFollowing = isFollowing;
            model.IsTrainer = await this.usersService.IsInRoleAsync(targetUser.Id, GlobalConstants.TrainerRoleName);
            model.IsAdmin = await this.usersService.IsInRoleAsync(targetUser.Id, GlobalConstants.AdministratorRoleName);
            model.Challenges = this.challengesService.GetUserStartedChallenges(targetUser.Id);

            if (model.IsTrainer)
            {
                model.Challenges = this.challengesService.GetUserCreatedChallenges(targetUser.Id);
            }

            return this.View(model);
        }
    }
}
