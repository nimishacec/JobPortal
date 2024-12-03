using JobPortalWebApplication.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Admin
{
    public partial class Applications : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            int AdminId = 0;

            if (Session["AdminID"] != null)
            {
                AdminId = (int)Session["AdminID"];
                if (!IsPostBack)
                {
                    if (Request.QueryString["ApplicationCode"] != null)
                    {
                        //int JobID;
                        //if (int.TryParse(Request.QueryString["JobID"], out JobID))
                        //{
                        string ApplicationCode = Request.QueryString["ApplicationCode"];
                        LoadApplications(ApplicationCode);
                    }
                }

            }
            else
            {
                Response.Redirect("AdminLogin.aspx");
            }
        }
        private void LoadApplications(string ApplicationCode)
        {
            var applications = _dataAccess.GetApplicationFromDB(ApplicationCode);
            if (applications!=null)
            {
               lblCandidateName.Text=applications.CandidateName;
                lblCandidateEmailAddress.Text=applications.CandidateEmailAddress;
                lblCompanyName.Text=applications.CompanyName;
                lblJobTitle.Text=applications.JobTitle;
                lblJobType.Text = applications.JobType;
                lblSalary.Text=applications.Salary.ToString();
                lblVacancy.Text=applications.Vacancy.ToString();
                lblQualifications.Text=applications.HighestEducationLevel.ToString();
                lblExperience.Text=applications.Experience.ToString();
                lblApplicationDate.Text=applications.ApplicationDate.ToString();
                lblApplicationDeadline.Text=applications.JobApplicationLastDate.ToString();

            }
        }
    }
}