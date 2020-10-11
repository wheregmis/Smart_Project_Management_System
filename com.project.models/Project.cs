using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SPMS.com.project.models
{
    public class Project
    {
        int projectID, project_admin, completedTask, ongoingTask, remainingTask;
        string projectName, startDate, endDate, status;
       

        public Project(string projectName, string startDate, string endDate, int project_admin)
        {
            this.projectName = projectName;
            this.startDate = startDate;
            this.endDate = endDate;
            this.project_admin = project_admin;

        }

        public Project(int projectID, string projectName, string startDate, string endDate, int project_admin, int completedTask, int ongoingTask, int remainingTask, string status)
        {
            this.projectID = projectID;
            this.projectName = projectName;
            this.startDate = startDate;
            this.endDate = endDate;
            this.project_admin = project_admin;
            this.completedTask = completedTask;
            this.ongoingTask = ongoingTask;
            this.remainingTask = remainingTask;
            this.status = status;
        }

        public string ProjectName { get => projectName; set => projectName = value; }
        public string StartDate { get => startDate; set => startDate = value; }
        public string EndDate { get => endDate; set => endDate = value; }
        public int Project_admin { get => project_admin; set => project_admin = value; }
        public int ProjectID { get => projectID; set => projectID = value; }
        public string Status { get => status; set => status = value; }
        public int CompletedTask { get => completedTask; set => completedTask = value; }
        public int OngoingTask { get => ongoingTask; set => ongoingTask = value; }
        public int RemainingTask { get => remainingTask; set => remainingTask = value; }
    }
}
