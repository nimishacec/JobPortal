using JobPortalWebApplication.DataBase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Employer
{
    public partial class ViewAllJobs : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            //  _AESKey = Global._AESKey;
            string constr = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            if (Session["EmployeeID"] != null)
            {
                int EmployeeID = Convert.ToInt32(Session["EmployeeID"]);

                if (!IsPostBack)
                {
                    LoadJobs(EmployeeID);
                }

            }
            else
            {
                Response.Redirect("EmployerLogin.aspx");
            }

        }
        protected void JobGridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                // Add <thead> tag manually for the header row
                e.Row.TableSection = TableRowSection.TableHeader;
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Add <tbody> tag manually for the data rows
                e.Row.TableSection = TableRowSection.TableBody;
            }
        }

        public void LoadJobs(int EmployeeID)
        {
            var jobs = _dataAccess.GetJobsFromDB(EmployeeID);
            if (jobs.Count > 0)
            {
                // Bind the data to the Repeater
                GridView1.DataSource = jobs;
                GridView1.DataBind();
            }
            else
            {
                // Handle case where no data is found
                GridView1.Visible = false; // or display a message indicating no jobs
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (Session["EmployeeID"] != null)
            {
                int EmployeeID = Convert.ToInt32(Session["EmployeeID"]);

                GridView1.PageIndex = e.NewPageIndex;
                LoadJobs(EmployeeID);
            }
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            if (btn != null)
            {          
                string jobID = btn.CommandArgument;
                int currentPageIndex = GridView1.PageIndex;
                EditJob(jobID, currentPageIndex);
            }

        }

        private void EditJob(string jobID, int currentPageIndex)
        {
            Session["JobId"] = jobID;
            Response.Redirect($"EditJobs.aspx?PageIndex={currentPageIndex}");
        }
       
    }
}
