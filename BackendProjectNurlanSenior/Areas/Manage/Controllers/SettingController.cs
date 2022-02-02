using BackendProjectNurlanSenior.Dal;
using BackendProjectNurlanSenior.Extensions;
using BackendProjectNurlanSenior.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles="Admin,SuperAdmin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        public SettingController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            Settings setting = _context.Settings.FirstOrDefault();
            return View(setting);
        }

        public IActionResult Edit(int id)
        {
            Settings settings = _context.Settings.FirstOrDefault(s => s.Id == id);
            return View(settings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Settings setting)
        {
            

            if (!ModelState.IsValid)
            {
                return View(setting);
            }

            Settings exist = _context.Settings.FirstOrDefault(s => s.Id == setting.Id);

            if (setting.BigImageFile!=null)
            {
                if (!setting.BigImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("BigImageFile", "Image size must be less than 2mb");
                    return View(setting);
                }
                if (!setting.BigImageFile.IsImage())
                {
                    ModelState.AddModelError("BigImageFile", "Select an image file! ");
                    return View(setting);
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/logo", exist.LogoBig);
                exist.LogoBig = setting.BigImageFile.SaveImg(_env.WebRootPath, "assets/img/logo");
            }






            if (setting.LittleImageFile != null)
            {
                if (!setting.LittleImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("LittleImageFile", "Image size must be less than 2mb");
                    return View(setting);
                }
                if (!setting.LittleImageFile.IsImage())
                {
                    ModelState.AddModelError("LittleImageFile", "Select an image file! ");
                    return View(setting);
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/logo", exist.LogoLittle);
                exist.LogoLittle = setting.LittleImageFile.SaveImg(_env.WebRootPath, "assets/img/logo");
            }

            exist.Adress = setting.Adress;
            exist.Question = setting.Question;

            exist.Phone = setting.Phone;
            exist.SearchIcon = setting.SearchIcon;
            exist.SubsTitle = setting.SubsTitle;
            exist.Mail = setting.Mail;
            exist.Website = setting.Website;
            exist.FooterPhone = setting.FooterPhone;
            exist.SubsDesc = setting.SubsDesc;
            exist.FooterDesc = setting.FooterDesc;
            exist.FbIcon = setting.FbIcon;
            exist.InstaIcon = setting.InstaIcon;
            exist.VimeoIcon = setting.VimeoIcon;
            exist.TwitterIcon = setting.TwitterIcon;
            exist.FbLink = setting.FbLink;
            exist.InstaLink = setting.InstaLink;
            exist.TwitterLink = setting.TwitterLink;
            exist.VimeoLink = setting.VimeoLink;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));






        }
    }
}
