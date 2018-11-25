using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Model
{
    public class QuestionTemplate : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int Type { get; set; }
        public bool HasBoxListingAnswer { get; set; }

    }
}
