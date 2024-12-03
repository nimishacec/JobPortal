using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Request;
using JobPortalWebApplication.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Candidate
{
    public partial class CandidateJobSearch : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            int CandidateId = 0;
            if (Session["CandidateID"] != null)
            {
                 CandidateId = Convert.ToInt32(Session["CandidateID"]);
                
            }
            else
                Response.Redirect("CandidateLogin.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtKeyword.Text;
            int location = int.TryParse(txtLocation.Text, out location) ? location : 0;
            decimal? minSalary = decimal.TryParse(txtMinSalary.Text, out decimal minSal) ? minSal : (decimal?)null;
            decimal? maxSalary = decimal.TryParse(txtMaxSalary.Text, out decimal maxSal) ? maxSal : (decimal?)null;
            string requiredSkills = txtRequiredSkills.Text;
            var jobserach = new JobSearch();
            jobserach = new JobSearch
            {
                Keyword = keyword,
                Location = location,
                MinSalary = minSalary,
                MaxSalary = maxSalary,
                RequiredSkills = requiredSkills,
            };
            int CandidateId = GetCandidateId();
            Session["CandidateID"] = CandidateId;
            List<JobSearchResult> jobs = _dataAccess.GetJobsFromDB(jobserach, CandidateId);
            if (jobs.Count() < 1)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", "alert('No Records Found.....Please visit again!!!!.'); window.location.href='CandidateJobSearch.aspx';", true);

                
            }
            else
            {
                Session["JobSearchResults"] = jobs;
                Response.Redirect("JobSearchResults.aspx");
            }
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
    }
}