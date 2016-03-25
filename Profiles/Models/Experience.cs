using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Profiles.Models
{
    public class Experience
    {
        public int ID { get; set; }
        public int PID { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Link { get; set; }
        public string Period { get; set; }
        public string Location { get; set; }
        public string Position { get; set; }
        public string Intro { get; set; }
    }
}