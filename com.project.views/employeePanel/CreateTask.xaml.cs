using LiveCharts;
using LiveCharts.Wpf;
using SPMS.com.project.controller;
using SPMS.com.project.models;
using SPMS.com.project.views.adminPanel;
using SPMS.com.project.views.managerPanel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
    /// Interaction logic for CreateTask.xaml
    /// </summary>
    public partial class CreateTask : Window
    {
        Employee employee;
        Project project;
        String TaskStatus;
        public CreateTask(Employee employee, Project project)
        {
            InitializeComponent();
            txtProject.Text = project.ProjectName;
            txtProjectStartDate.SelectedDate = Convert.ToDateTime(project.StartDate);
            txtProjectEndDate.SelectedDate = Convert.ToDateTime(project.EndDate);
            this.employee = employee;
            this.project = project;
            ListTask.Items.Clear();
            getTaskList();
            PieChartDrawing();
            AddTaskButton.Visibility = Visibility.Hidden;
            
            btnUpdateProject.Visibility = Visibility.Hidden;
            btnSubmitStatus.Visibility = Visibility.Hidden;
            btnActivity.Visibility = Visibility.Hidden;
            if (employee.Employee_ID1.Equals(project.Project_admin)) {
                AddTaskButton.Visibility = Visibility.Visible;
                
                btnUpdateProject.Visibility = Visibility.Visible;
                btnSubmitStatus.Visibility = Visibility.Visible;
                btnActivity.Visibility = Visibility.Visible;
            }

            if (this.project.Status.Equals("In Progress"))
            {
                rdoProgress.IsChecked = true;
            }
            else if (this.project.Status.Equals("Completed"))
            {
                rdoCompleted.IsChecked = true;
            }
            else if (this.project.Status.Equals("Remaining"))
            {
                rdoRemaining.IsChecked = true;
            }
            else
            {
                rdoProgress.IsChecked = false;
                rdoCompleted.IsChecked = false;
                rdoRemaining.IsChecked = false;
            }

        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            this.TaskStatus = (string)(sender as RadioButton).Content;
        }

        public void PieChartDrawing()
        {

            DataTable projectDetail = new ProjectController().GetProjectDetails(project.ProjectID);
            int compValue = Convert.ToInt32(projectDetail.Rows[0]["COMPLETED_TASK"]);
            int remValue = Convert.ToInt32(projectDetail.Rows[0]["REMAINING_TASK"]);
            int ongValue = Convert.ToInt32(projectDetail.Rows[0]["ONGOING_TASK"]);

            SeriesCollection = new SeriesCollection
            {
               new PieSeries
                {
                    Title = "Completed Tasks",
                    Values = new ChartValues<int> {compValue},
                    DataLabels= true,
                    LabelPoint = chartPoint =>
               string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation)
        }
            };

            SeriesCollection.Add(new PieSeries
            {
                Title = "Remaining Tasks",
                Values = new ChartValues<int> { remValue },
                DataLabels = true,
                LabelPoint = chartPoint =>
               string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation)

            });

            SeriesCollection.Add(new PieSeries
            {
                Title = "Ongoing Tasks",
                Values = new ChartValues<int> { ongValue },
                DataLabels = true,
                LabelPoint = chartPoint =>
               string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation)

            });


            //for pie chart value
            PointLabel = chartPoint =>
               string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            DataContext = this;

            PieProject.Update();

        }

        public SeriesCollection SeriesCollection { get; set; }
        public Func<ChartPoint, string> PointLabel { get; set; }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;


            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }



        public void getTaskList()
        {
           
            DataTable taskList = new TaskController().getProjectTasks(project.ProjectID);
            ListTask.ItemsSource = taskList.DefaultView;
        }

       

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            new AddTask(employee, project).Show();
            this.Close();
        }

        private void BtnUpdateTask_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                DataRowView CompRow;
                int SComp;
                int task_id;

                SComp = ListTask.SelectedIndex;
                CompRow = ListTask.Items.GetItemAt(SComp) as DataRowView;
                task_id = Convert.ToInt32(CompRow["task_id"]);

                DataTable taskDetail = new TaskController().GetTaskDetails(task_id);
                int taskID = Convert.ToInt32(taskDetail.Rows[0]["TASK_ID"]);
                string taskName = taskDetail.Rows[0]["TASK_NAME"].ToString();
                string startDate = taskDetail.Rows[0]["TASK_START_DATE"].ToString();
                string endDate = taskDetail.Rows[0]["TASK_END_DATE"].ToString();

                Task t = new Task(taskName, startDate, endDate, project.ProjectID);
                new AddTask(project, t).Show();
                this.Close();

            }
            catch (Exception ex) {
               
                MessageBox.Show("Please select a task to update");
            }





        }

        private void mouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView CompRow;
            int SComp;
            int task_id;

            SComp = ListTask.SelectedIndex;
            CompRow = ListTask.Items.GetItemAt(SComp) as DataRowView;
            task_id = Convert.ToInt32(CompRow["task_id"]);

            DataTable taskDetail = new TaskController().GetTaskAllDetails(task_id);
            string taskName = taskDetail.Rows[0]["TASK_NAME"].ToString();
            int project_id = Convert.ToInt32( taskDetail.Rows[0]["PROJECT_ID"]);
            string startDate = taskDetail.Rows[0]["TASK_START_DATE"].ToString();
            string endDate = taskDetail.Rows[0]["TASK_END_DATE"].ToString();
            int taskAssigned = Convert.ToInt32(taskDetail.Rows[0]["TASK_ASSIGNED_EMPLOYEE"]);
            int taskRemaining = Convert.ToInt32(taskDetail.Rows[0]["ESTIMATED_REMAINING_HOUR"]);
            int taskProgress = Convert.ToInt32(taskDetail.Rows[0]["ESTIMATED_PROGRESS"]);
            int taskSkill = Convert.ToInt32(taskDetail.Rows[0]["REQUIRED_SKILLS"]);
            string taskStatus = taskDetail.Rows[0]["TASK_STATUS"].ToString();
            string taskWork = taskDetail.Rows[0]["WORK_DONE"].ToString();

            Task t = new Task(task_id, taskName, startDate, endDate, project.ProjectID, taskStatus, taskAssigned, taskSkill, taskRemaining, taskProgress, taskWork);
            new TaskWindow(employee, project, t).Show();
            this.Close();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (employee.Employee_ID1.Equals(project.Project_admin)) {
                new CreateProject(employee).Show();
                this.Close();
            }
            else {
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new ActivityMonitor(project).Show();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            new ProjectController().DeleteProject(project.ProjectID);
            new CreateProject(employee).Show();
            this.Close();
        }

        private void BtnSubmitStatus_Click(object sender, RoutedEventArgs e)
        {
            // this.TaskStatus
            new ProjectController().UpdateStatus(this.TaskStatus, project.ProjectID);
        }

        private void BtnUpdateProject_Click(object sender, RoutedEventArgs e)
        {
            new ProjectController().UpdateProject(txtProject.Text, txtProjectStartDate.SelectedDate.Value.Date.ToString("yyyy-MM-dd"), txtProjectEndDate.SelectedDate.Value.Date.ToString("yyyy-MM-dd"), project.ProjectID);
        }
    }
}
