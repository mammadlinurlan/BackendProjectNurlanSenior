using BackendProjectNurlanSenior.Dal;
using BackendProjectNurlanSenior.Extensions;
using BackendProjectNurlanSenior.Models;
using BackendProjectNurlanSenior.ViewModels;
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
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        public CourseController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {

            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Courses.Count() / 3);
            CourseFeatureVM courseFeature = new CourseFeatureVM
            {
                Courses = _context.Courses.Include(c => c.Features).Skip((page - 1) * 3).Take(3).ToList()
            };
            return View(courseFeature);
        }

        public IActionResult Create()
        {
            ViewBag.CategoryIds = _context.CCategories.ToList();
            ViewBag.TagIds = _context.Tags.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(CourseFeatureVM courseFeature)
        {
            //return Json(courseFeature.Feature);
            if (courseFeature.Course.CCategoryId == 0)
            {
                courseFeature.Course.CCategoryId = null;
            }

            if (!ModelState.IsValid)
            {
                return View();
            }


            ViewBag.CategoryIds = _context.CCategories.ToList();
            ViewBag.TagIds = _context.Tags.ToList();




            if (courseFeature.Course.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Select image please!");
                return View();

            }
            if (!courseFeature.Course.ImageFile.IsSizeOkay(2))
            {
                ModelState.AddModelError("ImageFile", "Image size must be less than 2MB");
                return View();

            }
            if (!courseFeature.Course.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Select image file!");
                return View();
            }

            courseFeature.Course.Image = courseFeature.Course.ImageFile.SaveImg(_env.WebRootPath, "assets/img/course");


            courseFeature.Course.CourseTags = new List<CourseTag>();
            if (courseFeature.Course.TagIds != null)
            {
                foreach (int item in courseFeature.Course.TagIds)
                {
                    CourseTag courseTag = new CourseTag
                    {
                        TagId = item,
                        Course = courseFeature.Course
                    };

                    courseFeature.Course.CourseTags.Add(courseTag);
                }

            }


            courseFeature.Course.Features = courseFeature.Feature;
            courseFeature.Feature.Course = courseFeature.Course;
            courseFeature.Feature.CourseId = courseFeature.Course.Id;
            _context.Courses.Add(courseFeature.Course);
            _context.Features.Add(courseFeature.Feature);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(int id)
        {
            ViewBag.CategoryIds = _context.CCategories.ToList();
            ViewBag.TagIds = _context.Tags.ToList();

            CourseFeatureVM courseFeatureVM = new CourseFeatureVM
            {
                Course = _context.Courses.Include(c => c.CourseTags).ThenInclude(ct => ct.Tag).Include(c => c.CCategory).FirstOrDefault(c => c.Id == id),

                Feature = _context.Features.Include(f=>f.Course).FirstOrDefault(f => f.CourseId == id)
            };

            return View(courseFeatureVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CourseFeatureVM courseFeature,int id)
        {
          
            

            ViewBag.CategoryIds = _context.CCategories.ToList();
            ViewBag.TagIds = _context.Tags.Include(t=>t.CourseTags).ThenInclude(ct=>ct.Course).ToList();
            
           


            if (!ModelState.IsValid)
            {
                return View(courseFeature);
            }

            Course existedCourse = _context.Courses.Include(f=>f.Features).Include(c => c.CourseTags).ThenInclude(ct => ct.Tag).Include(c => c.CCategory).FirstOrDefault(c => c.Id == id);
            if (existedCourse==null)
            {
                return NotFound();
            }

            if (courseFeature.Course.ImageFile!=null)
            {
                if (!courseFeature.Course.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size must be less than 2MB");
                    return View(courseFeature);

                }
                if (!courseFeature.Course.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Select image file!");
                    return View(courseFeature);
                }

                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/course", existedCourse.Image);
                existedCourse.Image = courseFeature.Course.ImageFile.SaveImg(_env.WebRootPath, "assets/img/course");
            }

            if (courseFeature.Course.TagIds!=null)
            {
                //return Json(courseFeature.Course.TagIds);


                foreach (var tagId in courseFeature.Course.TagIds)
                {
                    CourseTag courseTag = existedCourse.CourseTags.FirstOrDefault(ct => ct.TagId == tagId);
                    if (courseTag == null)
                    {
                        CourseTag ctag = new CourseTag
                        {
                            TagId = tagId,
                            CourseId = existedCourse.Id
                        };
                        existedCourse.CourseTags.Add(ctag);
                        //return Json(ctag);
                    }
                }
            }
            else
            {
                var selectedTags = _context.CourseTags.Where(ct => ct.CourseId == existedCourse.Id);
                _context.CourseTags.RemoveRange(selectedTags);
            }

            if (courseFeature.Course.CCategoryId==0)
            {
                courseFeature.Course.CCategoryId = null;
            }
            existedCourse.About = courseFeature.Course.About;
            existedCourse.CCategoryId = courseFeature.Course.CCategoryId;
            existedCourse.Certification = courseFeature.Course.Certification;

            existedCourse.Description = courseFeature.Course.Description;
            existedCourse.HowToApply = courseFeature.Course.HowToApply;

            existedCourse.LeaveReply = courseFeature.Course.LeaveReply;

            existedCourse.LinkLogo = courseFeature.Course.LinkLogo;
            existedCourse.Name = courseFeature.Course.Name;
            existedCourse.Features.Assesments = courseFeature.Feature.Assesments;
            existedCourse.Features.ClassDuration = courseFeature.Feature.ClassDuration;

            existedCourse.Features.Duration = courseFeature.Feature.Duration;

            existedCourse.Features.Fee = courseFeature.Feature.Fee;
            existedCourse.Features.Language = courseFeature.Feature.Language;

            existedCourse.Features.SkillLevel = courseFeature.Feature.SkillLevel;
            existedCourse.Features.StartTime = courseFeature.Feature.StartTime;

            existedCourse.Features.StudentCount = courseFeature.Feature.StudentCount;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            Course existedCourse = _context.Courses.Include(c => c.CourseTags).ThenInclude(ct => ct.Tag).Include(c => c.CCategory).FirstOrDefault(c=>c.Id == id);

            if (existedCourse==null)
            {
                return Json(new { status = 400 });
            }
            if (existedCourse.Image != null)
            {
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/course", existedCourse.Image);
            }
                _context.Courses.Remove(existedCourse);

            Features feature = _context.Features.FirstOrDefault(f => f.CourseId == existedCourse.Id);
            _context.Features.Remove(feature);
            _context.SaveChanges();
            return Json(new { status = 200 });







        }

    }
}
