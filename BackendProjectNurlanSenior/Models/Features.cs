using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Models
{
    public class Features
    {
        public int Id { get; set; }
        [Required]
      
        public DateTime StartTime { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public string Duration { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string ClassDuration { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string SkillLevel { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]

        public string Language { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]

        public string Assesments { get; set; }

        [Required]
        public int StudentCount { get; set; }
        [Required]

        public int Fee { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }


    }
}
