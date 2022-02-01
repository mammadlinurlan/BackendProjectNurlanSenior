using BackendProjectNurlanSenior.Dal;
using BackendProjectNurlanSenior.Extensions;
using BackendProjectNurlanSenior.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class SpeakerController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        public SpeakerController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page=1)
        {

            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Tags.Count() / 4);


            List<Speaker> speakers = _context.Speakers.Include(s => s.EventSpeakers).ThenInclude(es => es.Event).Skip((page-1)*4).Take(4).ToList();
            return View(speakers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Speaker speaker)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }



            if (speaker.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Select image please");
                return View();
            }
            if (!speaker.ImageFile.IsSizeOkay(2))
            {
                ModelState.AddModelError("ImageFile", "Image size must be less than 2mb");
                return View();
            }
            if (!speaker.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Please select an image file");
                return View();
            }

            speaker.Image = speaker.ImageFile.SaveImg(_env.WebRootPath,"assets/img/event");
            _context.Speakers.Add(speaker);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            Speaker speaker = _context.Speakers.FirstOrDefault(s => s.Id == id);
            if (speaker==null)
            {
                return NotFound();
            }
            return View(speaker);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Speaker speaker)
        {
            if (!ModelState.IsValid)
            {
                return View(speaker);
            }

            Speaker exist = _context.Speakers.Include(s => s.EventSpeakers).ThenInclude(s => s.Event).FirstOrDefault(s => s.Id == speaker.Id);
            if (speaker.ImageFile!=null)
            {
                if (speaker.ImageFile == null)
                {
                    ModelState.AddModelError("ImageFile", "Select image please");
                    return View();
                }
                if (!speaker.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size must be less than 2mb");
                    return View();
                }
                if (!speaker.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Please select an image file");
                    return View();
                }

                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/event", exist.Image);
                exist.Image = speaker.ImageFile.SaveImg(_env.WebRootPath, "assets/img/event");

            }

            exist.Name = speaker.Name;
            exist.Speciality = speaker.Speciality;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int id)
        {
            Speaker speaker = _context.Speakers.FirstOrDefault(s => s.Id == id);
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (speaker == null)
            {
                return Json(new { status = 400 });
            }
            Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/event", speaker.Image);
            _context.Speakers.Remove(speaker);
            _context.SaveChanges();
            return Json(new { status = 200 });
        }


    }
}


