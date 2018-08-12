using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Core.Models.Pages
{
    public class PageViewModel
    {
        public string Title { get; set; }
        public string HeaderContent { get; set; }
        public string FooterContent { get; set; }
        public string PageContent { get; set; }
    }
}
