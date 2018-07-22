using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kent.Business.Core.Models.Forms.FormData
{
    public class VisitSchoolData
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateVisit { get; set; }
    }
}