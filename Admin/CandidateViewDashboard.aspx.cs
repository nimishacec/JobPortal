
using JobPortalWebApplication.DataBase;
using JobPortalWebApplication.Models.Response;
using System.IO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JobPortalWebApplication.Candidate;
using Org.BouncyCastle.Utilities;
using System.Security.Policy;
using static System.Net.WebRequestMethods;
using File = System.IO.File;
using iTextSharp.text.pdf;
using System.Security.Cryptography;
using System.Text;

namespace JobPortalWebApplication.Admin
{
    public partial class CandidateViewDashboard : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        public string AESKey;
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            AESKey = Global._AESKey;
            // int candidateId = GetCandidateId();
            if (Session["AdminID"] != null)
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["CandidateID"] != null)
                    {
                        string encryptedCandidateId = Request.QueryString["CandidateID"];
                        int decryptedCandidateId = Convert.ToInt32(Decrypt(encryptedCandidateId));

                        if (decryptedCandidateId != 0)
                        {
                            string resumeStatus = _dataAccess.GetResumeStatus(decryptedCandidateId);

                            lblRStatus.Text = resumeStatus;

                            switch (resumeStatus)
                            {
                                case "ACCEPTED":
                                    lblRStatus.CssClass = "text-success"; // Green for approved
                                    btnAcceptResume.Visible = false; // Hide the Accept button if already accepted
                                    btnRejectResume.Visible = true;
                                    break;
                                case "PENDING":
                                    lblRStatus.CssClass = "text-warning"; // Yellow for pending
                                    btnRejectResume.Visible = true;
                                    btnAcceptResume.Visible = true;
                                    break;
                                case "REJECTED":
                                    lblRStatus.CssClass = "text-danger"; // Red for rejected
                                    btnAcceptResume.Visible = true; // Hide the Accept button if already accepted
                                    btnRejectResume.Visible = false;
                                    break;
                                case "":
                                    btnAcceptResume.Visible = false;
                                    btnRejectResume.Visible = false;
                                    break;
                            }
                            //if (resumeStatus == "ACCEPTED")
                            //{
                            //    btnAcceptResume.Visible = false; // Hide the Accept button if already accepted
                            //    btnRejectResume.Visible = true;  // Show the Reject button
                            //}
                            //else if (resumeStatus == "REJECTED")
                            //{
                            //    btnAcceptResume.Visible = true; // Hide the Accept button if already accepted
                            //    btnRejectResume.Visible = false;  // Show the Reject button
                            //}

                            //else
                            //{
                            //    btnAcceptResume.Visible = true;
                            //    btnRejectResume.Visible = true;
                            //}
                            LoadCandidateProfile(resumeStatus, decryptedCandidateId);
                        }
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
            protected void btnLinkedInProfile_Click(object sender, EventArgs e)
            {
            int candidateId = GetCandidateId();
                CandidateResponse view = _dataAccess.CandidateProfileView(candidateId);

            string linkedInProfileUrl = "https://www.linkedin.com/in/nimisha-mohanan-05205217b/"; // view.LinkedInProfile;

            if (!string.IsNullOrEmpty(linkedInProfileUrl))
            {
                // Redirect to the LinkedIn profile
                Response.Redirect(linkedInProfileUrl);
            }
            else
            {
                // Handle the case where no LinkedIn profile URL is found
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('LinkedIn profile not found.');", true);
            }
        }

            private void LoadCandidateProfile(string resumeStatus, int candidateId)
            {
                // int candidateId = GetCandidateId();
                CandidateResponse view = _dataAccess.CandidateProfileView(candidateId);

                if (view != null)
                {
                    lblFullName.Text = view.FirstName + " " + view.LastName;
                    lblName.Text = view.FirstName + " " + view.LastName;
                    // lblAddress.Text = view.Address;
                    lblState1.Text = view.StateOrProvince;
                    //lblFirstNameValue.Text = view.FirstName;
                    //lblLastNameValue.Text = view.LastName;
                    lblEmail1.Text = view.EmailAddress;
                    lblPhone1.Text = view.MobileNumber;
                    lblAddress1.Text = view.Address;
                    lblCity1.Text = view.City;
                    lblPostalCode1.Text = view.PostalOrZipCode;
                    lblCountry1.Text = view.Country;
                    //lblStateValue.Text = view.StateOrProvince;
                    //lblPostalCodeValue.Text = view.PostalOrZipCode;
                    //lblCountryValue.Text = view.Country;
                    lblHighest.Text = view.HighestEducationLevel;
                    lblJobType.Text = view.JobType;
                    lblLocation.Text = view.JobLocation;
                    lblAvail.Text = view.Availability;
                    lblFreeTraing.Text = view.WillingToTakeFreeTraining == true ? "YES" : "NO";
                    lblPaidTrainig.Text = view.WillingToTakePaidTraining == true ? "YES" : "NO";
                    lblCareerConsult.Text = view.WillingToBeContactedByCareerConsultant == true ? "YES" : "NO";
                    lblRStatus.Text = resumeStatus;
                    //lblCoverLetterValue.Text = view.CoverLetter;
                    //lblLinkedInProfileValue.Text = view.LinkedInProfile;
                    //lblPortfolioValue.Text = view.Portfolio;
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
                        rptEducationDetails1.DataSource = view.Educations;
                        rptEducationDetails1.DataBind();
                    }
                    else
                    {
                        lblEducationDetails1.Text = " No Education Details added ";
                        lblEducationDetails1.Visible = true;
                    }
                    // Work Experience
                    if (view.Experiences != null && view.Experiences.Count > 0)
                    {
                        rptExperienceDetails1.DataSource = view.Experiences;
                        rptExperienceDetails1.DataBind();
                        //var firstExperience = view.Experiences.First();
                        //lblCompanyNameValue.Text = firstExperience.CompanyName;
                        //lblCompanyAddressValue.Text = firstExperience.CompanyAddress;
                        //lblDesignationValue.Text = firstExperience.Designation;
                        //lblKeySkillsPracticedValue.Text = firstExperience.KeySkillsPracticed;
                    }
                    else
                    {
                        lblExperience1.Text = " No Experiences added";
                        lblExperience1.Visible = true;
                    }
                    if (view.Skill.Any(a => a.CoreSkills != null))
                    {
                        var coreSkills = view.Skill
                           .Where(s => s.CoreSkills != null && s.SoftSkills == "")
                           .Select(s => new
                           {
                               SkillName = s.CoreSkills,
                               SkillPercentage = s.CoreSkillPercentage // Assuming percentage exists
                           }).ToList();

                        if (coreSkills.Any())
                        {
                            rptCoreSkills.DataSource = coreSkills;
                            rptCoreSkills.DataBind();
                        }

                    }
                    else
                    {
                        lblCoreskill1.Text = " No Core Skills added";
                        lblCoreskill1.Visible = true;
                    }
                    if (view.Skill.Any(a => a.SoftSkills != null))
                    {
                        var softSkills = view.Skill
                              .Where(s => s.SoftSkills != null && s.CoreSkills == "")
                              .Select(s => new
                              {
                                  SkillName = s.SoftSkills,
                                  SkillPercentage = s.SoftSkillpercentage // Assuming percentage exists
                              }).ToList();

                        if (softSkills.Any())
                        {
                            rptSoftSkills.DataSource = softSkills;
                            rptSoftSkills.DataBind();
                        }
                    }
                    else
                    {
                        lblSoftskill1.Text = " No soft Skills added";
                        lblSoftskill1.Visible = true;
                    }

                    //if (view.Skill != null && view.Skill.Any())
                    //{
                    //    lblCoreSkillValue.Text = string.Join(", ",
                    //        view.Skill
                    //            .Where(a => a != null && a.CoreSkills != null)
                    //            .Select(a => a.CoreSkills.ToString())
                    //            .Where(s => !string.IsNullOrEmpty(s))
                    //    );
                    //    lblCoreSkillPercentageValue.Text = string.Join(", ",
                    //        view.Skill
                    //            .Where(a => a != null && a.CoreSkillPercentage != null)
                    //            .Select(a => a.CoreSkillPercentage.ToString())
                    //            .Where(p => !string.IsNullOrEmpty(p))
                    //    );
                    //    lblSoftSkillValue.Text = string.Join(", ",
                    //        view.Skill
                    //            .Where(a => a != null && a.SoftSkills != null)
                    //            .Select(a => a.SoftSkills.ToString())
                    //            .Where(s => !string.IsNullOrEmpty(s))
                    //    );
                    //    lblSoftSkillPercentageValue.Text = string.Join(", ",
                    //        view.Skill
                    //            .Where(a => a != null && a.SoftSkillpercentage != null)
                    //            .Select(a => a.SoftSkillpercentage.ToString())
                    //            .Where(p => !string.IsNullOrEmpty(p))
                    //    );
                    //}
                    //else
                    //{
                    //    // Handle empty or null case
                    //    lblCoreSkillValue.Text = "No Core Skills";
                    //    lblCoreSkillPercentageValue.Text = "No Core Skill Percentages";
                    //    lblSoftSkillValue.Text = "No Soft Skills";
                    //    lblSoftSkillPercentageValue.Text = "No Soft Skill Percentages";
                    //}
                    // Job Preferences

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
                string encryptedCandidateId = Request.QueryString["CandidateID"];
                candidateId = Convert.ToInt32(Decrypt(encryptedCandidateId));

            }
            return candidateId;
        }
        protected void btnDownloadResume_Click(object sender, EventArgs e)
            {
                int CandidateId = 0;
                CandidateId = GetCandidateId();

                string resumePath = GetResumePathFromDatabase(CandidateId);
            string networkFilePath = $@"\\\projects-blueblocks\Nimisha\JobPortal - New\uploads\{resumePath}";

            // Check if the file exists
            if (File.Exists(networkFilePath))
            {
                // Set up the download response
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", $"attachment; filename={Path.GetFileName(networkFilePath)}");
                Response.TransmitFile(networkFilePath);
                Response.End();
            }
            else
            {
                // Handle the case where the file is not found
                Response.Write("<script>alert('Resume file not found.');</script>");
            }

            //      string encodedResumePath = Path.Combine("C:\\Users\\ThinkPad\\source\\repos\\JobPortalWebApplication\\uploads", resumePath);

            //      string downloadUrl = $"~/Employer/DownloadFile.aspx?file={encodedResumePath}&CandidateID={CandidateId}";
            //      Response.Redirect(downloadUrl);
            //}
        }
            private string GetResumePathFromDatabase(int candidateId)
            {
                string resumePath = _dataAccess.GetResumePath(candidateId);
                return resumePath;

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
                    Response.Redirect("CandidateViewDashboard.aspx");
                }
            }
            protected void btnAcceptResume_Click(object sender, EventArgs e)
            {
                int candidateId = GetCandidateId();
                //lblResumeStatus.Text = "Resume Status: ACCEPTED";
                btnAcceptResume.Visible = false;
                btnRejectResume.Visible = true;

                int resumeId = _dataAccess.GetResumeIdFromDatabase(candidateId); // Replace with actual logic to retrieve ResumeID

                bool result = _dataAccess.UpdateResumeStatus(resumeId, "ACCEPTED");
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "StatusAlert", "alert('Resume has been Accepted.');", true);
                lblRStatus.Text = "ACCEPTED";
                upCandidateView.Update();
                }
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Failed to Accept .');", true);

                //if (resumeStatus != "")
                //{
                //    if (resumeStatus == "Accepted" || resumeStatus == "Rejected" || resumeStatus == "Deleted")
                //    {
                //        // Show dialog box indicating the status
                //        ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Resume has already been {resumeStatus}.');", true);
                //    }
                //    else
                //    {
                //int resumeId = _dataAccess.GetResumeIdFromDatabase(candidateId); // Replace with actual logic to retrieve ResumeID
                //_dataAccess.UpdateResumeStatus(resumeId, "ACCEPTED");
                //ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Resume has  been Accepted.');", true);
                //        // Response.Write("Resume has been accepted.");
                //    }
                //}
                //else
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert(' the candidate has no resume.');", true);
                //}
            }

            // Reject Resume
            protected void btnRejectResume_Click(object sender, EventArgs e)
            {
                int candidateId = GetCandidateId();
                // lblResumeStatus.Text = "Resume Status: REJECTED";
                btnAcceptResume.Visible = true;
                btnRejectResume.Visible = false;
                int resumeId = _dataAccess.GetResumeIdFromDatabase(candidateId); // Replace with actual logic to retrieve ResumeID
                bool result = _dataAccess.UpdateResumeStatus(resumeId, "REJECTED");
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "StatusAlert", "alert('Resume has been Rejected.');", true);
                lblRStatus.Text = "REJECTED";
                upCandidateView.Update();
                }
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "StatusAlert", $"alert('Failed to Reject .');", true);
                //Response.Write("Resume has been rejected.");
                //}
            }

            // Delete Resume
            protected void btnDeleteResume_Click(object sender, EventArgs e)
            {
                int candidateId = GetCandidateId();
                int resumeId = _dataAccess.GetResumeIdFromDatabase(candidateId); // Replace with actual logic to retrieve ResumeID
                _dataAccess.DeleteResume(resumeId);
                Response.Write("Resume has been deleted.");
            }
        }
    }