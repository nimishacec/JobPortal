using JobPortalWebApplication.Candidate;
using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Trainer
{
    public partial class TrainingPrograms : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            if (!IsPostBack)
            {
                if (Session["TrainerID"] != null)
                {
                    int TrainerID = 0;

                    TrainerID = (int)Session["TrainerID"];
                }
                else
                {
                    Response.Redirect("TrainerLogin.aspx");
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int TrainerID = 0;

                TrainerID = (int)Session["TrainerID"];
            
            string programName = txtProgramName.Text.Trim();
                string description = txtDescription.Text.Trim();
                DateTime startDate = DateTime.Parse(txtStartDate.Text);
                DateTime endDate = DateTime.Parse(txtEndDate.Text);
                string deliveryMode = ddlDeliveryMode.SelectedValue;
                decimal price = decimal.Parse(txtPrice.Text.Trim());

                TrainingPgmRequest trainingPrograms = new TrainingPgmRequest();
                trainingPrograms.ProgramName = programName;
                trainingPrograms.Description = description;
                trainingPrograms.StartDate = startDate;
                trainingPrograms.EndDate = endDate;
                trainingPrograms.DeliveryMode = deliveryMode;
                trainingPrograms.Price = price;
                var training = _dataAccess.Insert_TrainingPrograms(trainingPrograms, TrainerID);

                Session["StatusMessage"]=training;
                Response.Redirect("StatusPage.aspx");
            }
        }
    }
}