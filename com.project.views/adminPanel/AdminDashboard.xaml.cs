using SPMS.com.project.controller;
using SPMS.com.project.models;
using SPMS.com.project.views.employeePanel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SPMS.com.project.views.adminPanel
{
    /// <summary>
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Window
    {
        string email;
        Employee employee;
        public AdminDashboard(String Email)
        {
            InitializeComponent();
            this.email = Email;
            lblUsername.Content = this.email;
            DataTable dt = new UserController().GetUserDetails(email);
            int id = Convert.ToInt32(dt.Rows[0]["Employee_ID"]);
            string name = dt.Rows[0]["Employee_Name"].ToString();
            string password = dt.Rows[0]["Employee_Password"].ToString();
            string gender = dt.Rows[0]["Employee_Gender"].ToString();
            int employtypeID = Convert.ToInt32(dt.Rows[0]["Employee_Type"]);

            employee = new Employee(id, name, email, password, gender, employtypeID);
        }

        private void BtnUsers_Click(object sender, RoutedEventArgs e)
        {
            new AddUser(employee).Show();
            this.Close();
        }

        private void BtnSkills_Click(object sender, RoutedEventArgs e)
        {
            new Add_Skills(employee).Show();
            this.Close();
        }

        private void BtnProjects_Click(object sender, RoutedEventArgs e)
        {
            new ViewAllProjects(employee).Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
