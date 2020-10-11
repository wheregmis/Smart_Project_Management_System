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
    class UserController
    {
        //method to get all users
        public DataTable GetAllUsers()
        {
            string query = "select * from tbl_employee";

            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;

        }

        //method to get user details
        public DataTable GetUserDetails(String email)
        {

            String query = "select * from tbl_employee where employee_email = '" + email + "'";
            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;
        }

        public DataTable GetUserDetailsbyID(int ID)
        {

            String query = "select * from tbl_employee where employee_id = " + ID;
            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;
        }
        //method to insert user
        public void InsertUser(Employee u)
        {
            string query = "INSERT INTO `tbl_employee` (`EMPLOYEE_ID`, `EMPLOYEE_NAME`, `EMPLOYEE_EMAIL`, `EMPLOYEE_PASSWORD`, `EMPLOYEE_GENDER`, `EMPLOYEE_TYPE`, `DATE_OF_BIRTH`, `ADDRESS`) VALUES(NULL, '" + u.Employee_Name1+"', '"+u.Employee_Email1+"', '"+u.Employee_Password1+"', '"+u.Employee_Gender1+ "', '2', '"+u.Dob+"', '"+u.Address+"')";
            Console.WriteLine(query);
            new DatabaseConnection().InsertData(query);

            MessageBox.Show("User Sucessfully Inserted");
        }
      
        //method to update user
        public void UpdateUser(Employee u)
        {
            string query = "UPDATE `tbl_employee` SET `EMPLOYEE_NAME` = '"+u.Employee_Name1+"', `EMPLOYEE_EMAIL` = '"+u.Employee_Email1+"', `EMPLOYEE_PASSWORD` = '"+u.Employee_Password1+"', `ADDRESS` = '"+u.Address+"' WHERE `tbl_employee`.`EMPLOYEE_ID` = " + u.Employee_ID1;
            Console.WriteLine(query);
            new DatabaseConnection().UpdateData(query);
            MessageBox.Show("User Sucessfully Updated");
        }

        public DataTable getUserHavingSkill(int id)
        {

            String query = "SELECT DISTINCT e.EMPLOYEE_NAME, e.EMPLOYEE_ID FROM tbl_employee e, tbl_employee_skills es WHERE es.EMPLOYEE_ID = e.EMPLOYEE_ID and es.SKILL_ID = "+id;
            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;
        }

        internal void DeleteUser(string text)
        {
            string query = "Delete from tbl_employee where employee_email = '"+text+"'";
            Console.WriteLine(query);
            new DatabaseConnection().DeleteData(query);
            MessageBox.Show("User Sucessfully Deleted");
        }

        internal void InsertAdmin(Employee u)
        {
            string query = "INSERT INTO `tbl_employee` (`EMPLOYEE_ID`, `EMPLOYEE_NAME`, `EMPLOYEE_EMAIL`, `EMPLOYEE_PASSWORD`, `EMPLOYEE_GENDER`, `EMPLOYEE_TYPE`, `DATE_OF_BIRTH`, `ADDRESS`) VALUES(NULL, '" + u.Employee_Name1 + "', '" + u.Employee_Email1 + "', '" + u.Employee_Password1 + "', '" + u.Employee_Gender1 + "', '1', '" + u.Dob + "', '" + u.Address + "')";
            Console.WriteLine(query);
            new DatabaseConnection().InsertData(query);

            MessageBox.Show("User Sucessfully Inserted");
        }
        /*

//method to delete user
public void DeleteUser(User u)
{
string query = "Delete from tbl_users where userID = " + u.Id;
Console.WriteLine(query);
new DatabaseConnection().DeleteData(query);
MessageBox.Show("User Sucessfully Deleted");
}
*/

    }
}

