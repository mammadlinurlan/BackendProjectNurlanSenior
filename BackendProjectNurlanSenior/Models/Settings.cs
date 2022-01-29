using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Models
{
    public class Settings
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 150)]
        public string Question { get; set; }
        [StringLength(maximumLength: 50)]

        public string Phone { get; set; }
        [StringLength(maximumLength: 150)]


        public string LogoBig { get; set; }
        [StringLength(maximumLength: 150)]


        public string LogoLittle { get; set; }



        public string SearchIcon { get; set; }
        [StringLength(maximumLength: 150)]


        public string SubsTitle { get; set; }
        [StringLength(maximumLength: 150)]
        public string Mail { get; set; }
        [StringLength(maximumLength: 50)]


        public string Adress { get; set; }

        [StringLength(maximumLength: 150)]
        public string Website { get; set; }
        [StringLength(maximumLength: 50)]
        public string FooterPhone { get; set; }
        [StringLength(maximumLength: 50)]


        public string SubsDesc { get; set; }
        [StringLength(maximumLength: 250)]

        public string FooterDesc { get; set; }

        public string FbIcon { get; set; }

        public string InstaIcon { get; set; }

        public string TwitterIcon { get; set; }

        public string VimeoIcon { get; set; }

        public string FbLink { get; set; }
        public string InstaLink { get; set; }
        public string TwitterLink { get; set; }
        public string VimeoLink { get; set; }





    }
}
