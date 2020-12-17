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

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
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
            return this.View(model);
        }
    }
}
