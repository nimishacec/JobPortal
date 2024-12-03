using iTextSharp.text.pdf;
using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Response;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Admin
{
    public partial class TrainersList : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        public string AESKey;
        PagedDataSource pds = new PagedDataSource();
        protected int currentPage;
        protected int pageSize = 5;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            AESKey=Global._AESKey;
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
                            DeleteTrainer(candidateID);
                            LoadTrainerList();
                        }
                    }
                    Log.Information("LoadTrainerProfile");
                    LoadTrainerList();
                }
            }
            else
            {
                Response.Redirect("AdminLogin.aspx");
            }
        }
        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "DeleteTrainer")
        //    {

        //        int trainerID = Convert.ToInt32(e.CommandArgument);
        //        DeleteEmployeeFromDatabase(trainerID);
        //        LoadTrainerList();
        //    }
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            currentPage = 1;
            // Call a method to search for employees based on the search text
            var trainers = SearchTrainers(searchText);

            if (trainers != null && trainers.Count > 0)
            {
                lblMessage.Text = "";
                BindCandidatesToGrid(trainers);
                // Call the LoadEmployeeList with search results and manage pagination
                //LoadEmployeeList(employees);
            }
            else
            {
                gvTrainers.DataSource = null; // Clear the GridView if no results found
                gvTrainers.DataBind();
                lblMessage.Text = "No candidates found matching the search criteria.";
            }

        }
        private List<TrainerResponse> SearchTrainers(string searchText)
        {
            List<TrainerResponse> candidateResponses = new List<TrainerResponse>();
            candidateResponses = _dataAccess.SearchTrainers(searchText);
            return candidateResponses;
        }
        private void BindCandidatesToGrid(List<TrainerResponse> trainers)
        {
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = trainers; // Use the filtered candidates, not the full list
            pds.AllowPaging = true;
            pds.PageSize = pageSize;
            pds.CurrentPageIndex = currentPage - 1; // Pages are zero-based

            // Bind data to the GridView
            gvTrainers.DataSource = pds;
            gvTrainers.DataBind();

            // Update pagination controls
            lnkPrevious.Enabled = !pds.IsFirstPage;
            lnkNext.Enabled = !pds.IsLastPage;

            lblPageInfo.Text = $"Page {currentPage} of {pds.PageCount}";
        }
        private void DeleteTrainer(int trainerID)
        {
            var status = _dataAccess.DeleteTrainer(trainerID);
            ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('{status}'); window.location='TrainersList.aspx'", true);

        }
        public void DeleteCandidate(int candidateID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

            // Create an instance of DataAccess and pass the connection string
            var _dataAccess = new DataAccess(connectionString);

            // Call the DeleteEmployee method
            var status = _dataAccess.DeleteTrainer(candidateID);
            if (status)
                // Return a success or error message
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Candidate deleted successfully' );", true);
            else
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Error occurred while deleting employee');", true);

        }

        //protected void gvTrainers_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvTrainers.PageIndex = e.NewPageIndex;

        //    LoadTrainerList();
        //}
        private void LoadTrainerList()
        {
            var trainerList = _dataAccess.GetTrainers();
            pds.DataSource = trainerList;
            pds.AllowPaging = true;
            pds.PageSize = pageSize;
            pds.CurrentPageIndex = currentPage - 1; // Pages are zero-based


            gvTrainers.DataSource = pds;
            gvTrainers.DataBind();

            lnkPrevious.Enabled = !pds.IsFirstPage;
            lnkNext.Enabled = !pds.IsLastPage;

            lblPageInfo.Text = $"Page {currentPage} of {pds.PageCount}";
           
        }
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            currentPage = Request.QueryString["page"] != null ? int.Parse(Request.QueryString["page"]) : 1;
            currentPage -= 1;
            Response.Redirect("TrainersList.aspx?page=" + currentPage);
        }

        // Event handler for the "Next" button click
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            currentPage = Request.QueryString["page"] != null ? int.Parse(Request.QueryString["page"]) : 1;
            currentPage += 1;
            Response.Redirect("TrainersList.aspx?page=" + currentPage);
        }
        protected void btnAddTrainer_Click(object sender, EventArgs e)
        {            
        //    Response.Redirect("AddTrainers.aspx");
        }
    }
}