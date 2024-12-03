<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CandidateDashboard.aspx.cs" MasterPageFile="~/Candidate/Candidate.Master" Inherits="JobPortalWebApplication.Candidate.CandidateDashboard" %>
  <asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
<script type="text/javascript">
    function showAddEducationForm() {
        var form = document.getElementById("addEducationForm");
        form.style.display = "block";
    }
    function showAddExperienceForm() {
        var form = document.getElementById("addExperienceForm");
        form.style.display = "block";
    }
    function showAddCoreSkillForm() {
        var form = document.getElementById("addCoreSkillForm");
        form.style.display = "block";
    }
    function showAddSoftSkillForm() {
        var form = document.getElementById("<%= addSoftSkillForm.ClientID %>");
        form.style.display = "block";
    }
    
  
</script>
      <style>
             .delete -btn {
      margin-top: 10px;  /* Adjust this value for top margin */
      margin-bottom: 10px;  /* Adjust this value for bottom margin */
  }
      </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="container">
        <!-- Personal Details Section -->
        <div class="row mb-4" style="background-color: aliceblue;">
            <div class="col-md-8">
                <h5>Personal Details</h5>
                <div class="form-group">
                    <asp:Label ID="lblFirstName" runat="server" Text="First Name:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlPlan" ErrorMessage="Country is required." CssClass="text-danger" Display="Dynamic" />
            
                </div>
                <div class="form-group">
                    <asp:Label ID="lblLastName" runat="server" Text="Last Name:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlPlan" ErrorMessage="Country is required." CssClass="text-danger" Display="Dynamic" />
            
                </div>
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="Email Address:" CssClass="form-label"></asp:Label>
                    <asp:Label ID="txtEmail" runat="server" CssClass="form-control"></asp:Label>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlPlan" ErrorMessage="Country is required." CssClass="text-danger" Display="Dynamic" />
            
                </div>
                <div class="form-group">
                    <asp:Label ID="lblMobileNumber" runat="server" Text="Mobile Number:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlPlan" ErrorMessage="Country is required." CssClass="text-danger" Display="Dynamic" />
             <asp:RegularExpressionValidator ID="revMobileNumber" runat="server" ControlToValidate="txtMobileNumber" ValidationExpression="^\d{10}$" ErrorMessage="Enter a valid 10-digit mobile number."
     CssClass="text-danger" Display="Dynamic" /><br />
    
                </div>
                <div class="form-group">
                    <asp:Label ID="Label3" runat="server" Text="Plan Types:" CssClass="form-label"></asp:Label>
                    <asp:Label ID="ddlPlanValue" runat="server" CssClass="form-label"></asp:Label><br />
                    <asp:DropDownList ID="ddlPlan" runat="server" CssClass="form-control"></asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPlan" ErrorMessage="Country is required." CssClass="text-danger" Display="Dynamic" />
                 
                </div>
                <div class="form-group">
                    <asp:Label ID="lblAddress" runat="server" Text="Address:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddress" ErrorMessage="Country is required." CssClass="text-danger" Display="Dynamic" />
                 
                </div>
                <div class="form-group">
                    <asp:Label ID="lblCity" runat="server" Text="City:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCity" ErrorMessage="Country is required." CssClass="text-danger" Display="Dynamic" />
                 
                </div>
                <div class="form-group">
                    <asp:Label ID="lblState" runat="server" Text="State/Province:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtState" runat="server" CssClass="form-control"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtState" ErrorMessage="Country is required." CssClass="text-danger" Display="Dynamic" />
                 
                </div>
                <div class="form-group">
                    <asp:Label ID="lblPostalOrZipCode" runat="server" Text="Postal/Zip Code:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtPostal" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfPostalOrZipCode" runat="server" ControlToValidate="txtPostal" ErrorMessage="P is required." CssClass="text-danger" Display="Dynamic" />
                    
                    <asp:RegularExpressionValidator ID="rvfPostalOrZipCode" runat="server" ControlToValidate="txtPostal" ValidationExpression="^\d{6}$" ErrorMessage="Enter a valid 6-digit Postal Code or Zip Code."
                        CssClass="text-danger" Display="Dynamic" /><br />

                </div>
                <div class="form-group">
                    <asp:Label ID="lblCountry" runat="server" Text="Country:" CssClass="form-label"></asp:Label>
                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" ></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rvfddlCountry" runat="server" ControlToValidate="ddlCountry" ErrorMessage="Country is required." CssClass="text-danger" Display="Dynamic" />
                   
                </div>
                <div class="form-group">
                    <asp:Label ID="lblHighestEducationLevel" runat="server" Text="Highest Education Level:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtHighestEducationLevel" runat="server" CssClass="form-control"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtHighestEducationLevel" ErrorMessage="Country is required." CssClass="text-danger" Display="Dynamic" />
                 
                </div>
            </div>
        </div>
         <br />
        <!-- Education Details Section -->
         <h4>Education Details</h4>
        <div class="row mb-4" style="background-color: aliceblue;">
            <div class="col-md-8">

                <asp:Repeater ID="rptEducationDetails" runat="server"  OnItemCommand="rptEducationDetails_ItemCommand">
                    <%--OnItemCommand="rptEducationDetails_ItemCommand"--%>
                    <ItemTemplate>
                       
                        <div class="form-group">
                            <asp:Label ID="lblDegree" runat="server" Text="<strong>DegreeName</strong>" CssClass="form-label"></asp:Label>
                            <asp:Label ID="Label2" runat="server" CssClass="form-control" Text='<%# Eval("DegreeName") %>'></asp:Label>

                           
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label9" runat="server" Text="<strong>CollegeUniversityName</strong>" CssClass="form-label"></asp:Label>
                            <asp:Label ID="txtCollegeUniversityName" runat="server" CssClass="form-control" Text='<%# Eval("CollegeUniversityName") %>'></asp:Label>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label10" runat="server" Text="<strong>PlaceAddress<</strong>" CssClass="form-label"></asp:Label>
                            <asp:Label ID="txtPlaceAddress" runat="server" CssClass="form-control" Text='<%# Eval("PlaceAddress") %>'></asp:Label>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label11" runat="server" Text="<strong>GraduatedOrPursuing</strong>" CssClass="form-label"></asp:Label>
                            <asp:Label ID="txtGraduatedOrPursuing" runat="server" CssClass="form-control" Text='<%# Eval("GraduatedOrPursuing") %>'></asp:Label>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label12" runat="server" Text="<strong>KeySkills</strong>" CssClass="form-label"></asp:Label>
                            <asp:Label ID="txtKeySkills" runat="server" CssClass="form-control" Text='<%# Eval("KeySkills") %>'></asp:Label>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label13" runat="server" Text="<strong>AcademicProject</strong>" CssClass="form-label"></asp:Label>
                            <asp:Label ID="txtAcademicProject" runat="server" CssClass="form-control" Text='<%# Eval("AcademicProject") %>'></asp:Label>
                        </div>

                        <%-- <div class="form-group">
                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Container.ItemIndex %>' CssClass="btn btn-secondary">Edit</asp:LinkButton>
                        --%>
            <div class="form-group">
                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("EducationId") %>' CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure you want to delete this education?');">Delete</asp:LinkButton>
                </div>     
                    </ItemTemplate>
                    <FooterTemplate>
                        <%-- <asp:Button ID="btnAddEducation" runat="server" Text="Add Education" OnClick="btnAddEducation_Click" CssClass="btn btn-primary" />
                        --%>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
        <asp:Button ID="btnShowAddEducation" runat="server" Text="Add New Education" CssClass="btn btn-primary" OnClientClick="showAddEducationForm(); return false;" />
       <div id="addEducationForm" style="display:none;">
        <div class="row mt-4">
            <div class="col-md-8">
                <h5>Add New Education</h5>
                <div class="form-group">
                    <asp:Label ID="lblNewDegree" runat="server" Text="Degree:" CssClass="form-label"></asp:Label>
                    <asp:DropDownList ID="ddlNewDegreeId" runat="server" CssClass="form-control" DataTextField="DegreeName" DataValueField="DegreeId">
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblNewCollege" runat="server" Text="College/University Name:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtNewCollegeUniversityName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblNewAddress" runat="server" Text="Address:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtNewPlaceAddress" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblNewGraduated" runat="server" Text="Graduated/Pursuing:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtNewGraduatedOrPursuing" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblNewKeySkills" runat="server" Text="Key Skills:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtNewKeySkills" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblNewAcademicProject" runat="server" Text="Academic Project:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtNewAcademicProject" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <asp:Button ID="btnAddEducation" runat="server" Text="Save Education" OnClick="btnAddEducation_Click" CssClass="btn btn-primary" />
          <asp:Button ID="btnCancelEducation" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancelEducation_Click"/>
                </div>
        </div>
    </div>
         <br />
