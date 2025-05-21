using System;

namespace DentaPlan.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public User Patient { get; set; }
        public int? DentistID { get; set; }
        public Dentist Dentist { get; set; }
        public int ServiceID { get; set; }
        public Service Service { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string Status { get; set; }
        public bool CreatedByAdmin { get; set; }
    }
}