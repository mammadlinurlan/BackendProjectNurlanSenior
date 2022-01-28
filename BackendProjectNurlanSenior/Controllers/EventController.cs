using BackendProjectNurlanSenior.Dal;
using BackendProjectNurlanSenior.Models;
using BackendProjectNurlanSenior.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BackendProjectNurlanSenior.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        public EventController(AppDbContext context)
        {
            _context = context;
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
                Event = _context.Events.Include(e=>e.EventSpeakers).ThenInclude(es=>es.Speaker).FirstOrDefault(e=>e.Id==id),
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
    }
}
