using Kent.Business.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Services
{
    public interface IUserServices
    {
        UserModel GetUserDetails(UserLogin user);
    }
}
