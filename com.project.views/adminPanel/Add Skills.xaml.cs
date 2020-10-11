using SPMS.com.project.controller;
using SPMS.com.project.models;
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

namespace SPMS.com.project.views.adminPanel
{
    /// <summary>
    /// Interaction logic for Add_Skills.xaml
    /// </summary>
    public partial class Add_Skills : Window
    {
        Employee employee;
        public Add_Skills(Employee employee)
        {
            InitializeComponent();
            listSkill.Items.Clear();
            fillSkill();
            this.employee = employee;
            
        }

        public void fillSkill()
        {
            DataTable skillList = new SkillController().GetAllSkill();
            listSkill.ItemsSource = skillList.DefaultView;

        }
       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new SkillController().InsertSkill(txtSkill.Text.ToString());
            fillSkill();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new AdminDashboard(employee.Employee_Email1).Show();
            this.Close();
        }
    }
}
