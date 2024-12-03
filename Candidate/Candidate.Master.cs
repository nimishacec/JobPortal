using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Candidate
{
    public partial class Candidate : System.Web.UI.MasterPage
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
          
            if (!IsPostBack)
            {
                BindJobTitles();
                BindJobLocation();
            }
        }
        public void BindJobLocation()
        {
            var jobLocations = _dataAccess.GetJobLocation();
            
            ddlJobLocation.DataSource = jobLocations;
            ddlJobLocation.DataTextField = "LocationName";
            ddlJobLocation.DataValueField = "LocationName";
            ddlJobLocation.DataBind();

            // Add the "Others" option manually
            ddlJobLocation.Items.Insert(0, new ListItem("Select Location", ""));
        }
        public void BindJobTitles()
        {
            var jobTitles = _dataAccess.GetJobTitles();
            ddlJobTitle.DataSource = jobTitles;
            ddlJobTitle.DataTextField = "JobTitle";
            ddlJobTitle.DataValueField = "JobTitle";
            ddlJobTitle.DataBind();

            // Add the "Others" option manually

            ddlJobTitle.Items.Insert(0, new ListItem("Select JobTitle", ""));

            // Add the "Others" option
            ddlJobTitle.Items.Add(new ListItem("Others", "Others"));
        }
        private int GetCandidateId()
        {
            int candidateId = 0;
            if (Session["CandidateID"] != null)
            {
                candidateId = (int)Session["CandidateID"];
            }
            else if (Request.QueryString["CandidateID"] != null)
            {
                int.TryParse(Request.QueryString["CandidateID"], out candidateId);
            }
            return candidateId;
        }
        protected void ddlJobTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlJobTitle.SelectedValue == "Others")
            {
                txtOtherJobTitle.Visible = true;
            }
            else
            {
                txtOtherJobTitle.Visible = false;
            }
        }
        protected void btnFilter_click(object sender, EventArgs e)
        {

            string sortBy = ddlSortBy.SelectedValue;
            string sortOrder = rblSortOrder.SelectedValue;
            string jobTitle = ddlJobTitle.SelectedValue == "Others" ? txtOtherJobTitle.Text : ddlJobTitle.SelectedValue;
          
            string location = ddlJobLocation.SelectedValue != null ? ddlJobLocation.SelectedValue : null;
            string skills = txtSkills.Text;
            string experience = ddlExperience.SelectedValue !=null ? ddlExperience.SelectedValue : null;
            string education = txtEducation.Text;

            // Store the values in session
            Session["jobTitle"] = jobTitle;
            Session["location"] = location;
            Session["skills"] = skills;
            Session["experience"] = experience;
            Session["education"] = education;
            Session["sortBy"] = sortBy;
            Session["sortOrder"] = sortOrder;

            // Redirect to the filtered results page
            Response.Redirect("FilteredResults.aspx");
        }
      
        protected void btnJobSearch_Click(object sender, EventArgs e)
        {
            int candidateId = GetCandidateId();
            if (candidateId != 0)
            {


                Response.Redirect($"CandidateJobSearch.aspx?CandidateID={candidateId}");
            }
            else
            {
               
                Response.Redirect("CandidateLogin.aspx");
            }
        }
        protected void btnJobAlerts_Click(object sender, EventArgs e)
        {
            int candidateId = GetCandidateId();
            if (candidateId != 0)
            {
                Response.Redirect("JobAlerts.aspx");
            }
            else
            {
                Response.Redirect("CandidateLogin.aspx");
            }
        }
        protected void btnMessages_Click(object sender, EventArgs e)
        {
            int candidateId = GetCandidateId();
            if (candidateId != 0)
            {
                Session["UserID"]=candidateId;
                Session["UserType"] = "Candidate";
                Response.Redirect("~/Candidate/ViewMessage.aspx");
            }
            else
            {
                Response.Redirect("CandidateLogin.aspx");
            }
        }
        protected void btnNotifications_Click(object sender, EventArgs e)
        {
            int candidateId = GetCandidateId();
            if (candidateId != 0)
            {
                Response.Redirect("JobNotifications.aspx");
            }
            else
            {
                Response.Redirect("CandidateLogin.aspx");
            }
        }
        protected void btnViewJobs_Click(object sender, EventArgs e)
        {
            int candidateId = 0;
            if (Session["CandidateID"] != null)
            {
                candidateId = (int)Session["CandidateID"];
                Session["CandidateID"] = candidateId;

                Response.Redirect($"ViewAllJobs.aspx?");
            }
            else
            {
                Response.Redirect("CandidateLogin.aspx");
            }
        }
        protected void btnAppliedJobs_Click(object sender, EventArgs e)
        {
            int candidateId = 0;
            if (Session["CandidateID"] != null)
            {
                candidateId = (int)Session["CandidateID"];
                Session["CandidateID"] = candidateId;

                Response.Redirect("ViewAppliedJobs.aspx");
            }
            else
            {
               
                Response.Redirect("CandidateLogin.aspx");
            }
        }
        protected void btnEditProfile_Click(object sender, EventArgs e)
        {
            int candidateId = GetCandidateId();
            if (candidateId != 0)
            {
                Response.Redirect($"CandidateDashboard.aspx?CandidateID={candidateId}");
            }
            else
            {
              
                Response.Redirect("CandidateLogin.aspx");
            }
        }
        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect($"CandidateLogin.aspx");

        }
    }
}