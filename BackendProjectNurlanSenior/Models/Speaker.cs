using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Models
{
    public class Speaker
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength:50)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]

        public string Speciality { get; set; }

        //[Required]
        [StringLength(maximumLength:120)]
        public string Image { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public List<EventSpeaker> EventSpeakers { get; set; }
    }
}
