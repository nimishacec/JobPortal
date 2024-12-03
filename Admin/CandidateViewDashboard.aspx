<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin1.Master" CodeBehind="CandidateViewDashboard.aspx.cs" Inherits="JobPortalWebApplication.Admin.CandidateViewDashboard" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .skills {
            display: flex;
            justify-content: space-between;
        }

        .education-details {
            margin-bottom: 15px;
        }

        .experience-details {
            margin-bottom: 15px;
        }

        .resume-btns {
            display: flex;
            align-content: center;
            justify-content: flex-start;
            flex-direction: row;
            gap: 10px;
        }

            .resume-btns a {
                display: flex;
                align-items: center;
                gap: 3px;
            }
    </style>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: left">
        <a href="CandidateList.aspx">Back</a>
    </div>

    <section>
        <div class="row">
            <div class="col-lg-4">
                <div class="card mb-4">
                    <div class="card-body text-center">
                        <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava3.webp" alt="avatar"
                            class="rounded-circle img-fluid" style="width: 150px;">
                        <h5 class="my-3">
                            <asp:Label ID="lblFullName" runat="server"></asp:Label>
                        </h5>
                        <p class="text-muted mb-1">
                            <asp:Label ID="lblEmail1" runat="server"></asp:Label>
                        </p>
                        <p class="text-muted mb-4">
                            <asp:Label ID="lblPhone1" runat="server"></asp:Label>

                        </p>
                        <%--<div class="d-flex justify-content-center mb-2">
                            <div class="row">
                                <div class="col-md-6">
                                    <p class="mb-0">Resume Status</p>
                                    <asp:Label ID="lblRStatus" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <asp:Button ID="Button1" runat="server" data-mdb-button-init="true" data-mdb-ripple-init="true" CssClass="btn btn-primary" Text="View Resume" OnClick="btnDownloadResume_Click" />
                                </div>
                            </div>
                           
                            </div>--%>
                        <%--<div class="d-flex justify-content-center mb-2">
                    <div class="row">
                        
                            <div class="col-md-3">
                                <asp:Button ID="btnAcceptResume" runat="server" Text="Accept" OnClick="btnAcceptResume_Click" data-mdb-button-init="true" data-mdb-ripple-init="true"  CssClass="btn btn-primary" /> &nbsp;
                                </div>
                         <div class="col-md-9">
