using Kent.Business.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Core.Models.TestKits
{
    public class QuestionModel : BaseModel
    {
        public string Questions { get; set; }
        public string ListOfAnswers { get; set; }
        public string CorrectAnswers { get; set; }

        public int SectionID { get; set; }
    }
}
