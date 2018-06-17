using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Libary.Configurations
{
    public class KentConfiguration
    {
        #region Sessions

        public const string CurrentUser = ":::CurrentUser:::";
        public const string CurrentCuture = ":::CurrentCuture:::";
        public const string SetupSessionKey = ":::SetupSessionKey:::";

        public const string CurrentControllerContext = ":::CurrentControllerContext:::";
        public const string CurrentControllerAction = ":::CurrentControllerAction:::";

        #endregion
    }
}
