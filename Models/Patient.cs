using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthcareFinal.Models
{
    public class Patient
    {
        public string UserEmail { get; set; } // Foreign key to Users table
        public string Password { get; set; }

        public int SSN { get; set; } // Primary key
        public string ElectronicHealthRecords { get; set; }
        public string EmergencyContactInformation { get; set; }
        public string Allergies { get; set; }
        public string ChronicDiseases { get; set; }
        public string Vaccines { get; set; }
        public string PrescribedDrugs { get; set; }
        public string Results { get; set; }
        public Hospital HospitalName { get; set; } // Foreign key to Hospital table
        public InsuranceCompany CompanyId { get; set; }
        public Nurse NurseId { get; set; } // Foreign key to Nurse table

        // Navigation properties for relationships if using Entity Framework
        
    }

}