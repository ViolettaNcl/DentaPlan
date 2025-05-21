using DentaPlan.Data;
using DentaPlan.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;

namespace DentaPlan.Views
{
    public partial class AdminWindow : Window
    {
        private readonly DentaPlanContext _context;

        public AdminWindow()
        {
            InitializeComponent();
            _context = DentaPlanContext.Instance;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DentistsDataGrid.ItemsSource = _context.Dentists
                    .Include(d => d.User)
                    .ToList();

                ServicesDataGrid.ItemsSource = _context.Services
                    .ToList();

                AppointmentsDataGrid.ItemsSource = _context.Appointments
                    .Include(a => a.Patient)
                    .Include(a => a.Dentist).ThenInclude(d => d.User)
                    .Include(a => a.Service)
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddDentistButton_Click(object sender, RoutedEventArgs e)
        {
            var addDentistWindow = new AddDentistWindow();
            addDentistWindow.ShowDialog();
            LoadData();
        }

        private void EditDentistButton_Click(object sender, RoutedEventArgs e)
        {
            if (DentistsDataGrid.SelectedItem is Dentist selectedDentist)
            {
                var editDentistWindow = new EditDentistWindow(selectedDentist);
                editDentistWindow.ShowDialog();
                LoadData();
            }
            else
            {
                MessageBox.Show("Выберите стоматолога для редактирования!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteDentistButton_Click(object sender, RoutedEventArgs e)
        {
            if (DentistsDataGrid.SelectedItem is Dentist selectedDentist)
            {
                var result = MessageBox.Show($"Удалить стоматолога {selectedDentist.User.FullName}?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var user = _context.Users.Find(selectedDentist.UserID);
                        _context.Dentists.Remove(selectedDentist);
                        if (user != null)
                            _context.Users.Remove(user);
                        _context.SaveChanges();
                        MessageBox.Show("Стоматолог удалён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите стоматолога для удаления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AddServiceButton_Click(object sender, RoutedEventArgs e)
        {
            var addServiceWindow = new AddServiceWindow();
            addServiceWindow.ShowDialog();
            LoadData();
        }

        private void EditServiceButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServicesDataGrid.SelectedItem is Service selectedService)
            {
                var editServiceWindow = new EditServiceWindow(selectedService);
                editServiceWindow.ShowDialog();
                LoadData();
            }
            else
            {
                MessageBox.Show("Выберите услугу для редактирования!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteServiceButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServicesDataGrid.SelectedItem is Service selectedService)
            {
                var result = MessageBox.Show($"Удалить услугу {selectedService.ServiceName}?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _context.Services.Remove(selectedService);
                        _context.SaveChanges();
                        MessageBox.Show("Услуга удалена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите услугу для удаления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ApproveAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentsDataGrid.SelectedItem is Appointment selectedAppointment)
            {
                try
                {
                    selectedAppointment.Status = "Approved";
                    _context.SaveChanges();
                    MessageBox.Show("Заявка подтверждена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите заявку для подтверждения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RejectAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentsDataGrid.SelectedItem is Appointment selectedAppointment)
            {
                try
                {
                    selectedAppointment.Status = "Rejected";
                    _context.SaveChanges();
                    MessageBox.Show("Заявка отклонена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите заявку для отклонения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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