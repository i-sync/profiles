using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Profiles.Models
{
    public class RPC
    {
        public int ID { get; set; }
        public int PID { get; set; }
        public int CID { get; set; }
        [DataType(DataType.Date)]
        public DateTime AddDate { get; set; }
    }
}