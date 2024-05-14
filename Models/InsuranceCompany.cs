using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthcareFinal.Models
{
    public class InsuranceCompany
    {
        public int CompanyId { get; set; }
        public string Name { get; set;}
        public Patient SSN { get; set; }
    }
}