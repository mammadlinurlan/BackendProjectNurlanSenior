using BackendProjectNurlanSenior.Dal;
using BackendProjectNurlanSenior.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ContactController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        public ContactController(AppDbContext context, IWebHostEnvironment env, UserManager<AppUser> userManager)
        {
            _context = context;
            _env = env;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            Contact contact = _context.Contacts.FirstOrDefault();
            return View(contact);
        }

        public IActionResult Messages()
        {
            List<ContactMessage> contactMessages = _context.ContactMessages.ToList();
            return View(contactMessages);
        }

        public IActionResult Edit(int id)
        {
            Contact contact = _context.Contacts.FirstOrDefault(c => c.Id == id);
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Contact contact)
        {
            Contact exist = _context.Contacts.FirstOrDefault(c => c.Id == contact.Id);
            if (!ModelState.IsValid)
            {
                return View(contact);

            }

            exist.Address = contact.Address;
            exist.AdressLogo = contact.AdressLogo;
            exist.Country = contact.Country;
            exist.CountryLogo = contact.CountryLogo;
            exist.Phone = contact.Phone;
            exist.PhoneLogo = contact.PhoneLogo;
            exist.LeaveReply = contact.LeaveReply;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
