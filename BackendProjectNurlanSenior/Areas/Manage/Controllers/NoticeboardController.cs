using BackendProjectNurlanSenior.Dal;
using BackendProjectNurlanSenior.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class NoticeboardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInResult;
        private readonly AppDbContext _context;

        public NoticeboardController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInResult, AppDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInResult = signInResult;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.NoticeBoards.Count() / 5);
            List<NoticeBoard> noticeBoards = _context.NoticeBoards.Skip((page - 1) * 5).Take(5).ToList();
            return View(noticeBoards);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NoticeBoard board)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.NoticeBoards.Add(board);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            NoticeBoard notice = _context.NoticeBoards.FirstOrDefault(n => n.Id == id);
            if (notice==null)
            {
                return NotFound();
            }

            return View(notice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NoticeBoard board)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            NoticeBoard existedBoard = _context.NoticeBoards.FirstOrDefault(n => n.Id == board.Id);
            if (existedBoard==null)
            {
                return NotFound();
            }

            existedBoard.Question = board.Question;
            existedBoard.Answer = board.Answer;

            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            NoticeBoard notice = _context.NoticeBoards.FirstOrDefault(t => t.Id == id);
            if (notice == null)
            {
                return Json(new { status = 400 });
            }

            _context.NoticeBoards.Remove(notice);
            _context.SaveChanges();
            return Json(new { status = 200 });


        }

    }
}
