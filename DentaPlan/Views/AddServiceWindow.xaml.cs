using DentaPlan.Data;
using DentaPlan.Models;
using System;
using System.Windows;

namespace DentaPlan.Views
{
    public partial class AddServiceWindow : Window
    {
        private readonly DentaPlanContext _context;

        public AddServiceWindow()
        {
            InitializeComponent();
            _context = DentaPlanContext.Instance;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string serviceName = ServiceNameTextBox.Text.Trim();
                string priceText = PriceTextBox.Text.Trim();

                // Валидация
                if (string.IsNullOrEmpty(serviceName) || string.IsNullOrEmpty(priceText))
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!decimal.TryParse(priceText, out decimal price) || price <= 0)
                {
                    MessageBox.Show("Введите корректную цену!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var service = new Service
                {
                    ServiceName = serviceName,
                    Price = price
                };
                _context.Services.Add(service);
                _context.SaveChanges();

                MessageBox.Show("Услуга добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}