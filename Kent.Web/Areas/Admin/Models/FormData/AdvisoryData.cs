using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kent.Web.Areas.Admin.Models.FormData
{
    public class AdvisoryData
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        //public List<int> Specialize { get; set; }
        public DateTime DateCreated { get; set; }
    }
}