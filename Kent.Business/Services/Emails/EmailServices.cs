using Kent.Entities.Model;
using Kent.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Services
{
    public class EmailServices : IEmailServices
    {
        public readonly IEmailRepository _emailRepository;
        public EmailServices(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        } 
    }
}
