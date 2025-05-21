using DentaPlan.Data;
using DentaPlan.Models;
using System;
using System.Linq;
using System.Windows;

namespace DentaPlan.Views
{
    public partial class AddDentistWindow : Window
    {
        private readonly DentaPlanContext _context;

        public AddDentistWindow()
        {
            InitializeComponent();
            _context = DentaPlanContext.Instance;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = UsernameTextBox.Text.Trim();
                string password = PasswordBox.Password.Trim();
                string fullName = FullNameTextBox.Text.Trim();
                string specialization = SpecializationTextBox.Text.Trim();

                // Валидация
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                    string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(specialization))
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (_context.Users.Any(u => u.Username == username))
                {
                    MessageBox.Show("Логин уже занят!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Создание пользователя
                var user = new User
                {
                    Username = username,
                    Password = password, // Сохраняем пароль в открытом виде
                    Role = "Dentist",
                    FullName = fullName
                };
                _context.Users.Add(user);
                _context.SaveChanges();

                // Создание стоматолога
                var dentist = new Dentist
                {
                    UserID = user.UserID,
                    Specialization = specialization
                };
                _context.Dentists.Add(dentist);
                _context.SaveChanges();

                MessageBox.Show("Стоматолог добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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