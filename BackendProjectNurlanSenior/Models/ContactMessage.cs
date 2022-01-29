using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength:50)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Subject { get; set; }
        [Required]
        [StringLength(maximumLength: 400)]
        public string Message { get; set; }
        [Required]
        [StringLength(maximumLength: 75)]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }
    }
}
