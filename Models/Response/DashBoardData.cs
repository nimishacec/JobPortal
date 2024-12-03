using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortalWebApplication.Models.Response
{
    public class DashBoardData
    {
        public int TotalEmployers {  get; set; }
        public int TotalCandidates { get; set; }
        public int TotalJobPostings { get; set; }
        public int TotalApplications { get; set; }

    }
}