using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Model
{
    public class QuestionKit : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<QuestionSection> QuestionSections { get; set; }
    }
}
