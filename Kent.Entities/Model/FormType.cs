using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Model
{
    public class FormType : BaseModel
    {


        public virtual ICollection<Form> Forms { get; set; }
    }
}
