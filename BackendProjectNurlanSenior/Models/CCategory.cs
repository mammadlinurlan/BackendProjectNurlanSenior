using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Models
{
    public class CCategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength:25)]
        public string Name { get; set; }

        public List<Course> Courses { get; set; }
    }
}
