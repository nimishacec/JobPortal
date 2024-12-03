using JobPortalWebApplication.DataBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using JobPortalWebApplication.Models.Response;
using JobPortalWebApplication.Candidate;
using iTextSharp.text.pdf;
using System.Security.Cryptography;
using System.Text;

namespace JobPortalWebApplication.Admin
{
    public partial class CandidateList : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            AESKey = Global._AESKey;
            if (Session["AdminID"] != null)
            {
                //int AdminID = (int)Session["AdminID"];
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
                    if (Request.QueryString["deleteID"] != null)
                    {
                        int candidateID;
                        if (int.TryParse(Request.QueryString["deleteID"], out candidateID))
                        {
                            DeleteCandidate(candidateID);
                            LoadCandidateList();
                        }
                    }
                    LoadCandidateList();
                }
            }
            else
            {
                Response.Redirect("AdminLogin.aspx");
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteCandidate")
            {               
                int candidateID = Convert.ToInt32(e.CommandArgument);              
                DeleteCandidate(candidateID);
                
                LoadCandidateList();
            }
        }
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            currentPage = 1;
            // Call a method to search for employees based on the search text
            var candidates = SearchCandidates(searchText);

            if (candidates != null && candidates.Count > 0)
            {
                lblMessage.Text = "";
                BindCandidatesToGrid(candidates);
                // Call the LoadEmployeeList with search results and manage pagination
                //LoadEmployeeList(employees);
            }
            else
            {
                gvCandidates.DataSource = null; // Clear the GridView if no results found
                gvCandidates.DataBind();
                lblMessage.Text = "No candidates found matching the search criteria.";
            }

        }
        private void BindCandidatesToGrid(List<CandidateDetails> candidates)
        {
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = candidates; // Use the filtered candidates, not the full list
            pds.AllowPaging = true;
            pds.PageSize = pageSize;
            pds.CurrentPageIndex = currentPage - 1; // Pages are zero-based

            // Bind data to the GridView
            gvCandidates.DataSource = pds;
            gvCandidates.DataBind();

            // Update pagination controls
            lnkPrevious.Enabled = !pds.IsFirstPage;
            lnkNext.Enabled = !pds.IsLastPage;

            lblPageInfo.Text = $"Page {currentPage} of {pds.PageCount}";
        }
        public void DeleteCandidate(int candidateID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

            // Create an instance of DataAccess and pass the connection string
            var _dataAccess = new DataAccess(connectionString);

            // Call the DeleteEmployee method
            var status = _dataAccess.DeleteCandidate(candidateID);
            if (status)
                // Return a success or error message
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Candidate deleted successfully' );", true);
            else
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Error occurred while deleting employee');", true);

        }
        //private void DeleteCandidateFromDatabase(int candidateID)
        //{
        //    var status = _dataAccess.DeleteCandidate(candidateID);
        //    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('{status}'); window.location='CandidateList.aspx'", true);

        //}

        private void LoadCandidateList()
        {
            var candidateList = _dataAccess.GetCandidates();
            BindCandidatesToGrid(candidateList);
            //pds.DataSource = candidateList;
            //pds.AllowPaging = true;
            //pds.PageSize = pageSize;
            //pds.CurrentPageIndex = currentPage - 1; // Pages are zero-based


            //gvCandidates.DataSource = pds;
            //gvCandidates.DataBind();

            //lnkPrevious.Enabled = !pds.IsFirstPage;
            //lnkNext.Enabled = !pds.IsLastPage;

            //lblPageInfo.Text = $"Page {currentPage} of {pds.PageCount}";
            //if (currentPage < 1 || currentPage > pds.PageCount)
            //{
            //    currentPage = 1; // Reset to the first page if out of range
            //    Response.Redirect("CandidateList.aspx?page=1");
            //}

        }
        private List<CandidateDetails> SearchCandidates(string searchText)
        {
            List<CandidateDetails> candidateResponses = new List<CandidateDetails>();
            candidateResponses = _dataAccess.SearchCandidates(searchText);
            return candidateResponses;
        }
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            currentPage = Request.QueryString["page"] != null ? int.Parse(Request.QueryString["page"]) : 1;
            currentPage -= 1;
            Response.Redirect("CandidateList.aspx?page=" + currentPage);
        }

        // Event handler for the "Next" button click
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            currentPage = Request.QueryString["page"] != null ? int.Parse(Request.QueryString["page"]) : 1;
            currentPage += 1;
            Response.Redirect("CandidateList.aspx?page=" + currentPage);
        }
        //protected void gvCandidates_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvCandidates.PageIndex = e.NewPageIndex;
        //    LoadCandidateList();
        //}
    }
}