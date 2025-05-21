using DentaPlan.Data;
using DentaPlan.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DentaPlan.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();
            string selectedRole = (RoleComboBox.SelectedItem as ComboBoxItem)?.Tag.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(selectedRole))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var user = DentaPlanContext.Instance.Users
                    .FirstOrDefault(u => u.Username == username && u.Role == selectedRole);

                if (user == null)
                {
                    MessageBox.Show($"Неверный логин или роль! Введено: Username={username}, Role={selectedRole}",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (user.Password == password)
                {
                    switch (selectedRole)
                    {
                        case "Admin":
                            var adminWindow = new AdminWindow();
                            adminWindow.Show();
                            Close();
                            break;
                        case "Patient":
                            var patientWindow = new PatientWindow(user);
                            patientWindow.Show();
                            Close();
                            break;
                        case "Dentist":
                            var dentistWindow = new DentistWindow(user);
                            dentistWindow.Show();
                            Close();
                            break;
                        default:
                            MessageBox.Show("Неизвестная роль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Неверный пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }
    }
}