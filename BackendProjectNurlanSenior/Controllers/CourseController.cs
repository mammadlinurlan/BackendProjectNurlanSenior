using BackendProjectNurlanSenior.Dal;
using BackendProjectNurlanSenior.Models;
using BackendProjectNurlanSenior.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;
        public CourseController(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;

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
                Course = _context.Courses.Include(c => c.Comments).ThenInclude(c => c.AppUser).Include(c => c.Features).Include(c => c.CourseTags).ThenInclude(ct => ct.Tag).Include(c => c.CCategory).FirstOrDefault(c => c.Id == id),

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
           

            List<Course> courses = Name==null ? _context.Courses.ToList() : _context.Courses.Where(c => c.Name.ToLower().Contains(Name.ToLower())).ToList();


            return View(courses);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return Json(StatusCode(400));
            if (!_context.Courses.Any(f => f.Id == comment.CourseId))
            {
                return NotFound();
            }
            Comment cmnt = new Comment
            {
                AppUserId = user.Id,
                CourseId = comment.CourseId,
                CreatedTime = DateTime.Now,
                Message = comment.Message,
                Subject=comment.Subject,
                IsAccepted = true
            };
            _context.Comments.Add(cmnt);
            _context.SaveChanges();
            return RedirectToAction("Details", "Course", new { id = cmnt.CourseId });
            //return RedirectToAction("Index", "Home");

            //CourseVM course = new CourseVM
            //{
            //    Course = _context.Courses.Include(c=>c.Comments).ThenInclude(c=>c.AppUser).FirstOrDefault()
            //};
            //return PartialView("_CommentPartialView",course);
            //return View();
        }

        public async Task<IActionResult> DeleteComment(int id)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return RedirectToAction("Details", "Course");
            if (!_context.Comments.Any(c => c.Id == id && c.AppUserId == user.Id)) return NotFound();
            Comment comment = _context.Comments.FirstOrDefault(c => c.Id == id && c.AppUserId == user.Id);
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return Json(new {status = 200 });



        }
    }
}
