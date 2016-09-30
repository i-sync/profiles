using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

namespace Profiles.Models
{
    public class Skill
    {
        public int ID { get; set; }
        public int PID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}