namespace SparkSide.Web.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SparkSide.Data.Models;
    using SparkSide.Services.Data.Contracts;

    [Route("api/[controller]")]
    [ApiController]
    public class UserActionsController : ControllerBase
    {
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public UserActionsController(
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route(nameof(Follow))]
        public async Task<IActionResult> Follow()
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if (currentUserId == userId)
            //{
            //    return this.BadRequest("You cannot follow yourself.");
            //}

            //await this.usersService.FollowAsync(currentUserId, userId);
            return this.Ok();
        }
    }
}
