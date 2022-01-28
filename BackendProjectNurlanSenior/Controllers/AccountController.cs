using BackendProjectNurlanSenior.Models;
using BackendProjectNurlanSenior.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = await _userManager.FindByNameAsync(register.Username);
            if (user==null)
            {
                user = new AppUser
                {
                    UserName = register.Username,
                    Fullname = register.Fullname,
                    Email = register.Email
                };
                if (register.Username == null)
                {
                    ModelState.AddModelError("Username", "Please fill this field");
                    return View();

                }
                if (!register.TermsAndConditions)
                {
                    ModelState.AddModelError("TermsAndConditions", "Please fill this box!");
                    return View();
                }
                IdentityResult result = await _userManager.CreateAsync(user, register.Password);
                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View();
                }

            }
            else
            {
                ModelState.AddModelError("", "This username already taken");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

    }
}
