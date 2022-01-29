using BackendProjectNurlanSenior.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.ViewModels
{
    public class HomeVM
    {

        public List<Slider> Sliders { get; set; }
        public WelcomeEdu WelcomeEdu { get; set; }

        public List<Course> Courses { get; set; }

        public List<NoticeBoard> NoticeBoards { get; set; }

        public List<Event> Events { get; set; }
    }
}