<br />
    <!-- Work Experience Section -->
    <div class="row mb-4" style="background-color: aliceblue;">
        <div class="col-md-8">
            <h3>Work Experience</h3>
            <!-- Repeater to display work experience -->
            <asp:Repeater ID="rptWorkExperience" runat="server" OnItemCommand="rptWorkExperience_ItemCommand">
                <ItemTemplate>
                    
                     <div class="form-group row align-items-center">
     <div class="col-md-4">
                        <asp:Label ID="lblDegree" runat="server" Text="<strong>Company Name</strong>" CssClass="form-label"></asp:Label>
                        <asp:Label ID="txtCompanyName" runat="server" CssClass="form-control" Placeholder="Company Name" Text='<%# Eval("CompanyName") %>'></asp:Label>
                   </div>
                    <div class="col-md-4">
                        <asp:Label ID="Label4" runat="server" Text="<strong>Company Address</strong>" CssClass="form-label"></asp:Label>
                        <asp:Label ID="txtCompanyAddress" runat="server" CssClass="form-control" Placeholder="Company Address" Text='<%# Eval("CompanyAddress") %>'></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="Label5" runat="server" Text="<strong>Designation</strong>" CssClass="form-label"></asp:Label>
                        <asp:Label ID="txtDesignation" runat="server" CssClass="form-control" Placeholder="Designation" Text='<%# Eval("Designation") %>'></asp:Label>
                    </div>
                     <div class="col-md-4">
                        <asp:Label ID="Label6" runat="server" Text="<strong>Key Skills</strong>" CssClass="form-label"></asp:Label>
                        <asp:Label ID="txtKeySkillsPracticed" runat="server" CssClass="form-control" Placeholder="Key Skills" Text='<%# Eval("KeySkills") %>'></asp:Label>
                    </div>
                          
                        <div class="col-md-2 text-right">
          <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("WorkExperienceId") %>' CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure you want to delete this work experience?');">Delete</asp:LinkButton>
      </div>   </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
        <asp:Button ID="Button1" runat="server" Text="Add New WorkExperience" CssClass="btn btn-primary" OnClientClick="showAddExperienceForm(); return false;" />
        <div id="addExperienceForm" style="display:none;">
    <div class="row mt-4">
        <div class="col-md-8">
            <h5>Add New Experience</h5>
            <div class="form-group">
            </div>
            <div class="form-group">
                <asp:Label ID="Label8" runat="server" Text="CompanyName:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="Label14" runat="server" Text="Company Address:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtCompanyAddress" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="Label15" runat="server" Text="Designation:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="Label16" runat="server" Text="Key Skills:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtkeySkills" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
             </div>        
    </div>
    <div class="form-group">
    <!-- Button to add more work experience entries -->
    <asp:Button ID="btnAddWorkExperience" runat="server" Text="Save Work Experience" OnClick="btnAddWorkExperience_Click" CssClass="btn btn-primary" />
     <asp:Button ID="btnCancelWorkExperience" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancelWorkExperience_Click"/>
       
        </div>
    </div>
        <br />
       <br />
   <!-- Core Skills Section -->
