using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kent.Web.Areas.Admin.Models.FormData
{
    public class AdmissionData
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateSubmit { get; set; }
    }
}