using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kent.Business.Core.Models.Pages;
using Kent.Libary.Enums;

namespace Kent.Business.Services
{
    public interface IPageServices
    {
        List<PageModel> GetPages(string keyword);
        bool SavePage(PageManageModel model);
        PageManageModel GetPageById(int id);
        PageViewModel GetPageByFiendlyUrl(string url, PageLanguages language);
        bool DeletePage(int id);
    }
}
