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
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class CommentController : Controller
    {
        private readonly AppDbContext _context;
        public CommentController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult EventComments(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Comments.Where(c => c.EventId != null).Count() / 4);
            List<Comment> comments = _context.Comments.Include(c=>c.AppUser).Include(c => c.Event).Where(c => c.EventId != null).Skip((page-1)*4).Take(4).ToList();
            return View(comments);
        }

        public IActionResult CourseComments(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Comments.Where(c => c.CourseId != null).Count() / 4);
            List<Comment> comments = _context.Comments.Include(c => c.AppUser).Include(c => c.Course).Where(c => c.CourseId != null).Skip((page-1)*4).Take(4).ToList();
            return View(comments);
        }

        public IActionResult BlogComments(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Comments.Where(c => c.BlogId != null).Count() / 4);
            List<Comment> comments = _context.Comments.Include(c => c.AppUser).Include(c => c.Blog).Where(c => c.BlogId != null).Skip((page-1)*4).Take(4).ToList();
            return View(comments);
        }

        public IActionResult CommentStatus(int id)
        {
            if (!_context.Comments.Any(c => c.Id == id))
            {
                return RedirectToAction("CourseComments", "Comment");
            }

            Comment comment = _context.Comments.FirstOrDefault(c => c.Id == id);
            comment.IsAccepted = comment.IsAccepted ? false : true;

            if (comment.EventId == null && comment.BlogId==null)
            {
                _context.SaveChanges();

                return RedirectToAction("CourseComments", "Comment");

            }
            if (comment.EventId == null && comment.CourseId == null)
            {
                _context.SaveChanges();

                return RedirectToAction("BlogComments", "Comment");
            }

            _context.SaveChanges();

            return RedirectToAction("EventComments", "Comment");



        }

    }
}
