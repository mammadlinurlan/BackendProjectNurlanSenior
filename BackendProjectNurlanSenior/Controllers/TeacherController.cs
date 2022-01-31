using BackendProjectNurlanSenior.Dal;
using BackendProjectNurlanSenior.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        public TeacherController(AppDbContext context)
        {

            _context = context;

        }
        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Teachers.Count() / 4);
            List<Teacher> teachers = _context.Teachers.Skip((page-1)*4).Take(4).ToList();

            return View(teachers);
        }

        public IActionResult Details(int id)
        {
            Teacher teacher = _context.Teachers.Include(t=>t.TeacherHobbies).ThenInclude(th=>th.Hobby).FirstOrDefault(t => t.Id == id);
            return View(teacher);
        }
    }
}
