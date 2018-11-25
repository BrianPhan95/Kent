using Kent.Business.Core.Models.HeaderTemplates;
using Kent.Libary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Kent.Business.Core.Models.Pages
{
    public class PageManageModel
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }
        public string FriendlyUrl { get; set; }



        public string TitleEnglish { get; set; }

        public string ContentEnglish { get; set; }

        public string FriendlyUrlEnglish { get; set; }

       

        public string Keywords { get; set; }

        public string HeaderTemplate { get; set; }

        public string FooterTemplate { get; set; }

        public PageStatus Status { get; set; }

        public bool IsHomePage { get; set; }

        //public bool ShowOnSitemap { get; set; }

        public bool IncludeInSiteNavigation { get; set; }

        //public bool DisableMenuCascade { get; set; }

        public DateTime? StartPublishingDate { get; set; }

        public DateTime? EndPublishingDate { get; set; }


        public IEnumerable<SelectListItem> HeaderTemplateList { get; set; }
        public IEnumerable<SelectListItem> FooterTemplateList { get; set; }
    }
}
