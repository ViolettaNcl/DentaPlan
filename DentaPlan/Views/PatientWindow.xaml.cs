using DentaPlan.Data;
using DentaPlan.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;

namespace DentaPlan.Views
{
    public partial class PatientWindow : Window
    {
        private readonly DentaPlanContext _context;
        private readonly User _currentUser;

        public PatientWindow(User currentUser)
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
                DentistComboBox.ItemsSource = _context.Dentists
                    .Include(d => d.User)
                    .ToList();

                ServiceComboBox.ItemsSource = _context.Services
                    .ToList();

                AppointmentsDataGrid.ItemsSource = _context.Appointments
                    .Include(a => a.Dentist).ThenInclude(d => d.User)
                    .Include(a => a.Service)
                    .Where(a => a.PatientID == _currentUser.UserID)
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SubmitAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DentistComboBox.SelectedItem == null || ServiceComboBox.SelectedItem == null || AppointmentDatePicker.SelectedDate == null)
                {
                    MessageBox.Show("Выберите стоматолога, услугу и дату!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string timeText = AppointmentTimeTextBox.Text.Trim();
                if (!TimeSpan.TryParse(timeText, out TimeSpan time))
                {
                    MessageBox.Show("Введите время в формате ЧЧ:ММ!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                DateTime date = AppointmentDatePicker.SelectedDate.Value.Date;
                DateTime appointmentDateTime = date.Add(time);

                if (appointmentDateTime <= DateTime.Now)
                {
                    MessageBox.Show("Выберите дату и время в будущем!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var dentist = (Dentist)DentistComboBox.SelectedItem;
                var service = (Service)ServiceComboBox.SelectedItem;

                var appointment = new Appointment
                {
                    PatientID = _currentUser.UserID,
                    DentistID = dentist.DentistID,
                    ServiceID = service.ServiceID,
                    AppointmentDateTime = appointmentDateTime,
                    Status = "Pending"
                };

                _context.Appointments.Add(appointment);
                _context.SaveChanges();

                MessageBox.Show("Заявка подана!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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