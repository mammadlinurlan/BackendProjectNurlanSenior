using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [StringLength(maximumLength:100)]
        public string Address { get; set; }
        [StringLength(maximumLength: 25)]

        public string Phone { get; set; }

        [StringLength(maximumLength: 25)]
        public string Country { get; set; }

        [StringLength(maximumLength:200)]
        public string LeaveReply { get; set; }
        [StringLength(maximumLength: 50)]

        public string CountryLogo { get; set; }
        [StringLength(maximumLength: 50)]

        public string AdressLogo { get; set; }
        [StringLength(maximumLength: 50)]


        public string PhoneLogo { get; set; }

    }
}