<div class="row mb-4" style="background-color: aliceblue;">
     <h5>Core Skills</h5>
     <div class="col-md-8">
 <asp:Repeater ID="rptCoreSkills" runat="server" OnItemCommand="rptCoreSkill_ItemCommand">
     <ItemTemplate>
          <div class="form-group">
         
              <asp:Label ID="Label16" runat="server" Text="Core Skills:" CssClass="form-label"></asp:Label>
              <asp:Label ID="Label7" runat="server"  CssClass="form-label" Text='<%# Eval("CoreSkills") %>'></asp:Label>
           
         </div>
          <div class="form-group">
             <asp:Label ID="Label17" runat="server" Text="Core Skill percentage:" CssClass="form-label"></asp:Label>
             <asp:Label ID="CoreSkillPercentage" runat="server" CssClass="form-label"  Text='<%# Eval("Percentage") %>'></asp:Label>
         </div>
             <div class="form-group">
                     <asp:LinkButton ID="btnDeleteCoreSkill" runat="server" CommandName="Delete" CommandArgument='<%# Eval("CandidateSkillId") %>' CssClass="btn btn-outline-danger btn-sm delete-btn" OnClientClick="return confirm('Are you sure you want to delete this skill?');">Delete</asp:LinkButton>
                  </div>                         
        
     </ItemTemplate>
 </asp:Repeater>
    </div>
 </div>
     <asp:Button ID="Button2" runat="server" Text="Add New Core Skills" CssClass="btn btn-primary" OnClientClick="showAddCoreSkillForm(); return false;" />
       <div id="addCoreSkillForm" style="display:none;">
    <h5>Core Skills</h5>
    <div class="form-group">
        <asp:DropDownList ID="ddlCoreSkill" runat="server" CssClass="form-control"></asp:DropDownList>
    </div>
    <div class="form-group">
        <asp:TextBox ID="txtCoreSkillPercentage" runat="server" CssClass="form-control" Placeholder="Percentage"></asp:TextBox>
    </div>
    <asp:Button ID="btnSaveCoreSkill" runat="server" Text="Save Core Skill" OnClick="btnAddCoreSkill_Click" CssClass="btn btn-primary" />
    <asp:Button ID="btnCancelCoreSkill" runat="server" Text="Cancel" OnClick="btnCancelCoreSkill_Click" CssClass="btn btn-secondary" />
