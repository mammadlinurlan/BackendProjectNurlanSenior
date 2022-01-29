using BackendProjectNurlanSenior.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.ViewModels
{
    public class AboutVM
    {
        public List<Teacher> Teachers { get; set; }
        public List<NoticeBoard> NoticeBoards { get; set; }

        public NoticeBoard NoticeBoard { get; set; }
        public WelcomeEdu WelcomeEdu { get; set; }
    }
}
