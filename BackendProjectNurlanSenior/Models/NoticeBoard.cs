using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Models
{
    public class NoticeBoard
    {
        public int Id { get; set; }
        [StringLength(maximumLength:150)]
        [Required]
        public string Question { get; set; }
        [Required]
        [StringLength(maximumLength: 350)]

        public string Answer { get; set; }

        [StringLength(maximumLength:150)]
        public string VideoUrl { get; set; }
    }
}
