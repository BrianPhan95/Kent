using Kent.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Core.Models.Users
{
    public class UserModel
    {
        public UserModel(User user)
        {
            ID = user.ID;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Roles = "admin";
        }

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
    }
}
