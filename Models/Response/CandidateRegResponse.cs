using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortalWebApplication.Models.Response
{
    public class CandidateRegResponse
    { public int CandidateId {  get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string EmailStatus { get; set; }
        public string Message { get; set; }
    }
}