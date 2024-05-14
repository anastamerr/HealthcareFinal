using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthcareFinal.Models
{
    public class Doctor
    {
        public string UserEmail { get; set; } 
        public string Password { get; set; }

        public string ElectronicHealthRecords { get; set; }
        public string EmergencyContactInformation { get; set; }
        public int DoctorId { get; set; } // Primary key
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string WorkingDaysAndHours { get; set; }
        public string PhoneNumber { get; set; }
        public string HospitalName { get; set; } // Foreign key to Hospital table

        // Navigation property for the relationship if using Entity Framework
        
    }

}