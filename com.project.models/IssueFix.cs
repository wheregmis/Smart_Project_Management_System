using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMS.com.project.models
{
    class IssueFix
    {
        int issuefixID, fixBy, issueID;
        string fixTitle, fixDetail;

        public int IssuefixID { get => issuefixID; set => issuefixID = value; }
        public int FixBy { get => fixBy; set => fixBy = value; }
        public int IssueID { get => issueID; set => issueID = value; }
        public string FixTitle { get => fixTitle; set => fixTitle = value; }
        public string FixDetail { get => fixDetail; set => fixDetail = value; }

        public IssueFix(int fixBy, int issueID, string fixTitle, string fixDetail)
        {
            FixBy = fixBy;
            IssueID = issueID;
            FixTitle = fixTitle;
            FixDetail = fixDetail;
        }
    }
}
