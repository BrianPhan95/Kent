using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Libary.Enums
{
    public class PageEnums
    {
        public enum PageLanguages
        {
            Vietnamese = 1,
            English = 2
        }

        public enum PageStatus
        {
            Online = 1,
            Draft = 2,
            Offline = 3
        }

        public enum PagePosition
        {
            Before = 1,
            After = 2
        }

        public enum PageResponseCode
        {
            Ok = 0,
            RequiredSSL,
            FileTemplateRedirect,
            PageDraft,
            PageOffline,
            Redirect301
        }

        public enum SEOScore
        {
            Good,
            Medium,
            Bad,
            VeryBad
        }
    }
}
