using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Request;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Admin
{
    public partial class ReviewPage : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;

            if (Session["AdminID"] != null)
            {
                int EmployeeId,JobId, RequestId = 0;
                int AdminID = Convert.ToInt32(Session["AdminID"]);
                if (Session["RequestID"] != null && Session["EmployeeID"] != null && Session["JobID"]!=null)
                {
                    EmployeeId = Convert.ToInt32(Session["EmployeeID"]);
                    RequestId = Convert.ToInt32(Session["RequestID"]);
                    JobId = Convert.ToInt32(Session["JobID"]);
                    if (!IsPostBack)
                    {
                        BindJobPostings(EmployeeId, JobId);
                        BindJobEditRequests(EmployeeId, RequestId);
                    }
                    BindJobPostings(EmployeeId, JobId);
                    BindJobEditRequests(EmployeeId, RequestId);
                }
                else
                    Response.Redirect("AdminLogin.aspx");
            }
        }
        private void BindJobPostings( int EmployeeId,int JobId)
        {
            // Fetch job postings from the database
           var jobPostings = _dataAccess.GetJobPostings(EmployeeId,JobId);
            if (jobPostings != null)
            {
                BindVerticalJobData(jobPostings);

                //GridView1.DataSource = jobPostings;
                //GridView1.DataBind();
            }
            else
            {
                Session["StatusMessage"] = "No jobs found";
                Response.Redirect("StatusPage.aspx");
            }
        }
        private void BindVerticalJobData(Jobs jobPostings)
        {
            DataTable dtJobDetail = new DataTable();
            dtJobDetail.Columns.Add("FieldName");
            dtJobDetail.Columns.Add("FieldValue");

            dtJobDetail.Rows.Add("Employee ID", jobPostings.EmployeeID);
            dtJobDetail.Rows.Add("Job ID", jobPostings.JobID);
            dtJobDetail.Rows.Add("Job Title", jobPostings.JobTitle);
            dtJobDetail.Rows.Add("Experience", jobPostings.Experience);
            dtJobDetail.Rows.Add("JobDescription", jobPostings.JobDescription);
            dtJobDetail.Rows.Add("JobLocation", jobPostings.JobLocation);
            dtJobDetail.Rows.Add("Qualifications", jobPostings.Qualifications);
            dtJobDetail.Rows.Add("ApplicationDeadline", jobPostings.ApplicationDeadline);
            dtJobDetail.Rows.Add("RequiredSkills", jobPostings.RequiredSkills);
            GridView1.DataSource = dtJobDetail;
            GridView1.DataBind();
        }
        private void BindJobEditRequests(int EmployeeId, int RequestId)
        {
            // Fetch job edit requests from the database
            var jobEditRequests = _dataAccess.GetJobEditRequests(EmployeeId, RequestId);
            BindVerticalJobEditData(jobEditRequests);
           
        }
        private void BindVerticalJobEditData(JobList jobEditRequests)
        {
            DataTable dtJobDetail = new DataTable();
            dtJobDetail.Columns.Add("FieldName");
            dtJobDetail.Columns.Add("FieldValue");

            dtJobDetail.Rows.Add("Employee ID", jobEditRequests.EmployeeID);
            dtJobDetail.Rows.Add("Job ID", jobEditRequests.JobID);
            dtJobDetail.Rows.Add("Job Title", jobEditRequests.JobTitle);
            dtJobDetail.Rows.Add("JobDescription", jobEditRequests.JobDescription);

            dtJobDetail.Rows.Add("JobLocation", jobEditRequests.JobLocation);
            dtJobDetail.Rows.Add("Experience", jobEditRequests.Experience);
            dtJobDetail.Rows.Add("RequiredSkills", jobEditRequests.RequiredSkills);
      
            dtJobDetail.Rows.Add("Qualifications", jobEditRequests.Qualifications);
            dtJobDetail.Rows.Add("Application Last Date", jobEditRequests.ApplicationDeadline);
            dtJobDetail.Rows.Add("Edit Request Date", jobEditRequests.RequestDate);
            dtJobDetail.Rows.Add("RequestStatus", jobEditRequests.RequestStatus);
           
            GridView2.DataSource = dtJobDetail;
            GridView2.DataBind();
        }


        protected void btnApprove_Click(object sender, EventArgs e)
        {
            int EmployeeId, RequestId = 0;
            if (Session["RequestID"] != null && Session["EmployeeID"] != null)
            {
                EmployeeId = Convert.ToInt32(Session["EmployeeID"]);
                RequestId = Convert.ToInt32(Session["RequestID"]);

                var status = _dataAccess.VerifyRequests(RequestId, EmployeeId, "Approved");
                if (status == "Approved")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", "alert('Job Edit Request has been Approved.'); window.location.href='JobEditApproval.aspx';", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('{status}'); window.location.href='JobEditApproval.aspx';", true);
                }

            }
            else
                Response.Redirect("JobEditApproval.aspx");
        }
        protected void btnReject_Click(object sender, EventArgs e)
        {
            int EmployeeId, RequestId = 0;
            if (Session["RequestID"] != null && Session["EmployeeID"] != null)
            {
                EmployeeId = Convert.ToInt32(Session["EmployeeID"]);
                RequestId = Convert.ToInt32(Session["RequestID"]);
                var status = _dataAccess.VerifyRequests(RequestId, EmployeeId, "Rejected");
                if (status == "Rejected")
                {
                    Session["StatusMessage"] = "The Job update request has been Rejected";
                    Response.Redirect("StatusPage.aspx");
                }
                else
                {
                    Session["StatusMessage"] = status;
                    Response.Redirect("StatusPage.aspx");
                }
            }
            else
                Response.Redirect("JobEditApproval.aspx");
        }
    }
}