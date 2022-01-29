using BackendProjectNurlanSenior.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.ViewModels
{
    public class ContactVM
    {
        public Contact Contact { get; set; }

        public List<ContactMessage> ContactMessages { get; set; }
    }
}
