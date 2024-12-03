using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Admin
{
    public partial class AddTrainers : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
        }
        protected void btnSaveProfile_Click(object sender, EventArgs e)
        {
            string companyLogoPath = null;

            // Check if a file has been uploaded
            if (fuCompanyLogo.HasFile)
            {
                try
                {
                    // Define the path to save the uploaded file
                    string folderPath = Server.MapPath("~/Images/CompanyLogos/");

                    // Ensure the directory exists
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Get the file name and save it
                    string fileName = Path.GetFileName(fuCompanyLogo.PostedFile.FileName);
                    string filePath = folderPath + fileName;
                    fuCompanyLogo.SaveAs(filePath);

                    // Display the uploaded image in the Image control
                    companyLogoPath = "~/Images/CompanyLogos/" + fileName;
                    imgCurrentLogo.ImageUrl = companyLogoPath;
                    // Optionally, you can save the file path to your database here
                    // SaveCompanyLogoUrlToDatabase("~/Images/CompanyLogos/" + fileName);

                    lblStatus.Text = "Upload successful!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Upload failed: " + ex.Message;
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblStatus.Text = "Please select a file to upload.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (int.TryParse(Request.QueryString["TrainerID"], out int TrainerID))
            {

                var trainer = new TrainerProfileRequest();
                string planIdString = ddlPlanId.SelectedValue;
                int PlanID = 0;

                if (int.TryParse(planIdString, out int planId))
                {
                    PlanID = planId;
                }
                trainer = new TrainerProfileRequest
                {
                    // CompanyName = txtCompanyName.Text,
                    CompanyRegistrationNumber = txtCompanyRegNumber.Text,
                    //CompanyEmail = txtEmail.Text,
                    CompanyLogo = imgCurrentLogo.ImageUrl,
                    CompanyDescription = txtCompanyDescription.Text,
                    CompanySize = txtCompanySize.Text,
                    ContactPersonName = txtContactPersonName.Text,
                    ContactPersonEmail = txtContactPersonEmail.Text,
                    ContactPersonPhoneNumber = txtContactPersonPhoneNumber.Text,
                    IndustryType = txtIndustryType.Text,
                    PhysicalAddress = txtPhysicalAddress.Text,
                    WebsiteUrl = txtWebsiteURL.Text,
                    PlanId = PlanID,
                    AreasOfSpecialization = txtAreaofSpecialization.Text,
                    //AgreementToTerms = chkAgreementToTerms.Checked,
                    TrainerID = TrainerID

                };
                string message = _dataAccess.TrainerUpdate(trainer);
                if (message.Contains("Successfully Updated"))
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert",
        $"alert('Employee information updated successfully!'); window.location.href='TrainerViewDash.aspx?TrainerID={trainer.TrainerID}';", true);

                    //lblStatus.Text = "Employee information updated successfully!";
                    //lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Employee Updation Failed'); window.location.href='TrainerDashboard.aspx?TrainerID={trainer.TrainerID}';", true);


                }
            }
            // Prov
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrainersList.aspx");
        }
    }
}