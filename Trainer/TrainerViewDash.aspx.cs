using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Employer;
using JobPortalWebApplication.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Trainer
{
    public partial class TrainerViewDash : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            int TrainerID = 0;
            if (Session["TrainerID"] != null)
            {
                TrainerID = (int)Session["TrainerID"];

                if (!IsPostBack)
                {

                    LoadTrainerProfile(TrainerID);
                }
            }
            else
            {
                Response.Redirect("TrainerLogin.aspx");
            }
        }
        private void LoadTrainerProfile(int TrainerID)
        {
            // Assuming you have a method to get employee data
            var trainer = _dataAccess.TrainerView(TrainerID);
            var trainerData = new List<TrainerResponse> { trainer };
            GridView1.DataSource = trainerData;
            GridView1.DataBind();
        }
        protected void btnEditProfile_Click(object sender, EventArgs e)
        {
            int TrainerId = 0;
            if (Session["TrainerID"] != null)
            {
                TrainerId = (int)Session["TrainerID"];
            
                 Session["TrainerID"] = TrainerId;
                Response.Redirect($"TrainerDashboard.aspx");
            }
            else
            {
                // Handle the case where the CandidateID is not available
                // For example, redirect to the login page
                Response.Redirect("EmployerLogin.aspx");
            }
        }
        protected void btnCandSearch_Click(object sender, EventArgs e)
        {
            int TrainerId = 0;
            if (Session["TrainerID"] != null)
            {
                TrainerId = (int)Session["TrainerID"];

                Session["TrainerID"] = TrainerId;
                Response.Redirect($"SearchCandidate.aspx?");
            }
            else
            {
                // Handle the case where the CandidateID is not available
                // For example, redirect to the login page
                Response.Redirect("EmployerLogin.aspx");
            }

           

        }

    }
}

