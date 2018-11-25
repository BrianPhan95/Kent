using Kent.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Core.Models.Menus
{
    public class MenuManageModel
    {
        public MenuManageModel()
        {

        }
        public MenuManageModel(Menu menuModel) : this()
        {
            Id = menuModel.ID;
            Name = menuModel.Name;
            Controller = menuModel.Controller;
            Action = menuModel.Action;
            Area = menuModel.Area;
            Visible = menuModel.Visible;
            ParentId = menuModel.ParentId;
        }

        public int? Id { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Area { get; set; }
        public bool Visible { get; set; }

        public int? ParentId { get; set; }
    }
}
