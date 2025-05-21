using DentaPlan.Data;
using DentaPlan.Models;
using System;
using System.Linq;
using System.Windows;

namespace DentaPlan.Views
{
    public partial class RegisterWindow : Window
    {
        private readonly DentaPlanContext _context;

        public RegisterWindow()
        {
            InitializeComponent();
            _context = DentaPlanContext.Instance;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = UsernameTextBox.Text.Trim();
                string password = PasswordBox.Password.Trim();
                string fullName = FullNameTextBox.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fullName))
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (_context.Users.Any(u => u.Username == username))
                {
                    MessageBox.Show("Логин уже занят!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var user = new User
                {
                    Username = username,
                    Password = password, 
                    Role = "Patient",
                    FullName = fullName
                };
                _context.Users.Add(user);
                _context.SaveChanges();

                MessageBox.Show("Регистрация успешна! Теперь вы можете войти.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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