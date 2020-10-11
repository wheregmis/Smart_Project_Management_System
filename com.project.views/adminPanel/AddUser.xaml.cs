using MaterialDesignThemes.Wpf;
using SPMS.com.project.controller;
using SPMS.com.project.models;
using SPMS.smtpServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        Employee employee;
        String TaskStatus;
        public AddUser(Employee employee)
        {
            InitializeComponent();
            listUser.Items.Clear();
            this.employee = employee;
            getUserList();
            btnDelete.Visibility = Visibility.Hidden;
            rdoAdmin.IsChecked = false;
            rdoEmploye.IsChecked = false;
        }

        public void getUserList() {
            
            DataTable userList = new UserController().GetAllUsers();
            //  var tableEnumerable = userList.AsEnumerable();            
            //  listUser.ItemsSource = tableEnumerable.ToArray(); ;
            listUser.ItemsSource = userList.DefaultView;
        }

        public void RefreshListView() {
            
            DataTable userList = new UserController().GetAllUsers();
            ICollectionView view = CollectionViewSource.GetDefaultView(userList.DefaultView);
            view.Refresh();
        }

        private void ListUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView CompRow;
            int SComp;
            string email;

            SComp = listUser.SelectedIndex;
            CompRow = listUser.Items.GetItemAt(SComp) as DataRowView;
            email = Convert.ToString(CompRow["Employee_Email"]);
            DataTable userDetail = new UserController().GetUserDetails(email);

            txtName.Text = userDetail.Rows[0]["Employee_Name"].ToString();
            txtEmail.Text = userDetail.Rows[0]["Employee_Email"].ToString();
            txtPassword.Password = userDetail.Rows[0]["Employee_Password"].ToString();

            cmbGender.SelectedValue = userDetail.Rows[0]["Employee_Gender"].ToString(); ;

            txtDOB.Text = userDetail.Rows[0]["DATE_OF_BIRTH"].ToString();
            txtAddress.Text = userDetail.Rows[0]["ADDRESS"].ToString();

            int status = Convert.ToInt32( userDetail.Rows[0]["EMPLOYEE_TYPE"]);

            if (status.Equals(1))
            {
                rdoAdmin.IsChecked = true;
            }
            else {
                rdoEmploye.IsChecked = true;
            }

            btnDelete.Visibility = Visibility.Visible;

        }
        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            this.TaskStatus = (string)(sender as RadioButton).Content;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Content = "Your username for SPMS is " + txtEmail.Text + " and your password is " + txtPassword.Password;
            string subject = "Your Login Credentials for SPMS";

            new SendMail().sentMail(txtEmail.Text, subject, Content);

                string name, useremail, password, gender, dob, address;
                name = txtName.Text;
                useremail = txtEmail.Text;
                password = txtPassword.Password;
            ComboBoxItem typeItem = (ComboBoxItem)cmbGender.SelectedItem;
            gender = typeItem.Content.ToString();
             
                dob = txtDOB.SelectedDate.Value.Date.ToString("yyyy-MM-dd");
                address = txtAddress.Text;

                Employee u = new Employee(name, useremail, password, gender, dob, address);
            if (TaskStatus.Equals("System Admin"))
            {
                new UserController().InsertAdmin(u);
                getUserList();
            }
            else if (TaskStatus.Equals("Employee"))
            {

                new UserController().InsertUser(u);
                getUserList();
            }
            else {
                MessageBox.Show("Please Check User Type");
            }
                


            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new AdminDashboard(employee.Employee_Email1).Show();
            this.Close();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            new UserController().DeleteUser(txtEmail.Text);
            getUserList();
        }
    }
}
