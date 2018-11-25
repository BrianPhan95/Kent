using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Model
{
    public class Question : BaseModel
    {
        public int QuestionNumber { get; set; }
        public string QuestionString { get; set; }
        public string Answer { get; set; }
        public string SelectedAnswers { get; set; }

        public int SectionID { get; set; }
        [ForeignKey("SectionID")]
        public virtual QuestionSection QuestionSection { get; set; }
    }
}
