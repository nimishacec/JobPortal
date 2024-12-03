using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobPortalWebApplication.Models.Request
{
    public class Jobs
    {
        public int EmployeeID { get; set; }

        public int JobID { get; set; }


        public string JobTitle { get; set; }


        public int Vacancy { get; set; }


        public string JobDescription { get; set; }


        public string Qualifications { get; set; }


        public string Experience { get; set; }


        public string RequiredSkills { get; set; }


        public string JobLocation { get; set; }


        public decimal? Salary { get; set; }





        public string JobType { get; set; }




        public DateTime? ApplicationDeadline { get; set; }


        public string Website { get; set; }



        public DateTime? ApplicationStartDate { get; set; }

        public string RequestStatus { get; set; }
    }
}

