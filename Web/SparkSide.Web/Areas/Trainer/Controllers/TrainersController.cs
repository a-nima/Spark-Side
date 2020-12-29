namespace SparkSide.Web.Areas.Trainer.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using SparkSide.Common;
    using SparkSide.Web.Controllers;

    [Authorize(Roles = GlobalConstants.TrainerRoleName)]
    [Area("Trainer")]
    public class TrainersController : BaseController
    {
    }
}
