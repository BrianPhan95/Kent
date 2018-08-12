using Kent.Business.Core.Models.FooterTemplates;
using Kent.Entities.Model;
using Kent.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Services
{
    public class FooterTemplateServices : IFooterTemplateServices
    {
        private IFooterTemplateRepository _footerRepository;

        public FooterTemplateServices(IFooterTemplateRepository headerRepository)
        {
            _footerRepository = headerRepository;
        }

        public List<FooterTemplateModel> GetFooterTemplates(string keyword)
        {
            List<FooterTemplate> data = _footerRepository.GetFooterTemplates(keyword);
            if (data != null)
            {
                return data.Select(d => Mapping(d)).ToList();
            }
            return new List<FooterTemplateModel>();
        }

        public bool SaveFooterTemplate(FooterTemplateManageModel model)
        {
            if (model.ID > 0)
            {
                var dataUpdate = new FooterTemplate()
                {
                    ID = model.ID.Value,
                    Content = model.Content,
                    IsDefaultTemplate = model.IsDefaultTemplate
                };

                return _footerRepository.UpdateFooterTemplate(dataUpdate);
            }
            else
            {
                FooterTemplate data = new FooterTemplate()
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
                return _footerRepository.AddFooterTemplate(data);
            }
        }

        public FooterTemplateManageModel GetFooterTemplateById(int id)
        {
            FooterTemplate data = _footerRepository.GetFooterTemplateById(id);
            if (data != null)
            {
                return MappingManageModel(data);
            }
            return new FooterTemplateManageModel();
        }

        public FooterTemplateModel GetFooterTemplateDefault()
        {
            FooterTemplate data = _footerRepository.GetFooterTemplateDefault();
            if (data != null)
            {
                return Mapping(data);
            }
            return new FooterTemplateModel();
        }

        private FooterTemplateModel Mapping(FooterTemplate data)
        {
            return new FooterTemplateModel()
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

        private FooterTemplateManageModel MappingManageModel(FooterTemplate data)
        {
            return new FooterTemplateManageModel()
            {
                ID = data.ID,
                Name = data.Name,
                Content = data.Content,
                IsDefaultTemplate = data.IsDefaultTemplate
            };
        }
    }
}
