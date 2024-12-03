using iTextSharp.text.pdf;
using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.ComponentModel.Design;
using JobPortalWebApplication.Models.Request;
using JobPortalWebApplication.Employer;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using iTextSharp.text.io;
using System.Diagnostics;
using Org.BouncyCastle.Asn1.Ocsp;
using JobPortalWebApplication.Models;

namespace JobPortalWebApplication.Admin
{
    [System.Web.Script.Services.ScriptService]
    public partial class NewJobs : System.Web.UI.Page
    {
        private DataAccess _dataAccess;
        public string AESKey;
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
        //public  static int adminID
        //{
        //    get
        //    {
        //        return ViewState["adminID"] != null ? (int)ViewState["adminID"] : 1;
        //    }
        //    set
        //    {
        //        ViewState["adminID"] = value;
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            AESKey = Global._AESKey;
            if (Session["AdminID"] != null)
            {

                int EmployeeId = Convert.ToInt32(Session["AdminID"]);
                if (!IsPostBack)
                {
                    BindCompanyName();

                    //hfAntiForgeryToken.Value = GenerateAntiForgeryToken();

                    if (Request.QueryString["page"] != null)
                    {
                        currentPage = int.Parse(Request.QueryString["page"]);
                    }
                    else
                    {
                        currentPage = 1;
                    }
                    if (Request.QueryString["deleteID"] != null)
                    {
                        int JobID;
                        if (int.TryParse(Request.QueryString["deleteID"], out JobID))
                        {
                            DeleteJob(JobID);
                            BindJobsGrid();
                        }
                    }

                    if (Request.QueryString["JobID"] != null && Request.QueryString["Action"] != null)
                    {
                        int JobID;
                        if (int.TryParse(Request.QueryString["JobID"], out JobID))
                        {
                            // string JobID = Request.QueryString["JobID"];
                            string action = Request.QueryString["Action"];

                           // var result=UpdateJobStatus(JobID, action);
                            //if (result)
                            //{
                            //    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Job Approved successfully' );", true);
                            //    BindJobsGrid();
                            //}
                            //else
                            //    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Job status failed' );", true);

                            BindJobsGrid();
                        }
                    }
                    
                }
                BindJobsGrid();
            }
            else
            {
                Response.Redirect("AdminLogin.aspx");
            }
        }
       

        private void BindCompanyName()
        {
            var company = _dataAccess.GetAllCompany();

            //foreach (var companyItem in company)
            //{
            //    ddlCompanyName.Items.Add(new ListItem(companyItem.CompanyName, companyItem.CompanyID.ToString()));
            //   // ddlCompanyName.InnerHtml += $"<option value='{companyItem.CompanyID}'>{companyItem.CompanyName}</option>";
            //}

            ddlCompanyName.DataSource = company;
            ddlCompanyName.DataTextField = "CompanyName";
            ddlCompanyName.DataValueField = "CompanyID";
            ddlCompanyName.DataBind();
            ddlCompanyName.Items.Insert(0, new ListItem("Search By Company Name", ""));
        }
        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    if (txtSearch.Text != null && txtSearch.Text !="")
        //    {
        //        string searchText = txtSearch.Text.Trim();
        //        currentPage = 1;


        //        var jobs = _dataAccess.SearchJobsbyKeyword(searchText);

        //        if (jobs != null && jobs.Count > 0)
        //        {
        //            lblMessage.Text = "";
        //            pds.DataSource = jobs;
        //            pds.AllowPaging = true;
        //            pds.PageSize = pageSize;
        //            pds.CurrentPageIndex = currentPage - 1; // Pages are zero-based


        //            rptJobs.DataSource = pds;
        //            rptJobs.DataBind();

        //            lnkPrevious.Enabled = !pds.IsFirstPage;
        //            lnkNext.Enabled = !pds.IsLastPage;

        //            lblPageInfo.Text = $"Page {currentPage} of {pds.PageCount}";
        //            lblMessage.Text = "";
        //            // Call the LoadEmployeeList with search results and manage pagination
        //            //LoadEmployeeList(employees);
        //        }
        //        else
        //        {
        //            rptJobs.DataSource = null; // Clear the GridView if no results found
        //            rptJobs.DataBind();
        //            lblMessage.Text = "No Jobs found matching the search criteria.";
        //        }
        //    }

        //    else if (ddlCompanyName.SelectedValue != null && ddlCompanyName.SelectedValue != "")
        //    {
        //        currentPage = 1;
        //        int? companyId = !string.IsNullOrEmpty(ddlCompanyName.SelectedValue) ? (int?)Convert.ToInt32(ddlCompanyName.SelectedValue) : null;


        //        if (companyId.HasValue)
        //        {
        //            ViewState["CompanyId"] = companyId.Value;
        //        }
        //        else
        //        {
        //            ViewState["CompanyId"] = null;
        //        }
        //        // Call a method to search for employees based on the search text
        //        var employees = _dataAccess.GetJobs(companyId);
        //        pds.DataSource = employees;
        //        pds.AllowPaging = true;
        //        pds.PageSize = pageSize;
        //        pds.CurrentPageIndex = currentPage - 1; // Pages are zero-based


