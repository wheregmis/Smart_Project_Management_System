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
    class SkillController
    {

        public DataTable GetAllSkill()
        {

            String query = "select * from tbl_skill";
            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;
        }

        public DataTable GetYourSkills(int id)
        {

            String query = "select s.SKILL_NAME, es.EXPERIENCE from tbl_skill s, tbl_employee_skills es, tbl_employee e WHERE s.SKILL_ID = es.SKILL_ID and es.EMPLOYEE_ID = e.EMPLOYEE_ID and e.EMPLOYEE_ID = "+id;
            DataTable dt = new DatabaseConnection().GetData(query);
            return dt;
        }

        //method to insert user
        public void InsertSkill(string skill)
        {
            string query = "INSERT INTO `tbl_skill` (`SKILL_ID`, `SKILL_NAME`) VALUES (NULL, '"+skill+"')";
            Console.WriteLine(query);
            new DatabaseConnection().InsertData(query);

            MessageBox.Show("Skill Sucessfully Created");
        }

        public void InsertEmployeeSkill(int employeID, int SkillID, int experience)
        {
            string query = "INSERT INTO `tbl_employee_skills` (`EMPLYEE_SKILL_ID`, `SKILL_ID`, `EMPLOYEE_ID`, `EXPERIENCE`) VALUES (NULL, '"+SkillID+"', '"+employeID+"', '"+experience+"')";
            Console.WriteLine(query);
            new DatabaseConnection().InsertData(query);

            MessageBox.Show("Skill Sucessfully Updated");
        }
    }
}
