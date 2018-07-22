using Kent.Business.BackgroundTask;
using Kent.Business.Core.Models.Forms;
using Kent.Business.Core.Models.Forms.FormData;
using Kent.Entities;
using Kent.Entities.Model;
using Kent.Entities.Repositories;
using Kent.Libary.Configurations;
using Kent.Libary.Enums;
using Kent.Libary.Logger;
using Kent.Libary.Utilities;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kent.Business.Services
{
    public class FormServices : IFormServices
    {
        private readonly IFormRepository _formRepository;
        private readonly IEmployeesServices _employeesService;
        private readonly IEmailQueueServices _emailQueueService;

        public FormServices(IFormRepository formRepository, IEmployeesServices employeesService, IEmailQueueServices emailQueueService)
        {
            _formRepository = formRepository;
            _employeesService = employeesService;
            _emailQueueService = emailQueueService;
        }
        public List<FormModel> GetListForms(FormsEnums.FormType type, string keyword)
        {
            List<FormModel> list = new List<FormModel>();
            List<Form> listData = new List<Form>();
            switch (type)
            {
                case FormsEnums.FormType.Admission:
                    listData = _formRepository.GetList((int)type, keyword);
                    list = Mapping(listData, type);
                    break;
                case FormsEnums.FormType.Advisory:
                    listData = _formRepository.GetList((int)type, keyword);
                    list = Mapping(listData, type);
                    break;
                case FormsEnums.FormType.Visit:
                    listData = _formRepository.GetList((int)type, keyword);
                    list = Mapping(listData, type);
                    break;
                default:
                    listData = _formRepository.GetList((int)type, keyword);
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
                CreateEmailQueue(form, model.FormTypeID);
                return true;
            }

            return false;
        }

        private void CreateEmailQueue(Form formData, FormsEnums.FormType type)
        {
            List<Employees> listEmployees = _employeesService.GetList();

            try
            {
                List<EmailQueue> emails = new List<EmailQueue>();
                foreach (var employees in listEmployees)
                {
                    EmailQueue newEmail = GetEmail(type, formData.Data, employees.Email, employees.Name);

                    int addToQueue = _emailQueueService.AddNewEmail(newEmail);
                    if (addToQueue > 0)
                    {
                        emails.Add(newEmail);
                    }
                }

                TaskSentMail(emails);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex);
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
        private static readonly string EmailFrom = ConfigurationManager.AppSettings[KentConfiguration.EmailFromSetting].ToString();
        private static readonly string EmailFromPassword = ConfigurationManager.AppSettings[KentConfiguration.EmailFromPasswordSetting].ToString();

        private void TaskSentMail(List<EmailQueue> emailQueueIds)
        {
            try
            {
                foreach (var emailQueue in emailQueueIds)
                {

                    //EmailQueue emailQueue = _emailQueueService.GetEmailByID(queueID);
                    Thread email = new Thread(delegate ()
                    {
                        SendEmail(emailQueue.To, EmailFrom, EmailFromPassword, emailQueue.Subject, emailQueue.Body);
                    });

                    email.IsBackground = true;
                    email.Start();
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex);
            }
        }

        private void SendEmail(string to, string from, string password, string subject, string body)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Host = ConfigurationManager.AppSettings[KentConfiguration.STMPHostSetting].ToString();
                client.Port = Convert.ToInt32(ConfigurationManager.AppSettings[KentConfiguration.STMPPort]);

                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(from, password);

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(from);
                mailMessage.To.Add(to);
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = subject;
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex);

            }

        }

        private EmailQueue GetEmail(FormsEnums.FormType type, string jsonData, string employeeEmail, string employeeName)
        {
            var email = new EmailQueue()
            {
                From = "",
                FromName = "Hệ Thống",
                To = employeeEmail,
                ToName = employeeName,
                Subject = "",
                CreatedBy = "system",
                Created = DateTime.Now,
                RecordActive = true,
            };
            var data = new object();
            string resourceName = string.Empty;
            string templateFilePath = string.Empty;

            switch (type)
            {
                case FormsEnums.FormType.Admission:
                    email.Subject = "Đăng kí nhập học";
                    data = SerializeUtilities.Deserialize<AdmissionData>(jsonData);
                    resourceName = "Kent.Business.Core.EmailTemplate.Admission.cshtml";
                    break;

                case FormsEnums.FormType.Advisory:
                    email.Subject = "Đăng kí tư vấn";
                    data = SerializeUtilities.Deserialize<AdvisoryData>(jsonData);
                    resourceName = "Kent.Business.Core.EmailTemplate.Advisory.cshtml";
                    break;

                case FormsEnums.FormType.Alumni:
                    email.Subject = "Thông tin cựu sinh viên";
                    data = SerializeUtilities.Deserialize<AlumniData>(jsonData);
                    resourceName = "Kent.Business.Core.EmailTemplate.Alumni.cshtml";
                    break;

                case FormsEnums.FormType.Contact:
                    email.Subject = "Liên hệ";
                    data = SerializeUtilities.Deserialize<ContactData>(jsonData);
                    resourceName = "Kent.Business.Core.EmailTemplate.Contact.cshtml";
                    break;

                case FormsEnums.FormType.Visit:
                    email.Subject = "Đăng kí tham quan";
                    data = SerializeUtilities.Deserialize<VisitSchoolData>(jsonData);
                    resourceName = "Kent.Business.Core.EmailTemplate.VisitSchool.cshtml";
                    break;
            }
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string templateStr = reader.ReadToEnd();
                email.Body = Engine.Razor.RunCompile(templateStr, "templateKey", null, data);
            }

            return email; ;
        }
    }
}
