using DentaPlan.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace DentaPlan.Tests
{
    public class ServiceTests
    {
        private readonly List<Service> _services;

        public ServiceTests()
        {
            _services = new List<Service>();
        }

        private void CreateService(string serviceName, decimal price)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
                throw new ArgumentException("Название услуги не может быть пустым.");

            if (price <= 0)
                throw new ArgumentException("Цена должна быть больше нуля.");

            if (_services.Exists(s => s.ServiceName == serviceName))
                throw new ArgumentException("Услуга с таким названием уже существует.");

            var service = new Service
            {
                ServiceID = _services.Count + 1,
                ServiceName = serviceName,
                Price = price
            };

            _services.Add(service);
        }

        [Fact]
        public void CreateService_ValidData_AddsToList()
        {
            CreateService("Лечение кариеса", 5000);
            Assert.Single(_services);
            Assert.Equal("Лечение кариеса", _services[0].ServiceName);
            Assert.Equal(5000, _services[0].Price);
        }

        [Fact]
        public void CreateService_EmptyName_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => CreateService("", 5000));
            Assert.Equal("Название услуги не может быть пустым.", exception.Message);
        }

        [Fact]
        public void CreateService_NonPositivePrice_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => CreateService("Лечение кариеса", 0));
            Assert.Equal("Цена должна быть больше нуля.", exception.Message);
        }

        [Fact]
        public void CreateService_DuplicateName_ThrowsException()
        {
            CreateService("Лечение кариеса", 5000);
            var exception = Assert.Throws<ArgumentException>(() => CreateService("Лечение кариеса", 6000));
            Assert.Equal("Услуга с таким названием уже существует.", exception.Message);
        }
    }
}