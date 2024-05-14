using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthcareFinal.Models
{
    public class Nurse
    {
        public string UserEmail { get; set; } // Foreign key to Users table
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int NurseId { get; set; } // Primary key
        public int Shifts { get; set; }

        // Navigation property for the relationship if using Entity Framework
        
    }

}