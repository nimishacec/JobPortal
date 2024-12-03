<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CandidateDashboard.aspx.cs" MasterPageFile="~/Admin/Admin1.Master" Inherits="JobPortalWebApplication.Admin.CandidateDashboard" EnableEventValidation="false" MaintainScrollPositionOnPostBack="true" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
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
            if (form) {
                form.style.display = "block";
            } else {
                console.log("Element with ID 'addSoftSkillForm' not found.");
            }
        }
       
            window.onload = function () {
          var updateSuccess = document.getElementById("hdnUpdateSuccess").value;
              if (updateSuccess === "true") {
                  alert("Updated successfully!");
          } else if (updateSuccess === "false") {
                  alert("Updation Failed");
          }
      };
    </script>




    <style>
        .delete -btn {
            margin-top: 10px; /* Adjust this value for top margin */
            margin-bottom: 10px; /* Adjust this value for bottom margin */
        }
    </style>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.5.0/font/bootstrap-icons.css">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="text-align: left">
        <asp:Button ID="Button4" runat="server" Text="Back" OnClientClick="window.history.back(); return false;" CssClass="btn btn-link" />
    </div>
    <div class="col-12 grid-margin">
        <div class="card">
            <div class="card-body">
                <h4 class="card-title">Candidate Details</h4>


                <p class="card-description">
                    Personal info
                </p>
                <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="lblFirstName" runat="server" Text="First Name:" CssClass="form-label"></asp:Label>
                        <div class="mt-2">
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtFirstName" ErrorMessage="FirstName is required." CssClass="text-danger" Display="Dynamic" />
                </div>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblLastName" runat="server" Text="Last Name:" CssClass="form-label"></asp:Label>
                           <div class="mt-2">
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtLastName" ErrorMessage="LastName is required." CssClass="text-danger" Display="Dynamic" />
 </div>
                    </div>
                </div>
                <div class="row mt-3">
                    <div class="col-md-6">
                        <asp:Label ID="Label1" runat="server" Text="Email Address:" CssClass="form-label"></asp:Label>
                         <div class="mt-2">
                        <asp:Label ID="txtEmail" runat="server" CssClass="form-control"></asp:Label>
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required." CssClass="text-danger" Display="Dynamic" />--%>
 </div>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblMobileNumber" runat="server" Text="Mobile Number:" CssClass="form-label"></asp:Label>
                         <div class="mt-2">
                        <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlPlan" ErrorMessage="Country is required." CssClass="text-danger" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revMobileNumber" runat="server" ControlToValidate="txtMobileNumber" ValidationExpression="^\d{10}$" ErrorMessage="Enter a valid 10-digit mobile number."
                            CssClass="text-danger" Display="Dynamic" /><br />
                    </div>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="Label3" runat="server" Text="Plan Types:"></asp:Label>
                         <div class="mt-2">
                       
                        <asp:DropDownList ID="ddlPlan" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPlan" ErrorMessage="Plan is required." CssClass="text-danger" Display="Dynamic" />
 </div>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblAddress" runat="server" Text="Address:" CssClass="form-label"></asp:Label>
                         <div class="mt-2">
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddress" ErrorMessage="Address is required." CssClass="text-danger" Display="Dynamic" />
                    </div> </div>
                </div>
                <div class="row mt-3">
                    <div class="col-md-6">
                        <asp:Label ID="lblCity" runat="server" Text="City:" CssClass="form-label"></asp:Label>
                         <div class="mt-2">
                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCity" ErrorMessage="City is required." CssClass="text-danger" Display="Dynamic" />
 </div>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblState" runat="server" Text="State/Province:" CssClass="form-label"></asp:Label>
                         <div class="mt-2">
                        <asp:TextBox ID="txtState" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtState" ErrorMessage="State is required." CssClass="text-danger" Display="Dynamic" />
 </div>
                    </div>
                </div>
                <div class="row mt-3">
                    <div class="col-md-6">
                        <asp:Label ID="lblPostalOrZipCode" runat="server" Text="Postal/Zip Code:" CssClass="form-label"></asp:Label>
                         <div class="mt-2">
                        <asp:TextBox ID="txtPostal" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfPostalOrZipCode" runat="server" ControlToValidate="txtPostal" ErrorMessage="P is required." CssClass="text-danger" Display="Dynamic" />

                        <asp:RegularExpressionValidator ID="rvfPostalOrZipCode" runat="server" ControlToValidate="txtPostal" ValidationExpression="^\d{6}$" ErrorMessage="Enter a valid 6-digit Postal Code or Zip Code."
                            CssClass="text-danger" Display="Dynamic" /><br />
 </div>
                    </div>

                    <div class="col-md-6">
                        <asp:Label ID="lblCountry" runat="server" Text="Country:" CssClass="form-label"></asp:Label>
                         <div class="mt-2">
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rvfddlCountry" runat="server" ControlToValidate="ddlCountry" ErrorMessage="Country is required." CssClass="text-danger" Display="Dynamic" />
                    </div> </div>
                </div>
            </div>
        </div>
        <br />


        <!-- Education Details Section -->
        <div class="card">
            <div class="card-body">
                <p class="card-description">Education Details </p>

                <div class="row">
                    <div class="col-md-6">

                        <asp:Label ID="lblHighestEducationLevel" runat="server" Text="Highest Education Level:" CssClass="form-label"></asp:Label>
                         <div class="mt-2">
                        <asp:TextBox ID="txtHighestEducationLevel" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtHighestEducationLevel" ErrorMessage="HighestEducationLevel is required." CssClass="text-danger" Display="Dynamic" />
 </div>
                    </div>

                </div>
                <asp:Repeater ID="rptEducationDetails" runat="server" OnItemCommand="rptEducationDetails_ItemCommand">
                    <%--OnItemCommand="rptEducationDetails_ItemCommand"--%>
                    <ItemTemplate>

                        <div class="row mt-2">
                            <div class="col-md-6">
                                <asp:Label ID="lblDegree" runat="server" Text=" DegreeName " CssClass="form-label"></asp:Label>
                                 <div class="mt-2">
                                <asp:Label ID="Label2" runat="server" CssClass="form-control" Text='<%# Eval("DegreeName") %>'></asp:Label>
 </div>

                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="Label9" runat="server" Text=" CollegeUniversityName " CssClass="form-label"></asp:Label>
                                 <div class="mt-2">
                                <asp:Label ID="txtCollegeUniversityName" runat="server" CssClass="form-control" Text='<%# Eval("CollegeUniversityName") %>'></asp:Label>
                            </div> </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-md-6">
                                <asp:Label ID="Label10" runat="server" Text=" PlaceAddress "></asp:Label>
                                 <div class="mt-2">
                                <asp:Label ID="txtPlaceAddress" runat="server" CssClass="form-control" Text='<%# Eval("PlaceAddress") %>'></asp:Label>
                            </div> </div>
                            <div class="col-md-6">
                                <asp:Label ID="Label11" runat="server" Text=" GraduatedOrPursuing " CssClass="form-label"></asp:Label>
                                 <div class="mt-2">
                                <asp:Label ID="txtGraduatedOrPursuing" runat="server" CssClass="form-control" Text='<%# Eval("GraduatedOrPursuing") %>'></asp:Label>
                            </div> </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-md-6">
                                <asp:Label ID="Label12" runat="server" Text=" KeySkills " CssClass="form-label"></asp:Label>
                                 <div class="mt-2">
                                <asp:Label ID="txtKeySkills" runat="server" CssClass="form-control" Text='<%# Eval("KeySkills") %>'></asp:Label>
                            </div> </div>
                            <div class="col-md-6">
                                <asp:Label ID="Label13" runat="server" Text=" AcademicProject " CssClass="form-label"></asp:Label>
                                 <div class="mt-2">
                                <asp:Label ID="txtAcademicProject" runat="server" CssClass="form-control" Text='<%# Eval("AcademicProject") %>'></asp:Label>
                            </div> </div>
                        </div>
                        <%-- <div class="form-group">
                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Container.ItemIndex %>' CssClass="btn btn-secondary">Edit</asp:LinkButton>
                        --%>
                        <div class="form-group mt-2 d-flex justify-content-end">
                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("EducationId") %>'  
                                OnClientClick="return confirm('Are you sure you want to delete this education?');"> <i class="bi bi-trash"></i></asp:LinkButton>
                        </div>
                    </ItemTemplate>
                    
                </asp:Repeater>

                <div class="row mt-2">
                    <div class="col-md-8">
                        <asp:LinkButton ID="btnShowAddEducation" runat="server" CssClass="btn btn-outline-primary" OnClientClick="showAddEducationForm(); return false;">
    <i class="mdi mdi-plus-box"></i> Add Education
                        </asp:LinkButton>
                    </div>
                </div>
                <div id="addEducationForm" style="display: none;">
                    <%--<div class="row">
                        <div class="col-md-6">--%>
                            <h5 class="mt-2">Add New Education</h5>
                            <div class="row mt-2">
                                <div class="col-md-6">
                                    <asp:Label ID="lblNewDegree" runat="server" Text="Degree:" CssClass="form-label"></asp:Label>
                                     <div class="mt-2">
                                    <asp:DropDownList ID="ddlNewDegreeId" runat="server" CssClass="form-control" DataTextField="DegreeName" DataValueField="DegreeId">
                                    </asp:DropDownList>
                                </div> </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblNewCollege" runat="server" Text="College/University Name:" CssClass="form-label"></asp:Label>
                                     <div class="mt-2">
                                    <asp:TextBox ID="txtNewCollegeUniversityName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div> </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="lblNewAddress" runat="server" Text="Address:" CssClass="form-label"></asp:Label>
                                     <div class="mt-2">
                                    <asp:TextBox ID="txtNewPlaceAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                </div> </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblNewGraduated" runat="server" Text="Graduated/Pursuing:" CssClass="form-label"></asp:Label>
                                     <div class="mt-2">
                                    <asp:TextBox ID="txtNewGraduatedOrPursuing" runat="server" CssClass="form-control"></asp:TextBox>
                                </div> </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-md-6">
                                    <asp:Label ID="lblNewKeySkills" runat="server" Text="Key Skills:" CssClass="form-label"></asp:Label>
                                     <div class="mt-2">
                                    <asp:TextBox ID="txtNewKeySkills" runat="server" CssClass="form-control"></asp:TextBox>
                                </div></div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblNewAcademicProject" runat="server" Text="Academic Project:" CssClass="form-label"></asp:Label>
                                     <div class="mt-2">
                                    <asp:TextBox ID="txtNewAcademicProject" runat="server" CssClass="form-control"></asp:TextBox>
                                </div> 
                            </div></div>
                            <div class="row mt-2">
                                <div class="col-md-6">

                                    <asp:LinkButton ID="btnAddEducation" runat="server" OnClick="btnAddEducation_Click" CssClass="btn btn-outline-primary">
                                <i class="mdi mdi-content-save-edit"></i>Save</asp:LinkButton>
                                    <asp:Button ID="btnCancelEducation" runat="server" Text="Cancel" CssClass="btn btn-outline-secondary" OnClick="btnCancelEducation_Click" />
                                </div>
                            </div>
                       
                    </div>
                </div>
            </div>
       
             <br />
        

        <!-- Work Experience Section -->
        <div class="card">
            <div class="card-body">
                <p class="card-description">Work Experience </p>

                <asp:Repeater ID="rptWorkExperience" runat="server" OnItemCommand="rptWorkExperience_ItemCommand">
                    <ItemTemplate>

                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="lblDegree" runat="server" Text=" Company Name " CssClass="form-label"></asp:Label>
                                 <div class="mt-2">
                                <asp:Label ID="txtCompanyName" runat="server" CssClass="form-control" Placeholder="Company Name" Text='<%# Eval("CompanyName") %>'></asp:Label>
                            </div> </div>
                            <div class="col-md-6">
                                <asp:Label ID="Label4" runat="server" Text=" Company Address " CssClass="form-label"></asp:Label>
                                 <div class="mt-2">
                                <asp:Label ID="txtCompanyAddress" runat="server" CssClass="form-control" Placeholder="Company Address" Text='<%# Eval("CompanyAddress") %>'></asp:Label>
                            </div> </div>
                        </div></br>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="Label5" runat="server" Text=" Designation " CssClass="form-label"></asp:Label>
                                 <div class="mt-2">
                                <asp:Label ID="txtDesignation" runat="server" CssClass="form-control" Placeholder="Designation" Text='<%# Eval("Designation") %>'></asp:Label>
                            </div> </div>
                            <div class="col-md-6">
                                <asp:Label ID="Label6" runat="server" Text=" Key Skills " CssClass="form-label"></asp:Label>
                                 <div class="mt-2">
                                <asp:Label ID="txtKeySkillsPracticed" runat="server" CssClass="form-control" Placeholder="Key Skills" Text='<%# Eval("KeySkills") %>'></asp:Label>
                            </div> </div>
                        </div>
                     <div class="form-group mt-2 d-flex justify-content-end">
                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("WorkExperienceId") %>' OnClientClick="return confirm('Are you sure you want to delete this work experience?');"><i class="bi bi-trash"></i></asp:LinkButton>
                        </div>

                    </ItemTemplate>
                </asp:Repeater>

                <div class="row mt-3">
                    <div class="col-md-8">

                        <asp:LinkButton ID="Button1" runat="server" CssClass="btn btn-outline-primary" OnClientClick="showAddExperienceForm(); return false;"><i class="mdi mdi-plus-box"></i>
                             Add WorkExperience
                        </asp:LinkButton>
                    </div>
                </div>
                <div id="addExperienceForm" style="display: none;">
                   
                            <h5>Add New Experience</h5>

                            <div class="row mt-2">
                                <div class="col-md-6">
                                    <asp:Label ID="Label8" runat="server" Text="CompanyName:" CssClass="form-label"></asp:Label>
                                     <div class="mt-2">
                                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div> </div>
                                <div class="col-md-6">
                                    <asp:Label ID="Label14" runat="server" Text="Company Address:" CssClass="form-label"></asp:Label>
                                     <div class="mt-2">
                                    <asp:TextBox ID="txtCompanyAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                </div> </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label15" runat="server" Text="Designation:" CssClass="form-label"></asp:Label>
                                     <div class="mt-2">
                                    <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control"></asp:TextBox>
                                </div> </div>
                                <div class="col-md-6">
                                    <asp:Label ID="Label16" runat="server" Text="Key Skills:" CssClass="form-label"></asp:Label>
                                     <div class="mt-2">
                                    <asp:TextBox ID="txtkeySkills" runat="server" CssClass="form-control"></asp:TextBox>
                                </div> </div>
                            </div>
                      

                        <div class="form-group mt-2">
                            <asp:LinkButton ID="btnAddWorkExperience" runat="server" OnClick="btnAddWorkExperience_Click" CssClass="btn btn-outline-primary ">
     <i class="mdi mdi-content-save-edit"></i>Save</asp:LinkButton>

                            <!-- Button to add more work experience entries -->
                            <%--  <asp:Button ID="btnAddWorkExperience" runat="server" Text="Save" OnClick="btnAddWorkExperience_Click" CssClass="btn btn-primary btn-rounded btn-fw" />
                            --%>
                               <asp:LinkButton ID="btnCancelWorkExperience" runat="server" Text="Cancel" CssClass="btn btn-outline-secondary" OnClick="btnCancelWorkExperience_Click" />
                                 
                            </div>
                     </div>
            </div>
        </div>
        <br />

        <!-- Core Skills Section -->
        <div class="card">
            <div class="card-body">
                <p class="card-description">Core Skills </p>
                <asp:Repeater ID="rptCoreSkills" runat="server" OnItemCommand="rptCoreSkill_ItemCommand">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="Label18" runat="server" Text="Core Skills:" CssClass="form-label"></asp:Label>
                              
                                <asp:Label ID="Label7" runat="server" CssClass="form-label" Text='<%# Eval("CoreSkills") %>'></asp:Label>
                                   <div class="mt-2">
                                <asp:DropDownList ID="ddlCoreSkill" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div> </div>
                            <div class="col-md-6">
                                <asp:Label ID="Label17" runat="server" Text="Core Skill percentage:" CssClass="form-label"></asp:Label>
                                 <div class="mt-2">
                                <asp:Label ID="CoreSkillPercentage" runat="server" CssClass="form-control" Text='<%# Eval("Percentage") %>'></asp:Label>
                            </div> </div>
                        </div>
                       <div class="form-group mt-2 d-flex justify-content-end">

                            <asp:LinkButton ID="btnDeleteCoreSkill" runat="server" CommandName="Delete" CommandArgument='<%# Eval("CandidateSkillId") %>'  OnClientClick="return confirm('Are you sure you want to delete this skill?');"><i class="bi bi-trash"></i></asp:LinkButton>

                        </div>

                    </ItemTemplate>
                </asp:Repeater>

                <div class="row mt-4">
                    <div class="col-md-8">

                        <asp:LinkButton ID="Button2" runat="server" CssClass="btn btn-outline-primary" OnClientClick="showAddCoreSkillForm(); return false;">
                            <i class="mdi mdi-plus-box"></i>
       Add Skills
                        </asp:LinkButton>
                    </div>
                </div>
                <div id="addCoreSkillForm" style="display: none;">
                    <h6>Core Skills</h6>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlCoreSkill" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtCoreSkillPercentage" runat="server" CssClass="form-control" Placeholder="Percentage"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:LinkButton ID="btnSaveCoreSkill" runat="server" OnClick="btnAddCoreSkill_Click"
                                CssClass="btn btn-primary btn-rounded btn-fw"><i class="mdi mdi-content-save-edit"></i>Save</asp:LinkButton>

                            <%--                            <asp:Button ID="btnSaveCoreSkill" runat="server" Text="Save " OnClick="btnAddCoreSkill_Click" CssClass="btn btn-primary" />
                            --%>
                            <asp:Button ID="btnCancelCoreSkill" runat="server" Text="Cancel" OnClick="btnCancelCoreSkill_Click" CssClass="btn btn-outline-secondary" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Button to Show Add Core Skill Form -->

        <br />


        <!-- Soft Skills Section -->
        <div class="card">
            <div class="card-body">
                <p class="card-description">Soft Skills </p>
                <asp:Repeater ID="rptSoftSkills" runat="server" OnItemCommand="rptSoftSkill_ItemCommand">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="Label16" runat="server" Text="Soft Skills:" CssClass="form-label"></asp:Label>
                                 
                                <asp:Label ID="Label7" runat="server" CssClass="form-label" Text='<%# Eval("SoftSkills") %>'></asp:Label>
                                <div class="mt-2">
                                <asp:DropDownList ID="ddlSoftskill" runat="server" CssClass="form-control"></asp:DropDownList>
 </div>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="Label17" runat="server" Text="Soft Skill percentage:" CssClass="form-label"></asp:Label>
                                 <div class="mt-2">
                                <asp:Label ID="SoftSkillPercentage" runat="server" CssClass="form-control" Text='<%# Eval("Percentage") %>'></asp:Label>
                            </div>
                                 </div>
                        </div>
                      <div class="form-group mt-2 d-flex justify-content-end">
                            <asp:LinkButton ID="btnDeleteSoftSkill" runat="server" CommandName="Delete" CommandArgument='<%# Eval("CandidateSkillId") %>' OnClientClick="return confirm('Are you sure you want to delete this skill?');"><i class="bi bi-trash"></i></asp:LinkButton>
                        </div>

                    </ItemTemplate>
                </asp:Repeater>

                <div class="row mt-4">
                    <div class="col-md-8">
                        <asp:LinkButton ID="btnaddSoftSkill" runat="server" CssClass="btn btn-outline-primary" OnClientClick="showAddSoftSkillForm(); return false;">
                            <i class="mdi mdi-plus-box"></i>
       Add Soft Skills
                        </asp:LinkButton>
                    </div>
                </div>
                <div id="addSoftSkillForm" style="display: none;" runat="server">
                    <h5>Soft Skills</h5>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlSoftSkill" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtSoftSkillPercentage" runat="server" CssClass="form-control" Placeholder="Percentage"></asp:TextBox>
                        </div>
                    </div>
                    <asp:LinkButton ID="btnSaveSoftSkill" runat="server" OnClick="btnAddSoftSkill_Click" CssClass="btn btn-primary btn-rounded btn-fw">
