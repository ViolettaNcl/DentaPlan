using DentaPlan.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace DentaPlan.Tests
{
    public class DentistTests
    {
        private readonly List<Dentist> _dentists;
        private readonly List<User> _users;

        public DentistTests()
        {
            _users = new List<User>
            {
                new User { UserID = 1, Username = "dentist1", Role = "Dentist", FullName = "Анна Петрова" }
            };
            _dentists = new List<Dentist>();
        }

        private void CreateDentist(int userId, string specialization)
        {
            if (!_users.Exists(u => u.UserID == userId && u.Role == "Dentist"))
                throw new ArgumentException("Пользователь не стоматолог.");

            if (string.IsNullOrWhiteSpace(specialization))
                throw new ArgumentException("Специализация не может быть пустой.");

            var dentist = new Dentist
            {
                DentistID = _dentists.Count + 1,
                UserID = userId,
                User = _users.Find(u => u.UserID == userId),
                Specialization = specialization
            };

            _dentists.Add(dentist);
        }

        [Fact]
        public void CreateDentist_ValidData_AddsToList()
        {
            CreateDentist(1, "Общая стоматология");
            Assert.Single(_dentists);
            Assert.Equal(1, _dentists[0].UserID);
            Assert.Equal("Общая стоматология", _dentists[0].Specialization);
        }

        [Fact]
        public void CreateDentist_InvalidUser_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => CreateDentist(999, "Общая стоматология"));
            Assert.Equal("Пользователь не стоматолог.", exception.Message);
        }

        [Fact]
        public void CreateDentist_EmptySpecialization_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => CreateDentist(1, ""));
            Assert.Equal("Специализация не может быть пустой.", exception.Message);
        }
    }
}