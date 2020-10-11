using SPMS.com.project.controller;
using SPMS.com.project.models;
using SPMS.com.project.views.managerPanel;
using SPMS.sentimentanalysis;
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

namespace SPMS.com.project.views.reviewuis
{
    /// <summary>
    /// Interaction logic for ReviewOthers.xaml
    /// </summary>
    public partial class ReviewOthers : Window
    {
        Employee employee;
        public ReviewOthers(Employee employee)
        {
            InitializeComponent();
            this.employee = employee;
            LoadProject();
           // LoadProjectUser();
            
        }

        public void LoadProject() {

           DataTable dt = new ProjectController().GetAllProjects();
            cmbProject.ItemsSource = dt.DefaultView;
        }

        public void LoadProjectUser()
        {

            // DataTable dt = new ProjectController().GetAllProjectsUser(Convert.ToInt32(cmbProject.SelectedItem));
            DataTable dt = new ProjectController().GetAllProjectsUser(Convert.ToInt32(cmbProject.SelectedValue));
            cmbUser.ItemsSource = dt.DefaultView;
        }

        private void BtnAnalyse_Click(object sender, RoutedEventArgs e)
        {
            LoadProjectUser();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            TextRange tr = new TextRange(txtReview.Document.ContentStart, txtReview.Document.ContentEnd);

            /*
            ComboBoxItem typeItem = (ComboBoxItem)cmbProject.SelectedItem;
            int projectID = Convert.ToInt32(typeItem.Content);

            ComboBoxItem typeItem2 = (ComboBoxItem)cmbUser.SelectedItem;
            int userID = Convert.ToInt32(typeItem2.Content);
            */
            int projectID = Convert.ToInt32(cmbProject.SelectedValue);
            int userID = Convert.ToInt32(cmbUser.SelectedValue);


            Review review = new Review(projectID,userID, tr.Text.ToString(), employee.Employee_ID1);

            String sentiment = new SentimentAnalysis(tr.Text).getSentiment();
            new ReviewController().InsertReview(review, sentiment);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new UserDashboard(employee.Employee_Email1).Show();
            this.Close();
        }
    }
}
