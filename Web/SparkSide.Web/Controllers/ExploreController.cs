namespace SparkSide.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ExploreController : Controller
    {
        [AllowAnonymous]
        public IActionResult AllChallenges()
        {
            return this.View();
        }
    }
}
