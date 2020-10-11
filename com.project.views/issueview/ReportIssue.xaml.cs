using SPMS.com.project.controller;
using SPMS.com.project.models;
using SPMS.com.project.views.employeePanel;
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
using System.Windows.Shapes;

namespace SPMS.com.project.views.issueview
{
    /// <summary>
    /// Interaction logic for ReportIssue.xaml
    /// </summary>
    public partial class ReportIssue : Window
    {
        Employee employee;
        public ReportIssue(Employee employe)
        {
            InitializeComponent();
            this.employee = employe;
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {

            TextRange tr = new TextRange(txtDetail.Document.ContentStart, txtDetail.Document.ContentEnd);

            Issue i = new Issue(txtTitle.Text.ToString(), tr.Text.ToString(), employee.Employee_ID1);
            new IssueController().InsertIssue(i);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ManageIssues(employee).Show();
            this.Close();
        }
    }
}
