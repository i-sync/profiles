using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Profiles.Models
{
    public class Link
    {
        public int ID { get; set; }
        public int PID { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string link { get; set; }
    }
}