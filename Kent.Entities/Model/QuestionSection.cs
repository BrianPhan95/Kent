using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Model
{
    public class QuestionSection : BaseModel
    {
        public int SectionType { get; set; }
        public string Description { get; set; }
        public string ListAnwser { get; set; }

        public bool HasTemplate { get; set; }
        public bool ContainListAnswers { get; set; }

        public int QuestionKitID { get; set; }
        [ForeignKey("QuestionKitID")]
        public virtual QuestionKit QuestionKit { get; set; }
        public int QuestionTemplateID { get; set; }
        [ForeignKey("QuestionTemplateID")]
        public virtual QuestionTemplate QuestionTemplate { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
