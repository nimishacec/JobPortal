using JobPortalWebApplication.Candidate;
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
using static System.Net.Mime.MediaTypeNames;

namespace JobPortalWebApplication.Admin
{
    public partial class ApplicationApproval : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        PagedDataSource pds = new PagedDataSource();
        //  protected int currentPage;
        protected int pageSize = 5;
        public int currentPage
        {
            get
            {
                return ViewState["currentPage"] != null ? (int)ViewState["currentPage"] : 1;
            }
            set
            {
                ViewState["currentPage"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            int AdminId = 0;

            if (Session["AdminID"] != null)
            {
                AdminId = (int)Session["AdminID"];
                if (!IsPostBack)
                {
                    if (Request.QueryString["page"] != null)
                    {
                        currentPage = int.Parse(Request.QueryString["page"]);
                    }
                    else
                    {
                        currentPage = 1;
                    }
                    if (Request.QueryString["ApplicationCode"] != null && Request.QueryString["Action"] != null)
                    {
                        //int JobID;
                        //if (int.TryParse(Request.QueryString["JobID"], out JobID))
                        //{
                        string ApplicationCode = Request.QueryString["ApplicationCode"];
                        string action = Request.QueryString["Action"];

                        UpdateApplicationStatus(ApplicationCode, action);

                        LoadApplications();
                        // }
                    }
                }

                    LoadApplications();

                

            }
            else
            {
                Response.Redirect("AdminLogin.aspx");
            }
        }
        public void UpdateApplicationStatus(string AppCode, string status)
        {
           
            string update = _dataAccess.UpdateApplicationStatus(AppCode, status);


            ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('{update}'); window.location='ApplicationApproval.aspx'", true);


          
        }
        protected void btnDisable_Click(object sender, EventArgs e)
        {
            LinkButton btnReject = (LinkButton)sender;
            string applicationcodeId =(btnReject.CommandArgument);

            var status = _dataAccess.DisableJobApplication(applicationcodeId);
            ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('{status}'); window.location='ApplicationApproval.aspx'", true);

            LoadApplications();
        }
        private void LoadApplications()
        {
            var applications = _dataAccess.GetJobApplicationFromDB();
            if (applications.Count() > 0)
            {
                pds.DataSource = applications;
                pds.AllowPaging = true;
                pds.PageSize = pageSize;
                pds.CurrentPageIndex = currentPage - 1; // Pages are zero-based


                rptJobs.DataSource = pds;
                rptJobs.DataBind();

                lnkPrevious.Enabled = !pds.IsFirstPage;
                lnkNext.Enabled = !pds.IsLastPage;

                lblPageInfo.Text = $"Page {currentPage} of {pds.PageCount}";
                //rptJobs.DataSource = applications;
                //rptJobs.DataBind();
                //  BindVerticalData(applications);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('No Records Found.....Please visit again!!!!' );", true);

                //Session["StatusMessage"] = "No Records Found.....Please visit again!!!!";
                //Response.Redirect("StatusPage.aspx");
            }
        }
        protected void gvJobResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        
        private void BindVerticalData(List<JobApplicationResult> jobApplResults)
        {
           
            DataTable dtJobDetails = new DataTable();
            dtJobDetails.Columns.Add("FieldName");
            dtJobDetails.Columns.Add("FieldValue");
           
            foreach (JobApplicationResult jobApplResult in jobApplResults)
            {               
                // Populate the DataTable with your actual job data
                dtJobDetails.Rows.Add("ApplicationID", jobApplResult.ApplicationCode);
                dtJobDetails.Rows.Add("Job ID", jobApplResult.JobID);
                dtJobDetails.Rows.Add("Job Title", jobApplResult.JobTitle);
                dtJobDetails.Rows.Add("Candidate ID", jobApplResult.CandidateID);
                dtJobDetails.Rows.Add("Candidate Email Address", jobApplResult.CandidateEmailAddress);
                dtJobDetails.Rows.Add("Highest Education Level", jobApplResult.HighestEducationLevel);
                dtJobDetails.Rows.Add("Company Name", jobApplResult.CompanyName);
                dtJobDetails.Rows.Add("Application Date", jobApplResult.ApplicationDate);
                dtJobDetails.Rows.Add("JobApplication LastDate", jobApplResult.JobApplicationLastDate);
                dtJobDetails.Rows.Add("Application Status", jobApplResult.ApplicationStatus);       

            }

            rptJobs.DataSource = dtJobDetails;
            rptJobs.DataBind();

        }
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            currentPage = Request.QueryString["page"] != null ? int.Parse(Request.QueryString["page"]) : 1;
            currentPage -= 1;
            Response.Redirect("ApplicationApproval.aspx?page=" + currentPage);
        }

        // Event handler for the "Next" button click
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            currentPage = Request.QueryString["page"] != null ? int.Parse(Request.QueryString["page"]) : 1;
            currentPage += 1;
            Response.Redirect("ApplicationApproval.aspx?page=" + currentPage);
        }



    }
}