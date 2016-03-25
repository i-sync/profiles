using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Profiles.Models
{
    public class Profile
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Avatar { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Intro { get; set; }
        [DataType(DataType.Date)]
        public DateTime AddDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; }
        public string Password { get; set; }

        public List<Skill> Skills { get; set; }
        public List<Project> Projects { get; set; }
        public List<Experience> Experiences { get; set; }
        public List<Education> Educations { get; set; }
        public List<Living> Livings { get; set; }
        public List<Link> Links { get; set; }

    }
}