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
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        public AboutController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            AboutVM about = new AboutVM
            {
                Teachers = _context.Teachers.Include(t=>t.SocialMedias).Take(4).ToList(),
                NoticeBoard = _context.NoticeBoards.FirstOrDefault(),
                NoticeBoards = _context.NoticeBoards.ToList(),
                WelcomeEdu = _context.WelcomeEdus.FirstOrDefault()

            };
            return View(about);
        }
    }
}
