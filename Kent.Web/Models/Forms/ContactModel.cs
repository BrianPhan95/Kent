using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kent.Web.Models.Forms
{
    public class ContactModel
    {
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string Content { get; set; }
    }
}