using Kent.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Repositories
{
    public interface IFooterTemplateRepository
    {
        bool AddFooterTemplate(FooterTemplate headerTemplate);
        List<FooterTemplate> GetFooterTemplates(string keyword);
        FooterTemplate GetFooterTemplateById(int id);
        bool UpdateFooterTemplate(FooterTemplate headerTemplate);

        FooterTemplate GetFooterTemplateDefault();
        bool DeleteFooterTemplate(int id);
    }
}
