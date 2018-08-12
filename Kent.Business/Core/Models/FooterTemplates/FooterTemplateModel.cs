using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Core.Models.FooterTemplates
{
    public class FooterTemplateModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Content { get; set; }

        public bool IsDefaultTemplate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
