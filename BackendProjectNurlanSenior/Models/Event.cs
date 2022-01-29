using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public DateTime DayMonth { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]

        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]

        public string Venue { get; set; }
        [Required]
        [StringLength(maximumLength: 2000)]

        public string Desc { get; set; }
        [Required]
        [StringLength(maximumLength: 300)]


        public string LeaveReply { get; set; }

        public List<EventSpeaker> EventSpeakers { get; set; }
        [Required]
        [StringLength(maximumLength:150)]
        public string Image { get; set; }

        public List<Comment> Comments { get; set; }

    }
}
