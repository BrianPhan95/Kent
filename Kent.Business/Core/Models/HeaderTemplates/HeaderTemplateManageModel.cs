using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Core.Models.HeaderTemplates
{
    public class HeaderTemplateManageModel
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public string Content { get; set; }

        public bool IsDefaultTemplate { get; set; }
    }
}
