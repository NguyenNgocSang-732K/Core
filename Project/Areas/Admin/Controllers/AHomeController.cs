using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models.Business;
using Project.Models.ViewModels;
using Supports;

namespace Project.Areas.Admin.Controllers
{
    [Authorize(Roles = "0", AuthenticationSchemes = "SCHEME_ADMIN")]
    [Area("admin")]
    [Route("admin/home")]
    public class AHomeController : Controller
    {

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {

            return View();
        }
    }
}