<i class="mdi mdi-content-save-edit"></i>Save</asp:LinkButton>
                    <%-- <asp:Button ID="btnSaveSoftSkill" runat="server" Text="Save Soft Skill" OnClick="btnAddSoftSkill_Click" CssClass="btn btn-primary" />
                    --%>
                    <asp:Button ID="btnCancelSoftSkill" runat="server" Text="Cancel" OnClick="btnCancelSoftSkill_Click" CssClass="btn btn-outline-secondary" />
                </div>
            </div>
        </div>
        <br />


        <!-- Job Preferences Section -->
        <div class="card">
            <div class="card-body">
                <p class="card-description">Job Preferences </p>

                <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="lblPreferredJobTypes" runat="server" Text="Preferred Job Types:" CssClass="form-label"></asp:Label>
                         <div class="mt-2">
                        <asp:DropDownList ID="ddlJobTypes" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlJobTypes" ErrorMessage="Country is required." CssClass="text-danger" Display="Dynamic" />
 </div>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblPreferredJobLocations" runat="server" Text="Preferred Job Locations:" CssClass="form-label"></asp:Label>
                         <div class="mt-2">
                        <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlJobLocation" ErrorMessage="Country is required." CssClass="text-danger" Display="Dynamic" />
                    </div>
                         </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="lblAvailability" runat="server" Text="Availability:" CssClass="form-label"></asp:Label>
                         <div class="mt-2">
                        <asp:DropDownList ID="ddlAvailability" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlAvailability" ErrorMessage="Country is required." CssClass="text-danger" Display="Dynamic" />
                    </div>
                         </div>
                </div>
            </div>
        </div>
        <br />

        <!-- Training Preferences Section -->
        <div class="card">
            <div class="card-body">
                <p class="card-description">Training Preferences </p>
                <div class="row ms-2">
                    <div class="col-md-6">
                        <asp:CheckBox ID="chkFreeTraining" runat="server" CssClass="form-check-input" />
                        <asp:Label ID="lblFreeTraining" runat="server" Text="Willing to take free training" CssClass="form-check-label"></asp:Label>

                    </div>
                    <div class="col-md-6">
                        <asp:CheckBox ID="chkPaidTraining" runat="server" CssClass="form-check-input" />
                        <asp:Label ID="lblPaidTraining" runat="server" Text="Willing to take paid training" CssClass="form-check-label"></asp:Label>
                    </div>
                </div>
                <div class="row ms-2">
                    <div class="col-md-6">
                        <asp:CheckBox ID="chkCareerConsultantContact" runat="server" CssClass="form-check-input" />
                        <asp:Label ID="lblCareerConsultantContact" runat="server" Text="Willing to be contacted by a professional career consultant" CssClass="form-check-label"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <br />

        <div class="card">
            <div class="card-body">
                <p class="card-description">Resume/CV </p>

                <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="lblResumeFile" runat="server" Text="Upload Resume/CV:" CssClass="form-label"></asp:Label>
                       
  
  <asp:FileUpload ID="fuResumeFile" runat="server" CssClass="form-control-file" />
   <%-- <div class="input-group col-xs-6">
       
        <span class="input-group-append">
            <button class="btn btn-primary form-control-file" type="button" 
                onclick="document.getElementById('<%= fuResumeFile.ClientID %>').click();">
                <i class="ti-upload btn-icon-prepend"></i>
                Browse
            </button>
        </span>
    </div>--%>
  <%-- <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-success mt-2" OnClick="btnUpload_Click" />--%>
