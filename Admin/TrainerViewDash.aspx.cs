using iTextSharp.text.pdf;
using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Employer;
using JobPortalWebApplication.Models.Response;
using JobPortalWebApplication.Trainer;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Admin
{
    public partial class TrainerViewDash : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        public string AESKey;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            AESKey = Global._AESKey;
            int AdminID = 0; int TrainerId = 0;
            if (Session["AdminID"] != null)
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["TrainerID"] != null)
                {
                    string encryptedTrainerId = Request.QueryString["TrainerID"];
                    int decryptedTrainerId = Convert.ToInt32(Decrypt(encryptedTrainerId));
                  
                        LoadTrainerProfile(decryptedTrainerId);
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
        private void LoadTrainerProfile(int TrainerID)
        {
            Log.Information("LoadTrainerProfile"); // Assuming you have a method to get employee data
            var view = _dataAccess.TrainerView(TrainerID);
           
            if (view != null)
            {
              
                //GridView1.DataSource = empData;
                //GridView1.DataBind();

                lblCompanyName.Text = view.CompanyName;
                lblCompanyRegNumberValue.Text = view.CompanyRegistrationNumber;
                lblEmailValue.Text = view.CompanyEmail;
                lblPhoneValue.Text = view.CompanyPhoneNumber;
                lblCompanyDescriptionValue.Text = view.CompanyDescription;
                lblCompanySizeValue.Text = view.CompanySize;
                lblContactPersonNameValue.Text = view.ContactPersonName;
                lblContactPersonEmailValue.Text = view.ContactPersonEmail;
                lblContactPersonPhoneNumberValue.Text = view.ContactPersonPhoneNumber;
                lblIndustryTypeValue.Text = view.IndustryType;
                lblWebsiteURLValue.Text = view.CompanyWebsiteUrl;
                lblPhysicalAddressValue.Text = view.PhysicalAddress;
                lblAgreementToTermsValue.Text = view.AgreementToTerms == "Yes" ? "True" : "False";
                lblSpecializationValue.Text = view.AreaOfSpecialization;
              
                lblPlanIdValue.Text = view.PlanType;
                Log.Information("Image URL" + view.CompanyLogoUrl);


                if (!string.IsNullOrEmpty(view.CompanyLogoUrl))
                {
                    Log.Information("Image URL" + imgCompanyLogo.ImageUrl);
                    imgCompanyLogo.ImageUrl = ResolveUrl(view.CompanyLogoUrl);
                    Log.Information("Image URL" + imgCompanyLogo.ImageUrl);
                }

            }
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

