using Kent.Business.BackgroundTask;
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
        private readonly ISalerServices _salerService;
        private readonly IEmailQueueServices _emailQueueService;

        public FormServices(IFormRepository formRepository, ISalerServices salerService, IEmailQueueServices emailQueueService)
        {
            _formRepository = formRepository;
            _salerService = salerService;
            _emailQueueService = emailQueueService;
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
            var formID = _formRepository.SaveFormData(form);
            if (formID > 0)
            {
                CreateEmailQueue(form);
                return true;
            }

            return false;
        }

        private void CreateEmailQueue(Form formData)
        {
            List<Saler> listSaler = _salerService.GetList();

            try
            {
                List<EmailQueue> emails = new List<EmailQueue>();
                foreach (var saler in listSaler)
                {
                    EmailQueue newEmail = new EmailQueue()
                    {
                        From = "",
                        FromName = "Service",
                        To = saler.Email,
                        ToName = saler.SalerName,
                        Subject = "",
                        Body = formData.Data,
                        CreatedBy = "system",
                        Created = DateTime.Now,
                        RecordActive = true,
                    };

                    int emailQueueID = _emailQueueService.AddNewEmail(newEmail);
                    if (emailQueueID > 0)
                    {
                        emails.Add(newEmail);
                    }
                }

                EmailBackgroundTask.Run(emails);
            }
            catch (Exception ex)
            {

            }
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
