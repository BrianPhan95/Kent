using Kent.Libary.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Model
{
    public class Page : BaseHierachyModel<Page>
    {
        [StringLength(255)]
        public string Title { get; set; }
        [StringLength(255)]
        public string TitleEnglish { get; set; }

        public string Content { get; set; }

        public string ContentEnglish { get; set; }

        [StringLength(255)]
        public string FriendlyUrl { get; set; }

        [StringLength(255)]
        public string FriendlyUrlEnglish { get; set; }

        [StringLength(255)]
        public string Keywords { get; set; }

        public int? HeaderTemplateId { get; set; }

        [ForeignKey("HeaderTemplateId")]
        public virtual HeaderTemplate HeaderTemplate { get; set; }

        public int? FooterTemplateId { get; set; }

        [ForeignKey("FooterTemplateId")]
        public virtual FooterTemplate FooterTemplate { get; set; }

        //public int? FileTemplateId { get; set; }

        //[ForeignKey("FileTemplateId")]
        //public virtual FileTemplate FileTemplate { get; set; }

        //public int? BodyTemplateId { get; set; }

        //[ForeignKey("BodyTemplateId")]
        //public virtual BodyTemplate BodyTemplate { get; set; }
        //public string Abstract { get; set; }

        //[StringLength(255)]
        //public string SeoTitle { get; set; }

        //public string Description { get; set; }

        //public string AbstractWorking { get; set; }

        public PageEnums.PageStatus Status { get; set; }

        public bool IsHomePage { get; set; }

        //public bool ShowOnSitemap { get; set; }

        public bool IncludeInSiteNavigation { get; set; }

        //public bool DisableMenuCascade { get; set; }

        public DateTime? StartPublishingDate { get; set; }

        public DateTime? EndPublishingDate { get; set; }

    }
}
