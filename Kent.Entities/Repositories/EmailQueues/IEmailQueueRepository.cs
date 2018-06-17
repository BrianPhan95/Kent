using Kent.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Repositories
{
    public interface IEmailQueueRepository
    {
        bool SaveEmailToQueue(EmailQueue email);

        EmailQueue GetEmailQueueByID(int id);
        int CreateEmailAsync(EmailQueue newEmail);
    }
}
