using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Employer
{
    public partial class EmployerNewDash : System.Web.UI.Page
    {
        public DataAccess _dataAccess;

        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            int AdminId = 0;
            if (!IsPostBack)
            {
                if (Session["EmployeeID"] != null)
                {
                    int employerId = Convert.ToInt32(Session["EmployeeID"]);
                   string employerName = GetEmployerName(employerId);
                  
                    if (!string.IsNullOrEmpty(employerName))
                    {
                        lblWelcomeMessage.Text = "Welcome " + employerName;
                    }

                    GetApplicatonCount(employerId);
                }
                else
                {
                    Response.Redirect("Employerlogin.aspx");
                }

                //SetCurrentDate();
                //GetAnalyticsData();
            }

        }
        private void GetApplicatonCount(int employeriD)
        {
            List<JobApplicationCount> jobApplicationCounts = _dataAccess.GetApplicationCountsForAllJobs(employeriD);
            rptJobApplicationCounts.DataSource = jobApplicationCounts;
            rptJobApplicationCounts.DataBind();
        }
        private string GetEmployerName(int employerId)
        {
            var employerName=_dataAccess.GetEmployerName(employerId) ?? string.Empty;
            return employerName;
          
        }

        //private void SetCurrentDate()
        //{
        //    // Get the current date
        //    DateTime currentDate = DateTime.Now;
        //    // Format the date as needed, e.g., "dd MMM yyyy"
        //    string formattedDate = currentDate.ToString("dd MMM yyyy");
        //    // Set the text of the label
        //    foreach (RepeaterItem item in repeaterTotalEmployers.Items)
        //    {
        //        Label litCurrentDate = (Label)item.FindControl("litCurrentDate");
        //        if (litCurrentDate != null)
        //        {
        //            litCurrentDate.Text = formattedDate; // Set the current date
        //        }
        //    }
        //        //  litCurrentDate.Text = formattedDate;
        //    }

       
        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            int EmployeeId = (int)Session["EmployeeID"];
            Response.Redirect("ViewMessage.aspx");

        }
       
    }
}