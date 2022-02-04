using BackendProjectNurlanSenior.Dal;
using BackendProjectNurlanSenior.Models;
using BackendProjectNurlanSenior.ViewModels;
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
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInResult;
        private readonly AppDbContext _context;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInResult,AppDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInResult = signInResult;
        }


        [Authorize(Roles = "SuperAdmin")]
        public IActionResult UserList()
        {

            List<AppUser> users = _userManager.Users.ToList();

            return View(users);
        }
        [Authorize(Roles = "SuperAdmin")]


       
        public async Task<IActionResult> AdminStatus(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (!ModelState.IsValid) return RedirectToAction("UserList", "Account");
            if (user == null)
            {
                return NotFound();
            }
            if (await _userManager.IsInRoleAsync(user, "SuperAdmin") == true)
            {
                return Content("you cannot touch superadmin!");
            }


            if (!user.IsAdmin == true)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                user.IsAdmin = true;
            }
            else
            {
                await _userManager.RemoveFromRoleAsync(user, "Admin");
                user.IsAdmin = false;
            }


            await _context.SaveChangesAsync();
            return RedirectToAction("UserList", "Account");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInResult.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = await _userManager.FindByNameAsync(login.Username);

            if (user == null)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View();
            }
            if (!user.IsAdmin)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View();
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInResult.PasswordSignInAsync(user, login.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View();
            }
            return RedirectToAction("Index", "Dashboard");

        }


        public IActionResult Testuser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { status = 400 });
            }

            return Json(new { status = 200 });
        }

        //public async Task CreateRole()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        //    await _roleManager.CreateAsync(new IdentityRole("Admin"));
        //    await _roleManager.CreateAsync(new IdentityRole("Member"));
        //}

        //public async Task CreateAdmin()
        //{
        //    AppUser user = new AppUser
        //    {
        //        UserName = "Nurlanmammadli",
        //        Email = "nurlanym@code.edu.az",
        //        Fullname = "Nurlan Mammadli",
        //        EmailConfirmed = true
        //    };

        //    await _userManager.CreateAsync(user, "nurlan123");
        //    await _userManager.AddToRoleAsync(user, "SuperAdmin");

        //}
    }
}
