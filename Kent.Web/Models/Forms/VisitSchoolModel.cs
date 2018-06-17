using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kent.Web.Models.Forms
{
    public class VisitSchoolModel
    {
        public string FullName { get;set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateVisit { get; set; }
        //public DaTime Time
        public string Message { get; set; }
    }
}