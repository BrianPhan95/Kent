using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Core.Models.TestKits.Manage
{
    public class ImportKitFormatModel
    {
        public ImportKitFormatModel()
        {
            SessionName = "1-5";
            QuestionNumber = 1;
            QuestionString = "How old are you?";
            AnswerList = "A. 15 /n B. 16 /n C. 17";
            Answer = "A";
            Type = 1;
        }

        public string SessionName { get; set; }
        public int QuestionNumber { get; set; }
        public string QuestionString { get; set; }
        public string AnswerList { get; set; }
        public string Answer { get; set; }
        public int Type { get; set; }
    }
}
