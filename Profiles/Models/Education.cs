using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Profiles.Models
{
    public class Education
    {
        public int ID { get; set; }
        public int PID { get; set; }
        public string Title { get; set; }

        public string Period { get; set; }
        public string Professional { get; set; }
        public string Link { get; set; }
        public string Intro { get; set; }
    }
}