using BackendProjectNurlanSenior.Dal;
using BackendProjectNurlanSenior.Models;
using BackendProjectNurlanSenior.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        public CourseController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            List<Course> courses = _context.Courses.ToList();
            return View(courses);
        }

        public IActionResult Details(int id=8)
        {
            CourseVM courseVM = new CourseVM
            {
                Course = _context.Courses.Include(c => c.Features).Include(c => c.CourseTags).ThenInclude(ct => ct.Tag).Include(c => c.CCategory).FirstOrDefault(c => c.Id == id),
                Courses = _context.Courses.ToList(),
                Tags = _context.Tags.Include(t => t.CourseTags).ThenInclude(ct => ct.Course).ToList()
        };

           
            ViewBag.CourseCategories = _context.CCategories.Include(c=>c.Courses).ToList();
            if (courseVM==null)
            {
                return NotFound();
            }
            return View(courseVM);
        }

        public IActionResult CategoryRelatedCourse(int id)
        {
            List<Course> courses = _context.Courses.Include(c => c.CCategory).Where(c => c.CCategory.Id == id).ToList();
            if (courses==null)
            {
                return NotFound();
            }
            return View(courses);
        }

        public IActionResult TagRelatedCourse(int id)
        {
            CourseVM courseVM = new CourseVM
            {
                Courses = _context.Courses.Include(c => c.CourseTags).ThenInclude(ct => ct.Tag).Where(c => c.CourseTags.All(ct => ct.TagId == id)).ToList()
            };

            List<Course> courses = _context.Courses.Include(c=>c.CourseTags).Where(c=>c.CourseTags.Any(ct=>ct.TagId==id)).ToList();
            

            if (courseVM==null)
            {
                return NotFound();
            }
            return View(courses);
        }

        public IActionResult Search(string Name)
        {
            List<Course> courses = _context.Courses.Where(c => c.Name.ToLower().Contains(Name.ToLower())).ToList();
            return View(courses);
        }
    }
}
