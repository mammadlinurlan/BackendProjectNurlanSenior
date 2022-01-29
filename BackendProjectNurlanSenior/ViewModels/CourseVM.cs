using BackendProjectNurlanSenior.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.ViewModels
{
    public class CourseVM
    {
        public List<Course> Courses { get; set; }
        public Course Course { get; set; }

        public List<Tag> Tags { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
