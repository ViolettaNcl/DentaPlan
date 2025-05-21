using DentaPlan.Data;
using DentaPlan.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;

namespace DentaPlan.Views
{
    public partial class EditDentistWindow : Window
    {
        private readonly Dentist _dentist;
        private readonly DentaPlanContext _context;

        public EditDentistWindow(Dentist dentist)
        {
            InitializeComponent();
            _dentist = dentist;
            _context = DentaPlanContext.Instance;

            UsernameTextBox.Text = _dentist.User.Username;
            FullNameTextBox.Text = _dentist.User.FullName;
            SpecializationTextBox.Text = _dentist.Specialization;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = UsernameTextBox.Text.Trim();
                string fullName = FullNameTextBox.Text.Trim();
                string specialization = SpecializationTextBox.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(specialization))
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (_context.Users.Any(u => u.Username == username && u.UserID != _dentist.UserID))
                {
                    MessageBox.Show("Логин уже занят!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var user = _context.Users.Find(_dentist.UserID);
                user.Username = username;
                user.FullName = fullName;
                _dentist.Specialization = specialization;

                _context.SaveChanges();
                MessageBox.Show("Стоматолог обновлён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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