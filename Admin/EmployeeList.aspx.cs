using iTextSharp.text;
using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Employer;
using JobPortalWebApplication.Models.Response;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Admin
{
    public partial class EmployeeList : System.Web.UI.Page
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
            Log.Information("EmployeesList loaded successfully-page load");
            if (Session["AdminID"] != null)
            {
                Log.Information("EmployeesList loaded successfully- session");
                int AdminID = (int)Session["AdminID"];
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
                        int employeeID = int.Parse(Request.QueryString["deleteID"]);
                        if (employeeID!=0)
                        {
                            Log.Information("EmployeeId parameter received", +employeeID);
                            DeleteEmployee(employeeID);
                            Log.Information("Employee Deleted",+employeeID);
                            LoadEmployeeList();
                            Log.Information("EmployeesList loaded successfully");
                        }
                    }
                    Log.Information("EmployeesList loaded without deleteid ");
                    LoadEmployeeList(null);
                }
            }
            else
            {
                Log.Information("Redirected to AdminLogin");
                Response.Redirect("AdminLogin.aspx");
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            currentPage = 1;
            // Call a method to search for employees based on the search text
            var employees = SearchEmployees(searchText);

            if (employees != null && employees.Count > 0)
            {
                lblMessage.Text = "";
                rptJobs.DataSource = employees; // Clear the GridView if no results found
                rptJobs.DataBind();
                // Call the LoadEmployeeList with search results and manage pagination
                //LoadEmployeeList(employees);
            }
            else
            {
                rptJobs.DataSource = null; // Clear the GridView if no results found
                rptJobs.DataBind();
                lblMessage.Text = "No employees found matching the search criteria.";
            }
           
        }

        private List<EmployerResponse> SearchEmployees(string searchText)
        {
            List<EmployerResponse> employerResponses = new List<EmployerResponse>();
            employerResponses = _dataAccess.SearchEmployees(searchText);
            return employerResponses;
        }

        protected void gvEmployees_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                // Add thead tag for the header row
                e.Row.TableSection = TableRowSection.TableHeader;
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Add tbody tag for the data rows
                e.Row.TableSection = TableRowSection.TableBody;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteEmployee")
            {
               
                int employeeID = Convert.ToInt32(e.CommandArgument);                
               // DeleteEmployeeFromDatabase(employeeID);               
               // LoadEmployeeList();
            }
        }

       
        public void DeleteEmployee(int employeeID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

            // Create an instance of DataAccess and pass the connection string
            var _dataAccess = new DataAccess(connectionString);

            // Call the DeleteEmployee method
            var status = _dataAccess.DeleteEmployee(employeeID);
            if (status)
                // Return a success or error message
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Employee deleted successfully' );", true);
            else
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Error occurred while deleting employee');", true); 

        }

        //protected void gvEmployees_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvEmployees.PageIndex = e.NewPageIndex;

        //    LoadEmployeeList();
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

        protected void ViewDetails_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string employeeId = btn.CommandArgument;
            string encryptedEmployeeId = Encrypt(employeeId);

            // Append the encrypted EmployeeID to the query string
            Response.Redirect("EmployeeDetails.aspx?empId=" + HttpUtility.UrlEncode(encryptedEmployeeId));

            // Redirect to the EmployerViewDashboard with the EmployeeID as a query string
            Response.Redirect($"~/Admin/EmployerViewDashboard.aspx?EmployeeID=" + HttpUtility.UrlEncode(encryptedEmployeeId));
        }

        private void LoadEmployeeList(List<EmployerResponse> employees = null)
        {
            Log.Information("EmployeesList loaded successfully on method call");
            var empList = _dataAccess.GetEmployees(); // Assuming this method gets the list from the database
            int totalRecords = empList.Count; // Update the total record count
            pds.DataSource = empList;
            pds.AllowPaging = true;
            pds.PageSize = pageSize;
            pds.CurrentPageIndex = currentPage - 1; // Pages are zero-based

          
            rptJobs.DataSource = pds;
            rptJobs.DataBind();           

            lnkPrevious.Enabled = !pds.IsFirstPage;
            lnkNext.Enabled = !pds.IsLastPage;          

            lblPageInfo.Text = $"Page {currentPage} of {pds.PageCount}";
        }
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            currentPage = Request.QueryString["page"] != null ? int.Parse(Request.QueryString["page"]) : 1;
            currentPage -= 1;
            Response.Redirect("EmployeeList.aspx?page=" + currentPage);
        }

        // Event handler for the "Next" button click
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            currentPage = Request.QueryString["page"] != null ? int.Parse(Request.QueryString["page"]) : 1;          
            currentPage += 1;
            Response.Redirect("EmployeeList.aspx?page=" + currentPage);
        }
        //protected void DataPager1_PreRender(object sender, EventArgs e)
        //{
        //    LoadEmployeeList();
        //}
    }
}