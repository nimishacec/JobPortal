using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Request;
using JobPortalWebApplication.Models.Response;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Employer
{
    public partial class JobDetails : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            if (Session["EmployeeID"]!=null)
            {
                if (Request.QueryString["JobID"] != null)
                {
                    if (!IsPostBack)
                    {
                        int jobID = Convert.ToInt32(Request.QueryString["JobID"]);
                        LoadJobDetails(jobID);
                    }
                }
            }
            else
            {
                Response.Redirect("EmployerLogin.aspx");
            }
        }
        public void LoadJobDetails(int jobID)
        {
            var jobDetails = _dataAccess.GetJobDetailsFromDBForCandidate(jobID);

            if (jobDetails!= null)
            {
                lblJobTitle.InnerText = jobDetails.JobTitle;
                lblCompanyName.InnerText = jobDetails.CompanyName;
                lblJobLocation.InnerText = jobDetails.JobLocation;
                lblJobType.InnerText = jobDetails.JobType;
                lblSalary.InnerText = jobDetails.Salary.ToString(); // Format as currency
                lblVacancy.InnerText = jobDetails.Vacancy.ToString();
                lblApplicationDeadline.InnerText = jobDetails.ApplicationDeadline.ToString();
                lblJobDescription.InnerText = jobDetails.JobDescription;
                lblRequiredSkills.InnerText = jobDetails.RequiredSkills;
                lblContactEmail.InnerText = jobDetails.ContactEmail;
                lnkWebsite.HRef = jobDetails.Website;
                lnkWebsite.InnerText = jobDetails.Website;               
            }
            else
            {
                Session["StatusMessage1"] = "The Job has expired...!!!!!";
                Response.Redirect("StatusPage.aspx");
               // Response.Redirect("StatusPage.aspx?returnUrl=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            // Redirect to the previous page or a specific page
            if (Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString()); // Go to the previous page
            }
            else
            {
                Response.Redirect("ViewAllJobs.aspx"); // Fallback page
            }
        }
        private List<int> GetAppliedJobIDs()
        {
            int candidateId = (int)Session["CandidateID"];
            var applied = _dataAccess.GetAppliedJobs(candidateId);
            return applied; // Example applied job IDs
        }
        protected void btnApply_Click(object sender, EventArgs e)
        {
            int candidateId = 0;

            if (Session["CandidateID"] != null)
            {
                candidateId = (int)Session["CandidateID"];

                var job = new JobApplication();
               
                if (Request.QueryString["JobID"] != null)
                {
                    string jobIdString = Request.QueryString["JobID"];

                    // Convert the string to an integer
                    int jobId;
                    if (int.TryParse(jobIdString, out jobId))
                    {

                        job.JobID = jobId;
                    }
                    List<int> appliedJobIDs = GetAppliedJobIDs();
                    bool hasApplied = appliedJobIDs.Contains(jobId);

                    if (hasApplied)
                    {
                        // Show a message box and redirect
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                            "alert('You have already applied for this job.'); window.location ='ViewAllJobs.aspx';", true);
                    }
                    else
                    {
                        job.CandidateID = candidateId;
                        var apply = _dataAccess.JobApplication(job);
                        
                        Session["StatusMessage"] = apply;
                        Response.Redirect("StatusPage.aspx");
                    }

                }
                else
                {
                    Session["StatusMessage"] = "No JobId Found.....Please visit again!!!!";
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