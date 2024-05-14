using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthcareFinal.Models
{
    public class MakeAppointments
    {
        public int Doctor_id { get; set; }
        public int SSN { get; set; }
        public DateTime Appointment_date { get; set; }
    }
}