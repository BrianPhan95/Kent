using Kent.Entities.Model;
using Kent.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Services
{
    public class EmailQueueServices : IEmailQueueServices
    {
        public readonly IEmailQueueRepository _emailQueueRepository;
        public EmailQueueServices(IEmailQueueRepository emailQueueRepository)
        {
            _emailQueueRepository = emailQueueRepository;
        }

        public int AddNewEmail(EmailQueue newEmail)
        {
            return  _emailQueueRepository.CreateEmailAsync(newEmail);
        }

        public EmailQueue GetEmailByID(int queueID)
        {
            return _emailQueueRepository.GetEmailQueueByID(queueID);
        }
    }
}
