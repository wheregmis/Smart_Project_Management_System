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

namespace SPMS.com.project.views.employeePanel
{
    /// <summary>
    /// Interaction logic for ActivityMonitor.xaml
    /// </summary>
    public partial class ActivityMonitor : Window
    {
        Project project;
        public ActivityMonitor(Project project)
        {
            InitializeComponent();
            this.project = project;
            fillActivity();
        }

        public void fillActivity() {
            DataTable dt = new ProjectController().getActivityMonitor(project.ProjectID);
            ListProject.ItemsSource = dt.DefaultView;
        }
    }
}
