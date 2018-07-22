using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kent.Business.Core.Models.Forms.FormData
{
    public class AdvisoryData
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<string> Specialize { get; set; }
        public string Message { get; set; }
    }
}