using BackendProjectNurlanSenior.Dal;
using BackendProjectNurlanSenior.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles ="SuperAdmin,Admin")]
    public class TagController : Controller
    {
        private readonly AppDbContext _context;
        public TagController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Tags.Count() / 3);

            ViewBag.CurrentPage = page;
            List<Tag> tags = _context.Tags.Include(t => t.CourseTags).ThenInclude(ct => ct.Course).Skip((page-1)*3).Take(3).ToList();
            return View(tags);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Tag tag1 = _context.Tags.Include(t => t.CourseTags).ThenInclude(ct => ct.Course).FirstOrDefault(c => c.Name.ToLower().Trim() == tag.Name.ToLower().Trim());
            if (tag1!=null)
            {
                ModelState.AddModelError("", "This tag already exists");
                return View();
            }
           
                _context.Tags.Add(tag);
                _context.SaveChanges();


            return RedirectToAction("Index","Tag");

        }

        public IActionResult Edit(int id)
        {
            Tag tag = _context.Tags.Include(t => t.CourseTags).ThenInclude(ct => ct.Course).FirstOrDefault(t => t.Id == id);
            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Tag tag1 = _context.Tags.Include(t => t.CourseTags).ThenInclude(ct => ct.Course).FirstOrDefault(t => t.Id == tag.Id);
            if (tag1==null)
            {
                return NotFound();
            }

            Tag sameName = _context.Tags.Include(t => t.CourseTags).ThenInclude(ct => ct.Course).FirstOrDefault(t => t.Name.ToLower().Trim() == tag.Name.ToLower().Trim());
            if (sameName!=null)
            {
                ModelState.AddModelError("", "This tag already exists");
                return View();
            }
            tag1.Name = tag.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Tag tag = _context.Tags.Include(t=>t.CourseTags).ThenInclude(ct=>ct.Course).FirstOrDefault(t => t.Id == id);
            if (tag == null)
            {
                return Json(new { status = 400 });
            }

            _context.Tags.Remove(tag);
            _context.SaveChanges();
            return Json(new { status = 200 });


        }
    }
}
