using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Models.Business;
using Project.Models.ViewModels;
using Supports;

namespace Project.Areas.Admin.Controllers
{
    [Authorize(Roles = "0", AuthenticationSchemes = "SCHEME_ADMIN")]
    [Area("admin")]
    [Route("admin/user")]
    public class AUserController : Controller
    {
        private IWebHostEnvironment webHostEnvironment;

        public AUserController(IWebHostEnvironment _webHostEnvironment)
        {
            webHostEnvironment = _webHostEnvironment;
        }

        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            string strPage = HttpContext.Request.Query["page"].ToString();
            int page = Convert.ToInt32(strPage == "" ? "1" : strPage);
            List<UserView> list = UserBus.GetData(page);
            ViewBag.Rows = PageBus.GetRowUser();
            return View("index", list);
        }

        [HttpGet("search")]
        public IActionResult Search()
        {
            string propertySearch = HttpContext.Request.Query["propertysearch"].ToString().Trim().ToLower();
            string textsearch = HttpContext.Request.Query["textsearch"].ToString().Trim().ToLower();
            string strPage = HttpContext.Request.Query["page"].ToString();
            int page = Convert.ToInt32(strPage == "" ? "1" : strPage);
            List<UserView> listUser = new List<UserView>();
            switch (propertySearch)
            {
                case "email":
                    listUser = UserBus.SearchByEmail(page, textsearch);
                    ViewBag.Rows = UserBus.GetRowCountSearchByEmail(textsearch);
                    break;
                case "name":
                    listUser = UserBus.SearchByName(page, textsearch);
                    ViewBag.Rows = UserBus.GetRowCountSearchByName(textsearch);
                    break;
                case "phone":
                    listUser = UserBus.SearchByPhone(page, textsearch);
                    ViewBag.Rows = UserBus.GetRowCountSearchByPhone(textsearch);
                    break;
                case "address":
                    listUser = UserBus.SearchByAddress(page, textsearch);
                    ViewBag.Rows = UserBus.GetRowCountSearchByAddress(textsearch);
                    break;
                default:
                    listUser = UserBus.SearchAll(page, textsearch);
                    ViewBag.Rows = UserBus.GetRowCountSearchAll(textsearch);
                    break;
            }
            return View("index", listUser);
        }

        [HttpGet("remove")]
        public IActionResult Remove(string listID)
        {
            try
            {
                string[] ids = JsonConvert.DeserializeObject<string[]>(listID);
                bool check = true;
                ids.ToList().ForEach(s =>
                {
                    if (!UserBus.SetStatus(Convert.ToInt32(s)))
                        check = false;
                });
                if (!check) return Json("");
                return Json("1");
            }
            catch
            {
                return Json("");
            }
        }

        [HttpGet("detail/{id}")]
        public IActionResult Detail(int id)
        {
            ViewBag.Result = TempData["Result"];
            UserView userView = UserBus.GetDataById(id);
            return View(userView);
        }


        [HttpPost("modify")]
        public IActionResult Modify(UserView userView, IFormFile photonew)
        {
            UserView user = UserBus.GetDataById(userView.Id);   //infor user cũ

            string fileOld = user.Photo;
            userView.Photo = fileOld;
            if (photonew != null)
                userView.Photo = FileCuaSang.SaveFile(webHostEnvironment, photonew, "assets/image");
            bool check = UserBus.Modify(userView);
            if (check)
            {
                if (photonew != null)
                {
                    FileCuaSang.RemoveFile(webHostEnvironment, fileOld);
                }
                TempData["Result"] = 200;
                return RedirectToAction("detail", "user", new
                {
                    area = "admin",
                    id = userView.Id
                });
            }
            else
            {
                user.Id = userView.Id;
                user.Name = userView.Name;
                user.Address = userView.Address;
                user.Phone = userView.Phone;
                user.Gender = userView.Gender;
                CookieCuaSang.Set(HttpContext, "user-edit", JsonConvert.SerializeObject(user), null);
                TempData["Result"] = 500;
                return RedirectToAction("detail", "user", new
                {
                    area = "admin",
                    id = userView.Id
                });
            }
        }

        [HttpGet("active")]
        public IActionResult Active(int id)
        {
            if (UserBus.Active(id))
            {
                return Json("/admin/user?event=success");
            }
            return Json("");
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            UserView userView = new UserView
            {
                Active = true,
                Gender = 0,
                Photo = "default.jpg",
                Role = 3
            };
            return View(userView);
        }

        [HttpPost("create")]
        public IActionResult Create(UserView userView, IFormFile inputphoto)
        {
            userView.DayCreate = DateTime.Now;
            userView.DayEdited = DateTime.Now;
            userView.Status = true;
            string FileNameSave = "default.jpg";
            if (inputphoto != null)
            {
                FileNameSave = FileCuaSang.SaveFile(webHostEnvironment, inputphoto, "assets/image");
            }
            userView.Photo = FileNameSave;
            int id = 0;
            if (ModelState.IsValid)
            {
                id = UserBus.Create(userView);

            }
            switch (id)
            {
                case -1:
                    ViewBag.Result = -1;
                    break;
                case 0:
                    ViewBag.Result = 0;
                    break;
                default:
                    return RedirectToAction("index");
            }
            return View(userView);
        }

        [HttpPost("upload_ckeditor")]
        public IActionResult Upload(IFormFile upload)
        {
            string filename = FileCuaSang.SaveFile(webHostEnvironment, upload, "/assets/image");
            return Json("/assets/image/" + filename);
        }

        [HttpGet("file_browser")]
        public IActionResult FileBrowser()
        {
            DirectoryInfo dir = new DirectoryInfo($"{webHostEnvironment.WebRootPath}\\" + "assets\\image\\");
            ViewBag.FileInfor = dir.GetFiles();
            return View();
        }


    }
}