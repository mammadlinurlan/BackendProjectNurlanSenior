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
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class HobbyController : Controller
    {
        private readonly AppDbContext _context;
        public HobbyController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Hobbies.Count() / 3);

            ViewBag.CurrentPage = page;
            List<Hobby> hobbies = _context.Hobbies.Include(t => t.TeacherHobbies).ThenInclude(ct => ct.Teacher).Skip((page - 1) * 3).Take(3).ToList();
            return View(hobbies);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Hobby hobby)
        {
            if (!ModelState.IsValid)
            {
                return View(hobby);
            }

            Hobby exist = _context.Hobbies.FirstOrDefault(h => h.Name.ToLower().Trim() == hobby.Name.ToLower().Trim());

            if (exist!=null)
            {
                ModelState.AddModelError("", "This hobby already exists");
                return View(hobby);
            }

            _context.Hobbies.Add(hobby);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public  IActionResult Edit(int id)
        {
            Hobby hobby = _context.Hobbies.FirstOrDefault(h => h.Id == id);
            if (hobby==null)
            {
                return NotFound();
            }
            return View(hobby);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Hobby hobby)
        {
            if (!ModelState.IsValid)
            {
                return View(hobby);
            }

            Hobby exist = _context.Hobbies.FirstOrDefault(h => h.Id == hobby.Id);
            if (exist==null)
            {
                return NotFound();
            }
            if (_context.Hobbies.Any(h => h.Name.ToLower().Trim() == hobby.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("", "This hobby already exists");
                return View(hobby);
            }

            exist.Name = hobby.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Hobby Hobby = _context.Hobbies.FirstOrDefault(t => t.Id == id);
            if (Hobby == null)
            {
                return Json(new { status = 400 });
            }

            _context.Hobbies.Remove(Hobby);
            _context.SaveChanges();
            return Json(new { status = 200 });


        }
    }
}
