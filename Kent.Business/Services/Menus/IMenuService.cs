using Kent.Business.Core.Models.Menus;
using Kent.Business.Services.Base;
using Kent.Entities.Model;
using Kent.Libary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Services.Menus
{
    public interface IMenuService : IBaseService<Menu>
    {
        List<MenuModel> ListingMenu(RequestModel request);
        ResponseModel SaveMenu(MenuManageModel request);
        MenuManageModel GetMenuManageModel(int? id = null);
        ResponseModel DeleteMenu(int id);
    }
}
