using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMS.com.project.models
{
    public class Employee
    {
        int Employee_ID;
        String Employee_Name;
        String Employee_Email;
        String Employee_Password;
        String Employee_Gender;
        String dob;
        String address;
        int Employee_Type;

        public int Employee_ID1 { get => Employee_ID; set => Employee_ID = value; }
        public string Employee_Name1 { get => Employee_Name; set => Employee_Name = value; }
        public string Employee_Email1 { get => Employee_Email; set => Employee_Email = value; }
        public string Employee_Password1 { get => Employee_Password; set => Employee_Password = value; }
        public int Employee_Type1 { get => Employee_Type; set => Employee_Type = value; }
        public string Employee_Gender1 { get => Employee_Gender; set => Employee_Gender = value; }
        public string Dob { get => dob; set => dob = value; }
        public string Address { get => address; set => address = value; }

        public Employee(string employee_Name, string employee_Email, string employee_Password, string employee_Gender)
        {
            Employee_Name = employee_Name;
            Employee_Email = employee_Email;
            Employee_Password = employee_Password;
            Employee_Gender = employee_Gender;

        }

        public Employee(string employee_Name, string employee_Email, string employee_Password, string employee_Gender, string dob, string address)
        {
            
            this.Employee_Name = employee_Name;
            this.Employee_Email = employee_Email;
            this.Employee_Password = employee_Password;
            this.Employee_Gender = employee_Gender;
            this.dob = dob;
            this.address = address;

        }

        public Employee(int employID, string employee_Name, string employee_Email, string employee_Password, string employee_Gender, string dob, string address)
        {
            this.Employee_ID1 = employID;
            this.Employee_Name = employee_Name;
            this.Employee_Email = employee_Email;
            this.Employee_Password = employee_Password;
            this.Employee_Gender = employee_Gender;
            this.dob = dob;
            this.address = address;

        }

        public Employee(int employee_id, string employee_Name, string employee_Email, string employee_Password, string employee_Gender, int employType)
        {
            Employee_ID = employee_id;
            Employee_Name = employee_Name;
            Employee_Email = employee_Email;
            Employee_Password = employee_Password;
            Employee_Gender = employee_Gender;
            this.Employee_Type = employType;

        }

       
    }
}
