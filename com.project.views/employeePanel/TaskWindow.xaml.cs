using SPMS.com.project.controller;
using SPMS.com.project.models;
using SPMS.sentimentanalysis;
using SPMS.smtpServer;
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
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        Task task;
        Project project;
        Employee employee;
        string TaskStatus;


        public TaskWindow(Employee employee, Project project, Task task)
        {
            InitializeComponent();
            this.task = task;
            this.project = project;
            this.employee = employee;
            FillSkill();
            FillData();
            btnSubmit.Visibility = Visibility.Hidden;
            btnShare.Visibility = Visibility.Hidden;
            btnUpdate.Visibility = Visibility.Hidden;
            btnReassign.Visibility = Visibility.Hidden;
            cmbSkill.Visibility = Visibility.Hidden;
            cmbUser.Visibility = Visibility.Hidden;
            btnAssign.Visibility = Visibility.Hidden; ;

            if (task.TaskAssignedEmployee.Equals(employee.Employee_ID1)) {
                btnSubmit.Visibility = Visibility.Visible;
                btnShare.Visibility = Visibility.Visible;
              
            }
            if (project.Project_admin.Equals(employee.Employee_ID1)) {
                btnUpdate.Visibility = Visibility.Visible;
                btnReassign.Visibility = Visibility.Visible;
            }
            
        }

        public void FillSkill()
        {
            DataTable dt = new SkillController().GetAllSkill();
            cmbSkill.ItemsSource = dt.DefaultView;
        }

        public void FillData() {
            DataTable taskDetail = new TaskController().GetTaskDetails(task.TaskID);
          //  txtTaskID.Text = taskDetail.Rows[0]["TASK_ID"].ToString();
            txtTask.Text = taskDetail.Rows[0]["TASK_NAME"].ToString();
            txtstartDate.Text = taskDetail.Rows[0]["TASK_START_DATE"].ToString();
            txtEndDate.Text = taskDetail.Rows[0]["TASK_END_DATE"].ToString();
            txtAssign.Content = taskDetail.Rows[0]["EMPLOYEE_NAME"].ToString();
            txtRemainingHour.Text = taskDetail.Rows[0]["ESTIMATED_REMAINING_HOUR"].ToString();
            txtCompletedProgress.Text = taskDetail.Rows[0]["ESTIMATED_PROGRESS"].ToString();
            txtSkill.Content = taskDetail.Rows[0]["SKILL_NAME"].ToString();
            cmbPriority.SelectedValue = taskDetail.Rows[0]["PRIORITY"].ToString();

            string status = taskDetail.Rows[0]["TASK_STATUS"].ToString();

            if (status.Equals("In Progress"))
            {
                rdoProgress.IsChecked = true;
            }
            else if (status.Equals("Completed"))
            {
                rdoCompleted.IsChecked = true;
            }
            else if (status.Equals("Remaining"))
            {
                rdoRemaining.IsChecked = true;
            }
            else {
                rdoProgress.IsChecked = false;
                rdoCompleted.IsChecked = false;
                rdoRemaining.IsChecked = false;
            }

            TextRange tr = new TextRange(txtWork.Document.ContentStart, txtWork.Document.ContentEnd);
            tr.Text = taskDetail.Rows[0]["WORK_DONE"].ToString();


        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            this.TaskStatus = (string)(sender as RadioButton).Content;
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(TaskStatus.ToString());
            TextRange tr = new TextRange(txtWork.Document.ContentStart, txtWork.Document.ContentEnd);
            
            new TaskController().UpdateTask(Convert.ToInt32(txtRemainingHour.Text), Convert.ToInt32(txtRemainingHour.Text), tr.Text, TaskStatus.ToString(), task.TaskID);

            if (TaskStatus.Equals("In Progress"))
            {
                new ProjectController().UpdateOngoingTasks(project.ProjectID, 1 + project.RemainingTask);
            }
            else if (TaskStatus.Equals("Completed"))
            {
                new ProjectController().UpdateCompletedTasks(project.ProjectID, 1 + project.RemainingTask);
            }
            
            else
            {
               
            }
            new ProjectController().UpdateRemainingTasks(project.ProjectID, 1 + project.RemainingTask);
            DataTable dt = new UserController().GetUserDetailsbyID(Convert.ToInt32(project.Project_admin));
            String email = dt.Rows[0]["Employee_Email"].ToString();
            string content = employee.Employee_Email1 + " has submitted his work. Please check on your projects";
            string subject = "Task Progress";
            new SendMail().sentMail(email, subject, content);

            // this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new CreateTask(employee, project).Show();
            this.Close();
        }

        private void BtnShare_Click(object sender, RoutedEventArgs e)
        {
            TextRange tr = new TextRange(txtExperience.Document.ContentStart, txtExperience.Document.ContentEnd);

            String feedbackSentiment = new SentimentAnalysis(tr.Text).getSentiment();
            double sentimentProbability = new SentimentAnalysis(tr.Text).getProbability();
            new ProjectController().InsertFeedback(tr.Text, project.ProjectID, employee.Employee_ID1, feedbackSentiment, sentimentProbability);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            new TaskController().DeleteTask(task.TaskID);
            new CreateTask(employee, project).Show();
            this.Close();
        }

        private void BtnDelete_Copy_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new UserController().getUserHavingSkill(Convert.ToInt32(cmbSkill.SelectedValue));
            cmbUser.ItemsSource = dt.DefaultView;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new TaskController().UpdateTaskInfo(task.TaskID, txtTask.Text, txtstartDate.SelectedDate.Value.Date.ToString("yyyy-MM-dd"), txtEndDate.SelectedDate.Value.Date.ToString("yyyy-MM-dd"), cmbPriority.SelectedValue.ToString());
        }

       
        private void BtnReassign_Click(object sender, RoutedEventArgs e)
        {
            cmbSkill.Visibility = Visibility.Visible;
            cmbUser.Visibility = Visibility.Visible;
            btnAssign.Visibility = Visibility.Visible;
        }

        private void BtnAssign_Click(object sender, RoutedEventArgs e)
        {
            new TaskController().ReassignUser(task.TaskID, cmbSkill.SelectedValue.ToString(), cmbUser.SelectedValue.ToString());
        }
    }
}
