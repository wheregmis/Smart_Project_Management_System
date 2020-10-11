using SPMS.com.project.dbconnection;
using SPMS.com.project.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SPMS.com.project.controller
{
    class IssueController
    {
        public DataTable GetAllIssues()
        {
            string query = "select issue_id, issue_title, employee_name from tbl_issues, tbl_employee where issue_by = employee_id";

            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;

        }

        //method to get user details
        public DataTable GetIssueDetail(int issueID)
        {

            String query = "SELECT * FROM `tbl_issues` where ISSUE_ID = "+issueID;
            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;
        }

        //method to insert user
        public void InsertIssue(Issue u)
        {
            string query = "INSERT INTO `tbl_issues` (`ISSUE_ID`, `ISSUE_TITLE`, `ISSUE_DETAIL`, `ISSUE_BY`) VALUES (NULL, '"+u.IssueTitle+"', '"+u.IssueDetail+"', '"+u.IssueBy+"');";
            Console.WriteLine(query);
            new DatabaseConnection().InsertData(query);

            MessageBox.Show("User Sucessfully Inserted");
        }

        //method to update user
        public void UpdateUser(Employee u)
        {
            string query = "UPDATE `tbl_employee` SET `EMPLOYEE_NAME` = '" + u.Employee_Name1 + "', `EMPLOYEE_EMAIL` = '" + u.Employee_Email1 + "', `ADDRESS` = '" + u.Address + "' WHERE `tbl_employee`.`EMPLOYEE_ID` = " + u.Employee_ID1;
            Console.WriteLine(query);
            new DatabaseConnection().UpdateData(query);
            MessageBox.Show("User Sucessfully Updated");
        }

    }
}
