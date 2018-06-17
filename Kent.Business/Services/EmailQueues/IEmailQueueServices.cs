using Kent.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Services
{
    public interface IEmailQueueServices
    {
        int AddNewEmail(EmailQueue newEmail);
        EmailQueue GetEmailByID(int queueID);
    }
}
