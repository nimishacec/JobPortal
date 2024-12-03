using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Request;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Employer
{
    public partial class EditJobs : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            //  _AESKey = Global._AESKey;
            string constr = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            if (Session["JobId"] != null)
            {
                int JobId = Convert.ToInt32(Session["JobId"]);
                int EmployeeID = Convert.ToInt32(Session["EmployeeID"]);
                if (!IsPostBack)
                {
                    BindJobTypes();
                    BindJobLocations();
                    GetJobTypeId(EmployeeID);
                    GetJobLocationId(EmployeeID);
                    LoadJobs(EmployeeID, JobId);
                }
                //if (Request.QueryString["EmployeeID"] != null)
                //{
                //    int EmployeeID;
                //    if (int.TryParse(Request.QueryString["EmployeeID"], out EmployeeID))
                //    {
                //        if (!IsPostBack)
                //        {
                //            BindJobTypes();
                //            BindJobLocations();
                //            
                //        }
                //    }
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
        private void GetJobTypeId(int employeeId)
        {

            int? JobTypeId = _dataAccess.GetJobTypeIdforEmployee(employeeId);


            if (JobTypeId.HasValue && ddlJobTypes.Items.FindByValue(JobTypeId.Value.ToString()) != null)
            {
                // If a valid CountryID is found, set it as the selected value
                ddlJobTypes.SelectedValue = JobTypeId.Value.ToString();
            }
            else
            {
                // If the CountryID is null or not found, set the default value (like "Select Country")
                ddlJobTypes.SelectedIndex = 0; // Or you can leave it as the first item
            }

        }
        private void GetJobLocationId(int employeeId)
        {

            int? JobLocationId = _dataAccess.GetJobLocationIdforEmployee(employeeId);


            if (JobLocationId.HasValue && ddlJobLocation.Items.FindByValue(JobLocationId.Value.ToString()) != null)
            {
                // If a valid CountryID is found, set it as the selected value
                ddlJobLocation.SelectedValue = JobLocationId.Value.ToString();
            }
            else
            {
                // If the CountryID is null or not found, set the default value (like "Select Country")
                ddlJobLocation.SelectedIndex = 0; // Or you can leave it as the first item
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string pageIndex = Request.QueryString["PageIndex"];
            Response.Redirect($"ViewJobs.aspx?PageIndex={pageIndex}");
          
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
        private int GetJobId()
        {
            int JobId = 0;
            if (Session["JobId"] != null)
            {
                JobId = Convert.ToInt32(Session["JobId"]);
            }
            else if (Request.QueryString["JobId"] != null)
            {
                int.TryParse(Request.QueryString["JobId"], out JobId);
            }
            return JobId;
        }
        public void LoadJobs(int EmployeeID ,int JobID)
        {
            //int EmployeeId = GetEmployeeId();
            //int JobId = GetJobId();
            var jobs = _dataAccess.GetJobFromDB(JobID);
            if (jobs == null)
                Response.Redirect("ViewJobs.aspx");
            else
            {
                txtJobTitle.Text=jobs.JobTitle;
                txtJobDescription.Text=jobs.JobDescription;
                txtQualifications.Text=jobs.Qualifications;
                txtExperience.Text=jobs.Experience;
                txtRequiredSkills.Text=jobs.RequiredSkills;
                txtSalary.Text=jobs.Salary.ToString();
                //txtCompanyName.Text=jobs.CompanyName;
                //txtContactEmail.Text=jobs.ContactEmail;
                //txtIndustry.Text=jobs.Industry;
                txtVacancy.Text=jobs.Vacancy.ToString();
                //txtWebsite.Text=jobs.Website;
                ddlJobLocation.SelectedValue = jobs.JobLocation;
                ddlJobTypes.SelectedValue = jobs.JobType;
                txtApplicationDeadline.Text=jobs.ApplicationDeadline.ToString();
                txtApplicationStartDate.Text=jobs.ApplicationStartDate.ToString();

            }

        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            var editjobs = new JobPostingRequest();
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
            editjobs = new JobPostingRequest
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
            int JobId = GetJobId();
            var jobs = _dataAccess.UpdateJobsDB(editjobs, EmployeeId, JobId);
            if (jobs)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Your job update request has been submitted. Please wait for admin approval.'); window.location='ViewJobs.aspx'", true);


                //Session["StatusMessage"] = "Your job update request has been submitted. Please wait for admin approval.";
                //Response.Redirect("StatusPage.aspx");
            }
            else
            {

                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('There was an error submitting your job update request. Please try again.'); window.location='ViewJobs.aspx'", true);

                //Session["StatusMessage"] = "There was an error submitting your job update request. Please try again.";
                //Response.Redirect("StatusPage.aspx");
                
            }
        }
        }
        }
    