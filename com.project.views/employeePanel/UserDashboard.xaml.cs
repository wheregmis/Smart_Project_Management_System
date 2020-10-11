using LiveCharts;
using LiveCharts.Wpf;
using SPMS.com.project.controller;
using SPMS.com.project.models;
using SPMS.com.project.views.employeePanel;
using SPMS.com.project.views.reviewuis;
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

namespace SPMS.com.project.views.managerPanel
{
    /// <summary>
    /// Interaction logic for UserDashboard.xaml
    /// </summary>
    public partial class UserDashboard : Window
    {
        string email;
        Employee employee;
        string project1, project2, project3;
        public UserDashboard(String Email)
        {
            InitializeComponent();
            lblUsername.Content = Email;
            ListViewMenu.SelectedItem = ItemHome;
            this.email = Email;
           DataTable dt = new UserController().GetUserDetails(email);
            int id = Convert.ToInt32( dt.Rows[0]["Employee_ID"]);
            string name = dt.Rows[0]["Employee_Name"].ToString();
            string password = dt.Rows[0]["Employee_Password"].ToString();
            string gender = dt.Rows[0]["Employee_Gender"].ToString();
            int employtypeID = Convert.ToInt32(dt.Rows[0]["Employee_Type"]);

            employee = new Employee(id, name, email, password, gender, employtypeID);


            DataTable projectList = new ProjectController().GetYourProjects(employee.Employee_ID1);
           // MessageBox.Show(projectList.Rows.Count.ToString());
            if (projectList.Rows.Count >= 3)
            {
                project1 = projectList.Rows[0].Field<string>(1).ToString();
                project2 = projectList.Rows[1].Field<string>(1).ToString();
                project3 = projectList.Rows[2].Field<string>(1).ToString();

                // For chart
                SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Completed",
                    Values = new ChartValues<int> { projectList.Rows[0].Field<int>(5) , projectList.Rows[1].Field<int>(5), projectList.Rows[2].Field<int>(5) }
                }
            };

                //adding series will update and animate the chart automatically
                SeriesCollection.Add(new ColumnSeries
                {
                    Title = "Remaining",
                    Values = new ChartValues<int> { projectList.Rows[0].Field<int>(6), projectList.Rows[1].Field<int>(6), projectList.Rows[2].Field<int>(6) }

                });

                SeriesCollection.Add(new ColumnSeries
                {
                    Title = "Ongoing",
                    Values = new ChartValues<int> { projectList.Rows[0].Field<int>(7), projectList.Rows[1].Field<int>(7), projectList.Rows[2].Field<int>(7) }

                });

                //also adding values updates and animates the chart automatically
                //  SeriesCollection[1].Values.Add(48d);

                Labels = new[] { project1, project2, project3 };

                // Labels = new[] { "Maria", "Susan", "Charles", "Frida" };

                Formatter = value => value.ToString("N");

                DataContext = this;


            }
            else if (projectList.Rows.Count.Equals(2))
            {
                project1 = projectList.Rows[0].Field<string>(1).ToString();
                project2 = projectList.Rows[1].Field<string>(1).ToString();

                // For chart
                SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Completed",
                    Values = new ChartValues<int> { projectList.Rows[0].Field<int>(5) , projectList.Rows[1].Field<int>(5)}
                }
            };

                //adding series will update and animate the chart automatically
                SeriesCollection.Add(new ColumnSeries
                {
                    Title = "Remaining",
                    Values = new ChartValues<int> { projectList.Rows[0].Field<int>(6), projectList.Rows[1].Field<int>(6)}

                });

                SeriesCollection.Add(new ColumnSeries
                {
                    Title = "Ongoing",
                    Values = new ChartValues<int> { projectList.Rows[0].Field<int>(7), projectList.Rows[1].Field<int>(7)}

                });

                //also adding values updates and animates the chart automatically
                //  SeriesCollection[1].Values.Add(48d);

                Labels = new[] { project1, project2 };

                // Labels = new[] { "Maria", "Susan", "Charles", "Frida" };

                Formatter = value => value.ToString("N");

                DataContext = this;


            }
            else if (projectList.Rows.Count.Equals(1))
            {
                project1 = projectList.Rows[0].Field<string>(1).ToString();

                // For chart
                SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Completed",
                    Values = new ChartValues<int> { projectList.Rows[0].Field<int>(5)}
                }
            };

                //adding series will update and animate the chart automatically
                SeriesCollection.Add(new ColumnSeries
                {
                    Title = "Remaining",
                    Values = new ChartValues<int> { projectList.Rows[0].Field<int>(6)}

                });

                SeriesCollection.Add(new ColumnSeries
                {
                    Title = "Ongoing",
                    Values = new ChartValues<int> { projectList.Rows[0].Field<int>(7) }

                });

                //also adding values updates and animates the chart automatically
                // SeriesCollection[1].Values.Add(48d);

                Labels = new[] { project1 };

                // Labels = new[] { "Maria", "Susan", "Charles", "Frida" };

                Formatter = value => value.ToString("N");

                DataContext = this;



            }
            else {

            }

            

           
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }


        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void BtnViewOthers_Click(object sender, RoutedEventArgs e)
        {
            new ViewProjects(employee).Show();
            this.Close();
        }

        private void BtnProject_Click(object sender, RoutedEventArgs e)
        {
            new CreateProject(employee).Show();
            this.Close();
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {

                case "ItemHome":
                default:

                    break;
                case "EditProfile":
                     new UpdateProfile(employee).Show();
                    this.Close();
                     break;
                case "UpdateSkill":
                    new UpdateSkill(employee).Show();
                    this.Close();
                    break;
                case "ReviewOther":
                    new ReviewOthers(employee).Show();
                    this.Close();
                    break;
                case "ReportIssue":
                    new ManageIssues(employee).Show();
                    this.Close();
                    break;
               
                case "Logout":
                    new MainWindow().Show();
                    this.Close();
                    break;

            }
        }
    }
}
