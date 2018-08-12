using Kent.Business.Core.Models.HeaderTemplates;
using Kent.Libary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Core.Models.Pages
{
    public class PageModel
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public string TitleEnglish { get; set; }

        public string ContentEnglish { get; set; }

        public string FriendlyUrlEnglish { get; set; }

        public string ContentWorking { get; set; }

        public string FriendlyUrl { get; set; }

        public string Keywords { get; set; }

        public int HeaderTemplateId { get; set; }

        public virtual HeaderTemplateModel HeaderTemplate { get; set; }

        public int FooterTemplateId { get; set; }

        public virtual FooterTemplates.FooterTemplateModel FooterTemplate { get; set; }

        public PageEnums.PageStatus Status { get; set; }

        public bool IsHomePage { get; set; }

        //public bool ShowOnSitemap { get; set; }

        public bool IncludeInSiteNavigation { get; set; }

        //public bool DisableMenuCascade { get; set; }

        public DateTime? StartPublishingDate { get; set; }

        public DateTime? EndPublishingDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
