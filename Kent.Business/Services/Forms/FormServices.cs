using Kent.Business.Core.Models.Forms;
using Kent.Entities;
using Kent.Entities.Model;
using Kent.Entities.Repositories;
using Kent.Libary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Services
{
    public class FormServices : IFormServices
    {
        private readonly IFormRepository _formRepository;

        private static KentEntities kentEntities { get; set; }
        public FormServices(IFormRepository formRepository)
        {
            kentEntities = new KentEntities();
            _formRepository = formRepository;

        }

        public bool SaveForm(FormModel model)
        {
            var form = new Form()
            {
                Data = model.Data,
                FormTypeID = (int)model.FormTypeID,
                Created = DateTime.Now,
                CreatedBy = "test",
                RecordActive = true,
                RecordDeleted = false,
                RecordOrder = 0
            };
            kentEntities.Forms.Add(form);
            kentEntities.SaveChanges();
            return true;
        }

        public List<FormModel> GetListForms(FormsEnums.FormType type)
        {
            List<FormModel> list = new List<FormModel>();
            List<Form> listData = new List<Form>();
            switch (type)
            {
                case FormsEnums.FormType.Admission:
                    listData = _formRepository.GetList((int)type);
                    list = Mapping(listData, type);
                    break;
                case FormsEnums.FormType.Advisory:
                    listData = _formRepository.GetList((int)type);
                    list = Mapping(listData, type);
                    break;
                case FormsEnums.FormType.Visit:
                    listData = _formRepository.GetList((int)type);
                    list = Mapping(listData, type);
                    break;
            }
            return list;
        }

        private List<FormModel> Mapping(List<Form> lst, FormsEnums.FormType type)
        {
            return lst.Select(d => new FormModel()
            {
                Data = d.Data,
                FormTypeID = type,
                DateSubmit = d.Created
            }).ToList();
        }
    }
}
