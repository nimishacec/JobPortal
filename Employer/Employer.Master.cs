using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Response;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Employer
{
    public partial class Employer : System.Web.UI.MasterPage
    {
        private DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            int EmployeeId = 0;
            if (Session["EmployeeID"] != null)
            {
                EmployeeId = (int)Session["EmployeeID"];
            }
            if (!IsPostBack)
            {
              //  List<JobApplicationCount> jobApplicationCounts = _dataAccess.GetApplicationCountsForAllJobs(EmployeeId);
                //rptJobApplicationCounts.DataSource = jobApplicationCounts;
                //rptJobApplicationCounts.DataBind();         
            }
        }
       
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            Response.Redirect("SearchCandidate.aspx");

        }
        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
           int EmployeeId = (int)Session["EmployeeID"];
            Response.Redirect("ViewMessage.aspx");

        }
        protected void btnSearchCandidates_Click(object sender, EventArgs e)
        {

            Response.Redirect("CandidatesSearch.aspx");

        }
        private int GetEmployeeId()
        {
            int EmployeeId = 0;
            if (Session["EmployeeID"] != null)
            {
                EmployeeId = (int)Session["EmployeeID"];
            }
            else if (Request.QueryString["EmployeeID"] != null)
            {
                int.TryParse(Request.QueryString["EmployeeID"], out EmployeeId);
            }
            return EmployeeId;
        }
        protected void btnViewProfile_Click(object sender, EventArgs e)
        {
            int EmployeeId = GetEmployeeId();
            if (EmployeeId != 0)
            {

                Response.Redirect($"EmployerViewDashboard.aspx?EmployeeID={EmployeeId}");
            }
            else
            {

                Response.Redirect("EmployerLogin.aspx");
            }
        }
        protected void btnEditProfile_Click(object sender, EventArgs e)
        {
            int EmployeeId = GetEmployeeId();
            if (EmployeeId != 0)
            {

                Response.Redirect($"EmployeeDashboard.aspx?EmployeeID={EmployeeId}");
            }
            else
            {

                Response.Redirect("EmployerLogin.aspx");
            }
        }
        protected void btnViewApplications_Click(object sender, EventArgs e)
        {
            int EmployeeId = GetEmployeeId();
            if (EmployeeId != 0)
            {

                Response.Redirect("ViewApplications.aspx");
            }
            else
            {

                Response.Redirect("EmployerLogin.aspx");
            }
        }
        protected void btnJobPost_Click(object sender, EventArgs e)
        {
            int EmployeeId = GetEmployeeId();
            if (EmployeeId != 0)
            {
                Session["EmployeeId"] = EmployeeId;
                Response.Redirect($"JobPost.aspx");
            }
            else
            {

                Response.Redirect("EmployerLogin.aspx");
            }
        }

        protected void btnViewJobs_Click(object sender, EventArgs e)
        {
            int EmployeeId = GetEmployeeId();
            if (EmployeeId != 0)
            {
                Session["EmployeeId"] = EmployeeId;
                Response.Redirect($"ViewJobs.aspx");
            }
            else
            {
                Response.Redirect("EmployerLogin.aspx");
            }
        }
        protected void btnViewJobEditStatus_Click(object sender, EventArgs e)
        {
            int EmployeeId = GetEmployeeId();
            if (EmployeeId != 0)
            {
                Session["EmployeeId"] = EmployeeId;
                Response.Redirect("ViewJobEditStatus.aspx");
            }
            else
            {
                Response.Redirect("EmployerLogin.aspx");
            }
        }
        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployerLogin.aspx");
        }
    }
}