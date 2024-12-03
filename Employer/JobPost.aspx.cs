using JobPortalWebApplication.Candidate;
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
    public partial class JobPost : System.Web.UI.Page
    {
        private DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;

            if (Session["EmployeeID"] != null)
            {
             int   EmployeeId = (int)Session["EmployeeID"];
                if (!IsPostBack)
                    {
                        BindJobTypes();
                        BindJobLocations();
                    }
            }
            else
            {
                Response.Redirect("EmployerLogin.aspx");
            }

            
        }
        protected void cvStartDateValidation_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime startDate;
            if (DateTime.TryParse(txtApplicationStartDate.Text, out startDate))
            {
                if (startDate > DateTime.Today)
                {
                    args.IsValid = true; // Start date is valid (greater than today's date)
                }
                else
                {
                    args.IsValid = false; // Start date is not valid
                }
            }
            else
            {
                args.IsValid = false; // Invalid date format
            }
        }

        protected void cvDateComparison_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime startDate, endDate;

            bool isStartDateValid = DateTime.TryParse(txtApplicationStartDate.Text, out startDate);
            bool isEndDateValid = DateTime.TryParse(txtApplicationDeadline.Text, out endDate);

            if (isStartDateValid && isEndDateValid)
            {
                args.IsValid = endDate > startDate;
            }
            else
            {
                args.IsValid = false;
            }
        }
        private void BindJobTypes()
        {
            var jobTypes = _dataAccess.GetAllJobTypes();
            ddlJobTypes.DataSource = jobTypes;
            ddlJobTypes.DataTextField = "JobName";
            ddlJobTypes.DataValueField = "JobTypeID";
            ddlJobTypes.DataBind();
            ddlJobTypes.Items.Insert(0, new ListItem("Select Job Type", ""));
        }

        private void BindJobLocations()
        {
            var jobLocations = _dataAccess.GetAllJobLocation();
            ddlJobLocation.DataSource = jobLocations;
            ddlJobLocation.DataTextField = "LocationName";
            ddlJobLocation.DataValueField = "JobLocationID";
            ddlJobLocation.DataBind();
            ddlJobLocation.Items.Insert(0, new ListItem("Select Job Location", ""));
        }
        private int GetEmployeeId()
        {
            int EmployeeId = 0;
            if (Session["EmployeeID"] != null)
            {
                EmployeeId = (int)Session["EmployeeID"];
            }
            else if (Request.QueryString["EmployeeID"] != null)
            {
                int.TryParse(Request.QueryString["EmployeeID"], out EmployeeId);
            }
            return EmployeeId;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployerViewDashBoard.aspx"); // Redirect to the previous page or wherever appropriate
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var jobpost = new JobPostingRequest();
            int jobtype = 0;
            if (int.TryParse(ddlJobTypes.SelectedValue, out int jobtypeId))
            {
               jobtype = jobtypeId;
            }
            int jobloc = 0;
            if (int.TryParse(ddlJobLocation.SelectedValue, out int jobLocId))
            {
                jobloc = jobLocId;
            }
            jobpost = new JobPostingRequest
            {
                JobTitle = txtJobTitle.Text,
                Vacancy = int.Parse(txtVacancy.Text),
                JobDescription = txtJobDescription.Text,
                Qualifications = txtQualifications.Text,
                Experience = txtExperience.Text,
                RequiredSkills = txtRequiredSkills.Text,
                JobLocationId = jobloc,
                Salary = string.IsNullOrEmpty(txtSalary.Text) ? (decimal?)null : decimal.Parse(txtSalary.Text),
                //CompanyName = txtCompanyName.Text,
                JobTypeId = jobtype,
                //Address = txtAddress.Text,
                //ContactEmail = txtContactEmail.Text,
                //Website = txtWebsite.Text,
                //Industry = txtIndustry.Text,
                ApplicationDeadline = string.IsNullOrEmpty(txtApplicationDeadline.Text) ? (DateTime?)null : DateTime.Parse(txtApplicationDeadline.Text),
                ApplicationStartDate = string.IsNullOrEmpty(txtApplicationStartDate.Text) ? (DateTime?)null : DateTime.Parse(txtApplicationStartDate.Text)

            };
            int EmployeeId = GetEmployeeId();
            var jobs = _dataAccess.PostJobsDB(jobpost,EmployeeId);
            if (jobs)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Job Posted Successfully'); window.location='JobPost.aspx'", true);

               
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Job Posting failed'); window.location='JobPost.aspx'", true);

            }
          

        }
    }
}