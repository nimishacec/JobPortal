using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Candidate
{
    public partial class ViewAppliedJobs : System.Web.UI.Page
    {
        private DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            if (Session["CandidateID"] != null)
            {
                int candidateId = Convert.ToInt32(Session["CandidateID"]);

                if (!IsPostBack)
                {
                    LoadAppliedJobs(candidateId);
                }
            }
            else
            {
                Response.Redirect("CandidateLogin.aspx");
            }
        }

            private void LoadAppliedJobs(int candidateId)
            {

                List<JobSearchResult> appliedJobs = _dataAccess.GetAllAppliedJobs(candidateId); // Retrieve applied jobs from DB

                if (appliedJobs != null && appliedJobs.Count > 0)
                {
                    rptAppliedJobs.DataSource = appliedJobs;
                    rptAppliedJobs.DataBind();
                }
                else
                {
                    // Handle case where no jobs have been applied to
                    lblNoJobs.Visible = true;
                }
            }
        }
    }
