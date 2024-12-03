using iTextSharp.text.pdf;
using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Request;

using JobPortalWebApplication.Models.Response;
using JobPortalWebApplication.Trainer;
using Org.BouncyCastle.Asn1.Cmp;
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
    public partial class EmployeeDashboard : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        public string AESKey;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
              AESKey = Global._AESKey;
            //string constr = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            if (Session["AdminID"] != null)
            {
                int AdminID = (int)Session["AdminID"];
                if (!IsPostBack)
                {
                    if (Request.QueryString["EmployeeID"] != null)
                    {
                        string encryptedEmployeeId = Request.QueryString["EmployeeID"];
                        int decryptedEmployeeId = Convert.ToInt32(Decrypt(encryptedEmployeeId));
                        hiddenFieldCandidateId.Value = decryptedEmployeeId.ToString();
                        BindPlanTypes();
                        LoadEmployeeProfile(decryptedEmployeeId);
                        GetPlanId(decryptedEmployeeId);
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

                   // lblStatus.Text = "Upload successful!";
                   // lblStatus.ForeColor = System.Drawing.Color.Green;
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
        public void LoadEmployeeProfile(int EmployeeID)
        {
            EmployerResponse view = _dataAccess.EmployeeView(EmployeeID);
            if (view != null)
            {

                imgCurrentLogo.ImageUrl = view.CompanyLogoUrl;
                // Set other profile details...

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
                    chkAgreementTerms.Checked = view.AgreementToTerms == "YES" ? true : false;
                }
                else
                {
                    //chkAgreementToTerms.Checked = false; // or a default value if `view.AgreementToTerms` is null
                }

                if (view.TrainingAndPlacementProgram!=null)
                {
                    chkPlacement.Checked = view.TrainingAndPlacementProgram == "YES" ? true : false;
                }

                else
                {
                  //  chkTrainingProgram.Checked = false; // or a default value if `view.AgreementToTerms` is null
                }

                // chkAgreementToTerms.Checked = view.AgreementToTerms == "Yes" ? true : false;
                //chkTrainingProgram.Checked = view.TrainingAndPlacementProgram;
                if (ddlPlanId != null && view.PlanId != null)
                {
                    ListItem item = ddlPlanId.Items.FindByValue(view.PlanId.ToString());
                    if (item != null)
                        ddlPlanId.SelectedValue = view.PlanId.ToString();
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
            Response.Redirect("EmployeeList.aspx");
        }
        private void GetPlanId(int TrainerId)
        {

            int? planId = _dataAccess.GetPlanIdforEmployee(TrainerId);


            if (planId.HasValue && ddlPlanId.Items.FindByValue(planId.Value.ToString()) != null)
            {

                ddlPlanId.SelectedValue = planId.Value.ToString();
            }
            else
            {

                ddlPlanId.SelectedIndex = 0; // Or you can leave it as the first item
            }

        }
        public string Encrypt(string plainText)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(AESKey); // Use a proper key here
                aes.IV = new byte[16]; // Initialization vector, can be randomized for added security

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int employeeId = Convert.ToInt32(hiddenFieldCandidateId.Value);
            string encryptEmployeeId = Encrypt(hiddenFieldCandidateId.Value);
            var employee = new EmployeeProfileRequest();
            string planIdString = ddlPlanId.SelectedValue;

            int PlanID = 0;

            if (int.TryParse(planIdString, out int planId))
            {
                PlanID = planId;
            }
            employee = new EmployeeProfileRequest
            {
                CompanyName = txtCompanyName.Text,
                CompanyRegistrationNumber = txtCompanyRegNumber.Text,
                CompanyEmail = txtEmail.Text,
                CompanyPhoneNumber = txtPhone.Text,
                CompanyDescription = txtCompanyDescription.Text,
                CompanySize = txtCompanySize.Text,
                ContactPersonName = txtContactPersonName.Text,
                ContactPersonEmail = txtContactPersonEmail.Text,
                ContactPersonPhoneNumber = txtContactPersonPhoneNumber.Text,
                IndustryType = txtIndustryType.Text,
                PhysicalAddress = txtPhysicalAddress.Text,
                WebsiteUrl = txtWebsiteURL.Text,
                PlanId = PlanID,
                //TrainingAndPlacementProgram = chkTrainingProgram.Checked,
             //   AgreementToTerms = chkAgreementToTerms.Checked,
                CompanyLogo = imgCurrentLogo.ImageUrl,

            };
            string message = _dataAccess.EmployeeUpdate(employee, employeeId);
            if (message.Contains("Successfully Updated"))
            {
                string urlEncodedEmployeeId = Server.UrlEncode(encryptEmployeeId);

                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert",
    $"alert('Employee information updated successfully!'); window.location.href='EmployerViewDashboard.aspx?EmployeeID={urlEncodedEmployeeId}';", true);

                //lblStatus.Text = "Employee information updated successfully!";
                //lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
            }
            else
            {
                string urlEncodedEmployeeId = Server.UrlEncode(encryptEmployeeId);
                ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Employee Updation Failed'); window.location.href='EmployerDashboard.aspx?EmployeeID={urlEncodedEmployeeId}';", true);


            }
            // Prov
        }
    }
}