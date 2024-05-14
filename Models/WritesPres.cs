using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthcareFinal.Models
{
    public class WritesPres
    {
        public Patient SSN {  get; set; }
        public Doctor Doctor_id { get; set; }
        public DateTime dateofpres { get; set; }
    }
}