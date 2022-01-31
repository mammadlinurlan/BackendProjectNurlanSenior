using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [StringLength(maximumLength:50)]
        [Required]
        public string Fullname { get; set; }

        [StringLength(maximumLength: 600)]
        [Required]
        public string About { get; set; }
        [Required]

        [StringLength(maximumLength: 50)]

        public string Degree { get; set; }

        [StringLength(maximumLength: 50)]
        [Required]


        public string Experience { get; set; }

        [StringLength(maximumLength: 70)]
        [DataType(DataType.EmailAddress)]
        [Required]

        public string Email { get; set; }

        [StringLength(maximumLength: 50)]
        [Required]

        public string Phone { get; set; }

        [StringLength(maximumLength: 50)]

        public string Skype { get; set; }

        [StringLength(maximumLength:75)]
        [Required]

        public string Faculty { get; set; }

        public List<TeacherHobbies> TeacherHobbies { get; set; }

        [StringLength(maximumLength: 70)]
        [Required]
        public string Speciality { get; set; }

        [StringLength(maximumLength: 120)]



        public string Image { get; set; }

        //public List<SocialMedia> SocialMedias { get; set; }

        [NotMapped]
        public IFormFile  ImageFile { get; set; }

        [NotMapped]
        //[Required]
        public List<int>  HobbyIds { get; set; }

        public string Fblink { get; set; }
        public string Instalink { get; set; }

        public string Vimeolink { get; set; }

        public string PinterestLink { get; set; }



    }
}
