using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMS.com.project.models
{
    public class Review
    {
        int reviewID, reviewby, reviewfor, reviewproject;
        string review;

        public int ReviewID { get => reviewID; set => reviewID = value; }
        public int Reviewby { get => reviewby; set => reviewby = value; }
        public int Reviewfor { get => reviewfor; set => reviewfor = value; }
        public int Reviewproject { get => reviewproject; set => reviewproject = value; }
        public string ReviewDesc { get => review; set => review = value; }

        public Review(int reviewproject, int reviewfor,  string review, int reviewby)
        {
            this.reviewby = reviewby;
            this.reviewfor = reviewfor;
            this.reviewproject = reviewproject;
            this.review = review;
        }
    }
}
