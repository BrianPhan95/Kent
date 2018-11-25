using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Libary.Configurations
{
    public class KentConfiguration
    {
        public const string ConnectionString = "KentDatabase";
        #region Sessions

        public const string CurrentUser = ":::CurrentUser:::";
        public const string CurrentCuture = ":::CurrentCuture:::";
        public const string SetupSessionKey = ":::SetupSessionKey:::";

        public const string CurrentControllerContext = ":::CurrentControllerContext:::";
        public const string CurrentControllerAction = ":::CurrentControllerAction:::";

        #endregion

        #region Mail setting
        public const string EmailFromSetting = "EmailFrom";
        public const string EmailFromPasswordSetting = "EmailFromPassword";
        public const string STMPHostSetting = "STMPHostSetting";
        public const string STMPPort = "STMPPort";
        public const string STMPUseAuthentication = "STMPUseAuthentication";
        #endregion
        public const string CurrentTimezone = ":::CurrentTimezone:::";

        #region Media

        public const string MediaPath = "/Media";

        public const string ResizedFolder = "resized";
        #endregion

        #region Hierarchy

        //Hierarchy
        public const string HierarchyLevelPrefix = "---";
        public const string ParentIdPropertyName = "ParentId";
        public const string HierarchyPropertyName = "Hierarchy";
        public const char IdSeparator = '.';
        public const string HierarchyNodeFormat = "D6";

        #endregion

        #region Common

        public const string DefaultSystemAccount = "system";

        public const string DefaultMigrationAccount = "migrator";

        public const string UniqueLinkSeparator = "::::::";

        public const double Zero = 0.000000000000001;

        public const string NotificationEmail = "notifications@interactivepartners.com.au";

        public const string WebEd9Email = "weded9@interactivepartners.com.au";

        public const string SemicolonSeparator = "; ";
        public const string ColonSeparator = ", ";
        public const string Semicolon = ";";
        public const string Colon = ",";
        public const string Space = " ";
        public const string BreakLine = "<br />";

        public const int DefaultEmailSendTimeout = 30000;

        public const string LanguageResourceNamespaceFormat = "WebEd.Business.Core.Resources.Languages.Resources_{0}";
        public const string DefaultLanguageResourceNamespace = "WebEd.Business.Core.Resources.Languages.Resources";

        public const string WebEdProject = "WebEd";
        public const string WebEdBusinessProject = "WebEd.Business";

        public const string UniqueDateTimeFormat = "-yyyyMMddhhmmss";

        #endregion
    }
}
