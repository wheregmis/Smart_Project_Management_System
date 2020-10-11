using SPMS.com.project.dbconnection;
using SPMS.com.project.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace SPMS.com.project.controller
{
    class TaskController
    {
        public DataTable GetAllTasks()
        {
            string query = "select * from tbl_task";

            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;

        }

        public DataTable getProjectTasks(int id)
        {
            string query = "select * from tbl_task where project_id = '"+id+"' ORDER BY PRIORITY";

            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;

        }

        //method to get user details
        public DataTable GetTaskAllDetails(int id)
        {

            String query = "select * from tbl_task where TASK_ID = " + id;
            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;
        }

        //method to get user details
        public DataTable GetTaskDetails(int id)
        {

            String query = "select t.TASK_ID, t.TASK_STATUS, t.TASK_NAME, t.PROJECT_ID, t.TASK_START_DATE, t.TASK_END_DATE, e.EMPLOYEE_NAME, t.ESTIMATED_REMAINING_HOUR, t.ESTIMATED_PROGRESS, t.WORK_DONE, t.PRIORITY, s.SKILL_NAME from tbl_task t, tbl_skill s, tbl_employee e where t.TASK_ASSIGNED_EMPLOYEE = e.EMPLOYEE_ID and t.REQUIRED_SKILLS = s.SKILL_ID and t.TASK_ID = "+id;
            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;
        }

        //method to insert user
        public void InsertTask(Task T)
        {
            string query = "INSERT INTO `tbl_task` (`TASK_ID`, `TASK_NAME`, `TASK_START_DATE`, `TASK_END_DATE`, `PROJECT_ID`, `TASK_STATUS`, `TASK_ASSIGNED_EMPLOYEE`, `REQUIRED_SKILLS`, `ESTIMATED_REMAINING_HOUR`, `ESTIMATED_PROGRESS`, `WORK_DONE`, `PRIORITY`) VALUES (NULL, '"+T.TaskName+"', '"+T.StartDate+"', '"+T.EndDate+"', '"+T.ProjectID+"', 'Remaining', '"+T.TaskAssignedEmployee+"', '"+T.RequiredSkill+"', '', '', '', '"+T.Priority+"')";
            Console.WriteLine(query);
            new DatabaseConnection().InsertData(query);

            MessageBox.Show("Task Sucessfully Created");
        }

        //method to insert user
        public void UpdateTask(int remaininHours, int progress, string work, string status, int taskID)
        {
            string query = "UPDATE `tbl_task` SET `TASK_STATUS` = '"+status+"', `ESTIMATED_REMAINING_HOUR` = '"+remaininHours+"', `ESTIMATED_PROGRESS` = '"+progress+"', `WORK_DONE` = '"+work+"' WHERE `tbl_task`.`TASK_ID` = "+taskID;
            Console.WriteLine(query);
            new DatabaseConnection().UpdateData(query);

            MessageBox.Show("Task Sucessfully Created");
        }

        internal void DeleteTask(int taskID)
        {
            string query = "DELETE FROM `tbl_task` WHERE TASK_ID = " + taskID;
            Console.WriteLine(query);
            new DatabaseConnection().DeleteData(query);

            MessageBox.Show("Project Sucessfully Deleted");
        }

        internal void UpdateTaskInfo(int taskID, string text, string v1, string v2, string priority)
        {
            string query = "UPDATE `tbl_task` SET `TASK_NAME` = '"+text+"', `TASK_START_DATE` = '"+v1+"', `TASK_END_DATE` = '"+v2+"', `PRIORITY` = '"+priority+"' WHERE `tbl_task`.`TASK_ID` =" + taskID;
            Console.WriteLine(query);
            new DatabaseConnection().UpdateData(query);

            MessageBox.Show("Task Sucessfully Updated");
        }

        internal void ReassignUser(int taskID, string v1, string v2)
        {
            string query = "UPDATE `tbl_task` SET `TASK_ASSIGNED_EMPLOYEE` = '"+v2+"', `REQUIRED_SKILLS` = '"+v1+"' WHERE `tbl_task`.`TASK_ID` = " + taskID;
            Console.WriteLine(query);
            new DatabaseConnection().UpdateData(query);

            MessageBox.Show("Employee Sucessfully Re-Assigned");
        }
    }
}
