using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kent.Web.Areas.Admin.Models.Users
{
    public class UserSessionModel
    {

        public UserSessionModel()
        {

        }

        #region Properties
        public int? UserId { get; set; }
        public string UserName { get; set; }

        public dynamic Session { get; set; }

        public DateTime LastLogin { get; set; }
        #endregion

        #region Method

        //public static void SetUserSession(User user, DateTime? lastGettingUpdates = null, dynamic session = null)
        //{
        //    Session = session;
        //    LastLogin = LastLogin
        //}
        #endregion
    }
}