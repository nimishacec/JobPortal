using JobPortalWebApplication.DataBase;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Admin
{
    public partial class JobEditApproval : System.Web.UI.Page
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

            if (Session["AdminID"] != null)
            {
                int AdminID = Convert.ToInt32(Session["AdminID"]);

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
                        int RequestID;
                        if (int.TryParse(Request.QueryString["deleteID"], out RequestID))
                        {
                            DeleteJobRequest(RequestID);
                           // LoadJobs(AdminID);
                        }
                    }
                    LoadJobs(AdminID);
                }
             //   LoadJobs(AdminID);
            }

            else
            {
                Response.Redirect("AdminLogin.aspx");
            }
        }
        public void DeleteJobRequest(int JobID)
        {
            var status = _dataAccess.DeleteJobEditRequest(JobID);
            if (status)
                // Return a success or error message
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Job deleted successfully' );", true);
            else
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Error occurred while deleting Job');", true);

        }

        public void LoadJobs(int AdminID)
        {
            var jobs = _dataAccess.GetJobRequestFromDB();
            if (jobs.Count > 0)
            {
                pds.DataSource = jobs;
                pds.AllowPaging = true;
                pds.PageSize = pageSize;
                pds.CurrentPageIndex = currentPage - 1; // Pages are zero-based


                rptJobs.DataSource = pds;
                rptJobs.DataBind();

                lnkPrevious.Enabled = !pds.IsFirstPage;
                lnkNext.Enabled = !pds.IsLastPage;

                lblPageInfo.Text = $"Page {currentPage} of {pds.PageCount}";
                // Bind the data to the Repeater
                //rptJobs.DataSource = jobs;
                //rptJobs.DataBind();
            }
            else
            {
                // Handle case where no data is found
                rptJobs.Visible = false; // or display a message indicating no jobs
                //lblMessage.Text = "No edit requests found";
                //lblMessage.Visible = true;
            }
        }
        protected void GridView1_PageIndexChanging(object sender, EventArgs e)
        {

        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            currentPage = Request.QueryString["page"] != null ? int.Parse(Request.QueryString["page"]) : 1;
            currentPage -= 1;
            Response.Redirect("JobEditApproval.aspx?page=" + currentPage);
        }

        // Event handler for the "Next" button click
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            currentPage = Request.QueryString["page"] != null ? int.Parse(Request.QueryString["page"]) : 1;
            currentPage += 1;
            Response.Redirect("JobEditApproval.aspx?page=" + currentPage);
        }
        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Verify")
        //    {
        //        // Retrieve the index of the row that fired the event
        //        int rowIndex = Convert.ToInt32(e.CommandArgument);
        //        if (rowIndex >= 0 && rowIndex < GridView1.Rows.Count)
        //        {

        //            // Access the row data
        //            GridViewRow row = GridView1.Rows[rowIndex];

        //            // Retrieve JobID from the DataKeys collection
        //            int RequestId = Convert.ToInt32(GridView1.DataKeys[rowIndex]["RequestID"]);
        //            int EmployeeId = Convert.ToInt32(GridView1.DataKeys[rowIndex]["EmployeeID"]);
        //            int JobId = Convert.ToInt32(GridView1.DataKeys[rowIndex]["JobID"]);
        //            //int AdminID;
        //            if (Session["AdminID"] != null)
        //            {
        //                int AdminID = Convert.ToInt32(Session["AdminID"]);
        //                Session["AdminID"] = AdminID;
        //                // Set the session variable
        //                Session["RequestID"] = RequestId;
        //                Session["EmployeeID"] = EmployeeId;
        //                Session["JobID"] = JobId;

        //                Response.Redirect("ReviewPage.aspx");
        //            }
        //            //var status = _dataAccess.VerifyRequests(RequestId, EmployeeId);
        //            //if (status == "Approved")
        //            //{
        //            //    Response.Redirect("JobEditApproval.aspx");
        //            //}
        //            //else
        //            //    Response.Redirect("JobEditApproval.aspx");
        //        }
        //    }
        //}

    }

}
