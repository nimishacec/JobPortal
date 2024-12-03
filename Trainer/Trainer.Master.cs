using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Trainer
{
    public partial class Trainer : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {

        //    Response.Redirect("SearchCandidate.aspx");

        }
        protected void btnEditProfile_Click(object sender, EventArgs e)
        {
            int TrainerId = 0;

            if (Session["TrainerID"] != null)
            {
                TrainerId = (int)Session["TrainerID"];
            }
            if (TrainerId != 0)
            {

                Response.Redirect($"TrainerDashboard.aspx");
            }
            else
            {

                Response.Redirect("TrainerLogin.aspx");
            }
        }
        protected void btnViewTrainingPrograms_Click(object sender, EventArgs e)
        {
            int TrainerId = 0;

            if (Session["TrainerID"] != null)
            {
                TrainerId = (int)Session["TrainerID"];
            }
            if (TrainerId != 0)
            {

                Response.Redirect($"TrainerDashboard.aspx");
            }
            else
            {

                Response.Redirect("TrainerLogin.aspx");
            }
        }
        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrainerLogin.aspx");
        }
        protected void btnTrainingPrograms_Click(object sender, EventArgs e)
        {
            int TrainerId = 0;

            if (Session["TrainerID"] != null)
            {
                TrainerId = (int)Session["TrainerID"];
            }
            if (TrainerId != 0)
            {
                Session["TrainerID"] = TrainerId;
                Response.Redirect($"TrainingPrograms.aspx");
            }
            else
            {

                Response.Redirect("TrainerLogin.aspx");
            }
        }
    }
}