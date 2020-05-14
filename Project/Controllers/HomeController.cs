using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models.Business;
using Project.Models.Dto;
using Project.Models.ViewModels;
using Supports;

namespace Project.Controllers
{
    [Route("home")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [Route("~/")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}