</div>

                   
                    <div class="col-md-6">
                        <asp:Label ID="lblCoverLetter" runat="server" Text="Cover Letter:" CssClass="form-label"></asp:Label>
                         <div class="mt-2">
                        <asp:TextBox ID="txtCoverLetter" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                    </div>
                        </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="lblLinkedInProfile" runat="server" Text="LinkedIn Profile:" CssClass="form-label"></asp:Label>
                         <div class="mt-2">
                        <asp:TextBox ID="txtLinkedInProfile" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                        </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblPortfolio" runat="server" Text="Portfolio:" CssClass="form-label"></asp:Label>
                         <div class="mt-2">
                        <asp:TextBox ID="txtPortfolio" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                        </div>
                </div>
                <hr />
                <div class="form-group mt-3 d-flex justify-content-center">
                    <asp:HiddenField ID="hiddenFieldCandidateId" runat="server" />
                    <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSave_Click">
                            <i class="mdi mdi-content-save-all"></i>Save Profile
                    </asp:LinkButton>&nbsp;
                    <asp:HiddenField ID="hdnUpdateSuccess" runat="server" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-secondary" OnClick="btnCancel_Click" CausesValidation="false"/>
                </div>


                <div class="row mt-2 justify-content-center">
                    <div class="col-auto">
                        <asp:Label ID="lblStatus" runat="server" Text="" CssClass="form-label" Visible="false"></asp:Label>
                    </div>
                </div>

            </div>

            <%--   <div class="card">
 <div class="card-body">
        <div class="row mb-4">
            <div class="col-md-8">
                <asp:Button ID="btnSave" runat="server" Text="Save Profile" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
            </div>
        </div>
        <asp:Label ID="lblStatus" runat="server" Text="" CssClass="form-label" Visible="false"></asp:Label>
    </div>
               </div>--%>
        </div>
      

</asp:Content>
