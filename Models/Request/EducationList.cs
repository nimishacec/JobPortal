using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortalWebApplication.Models.Request
{
    public class EducationList
    {
        public int EducationId { get; set; }    
        public string DegreeId { get; set; }
        public string DegreeName { get; set; }
        public string CollegeUniversityName { get; set; }
        public string PlaceAddress { get; set; }
        public string GraduatedOrPursuing { get; set; }
        public string KeySkills { get; set; }
        public string AcademicProject { get; set; }
    }
}