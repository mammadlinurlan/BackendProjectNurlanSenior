using BackendProjectNurlanSenior.Dal;
using BackendProjectNurlanSenior.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM home = new HomeVM
            {
                Sliders = _context.Sliders.ToList(),
                WelcomeEdu = _context.WelcomeEdus.FirstOrDefault(),
                Courses = _context.Courses.OrderByDescending(c => c.Id).Take(3).ToList(),
                NoticeBoards = _context.NoticeBoards.ToList(),
                Events = _context.Events.ToList(),
                Blogs = _context.Blogs.Include(b => b.Comments).ToList()

            };
            return View(home);
        }
    }
}
