using JobPortalWebApplication.Candidate;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Admin
{
    public partial class Admin1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int AdminId = 0;
            if (Session["AdminID"] != null)
            {

                AdminId = (int)Session["AdminID"];
            }
        }
        protected void btnNewJobs_Click(object sender, EventArgs e)
        {
            int AdminId = 0;
            if (Session["AdminID"] != null)
            {

                AdminId = (int)Session["AdminID"];
                Session["AdminID"] = AdminId;


                Response.Redirect("NewJobs.aspx");
            }
            else
            {
                Response.Redirect("AdminLogin.aspx");

            }

        }
        protected void btnProfileView_Click(object sender, EventArgs e)
        {
            int AdminId = 0;
            if (Session["AdminID"] != null)
            {

                AdminId = (int)Session["AdminID"];
                Session["AdminID"] = AdminId;


                Response.Redirect("AdminNewDash.aspx");
            }
            else
            {
                Response.Redirect("AdminLogin.aspx");

            }

        }
        
        protected void btnJobStatics_Click(object sender, EventArgs e)
        {
            int AdminId = 0;
            if (Session["AdminID"] != null)
            {

                AdminId = (int)Session["AdminID"];
                Session["AdminID"] = AdminId;


                Response.Redirect("JobStatics.aspx");
            }
            else
            {
                Response.Redirect("AdminLogin.aspx");

            }

        }
        protected void btnJobEditRequest_Click(object sender, EventArgs e)
        {
            int AdminId = 0;
            if (Session["AdminID"] != null)
            {

                AdminId = (int)Session["AdminID"];
                Session["AdminID"] = AdminId;

                Response.Redirect("JobEditApproval.aspx");
            }
            else
            {
                Response.Redirect("AdminLogin.aspx");

            }
        }
        protected void btnApplicationApprovalRequest_Click(object sender, EventArgs e)
        {
            int AdminId = 0;
            if (Session["AdminID"] != null)
            {
                AdminId = (int)Session["AdminID"];
                Session["AdminID"] = AdminId;

                Response.Redirect("ApplicationApproval.aspx");
            }
            else
            {
                Response.Redirect("AdminLogin.aspx");

            }
        }
        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminLogin.aspx");

        }
        protected void btnEmployees_Click(object sender, EventArgs e)
        {
            Log.Information("EmployeesList.....");
            if (Session["AdminID"] != null)
            {
                int AdminId = 0;
                AdminId = (int)Session["AdminID"];
                Session["AdminID"] = AdminId;
                Log.Information("EmployeesList-Redirected");
                Response.Redirect("EmployeeList.aspx");
            }
            else
            {
                Log.Error("EmployeesList-AdminLogin session invalid");
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

                Response.Redirect("CandidateList.aspx");
            }
            else
            {
                Response.Redirect("AdminLogin.aspx");
            }

        }
        protected void btnTrainers_Click(object sender, EventArgs e)
        {
            if (Session["AdminID"] != null)
            {
                int AdminId = 0;
                AdminId = (int)Session["AdminID"];
                Session["AdminID"] = AdminId;

                Response.Redirect("TrainersList.aspx");
            }
            else
            {
                Response.Redirect("AdminLogin.aspx");
            }

        }
    }
}