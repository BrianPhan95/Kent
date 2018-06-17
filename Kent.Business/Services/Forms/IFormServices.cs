using Kent.Business.Core.Models.Forms;
using Kent.Libary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Services
{
    public interface IFormServices
    {
        bool SaveForm(FormModel model);

        List<FormModel> GetListForms(FormsEnums.FormType type);
    }
}
