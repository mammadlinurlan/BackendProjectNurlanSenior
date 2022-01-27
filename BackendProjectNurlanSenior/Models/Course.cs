using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        public string Image { get; set; }

        [Required]
        [StringLength(maximumLength: 550)]
        public string Description { get; set; }

        [Required]
        [StringLength(maximumLength: 450)]
        public string About { get; set; }

        [Required]
        [StringLength(maximumLength: 450)]
        public string Certification { get; set; }

        [Required]
        [StringLength(maximumLength: 450)]
        public string HowToApply { get; set; }

        [Required]
        [StringLength(maximumLength: 450)]
        public string LeaveReply { get; set; }

        [Required]
        [StringLength(maximumLength:50)]
        public string LinkLogo { get; set; }

        public Features Features { get; set; }


    }
}
