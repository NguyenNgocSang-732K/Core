using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models.Business;
using Project.Models.ViewModels;
using Supports;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Project.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [AllowAnonymous]
    public class AdminController : Controller
    {
        private IConfiguration configuration;

        public AdminController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        [HttpGet("")]
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login(UserView userView)
        {
            if (ValidateUserView(userView))
            {
                UserView user = UserBus.LoginAdmin(userView);
                if (user == null)
                {
                    ViewBag.Error = "[Email or password invalid]";
                    return View();
                }
                SercurityManagerCuaSang.Login(HttpContext, user, "SCHEME_ADMIN");
                return RedirectToAction("index", "home", new { area = "admin" });
            }
            ViewBag.Error = "[Data invalid]";
            return View();
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            SercurityManagerCuaSang.Logout(HttpContext, "SCHEME_ADMIN");
            return RedirectToAction("login");
        }

        [HttpGet("forgotpassword")]
        public IActionResult ForgotPassword()
        {
            if (TempData["Result"] != null && TempData["Result"].ToString() == "0")
            {
                ViewBag.Result = "OK";
            }
            return View();
        }

        [HttpPost("forgotpassword")]
        public IActionResult ForgotPassword(string email)
        {
            string resultCode = UserBus.ForgorPassword(email); // mail +"-"+chuỗi mã hoá: khi lấy ra thì slipt cái "-" rồi lấy chuỗi so khớp
            if (resultCode != null)
            {
                string link = "https://localhost:44307/admin/changePW?pwId=" + Convert.ToBase64String(Encoding.ASCII.GetBytes(resultCode));
                if (new SendMail(configuration).Send(email, "Change password", "Click the following link: " + link))
                {
                    TempData["Result"] = "0";
                    return RedirectToAction("forgotpassword", "admin", new { area = "admin" });
                }
                else
                {
                    ViewBag.Error = "[Network error please try again later]";
                    return View();
                }
            }
            ViewBag.Error = "[Email invalid. Please check again]";
            return View();
        }

        [HttpGet("changePW")]
        public IActionResult ChangePW()
        {
            try
            {
                string result = Encoding.ASCII.GetString(Convert.FromBase64String(HttpContext.Request.Query["pwId"].ToString()));
                string[] splits = result.Split(new char[] { '-' });
                string email = splits[0];
                string code = splits[1];
                UserView userView = UserBus.CompareCodeChangePW(email, code);
                if (userView != null)
                {
                    return View(userView);
                }
            }
            catch { }
            return RedirectToAction("accessDenied");
        }

        [HttpPost("changePW")]
        public IActionResult ChangePW(UserView userView)
        {

            if (UserBus.UpdatePassword(userView))
            {
                return Json("200");
            }
            else
            {
                return Json("404");
            }
        }

        [HttpGet("accessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private bool ValidateUserView(UserView userView)
        {
            try
            {
                new MailAddress(userView.Email);
                Regex regex = new Regex(@"\w");
                return regex.IsMatch(userView.Password);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
    }
}