using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthcareFinal.Models
{
    public class CheckPres
    {
        public DateTime dateofpres { get; set; }
        public int SSN { get; set; }  
        public int Doctor_id { get; set; }
    }
}