using SPMS.com.project.controller;
using SPMS.com.project.models;
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
    /// Interaction logic for AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        Project project;
        Employee employee;
        public AddTask(Employee employee, Project project)
        {
            InitializeComponent();
            this.project = project;
            this.employee = employee;
            btnAddTask.Visibility = Visibility.Visible;
            
            FillSkill();
        }

        public AddTask(Project project, Task task)
        {
            InitializeComponent();
            btnAddTask.Visibility = Visibility.Hidden;
            
            this.project = project;
            txtTask.Text = task.TaskName;
            txtstartDate.Text = task.StartDate.ToString();
            txtEndDate.Text = task.EndDate.ToString();

        }

        public void FillSkill() {
            DataTable dt = new SkillController().GetAllSkill();
            cmbSkill.ItemsSource = dt.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task t = new Task(txtTask.Text, txtstartDate.SelectedDate.Value.Date.ToString("yyyy-MM-dd"), txtEndDate.SelectedDate.Value.Date.ToString("yyyy-MM-dd"), project.ProjectID, "Remaining", Convert.ToInt32( cmbUser.SelectedValue), Convert.ToInt32(cmbSkill.SelectedValue), cmbPriority.SelectedValue.ToString());
          //  MessageBox.Show(cmbPriority.SelectedValue.ToString());
           new TaskController().InsertTask(t);

           
          //  new SendMail().sentMail(email, subject, content);

            new ProjectController().UpdateRemainingTasks(project.ProjectID, 1+project.RemainingTask);
            

            new CreateTask(employee, project).Show();
            DataTable dt = new UserController().GetUserDetailsbyID(Convert.ToInt32(cmbUser.SelectedValue));
            String email = dt.Rows[0]["Employee_Email"].ToString();
            string content = "You have been assigned to a task " + txtTask.Text;
            string subject = "Congratulation";
            new SendMail().sentMail(email, subject, content);
            this.Close();

        }

        private void LoadUsers_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new UserController().getUserHavingSkill(Convert.ToInt32(cmbSkill.SelectedValue));
            cmbUser.ItemsSource = dt.DefaultView;
        }
    }
}