</div>

<!-- Button to Show Add Core Skill Form -->

 <br />
<br />

<!-- Soft Skills Section -->
<div class="row mb-4" style="background-color: aliceblue;">
    <div class="col-md-8">
            <h5>Soft Skills</h5>
        <asp:Repeater ID="rptSoftSkills" runat="server" OnItemCommand="rptSoftSkill_ItemCommand">
    <ItemTemplate>
        <div class="form-group">
             <asp:Label ID="Label16" runat="server" Text="Soft Skills:" CssClass="form-label"></asp:Label>
             <asp:Label ID="Label7" runat="server"  CssClass="form-label" Text='<%# Eval("SoftSkills") %>'></asp:Label>
          
        </div>
        <div class="form-group">
            <asp:Label ID="Label17" runat="server" Text="Soft Skill percentage:" CssClass="form-label"></asp:Label>
            <asp:Label ID="SoftSkillPercentage" runat="server" CssClass="form-label"  Text='<%# Eval("Percentage") %>'></asp:Label>
        </div>
         <div class="form-group">
                    <asp:LinkButton ID="btnDeleteSoftSkill" runat="server" CommandName="Delete" CommandArgument='<%# Eval("CandidateSkillId") %>' CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure you want to delete this skill?');">Delete</asp:LinkButton>
                </div>
        
    </ItemTemplate>
</asp:Repeater>
 </div>
   </div>

     <asp:Button ID="Button3" runat="server" Text="Add New Soft Skills" CssClass="btn btn-primary" OnClientClick="showAddSoftSkillForm(); return false;" />
   <div id="addSoftSkillForm" style="display:none;" runat="server">
    <h5>Soft Skills</h5>
    <div class="form-group">
        <asp:DropDownList ID="ddlSoftSkill" runat="server" CssClass="form-control"></asp:DropDownList>
    </div>
    <div class="form-group">
        <asp:TextBox ID="txtSoftSkillPercentage" runat="server" CssClass="form-control" Placeholder="Percentage"></asp:TextBox>
    </div>
    <asp:Button ID="btnSaveSoftSkill" runat="server" Text="Save Soft Skill" OnClick="btnAddSoftSkill_Click" CssClass="btn btn-primary" />
    <asp:Button ID="btnCancelSoftSkill" runat="server" Text="Cancel" OnClick="btnCancelSoftSkill_Click" CssClass="btn btn-secondary" />
