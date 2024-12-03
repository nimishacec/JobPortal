
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
    public partial class CandidateViewDashboard : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            int candidateId = GetCandidateId();
            if (candidateId != 0)
            {
                if (!IsPostBack)
                {
                    LoadCandidateProfile();
                }
            }
            else
            {

                Response.Redirect("CandidateLogin.aspx");
            }


        }
        private void LoadCandidateProfile()
        {
            int candidateId = GetCandidateId();
            CandidateResponse view = _dataAccess.CandidateProfileView(candidateId);

            if (view != null)
            {
                lblFirstNameValue.Text = view.FirstName != null ? view.FirstName : null;
                lblLastNameValue.Text = view.LastName != null ? view.LastName : null;
                lblEmailValue.Text = view.EmailAddress != null ? view.EmailAddress : null;
                lblPhoneValue.Text = view.MobileNumber != null ? view.MobileNumber : null;
                lblAddressValue.Text = view.Address != null ? view.Address : null;
                lblCityValue.Text = view.City != null ? view.City : null;
                lblStateValue.Text = view.StateOrProvince != null ? view.StateOrProvince : null;
                lblPostalCodeValue.Text = view.PostalOrZipCode != null ? view.PostalOrZipCode : null;
                lblCountryValue.Text = view.Country != null ? view.Country : null;
                lblHighestEducationValue.Text = view.HighestEducationLevel != null ? view.HighestEducationLevel : null;
                // Education Details
                //if (view.Educations != null && view.Educations.Count > 0)
                //{
                //    var firstEducation = view.Educations.First();
                //    lblDegreeValue.Text = firstEducation.Degree;
                //    lblCollegeUniversityNameValue.Text = firstEducation.CollegeUniversityName;
                //    lblPlaceAddressValue.Text = firstEducation.PlaceAddress;
                //    lblGraduatedOrPursuingValue.Text = firstEducation.GraduatedOrPursuing;
                //    lblKeySkillsValue.Text = firstEducation.KeySkills;
                //    lblAcademicProjectValue.Text = firstEducation.AcademicProject;
                //}
                if (view.Educations != null && view.Educations.Count > 0)
                {
                    rptEducationDetails.DataSource = view.Educations != null ? view.Educations : null;
                    rptEducationDetails.DataBind();
                }
                // Work Experience
                if (view.Experiences != null && view.Experiences.Count > 0)
                {
                  
                    rptWorkExperience.DataSource = view.Experiences != null ? view.Experiences : null;
                    rptWorkExperience.DataBind();
                }
                // Assuming view.Skill is a list of Skill objects
                if (view.Skill != null && view.Skill.Any())
                {
                    // Ensure CoreSkills is a string or convert to string if needed
                    lblCoreSkillValue.Text = string.Join(", ",
                        view.Skill
                            .Where(a => a != null && a.CoreSkills != null) // Check if item and CoreSkills are not null
                            .Select(a => a.CoreSkills.ToString())
                            .Where(s => !string.IsNullOrEmpty(s))
                    );

                    // Convert boolean CoreSkillPercentages to string
                    lblCoreSkillPercentageValue.Text = string.Join(", ",
                        view.Skill
                            .Where(a => a != null && a.CoreSkillPercentage != null) // Check if item and CoreSkillPercentages are not null
                            .Select(a => a.CoreSkillPercentage.ToString())
                            .Where(p => !string.IsNullOrEmpty(p))
                    );

                    // Ensure SoftSkill is a string or convert to string if needed
                    lblSoftSkillValue.Text = string.Join(", ",
                        view.Skill
                            .Where(a => a != null && a.SoftSkills != null) // Check if item and SoftSkill are not null
                            .Select(a => a.SoftSkills.ToString())
                            .Where(s => !string.IsNullOrEmpty(s))
                    );

                    // Convert boolean SoftSkillPercentages to string
                    lblSoftSkillPercentageValue.Text = string.Join(", ",
                        view.Skill
                            .Where(a => a != null && a.SoftSkillpercentage != null) // Check if item and SoftSkillPercentages are not null
                            .Select(a => a.SoftSkillpercentage.ToString())
                            .Where(p => !string.IsNullOrEmpty(p))
                    );
                }
                else
                {
                    // Handle empty or null case
                    lblCoreSkillValue.Text = "No Core Skills";
                    lblCoreSkillPercentageValue.Text = "No Core Skill Percentages";
                    lblSoftSkillValue.Text = "No Soft Skills";
                    lblSoftSkillPercentageValue.Text = "No Soft Skill Percentages";
                }



                // Job Preferences
                lblJobTypesValue.Text = view.JobType != null ? view.JobType: null;
                lblJobLocationValue.Text = view.JobLocation != null ? view.JobLocation : null;
                lblAvailabilityValue.Text = view.Availability != null ? view.Availability : null;
                lblFreeTrainingValue.Text = view.WillingToTakeFreeTraining == true ? "YES" : "NO";
                lblPaidTrainingValue.Text = view.WillingToTakePaidTraining == true ? "YES" : "NO";
                lblCareerConsultantContactValue.Text = view.WillingToBeContactedByCareerConsultant == true ? "YES" : "NO";
                // Resume and Portfolio
                //lblResumeFile.Text = view.ResumeFilePath != null && view.ResumeFilePath != "" ? view.ResumeFilePath: null ; // Add this if you have a label for the resume
                lblCoverLetterValue.Text = view.CoverLetter != null ? view.CoverLetter : null;
                lblLinkedInProfileValue.Text = view.LinkedInProfile != null ? view.LinkedInProfile : null;
                lblPortfolioValue.Text = view.Portfolio != null ? view.Portfolio : null;
            }


        }
        private int GetCandidateId()
        {
            int candidateId = 0;
            if (Session["CandidateID"] != null)
            {
                candidateId = (int)Session["CandidateID"];
            }
            else if (Request.QueryString["CandidateID"] != null)
            {
                int.TryParse(Request.QueryString["CandidateID"], out candidateId);
            }
            return candidateId;
        }
        protected void btnEditProfile_Click(object sender, EventArgs e)
        {
            int candidateId = GetCandidateId();
            if (candidateId != 0)
            {


                Response.Redirect($"CandidateDashboard.aspx?CandidateID={candidateId}");
            }
            else
            {
                // Handle the case where the CandidateID is not available
                // For example, redirect to the login page
                Response.Redirect("CandidateLogin.aspx");
            }
        }
        protected void btnJobSearch_Click(object sender, EventArgs e)
        {
            int candidateId = GetCandidateId();
            if (candidateId != 0)
            {


                Response.Redirect($"CandidateJobSearch.aspx?CandidateID={candidateId}");
            }
            else
            {
                // Handle the case where the CandidateID is not available
                // For example, redirect to the login page
                Response.Redirect("CandidateViewDashboard.aspx");
            }
        }
    }
}