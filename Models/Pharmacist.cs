using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthcareFinal.Models
{
    public class Pharmacist
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public int identificationCode { get; set; }
        public string name { get; set; }
        public string pharmacy_name { get; set; }
        public DateTime first_working_day { get; set; }
        public int YearsOfExperience => DateTime.Now.Year - first_working_day.Year;
    }
}