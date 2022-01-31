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

    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.CCategories.Count() / 3);

            ViewBag.CurrentPage = page;
            List<CCategory> categories = _context.CCategories.Include(c=>c.Courses).Skip((page-1)*3).Take(3).ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(CCategory category)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            CCategory cCategory = _context.CCategories.Include(c=>c.Courses).FirstOrDefault(c => c.Name.ToLower().Trim() == category.Name.ToLower().Trim());
            if (cCategory!=null)
            {
                ModelState.AddModelError("", "This category name already exists");
                return View();
            }
            else
            {
                _context.CCategories.Add(category);
                _context.SaveChanges();
            }

          
          
            return RedirectToAction("Index", "Category");      
        }

        public IActionResult Edit(int id)
        {
            CCategory cCategory = _context.CCategories.Include(c => c.Courses).FirstOrDefault(c => c.Id == id);
            return View(cCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CCategory category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            CCategory cCategory = _context.CCategories.Include(c => c.Courses).FirstOrDefault(c => c.Id == category.Id);
            if (cCategory==null)
            {
                return NotFound();
            }

            CCategory existedCategory = _context.CCategories.Include(c => c.Courses).FirstOrDefault(c => c.Name.ToLower().Trim() == category.Name.ToLower().Trim());
            if (existedCategory!=null)
            {
                ModelState.AddModelError("", "This category already exists!");
                return View();
            }
            else
            {
                cCategory.Name = category.Name;
                _context.SaveChanges();
            }
            




            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid) {
                return View();
            }

            CCategory category = _context.CCategories.Include(c => c.Courses).FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return Json(new { status = 400 });
            }

            _context.CCategories.Remove(category);
            _context.SaveChanges();
            return Json(new { status=200});


        }
    }
}
