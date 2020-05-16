using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

        private IConfiguration configuration;
        private IWebHostEnvironment webHostEnvironment;

        public HomeController(IConfiguration _configuration, IWebHostEnvironment _webHostEnvironment)
        {
            configuration = _configuration;
            webHostEnvironment = _webHostEnvironment;
        }

        [Route("~/")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
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
                UserView user = UserBus.Login(userView);
                if (user == null)
                {
                    ViewBag.Error = "[Email or password invalid]";
                    return View();
                }
                SercurityManagerCuaSang.Login(HttpContext, user, "SCHEME_USER");
                return RedirectToAction("index", "home");
            }
            ViewBag.Error = "[Data Invalid]";
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
                string link = "https://localhost:44307/home/changePW?pwId=" + Convert.ToBase64String(Encoding.ASCII.GetBytes(resultCode));
                if (new SendMail(configuration).Send(email, "Change password", "Click the following link: " + link))
                {
                    TempData["Result"] = "0";
                    return RedirectToAction("forgotpassword", "home");
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

        [HttpPost("upload_ckeditor")]
        public IActionResult Upload(IFormFile upload)
        {
            string filename = FileCuaSang.SaveFile(webHostEnvironment, upload, $"\\assets\\image");
            return Json($"\\assets\\image\\" + filename);
        }

        [HttpGet("file_browser")]
        public IActionResult FileBrowser()
        {
            DirectoryInfo dir = new DirectoryInfo($"{webHostEnvironment.WebRootPath}\\" + "assets\\image\\");
            ViewBag.FileInfor = dir.GetFiles();
            return View();
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