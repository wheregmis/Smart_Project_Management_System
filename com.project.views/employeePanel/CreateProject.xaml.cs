using LiveCharts;
using LiveCharts.Wpf;
using SPMS.com.project.controller;
using SPMS.com.project.models;
using SPMS.com.project.views.adminPanel;
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

namespace SPMS.com.project.views.managerPanel
{
    /// <summary>
    /// Interaction logic for CreateProject.xaml
    /// </summary>
    public partial class CreateProject : Window
    {
        Employee employee;
        Project project;
        public CreateProject(Employee employee)
        {
            InitializeComponent();
            this.employee = employee;
            ListProject.Items.Clear();
            getProjectList();

        }

       

        public void getProjectList()
        {

            DataTable projectList = new ProjectController().GetYourProjects(employee.Employee_ID1);
            ListProject.ItemsSource = projectList.DefaultView;

           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Project p = new Project(txtProject.Text, txtstartDate.SelectedDate.Value.Date.ToString("yyyy-MM-dd"), txtEndDate.SelectedDate.Value.Date.ToString("yyyy-MM-dd"), employee.Employee_ID1);
            //  MessageBox.Show(txtstartDate.SelectedDate.Value.ToString("yyyy-MM-dd"));
            new ProjectController().InsertProject(p);
            getProjectList();
        }

        private void ListProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void mouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView CompRow;
            int SComp;
            int project_id;

            SComp = ListProject.SelectedIndex;
            CompRow = ListProject.Items.GetItemAt(SComp) as DataRowView;
            project_id = Convert.ToInt32(CompRow["Project_id"]);
            
            DataTable projectDetail = new ProjectController().GetProjectDetails(project_id);
            int projectID = Convert.ToInt32(projectDetail.Rows[0]["Project_id"]);
            int project_admin = Convert.ToInt32(projectDetail.Rows[0]["Project_Admin"]);
            string projectName = projectDetail.Rows[0]["project_name"].ToString();
            string startDate = projectDetail.Rows[0]["project_start_date"].ToString();
            string endDate = projectDetail.Rows[0]["project_end_date"].ToString();
            int completedTask = Convert.ToInt32(projectDetail.Rows[0]["COMPLETED_TASK"]);
            int ongoingTask = Convert.ToInt32(projectDetail.Rows[0]["ONGOING_TASK"]);
            int remainingTask = Convert.ToInt32(projectDetail.Rows[0]["REMAINING_TASK"]);
            string status = projectDetail.Rows[0]["PROJECT_STATUS"].ToString();
           // MessageBox.Show(status.ToString());
            project = new Project(projectID, projectName, startDate, endDate, project_admin, completedTask, ongoingTask, remainingTask, status);
           new CreateTask(employee, project).Show();
            this.Close();

        }

        private void TxtSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
          
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (employee.Employee_Type1.Equals(1))
            {

                new AdminDashboard(employee.Employee_Email1).Show();
                this.Close();
            }
            else {
                new UserDashboard(employee.Employee_Email1).Show();
                this.Close();

            }
        }
    }
}
