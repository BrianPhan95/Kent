using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Model
{
    public class FooterTemplate : BaseModel
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public string ContentEnglish { get; set; }

        public bool IsDefaultTemplate { get; set; }
    }
}
