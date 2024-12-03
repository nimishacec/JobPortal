using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Employer;
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
using static System.Net.Mime.MediaTypeNames;
using ViewJobs = JobPortalWebApplication.Models.Response.ViewJobs;

namespace JobPortalWebApplication.Admin
{
    public partial class JobVerification : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            if (Session["AdminID"] != null)
            {
                if (Request.QueryString["RequestID"] != null)
                {
                    int AdminID = Convert.ToInt32(Session["AdminID"]);
                    string ApplicationID = (Request.QueryString["ApplicationCode"]);
                    int RequestID = Convert.ToInt32(Request.QueryString["RequestID"]);
                    if (!IsPostBack)
                    {
                        LoadApplications(ApplicationID);
                        LoadJob(RequestID);
                    }
                }
            }
            else
            {
                Response.Redirect("AdminLogin.aspx");
            }
        }
        private void LoadApplications(string ApplicationID)
        {
            var applications = _dataAccess.GetApplicationFromDB(ApplicationID);
            if (applications != null)
            {
                //BindVerticalApplData(applications);
            }
            else
            {
                Session["StatusMessage"] = "No Records Found.....Please visit again!!!!";
                Response.Redirect("StatusPage.aspx");
            }
        }
        private void LoadJob(int RequestID)
        {
            var jobDetails = _dataAccess.GetJobEditDetails(RequestID);
            if (jobDetails.Rows.Count > 0)
            {
                DataRow row = jobDetails.Rows[0];
                HighlightChanges(lblJobTitle, row["OriginalJobTitle"], row["RequestedJobTitle"]);
                HighlightChanges(lblCompanyName, row["OriginalCompanyName"], row["RequestedCompanyName"]);
                HighlightChanges(lblJobLocation, row["OriginalJobLocation"], row["RequestedJobLocation"]);
                HighlightChanges(lblSalary, row["OriginalSalary"], row["RequestedSalary"]);
                HighlightChanges(lblJobType, row["OriginalJobType"], row["RequestedJobType"]);
                HighlightChanges(lblVacancy, row["OriginalVacancy"], row["RequestedVacancy"]);
                HighlightChanges(lblExperience, row["OriginalExperience"], row["RequestedExperience"]);
                HighlightChanges(lblApplicationDeadline, row["OriginalApplicationDeadline"], row["RequestedApplicationDeadline"]);

                HighlightChanges(lblApplicationStarts, row["OriginalStartDate"], row["RequestedStartDate"]);
                HighlightChanges(lblQualifications, row["OriginalQualifications"], row["RequestedQualifications"]);
                HighlightChanges(lblRequiredSkills, row["OriginalRequiredSkills"], row["RequestedRequiredSkills"]);
                HighlightChanges(lblJobDescription, row["OriginalJobDescription"], row["RequestedJobDescription"]);
                HighlightChanges(lblContactEmail, row["OriginalContactEmail"], row["RequestedContactEmail"]);
                HighlightChanges(lnkWebsite, row["OriginalCompanyWebsite"], row["RequestedCompanyWebsite"]);

                // BindVerticalJobData(jobDetails);
            }
        }
        private void HighlightChanges(Label label, object originalValue, object requestedValue)
        {
            // var label = FindControl(labelId) as Label;

            if (originalValue != null && requestedValue != null && !originalValue.ToString().Equals(requestedValue.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                label.Text = requestedValue.ToString();

                label.ForeColor = System.Drawing.Color.Red; // Set the background color to highlight

            }
            else
            {
                label.Text = originalValue?.ToString() ?? string.Empty;
            }
        }

        //private void BindVerticalApplData(JobApplicationResult jobApplResult)
        //{           
        //    DataTable dtJobDetails = new DataTable();
        //    dtJobDetails.Columns.Add("FieldName");
        //    dtJobDetails.Columns.Add("FieldValue");            
        //    dtJobDetails.Rows.Add("Application ID", jobApplResult.ApplicationCode);
        //    //dtJobDetails.Rows.Add("Job ID", jobApplResult.JobID);
        //    dtJobDetails.Rows.Add("Job Title", jobApplResult.JobTitle);
        //   // dtJobDetails.Rows.Add("Candidate ID", jobApplResult.CandidateID);
        //    dtJobDetails.Rows.Add("Candidate Email Address", jobApplResult.CandidateEmailAddress);
        //    dtJobDetails.Rows.Add("Highest Education Level", jobApplResult.HighestEducationLevel);
        //    dtJobDetails.Rows.Add("Company Name", jobApplResult.CompanyName);
        //    dtJobDetails.Rows.Add("Application Date", jobApplResult.ApplicationDate);
        //    dtJobDetails.Rows.Add("JobApplication LastDate", jobApplResult.JobApplicationLastDate);           
        //    GridView1.DataSource = dtJobDetails;
        //    GridView1.DataBind();
        //}
        protected void btnVerify_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["RequestID"] != null)
            {

                //   string ApplicationID = (Request.QueryString["ApplicationCode"]);
                int RequestID = Convert.ToInt32(Request.QueryString["RequestID"]);
                //string appstatus = _dataAccess.GetApplicationStatusForAdmin(ApplicationID); // Function to get current approval status

                //if (appstatus == "APPROVED")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Application has already been approved.'); window.location='ApplicationApproval.aspx' ", true);
                //}
                //else
                //{

                    var status = _dataAccess.VerifyJobRequest(RequestID, "APPROVED");
                    if (status == "APPROVED")
                    {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", "$('#confirmationModal').modal('show');", true);
                     }
                else
                    {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Not updated'); window.location='JobEditApproval.aspx'", true);

                }

            }
            else
                Response.Redirect("JobEditApproval.aspx");
        }
        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ApplicationCode"] != null && Request.QueryString["JobID"] != null)
            {

                string ApplicationID = (Request.QueryString["ApplicationCode"]);
                int JobID = Convert.ToInt32(Request.QueryString["JobID"]);
                string appstatus = _dataAccess.GetApplicationStatusForAdmin(ApplicationID); // Function to get current approval status

                if (appstatus == "REJECTED")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Application has already been REJECTED.');", true);
                }
                else
                {

                    var status = _dataAccess.VerifyJobApplication(ApplicationID, JobID, "REJECTED", "ADMIN");
                    if (status == "Approved")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('The Job Application request has been Rejected by Admin.');", true);

                    }
                    else
                    {
                        Session["StatusMessage"] = status;

                        Response.Redirect("StatusPage.aspx");
                    }
                }
            }
            else
                Response.Redirect("JobEditApproval.aspx");
        }

        private void BindVerticalJobData(ViewJobs jobResult)
        {
            // Example data (usually retrieved from a database)
            DataTable dtJobDetails = new DataTable();
            dtJobDetails.Columns.Add("FieldName");
            dtJobDetails.Columns.Add("FieldValue");

            // Populate the DataTable with your actual job data
            // dtJobDetails.Rows.Add("Application ID", jobResult.ApplicationID);
            dtJobDetails.Rows.Add("Job ID", jobResult.JobID);
            dtJobDetails.Rows.Add("CompanyName", jobResult.CompanyName);
            dtJobDetails.Rows.Add("Job Title", jobResult.JobTitle);
            dtJobDetails.Rows.Add("JobDescription", jobResult.JobDescription);
            dtJobDetails.Rows.Add("JobLocation", jobResult.JobLocation);
            dtJobDetails.Rows.Add("JobType", jobResult.JobType);
            dtJobDetails.Rows.Add("Experience", jobResult.Experience);
            dtJobDetails.Rows.Add("Qualifications", jobResult.Qualifications);
            dtJobDetails.Rows.Add("RequiredSkills", jobResult.RequiredSkills);
            dtJobDetails.Rows.Add("ApplicationDeadline", jobResult.ApplicationDeadline);
            dtJobDetails.Rows.Add("Salary", jobResult.Salary);


            // Bind the data to the GridView
            //GridView2.DataSource = dtJobDetails;
            //GridView2.DataBind();

        }



    }
}