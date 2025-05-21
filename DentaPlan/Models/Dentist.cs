namespace DentaPlan.Models
{
    public class Dentist
    {
        public int DentistID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public string Specialization { get; set; }
    }
}