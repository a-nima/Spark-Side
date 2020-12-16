using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkSide.Web.Controllers
{
    public class ExploreController : Controller
    {
        [AllowAnonymous]
        public IActionResult AllChallenges()
        {
            return this.View();
        }
    }
}
