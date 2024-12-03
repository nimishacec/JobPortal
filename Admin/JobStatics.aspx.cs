using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Response;

namespace JobPortalWebApplication.Admin
{
    public partial class JobStatics : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess=Global.DataAccess;
            GetAnalyticsData();
            //dataContainer.Visible = true;
            //loadingContainer.Visible = false;


        }
        public void GetAnalyticsData()
        {
            var data = _dataAccess.GetDashboardAnalytics();
            
            litTotalEmployers.Text = data.TotalEmployers.ToString();
           
            litTotalCandidates.Text = data.TotalCandidates.ToString();
            
            litTotalJobPostings.Text = data.TotalJobPostings.ToString();
           
            litTotalApplications.Text = data.TotalApplications.ToString();
            


        }
       
    }

    
}