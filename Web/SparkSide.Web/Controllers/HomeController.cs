namespace SparkSide.Web.Controllers
{
    using System.Diagnostics;
    using SparkSide.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using SparkSide.Web.ViewModels.Home;
    using SparkSide.Services.Data.Contracts;
    using SparkSide.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;
    using SparkSide.Services.Data.Models;

    public class HomeController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            if (this.User?.Identity != null && this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Dashboard", "Home");
            }

            return this.RedirectToAction("AllChallenges", "Explore");
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            DashboardViewModel model = new DashboardViewModel();
            ApplicationUser currentUser = await this.userManager.GetUserAsync(this.User);
            model.User = new UserDTO(currentUser);
            return this.View(model);
        }

        public IActionResult About()
        {
            return this.View();
        }
    }
}
