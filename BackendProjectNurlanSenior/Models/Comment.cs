using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        [Required]
        [StringLength(maximumLength:350)]
        public string Message { get; set; }
        [Required]
        [StringLength(maximumLength:60)]
        public string Subject { get; set; }

        public bool IsAccepted { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int? CourseId { get; set; }
        public Course Course { get; set; }
        public int? EventId { get; set; }
        public Event Event { get; set; }
    }
}
