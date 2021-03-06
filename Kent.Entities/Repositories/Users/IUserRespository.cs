﻿using Kent.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Repositories
{
    public interface IUserRespository
    {
        bool UpdateLastLoginTime(int userId);
        User GetUserDetails(string email, string password);
    }
}
