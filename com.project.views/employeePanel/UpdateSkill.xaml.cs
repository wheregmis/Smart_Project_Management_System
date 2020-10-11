using SPMS.com.project.controller;
using SPMS.com.project.models;
using SPMS.com.project.views.managerPanel;
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

namespace SPMS.com.project.views.employeePanel
{
    /// <summary>
    /// Interaction logic for UpdateSkill.xaml
    /// </summary>
    public partial class UpdateSkill : Window
    {
        Employee employee;
        public UpdateSkill(Employee employee)
        {
            InitializeComponent();
            this.employee = employee;
            FillSkill();
            getSkills();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new SkillController().InsertEmployeeSkill(employee.Employee_ID1, Convert.ToInt32(cmbSkill.SelectedValue), Convert.ToInt32(txtExperience.Text));
            getSkills();
        }
        public void FillSkill()
        {
            DataTable dt = new SkillController().GetAllSkill();
            cmbSkill.ItemsSource = dt.DefaultView;
        }

        public void getSkills()
        {
            DataTable skillList = new SkillController().GetYourSkills(employee.Employee_ID1);
            listUser.ItemsSource = skillList.DefaultView;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new UserDashboard(employee.Employee_Email1).Show();
            this.Close();
        }
    }
}
