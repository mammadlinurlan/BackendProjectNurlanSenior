using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Models
{
    public class WelcomeEdu
    {
        public int Id { get; set; }
        [StringLength(maximumLength:50)]
        public string Title { get; set; }
        [StringLength(maximumLength: 50)]

        public string TitleColored { get; set; }
        [StringLength(maximumLength:100)]

        public string Image { get; set; }
        [StringLength(maximumLength: 500)]
        public string Description { get; set; }
        public string Link { get; set; }
    }
}
