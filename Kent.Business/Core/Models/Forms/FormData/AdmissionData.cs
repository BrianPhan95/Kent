using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kent.Business.Core.Models.Forms.FormData
{
    public class AdmissionData
    {
        #region Basic information
        public string FullName { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceBirth { get; set; }
        public string Literacy { get; set; }
        public string School { get; set; }
        public string Province { get; set; }
        public string ImagePath { get; set; }
        public string IdNumber { get; set; }
        public DateTime DateRange { get; set; }
        public string PlaceRange { get; set; }
        public string MediumScrore { get; set; }
        #endregion

        #region Majors
        public List<string> InternationalBachelorProgram { get; set; }
        public List<string> UniversityFoundationProgram { get; set; }
        #endregion

        #region Related Informations
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string GuardianName { get; set; }
        public string Household { get; set; }
        public string Email { get; set; }
        public string GuardianPhoneNumber { get; set; }
        #endregion

        #region You know our company by
        public List<string> list { get; set; }
        public string another { get; set; }

        #endregion
        public bool Promise { get; set; }
    }
}