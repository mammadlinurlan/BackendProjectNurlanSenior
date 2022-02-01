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
{ [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        public EventController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Events.Count() / 3);
            List<Event> events = _context.Events.Include(e => e.EventSpeakers).ThenInclude(es=>es.Speaker).Skip((page-1)*3).Take(3).ToList();
            return View(events);
        }

        public IActionResult Create()
        {
            ViewBag.SpeakerIds = _context.Speakers.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event event1)
        {
            ViewBag.SpeakerIds = _context.Speakers.ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (event1.ImageFile==null)
            {
                ModelState.AddModelError("ImageFile", "select image please");
                return View();
            }
            if (!event1.ImageFile.IsSizeOkay(2))
            {
                ModelState.AddModelError("ImageFile", "Image size must be less than 2MB");
                return View();

            }
            if (!event1.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Select image file!");
                return View();
            }

           event1.Image = event1.ImageFile.SaveImg(_env.WebRootPath, "assets/img/event");

            event1.EventSpeakers = new List<EventSpeaker>();

            if (event1.SpeakerIds!=null)
            {
                foreach (int item in event1.SpeakerIds)
                {
                    EventSpeaker speaker = new EventSpeaker
                    {
                        EventId = event1.Id,
                        SpeakerId = item
                    };
                    event1.EventSpeakers.Add(speaker);
                }
            }

            _context.Events.Add(event1);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));



        }

        public IActionResult Edit(int id)
        {
            Event event1 = _context.Events.Include(e => e.EventSpeakers).ThenInclude(es => es.Event).FirstOrDefault(e => e.Id == id);

            //return Json(event1.Image);

            ViewBag.SpeakerIds = _context.Speakers.ToList();

            if (event1==null)
            {
                return NotFound();
            }
            return View(event1);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Event event1)
        {
            ViewBag.SpeakerIds = _context.Speakers.ToList();

            if (!ModelState.IsValid)
            {
                return View(event1);
            }

            Event existevent = _context.Events.Include(e => e.EventSpeakers).ThenInclude(es => es.Event).FirstOrDefault(e => e.Id==event1.Id);

            if (existevent==null)
            {
                return NotFound();
            }
            if (event1.ImageFile!=null)
            {
                if (!event1.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size must be less than 2MB");
                    return View(event1);

                }
                if (!event1.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Select image file!");
                    return View(event1);
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/event", existevent.Image);
                existevent.Image = event1.ImageFile.SaveImg(_env.WebRootPath, "assets/img/event");
            }

            if (event1.SpeakerIds!=null)
            {
                foreach (var item in event1.SpeakerIds)

                {
                    EventSpeaker eventSpeaker = existevent.EventSpeakers.FirstOrDefault(th => th.SpeakerId == item);
                    if (eventSpeaker==null)
                    {

                        EventSpeaker speaker = new EventSpeaker
                        {
                            SpeakerId = item,
                            EventId = existevent.Id
                        };
                        existevent.EventSpeakers.Add(speaker);

                    }

                }
            }
            else
            {
                var selected = _context.EventSpeakers.Where(es => es.EventId == existevent.Id);
                _context.EventSpeakers.RemoveRange(selected);
            }

            existevent.DayMonth = event1.DayMonth;
            existevent.StartTime = event1.StartTime;

            existevent.EndTime = event1.EndTime;
            existevent.Name = event1.Name;

            existevent.Desc = event1.Desc;

            existevent.LeaveReply = event1.LeaveReply;
            existevent.Venue = event1.Venue;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
           


            //return Json(id);
            if (!ModelState.IsValid)
            {
                return Json(new {status=400 });

            }
            Event existevent = _context.Events.Include(e => e.EventSpeakers).ThenInclude(es => es.Speaker).FirstOrDefault(e => e.Id == id);
            //var speaker = _context.EventSpeakers.Where(s => s.EventId == id);

            //List<Comment> comments = _context.Comments.Where(c => c.EventId == id).ToList();
            //foreach (Comment cmnt in comments)
            //{
            //    _context.Comments.Remove(cmnt);
            //}

            if (existevent == null)
            {
                return NotFound();
            }

            Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/event", existevent.Image);
            _context.Events.Remove(existevent);
            //_context.EventSpeakers.RemoveRange(speaker);
            _context.SaveChanges();
            return Json(new { status = 200 });



            
        }

    }
}
