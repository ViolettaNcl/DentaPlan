using Microsoft.EntityFrameworkCore;
using DentaPlan.Models;
using System.Configuration;

namespace DentaPlan.Data
{
    public class DentaPlanContext : DbContext
    {
        private static DentaPlanContext _instance;

        public static DentaPlanContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DentaPlanContext();
                }
                return _instance;
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Dentist> Dentists { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public DentaPlanContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DentaPlanContext"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Dentist>()
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserID);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientID);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Dentist)
                .WithMany()
                .HasForeignKey(a => a.DentistID);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Service)
                .WithMany()
                .HasForeignKey(a => a.ServiceID);
        }
    }
}