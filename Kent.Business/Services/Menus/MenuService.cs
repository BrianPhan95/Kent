using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kent.Business.Core.Models.Menus;
using Kent.Business.Services.Base;
using Kent.Entities.Repositories.Menus;
using Kent.Entities.Model;
using Kent.Business.Core.Models.Base;
using Kent.Entities.Repositories;
using Kent.Libary.Models;

namespace Kent.Business.Services.Menus
{
    public class MenuService : IMenuService
    {
        public readonly IMenuRepository _menuRepository;
        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        #region base
        public ResponseModel Delete(int id)
        {
            return _menuRepository.Delete(id);
        }

        public Menu GetById(int id)
        {
            return _menuRepository.GetById(id);
        }

        public List<Menu> GetList(RequestModel request)
        {
            return _menuRepository.GetList(request);
        }

        public ResponseModel Insert(Menu model)
        {
            return _menuRepository.Insert(model);
        }

        public ResponseModel Update(Menu model)
        {
            return _menuRepository.Update(model);
        }

        private MenuModel Map(Menu model)
        {
            return new MenuModel()
            {
                ID = model.ID,
                RecordActive = model.RecordActive,
                RecordDeleted = model.RecordDeleted,
                RecordOrder = model.RecordOrder,
                Created = model.Created,
                CreatedBy = model.CreatedBy,
                LastUpdateBy = model.LastUpdateBy,
                LastUpdate = model.LastUpdate,

                Name = model.Name,
                Controller = model.Controller,
                Action = model.Action,
                Area = model.Area,
                Visible = model.Visible,
                Hierarchy = model.Hierarchy,
                ParentId = model.ParentId
            };
        }

        private Menu Map(MenuModel model)
        {
            return new Menu()
            {
                ID = model.ID,
                RecordActive = model.RecordActive,
                RecordDeleted = model.RecordDeleted,
                RecordOrder = model.RecordOrder,
                Created = model.Created,
                CreatedBy = model.CreatedBy,
                LastUpdateBy = model.LastUpdateBy,
                LastUpdate = model.LastUpdate,

                Name = model.Name,
                Controller = model.Controller,
                Action = model.Action,
                Area = model.Area,
                Visible = model.Visible,
                Hierarchy = model.Hierarchy,
                ParentId = model.ParentId
            };
        }
        #endregion

        public List<MenuModel> ListingMenu(RequestModel request)
        {
            return GetList(request).Select(d => Map(d)).ToList();
        }

        public ResponseModel SaveMenu(MenuManageModel request)
        {
            ResponseModel response = new ResponseModel();
            if (request.Id.HasValue && request.Id > 0)
            {
                var model = GetById(request.Id.Value);
                if (model == null)
                    return response;

                model.Action = request.Action;
                model.Area = request.Area;
                model.Controller = request.Controller;
                model.ParentId = request.ParentId;

                response = Update(model);
            }
            else
            {
                Menu model = new Menu()
                {
                    Action = request.Action,
                    Area = request.Area,
                    Controller = request.Controller,
                    Name = request.Name,
                    //RecordActive = true,
                    //Created = DateTime.Now,
                    //CreatedBy = "",
                };
                response = Insert(model);
            }
            return response;
        }

        public MenuManageModel GetMenuManageModel(int? id = null)
        {
            var menu = GetById(id.HasValue ? id.Value : 0);
            return menu != null ? new MenuManageModel(menu) : new MenuManageModel();
        }

        public ResponseModel DeleteMenu(int id)
        {
            return Delete(id);
        }
    }
}