</div>
 <br />
<br />

    <!-- Job Preferences Section -->
    <div class="row mb-4" style="background-color: aliceblue;">
        <div class="col-md-8">
            <h5>Job Preferences</h5>
            <div class="form-group">
                <asp:Label ID="lblPreferredJobTypes" runat="server" Text="Preferred Job Types:" CssClass="form-label"></asp:Label>
                <asp:DropDownList ID="ddlJobTypes" runat="server" CssClass="form-control"></asp:DropDownList>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlJobTypes" ErrorMessage="Country is required." CssClass="text-danger" Display="Dynamic" />
                 
            </div>
            <div class="form-group">
                <asp:Label ID="lblPreferredJobLocations" runat="server" Text="Preferred Job Locations:" CssClass="form-label"></asp:Label>
                <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="form-control"></asp:DropDownList>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlJobLocation" ErrorMessage="Country is required." CssClass="text-danger" Display="Dynamic" />
                 
            </div>
            <div class="form-group">
                <asp:Label ID="lblAvailability" runat="server" Text="Availability:" CssClass="form-label"></asp:Label>
                <asp:DropDownList ID="ddlAvailability" runat="server" CssClass="form-control"></asp:DropDownList>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlAvailability" ErrorMessage="Country is required." CssClass="text-danger" Display="Dynamic" />
                 
            </div>
        </div>
    </div>
 <br />
<br />
    <!-- Training Preferences Section -->
    <div class="row mb-4" style="background-color: aliceblue;">
        <div class="col-md-8">
            <h5>Training Preferences</h5>
            <div class="form-check">
                <asp:CheckBox ID="chkFreeTraining" runat="server" CssClass="form-check-input" />
                <asp:Label ID="lblFreeTraining" runat="server" Text="Willing to take free training" CssClass="form-check-label"></asp:Label>

            </div>
            <div class="form-check">
                <asp:CheckBox ID="chkPaidTraining" runat="server" CssClass="form-check-input" />
                <asp:Label ID="lblPaidTraining" runat="server" Text="Willing to take paid training" CssClass="form-check-label"></asp:Label>
            </div>
            <div class="form-check">
                <asp:CheckBox ID="chkCareerConsultantContact" runat="server" CssClass="form-check-input" />
                <asp:Label ID="lblCareerConsultantContact" runat="server" Text="Willing to be contacted by a professional career consultant" CssClass="form-check-label"></asp:Label>
            </div>
        </div>
    </div>
         <br />
<br />
    <!-- Resume/CV Section -->
    <div class="row mb-4" style="background-color: aliceblue;">
        <div class="col-md-8">
            <h5>Resume/CV</h5>
            <div class="form-group">
                <asp:Label ID="lblResumeFile" runat="server" Text="Upload Resume/CV:" CssClass="form-label"></asp:Label>
                <asp:FileUpload ID="fuResumeFile" runat="server" CssClass="form-control-file" />
            </div>
            <div class="form-group">
                <asp:Label ID="lblCoverLetter" runat="server" Text="Cover Letter:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtCoverLetter" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblLinkedInProfile" runat="server" Text="LinkedIn Profile:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtLinkedInProfile" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblPortfolio" runat="server" Text="Portfolio:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtPortfolio" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>
 <br />
<br />
    <!-- Buttons Section -->
    <div class="row mb-4">
        <div class="col-md-8">
            <asp:Button ID="btnSave" runat="server" Text="Save Profile" CssClass="btn btn-primary" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
        </div>
    </div>
    <asp:Label ID="lblStatus" runat="server" Text="" CssClass="form-label" Visible="false"></asp:Label>
    </div>

</asp:Content>
