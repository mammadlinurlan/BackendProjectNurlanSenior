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
    public class WelcomeEduController : Controller
    {

        public WelcomeEduController()
        {
                private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public WelcomeEduController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            WelcomeEdu welcomeEdu = _context.WelcomeEdus.FirstOrDefault();
            return View(welcomeEdu);
        }
    }

        public IActionResult Index()
        {
            return View();
        }
    }
}
