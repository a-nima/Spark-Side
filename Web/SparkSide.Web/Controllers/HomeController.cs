namespace SparkSide.Web.Controllers
{
    using System.Diagnostics;

    using SparkSide.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using SparkSide.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            if (User?.Identity != null && User.Identity.IsAuthenticated)
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
        public IActionResult Dashboard()
        {

            return this.View(new DashboardViewModel());
        }
    }
}