        //        rptJobs.DataSource = pds;
        //        rptJobs.DataBind();

        //        lnkPrevious.Enabled = !pds.IsFirstPage;
        //        lnkNext.Enabled = !pds.IsLastPage;

        //        lblPageInfo.Text = $"Page {currentPage} of {pds.PageCount}";
        //        lblMessage.Text = "";
        //    }
        //    else
        //    {
        //        BindJobsGrid();
        //        //rptJobs.DataSource = null; // Clear the GridView if no results found
        //        //rptJobs.DataBind();
        //        //lblMessage.Text = "No Jobs found matching the search criteria.";
        //    }

        //}


        private List<EmployerResponse> SearchEmployees(string searchText)
        {
            List<EmployerResponse> employerResponses = new List<EmployerResponse>();
            //   employerResponses = _dataAccess.SearchEmployees(searchText);
            return employerResponses;
        }
        public void DeleteJob(int JobID)
        {
            var status = _dataAccess.DeleteJobPosting(JobID);
            if (status)
                // Return a success or error message
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Job deleted successfully' );", true);
            else
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Error occurred while deleting Job');", true);

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPage = 1; // Reset to the first page on new search

            // Get search parameters
            string searchText = txtSearch.Text.Trim();
            int? companyId = !string.IsNullOrEmpty(ddlCompanyName.SelectedValue) ? (int?)Convert.ToInt32(ddlCompanyName.SelectedValue) : null;

            // Store company ID in ViewState to retain selection during pagination
            if (companyId.HasValue)
            {
                ViewState["CompanyId"] = companyId.Value;
            }
            else
            {
                ViewState["CompanyId"] = null;
            }

            // Bind data based on search text or company selection
            BindJobsGrid(searchText, companyId);
        }

        protected void BindJobsGrid(string searchText = "", int? companyId = null)
        {
            // Initialize paged data source
            pds = new PagedDataSource();
            pds.AllowPaging = true;
            pds.PageSize = pageSize;
            pds.CurrentPageIndex = currentPage - 1;

            List<ViewJobs> jobs;

            if (!string.IsNullOrEmpty(searchText))
            {
                jobs = _dataAccess.SearchJobsbyKeyword(searchText);
            }
            else if (companyId.HasValue)
            {
                jobs = _dataAccess.GetJobs(companyId.Value);
            }
            else
            {
                jobs = _dataAccess.GetAllJobsForAdmin();
            }

            if (jobs != null && jobs.Count > 0)
            {
                pds.DataSource = jobs;
                rptJobs.DataSource = pds;
                rptJobs.DataBind();

                lnkPrevious.Enabled = !pds.IsFirstPage;
                lnkNext.Enabled = !pds.IsLastPage;
                lblPageInfo.Text = $"Page {currentPage} of {pds.PageCount}";
                lblMessage.Text = "";
            }
            else
            {
                rptJobs.DataSource = null;
                rptJobs.DataBind();
                lblMessage.Text = "No jobs found matching the search criteria.";
            }
        }

