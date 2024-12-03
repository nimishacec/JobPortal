using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JobPortalWebApplication.DataBase;
using System.Data.Common;
using System.Configuration;

using JobPortalWebApplication.Models.Request;
using JobPortalWebApplication.Models.Response;
using System.Net.NetworkInformation;
using iTextSharp.text.pdf;
using System.Security.Cryptography;
using System.Text;
using JobPortalWebApplication.Candidate;


namespace JobPortalWebApplication.Admin
{
    public partial class CandidateDashboard : System.Web.UI.Page
    {
        public DataAccess _dataAccess;
        private string AESKey;
        private List<EducationList> educationDetailsList = new List<EducationList>();
        private List<WorkExperienceList> workExperiencesList = new List<WorkExperienceList>();
        private List<SkillList> skillLists = new List<SkillList>();


        protected void Page_Load(object sender, EventArgs e)
        {
            _dataAccess = Global.DataAccess;
            AESKey = Global._AESKey.ToString();
            string constr = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            if (Session["AdminID"] != null)
            {
                int AdminID = (int)Session["AdminID"];
                if (!IsPostBack)
                {
                    if (Request.QueryString["CandidateID"] != null)
                    {
                        string encryptedCandidateId = Request.QueryString["CandidateID"];
                        int decryptedCandidateId = Convert.ToInt32(Decrypt(encryptedCandidateId));
                        hiddenFieldCandidateId.Value = decryptedCandidateId.ToString();

                        BindPlanTypes();
                            BindCountry();
                            BindCoreSkills(ddlCoreSkill);
                            GetCountryId(decryptedCandidateId);
                            BindDropdowns(decryptedCandidateId);
                            //BindDegrees();
                            BindSkills(decryptedCandidateId);
                            BindCoreSkills(decryptedCandidateId);
                            BindSoftSkills(decryptedCandidateId);
                            BindEducationDetails(decryptedCandidateId);
                            BindDegreeDropdowns();
                            BindWorkExperience(decryptedCandidateId);
                            LoadCandidateProfile(decryptedCandidateId);
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
        protected void btnAddSkill_Click(object sender, EventArgs e)
        {
            // Collect Core Skills
            var coreSkills = CollectSkills(rptCoreSkills, "Core");

            // Collect Soft Skills
            var softSkills = CollectSkills(rptSoftSkills, "Soft");

            // Optionally, you could also handle the collected data (e.g., save it to a database)
            // For demonstration, we'll just rebind the repeaters to show updated data

            //BindRepeater(rptCoreSkills, coreSkills);
            //BindRepeater(rptSoftSkills, softSkills);
        }
        //private void BindEducationDetails()
        //{
        //    educationDetailsList.Add(new EducationDetails()); // Add an empty item to initialize the repeater
        //    rptEducationDetails.DataSource = educationDetailsList;
        //    rptEducationDetails.DataBind();

        //    // Bind degrees to dropdown lists in the repeater
        //    foreach (RepeaterItem item in rptEducationDetails.Items)
        //    {
        //        DropDownList ddlDegreeId = item.FindControl("ddlDegreeId") as DropDownList;
        //        BindDegrees(ddlDegreeId);
        //    }
        //}
        private void BindCoreSkills(int CandidateId)
        {
            var corekills = _dataAccess.GetCoreSkill(CandidateId);
            rptCoreSkills.DataSource = corekills;
            rptCoreSkills.DataBind();

            // Bind degrees to dropdown lists in the repeater
            foreach (RepeaterItem item in rptCoreSkills.Items)
            {
                DropDownList ddlCoreSkill = item.FindControl("ddlCoreSkill") as DropDownList;
                BindCoreSkills(ddlCoreSkill);
            }
        }
        
        private void BindSoftSkills(int CandidateId)
        {
            var softskills = _dataAccess.GetSoftSkill(CandidateId);
            rptSoftSkills.DataSource = softskills;
            rptSoftSkills.DataBind();

            // Bind degrees to dropdown lists in the repeater
            foreach (RepeaterItem item in rptSoftSkills.Items)
            {
                DropDownList ddlSoftSkill = item.FindControl("ddlSoftSkill") as DropDownList;
                BindSoftSkills(ddlSoftSkill);
            }
        }
        private void BindWorkExperience()
        {
            workExperiencesList.Add(new WorkExperienceList()); // Add an empty item to initialize the repeater
            rptWorkExperience.DataSource = workExperiencesList;
            rptWorkExperience.DataBind();
        }

        //private void BindDegrees()
        //{
        //    var degrees = _dataAccess.GetAllDegrees();
        //    ddlDegreeId.DataSource = degrees;
        //    ddlDegreeId.DataTextField = "DegreeName";
        //    ddlDegreeId.DataValueField = "DegreeId";
        //    ddlDegreeId.DataBind();
        //    ddlDegreeId.Items.Insert(0, new ListItem("Select Degree", ""));
        //}
        private void BindCoreSkills(DropDownList ddlCoreSkill)
        {
            var coreskill = _dataAccess.GetCoreSkills();

            if (ddlCoreSkill != null)  // Always check for null
            {
                ddlCoreSkill.DataSource = coreskill;
                ddlCoreSkill.DataTextField = "CoreSkills";
                ddlCoreSkill.DataValueField = "Id";
                ddlCoreSkill.DataBind();
                ddlCoreSkill.Items.Insert(0, new ListItem("Select  Core Skills", "0"));
            }
        }
        private void BindSoftSkills(DropDownList ddlSoftSkill)
        {
            var softskill = _dataAccess.GetSoftSkills();
            if (softskill.Count() > 0)
            {
                ddlSoftSkill.DataSource = softskill;
                ddlSoftSkill.DataTextField = "SoftSkills";
                ddlSoftSkill.DataValueField = "Id";
                ddlSoftSkill.DataBind();
                ddlSoftSkill.Items.Insert(0, new ListItem("Select Soft Skills", "0"));
            }
        }
        private void BindCountry()
        {
            var country = _dataAccess.GetAllCountry();
            ddlCountry.DataSource = country;
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryID";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("Select Country", ""));
        }
        private void GetCountryId(int CandidateId)
        {

            int? countryId = _dataAccess.GetCountryId(CandidateId);


            if (countryId.HasValue && ddlCountry.Items.FindByValue(countryId.Value.ToString()) != null)
            {
                // If a valid CountryID is found, set it as the selected value
                ddlCountry.SelectedValue = countryId.Value.ToString();
            }
            else
            {
                // If the CountryID is null or not found, set the default value (like "Select Country")
                ddlCountry.SelectedIndex = 0; // Or you can leave it as the first item
            }

        }
        private void GetJobTypeId(int CandidateId)
        {

            int? JobTypeId = _dataAccess.GetJobTypeId(CandidateId);


            if (JobTypeId.HasValue && ddlJobTypes.Items.FindByValue(JobTypeId.Value.ToString()) != null)
            {
                // If a valid CountryID is found, set it as the selected value
                ddlJobTypes.SelectedValue = JobTypeId.Value.ToString();
            }
            else
            {
                // If the CountryID is null or not found, set the default value (like "Select Country")
                ddlJobTypes.SelectedIndex = 0; // Or you can leave it as the first item
            }

        }
        private void GetCoreSkillId(int CandidateId)
        {

            //List<SkillList> = _dataAccess.GetCoreSkill(CandidateId);


            //if (skillId.HasValue && ddlCoreSkill.Items.FindByValue(skillId.Value.ToString()) != null)
            //{
            //    // If a valid CountryID is found, set it as the selected value
            //    ddlCoreSkill.SelectedValue = skillId.Value.ToString();
            //}
            //else
            //{
            //    // If the CountryID is null or not found, set the default value (like "Select Country")
            //    ddlCoreSkill.SelectedIndex = 0; // Or you can leave it as the first item
            //}

        }
        private void GetJobLocationId(int CandidateId)
        {

            int? JobLocationId = _dataAccess.GetJobLocationId(CandidateId);


            if (JobLocationId.HasValue && ddlJobLocation.Items.FindByValue(JobLocationId.Value.ToString()) != null)
            {
                // If a valid CountryID is found, set it as the selected value
                ddlJobLocation.SelectedValue = JobLocationId.Value.ToString();
            }
            else
            {
                // If the CountryID is null or not found, set the default value (like "Select Country")
                ddlJobLocation.SelectedIndex = 0; // Or you can leave it as the first item
            }

        }
        private void GetAvailabilityId(int CandidateId)
        {

            int? AvailabilityId = _dataAccess.GetAvailabilityId(CandidateId);


            if (AvailabilityId.HasValue && ddlAvailability.Items.FindByValue(AvailabilityId.Value.ToString()) != null)
            {
                // If a valid CountryID is found, set it as the selected value
                ddlAvailability.SelectedValue = AvailabilityId.Value.ToString();
            }
            else
            {
                // If the CountryID is null or not found, set the default value (like "Select Country")
                ddlAvailability.SelectedIndex = 0; // Or you can leave it as the first item
            }

        }
        //protected void btnAddEducation_Click(object sender, EventArgs e)
        //{

        //    educationDetailsList.Add(new EducationDetails());
        //    BindEducationDetails();
        //}

        //protected void btnAddWorkExperience_Click(object sender, EventArgs e)
        //{
        //    workExperiencesList.Add(new WorkExperiences());
        //    BindWorkExperience();
        //}
        //protected void btnAddCoreSkill_Click(object sender, EventArgs e)
        //{
        //    skillLists.Add(new SkillList());
        //    BindCoreSkills();
        //}
        //protected void btnAddSoftSkill_Click(object sender, EventArgs e)
        //{
        //    skillLists.Add(new SkillList());
        //    BindSoftSkills();
        //}
        private void BindDropdowns(int candidateId)
        {
            BindJobTypes();
            GetJobTypeId(candidateId);
            BindJobLocations();
            GetJobLocationId(candidateId);
            BindAvailability();
            GetAvailabilityId(candidateId);
        }

        private void BindJobTypes()
        {
            var jobTypes = _dataAccess.GetAllJobTypes();
            ddlJobTypes.DataSource = jobTypes;
            ddlJobTypes.DataTextField = "JobName";
            ddlJobTypes.DataValueField = "JobTypeID";
            ddlJobTypes.DataBind();
            ddlJobTypes.Items.Insert(0, new ListItem("Select Job Type", ""));
        }

        private void BindJobLocations()
        {
            var jobLocations = _dataAccess.GetAllJobLocation();
            ddlJobLocation.DataSource = jobLocations;
            ddlJobLocation.DataTextField = "LocationName";
            ddlJobLocation.DataValueField = "JobLocationID";
            ddlJobLocation.DataBind();
            ddlJobLocation.Items.Insert(0, new ListItem("Select Job Location", ""));
        }

        private void BindAvailability()
        {
            var availabilityOptions = _dataAccess.GetAllAvailability();
            ddlAvailability.DataSource = availabilityOptions;
            ddlAvailability.DataTextField = "AvailabilityName";
            ddlAvailability.DataValueField = "AvailabilityID";
            ddlAvailability.DataBind();
            ddlAvailability.Items.Insert(0, new ListItem("Select Availability", ""));
        }
        private void LoadCandidateProfile(int candidateId)
        {
            // int candidateId = GetCandidateId();
            CandidateRequest view = _dataAccess.CandidateView(candidateId);

            if (view != null)
            {
                txtFirstName.Text = view.FirstName != null ? view.FirstName.ToString() : null;
                txtLastName.Text = view.LastName != null ? view.LastName.ToString() : null;
                txtEmail.Text = view.EmailAddress != null ? view.EmailAddress.ToString() : null;
                txtMobileNumber.Text = view.MobileNumber != null ? view.MobileNumber.ToString() : null;
                txtAddress.Text = view.Address != null ? view.Address.ToString() : null;
                txtCity.Text = view.City != null ? view.City.ToString() : null;
                txtState.Text = view.StateOrProvince != null ? view.StateOrProvince.ToString() : null;
                txtPostal.Text = view.PostalOrZipCode != null ? view.PostalOrZipCode.ToString() : null;
                //ddlCountry.SelectedValue = view.CountryID.ToString();
                // Setting Highest Education Level and Plan
                txtHighestEducationLevel.Text = view.HighestEducationLevel != null ? view.HighestEducationLevel.ToString() : null;
                ddlPlan.SelectedValue = view.PlanId != null ? view.PlanId.ToString() : null;

                // Populate Education Details
                if (view.Education != null && view.Education.Count > 0)
                {
                    rptEducationDetails.DataSource = view.Education;
                    rptEducationDetails.DataBind();
                }


                // Populate Work Experience
                if (view.Experiences != null && view.Experiences.Count > 0)
                {
                    rptWorkExperience.DataSource = view.Experiences;
                    rptWorkExperience.DataBind();
                }


                // Populate Skills
                // Binding Core Skills
                //var coreSkills = view.Skills.CoreSkill.Select((skillId, index) => new
                //{
                //    SkillId = skillId,
                //    Percentage = view.Skills.CoreSkillPercentages[index]
                //}).ToList();

                //rptCoreSkills.DataSource = coreSkills;
                //rptCoreSkills.DataBind();

                // Binding Soft Skills
                //var softSkills = view.Skills.SoftSkillIds.Select((skillId, index) => new
                //{
                //    SkillId = skillId,
                //    Percentage = view.Skills.SoftSkillPercentages[index]
                //}).ToList();

                //rptSoftSkills.DataSource = softSkills;
                //rptSoftSkills.DataBind();


                // Other fields
                chkFreeTraining.Checked = view.FreeTraining == true ? true : false;
                chkPaidTraining.Checked = view.PaidTraining == true ? true : false;
                chkCareerConsultantContact.Checked = view.CareerConsultantContact == true ? true : false;
                txtCoverLetter.Text = view.CoverLetter != null ? view.CoverLetter.ToString() : null;
                txtLinkedInProfile.Text = view.LinkedInProfile != null ? view.LinkedInProfile.ToString() : null;
                txtPortfolio.Text = view.Portfolio != null ? view.Portfolio.ToString() : null;
                //Jobpreference---
                //ddlJobTypes.Text = view.JobType;
                //ddlJobLocation.Text = view.JobLocation;
                //ddlAvailability.Text = view.Availability;


                //  lblResume.Text = reader["ResumeFile"].ToString();
                //txtCoverLetter.Text = view.CoverLetter != null ? view.CoverLetter.ToString() : null;
                //txtLinkedInProfile.Text = view.LinkedInProfile != null ? view.LinkedInProfile.ToString() : null;
                //txtPortfolio.Text = view.Portfolio != null ? view.Portfolio.ToString() : null;
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
           int candidateId =Convert.ToInt32(hiddenFieldCandidateId.Value);
            var candidate = new CandidateRequest();
            string planIdString = ddlPlan.SelectedValue;

            if (int.TryParse(planIdString, out int planId))
            {
                candidate.PlanId = planId;
            }
            candidate.Address = txtAddress.Text.ToString();
            candidate.City = txtCity.Text.ToString();
            candidate.StateOrProvince = txtState.Text.ToString();
            candidate.PostalOrZipCode = txtPostal.Text.ToString();
            string CountryID = ddlCountry.SelectedValue != null ? ddlCountry.SelectedValue.ToString() : null;
            if (CountryID != "")
            {
                if (int.TryParse(CountryID, out int CountryId))
                {

                    candidate.CountryID = CountryId;

                }
            }
            if (candidate.SkillLists == null)
            {
                candidate.SkillLists = new List<SkillList>();
            }

            SkillList newSkill = new SkillList
            {
                CoreSkills = ddlCoreSkill.SelectedValue != null ? ddlCoreSkill.SelectedItem.Text : string.Empty,
                SoftSkills = ddlSoftSkill.SelectedValue != null ? ddlSoftSkill.SelectedItem.Text : string.Empty,
                
            };

            candidate.SkillLists.Add(newSkill);
            candidate.HighestEducationLevel = txtHighestEducationLevel.Text.ToString();
            // Collect complex fields
            candidate.Education = _dataAccess.GetEducations(candidateId);
            // candidate.Educations = CollectEducationDetails();
            candidate.Experiences = _dataAccess.GetWorkExperience(candidateId);
            // candidate.SkillLists=_dataAccess.GetSkillsFromDB(candidateId);
            // Collect additional fields
            //candidate.Skills = new Skills
            //{
            //    CoreSkill = true, // Assuming true if using CoreSkill
            //    CoreSkillIds = CollectSkills(rptCoreSkills, "Core").Select(s => s.Id).ToList(),
            //    CoreSkillPercentages = CollectSkills(rptCoreSkills, "Core").Select(s => s.Percentage).ToList(),
            //    SoftSkill = true, // Assuming true if using SoftSkill
            //    SoftSkillIds = CollectSkills(rptSoftSkills, "Soft").Select(s => s.Id).ToList(),
            //    SoftSkillPercentages = CollectSkills(rptSoftSkills, "Soft").Select(s => s.Percentage).ToList()

            //};

            int JobTypesID = 0;

            if (!string.IsNullOrEmpty(ddlJobTypes.SelectedValue))
            {

                int.TryParse(ddlJobTypes.SelectedValue, out JobTypesID);
            }
            int JobLocationID = 0;
            if (!string.IsNullOrEmpty(ddlJobLocation.SelectedValue))
            {

                int.TryParse(ddlJobLocation.SelectedValue, out JobLocationID);
            }
            int AvailabilityID = 0;
            if (!string.IsNullOrEmpty(ddlAvailability.SelectedValue))
            {

                int.TryParse(ddlAvailability.SelectedValue, out AvailabilityID);
            }

            candidate.JobTypesID = JobTypesID;
            candidate.JobLocationID = JobLocationID;
            candidate.AvailabilityID = AvailabilityID;
            candidate.FreeTraining = chkFreeTraining.Checked;
            candidate.PaidTraining = chkPaidTraining.Checked;
            candidate.CareerConsultantContact = chkCareerConsultantContact.Checked;
            candidate.ResumeFile = fuResumeFile.HasFile ? fuResumeFile.PostedFile : null;
            candidate.CoverLetter = txtCoverLetter.Text.ToString();
            candidate.LinkedInProfile = txtLinkedInProfile.Text.ToString();
            candidate.Portfolio = txtPortfolio.Text.ToString();


            // Save candidate data to the database
            string message = _dataAccess.CandidateUpdate(candidate, candidateId);
            if (message.Contains("Successfully updated"))
            {
                string script = "alert('Updated successfully!'); window.location.href='CandidateList.aspx';";
                ClientScript.RegisterStartupScript(this.GetType(), "alertRedirect", script, true);
            }
            else
            {
                string urlEncodedEmployeeId = Server.UrlEncode(candidateId.ToString());
                string script = "alert('Updated failed!'); window.location.href='CandidateDashboard.aspx?CandidateID={urlEncodedEmployeeId}';";
                ClientScript.RegisterStartupScript(this.GetType(), "alertRedirect", script, true);
                

            }
            // Provide feedback to the user

        }
        private List<SkillSelection> CollectSkills()
        {
            var collectedSkills = new List<SkillSelection>();

            foreach (RepeaterItem item in rptCoreSkills.Items)
            {
                var ddlSkill = item.FindControl("ddlCoreSkill") as DropDownList;
                var txtPercentage = item.FindControl("txtCoreSkillPercentage") as TextBox;

                if (ddlSkill != null && txtPercentage != null)
                {
                    int skillId = int.Parse(ddlSkill.SelectedValue);
                    float percentage = float.Parse(txtPercentage.Text);

                    collectedSkills.Add(new SkillSelection
                    {
                        SkillId = skillId,
                        Percentage = percentage
                    });
                }
            }

            return collectedSkills;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CandidateList.aspx");
        }
        private void BindSkills(int candidateId)
        {
            // Assuming you have methods to get core and soft skills from the database
            var coreSkills = _dataAccess.GetCoreSkills();
            var softSkills = _dataAccess.GetSoftSkills();
            //BindRepeater(rptCoreSkills, coreSkills);
            //BindRepeater(rptSoftSkills, softSkills);
            BindCoreSkills(ddlCoreSkill);
            BindSoftSkills(ddlSoftSkill);
            //ddlCoreSkill.DataSource = coreSkills;
            //ddlCoreSkill.DataTextField = "CoreSkills";
            //ddlCoreSkill.DataValueField = "Id";
            //ddlCoreSkill.DataBind();
            //ddlCoreSkill.Items.Insert(0, new ListItem("-- Select a Skill --", "0"));

            //ddlSoftSkill.DataSource = softSkills;
            //ddlSoftSkill.DataTextField = "SoftSkills";
            //ddlSoftSkill.DataValueField = "Id";
            //ddlSoftSkill.DataBind();
            //ddlSoftSkill.Items.Insert(0, new ListItem("Select SoftSkill", ""));

            

        }

        //private void BindRepeater(Repeater repeater, List<SkillList> skills)
        //{
        //    repeater.DataSource = skills;
        //    repeater.DataBind();
        //}
        private List<SkillList> CollectSkills(Repeater repeater, string skillType)
        {
            var skills = new List<SkillList>();

            foreach (RepeaterItem item in repeater.Items)
            {
                var ddlSkill = item.FindControl("ddl" + skillType + "Skill") as DropDownList;
                var txtPercentage = item.FindControl("txt" + skillType + "SkillPercentage") as TextBox;

                if (ddlSkill != null && txtPercentage != null)
                {
                    int skillId = int.Parse(ddlSkill.SelectedValue);
                    float percentage = float.Parse(txtPercentage.Text);

                    skills.Add(new SkillList
                    {
                        Id = skillId,
                        Percentage = percentage
                    });
                }
            }

            return skills;
        }

        //private List<EducationDetails> CollectEducationDetails()
        //{
        //    var educationDetails = new List<EducationDetails>();

        //    foreach (RepeaterItem item in rptEducationDetails.Items)
        //    {
        //        var ddlDegreeId = item.FindControl("ddlDegreeId") as DropDownList;
        //        var txtCollegeUniversityName = item.FindControl("txtCollegeUniversityName") as TextBox;
        //        var txtPlaceAddress = item.FindControl("txtPlaceAddress") as TextBox;
        //        var txtGraduatedOrPursuing = item.FindControl("txtGraduatedOrPursuing") as TextBox;
        //        var txtKeySkills = item.FindControl("txtKeySkills") as TextBox;
        //        var txtAcademicProject = item.FindControl("txtAcademicProject") as TextBox;

        //        var educationDetail = new EducationDetails
        //        {
        //            DegreeId = !string.IsNullOrEmpty(ddlDegreeId?.SelectedValue) ?
        //                new List<int?> { int.Parse(ddlDegreeId.SelectedValue) } :
        //                new List<int?>(),
        //            CollegeUniversityName = new List<string> { txtCollegeUniversityName?.Text },
        //            PlaceAddress = new List<string> { txtPlaceAddress?.Text },
        //            GraduatedOrPursuing = new List<string> { txtGraduatedOrPursuing?.Text },
        //            KeySkills = new List<string> { txtKeySkills?.Text },
        //            AcademicProject = new List<string> { txtAcademicProject?.Text }
        //        };

        //        educationDetails.Add(educationDetail);
        //    }

        //    return educationDetails;
        //}

        //private List<WorkExperiences> CollectWorkExperiences()
        //{
        //    var workExperiences = new List<WorkExperiences>();

        //    foreach (RepeaterItem item in rptWorkExperience.Items)
        //    {
        //        var txtCompanyName = item.FindControl("txtCompanyName") as TextBox;
        //        var txtCompanyAddress = item.FindControl("txtCompanyAddress") as TextBox;
        //        var txtDesignation = item.FindControl("txtDesignation") as TextBox;
        //        var txtKeySkillsPracticed = item.FindControl("txtKeySkillsPracticed") as TextBox;

        //        var work = new WorkExperiences
        //        {
        //            CompanyName = new List<string> { txtCompanyName.Text },
        //            CompanyAddress = new List<string> { txtCompanyAddress.Text },
        //            Designation = new List<string> { txtDesignation.Text },
        //            KeySkillsPracticed = new List<string> { txtKeySkillsPracticed.Text }
        //        };

        //        workExperiences.Add(work);
        //    }

        //    return workExperiences;
        //}

        private void BindPlanTypes()
        {
            var planTypes = _dataAccess.GetPlanTypes();
            ddlPlan.DataSource = planTypes;
            ddlPlan.DataTextField = "PlanName";
            ddlPlan.DataValueField = "PlanId";
            ddlPlan.DataBind();

            // Add a default item
            ddlPlan.Items.Insert(0, new ListItem("Select Plan Type", " "));
        }
        public List<EducationList> EducationList
        {
            get
            {
                if (ViewState["EducationList"] == null)
                    ViewState["EducationList"] = new List<EducationList>();
                return (List<EducationList>)ViewState["EducationList"];
            }
            set
            {
                ViewState["EducationList"] = value;
            }
        }
        private List<WorkExperienceList> WorkExperienceList
        {
            get
            {
                // Use session to maintain the work experience list
                return (List<WorkExperienceList>)Session["WorkExperienceList"] ?? new List<WorkExperienceList>();
            }
            set
            {
                Session["WorkExperienceList"] = value;
            }
        }
        private void BindDegreeDropdowns()
        {
            var degrees = _dataAccess.GetAllDegrees(); // Replace with actual data access method
            //foreach (RepeaterItem item in rptEducationDetails.Items)
            //{
            //    DropDownList ddlDegreeId = (DropDownList)item.FindControl("ddlDegreeId");
            //    ddlDegreeId.DataSource = degrees;
            //    ddlDegreeId.DataTextField = "DegreeName";
            //    ddlDegreeId.DataValueField = "DegreeId";
            //    ddlDegreeId.DataBind();
            //    ddlDegreeId.Items.Insert(0, new ListItem("Select Degree", ""));
            //}
            ddlNewDegreeId.DataSource = degrees;
            ddlNewDegreeId.DataTextField = "DegreeName";
            ddlNewDegreeId.DataValueField = "DegreeId";
            ddlNewDegreeId.DataBind();
            ddlNewDegreeId.Items.Insert(0, new ListItem("Select Degree", ""));
        }

        private void BindEducationDetails(int candidateId)
        {
            List<EducationList> educationDetails = _dataAccess.GetEducations(candidateId); // Replace with actual data access method
            rptEducationDetails.DataSource = educationDetails;
            rptEducationDetails.DataBind();
            BindDegreeDropdowns();
        }
        private void BindWorkExperience(int candidateId)
        {
            List<WorkExperienceList> workDetails = _dataAccess.GetWorkExperience(candidateId); // Replace with actual data access method
            rptWorkExperience.DataSource = workDetails;
            rptWorkExperience.DataBind();
            // BindDegreeDropdowns();
        }


        public List<SkillList> CoreSkills
        {
            get
            {
                if (ViewState["CoreSkills"] == null)
                {
                    ViewState["CoreSkills"] = new List<SkillList>();
                }
                return (List<SkillList>)ViewState["CoreSkills"];
            }
            set
            {
                ViewState["CoreSkills"] = value;
            }
        }
        public List<SkillList> SoftSkills
        {
            get
            {
                if (ViewState["SoftSkills"] == null)
                {
                    ViewState["SoftSkills"] = new List<SkillList>();
                }
                return (List<SkillList>)ViewState["SoftSkills"];
            }
            set
            {
                ViewState["SoftSkills"] = value;
            }
        }
        private Control FindControlRecursive(Control root, string id)
        {
            if (root.ID == id) return root;

            foreach (Control child in root.Controls)
            {
                Control found = FindControlRecursive(child, id);
                if (found != null) return found;
            }
            return null;
        }

        protected void btnAddCoreSkill_Click(object sender, EventArgs e)
        {
            int candidateId = Convert.ToInt32(hiddenFieldCandidateId.Value);
            // Get the current list of core skills
            var coreSkills = CoreSkills;

            int selectedSkillId = int.Parse(ddlCoreSkill.SelectedItem.Value);
            string selectedSkillName = ddlCoreSkill.SelectedItem.Text;

            float skillPercentage;
            if (float.TryParse(txtCoreSkillPercentage.Text, out skillPercentage))
            {
                // Create a new core skill object
                var newCoreSkill = new SkillList
                {
                    Id = selectedSkillId,
                    CoreSkills = selectedSkillName,
                    Percentage = skillPercentage
                };

                // Add the new skill to the list
                coreSkills.Add(newCoreSkill);

                // Update the ViewState
                CoreSkills = coreSkills;

                // Re-bind the repeater to display the updated list

                _dataAccess.AddCoreSkillToDatabase(newCoreSkill, candidateId);
                BindCoreSkills(candidateId);
            }

        }
        protected void btnAddSoftSkill_Click(object sender, EventArgs e)
        {

            int candidateId = Convert.ToInt32(hiddenFieldCandidateId.Value);
            // Get the current list of core skills
            var softSkills = SoftSkills;

            // Find the DropDownList and TextBox within the Repeater item template
            DropDownList ddlSoftSkill = (DropDownList)FindControlRecursive(this.Page, "ddlSoftSkill");
            TextBox txtSoftSkillPercentage = (TextBox)FindControlRecursive(this.Page, "txtSoftSkillPercentage");

            // Create a new core skill
            var newCoreSkill = new SkillList
            {
                Id = ddlSoftSkill.SelectedIndex,
                SoftSkills = ddlSoftSkill.SelectedItem.Text,  // Get selected skill name
                Percentage = int.Parse(txtSoftSkillPercentage.Text)  // Get percentage value
            };

            // Add the new skill to the list
            softSkills.Add(newCoreSkill);

            // Update the ViewState
            SoftSkills = softSkills;

            // Re-bind the repeater to display the updated list

            _dataAccess.AddSoftSkillToDatabase(newCoreSkill, candidateId);
            BindSoftSkills(candidateId);
        }
        protected void btnAddEducation_Click(object sender, EventArgs e)
        {
            int candidateId = GetCandidateId();
            string degreeId = ddlNewDegreeId.SelectedValue;
            string collegeName = txtNewCollegeUniversityName.Text;
            string address = txtNewPlaceAddress.Text;
            string graduated = txtNewGraduatedOrPursuing.Text;
            string keySkills = txtNewKeySkills.Text;
            string academicProject = txtNewAcademicProject.Text;

            _dataAccess.AddEducationDetail(degreeId, collegeName, address, graduated, keySkills, academicProject, candidateId); // Replace with your add method

            BindEducationDetails(candidateId); // Refresh the repeater
        }
        protected void btnCancelWorkExperience_Click(object sender, EventArgs e)
        {
            // Hide the form and reset
            // addExperienceForm.Visible = false;
            btnCancelWorkExperience.Visible = false;
            btnAddWorkExperience.Visible = true;  // Show the Add button
        }
        protected void btnAddWorkExperience_Click(object sender, EventArgs e)
        {
            int candidateId = GetCandidateId();
            // string degreeId = ddlNewDegreeId.SelectedValue;
            string companyName = txtCompanyName.Text!=null ? txtCompanyName.Text:  null ;
            string address = txtCompanyAddress.Text;
            string designation = txtDesignation.Text;
            string keySkills = txtkeySkills.Text;


            _dataAccess.AddExperienceDetail(companyName, address, designation, keySkills, candidateId); // Replace with your add method

            BindWorkExperience(candidateId); // Refresh the repeater  
        }
        protected void btnCancelEducation_Click(object sender, EventArgs e)
        {
            // Hide the form and reset
            // addExperienceForm.Visible = false;
            btnCancelEducation.Visible = false;
            btnAddEducation.Visible = true;  // Show the Add button
        }

        protected void btnCancelCoreSkill_Click(object sender, EventArgs e)
        {
            btnCancelCoreSkill.Visible = false;
            btnSaveCoreSkill.Visible = true;
        }

        protected void btnCancelSoftSkill_Click(object sender, EventArgs e)
        {
            btnCancelSoftSkill.Visible = false;
            btnSaveSoftSkill.Visible = true;
        }
        protected void rptEducationDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "Delete")
            {
                int candidateId = GetCandidateId();
                // Get the EducationId from the CommandArgument
                int educationId = Convert.ToInt32(e.CommandArgument);
                bool status = _dataAccess.DeleteEducationDetail(candidateId, educationId);
                if (status)
                {
                    BindEducationDetails(candidateId);
                }
            }
        }
        protected void rptWorkExperience_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int candidateId = GetCandidateId();
                // Get the EducationId from the CommandArgument
                int workId = Convert.ToInt32(e.CommandArgument);
                bool status = _dataAccess.DeleteExperienceDetail(candidateId, workId);
                if (status)
                {
                    BindWorkExperience(candidateId);
                }
            }
        }
        protected void rptCoreSkill_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int candidateId = Convert.ToInt32(hiddenFieldCandidateId.Value);
                // Get the EducationId from the CommandArgument
                int coreId = Convert.ToInt32(e.CommandArgument);
                bool status = _dataAccess.DeleteCoreSkill(candidateId, coreId);
                if (status)
                {
                    BindCoreSkills(candidateId);
                }
            }
        }
        protected void rptSoftSkill_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                  int candidateId =Convert.ToInt32(hiddenFieldCandidateId.Value);
                // Get the EducationId from the CommandArgument
                int softId = Convert.ToInt32(e.CommandArgument);
                bool status = _dataAccess.DeleteSoftSkill(candidateId, softId);
                if (status)
                {
                    BindSoftSkills(candidateId);
                }
            }
        }

        //protected void btnUpload_Click(object sender, EventArgs e)
        //{
        //    if (fileUploadResume.HasFile)
        //    {
        //        try
        //        {
        //            // Set the file path where the file will be saved
        //            string filePath = Server.MapPath("~/Uploads/") + fileUploadResume.FileName;

        //            // Save the file to the specified path
        //            fileUploadResume.SaveAs(filePath);

        //            // Optionally, show the filename in the text box
        //            txtFileName.Text = fileUploadResume.FileName;

        //            // Display success message or handle further logic
        //            // e.g., LabelSuccess.Text = "File uploaded successfully!";
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle errors (e.g., display error message)
        //            // LabelError.Text = "Error: " + ex.Message;
        //        }
        //    }
        //    else
        //    {
        //        // Handle case where no file is selected
        //        // LabelError.Text = "Please select a file to upload.";
        //    }
        //}






    }
}