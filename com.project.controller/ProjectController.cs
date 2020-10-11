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
    class ProjectController
    {

        public DataTable GetAllProjects()
        {
            string query = "select * from tbl_project";

            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;

        }

        public DataTable GetInvolvedProjects(int id)
        {
            string query = "SELECT p.PROJECT_ID, p.PROJECT_NAME, p.PROJECT_START_DATE, p.PROJECT_END_DATE FROM tbl_task t, tbl_project p WHERE p.PROJECT_ID = t.PROJECT_ID and t.TASK_ASSIGNED_EMPLOYEE = "+id;

            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;

        }

        internal DataTable getActivityMonitor(int id)
        {
            string query = "select DISTINCT e.EMPLOYEE_NAME, t.TASK_NAME, t.ESTIMATED_REMAINING_HOUR, t.ESTIMATED_PROGRESS, f.FEEDBACK_SENTIMENT from tbl_task t, tbl_employee e, tbl_employee_project_feedback f WHERE t.PROJECT_ID = f.PROJECT_ID and t.TASK_ASSIGNED_EMPLOYEE = e.EMPLOYEE_ID and e.EMPLOYEE_ID = f.FEEDBACK_BY and t.PROJECT_ID = " + id;
            Console.WriteLine(query);
            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;
        }

        public DataTable GetAllProjectsUser(int id)
        {
            string query = "SELECT tbl_employee.EMPLOYEE_EMAIL, tbl_employee.EMPLOYEE_ID FROM `tbl_task`, tbl_project, tbl_employee WHERE tbl_task.PROJECT_ID = tbl_project.PROJECT_ID and tbl_task.TASK_ASSIGNED_EMPLOYEE = tbl_employee.EMPLOYEE_ID and tbl_project.PROJECT_ID = "+id;

            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;

        }

        //method to get user details
        public DataTable GetProjectDetails(int id)
        {

            String query = "select * from tbl_project where project_id = '" + id + "'";
            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;
        }

        //method to insert user
        public void InsertProject(Project P)
        {
            string query = "INSERT INTO `tbl_project` (`PROJECT_ID`, `PROJECT_NAME`, `PROJECT_START_DATE`, `PROJECT_END_DATE`, `PROJECT_ADMIN`, PROJECT_STATUS) VALUES (NULL, '" + P.ProjectName+"', '"+P.StartDate+"', '"+P.EndDate+"', '"+P.Project_admin+"', 'Remaining')";
            Console.WriteLine(query);
            new DatabaseConnection().InsertData(query);

            MessageBox.Show("Project Sucessfully Created");
        }

        public void UpdateRemainingTasks(int id, int reminingTasks)
        {
            string query = "UPDATE `tbl_project` SET `REMAINING_TASK` = '"+ reminingTasks + "' WHERE `tbl_project`.`PROJECT_ID` = "+id;
            Console.WriteLine(query);
            new DatabaseConnection().InsertData(query);

            MessageBox.Show("Project Sucessfully Created");
        }

        public void UpdateOngoingTasks(int id, int ongoingTask)
        {
            string query = "UPDATE `tbl_project` SET `ONGOING_TASK` = '" + ongoingTask + "' WHERE `tbl_project`.`PROJECT_ID` = " + id;
            Console.WriteLine(query);
            new DatabaseConnection().InsertData(query);

            MessageBox.Show("Project Sucessfully Created");
        }

        public void UpdateCompletedTasks(int id, int completedTask)
        {
            string query = "UPDATE `tbl_project` SET `COMPLETED_TASK` = '" + completedTask + "' WHERE `tbl_project`.`PROJECT_ID` = " + id;
            Console.WriteLine(query);
            new DatabaseConnection().InsertData(query);

            MessageBox.Show("Project Sucessfully Created");
        }

        public DataTable GetYourProjects(int employee_id)
        {
            string query = "select * from tbl_project where project_admin ="+employee_id;

            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;
        }

        internal void InsertFeedback(string text, int projectID, int employee_ID1, string feedbackSentiment, double sentimentProbability)
        {
            string query = "INSERT INTO `tbl_employee_project_feedback` (`FEEDBACK_ID`, `FEEDBACK`, `PROJECT_ID`, `FEEDBACK_BY`, `FEEDBACK_SENTIMENT`, `SENTIMENT_PROBABILITY`) VALUES (NULL, '"+text+"', '"+projectID+"', '"+employee_ID1+"', '"+feedbackSentiment+"', '"+sentimentProbability+"')";

            Console.WriteLine(query);
            new DatabaseConnection().InsertData(query);

            MessageBox.Show("Thank you for your feedback");
        }

        internal void DeleteProject(int projectID)
        {
            string query = "DELETE FROM `tbl_project` WHERE `tbl_project`.`PROJECT_ID` = "+projectID;
            Console.WriteLine(query);
            new DatabaseConnection().DeleteData(query);

            MessageBox.Show("Project Sucessfully Deleted");
        }

        internal void UpdateStatus(string taskStatus, int projectID)
        {
            string query = "UPDATE `tbl_project` SET `PROJECT_STATUS` = '"+taskStatus+"' WHERE `tbl_project`.`PROJECT_ID` = " + projectID;
            Console.WriteLine(query);
            new DatabaseConnection().InsertData(query);

            MessageBox.Show("Project Status Sucessfully Updated");
        }

        internal void UpdateProject(string text, string v1, string v2, int projectID)
        {
            string query = "UPDATE `tbl_project` SET `PROJECT_NAME` = '"+text+"', `PROJECT_START_DATE` = '"+v1+"', `PROJECT_END_DATE` = '"+v2+"' WHERE `tbl_project`.`PROJECT_ID` = " + projectID;
            Console.WriteLine(query);
            new DatabaseConnection().InsertData(query);

            MessageBox.Show("Project Status Sucessfully Updated");
        }
    }
}
