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
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Sliders.Count() / 3);
            List<Slider> sliders = _context.Sliders.Skip((page - 1) * 3).Take(3).ToList();
            return View(sliders);

           
        }

        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (!ModelState.IsValid) return View();

            if (slider.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Choose image please");
                return View();
            }
            if (!slider.ImageFile.IsSizeOkay(2))
            {
                ModelState.AddModelError("ImageFile", "Image size must be less than 2mb");
                return View();
            }
            if (!slider.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Please select an image file!");
                return View();
            }



            slider.Image = slider.ImageFile.SaveImg(_env.WebRootPath, "assets/img/slider");
            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction("Index", "Slider");

        }

        public IActionResult Edit(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Slider slider)
        {
            Slider existSlider = _context.Sliders.FirstOrDefault(es => es.Id == slider.Id);
            if (existSlider == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(slider);
            }

           

            if (slider.ImageFile!=null)
            {
                if (!slider.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Select an image file!");
                    return View(slider);
                }
                if (!slider.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size must be less than 2MB");
                    return View(slider);
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/slider", existSlider.Image);
                existSlider.Image = slider.ImageFile.SaveImg(_env.WebRootPath, "assets/img/slider");
                
            }

            existSlider.Title = slider.Title;
            existSlider.Description = slider.Description;
            existSlider.LearnMoreLink = slider.LearnMoreLink;
            _context.SaveChanges();
            return RedirectToAction("Index", "Slider");
                 
            

        }

        public IActionResult Delete(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (slider==null)
            {
                return Json(new { status=400 });
            }
            Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/slider", slider.Image);
            _context.Sliders.Remove(slider);
            _context.SaveChanges();
            return Json(new { status = 200 });
        }
    }
}
