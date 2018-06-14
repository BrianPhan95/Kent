using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Model
{
    public class EmailType : BaseModel
    {
        public string Name { get; set; }
        public string Template { get; set; }

    }
}