<asp:Button ID="btnRejectResume" runat="server" Text="Reject" data-mdb-button-init="true" data-mdb-ripple-init="true"  OnClick="btnRejectResume_Click" CssClass="btn btn-warning " />

                        </div>
                    </div>
                        </div>--%>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnLinkedInProfile" runat="server" data-mdb-button-init="true" data-mdb-ripple-init="true" CssClass="btn btn-primary" Text="LinkedIn Profile" OnClick="btnLinkedInProfile_Click" />
                            &nbsp;
   
              <%-- <button  type="button" data-mdb-button-init data-mdb-ripple-init class="btn btn-primary">LinkedInProfile</button>--%>
                           <%-- <button type="button" data-mdb-button-init="true" data-mdb-ripple-init="true" class="btn btn-primary">Follow</button>--%>
                            <button type="button" data-mdb-button-init="true" data-mdb-ripple-init="true" class="btn btn-outline-primary ms-1">Portfolio</button>
                        </div>

                    </div>
                </div>
                <div class="card mb-4">
                    <div class="card-body">
                        <asp:ScriptManager ID="ScriptManager1" runat="server" />
                        <asp:UpdatePanel ID="upCandidateView" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <b>Resume:
                            <asp:Label ID="lblRStatus" runat="server"></asp:Label></b>
                                <div class="resume-btns mt-2">
                                    <asp:LinkButton ID="btnAcceptResume" runat="server" CssClass="btn btn-primary" 
                                        OnClick="btnAcceptResume_Click"><i class="bi bi-check-circle"></i>Accept</asp:LinkButton>

                                    <!-- Reject Button -->
                                    <asp:LinkButton ID="btnRejectResume" runat="server" CssClass="btn btn-warning"
                                        OnClick="btnRejectResume_Click"><i class="bi bi-x-circle"></i>Reject</asp:LinkButton>

                                    <!-- Download Button -->
                                    <asp:LinkButton ID="btnDwldResume" runat="server" CssClass="btn btn-success" 
                                        OnClick="btnDownloadResume_Click"><i class="mdi mdi-folder-download"></i>Download</asp:LinkButton>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAcceptResume" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnRejectResume" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="card mb-4 mb-lg-0">
                    <div class="card-body p-0">
                        <div class="p-2">Core Skills</div>
                        <asp:Label ID="lblCoreskill1" runat="server"></asp:Label>
                        <asp:Repeater ID="rptCoreSkills" runat="server">
                            <HeaderTemplate>
                                <ul class="list-group list-group-flush rounded-3">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li class="list-group-item justify-content-between align-items-center p-3">
                                    <p class="mt-1 mb-1 skills" style="font-size: .77rem;">
                                        <asp:Label ID="lblskillname" runat="server"><%# Eval("SkillName") %></asp:Label>
                                        <asp:Label ID="lblskillpercent" runat="server"><%# Eval("SkillPercentage") %>%</asp:Label>
                                    </p>
                                    <div class="progress rounded" style="height: 5px;">
                                        <div class="progress-bar" role="progressbar" style="width: <%# Eval("SkillPercentage") %>%" aria-valuenow="<%# Eval("SkillPercentage") %>"
                                            aria-valuemin="0" aria-valuemax="100">
                                        </div>
                                    </div>
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>   
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>

                <div class="card mt-4 mb-lg-0">
                    <div class="card-body p-0">
                        <div class="p-2">Soft Skills</div>
                        <asp:Label ID="lblSoftskill1" runat="server"></asp:Label>
                        <asp:Repeater ID="rptSoftSkills" runat="server">
                            <HeaderTemplate>
                                <ul class="list-group list-group-flush rounded-3">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li class="list-group-item justify-content-between align-items-center p-3">
                                    <p class="mt-1 mb-1 skills" style="font-size: .77rem;">
                                        <asp:Label ID="lblskillname" runat="server"><%# Eval("SkillName") %></asp:Label>
                                        <asp:Label ID="lblskillpercent" runat="server"><%# Eval("SkillPercentage") %>%</asp:Label>
                                    </p>
                                    <div class="progress rounded" style="height: 5px;">
                                        <div class="progress-bar" role="progressbar" style="width: <%# Eval("SkillPercentage") %>%" aria-valuenow="<%# Eval("SkillPercentage") %>"
                                            aria-valuemin="0" aria-valuemax="100">
                                        </div>
                                    </div>
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>   
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>

                <div class="card mt-4 mb-lg-0">
                    <div class="card-body p-0 ms-2">
                        <p class="mt-4">
                            Job Preferences
                        </p>
                        <div class="row">
                            <div class="col-md-6">
                                <p class="mb-0">Job Types</p>
                                <p class="text-muted mb-0">
                                    <asp:Label ID="lblJobType" runat="server"></asp:Label>
                                </p>
                            </div>
                            <div class="col-md-6 ">
                                <p class="mb-0">Job Location</p>
                                <p class="text-muted mb-0">
                                    <asp:Label ID="lblLocation" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <p class="mb-0">Availability</p>
                                <p class="text-muted mb-0">
                                    <asp:Label ID="lblAvail" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <hr />
                        <p class="mb-4">
                            Training Preferences
                        </p>
                        <div class="row">
                            <div class="col-md-6">
                                <p class="mb-0">Free Training</p>
                                <p class="text-muted mb-0">
                                    <asp:Label ID="lblFreeTraing" runat="server"></asp:Label>
                                </p>
                            </div>
                            <div class="col-md-6 ">
                                <p class="mb-0">Paid Training</p>
                                <p class="text-muted mb-0">
                                    <asp:Label ID="lblPaidTrainig" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <p class="mb-0">Career Consultant Contact</p>
                                <p class="text-muted mb-0">
                                    <asp:Label ID="lblCareerConsult" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="col-lg-8">
                <div class="card mb-4">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-sm-3">
                                <p class="mb-0">Full Name</p>
                            </div>
                            <div class="col-sm-9">
                                <p class="text-muted mb-0">
                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-sm-3">
                                <p class="mb-0">Address</p>
                            </div>
                            <div class="col-sm-9">
                                <p class="text-muted mb-0">
                                    <asp:Label ID="lblAddress1" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-sm-3">
                                <p class="mb-0">City</p>
                            </div>
                            <div class="col-sm-9">
                                <p class="text-muted mb-0">
                                    <asp:Label ID="lblCity1" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-sm-3">
                                <p class="mb-0">State</p>
                            </div>
                            <div class="col-sm-9">
                                <p class="text-muted mb-0">
                                    <asp:Label ID="lblState1" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-sm-3">
                                <p class="mb-0">Postal Code</p>
                            </div>
                            <div class="col-sm-9">
                                <p class="text-muted mb-0">
                                    <asp:Label ID="lblPostalCode1" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-sm-3">
                                <p class="mb-0">Country</p>
                            </div>
                            <div class="col-sm-9">
                                <p class="text-muted mb-0">
                                    <asp:Label ID="lblCountry1" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-sm-3">
                                <p class="mb-0">Highest Qualification</p>
                            </div>
                            <div class="col-sm-9">
                                <p class="text-muted mb-0">
                                    <asp:Label ID="lblHighest" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="card mb-4 mb-md-0">
                            <div class="card-body">
                                <p class="mb-4">
                                    Qualifications
                                </p>

                                <asp:Label ID="lblEducationDetails1" runat="server" Visible="false" Style="margin-left: 50px;"></asp:Label>
                                <asp:Repeater ID="rptEducationDetails1" runat="server">
                                    <ItemTemplate>
                                        <%--<div class="education-details">
                                    <div class="table table-bordered">--%>

                                        <div class="qualifications">
                                            <p><b>Gratuated <%# Eval("Degree") %></b></p>
                                            <p><%# Eval("CollegeUniversityName") %> , <%# Eval("PlaceAddress") %></p>
                                            <p>Project: <%# Eval("AcademicProject") %></p>
                                            <p>Key Skills: <%# Eval("KeySkills") %></p>
                                            <hr>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>

                        <%--                                    <div class="col-sm-6">
                                        <p class="mb-0">University Name</p>
                                        <p class="text-muted mb-0">
                                            <asp:Label ID="txtCollegeUniversityName" runat="server" Text='<%# Eval("CollegeUniversityName") %>'></asp:Label>
                                        </p>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-sm-6">
                                        <p class="mb-0">Address</p>

                                        <p class="text-muted mb-0">
                                            <asp:Label ID="txtPlaceAddress" runat="server" Text='<%# Eval("PlaceAddress") %>'></asp:Label>
                                        </p>
                                    </div>

                                    <div class="col-sm-6">
                                        <p class="mb-0">Graduated/Pursuing</p>

                                        <p class="text-muted mb-0">
                                            <asp:Label ID="txtGraduatedOrPursuing" runat="server" Text='<%# Eval("GraduatedOrPursuing") %>'></asp:Label>
                                        </p>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-6">
                                        <p class="mb-0">Key Skills</p>

                                        <p class="text-muted mb-0">
                                            <asp:Label ID="txtKeySkills" runat="server" Text='<%# Eval("KeySkills") %>'></asp:Label>
                                        </p>
                                    </div>

                                    <div class="col-sm-6">
                                        <p class="mb-0">Academic Project</p>

                                        <p class="text-muted mb-0">
                                            <asp:Label ID="txtAcademicProject" runat="server" Text='<%# Eval("AcademicProject") %>'></asp:Label>
                                        </p>
                                    </div>
                                </div>
                                <hr />--%>
                        <%--   </div>
                                </div>--%>


                        <!-- Show More/Less Button -->
                        <%--  <div class="text-right mt-3">
                            <a href="javascript:void(0);" class="btn btn-info" id="btnShowMoreLess" onclick="toggleEducationDetails()">Show More</a>
                        </div>--%>
                    </div>


                    <div class="col-md-12 mt-4">
                        <div class="card mb-4 mb-md-0">
                            <div class="card-body">

                                <p class="mb-4">
                                    Professional Experience
                                </p>

                                <asp:Label ID="lblExperience1" runat="server" Visible="false" Style="margin-left: 50px;"></asp:Label>
                                <asp:Repeater ID="rptExperienceDetails1" runat="server">
                                    <ItemTemplate>
                                        <p><b><%# Eval("CompanyName") %>, <%# Eval("CompanyAddress") %></b></p>
                                        <p><%# Eval("Designation") %></p>
                                        <p>Key Skills: <%# Eval("KeySkills") %></p>
                                        <hr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>

                        <%--  <asp:Label ID="lblExperience1" runat="server" Visible="false" Style="margin-left: 50px;"></asp:Label>
                        <asp:Repeater ID="rptExperienceDetails1" runat="server">
                            <ItemTemplate>
                                <%-- <div class="experience-details">
                                    <div class="table table-bordered">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <p class="mb-0">Company Name</p>

                                        <p class="text-muted mb-0">
                                            <asp:Label ID="lblCompanyNameValue" runat="server" Text='<%# Eval("CompanyName") %>'></asp:Label>
                                        </p>
                                    </div>


                                    <div class="col-sm-6">
                                        <p class="mb-0">Company Address</p>


                                        <p class="text-muted mb-0">
                                            <asp:Label ID="lblCompanyAddressValue" runat="server" Text='<%# Eval("CompanyAddress") %>'></asp:Label>
                                        </p>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-sm-6">
                                        <p class="mb-0">Designation</p>

                                        <p class="text-muted mb-0">
                                            <asp:Label ID="lblDesignationValue" runat="server" Text='<%# Eval("Designation") %>'></asp:Label>
                                        </p>
                                    </div>

                                    <div class="col-sm-6">
                                        <p class="mb-0">Key Skills</p>

                                        <p class="text-muted mb-0">

                                            <asp:Label ID="lblKeySkillsPracticedValue" runat="server" CssClass="form-control-plaintext" Text='<%# Eval("KeySkills") %>'></asp:Label>
                                        </p>
                                    </div>
                                </div>
                                <hr />
                            </ItemTemplate>
                        </asp:Repeater>
                        <%-- <div class="text-right mt-3">
                            <a href="javascript:void(0);" class="btn btn-info" id="btnShowMoreLessExp" onclick="toggleExperienceDetails()">Show More</a>
                        </div>
                    </div>
                </div>--%>
                    </div>
                </div>
                <%--</div>
                </div>
            </div>
        </div>--%>
    </section>












   <%--<%-- <%-- <div class="card">
              <%--  <h2 style="text-align:center">Employee Details</h2>--%>


    <%--<h4 style="text-align: center" class="mt-3">Candidate Details</h4>
    <hr />
    <h5 style="text-decoration: underline; margin-left: 20px;">Personal Details</h5>
    <hr />
    <div class="row ms-4">
        <div class="col-md-6">
            <asp:Label ID="lblFirstName" runat="server" Text="<b>First Name:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblFirstNameValue" runat="server"></asp:Label>

        </div>
        <div class="col-md-6">
            <asp:Label ID="lblLastName" runat="server" Text="<b>Last Name:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblLastNameValue" runat="server"></asp:Label>
        </div>
    </div>
    <hr />
    <div class="row ms-4">
        <div class="col-md-6">
            <asp:Label ID="lblEmail" runat="server" Text="<b>Email:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblEmailValue" runat="server"></asp:Label>
        </div>
        <div class="col-md-6">
            <asp:Label ID="lblPhone" runat="server" Text="<b>Phone Number:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblPhoneValue" runat="server"></asp:Label>
        </div>
    </div>
    <hr />
    <div class="row ms-4">
        <div class="col-md-6">
            <asp:Label ID="lblAddress" runat="server" Text="<b>Address:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblAddressValue" runat="server"></asp:Label>
        </div>
        <div class="col-md-6">
            <asp:Label ID="lblCity" runat="server" Text="<b>City:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblCityValue" runat="server"></asp:Label>
        </div>
    </div>
    <hr />
    <div class="row ms-4">
        <div class="col-md-6">
            <asp:Label ID="lblState" runat="server" Text="<b>State/Province:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblStateValue" runat="server"></asp:Label>
        </div>
        <div class="col-md-6">
            <asp:Label ID="lblPostalCode" runat="server" Text="<b>Postal/Zip Code:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblPostalCodeValue" runat="server"></asp:Label>
        </div>
    </div>
    <hr />
    <div class="row ms-4">
        <div class="col-md-6">
            <asp:Label ID="lblCountry" runat="server" Text="<b>Country:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblCountryValue" runat="server"></asp:Label>
        </div>
        <div class="col-md-6">
            <asp:Label ID="lblHighestEducation" runat="server" Text="<b>Highest Education Level:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblHighestEducationValue" runat="server"></asp:Label>
        </div>
    </div>

    <hr />
    <h5 style="text-decoration: underline; margin-left: 20px;">EducationDetails</h5>
    <asp:Label ID="lblEducationDetails" runat="server" Visible="false" Style="margin-left: 50px;"></asp:Label>
    <asp:Repeater ID="rptEducationDetails" runat="server">

        <ItemTemplate>

            <hr />
            <div class="row ms-4">
                <div class="col-md-6">
                    <asp:Label ID="lblDegree" runat="server" Text=" DegreeName " CssClass="form-label"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Degree") %>'></asp:Label>

                    <%--<asp:DropDownList ID="ddlDegreeId" runat="server" CssClass="form-control" DataTextField="DegreeName" DataValueField="DegreeId">
                    </asp:DropDownList>-
                </div>
                <div class="col-md-6">
                    <asp:Label ID="Label9" runat="server" Text=" CollegeUniversityName " CssClass="form-label"></asp:Label>
                    <asp:Label ID="txtCollegeUniversityName" runat="server" Text='<%# Eval("CollegeUniversityName") %>'></asp:Label>
                </div>
            </div>
            <hr />
            <div class="row ms-4">
                <div class="col-md-6">
                    <asp:Label ID="Label10" runat="server" Text=" PlaceAddress " CssClass="form-label"></asp:Label>
                    <asp:Label ID="txtPlaceAddress" runat="server" Text='<%# Eval("PlaceAddress") %>'></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="Label11" runat="server" Text=" GraduatedOrPursuing " CssClass="form-label"></asp:Label>
                    <asp:Label ID="txtGraduatedOrPursuing" runat="server" Text='<%# Eval("GraduatedOrPursuing") %>'></asp:Label>
                </div>
            </div>
            <hr />
            <div class="row ms-4">
                <div class="col-md-6">
                    <asp:Label ID="Label12" runat="server" Text=" KeySkills " CssClass="form-label"></asp:Label>
                    <asp:Label ID="txtKeySkills" runat="server" Text='<%# Eval("KeySkills") %>'></asp:Label>
                </div>
                <div class="col-md-6 ">
                    <asp:Label ID="Label13" runat="server" Text=" AcademicProject " CssClass="form-label"></asp:Label>
                    <asp:Label ID="txtAcademicProject" runat="server" Text='<%# Eval("AcademicProject") %>'></asp:Label>
                </div>
            </div>

             <div class="form-group">
                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Container.ItemIndex %>' CssClass="btn btn-secondary">Edit</asp:LinkButton>
                
    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("EducationId") %>' CssClass="btn btn-danger">Delete</asp:LinkButton>
