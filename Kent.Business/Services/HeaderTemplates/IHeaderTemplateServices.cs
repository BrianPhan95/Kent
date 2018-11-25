using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kent.Business.Core.Models.HeaderTemplates;

namespace Kent.Business.Services
{
    public interface IHeaderTemplateServices
    {
        List<HeaderTemplateModel> GetHeaderTemplates(string keyword);
        bool SaveHeaderTemplate(HeaderTemplateManageModel model);
        HeaderTemplateManageModel GetHeaderTemplateById(int id);

        HeaderTemplateModel GetHeaderTemplateDefault();
        bool DeleteHeaderTemplate(int id);
    }
}
