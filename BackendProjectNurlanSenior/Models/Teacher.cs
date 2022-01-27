using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [StringLength(maximumLength:50)]
        public string Fullname { get; set; }

        [StringLength(maximumLength: 600)]

        public string About { get; set; }

        [StringLength(maximumLength: 50)]

        public string Degree { get; set; }

        [StringLength(maximumLength: 50)]

        public string Experience { get; set; }

        [StringLength(maximumLength: 70)]

        public string Email { get; set; }

        [StringLength(maximumLength: 50)]

        public string Phone { get; set; }

        [StringLength(maximumLength: 50)]

        public string Skype { get; set; }

        [StringLength(maximumLength:75)]
        public string Faculty { get; set; }

        public List<TeacherHobbies> TeacherHobbies { get; set; }

        [StringLength(maximumLength: 70)]

        public string Speciality { get; set; }

        [StringLength(maximumLength: 120)]

        public string Image { get; set; }

        public List<SocialMedia> SocialMedias { get; set; }

    }
}
