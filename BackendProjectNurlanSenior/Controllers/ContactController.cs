using BackendProjectNurlanSenior.Dal;
using BackendProjectNurlanSenior.Models;
using BackendProjectNurlanSenior.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        public ContactController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ContactVM contactVM = new ContactVM
            {
                Contact = _context.Contacts.FirstOrDefault(),
                ContactMessages = _context.ContactMessages.ToList()
            };
            return View(contactVM);
        }

      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ContactMessage contactMessage)
        {
            ContactVM contactVM = new ContactVM
            {
                Contact = _context.Contacts.FirstOrDefault(),
                ContactMessages = _context.ContactMessages.ToList()
            };
           
            if (!ModelState.IsValid)
            {
                
                
                return View(contactVM);

            }
            ContactMessage newMessage = new ContactMessage
            {
                Email = contactMessage.Email,
                Message = contactMessage.Message,
                Name = contactMessage.Name,
                Subject = contactMessage.Subject
            };

            _context.ContactMessages.Add(newMessage);
            _context.SaveChanges();

            return RedirectToAction("Index","Home");


        }
    }

  
}
