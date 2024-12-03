using JobPortalWebApplication.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Candidate
{
    public partial class JobAlerts : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            int candidateId = 0;
            if (Session["CandidateID"] != null)
            {
                candidateId = (int)Session["CandidateID"];
                if (!IsPostBack)
                {
                    BindJobTitles();
                    BindJobLocation();
                }
            }
            else
            {
                Response.Redirect("CandidateLogin.aspx");
            }
        }
        public void BindJobLocation()
        {
            var jobLocations = _dataAccess.GetJobLocation();

            ddlJobLocation.DataSource = jobLocations;
            ddlJobLocation.DataTextField = "LocationName";
            ddlJobLocation.DataValueField = "JobLocationID";
            ddlJobLocation.DataBind();

            // Add the "Others" option manually
            ddlJobLocation.Items.Insert(0, new ListItem("Select Location", " "));
        }
        public void BindJobTitles()
        {
            var jobTitles = _dataAccess.GetJobTitles();
            ddlJobTitle.DataSource = jobTitles;
            ddlJobTitle.DataTextField = "JobTitle";
            ddlJobTitle.DataValueField = "JobTitle";
            ddlJobTitle.DataBind();

            // Add the "Others" option manually

            ddlJobTitle.Items.Insert(0, new ListItem("Select JobTitle", ""));

            // Add the "Others" option
            ddlJobTitle.Items.Add(new ListItem("Others", "Others"));
        }
        protected void btnJobAlert_click(object sender, EventArgs e)
        {
            int candidateId = 0;
            if (Session["CandidateID"] != null)
            {
                candidateId = (int)Session["CandidateID"];
            }
            string keyword = ddlJobTitle.Text;
            int Location = ddlJobLocation.SelectedIndex;
            string Requiredskills = Request.Form["requiredskills"];
            decimal MinSalary;
            decimal MaxSalary;

            bool isMinSalaryValid = decimal.TryParse(txtminSalary.Text, out MinSalary);
            bool isMaxSalaryValid = decimal.TryParse(txtmaxSalary.Text, out MaxSalary);
            decimal? minSalary = isMinSalaryValid ? MinSalary : (decimal?)null;
            decimal? maxSalary = isMaxSalaryValid ? MaxSalary : (decimal?)null ;
           
            //decimal MinSalary = txtminSalary.Text != null ? Convert.ToDecimal(txtminSalary.Text) : 0;
            //decimal MaxSalary = txtmaxSalary.Text != null ? Convert.ToDecimal(txtmaxSalary.Text) : 0;
            string selectedFrequency = ddlFrequency.SelectedValue;
            string frequency = ddlFrequency.SelectedValue;
           

                if (string.IsNullOrEmpty(keyword))
                {

                    Response.Write("<script>alert('Please fill in the required fields.');</script>");
                    return;
                }

                bool isSaved = _dataAccess.SaveJobAlert(candidateId, keyword, Location, Requiredskills, minSalary, maxSalary, frequency);


                if (isSaved)
                {
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Job alert saved successfully.'); window.location='JobAlerts.aspx'", true);
              
                }
                else
                {
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Failed to save job alert. Please try again'); window.location='JobAlerts.aspx'", true);

               
                }
            }
        }
    }
