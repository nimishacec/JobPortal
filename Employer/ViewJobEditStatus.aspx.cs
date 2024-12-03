using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Employer
{
    public partial class ViewJobEditStatus : System.Web.UI.Page
    {
        private DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            if (Session["EmployeeID"] != null)
            {
                int EmployeeId =Convert.ToInt32(Session["EmployeeID"]);
                if (!IsPostBack)
                {
                    LoadRequests(EmployeeId);
                }
            }
            else
            {
                Response.Redirect("EmployerLogin.aspx");
            }

        }
        protected string GetStatusCssClass(string status)
        {
            switch (status.ToLower())
            {
                case "approved":
                    return "status-approved";
                case "rejected":
                    return "status-rejected";
                case "pending":
                    return "status-pending";
                default:
                    return string.Empty; // No class if status is not recognized
            }
        }
        protected void GridView1_PageIndexChanging(object sender, EventArgs e)
        {

        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadRequests(int employeeId)
        {

            var jobs = _dataAccess.GetJobEditRequestStatus(employeeId);
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


    }
}