</div>  
        </ItemTemplate>

    </asp:Repeater>
    <hr />
    <!-- Work Experience -->
    <h5 style="text-decoration: underline; margin-left: 20px;">Work Experience</h5>
    <asp:Label ID="lblExperienceDetails" runat="server" Visible="false" Style="margin-left: 50px;"></asp:Label>
    <hr />

    <asp:Repeater ID="rptExperienceDetails" runat="server">
        <ItemTemplate>

            <div class="row ms-4">
                <div class="col-md-6">
                    <asp:Label ID="lblCompanyName" runat="server" Text="<b>Company Name:</b>" CssClass="form-label"></asp:Label>
                    <asp:Label ID="lblCompanyNameValue" runat="server" Text='<%# Eval("CompanyName") %>'></asp:Label>
                </div>
                <div class="col-md-6 ">
                    <asp:Label ID="lblCompanyAddress" runat="server" Text="<b>Company Address:</b>" CssClass="form-label"></asp:Label>
                    <asp:Label ID="lblCompanyAddressValue" runat="server" Text='<%# Eval("CompanyAddress") %>'></asp:Label>
                </div>
            </div>
            <hr />
            <div class="row ms-4">
                <div class="col-md-6">
                    <asp:Label ID="lblDesignation" runat="server" Text="<b>Designation:</b>" CssClass="form-label"></asp:Label>
                    <asp:Label ID="lblDesignationValue" runat="server" Text='<%# Eval("Designation") %>'></asp:Label>
                </div>
                <div class="col-md-6 ">
                    <asp:Label ID="lblKeySkillsPracticed" runat="server" Text="<b>Key Skills Practiced:</b>" CssClass="form-label"></asp:Label>
                    <asp:Label ID="lblKeySkillsPracticedValue" runat="server" CssClass="form-control-plaintext" Text='<%# Eval("KeySkills") %>'></asp:Label>
                </div>
            </div>
            <hr />
        </ItemTemplate>
    </asp:Repeater>

    <!-- Core Skills -->
    <h5 style="text-decoration: underline; margin-left: 20px;">Core Skills</h5>
    <hr />
    <div class="row ms-4">
        <div class="col-md-6">
            <asp:Label ID="lblCoreSkill" runat="server" Text="<b>Core Skills:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblCoreSkillValue" runat="server"></asp:Label>
        </div>
        <div class="col-md-6 ">
            <asp:Label ID="lblCoreSkillPercentage" runat="server" Text="<b>Core Skills Percentage:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblCoreSkillPercentageValue" runat="server"></asp:Label>
        </div>
    </div>
    <hr />
    <!-- Soft Skills -->
    <h5 style="text-decoration: underline; margin-left: 20px;">Soft Skills</h5>
    <hr />
    <div class="row ms-4">
        <div class="col-md-6">
            <asp:Label ID="lblSoftSkill" runat="server" Text="<b>Soft Skills:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblSoftSkillValue" runat="server"></asp:Label>
        </div>
        <div class="col-md-6 ">
            <asp:Label ID="lblSoftSkillPercentage" runat="server" Text="<b>Soft Skills Percentage:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblSoftSkillPercentageValue" runat="server"></asp:Label>
        </div>
    </div>
    <hr />
    <!-- Job Preferences -->
    <h5 style="text-decoration: underline; margin-left: 20px;">Job Preferences</h5>
    <hr />
    <div class="row ms-4">
        <div class="col-md-6">
            <asp:Label ID="lblJobTypes" runat="server" Text="<b>Job Types:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblJobTypesValue" runat="server"></asp:Label>
        </div>
        <div class="col-md-6 ">
            <asp:Label ID="lblJobLocation" runat="server" Text="<b>Job Location:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblJobLocationValue" runat="server"></asp:Label>
        </div>
    </div>
    <hr />
    <div class="row ms-4">
        <div class="col-md-6">
            <asp:Label ID="lblAvailability" runat="server" Text="<b>Availability:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblAvailabilityValue" runat="server"></asp:Label>
        </div>
    </div>
    <hr />
    <!-- Training Preferences -->
    <h5 style="text-decoration: underline; margin-left: 20px;">Training Preferences</h5>
    <hr />
    <div class="row ms-4">
        <div class="col-md-6">
            <asp:Label ID="lblFreeTraining" runat="server" Text="<b>Free Training:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblFreeTrainingValue" runat="server"></asp:Label>
        </div>
        <div class="col-md-6 ">
            <asp:Label ID="lblPaidTraining" runat="server" Text="<b>Paid Training:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblPaidTrainingValue" runat="server"></asp:Label>
        </div>
    </div>
    <hr />
    <div class="row ms-4">
        <div class="col-md-6">
            <asp:Label ID="lblCareerConsultantContact" runat="server" Text="<b>Career Consultant Contact:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblCareerConsultantContactValue" runat="server"></asp:Label>
        </div>
    </div>
    <hr />
    <!-- Resume and Portfolio -->
    <h5 style="text-decoration: underline; margin-left: 20px;">Resume and Portfolio</h5>
    <hr />
    <div class="row ms-4">
        <div class="col-md-6">
            <asp:Label ID="Label1" runat="server" Text="<b>Resume Status:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblResumeStatus" runat="server" CssClass="badge badge-info"></asp:Label>
        </div>
        <div class="col-md-6 ">
            <asp:Label ID="lblResumeFile" runat="server" Text="<b>Resume/CV:</b>" CssClass="form-label"></asp:Label>
            <asp:Button ID="btnDownloadResume" runat="server" Text="Download Resume" OnClick="btnDownloadResume_Click" CssClass="btn btn-link " />
                    <asp:Button ID="btnAcceptResume" runat="server" Text="Accept" OnClick="btnAcceptResume_Click" CssClass="btn bg-primary" />
            <asp:Button ID="btnRejectResume" runat="server" Text="Reject" OnClick="btnRejectResume_Click" CssClass="btn btn-outline-warning " />

             <asp:Button ID="Button2" runat="server" Text="Delete" OnClick="btnDeleteResume_Click" CssClass="btn btn btn-outline-danger" />
        </div>

        <hr />
        <div class="col-md-6 ">
            <asp:Label ID="lblCoverLetter" runat="server" Text="<b>Cover Letter:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblCoverLetterValue" runat="server"></asp:Label>
        </div>
    </div>
    <hr />
    <div class="row ms-4">
        <div class="col-md-6">
            <asp:Label ID="lblLinkedInProfile" runat="server" Text="<b>LinkedIn Profile:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblLinkedInProfileValue" runat="server"></asp:Label>
        </div>
        <div class="col-md-6 ">
            <asp:Label ID="lblPortfolio" runat="server" Text="<b>Portfolio:</b>" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblPortfolioValue" runat="server"></asp:Label>
        </div>
    </div>
    <hr />
    <!-- Action Button -->
     <div class="row mb-3">
                        <div class="col-md-12 text-center border border-primary p-3">
                           <asp:Button ID="btnEditProfile" class="btn btn-primary" runat="server" Text="Edit Profile" OnClick="btnEditProfile_Click" />
                        </div>
                    </div>--%>

    <script>
        //function toggleEducationDetails() {
        //    // Get all education detail rows
        //    var educationDetails = document.querySelectorAll('.education-details');
        //    var btn = document.getElementById('btnShowMoreLess');

        //    // If collapsed, expand
        //    if (btn.innerHTML === "Show More") {
        //        educationDetails.forEach(function (detail, index) {
        //            detail.style.display = "block"; // Show all education details
        //        });
        //        btn.innerHTML = "Show Less"; // Update button text to "Show Less"
        //    }
        //    // If expanded, collapse
        //    else {
        //        educationDetails.forEach(function (detail, index) {
        //            if (index > 0) { // Keep the first detail visible
        //                detail.style.display = "none"; // Hide all except the first
        //            }
        //        });
        //        btn.innerHTML = "Show More"; // Update button text to "Show More"
        //    }
        //}

        //function toggleExperienceDetails() {
        //    var experienceDetails = document.querySelectorAll('.experience-details');
        //    var btnExp = document.getElementById('btnShowMoreLessExp');

        //    // If collapsed, expand
        //    if (btnExp.innerHTML === "Show More") {
        //        experienceDetails.forEach(function (detail, index) {
        //            detail.style.display = "block"; // Show all experience details
        //        });
        //        btnExp.innerHTML = "Show Less"; // Update button text to "Show Less"
        //    }
        //    // If expanded, collapse
        //    else {
        //        experienceDetails.forEach(function (detail, index) {
        //            if (index > 0) { // Keep the first experience visible
        //                detail.style.display = "none"; // Hide all except the first
        //            }
        //        });
        //        btnExp.innerHTML = "Show More"; // Update button text to "Show More"
        //    }
        //}

        //// Combined onload function to hide all except the first education and experience detail on page load
        //window.onload = function () {
        //    // Hide extra education details
        //    var educationDetails = document.querySelectorAll('.education-details');
        //    educationDetails.forEach(function (detail, index) {
        //        if (index > 0) { // Skip the first one
        //            detail.style.display = "none"; // Hide all except the first
        //        }
        //    });

        //    // Hide extra experience details
        //    var experienceDetails = document.querySelectorAll('.experience-details');
        //    experienceDetails.forEach(function (detail, index) {
        //        if (index > 0) { // Skip the first one
        //            detail.style.display = "none"; // Hide all except the first
        //        }
        //    });
        //};
    </script>


</asp:Content>
