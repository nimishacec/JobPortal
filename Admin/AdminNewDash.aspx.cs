using JobPortalWebApplication.DataBase;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Admin
{
    public partial class AdminNewDash : System.Web.UI.Page
    {
        public DataAccess _dataAccess;

        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            int AdminId = 0;
            if (!IsPostBack)
            {
                Log.Information("Admin logged");
                if (Session["AdminID"] != null)
                {
                    AdminId = (int)Session["AdminID"];
                    
                }

                //SetCurrentDate();
                GetAnalyticsData();
            }

        }
        private void SetCurrentDate()
        {
            // Get the current date
            DateTime currentDate = DateTime.Now;
            // Format the date as needed, e.g., "dd MMM yyyy"
            string formattedDate = currentDate.ToString("dd MMM yyyy");
            // Set the text of the label
            foreach (RepeaterItem item in repeaterTotalEmployers.Items)
            {
                Label litCurrentDate = (Label)item.FindControl("litCurrentDate");
                if (litCurrentDate != null)
                {
                    litCurrentDate.Text = formattedDate; // Set the current date
                }
            }
                //  litCurrentDate.Text = formattedDate;
            }

        protected void btnEmployees_Click(object sender, EventArgs e)
        {
            Log.Information("On EmployeeList click");
            if (Session["AdminID"] != null)
            {
                int AdminId = 0;
                AdminId = (int)Session["AdminID"];
                Session["AdminID"] = AdminId;
                Log.Information("EmployeeList clicked");
                Response.Redirect("EmployeeList.aspx");
            }
            else
            {
                Log.Information("On EmployeeList click-No session");
                Response.Redirect("AdminLogin.aspx");
            }
        }
        protected void btnCandidates_Click(object sender, EventArgs e)
        {
            if (Session["AdminID"] != null)
            {
                int AdminId = 0;
                AdminId = (int)Session["AdminID"];
                Session["AdminID"] = AdminId;
                Log.Information("CandidateList clicked");
                Response.Redirect("CandidateList.aspx");
            }
            else
            {
                Response.Redirect("AdminLogin.aspx");
            }

        }
        public void GetAnalyticsData()
        {
            var data = _dataAccess.GetDashboardAnalytics();
            if (data != null)
            {
                // Create a data source for each Repeater
                var employersData = new List<object>
        {
            new { TotalEmployers = data.TotalEmployers }
        };
                repeaterTotalEmployers.DataSource = employersData;
                repeaterTotalEmployers.DataBind();

                var candidatesData = new List<object>
        {
            new { TotalCandidates = data.TotalCandidates }
        };
                repeaterTotalCandidates.DataSource = candidatesData;
                repeaterTotalCandidates.DataBind();

                var jobPostingsData = new List<object>
        {
            new { TotalJobPostings = data.TotalJobPostings }
        };
                repeaterTotalJobPostings.DataSource = jobPostingsData;
                repeaterTotalJobPostings.DataBind();

                var applicationsData = new List<object>
        {
            new { TotalApplications = data.TotalApplications }
        };
                repeaterTotalApplications.DataSource = applicationsData;
                repeaterTotalApplications.DataBind();
            }

        }
    }
}