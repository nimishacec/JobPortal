using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Response;
using JobPortalWebApplication.Trainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Admin
{
    public partial class EmployerViewDashboard : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        public string AESKey;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            AESKey=Global._AESKey;
            int EmployeeId = 0;
            if (Session["AdminID"] != null)
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["EmployeeId"] != null)
                {
                  //  int.TryParse(Request.QueryString["EmployeeId"], out  EmployeeId);
                   
                   
                        string encryptedEmployeeId = Request.QueryString["EmployeeId"];
                        int decryptedEmployeeId =Convert.ToInt32(Decrypt(encryptedEmployeeId));
                        //int employerId = (int)Session["EmployerID"]; // Assuming employer ID is stored in session
                        LoadEmployeeProfile(decryptedEmployeeId);
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

       

        private void LoadEmployeeProfile( int EmployeeId)
        {
            //int EmployeeId = GetEmployeeId();
            EmployerResponse view = _dataAccess.EmployeeView(EmployeeId);

            if (view != null) { 
                var empData = new List<EmployerResponse> { view };
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
                if (view.TrainingAndPlacementProgram=="YES")
                {
                    lblTrainingAndPlacementProgramValue.Text = "True";
                }
                else
                {
                    lblTrainingAndPlacementProgramValue.Text = "False";
                }
                lblPlanIdValue.Text = view.PlanId;
                if (!string.IsNullOrEmpty(view.CompanyLogoUrl))
                {
                    imgCompanyLogo.ImageUrl = ResolveUrl(view.CompanyLogoUrl);
                }

            }

        }
        private int GetEmployeeId()
        {
            int EmployeeId = 0;
            if (Session["EmployeeId"] != null)
            {
                EmployeeId = (int)Session["EmployeeId"];
            }
            else if (Request.QueryString["EmployeeId"] != null)
            {
                int.TryParse(Request.QueryString["EmployeeId"], out EmployeeId);
            }
            return EmployeeId;
        }
        protected void btnEditProfile_Click(object sender, EventArgs e)
        {
            int EmployeeId = GetEmployeeId();
            if (EmployeeId != 0)
            {
                Session["EmployeeId"]=EmployeeId;
                Response.Redirect($"EmployeeDashboard.aspx");
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
           

                Response.Redirect($"SearchCandidate.aspx?");
           
        }
    }
}