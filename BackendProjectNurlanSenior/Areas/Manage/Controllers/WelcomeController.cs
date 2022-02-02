using BackendProjectNurlanSenior.Dal;
using BackendProjectNurlanSenior.Extensions;
using BackendProjectNurlanSenior.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class WelcomeController : Controller
    {


        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        public WelcomeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
            
        }
        public IActionResult Index()
        {
            WelcomeEdu welcome = _context.WelcomeEdus.FirstOrDefault();
            return View(welcome);
        }

        public IActionResult Edit(int id)
        {
            WelcomeEdu welcome = _context.WelcomeEdus.FirstOrDefault(w => w.Id == id);
            if (welcome==null)
            {
                return NotFound();
            }
            return View(welcome);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(WelcomeEdu welcome)
        {
            if (!ModelState.IsValid)
            {
                return View(welcome);
            }
            WelcomeEdu exist = _context.WelcomeEdus.FirstOrDefault(w => w.Id == welcome.Id);
            if (exist==null)
            {
                return NotFound();
            }

            if (welcome.ImageFile!=null)
            {
                if (!welcome.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size must be less than 2MB");
                    return View(welcome);

                }
                if (!welcome.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Select image file!");
                    return View(welcome);

                }

                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/about", exist.Image);
                exist.Image = welcome.ImageFile.SaveImg(_env.WebRootPath, "assets/img/about");
            }

            exist.Link = welcome.Link;
            exist.Title = welcome.Title;
            exist.TitleColored = welcome.TitleColored;
            exist.Description = welcome.Description;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
