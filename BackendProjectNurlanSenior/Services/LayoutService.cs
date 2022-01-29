using BackendProjectNurlanSenior.Dal;
using BackendProjectNurlanSenior.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;
        public LayoutService(AppDbContext context)
        {
            _context = context;
        }

        public Settings GetSettings()
        {
            Settings data = _context.Settings.FirstOrDefault();
            return data;
        }
    }
}
