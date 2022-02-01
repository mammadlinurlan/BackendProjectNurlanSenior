using BackendProjectNurlanSenior.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.ViewModels
{
    public class CourseFeatureVM
    {
        public List<Course> Courses { get; set; }
        public List<Features> Features { get; set; }

        public Course Course { get; set; }
        public Features Feature { get; set; }
    }
}
