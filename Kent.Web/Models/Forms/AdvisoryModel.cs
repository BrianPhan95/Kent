using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kent.Web.Models.Forms
{
    public class AdvisoryModel
    {

        #region Properties
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public List<int> Specialize { get; set; }
        public string Message { get; set; }
        #endregion
    }
}
