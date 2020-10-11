using SPMS.com.project.controller;
using SPMS.com.project.models;
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

namespace SPMS.com.project.views.issueview
{
    /// <summary>
    /// Interaction logic for IssueForm.xaml
    /// </summary>
    public partial class IssueForm : Window
    {
        Employee employee;
        Issue issue;
        int fixID;
        string TaskStatus;
        public IssueForm(Employee employee, Issue issue)
        {
            InitializeComponent();
            this.issue = issue;
            this.employee = employee;
            listIssueFixes.Items.Clear();
            FillFixes();
            txtIssueTitle.Text = issue.IssueTitle.ToString();

            DataTable dt = new UserController().GetUserDetailsbyID(this.issue.IssueBy);
            txtIssueBy.Text = dt.Rows[0]["EMPLOYEE_NAME"].ToString();

            TextRange tr = new TextRange(txtIssueDescription.Document.ContentStart, txtIssueDescription.Document.ContentEnd);
            tr.Text = issue.IssueDetail.ToString();
        }

        public void FillFixes() {
            DataTable listfixes = new IssueFixController().GetIssueFixes(issue.IssueID);
            listIssueFixes.ItemsSource = listfixes.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ManageIssues(this.employee).Show();
        }

        private void BtnResolve_Click(object sender, RoutedEventArgs e)
        {
            TextRange tr = new TextRange(txtFixDescription.Document.ContentStart, txtFixDescription.Document.ContentEnd);
            
            IssueFix issuefix = new IssueFix(employee.Employee_ID1, issue.IssueID, txtFixTitle.Text, tr.Text);
            new IssueFixController().insertIssueFixes(issuefix);
            FillFixes();
        }

        private void mouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView CompRow;
            int SComp;
            int fixID;

            SComp = listIssueFixes.SelectedIndex;
            CompRow = listIssueFixes.Items.GetItemAt(SComp) as DataRowView;
            fixID = Convert.ToInt32(CompRow["ISSUE_FIX_ID"]);

            DataTable fixDetails = new IssueFixController().GetFixDetails(fixID);

            int fixBy = Convert.ToInt32( fixDetails.Rows[0]["FIXED_BY"]);

            DataTable dt = new UserController().GetUserDetailsbyID(fixBy);
            txtFixBy.Text = dt.Rows[0]["EMPLOYEE_NAME"].ToString();

            txtFixTitle.Text = fixDetails.Rows[0]["FIX_TITLE"].ToString();
            
            TextRange tr = new TextRange(txtFixDescription.Document.ContentStart, txtFixDescription.Document.ContentEnd);
            tr.Text = fixDetails.Rows[0]["FIX_DESCRIPTION"].ToString();

            string fixStatus = fixDetails.Rows[0]["FIX_STATUS"].ToString();
            if (fixStatus.Equals("Worked"))
            {
                rdoWorked.IsChecked = true;
            }
            else if (fixStatus.Equals("Didnt Worked"))
            {
                rdodidntworked.IsChecked = true;
            }
            else {
                rdoWorked.IsChecked = false;
                rdodidntworked.IsChecked = false;
            }

        }
        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            this.TaskStatus = (string)(sender as RadioButton).Content;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataRowView CompRow;
            int SComp;
            int fixID;

            SComp = listIssueFixes.SelectedIndex;
            CompRow = listIssueFixes.Items.GetItemAt(SComp) as DataRowView;
            fixID = Convert.ToInt32(CompRow["ISSUE_FIX_ID"]);

            
            new IssueFixController().UpdateFixStatus(fixID, this.TaskStatus);
            FillFixes();
        }
    }
}
