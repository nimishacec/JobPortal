using iTextSharp.text.pdf;
using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Request;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.EnterpriseServices;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Admin
{
    public partial class EditJobs : System.Web.UI.Page
    {
        public DataAccess _dataAccess; public string AESKey;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            AESKey = Global._AESKey;
            if (Session["AdminID"] != null)
            {
                if (!IsPostBack)
                {
                    int RequestID = 0; int decryptedJobId = 0;
                    int EmployeeId = Convert.ToInt32(Session["AdminID"]);
                    if (Request.QueryString["JobID"] != null)
                    {

                        string encryptedJobId = Request.QueryString["JobID"];
                        decryptedJobId = Convert.ToInt32(Decrypt(encryptedJobId));
                        var jobs = _dataAccess.GetJobFromDB(decryptedJobId);
                        DateTime? applicationStartDate = jobs.ApplicationStartDate;
                        DateTime? applicationLastDate = jobs.ApplicationDeadline;
                        // Store it in ViewState for later use in validation
                        ViewState["ApplicationStartDate"] = applicationStartDate;
                        ViewState["ApplicationLastDate"] = applicationLastDate;
                        // Display date in the TextBox if it exists
                        if (applicationStartDate.HasValue)
                        {
                            txtApplicationStartDate.Text = applicationStartDate.Value.ToString("yyyy-MM-dd");
                        }
                        if (applicationLastDate.HasValue)
                        {
                            txtApplicationDeadline.Text = applicationLastDate.Value.ToString("yyyy-MM-dd");
                        }
                        BindJobTypes();
                        BindJobLocations();
                        GetJobTypeId(decryptedJobId);
                        GetJobLocationId(decryptedJobId);
                        LoadJobs(decryptedJobId, RequestID);

                    }
                    if (Request.QueryString["RequestID"] != null)
                    {

                        RequestID = Convert.ToInt32(Request.QueryString["RequestID"]);
                        // int decryptedJobId = Convert.ToInt32(Decrypt(encryptedJobId));
                        BindJobTypes();
                        BindJobLocations();
                        GetJobType(RequestID);
                        GetJobLocation(RequestID);
                        LoadJobs(decryptedJobId, RequestID);
                    }

                }
            }
            else
            {
                Response.Redirect("AdminLogin.aspx");
            }
        }
        public string Decrypt(string encryptedText)
        {
            byte[] cipherBytes = Convert.FromBase64String(encryptedText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(AESKey); // Same key used for encryption
                aes.IV = new byte[16];

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }
        protected void cvStartDateValidation_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime? existingStartDate = ViewState["ApplicationStartDate"] as DateTime?;
            DateTime selectedDate;

            // Try parsing the date from the TextBox
            if (DateTime.TryParse(txtApplicationStartDate.Text, out selectedDate))
            {
                // Validate only if the selected date is different from the existing start date and is not greater than today
                if (existingStartDate.HasValue && selectedDate != existingStartDate.Value && selectedDate <= DateTime.Today)
                {
                    args.IsValid = false; // Invalid if selected date is in the past
                }
                else
                {
                    args.IsValid = true;
                }
            }
            else
            {
                args.IsValid = false; // Invalid if parsing fails
            }

        }

        //protected void cvStartDateValidation_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    DateTime startDate;
        //    if (DateTime.TryParse(txtApplicationStartDate.Text, out startDate))
        //    {
        //        if (startDate > DateTime.Today)
        //        {
        //            args.IsValid = true; // Start date is valid (greater than today's date)
        //        }
        //        else
        //        {
        //            args.IsValid = false; // Start date is not valid
        //        }
        //    }
        //    else
        //    {
        //        args.IsValid = false; // Invalid date format
        //    }
        //}
        private void GetJobType(int employeeId)
        {

            int? JobTypeId = _dataAccess.GetJobTypes(employeeId);


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
        private void GetJobLocation(int employeeId)
        {

            int? JobLocationId = _dataAccess.GetJobLocation(employeeId);


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

            Response.Redirect("NewJobs.aspx");

        }
        protected void cvDateComparison_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(txtApplicationStartDate.Text) || string.IsNullOrWhiteSpace(txtApplicationDeadline.Text))
            {
                // If either date is missing, do not validate; assume fields are required through separate RequiredFieldValidators
                args.IsValid = true;
                return;
            }

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
            int decryptedJobId = 0;
            //if (Session["JobId"] != null)
            //{
            //    JobId = Convert.ToInt32(Session["JobId"]);
            //}
            if (Request.QueryString["JobId"] != null)
            {
                string encryptedJobId = Request.QueryString["JobID"];
                decryptedJobId = Convert.ToInt32(Decrypt(encryptedJobId));
            }
            return decryptedJobId;

        }
        private int GetRequestId()
        {
            int RequestID = 0;
            if (Request.QueryString["RequestID"] != null)
            {
                RequestID = Convert.ToInt32(Request.QueryString["RequestID"]);

            }
                return RequestID;
            

        }
        public void LoadJobs(int JobID, int RequestID)
        {
            if (JobID > 0)
            {
                //int EmployeeId = GetEmployeeId();
                //int JobId = GetJobId();
                var jobs = _dataAccess.GetJobFromDB(JobID);
                if (jobs == null)
                    Response.Redirect("NewJobs.aspx");
                else
                {
                    DateTime? applicationstartDate = jobs.ApplicationStartDate;
                    txtJobTitle.Text = jobs.JobTitle;
                    txtJobDescription.Text = jobs.JobDescription;
                    txtQualifications.Text = jobs.Qualifications;
                    txtExperience.Text = jobs.Experience;
                    txtRequiredSkills.Text = jobs.RequiredSkills;
                    txtSalary.Text = jobs.Salary.ToString();
                    txtCompanyName.Text = jobs.CompanyName;
                    //txtContactEmail.Text=jobs.ContactEmail;
                    //txtIndustry.Text=jobs.Industry;
                    txtVacancy.Text = jobs.Vacancy.ToString();
                    //txtWebsite.Text=jobs.Website;
                    ddlJobLocation.SelectedItem.Value = jobs.JobLocation;
                    ddlJobTypes.SelectedItem.Value = jobs.JobType;

                    txtApplicationStartDate.Text = jobs.ApplicationStartDate.Value.ToString("yyyy-MM-dd");
                    txtApplicationDeadline.Text = jobs.ApplicationDeadline.Value.ToString("yyyy-MM-dd"); ;
                   

                }
            }
            if (RequestID > 0)
            {
                var jobs = _dataAccess.GetJobRequestFromDB(RequestID);
                if (jobs == null)
                    Response.Redirect("JobEditApproval.aspx");
                else
                {
                    txtJobTitle.Text = jobs.JobTitle;
                    txtJobDescription.Text = jobs.JobDescription;
                    txtQualifications.Text = jobs.Qualifications;
                    txtExperience.Text = jobs.Experience;
                    txtRequiredSkills.Text = jobs.RequiredSkills;
                    txtSalary.Text = jobs.Salary.ToString();
                    txtCompanyName.Text = jobs.CompanyName;
                    //txtContactEmail.Text=jobs.ContactEmail;
                    //txtIndustry.Text=jobs.Industry;
                    txtVacancy.Text = jobs.Vacancy.ToString();
                    //txtWebsite.Text=jobs.Website;
                    ddlJobLocation.SelectedItem.Value = jobs.JobLocation;
                    ddlJobTypes.SelectedItem.Value = jobs.JobType;
                    txtApplicationDeadline.Text = jobs.ApplicationDeadline.ToString();
                    txtApplicationStartDate.Text = jobs.ApplicationStartDate.ToString();

                }
            }

        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            int RequestID = GetRequestId();
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
            //var job = _dataAccess.GetJobRequestFromDB(RequestID);
            int JobId = GetJobId();
            editjobs = new JobPostingRequest
            {
                JobId = JobId,
                JobTitle = txtJobTitle.Text,
                Vacancy = int.Parse(txtVacancy.Text),
                JobDescription = txtJobDescription.Text,
                Qualifications = txtQualifications.Text,
                Experience = txtExperience.Text,
                RequiredSkills = txtRequiredSkills.Text,
                JobLocationId = jobloc,
                Salary = string.IsNullOrEmpty(txtSalary.Text) ? (decimal?)null : decimal.Parse(txtSalary.Text),
                CompanyName = txtCompanyName.Text,
                JobTypeId = jobtype,
                //Address = txtAddress.Text,
                //ContactEmail = txtContactEmail.Text,
                //Website = txtWebsite.Text,
                //Industry = txtIndustry.Text,
                ApplicationDeadline = string.IsNullOrEmpty(txtApplicationDeadline.Text) ? (DateTime?)null : DateTime.Parse(txtApplicationDeadline.Text),
                ApplicationStartDate = string.IsNullOrEmpty(txtApplicationStartDate.Text) ? (DateTime?)null : DateTime.Parse(txtApplicationStartDate.Text)

            };
          
           
            var jobs = _dataAccess.UpdateJobByAdmin(editjobs, JobId);
            if (jobs)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Job updated.'); window.location='NewJobs.aspx'", true);
                LoadJobs(JobId, RequestID);

            }
            else
            {

                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('There was an error submitting your job update request. Please try again.'); window.location='NewJobs.aspx'", true);

            }
        }
    }
}
