using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortalWebApplication.Models.Request
{
    public class TrainingPgmRequest
    {
        public int ProgramID { get; set; } // Optional: If you need to retrieve an existing program
        public string ProgramName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DeliveryMode { get; set; }
        public decimal Price { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public DateTime ModifiedDate { get; set; }
    }
}