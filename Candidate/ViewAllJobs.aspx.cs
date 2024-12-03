using JobPortalWebApplication.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Candidate
{
    public partial class ViewAllJobs : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        private List<int> appliedJobIDs;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;


            if (Session["CandidateID"] != null)
            {
                int candidateId = (int)Session["CandidateID"];
                appliedJobIDs = GetAppliedJobIDs();
                if (!IsPostBack)
                {
                    
                    LoadAllJobs();
                }
            }
            else
            {
                Response.Redirect("CandidateLogin.aspx");
            }
        }
        private List<int> GetAppliedJobIDs()
        {
            int candidateId = (int)Session["CandidateID"];
            var applied = _dataAccess.GetAppliedJobs(candidateId);
            return applied; // Example applied job IDs
        }

        protected string GetAppliedStatus(object jobID)
        {
            int jobId = Convert.ToInt32(jobID);
            bool isApplied = appliedJobIDs.Contains(jobId);

            if (isApplied)
            {
                return "<span style='color: #ffffff; background-color: #006600; border: 1px solid #006600; padding: 4px 8px; border-radius: 4px;'>Applied</span>"; // Green background for applied
            }
            else
            {
                return "<span style='color: #ffffff; background-color: #cc0000; border: 1px solid #cc0000; padding: 4px 8px; border-radius: 4px;'>Not Applied</span>"; // Red background for not applied
            }
        }
        protected void gvJobs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvJobs.PageIndex = e.NewPageIndex;            
            LoadAllJobs();

        }
        private void LoadAllJobs()
        {
            int candidateID = Convert.ToInt32(Session["CandidateID"]);
            var jobs = _dataAccess.GetAllJobsFromDB();

            if (jobs != null)
            {
                gvJobs.DataSource = jobs;
                gvJobs.DataBind();
            }
            else
            {
                // Handle the case when no jobs are found
                gvJobs.EmptyDataText = "No jobs found.";
                gvJobs.DataBind();
            }
        }
    }
}