using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kent.Entities.Model;

namespace Kent.Entities.Repositories
{
    public interface IHeaderTemplateRepository
    {
        bool AddHeaderTemplate(HeaderTemplate headerTemplate);
        List<HeaderTemplate> GetHeaderTemplates(string keyword);
        HeaderTemplate GetHeaderTemplateById(int id);
        HeaderTemplate GetHeaderTemplateDefault();
        bool UpdateHeaderTemplate(HeaderTemplate headerTemplate);
    }
}
