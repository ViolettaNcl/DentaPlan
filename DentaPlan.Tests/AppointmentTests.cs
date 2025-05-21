using DentaPlan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DentaPlan.Tests
{
    public class AppointmentTests
    {
        private readonly List<Appointment> _appointments;
        private readonly List<Dentist> _dentists;
        private readonly List<Service> _services;
        private readonly List<User> _users;

        public AppointmentTests()
        {
            _appointments = new List<Appointment>();
            _users = new List<User> { new User { UserID = 1, Username = "patient", Role = "Patient", FullName = "Иван Иванов" } };
            _dentists = new List<Dentist> { new Dentist { DentistID = 1, UserID = 2, Specialization = "Общая стоматология" } };
            _services = new List<Service> { new Service { ServiceID = 1, ServiceName = "Лечение кариеса", Price = 5000 } };
        }

        private void CreateAppointment(int patientId, int dentistId, int serviceId, DateTime appointmentDateTime)
        {
            if (appointmentDateTime <= DateTime.Now)
                throw new ArgumentException("Дата и время должны быть в будущем.");

            if (!_dentists.Any(d => d.DentistID == dentistId))
                throw new ArgumentException("Стоматолог не найден.");

            if (!_services.Any(s => s.ServiceID == serviceId))
                throw new ArgumentException("Услуга не найдена.");

            if (!_users.Any(u => u.UserID == patientId && u.Role == "Patient"))
                throw new ArgumentException("Пациент не найден.");

            var appointment = new Appointment
            {
                PatientID = patientId,
                DentistID = dentistId,
                ServiceID = serviceId,
                AppointmentDateTime = appointmentDateTime,
                Status = "Pending"
            };

            _appointments.Add(appointment);
        }

        [Fact]
        public void CreateAppointment_ValidData_AddsToList()
        {
            CreateAppointment(1, 1, 1, DateTime.Now.AddDays(1));
            Assert.Equal(1, _appointments.Count);
            Assert.Equal("Pending", _appointments[0].Status);
        }

        [Fact]
        public void CreateAppointment_PastDate_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => CreateAppointment(1, 1, 1, DateTime.Now.AddDays(-1)));
            Assert.Equal("Дата и время должны быть в будущем.", exception.Message);
        }

        [Fact]
        public void CreateAppointment_InvalidDentist_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => CreateAppointment(1, 999, 1, DateTime.Now.AddDays(1)));
            Assert.Equal("Стоматолог не найден.", exception.Message);
        }
    }
}