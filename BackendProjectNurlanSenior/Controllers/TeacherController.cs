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
        public IActionResult Index()
        {

            List<Teacher> teachers = _context.Teachers.Include(t=>t.SocialMedias).ToList();

            return View(teachers);
        }

        public IActionResult Details(int id)
        {
            Teacher teacher = _context.Teachers.Include(t=>t.SocialMedias).Include(t=>t.TeacherHobbies).ThenInclude(th=>th.Hobby).FirstOrDefault(t => t.Id == id);
            return View(teacher);
        }
    }
}
