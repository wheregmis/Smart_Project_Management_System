using SPMS.com.project.controller;
using SPMS.com.project.models;
using SPMS.com.project.views.managerPanel;
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

namespace SPMS.com.project.views.employeePanel
{
    /// <summary>
    /// Interaction logic for UpdateProfile.xaml
    /// </summary>
    public partial class UpdateProfile : Window
    {
        Employee employee;
        public UpdateProfile(Employee employee)
        {
            InitializeComponent();

            DataTable userDetail = new UserController().GetUserDetails(employee.Employee_Email1);

            txtName.Text = userDetail.Rows[0]["Employee_Name"].ToString();
            txtEmail.Text = userDetail.Rows[0]["Employee_Email"].ToString();
            txtPassword.Password = userDetail.Rows[0]["Employee_Password"].ToString();
            cmbGender.SelectedValue = userDetail.Rows[0]["Employee_Gender"].ToString();
            txtAddress.Text = userDetail.Rows[0]["Address"].ToString();
            txtDOB.Text = userDetail.Rows[0]["Date_of_Birth"].ToString();

            this.employee = employee;

            txtName.IsReadOnly = true;
            txtEmail.IsReadOnly = true;
            
            txtAddress.IsReadOnly = true;
          
            btnUpdate.Visibility = Visibility.Hidden;

            getSkills();
            getReviews();
        }

        public void getSkills() {
            DataTable skillList = new SkillController().GetYourSkills(employee.Employee_ID1);
            listSkills.ItemsSource = skillList.DefaultView;
        }

        public void getReviews()
        {
            DataTable reviewList = new ReviewController().GetYourReviews(employee.Employee_ID1);
            listReview.ItemsSource = reviewList.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name, useremail, password, gender, dob, address;
            name = txtName.Text;
            useremail = txtEmail.Text;
            password = txtPassword.Password;
            gender = cmbGender.SelectedValue.ToString();
            dob = txtDOB.Text;
            address = txtAddress.Text;           


            Employee u = new Employee(employee.Employee_ID1, name, useremail, password, gender, dob, address);
            new UserController().UpdateUser(u);
            new UserDashboard(employee.Employee_Email1).Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new UserDashboard(employee.Employee_Email1).Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new UserDashboard(employee.Employee_Email1).Show();
            this.Close();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            btnUpdate.Visibility = Visibility.Visible;
            txtAddress.IsReadOnly = false;
            
            txtEmail.IsReadOnly = false;
            txtName.IsReadOnly = false;


        }
    }
}
