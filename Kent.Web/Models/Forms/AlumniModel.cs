using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kent.Web.Models.Forms
{
    public class AlumniModel
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime GraduatingDate { get; set; }
        public string Specialized { get; set; }
        public string CurrentCompany { get; set; }
        public string CompanyField { get; set; }
        public string Staff { get; set; }
        public string Position { get; set; }
        public string Salary { get; set; }
        public string SalaryStarting { get; set; }
        public string StudyAbroad { get; set; }
    }
}

