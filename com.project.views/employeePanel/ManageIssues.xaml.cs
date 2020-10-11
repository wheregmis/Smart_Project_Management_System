using SPMS.com.project.controller;
using SPMS.com.project.models;
using SPMS.com.project.views.issueview;
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
    /// Interaction logic for ManageIssues.xaml
    /// </summary>
    public partial class ManageIssues : Window
    {
        Employee employee;
        Issue issue;
        public ManageIssues(Employee employee)
        {
            InitializeComponent();
            listIssues.Items.Clear();
            getIssueList();
            this.employee = employee;
            
        }


        private void mouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView CompRow;
            int SComp;
            int issueID;

            SComp = listIssues.SelectedIndex;
            CompRow = listIssues.Items.GetItemAt(SComp) as DataRowView;
            issueID = Convert.ToInt32(CompRow["ISSUE_ID"]);

            DataTable IssueDetail = new IssueController().GetIssueDetail(issueID);
            
            
            string issueTitle = IssueDetail.Rows[0]["ISSUE_TITLE"].ToString();
            string issueDetail = IssueDetail.Rows[0]["ISSUE_DETAIL"].ToString();            
            int issueBy = Convert.ToInt32(IssueDetail.Rows[0]["ISSUE_BY"]);

            issue = new Issue(issueID, issueTitle, issueDetail, issueBy);
            new IssueForm(employee, issue).Show();
            this.Close();

        }

        public void getIssueList()
        {
            DataTable projectList = new IssueController().GetAllIssues();
            listIssues.ItemsSource = projectList.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ReportIssue(employee).Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new UserDashboard(employee.Employee_Email1).Show();
            this.Close();
        }
    }
}
