using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Response;
using JobPortalWebApplication.Trainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortalWebApplication.Employer
{
    public partial class EmployerViewDashboard : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            int EmployeeId = 0;

            if (Session["EmployeeId"] != null)
            {
                EmployeeId = (int)Session["EmployeeId"];
                if (!IsPostBack)
                {
                    //int employerId = (int)Session["EmployerID"]; // Assuming employer ID is stored in session
                    LoadEmployeeProfile(EmployeeId);
                }
            }
           else if (Request.QueryString["EmployeeId"] != null)
            {
                int.TryParse(Request.QueryString["EmployeeId"], out int EmployeeID);
                EmployeeId = EmployeeID;
                if (!IsPostBack)
                {
                    //int employerId = (int)Session["EmployerID"]; // Assuming employer ID is stored in session
                    LoadEmployeeProfile(EmployeeId);
                }

            }

            
            
            else
            {
                Response.Redirect("EmployerLogin.aspx");
            }
        }
        private void LoadEmployeeProfile( int EmployeeId)
        {
            //int EmployeeId = GetEmployeeId();
            EmployerResponse view = _dataAccess.EmployeeView(EmployeeId);

            if (view != null) { 
                var empData = new List<EmployerResponse> { view };
            GridView1.DataSource = empData;
            GridView1.DataBind();
            
                //lblCompanyNameValue.Text = view.CompanyName;
                //lblCompanyRegNumberValue.Text = view.CompanyRegistrationNumber;
                //lblEmailValue.Text = view.CompanyEmail;
                //lblPhoneValue.Text = view.CompanyPhoneNumber;
                //lblCompanyDescriptionValue.Text = view.CompanyDescription;
                //lblCompanySizeValue.Text = view.CompanySize;
                //lblContactPersonEmailValue.Text = view.ContactPersonName;
                //lblContactPersonEmailValue.Text = view.ContactPersonEmail;
                //lblContactPersonPhoneNumberValue.Text = view.ContactPersonPhoneNumber;
                //lblIndustryTypeValue.Text = view.IndustryType;
                //lblWebsiteURLValue.Text=view.CompanyWebsiteUrl;
                //lblPhysicalAddressValue.Text=view.PhysicalAddress;
                //lblAgreementToTermsValue.Text=view.AgreementToTerms =="Yes"? "True": "False";
                //lblTrainingAndPlacementProgramValue.Text = view.TrainingAndPlacementProgram == true ? "True" : "False";
                //lblPlanIdValue.Text=view.PlanId;
                //EducationDetail---

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