using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Request;
using JobPortalWebApplication.Models.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Candidate
{
    public partial class JobSearchResults : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            int candidateId = 0;
            if (!IsPostBack)
            {
                // Retrieve the search results from the session
                List<JobSearchResult> jobs = Session["JobSearchResults"] as List<JobSearchResult>;
                if (Session["CandidateID"] != null)
                {
                    candidateId = (int)Session["CandidateID"];

                    if (jobs != null)
                    {
                        BindVerticalData(jobs);
                        //gvJobResults.DataSource = jobs;
                        //gvJobResults.DataBind();
                    }
                }
                else
                {
                    Response.Redirect("CandidateLogin.aspx");
                }
            }

        }
        protected void gvJobResults_PageIndexChanging(object sender, EventArgs e)
        {

        }
        private void BindVerticalData(List<JobSearchResult> jobSearchResults)
        {
            // Example data (usually retrieved from a database)
            DataTable dtJobDetails = new DataTable();
            dtJobDetails.Columns.Add("FieldName");
            dtJobDetails.Columns.Add("FieldValue");
            foreach (JobSearchResult jobSearchResult in jobSearchResults)
            {
                // Populate the DataTable with your actual job data
                dtJobDetails.Rows.Add("Job ID", jobSearchResult.JobID);
                dtJobDetails.Rows.Add("Job Title", jobSearchResult.JobTitle);
                dtJobDetails.Rows.Add("Job Description", jobSearchResult.JobDescription);
                dtJobDetails.Rows.Add("Location", jobSearchResult.JobLocation);
                dtJobDetails.Rows.Add(" Annual Salary(CTC)", jobSearchResult.Salary);
                dtJobDetails.Rows.Add("Company Name", jobSearchResult.CompanyName!= null?jobSearchResult.CompanyName: null);
                dtJobDetails.Rows.Add("Job Type", jobSearchResult.JobType);
                dtJobDetails.Rows.Add("Application Deadline", jobSearchResult.ApplicationDeadline);
                dtJobDetails.Rows.Add("Required Skills", jobSearchResult.RequiredSkills);

                // Bind the data to the GridView
                gvJobResults.DataSource = dtJobDetails;
                gvJobResults.DataBind();
            }
        }
        protected void btnApply_Click(object sender, EventArgs e)
        {
            int candidateId = 0;

            if (Session["CandidateID"] != null)
            {
                candidateId = (int)Session["CandidateID"];

                var job = new JobApplication();
                if (Session["JobSearchResults"] != null)
                {
                    List<JobSearchResult> jobsList = (List<JobSearchResult>)Session["JobSearchResults"];
                    if (jobsList.Count() < 1)
                    {                   

                        foreach (var jobs in jobsList)
                        {
                            job.JobID = jobs.JobID;
                            job.CandidateID = candidateId;
                            job.ApplicationDate = DateTime.Now;
                            var apply = _dataAccess.JobApplication(job);
                            Session["StatusMessage"] = apply;
                            Response.Redirect("StatusPage.aspx");
                        }
                    }
                    else
                    {
                        Session["StatusMessage"] = "No Records Found.....Please visit again!!!!";
                        Response.Redirect("StatusPage.aspx");
                    }
                }
                else
                {
                    Session["StatusMessage"] = "No Records Found.....Please visit again!!!!";
                    Response.Redirect("StatusPage.aspx");
                }
            }
            else
            {
                Response.Redirect("CandidateLogin.aspx");
            }


        }
    }
}
