using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Models
{
    public class Slider
    {
        public int Id { get; set; }

        [StringLength(maximumLength:150)]


        public string Image { get; set; }
        [StringLength(maximumLength: 150)]
        public string Title { get; set; }
        [StringLength(maximumLength: 150)]
        public string Description { get; set; }

        [StringLength(maximumLength: 150)]

        public string LearnMoreLink { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
