using SPMS.com.project.dbconnection;
using SPMS.com.project.views.adminPanel;
using SPMS.com.project.views.managerPanel;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SPMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {

            if (txtUsername.Text.Equals(""))
            {
                MessageBox.Show("Please input your username");
            }
            else if (txtPassword.Password.Equals(""))
            {
                MessageBox.Show("Please input your Password");
            }
            else { 

            String validate = new DatabaseConnection().LoginValidate(txtUsername.Text, txtPassword.Password);
                if (validate == "No")
                {
                    MessageBox.Show("Login UnSucessfull");

                }
                else
                {
                    if (Convert.ToInt32(validate).Equals(1))
                    {
                        MessageBox.Show("Login Sucessfull Mr Admin");
                        new AdminDashboard(txtUsername.Text).Show();
                        this.Close();
                    }
                    else if (Convert.ToInt32(validate).Equals(2))
                    {
                        MessageBox.Show("Login Sucessfull Mr Employee");
                        new UserDashboard(txtUsername.Text).Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Something is Wrong");
                        this.Close();
                    }
                }

            }
        }
    }
}
