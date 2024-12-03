using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortalWebApplication.Models.Response
{
    public class EmployerRegResponse
    {
        public string EmailStatus { get; set; }
     public int EmployeeId {  get; set; }
        public string Password { get; set; }
        public string CompanyEmail { get; set; }
        public string message {  get; set; }
    }
}