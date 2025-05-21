using DentaPlan.Data;
using DentaPlan.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;

namespace DentaPlan.Views
{
    public partial class DentistWindow : Window
    {
        private readonly DentaPlanContext _context;
        private readonly User _currentUser;

        public DentistWindow(User currentUser)
        {
            InitializeComponent();
            _context = DentaPlanContext.Instance;
            _currentUser = currentUser;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var dentist = _context.Dentists
                    .FirstOrDefault(d => d.UserID == _currentUser.UserID);

                if (dentist == null)
                {
                    MessageBox.Show("Ошибка: пользователь не зарегистрирован как стоматолог!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                    return;
                }

                AppointmentsDataGrid.ItemsSource = _context.Appointments
                    .Include(a => a.Patient)
                    .Include(a => a.Service)
                    .Where(a => a.DentistID == dentist.DentistID && a.Status == "Approved")
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
    }
}