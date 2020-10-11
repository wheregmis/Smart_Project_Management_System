using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMS.com.project.models
{
    public class Task
    {
        int taskID, projectID, taskAssignedEmployee, requiredSkill, remainingHour, estimatedProgress;
        string taskName, startDate, endDate, workDone, status, priority;
       

        public Task(string taskName, string startDate, string endDate, int projectID)
        {
            this.taskName = taskName;
            this.startDate = startDate;
            this.endDate = endDate;
            this.projectID = projectID;
        }

        public Task(int task_id, string taskName, string startDate, string endDate, int projectID, string taskStatus, int taskAssigned, int taskSkill, int taskRemaining, int taskProgress, string taskWork)
        {
            this.taskID = task_id;
            this.taskName = taskName;
            this.startDate = startDate;
            this.endDate = endDate;
            this.projectID = projectID;
            this.status = taskStatus;
            this.taskAssignedEmployee = taskAssigned;
            this.requiredSkill = taskSkill;
            this.RemainingHour = taskRemaining;
            this.estimatedProgress = taskProgress;
            this.workDone = taskWork;
        }

        public Task(string taskName, string startDate, string endDate, int projectID, string taskStatus, int taskAssigned, int taskSkill, string priority)
        {
           
            this.taskName = taskName;
            this.startDate = startDate;
            this.endDate = endDate;
            this.projectID = projectID;
            this.status = taskStatus;
            this.taskAssignedEmployee = taskAssigned;
            this.requiredSkill = taskSkill;
            this.priority = priority;
        }

        public int TaskID { get => taskID; set => taskID = value; }
        public int ProjectID { get => projectID; set => projectID = value; }
        public int TaskAssignedEmployee { get => taskAssignedEmployee; set => taskAssignedEmployee = value; }
        public int RequiredSkill { get => requiredSkill; set => requiredSkill = value; }
        public int RemainingHour { get => remainingHour; set => remainingHour = value; }
        public int EstimatedProgress { get => estimatedProgress; set => estimatedProgress = value; }
        public string TaskName { get => taskName; set => taskName = value; }
        public string StartDate { get => startDate; set => startDate = value; }
        public string EndDate { get => endDate; set => endDate = value; }
        public string WorkDone { get => workDone; set => workDone = value; }
        public string Status { get => status; set => status = value; }
        public string Priority { get => priority; set => priority = value; }
    }
}