        // Pagination event handlers
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            currentPage++;
            ApplyPagination();
        }

        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            currentPage--;
            ApplyPagination();
        }

        private void ApplyPagination()
        {
            // Retain search text and company selection on pagination
            string searchText = txtSearch.Text.Trim();
            int? companyId = ViewState["CompanyId"] != null ? (int)ViewState["CompanyId"] : (int?)null;

            // Bind the data based on the current parameters
            BindJobsGrid(searchText, companyId);
        }

        //public void BindJobsGrid()
        //{

        //    var jobs = _dataAccess.GetAllJobsForAdmin();
        //    pds.DataSource = jobs;
        //    pds.AllowPaging = true;
        //    pds.PageSize = pageSize;
        //    pds.CurrentPageIndex = currentPage - 1; // Pages are zero-based


        //    rptJobs.DataSource = pds;
        //    rptJobs.DataBind();

        //    lnkPrevious.Enabled = !pds.IsFirstPage;
        //    lnkNext.Enabled = !pds.IsLastPage;

        //    lblPageInfo.Text = $"Page {currentPage} of {pds.PageCount}";
        //    //rptJobs.DataSource = jobs;
        //    //rptJobs.DataBind();
        //}
        public string Encrypt(string plainText)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(AESKey); // Use a proper key here
                aes.IV = new byte[16]; // Initialization vector, can be randomized for added security

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }
        //protected void gvJobs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    rptJobs.PageIndex = e.NewPageIndex;
        //    BindJobsGrid();
        //}
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            Button btnApprove = (Button)sender;
            int jobId = Convert.ToInt32(btnApprove.CommandArgument);
            string status = _dataAccess.GetApprovalStatus(jobId); // Function to get current approval status

            if (status == "APPROVED")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This job has already been approved.');", true);
                BindJobsGrid();
            }
            else
            {
                var statusmsg = _dataAccess.ApproveJobPosting(jobId);
                BindJobsGrid();
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('{statusmsg}'); window.location='NewJobs.aspx'", true);





            }
        }
        public string GenerateAntiForgeryToken()
        {
            // Generate a token and store it in session or cookies
            string token = Guid.NewGuid().ToString();
            Session["AntiForgeryToken"] = token; // Store it in session or cookies
            return token; // Return it to the form field
        }

        //public bool UpdateJobStatus(int id, string status)
        //{
        //    if (HttpContext.Current.Session["AdminID"] == null)
        //    {
        //        HttpContext.Current.Response.Redirect("Login.aspx"); // Redirect to login if session is invalid
        //    }
        //    //if (HttpContext.Current.Session["AntiForgeryToken"] == null)
        //    //{
        //    //    return new
        //    //    {
        //    //        success = false,
        //    //        message = "Session token is missing."
        //    //    };
        //    //}

        //    //// Validate anti-forgery token
        //    //if (HttpContext.Current.Session["AntiForgeryToken"].ToString() != RequestVerificationToken)
        //    //{
        //    //    return new
        //    //    {
        //    //        success = false,
        //    //        message = "Anti-forgery token validation failed."
        //    //    };
        //    //}
        //    bool success=true;
        //    //// Proceed with your logic if the token is valid
        //    //string connStr = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
        //    //var dataAccess = new DataAccess(connStr);
        //    string update = _dataAccess.UpdateJobStatus(id, status);
        //    if (update == "Success")
        //    {
        //        success = true;
        //        return success;
        //    }
        //    else success = false;
        //    return success ;
        //    //var response = new { success = success };
        //    //return new JavaScriptSerializer().Serialize(response);
        //    //return new
        //    //{
        //    //    success = update,
        //    //    message = update != "Success" ? "Success" : "Failed"
        //    //};
        //}


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string UpdateJobStatus(int id, string status)
        {
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
                var dataAccess = new DataAccess(connStr);
                System.Diagnostics.Debug.WriteLine($"JobId: {id}, Action: {status}");

                string update = dataAccess.UpdateJobStatus(id, status);

                // Return a JSON-friendly response
                return new JavaScriptSerializer().Serialize(new { success = true, message = "Operation completed successfully." });
            }
            catch (Exception ex)
            {
                // Log or handle exception as needed
                return new JavaScriptSerializer().Serialize(new { success = false, message = ex.Message });
            }
        }
        //return new
        //{
        //    success = update,
        //    message = update!="Success" ? "Success" : "Failed"
        //};

        //public void UpdateApplicationStatus(int id, string status)
        //{
        //    //if (HttpContext.Current.Session["AntiForgeryToken"] == null)
        //    //{
        //    //    return new
        //    //    {
        //    //        success = false,
        //    //        message = "Session token is missing."
        //    //    };
        //    //}

        //    //// Validate anti-forgery token
        //    //if (HttpContext.Current.Session["AntiForgeryToken"].ToString() != RequestVerificationToken)
        //    //{
        //    //    return new
        //    //    {
        //    //        success = false,
        //    //        message = "Anti-forgery token validation failed."
        //    //    };
        //    //}

        //    // Proceed with your logic if the token is valid
        //    //string connStr = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
        //    //var dataAccess = new DataAccess(connStr);
        //    string update = _dataAccess.UpdateJobStatus(id, status);


        //    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('{update}'); window.location='NewJobs.aspx'", true);


        //    //return new
        //    //{
        //    //    success = update,
        //    //    message = update ? "Success" : "Failed"
        //    //};
        //}
        [WebMethod]
        public static string GetData()
        {
            return "Hello";
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            Button btnReject = (Button)sender;
            int jobId = Convert.ToInt32(btnReject.CommandArgument);

            var status = _dataAccess.RejectJobPosting(jobId);
            ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('{status}'); window.location='NewJobs.aspx'", true);

            BindJobsGrid();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btnDelete = (Button)sender;
            int jobId = Convert.ToInt32(btnDelete.CommandArgument);

            var status = _dataAccess.DeleteJobPosting(jobId);
            ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('{status}'); window.location='NewJobs.aspx'", true);

            BindJobsGrid();
        }
        //protected void lnkPrevious_Click(object sender, EventArgs e)
        //{
        //    currentPage = Request.QueryString["page"] != null ? int.Parse(Request.QueryString["page"]) : 1;
        //    currentPage -= 1;
        //    Response.Redirect("NewJobs.aspx?page=" + currentPage);
        //}

        //// Event handler for the "Next" button click
        //protected void lnkNext_Click(object sender, EventArgs e)
        //{
        //    currentPage = Request.QueryString["page"] != null ? int.Parse(Request.QueryString["page"]) : 1;
        //    currentPage += 1;

        //    Response.Redirect("NewJobs.aspx?page=" + currentPage);
        //}


    }
}