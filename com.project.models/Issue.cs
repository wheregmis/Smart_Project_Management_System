using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMS.com.project.models
{
   public class Issue
    {
        int issueID, issueBy;
        string issueTitle, issueDetail;

        public int IssueID { get => issueID; set => issueID = value; }
        public int IssueBy { get => issueBy; set => issueBy = value; }
        public string IssueTitle { get => issueTitle; set => issueTitle = value; }
        public string IssueDetail { get => issueDetail; set => issueDetail = value; }

        public Issue(int issueID, string issueTitle, string issueDetail, int issueBy)
        {
            this.issueID = issueID;
            this.issueBy = issueBy;
            this.issueTitle = issueTitle;
            this.issueDetail = issueDetail;
        }

        public Issue(string issueTitle, string issueDetail, int issueBy)
        {
            this.issueBy = issueBy;
            this.issueTitle = issueTitle;
            this.issueDetail = issueDetail;
        }
    }
}
