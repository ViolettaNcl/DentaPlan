using DentaPlan.Data;
using System;
using System.Linq;
using System.Windows;

namespace DentaPlan
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TestDatabaseConnection();
        }

        private void TestDatabaseConnection()
        {
            try
            {
                var context = DentaPlanContext.Instance;
                var userCount = context.Users.Count();
                MessageBox.Show($"Users in database: {userCount}", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Connection Failed");
            }
        }
    }
}