using Kent.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Repositories
{
    public interface IPageRepository
    {
        List<Page> GetPages(string keyword);
        Page GetPageById(int id);
        Page GetPageByFriendlyUrl(string url);
        bool AddPage(Page page);
        bool UpdatePage(Page page);
    }
}
