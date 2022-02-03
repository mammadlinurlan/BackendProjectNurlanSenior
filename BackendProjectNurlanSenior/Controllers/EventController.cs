using BackendProjectNurlanSenior.Dal;
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

            if (!_context.Events.Any(e=>e.Id==id))
            {
                return NotFound();
            }

            return View(eventVM);
        }

        public IActionResult SearchResult(string Name)
        {
            EventVM eventVM = new EventVM
            {
                Events =  Name==null ? _context.Events.ToList() : _context.Events.Where(e => e.Name.ToLower().Trim().Contains(Name.ToLower().Trim())).ToList()
            };
            ViewBag.LastEvents = _context.Events.OrderByDescending(e => e.Id).Take(3).ToList();
            return View(eventVM);
        }

        public IActionResult LiveSearch(string Name)
        {


            EventVM eventVM = new EventVM
            {
                Events = Name == null ? _context.Events.ToList() : _context.Events.Where(e => e.Name.ToLower().Trim().Contains(Name.ToLower().Trim())).ToList()
            };

            return PartialView("_EventPartialView", eventVM);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(string Subject, string Message, int EventId)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return View();
            //if (!_context.Events.Any(f => f.Id == comment.EventId))
            //{
            //    return NotFound();
            //}
            Comment cmnt = new Comment
            {
                AppUserId = user.Id,
                EventId = EventId,
                CreatedTime = DateTime.Now,
                Message =Message,
                Subject = Subject,
                IsAccepted = true
            };
            _context.Comments.Add(cmnt);
            _context.SaveChanges();

            EventVM eventVM = new EventVM
            {
                Event = _context.Events.Include(b => b.Comments).ThenInclude(b => b.Blog).FirstOrDefault(b => b.Id == EventId)
            };

            return PartialView("_eCommentPartialView",eventVM);

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
