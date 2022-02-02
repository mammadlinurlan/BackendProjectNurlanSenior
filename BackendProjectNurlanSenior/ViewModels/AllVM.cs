using BackendProjectNurlanSenior.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.ViewModels
{
    public class AllVM
    {
        public List<Teacher> Teachers { get; set; }
        public List<Course> Courses { get; set; }

        public List<Event> Events { get; set; }
        public List<Blog> Blogs { get; set; }


        


    }
}
