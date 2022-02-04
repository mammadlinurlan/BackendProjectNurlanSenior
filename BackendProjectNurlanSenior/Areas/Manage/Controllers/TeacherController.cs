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
{

    [Area("Manage")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        public TeacherController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Teachers.Count() / 4);

            List<Teacher> teachers = _context.Teachers.Include(t => t.TeacherHobbies).ThenInclude(th => th.Teacher).Skip((page-1)*4).Take(4).ToList();
            return View(teachers);
        }

        public IActionResult Create()
        {
            ViewBag.SocialLinks = _context.SocialMedias.ToList();
            ViewBag.Hobbies = _context.Hobbies.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Teacher teacher)
        {

            ViewBag.Hobbies = _context.Hobbies.ToList();
            if (!ModelState.IsValid) return View();
            

            teacher.TeacherHobbies = new List<TeacherHobbies>();

          

            if (teacher.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Select image please!");
                return View();

            }
            if (!teacher.ImageFile.IsSizeOkay(2))
            {
                ModelState.AddModelError("ImageFile", "Image size must be less than 2MB");
                return View();

            }
            if (!teacher.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Select image file!");
                return View();

            }
            if (teacher.HobbyIds != null)
            {
                foreach (int item in teacher.HobbyIds)
                {
                    TeacherHobbies hobbies = new TeacherHobbies
                    {
                        Teacher = teacher,
                        HobbyId = item
                    };
                    teacher.TeacherHobbies.Add(hobbies);

                }
            }
         

           

            teacher.Image = teacher.ImageFile.SaveImg(_env.WebRootPath, "assets/img/teacher");
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
            //return Content(teacher.TeacherHobbies.FirstOrDefault().ToString());
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Hobbies = _context.Hobbies.Include(h=>h.TeacherHobbies).ThenInclude(th=>th.Teacher).ToList();

            Teacher teacher = _context.Teachers.Include(t => t.TeacherHobbies).ThenInclude(th => th.Hobby).FirstOrDefault(t => t.Id == id);
            if (teacher==null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Teacher teacher)
        {
            ViewBag.Hobbies = _context.Hobbies.Include(h => h.TeacherHobbies).ThenInclude(th => th.Teacher).ToList();


            if (!ModelState.IsValid)
            {
                return View(teacher);
            }

            Teacher existedTeacher = _context.Teachers.Include(t => t.TeacherHobbies).ThenInclude(th => th.Hobby).FirstOrDefault(t => t.Id == teacher.Id);
           
            if (existedTeacher==null)
            {
                return NotFound();
            }
           


            if (teacher.ImageFile != null)
            {
                if (!teacher.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Select an image file");
                    return View(teacher);
                }
                if (!teacher.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size must be less than 2MB");
                    return View(teacher);
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/teacher", existedTeacher.Image);

                existedTeacher.Image = teacher.ImageFile.SaveImg(_env.WebRootPath, "assets/img/teacher");


            }
          
        

            if (teacher.HobbyIds != null)
            {
               
                foreach (var item in teacher.HobbyIds)
                {
                    TeacherHobbies teacherHobby = existedTeacher.TeacherHobbies.FirstOrDefault(th => th.HobbyId == item);

                    if (teacherHobby == null)
                    {
                        TeacherHobbies hobbies = new TeacherHobbies
                        {
                            HobbyId = item,
                            TeacherId = existedTeacher.Id
                        };

                       
                        existedTeacher.TeacherHobbies.Add(hobbies);
                    }
                }


            }
            else
            {
                var selectedHobbies = _context.TeacherHobbies.Where(n => n.TeacherId == existedTeacher.Id);
                _context.TeacherHobbies.RemoveRange(selectedHobbies);
            }



            existedTeacher.Fullname = teacher.Fullname;
            existedTeacher.About = teacher.About;
            existedTeacher.Degree = teacher.Degree;
            existedTeacher.Email = teacher.Email;
            existedTeacher.Experience = teacher.Experience;
            existedTeacher.Faculty = teacher.Faculty;
            existedTeacher.Phone = teacher.Phone;
            existedTeacher.Skype = teacher.Skype;
            existedTeacher.Speciality = teacher.Speciality;
            existedTeacher.Fblink = teacher.Fblink;
            existedTeacher.PinterestLink = teacher.PinterestLink;
            existedTeacher.Instalink = teacher.Instalink;
            existedTeacher.Vimeolink = teacher.Vimeolink;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));


        }

        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            Teacher teacher = _context.Teachers.FirstOrDefault(t => t.Id == id);
            if (teacher==null)
            {
                return NotFound();
            }
            Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/teacher", teacher.Image);
            _context.Teachers.Remove(teacher);
            _context.SaveChanges();
            return Json(new { status = 200 });

               

        }
    }
}
