using JobPortalWebApplication.Candidate;
using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Request;
using JobPortalWebApplication.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace JobPortalWebApplication.Employer
{
    public partial class ViewApplications : System.Web.UI.Page
    {
        private DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            if (Session["EmployeeID"] != null)
            {
                int employeeId = Convert.ToInt32(Session["EmployeeID"]);

                if (!IsPostBack)
                {
                    //string appstatus = _dataAccess.GetApplicationStatus(ApplicationID);
                    //if (resumeStatus == "ACCEPTED")
                    //{
                    //    btnAcceptResume.Visible = false; // Hide the Accept button if already accepted
                    //    btnRejectResume.Visible = true;  // Show the Reject button
                    //}
                    //else
                    //{
                    //    // If the resume is in pending status or not reviewed
                    //    btnAcceptResume.Visible = true;
                    //    btnRejectResume.Visible = true;
                    //}
                   

                    LoadAppliedJobs(employeeId);
                }
            }
            else
            {
                Response.Redirect("EmployerLogin.aspx");
            }

        }
        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            int employeeId = Convert.ToInt32(Session["EmployeeID"]);
            Button btn = (Button)sender;
            string[] commandArgs = btn.CommandArgument.Split(',');
            //if (e.CommandName == "SendMessage")
            //{
            // Extract CandidateID and JobID from CommandArgument
           // string[] commandArgs = e.CommandArgument.ToString().Split(',');
                int candidateId = Convert.ToInt32(commandArgs[0]);
                int jobId = Convert.ToInt32(commandArgs[1]);
            Session["EmployeeID"] = employeeId;
                // Now, you can use the CandidateID and JobID as needed.
                // For example, redirect to a SendMessage page or load message modal.
                Response.Redirect($"~/Employer/MessageDashboard.aspx?CandidateID={candidateId}&JobID={jobId}");
            
        }

        protected void rptAppliedJobs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Retrieve the ApplicationStatus value
                string applicationStatus = DataBinder.Eval(e.Item.DataItem, "ApplicationStatus").ToString();

                // Find the Approve and Reject buttons
                Button btnApprove = (Button)e.Item.FindControl("btnApprove");
                Button btnReject = (Button)e.Item.FindControl("btnReject");

                // Check the ApplicationStatus and control the visibility of the buttons
                if (applicationStatus == "APPROVED")
                {
                    btnApprove.Visible = false;  // Hide Approve button if already approved
                }
                else if (applicationStatus == "REJECTED")
                {
                    btnReject.Visible = false;  // Hide Reject button if already rejected
                }
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] args = btn.CommandArgument.Split(',');

            string ApplicationID = (args[0]);
            int JobID = Convert.ToInt32(args[1]);
            if (ApplicationID != null && JobID != 0)
            {
                string appstatus = _dataAccess.GetApplicationStatus(ApplicationID); // Function to get current approval status

                if (appstatus == "APPROVED")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This job has already been approved.'); window.location='ViewApplications.aspx'", true);
                }
                else
                {
                    var status = _dataAccess.VerifyJobApplication(ApplicationID, JobID, "APPROVED", "EMPLOYEE");
                    if (status == "Success")
                    {

                        ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('The Job Application request has been Approved by Admin.');window.location='ViewApplications.aspx'", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('{status}'); window.location='ViewApplications.aspx'", true);                     
                    }
                }
            }
            else
                Response.Redirect("ViewApplications.aspx");
          
        }
        protected void btnReject_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] args = btn.CommandArgument.Split(',');

            string ApplicationID = (args[0]);
            int JobID = Convert.ToInt32(args[1]);
            if (ApplicationID != null && JobID != 0)
            {
                string appstatus = _dataAccess.GetApplicationStatus(ApplicationID); // Function to get current approval status

                if (appstatus == "REJECTED")
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This job has already been Rejected.'); window.location='ViewApplications.aspx'", true);
                }
                else
                {

                    var status = _dataAccess.VerifyJobApplication(ApplicationID, JobID, "REJECTED", "EMPLOYEE");
                    if (status == "Success")
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('The Job Application request has been Rejected by Admin'); window.location='ViewApplications.aspx'", true);
                      
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('{status}'); window.location='ViewApplications.aspx'", true);                    }
                }
            }
            else
                Response.Redirect("JobEditApproval.aspx");
        }

        private void LoadAppliedJobs(int employeeId)
        {

            List<JobApplicationResult> appliedJobs = _dataAccess.GetAllAppliedJobsForEmployer(employeeId);

            if (appliedJobs != null && appliedJobs.Count > 0)
            {
                rptAppliedJobs.DataSource = appliedJobs;
                rptAppliedJobs.DataBind();
            }
            else
            {
                // Handle case where no jobs have been applied to
                lblNoJobs.Visible = true;
            }
        }
    }
}