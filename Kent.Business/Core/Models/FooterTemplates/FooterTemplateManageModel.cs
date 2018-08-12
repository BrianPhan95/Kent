using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Kent.Business.Core.Models.FooterTemplates
{
    public class FooterTemplateManageModel
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        public bool IsDefaultTemplate { get; set; }
    }
}
