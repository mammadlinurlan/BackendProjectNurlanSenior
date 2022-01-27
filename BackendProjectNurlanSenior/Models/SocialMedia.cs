using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Models
{
    public class SocialMedia
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 25)]

        public string Name { get; set; }

        [StringLength(maximumLength: 110)]
        public string Logo { get; set; }

        [StringLength(maximumLength: 110)]

        public string Link { get; set; }

        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }

    }
}
