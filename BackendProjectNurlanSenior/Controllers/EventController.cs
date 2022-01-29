﻿using BackendProjectNurlanSenior.Dal;
using BackendProjectNurlanSenior.Models;
using BackendProjectNurlanSenior.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public EventController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            EventVM eventVM = new EventVM
            {
                Events = _context.Events.ToList()
                
            };
            
            return View(eventVM);
        }

        public IActionResult Details(int id=1)
        {
            EventVM eventVM = new EventVM
            {
                Events=_context.Events.ToList(),
                Event = _context.Events.Include(e=>e.Comments).ThenInclude(c=>c.AppUser).Include(e=>e.EventSpeakers).ThenInclude(es=>es.Speaker).FirstOrDefault(e=>e.Id==id),
                Speakers = _context.Speakers.Where(s=>s.EventSpeakers.Any(es=>es.EventId==id)).ToList()
            };
            return View(eventVM);
        }

        public IActionResult SearchResult(string Name)
        {
            EventVM eventVM = new EventVM
            {
                Events = _context.Events.Where(e => e.Name.ToLower().Contains(Name.ToLower())).ToList()
            };
            ViewBag.LastEvents = _context.Events.OrderByDescending(e => e.Id).Take(3).ToList();
            return View(eventVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return Json(StatusCode(400));
            if (!_context.Events.Any(f => f.Id == comment.EventId))
            {
                return NotFound();
            }
            Comment cmnt = new Comment
            {
                AppUserId = user.Id,
                EventId = comment.EventId,
                CreatedTime = DateTime.Now,
                Message = comment.Message,
                Subject = comment.Subject,
                IsAccepted = true
            };
            _context.Comments.Add(cmnt);
            _context.SaveChanges();
            //return RedirectToAction("Index", "Home");
            return RedirectToAction("Details", "Event", new { id = cmnt.EventId });

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
            if (!ModelState.IsValid) return RedirectToAction("Details", "Event");
            if (!_context.Comments.Any(c => c.Id == id && c.AppUserId == user.Id)) return NotFound();
            Comment comment = _context.Comments.FirstOrDefault(c => c.Id == id && c.AppUserId == user.Id);
            EventVM eventVM = new EventVM
            {
                Event = _context.Events.Include(e => e.Comments).ThenInclude(c => c.AppUser).FirstOrDefault()
            };
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            //return RedirectToAction("Details", "Event", new { id = comment.EventId });
            return Json(new {status = 200 });

            //return PartialView("_eCommentPartialView", eventVM);



        }
    }
}
