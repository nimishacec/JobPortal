using iTextSharp.text.pdf;
using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Request;
using JobPortalWebApplication.Models.Response;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Admin
{
    public partial class JobDetails : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        public string AESKey;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            AESKey = Global._AESKey;
            if (Session["AdminID"] != null)
            {
                int EmployeeId = Convert.ToInt32(Session["AdminID"]);
                if (Request.QueryString["JobID"] != null)
                {
                    if (!IsPostBack)
                    {
                        //  int jobID = Convert.ToInt32(Request.QueryString["JobID"]);
                        string encryptedJobId = Request.QueryString["JobID"];
                        int decryptedJobId = Convert.ToInt32(Decrypt(encryptedJobId));
                        LoadJobDetails(decryptedJobId);
                    }
                }
            }
            else
            {
                Response.Redirect("AdminLogin.aspx");
            }
        }
        public string Decrypt(string encryptedText)
        {
            byte[] cipherBytes = Convert.FromBase64String(encryptedText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(AESKey); // Same key used for encryption
                aes.IV = new byte[16];

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }
        private void LoadJobDetails(int jobID)
        {
            var jobDetails = _dataAccess.GetJobDetailsFromDB(jobID);

            if (jobDetails!= null)
            {
                lblJobTitle.Text = jobDetails.JobTitle;
                lblCompanyName.Text = jobDetails.CompanyName;
                lblJobLocation.Text = jobDetails.JobLocation;
                lblJobType.Text = jobDetails.JobType;
                lblSalary.Text = jobDetails.Salary.ToString(); // Format as currency
                lblVacancy.Text = jobDetails.Vacancy.ToString();
                lblApplicationDeadline.Text = jobDetails.ApplicationDeadline.ToString();
                lblQualifications.Text = jobDetails.Qualifications;
                lblRequiredSkills.Text = jobDetails.RequiredSkills;
                lblContactEmail.Text = jobDetails.ContactEmail;
                lblApplicationStarts.Text=jobDetails.ApplicationStartDate.ToString();
                lblExperience.Text= jobDetails.Experience;
                //lnkWebsite.HRef = jobDetails.Website;
                lnkWebsite.Text = jobDetails.Website!=null ? jobDetails.Website: null;               
            }
            else
            {
                Session["StatusMessage1"] = "The Job has expired...!!!!!";
                Response.Redirect("StatusPage.aspx");
               // Response.Redirect("StatusPage.aspx?returnUrl=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
            }
        }
        private List<int> GetAppliedJobIDs()
        {
            int candidateId = (int)Session["CandidateID"];
            var applied = _dataAccess.GetAppliedJobs(candidateId);
            return applied; // Example applied job IDs
        }
        //protected void btnApply_Click(object sender, EventArgs e)
        //{
        //    int candidateId = 0;

        //    if (Session["CandidateID"] != null)
        //    {
        //        candidateId = (int)Session["CandidateID"];

        //        var job = new JobApplication();
               
        //        if (Request.QueryString["JobID"] != null)
        //        {
        //            string jobIdString = Request.QueryString["JobID"];

        //            // Convert the string to an integer
        //            int jobId;
        //            if (int.TryParse(jobIdString, out jobId))
        //            {

        //                job.JobID = jobId;
        //            }
        //            List<int> appliedJobIDs = GetAppliedJobIDs();
        //            bool hasApplied = appliedJobIDs.Contains(jobId);

        //            if (hasApplied)
        //            {
        //                // Show a message box and redirect
        //                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
        //                    "alert('You have already applied for this job.'); window.location ='ViewAllJobs.aspx';", true);
        //            }
        //            else
        //            {
        //                job.CandidateID = candidateId;
        //                var apply = _dataAccess.JobApplication(job);
                        
        //                Session["StatusMessage"] = apply;
        //                Response.Redirect("StatusPage.aspx");
        //            }

        //        }
        //        else
        //        {
        //            Session["StatusMessage"] = "No JobId Found.....Please visit again!!!!";
        //            Response.Redirect("StatusPage.aspx");
        //        }
        //    }
        //    else
        //    {
        //        Response.Redirect("AdminLogin.aspx");
        //    }
        //}
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            int jobID = Convert.ToInt32(Request.QueryString["JobID"]);
            Session["JobID"]=jobID;
            Response.Redirect("EditJobs.aspx");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            //if (Request.UrlReferrer != null)
            //{
            //    // Redirect to the previous page
            //    Response.Redirect(Request.UrlReferrer.ToString());
            //}
            //else
            //{
                // If no referrer is available, you can redirect to a default page
                Response.Redirect("NewJobs.aspx");
           // }
        }

    }
}