<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Candidate/Candidate.Master" CodeBehind="CandidateViewDashboard.aspx.cs" Inherits="JobPortalWebApplication.Candidate.CandidateViewDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container mt-5" style="background-color: aliceblue">
        <div class="row justify-content-center">
            <div class="row justify-content-center">

                <h4 style="text-align: center">Candidate Dashboard</h4>

                <h5 class="mt-4 mb-3">Personal Details</h5>
                <div class="row mb-3">
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblFirstName" runat="server" Text="First Name:" CssClass="form-label"></asp:Label><asp:Label ID="lblFirstNameValue" runat="server" CssClass="form-control-plaintext"></asp:Label>

                    </div>
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblLastName" runat="server" Text="Last Name:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblLastNameValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblEmailValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblPhone" runat="server" Text="Phone Number:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblPhoneValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                </div>

                <div class="row mb-3 ">
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblAddress" runat="server" Text="Address:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblAddressValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblCity" runat="server" Text="City:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblCityValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblState" runat="server" Text="State/Province:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblStateValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblPostalCode" runat="server" Text="Postal/Zip Code:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblPostalCodeValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblCountry" runat="server" Text="Country:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblCountryValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblHighestEducation" runat="server" Text="Highest Education Level:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblHighestEducationValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                </div>

                <!-- Education Details -->
                <h5 class="mt-4 mb-3">Education Details</h5>
                <asp:Repeater ID="rptEducationDetails" runat="server">

                    <ItemTemplate>

                        <div class="row mb-3">
                            <div class="col-md-6 border border-primary p-3">
                                <asp:Label ID="lblDegree" runat="server" Text="<strong>DegreeName</strong>" CssClass="form-label"></asp:Label>
                                <asp:Label ID="Label2" runat="server" CssClass="form-control-plaintext" Text='<%# Eval("Degree") %>'></asp:Label>

                                <%--<asp:DropDownList ID="ddlDegreeId" runat="server" CssClass="form-control" DataTextField="DegreeName" DataValueField="DegreeId">
                    </asp:DropDownList>--%>
                            </div>

                            <div class="col-md-6 border border-primary p-3">
                                <asp:Label ID="Label9" runat="server" Text="<strong>CollegeUniversityName</strong>" CssClass="form-label"></asp:Label>
                                <asp:Label ID="txtCollegeUniversityName" runat="server" CssClass="form-control-plaintext" Text='<%# Eval("CollegeUniversityName") %>'></asp:Label>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6 border border-primary p-3">
                                <asp:Label ID="Label10" runat="server" Text="<strong>PlaceAddress</strong>" CssClass="form-label"></asp:Label>
                                <asp:Label ID="txtPlaceAddress" runat="server" CssClass="form-control-plaintext" Text='<%# Eval("PlaceAddress") %>'></asp:Label>
                            </div>

                            <div class="col-md-6 border border-primary p-3">
                                <asp:Label ID="Label11" runat="server" Text="<strong>GraduatedOrPursuing</strong>" CssClass="form-label"></asp:Label>
                                <asp:Label ID="txtGraduatedOrPursuing" runat="server" CssClass="form-control-plaintext" Text='<%# Eval("GraduatedOrPursuing") %>'></asp:Label>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6 border border-primary p-3">
                                <asp:Label ID="Label12" runat="server" Text="<strong>KeySkills</strong>" CssClass="form-label"></asp:Label>
                                <asp:Label ID="txtKeySkills" runat="server" CssClass="form-control-plaintext" Text='<%# Eval("KeySkills") %>'></asp:Label>
                            </div>

                            <div class="col-md-6 border border-primary p-3">
                                <asp:Label ID="Label13" runat="server" Text="<strong>AcademicProject</strong>" CssClass="form-label"></asp:Label>
                                <asp:Label ID="txtAcademicProject" runat="server" CssClass="form-control-plaintext" Text='<%# Eval("AcademicProject") %>'></asp:Label>
                            </div>
                        </div>

                        <%-- <div class="form-group">
                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Container.ItemIndex %>' CssClass="btn btn-secondary">Edit</asp:LinkButton>
                
    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("EducationId") %>' CssClass="btn btn-danger">Delete</asp:LinkButton>
