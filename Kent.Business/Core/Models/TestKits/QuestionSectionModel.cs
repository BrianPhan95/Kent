using Kent.Business.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Core.Models.TestKits
{
    public class QuestionSectionModel : BaseModel
    {
        public int SectionType { get; set; }
        public string Description { get; set; }

        public int QuestionKitID { get; set; }
    }
}
