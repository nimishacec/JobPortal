using JobPortalWebApplication.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.XPath;

namespace JobPortalWebApplication.Employer
{
    public partial class CandidatesSearch : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            if (Session["EmployeeID"] != null)
            {
                int candidateId = Convert.ToInt32(Session["EmployeeID"]);

                if (!IsPostBack)
                {
                    BindJobTitles();


                    // LoadFilteredCandidates();

                }
            }
            else
            {
                Response.Redirect("EmployerLogin.aspx");
            }
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
            
        }
        protected  void btnSearchCandidates_Click(object sender ,EventArgs e)
        {
            //string sortBy = ddlSortBy.SelectedValue;
            //string sortOrder = rblSortOrder.SelectedValue;
            string jobTitle = ddlJobTitle.SelectedValue !=null ?  ddlJobTitle.SelectedValue: null;

            string location = ddlJobLocation.SelectedValue != null ? ddlJobLocation.SelectedValue : null;
            string skills = txtSkills.Text;
            string experience = ddlExperience.SelectedValue != null ? ddlExperience.SelectedValue : null;
            string education = txtEducation.Text;

            var searchResult=_dataAccess.GetFilteredCandidates(jobTitle,location, skills, experience,education);

        }
    }
}