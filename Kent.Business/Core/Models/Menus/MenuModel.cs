using Kent.Business.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Core.Models.Menus
{
    public class MenuModel : BaseHierachyModel<MenuModel>
    {
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Area { get; set; }
        public bool Visible { get; set; }
    }
}
