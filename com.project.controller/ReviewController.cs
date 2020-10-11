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
    class ReviewController
    {


        public DataTable GetAllReviews()
        {
            string query = "select issue_id, issue_title, employee_name from tbl_issues, tbl_employee where issue_by = employee_id";

            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;

        }

        //method to get user details
        public DataTable GetYourReviews(int id)
        {
             
            String query = "select DISTINCT p.PROJECT_NAME, r.REVIEW, r.REVIEW_SENTIMENT from tbl_review r, tbl_project p , tbl_employee e WHERE r.REVIEW_PROJECT = p.PROJECT_ID AND r.REVIEWED_FOR =" + id;
            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;
        }

        //method to insert user
        public void InsertReview(Review u, string sentiment)
        {
            string query = "INSERT INTO `tbl_review` (`REVIEW_ID`, `REVIEW`, `REVIEW_PROJECT`, `REVIEWED_FOR`, `REVIEWED_BY`, REVIEW_SENTIMENT) VALUES (NULL, '" + u.ReviewDesc+"', '"+u.Reviewproject+"', '"+u.Reviewfor+"', '"+u.Reviewby+ "', '" + sentiment + "')";
            Console.WriteLine(query);
            new DatabaseConnection().InsertData(query);

            MessageBox.Show("User Sucessfully Inserted");
        }
    }
}
