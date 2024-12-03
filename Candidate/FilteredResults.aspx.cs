using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Candidate
{
    public partial class FilteredResults : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            if (Session["CandidateID"] != null)
            {
                int candidateId = Convert.ToInt32(Session["CandidateID"]);

                if (!IsPostBack)
                {
                    string jobTitle = Session["jobTitle"] as string;
                    string location = Session["location"] as string;
                    string skills = Session["skills"] as string;
                    string experience = Session["experience"] as string;
                    string education = Session["education"] as string;
                    string sortBy = Session["sortBy"] as string;
                    string sortOrder = Session["sortOrder"] as string;

                    LoadFilteredJobs(candidateId,jobTitle, location, skills, experience, education,sortBy,sortOrder);                    
                }
            }
            else
            {
                Response.Redirect("CandidateLogin.aspx");
            }
          
        }
        private void LoadFilteredJobs(int candidateId,string jobTitle,string location, string skills,string experience,string education,string sortBy,string sortOrder)
        {

            List<JobSearchResult> appliedJobs = _dataAccess.GetFilteredJobs(jobTitle, location, skills, experience, education,sortBy,sortOrder); // Retrieve applied jobs from DB

            if (appliedJobs != null && appliedJobs.Count > 0)
            {
                rptAppliedJobs.DataSource = appliedJobs;
                rptAppliedJobs.DataBind();
            }
            else
            {
                
                lblNoJobs.Visible = true;
            }
        }
    }
}