using BackendProjectNurlanSenior.Dal;
using BackendProjectNurlanSenior.Extensions;
using BackendProjectNurlanSenior.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
    public class BlogController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        public BlogController(AppDbContext context, IWebHostEnvironment env,UserManager<AppUser> userManager)
        {
            _context = context;
            _env = env;
            _userManager = userManager;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Blogs.Count() / 3);
            List<Blog> blogs = _context.Blogs.Skip((page - 1) * 3).Take(3).ToList();
            return View(blogs);
        }

        [Authorize(Roles ="SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Blog blog)
        {
            if (blog.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Select image please!");
                return View();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user =await _userManager.FindByNameAsync(User.Identity.Name);
            

           
            

           

            if (!blog.ImageFile.IsSizeOkay(2))
            {
                ModelState.AddModelError("ImageFile", "Image size must be less than 2mb");
                return View();
            }
            if (!blog.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Select an image file! ");
                return View();
            }

            blog.Image = blog.ImageFile.SaveImg(_env.WebRootPath, "assets/img/blog");
            blog.Author = user.Fullname;
            blog.CreatedTime = DateTime.Now;
            _context.Blogs.Add(blog);
            _context.SaveChanges();
            return Content("okay");

        }

        public IActionResult Edit(int id)
        {
            Blog blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog==null)
            {
                return NotFound();
            }
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
          public IActionResult Edit(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return View(blog);
            }

            Blog existedBlog = _context.Blogs.FirstOrDefault(b => b.Id == blog.Id);
            if (existedBlog==null)
            {
                return NotFound();
            }

            if (blog.ImageFile!=null)
            {
                if (!blog.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "image file must be less than 2mb");
                    return View(blog);
                }
                if (!blog.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "select an image file");
                    return View(blog);
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/blog", existedBlog.Image);
                existedBlog.Image = blog.ImageFile.SaveImg(_env.WebRootPath, "assets/img/blog");

            }
            existedBlog.CreatedTime = DateTime.Now;
            existedBlog.LeaveReply = blog.LeaveReply;
            existedBlog.Name = blog.Name;
            existedBlog.BlackQuote = blog.BlackQuote;
            existedBlog.Description = blog.Description;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Blog blog = _context.Blogs.Include(b=>b.Comments).FirstOrDefault(s => s.Id == id);
            List<Comment> comments = _context.Comments.Where(c => c.BlogId == id).ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (blog == null)
            {
                return Json(new { status = 400 });
            }
            Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/blog", blog.Image);
            foreach (Comment item in comments)
            {

                _context.Comments.Remove(item);

            }
            _context.Blogs.Remove(blog);

            
            _context.SaveChanges();
            return Json(new { status = 200 });
        }
    }
}
