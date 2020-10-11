using SPMS.com.project.controller;
using SPMS.com.project.models;
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
    /// Interaction logic for ViewProjects.xaml
    /// </summary>
    public partial class ViewProjects : Window
    {
        Project project;
        Employee employee;
        public ViewProjects(Employee employee)
        {
            InitializeComponent();
            this.employee = employee;
            getProjectList();
           

        }
        public void getProjectList()
        {

            DataTable projectList = new ProjectController().GetInvolvedProjects(employee.Employee_ID1);
            ListProject.ItemsSource = projectList.DefaultView;
        }

        private void mouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView CompRow;
            int SComp;
            int project_id;

            SComp = ListProject.SelectedIndex;
            CompRow = ListProject.Items.GetItemAt(SComp) as DataRowView;
            project_id = Convert.ToInt32(CompRow["Project_id"]);
          //  MessageBox.Show(project_id.ToString());
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

            project = new Project(projectID, projectName, startDate, endDate, project_admin, completedTask, ongoingTask, remainingTask, status);

          
            new CreateTask(employee, project).Show();
            this.Close();

        }

    }
}
