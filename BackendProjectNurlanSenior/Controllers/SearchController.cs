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
    public class SearchController : Controller
    {

        private readonly AppDbContext _context;
        public SearchController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string String)
        {
            AllVM all = new AllVM
            {
                Courses = String == null ? _context.Courses.ToList() : _context.Courses.Where(c => c.Name.ToLower().Contains(String.ToLower())).ToList(),
                Events = String == null ? _context.Events.ToList() : _context.Events.Where(e => e.Name.ToLower().Trim().Contains(String.ToLower().Trim())).ToList(),
                Blogs = String == null ? _context.Blogs.Include(b => b.Comments).ToList() : _context.Blogs.Include(b => b.Comments).Where(e => e.Name.ToLower().Trim().Contains(String.ToLower().Trim())).ToList(),
                Teachers = String == null ? _context.Teachers.ToList() : _context.Teachers.Where(t => t.Fullname.ToLower().Trim().Contains(String.ToLower().Trim())).ToList()
                

            };
            return View(all);
        }
    }
}
