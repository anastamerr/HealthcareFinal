using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthcareFinal.Models
{
    public class ChecksHospitalDocs
    {
        public int SSN { get; set; }
        public int Doctor_id { get; set; }
        public string hospital_name { get; set; }
    }
}