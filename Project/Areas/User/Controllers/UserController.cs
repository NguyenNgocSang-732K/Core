using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Models.Business;
using Project.Models.ViewModels;
using Supports;

namespace Project.Areas.User.Controllers
{
    [Area("user")]
    [Route("user")]
    public class UserController : Controller
    {
        [HttpGet("")]
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login(UserView userView)
        {
            if (ModelState.IsValid)
            {
                UserView user = UserBus.Login(userView);
                if (user == null)
                {
                    ViewBag.Error = "ERROR: Email or password invalid";
                    return View();
                }
                SercurityManagerCuaSang.Login(HttpContext, user, "SCHEME_USER");
                return Redirect($"/user/home");
            }
            ViewBag.Error = "ERROR: Data Invalid";
            return View();
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            SercurityManagerCuaSang.Logout(HttpContext, "SCHEME_USER");
            return RedirectToAction("login");
        }

        [HttpGet("forgotpassword")]
        public IActionResult ForgotPassword()
        {
            return Ok("Sáng");
        }

        [HttpGet("accessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}