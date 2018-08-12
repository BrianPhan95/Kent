using Kent.Business.Core.Models.FooterTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Services
{
    public interface IFooterTemplateServices
    {
        List<FooterTemplateModel> GetFooterTemplates(string keyword);
        bool SaveFooterTemplate(FooterTemplateManageModel model);
        FooterTemplateManageModel GetFooterTemplateById(int id);

        FooterTemplateModel GetFooterTemplateDefault();
    }
}
