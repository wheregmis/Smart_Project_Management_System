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
    class IssueFixController
    {
        public DataTable GetIssueFixes(int issueID)
        {

            String query = "SELECT f.ISSUE_FIX_ID, f.FIX_TITLE, e.EMPLOYEE_NAME FROM tbl_issue_fix f, tbl_issues i, tbl_employee e where f.ISSUE_ID = i.ISSUE_ID and f.FIXED_BY = e.EMPLOYEE_ID and i.ISSUE_ID = " + issueID;
            Console.WriteLine(query);
            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;
        }

        public DataTable GetFixDetails(int fixID)
        {

            String query = "SELECT * FROM `tbl_issue_fix` where ISSUE_FIX_ID =" + fixID;
            Console.WriteLine(query);
            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;
        }

        internal void insertIssueFixes(IssueFix i)
        {
            string query = "INSERT INTO `tbl_issue_fix` (`ISSUE_FIX_ID`, `FIX_TITLE`, `FIX_DESCRIPTION`, `FIXED_BY`, `ISSUE_ID`) VALUES (NULL, '"+i.FixTitle+"', '"+i.FixDetail+"', '"+i.FixBy+"', '"+i.IssueID+"')";
            Console.WriteLine(query);
            new DatabaseConnection().InsertData(query);

            MessageBox.Show("Fix Sucessfully Inserted");
        }

        internal void UpdateFixStatus(int fixID, string status)
        {
            string query = "UPDATE `tbl_issue_fix` SET `FIX_STATUS` = '"+status+"' WHERE `tbl_issue_fix`.`ISSUE_FIX_ID` = "+fixID;
            Console.WriteLine(query);
            new DatabaseConnection().UpdateData(query);

            MessageBox.Show("Fix Sucessfully Updated");
        }
    }
}
