using Kent.Business.Core.Models.HeaderTemplates;
using Kent.Entities.Model;
using Kent.Entities.Repositories;
using Kent.Libary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Services
{
    public class HeaderTemplateServices : IHeaderTemplateServices
    {
        private IHeaderTemplateRepository _headerRepository;

        public HeaderTemplateServices(IHeaderTemplateRepository headerRepository)
        {
            _headerRepository = headerRepository;
        }

        public List<HeaderTemplateModel> GetHeaderTemplates(string keyword)
        {
            List<HeaderTemplate> data = _headerRepository.GetHeaderTemplates(keyword);
            if (data != null)
            {
                return data.Select(d => Mapping(d)).ToList();
            }
            return new List<HeaderTemplateModel>();
        }

        public bool SaveHeaderTemplate(HeaderTemplateManageModel model)
        {
            if (model.ID > 0)
            {
                var dataUpdate = new HeaderTemplate()
                {
                    ID = model.ID.Value,
                    Content = model.Content,
                    IsDefaultTemplate = model.IsDefaultTemplate
                };

                return _headerRepository.UpdateHeaderTemplate(dataUpdate);
            }
            else
            {
                HeaderTemplate data = new HeaderTemplate()
                {
                    Name = model.Name,
                    Content = model.Content,
                    IsDefaultTemplate = model.IsDefaultTemplate,
                    Created = DateTime.Now,
                    CreatedBy = "Admin",
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = "Admin",
                    RecordActive = true,
                    RecordDeleted = false,
                    RecordOrder = 1
                };
                return _headerRepository.AddHeaderTemplate(data);
            }
        }

        public HeaderTemplateManageModel GetHeaderTemplateById(int id)
        {
            HeaderTemplate data = _headerRepository.GetHeaderTemplateById(id);
            if (data != null)
            {
                return MappingManageModel(data);
            }
            return new HeaderTemplateManageModel();
        }

        public HeaderTemplateModel GetHeaderTemplateDefault()
        {
            HeaderTemplate data = _headerRepository.GetHeaderTemplateDefault();
            if (data != null)
            {
                return Mapping(data);
            }
            return new HeaderTemplateModel();
        }

        private HeaderTemplateModel Mapping(HeaderTemplate data)
        {
            return new HeaderTemplateModel()
            {
                ID = data.ID,
                Name = data.Name,
                Content = data.Content,
                IsDefaultTemplate = data.IsDefaultTemplate,
                Created = data.Created,
                CreatedBy = data.CreatedBy,
                LastUpdate = data.LastUpdate,
                LastUpdateBy = data.LastUpdateBy
            };
        }

        private HeaderTemplateManageModel MappingManageModel(HeaderTemplate data)
        {
            return new HeaderTemplateManageModel()
            {
                ID = data.ID,
                Name = data.Name,
                Content = data.Content,
                IsDefaultTemplate = data.IsDefaultTemplate
            };
        }
    }
}
