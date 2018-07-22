using Kent.Business.Core.Models.Users;
using Kent.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRespository _userRespository;

        public UserServices(IUserRespository userRespository)
        {
            _userRespository = userRespository;
        }

        public UserModel GetUserDetails(UserLogin user)
        {
            var userDetails= _userRespository.GetUserDetails(user.Email, user.Password);
            if (userDetails != null)
            {
                _userRespository.UpdateLastLoginTime(userDetails.ID);
                return new UserModel(userDetails);
            }
            return null;
        }
    }
}
