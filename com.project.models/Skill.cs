using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMS.com.project.models
{
    class Skill
    {
        int skillID;
        string skill;

        public int SkillID { get => skillID; set => skillID = value; }
        public string SkillName { get => skill; set => skill = value; }
    }
}
