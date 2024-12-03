using iTextSharp.text.pdf;
using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Request;
using JobPortalWebApplication.Models.Response;
using JobPortalWebApplication.Trainer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Admin
{
    public partial class TrainerDashboard : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        public string _AESKey;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
              _AESKey = Global._AESKey;
            string constr = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            if (Session["AdminID"] != null)
            {
                if (Request.QueryString["TrainerID"] != null)
                {
                    string encryptedTrainerID = Request.QueryString["TrainerID"];
                    int decryptedTrainerID = Convert.ToInt32(Decrypt(encryptedTrainerID));
                    

                    if (!IsPostBack)
                    {
                        LoadTrainerProfile(decryptedTrainerID);
                        GetPlanId(decryptedTrainerID);
                        BindPlanTypes();
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
                aes.Key = Encoding.UTF8.GetBytes(_AESKey); // Same key used for encryption
                aes.IV = new byte[16];

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
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
        private void BindPlanTypes()
        {
            var planTypes = _dataAccess.GetPlanTypes();
            ddlPlanId.DataSource = planTypes;
            ddlPlanId.DataTextField = "PlanName";
            ddlPlanId.DataValueField = "PlanId";
            ddlPlanId.DataBind();

            // Add a default item
            ddlPlanId.Items.Insert(0, new ListItem("Select Plan Type", ""));
        }
        private void LoadTrainerProfile(int TrainerID)
        {
            // Assuming you have a method to get employee data
            var view = _dataAccess.TrainerView(TrainerID);
            if (view != null)
            {
                txtCompanyName.Text = view.CompanyName;
                txtCompanyRegNumber.Text = view.CompanyRegistrationNumber;
                txtEmail.Text = view.CompanyEmail;
                txtPhone.Text = view.CompanyPhoneNumber;
                txtCompanyDescription.Text = view.CompanyDescription;
                txtCompanySize.Text = view.CompanySize;
                txtContactPersonName.Text = view.ContactPersonName;
                txtContactPersonEmail.Text = view.ContactPersonEmail;
                txtContactPersonPhoneNumber.Text = view.ContactPersonPhoneNumber;
                txtIndustryType.Text = view.IndustryType;
                txtWebsiteURL.Text = view.CompanyWebsiteUrl;
                txtPhysicalAddress.Text = view.PhysicalAddress;
                if (view.AgreementToTerms != null)
                {
                    chkAgreementToTerms.Checked = view.AgreementToTerms.Equals("Yes", StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    chkAgreementToTerms.Checked = false; // or a default value if `view.AgreementToTerms` is null
                }

                //chkAgreementToTerms.Checked = view.AgreementToTerms == "Yes" ? true : false;
                txtAreaofSpecialization.Text = view.AreaOfSpecialization;
                if (ddlPlanId != null && view.PlanType != null)
                {
                    ListItem item = ddlPlanId.Items.FindByValue(view.PlanType.ToString());
                    if (item != null)
                        ddlPlanId.SelectedValue = view.PlanType.ToString();
                }
                else
                {

                    ddlPlanId.SelectedIndex = 0;
                    //ddlPlanId.SelectedValue = view.PlanId;
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrainersList.aspx");
        }
        private void GetPlanId(int TrainerId)
        {

            int? planId = _dataAccess.GetPlanId(TrainerId);


            if (planId.HasValue && ddlPlanId.Items.FindByValue(planId.Value.ToString()) != null)
            {
                
                ddlPlanId.SelectedValue = planId.Value.ToString();
            }
            else
            {
                
                ddlPlanId.SelectedIndex = 0; // Or you can leave it as the first item
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["TrainerID"] != null)
            {
                string encryptedTrainerID = Request.QueryString["TrainerID"];
                int decryptedTrainerID = Convert.ToInt32(Decrypt(encryptedTrainerID));


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
                    CompanyLogo= imgCurrentLogo.ImageUrl,
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
                    TrainerID = decryptedTrainerID

                };
                string message = _dataAccess.TrainerUpdate(trainer);
                if (message.Contains("Successfully Updated"))
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert",
        $"alert('Employee information updated successfully!'); window.location.href='TrainersList.aspx';", true);

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
    }
}