</div>       --%>
                    </ItemTemplate>
                    <FooterTemplate>
                        <%-- <asp:Button ID="btnAddEducation" runat="server" Text="Add Education" OnClick="btnAddEducation_Click" CssClass="btn btn-primary" />
                        --%>
                    </FooterTemplate>
                </asp:Repeater>
                <br />
                <br />


                <!-- Work Experience -->



                <h5 class="mt-4 mb-3">Work Experience Details</h5>
                <asp:Repeater ID="rptWorkExperience" runat="server">

                    <ItemTemplate>
                         <div class="row mb-3">
                        <div class="col-md-6 border border-primary p-3">
                            <asp:Label ID="Label9" runat="server" Text="<strong>CompanyName</strong>" CssClass="form-label"></asp:Label>
                            <asp:Label ID="txtCollegeUniversityName" runat="server" CssClass="form-control-plaintext" Text='<%# Eval("CompanyName") %>'></asp:Label>
                        </div>
                     
                      <div class="col-md-6 border border-primary p-3">
                          <asp:Label ID="Label10" runat="server" Text="<strong>CompanyAddress</strong>" CssClass="form-label"></asp:Label>
                          <asp:Label ID="txtPlaceAddress" runat="server" CssClass="form-control-plaintext" Text='<%# Eval("CompanyAddress") %>'></asp:Label>
                      </div>
      </div>
<div class="row mb-3">
                      <div class="col-md-6 border border-primary p-3">
                          <asp:Label ID="Label11" runat="server" Text="<strong>Designation</strong>" CssClass="form-label"></asp:Label>
                          <asp:Label ID="txtGraduatedOrPursuing" runat="server" CssClass="form-control-plaintext" Text='<%# Eval("Designation") %>'></asp:Label>
                      </div>
                 
                            <div class="col-md-6 border border-primary p-3">
                                <asp:Label ID="Label12" runat="server" Text="<strong>KeySkills</strong>" CssClass="form-label"></asp:Label>
                                <asp:Label ID="txtKeySkills" runat="server" CssClass="form-control-plaintext" Text='<%# Eval("KeySkills") %>'></asp:Label>
                            </div>
    <div>------------------------------------------------------------------------------------------------------------------------------------------------</div>

                        </div>

                        <%-- <div class="form-group">
                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Container.ItemIndex %>' CssClass="btn btn-secondary">Edit</asp:LinkButton>
                
    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("EducationId") %>' CssClass="btn btn-danger">Delete</asp:LinkButton>
</div>       --%>
                    </ItemTemplate>
                    <FooterTemplate>
                        <%-- <asp:Button ID="btnAddEducation" runat="server" Text="Add Education" OnClick="btnAddEducation_Click" CssClass="btn btn-primary" />
                        --%>
                    </FooterTemplate>
                </asp:Repeater>
                <br />
                <br />

                <!-- Core Skills -->
                <h5 class="mt-4 mb-3">Core Skills</h5>
                <div class="row mb-3">
                    <div class="col-md-6 border border-primary p-3 ">
                        <asp:Label ID="lblCoreSkill" runat="server" Text="Core Skills:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblCoreSkillValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblCoreSkillPercentage" runat="server" Text="Core Skills Percentage:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblCoreSkillPercentageValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                </div>

                <!-- Soft Skills -->
                <h5 class="mt-4 mb-3">Soft Skills</h5>
                <div class="row mb-3">
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblSoftSkill" runat="server" Text="Soft Skills:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblSoftSkillValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblSoftSkillPercentage" runat="server" Text="Soft Skills Percentage:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblSoftSkillPercentageValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                </div>

                <!-- Job Preferences -->
                <h5 class="mt-4 mb-3">Job Preferences</h5>
                <div class="row mb-3">
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblJobTypes" runat="server" Text="Job Types:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblJobTypesValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblJobLocation" runat="server" Text="Job Location:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblJobLocationValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblAvailability" runat="server" Text="Availability:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblAvailabilityValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                </div>

                <!-- Training Preferences -->
                <h5 class="mt-4 mb-3">Training Preferences</h5>
                <div class="row mb-3">
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblFreeTraining" runat="server" Text="Free Training:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblFreeTrainingValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblPaidTraining" runat="server" Text="Paid Training:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblPaidTrainingValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblCareerConsultantContact" runat="server" Text="Career Consultant Contact:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblCareerConsultantContactValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                </div>

                <!-- Resume and Portfolio -->
                <h5 class="mt-4 mb-3">Resume and Portfolio</h5>
                <div class="row mb-3">
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblResume" runat="server" Text="Resume:" CssClass="form-label"></asp:Label>
                         <asp:Label ID="lblResumeFile" runat="server"  CssClass="form-label"></asp:Label>
                        <!-- Add resume control here -->
                    </div>
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblCoverLetter" runat="server" Text="Cover Letter:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblCoverLetterValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblLinkedInProfile" runat="server" Text="LinkedIn Profile:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblLinkedInProfileValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                    <div class="col-md-6 border border-primary p-3">
                        <asp:Label ID="lblPortfolio" runat="server" Text="Portfolio:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblPortfolioValue" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                </div>

                <!-- Action Button -->
                <%-- <div class="row mb-3">
                        <div class="col-md-12 text-center border border-primary p-3">
                           <asp:Button ID="btnEditProfile" class="btn btn-primary" runat="server" Text="Edit Profile" OnClick="btnEditProfile_Click" />
                        </div>
                    </div>--%>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
