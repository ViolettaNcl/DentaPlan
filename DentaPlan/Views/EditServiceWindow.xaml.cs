using DentaPlan.Data;
using DentaPlan.Models;
using System;
using System.Windows;

namespace DentaPlan.Views
{
    public partial class EditServiceWindow : Window
    {
        private readonly Service _service;
        private readonly DentaPlanContext _context;

        public EditServiceWindow(Service service)
        {
            InitializeComponent();
            _service = service;
            _context = DentaPlanContext.Instance;

            // Заполнение полей текущими данными
            ServiceNameTextBox.Text = _service.ServiceName;
            PriceTextBox.Text = _service.Price.ToString();
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

                // Обновление данных
                _service.ServiceName = serviceName;
                _service.Price = price;
                _context.SaveChanges();

                MessageBox.Show("Услуга обновлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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