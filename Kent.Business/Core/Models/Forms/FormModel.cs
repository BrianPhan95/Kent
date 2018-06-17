using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kent.Libary.Enums;

namespace Kent.Business.Core.Models.Forms
{
    public class FormModel
    {
        public FormsEnums.FormType FormTypeID { get; set; }

        public string Data { get; set; }

        public DateTime? DateSubmit { get; set; }
    }
}
