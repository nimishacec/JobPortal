<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin1.Master" AutoEventWireup="true" CodeBehind="JobDetails.aspx.cs" Inherits="JobPortalWebApplication.Admin.JobDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="text-align: left">
        <asp:Button ID="Button2" runat="server" Text="Back" OnClientClick="window.history.back(); return false;" CssClass="btn btn-link" />
    </div>

    <div class="col-12 grid-margin">
        <div class="card">
            <div class="card-body">
                <h4 class="card-title">Job Details</h4>


                <p class="card-description">
                </p>
                <%--    <asp:ValidationSummary ID="vsSummary" runat="server" ForeColor="Red" HeaderText="Please correct the following errors:" />--%>

                <div class="row">
                    <!-- Company Name -->
                    <div class="col-md-6">
                        <asp:Label ID="Label1" runat="server" Text="Job Title:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblJobTitle" runat="server" CssClass="form-control mt-2"></asp:Label>

                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label8" runat="server" Text="Job Description:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblJobDescription" runat="server" CssClass="form-control mt-2"></asp:Label>

                    </div>
                </div>
                <div class="row mt-2">
                    <div class="col-md-6">
                        <asp:Label ID="Label2" runat="server" Text="Company Name:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblCompanyName" runat="server" CssClass="form-control mt-2"></asp:Label>
                    </div>



                    <!-- Company Name -->
                    <div class="col-md-6">
                        <asp:Label ID="Label3" runat="server" Text="Job Location:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblJobLocation" runat="server" CssClass="form-control mt-2"></asp:Label>

                    </div>
                </div>
            <div class="row mt-2">
                <div class="col-md-6">
                    <asp:Label ID="Label5" runat="server" Text="Job Type:" CssClass="form-label"></asp:Label>
                    <asp:Label ID="lblJobType" runat="server" CssClass="form-control mt-2"></asp:Label>
                </div>

                <!-- Company Name -->
                <div class="col-md-6">
                    <asp:Label ID="Label4" runat="server" Text="Annual Salary(CTC):" CssClass="form-label"></asp:Label>
                    <asp:Label ID="lblSalary" runat="server" CssClass="form-control mt-2"></asp:Label>

                </div>
            </div>

            <div class="row mt-2">
                <div class="col-md-6">
                    <asp:Label ID="Label7" runat="server" Text="Vacancy:" CssClass="form-label"></asp:Label>
                    <asp:Label ID="lblVacancy" runat="server" CssClass="form-control mt-2"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="Label12" runat="server" Text="Company Website:" CssClass="form-label"></asp:Label>
                    <asp:Label ID="lnkWebsite" runat="server" CssClass="form-control mt-2"></asp:Label>
                </div>
                <!-- Company Name -->

            </div>
            <div class="row mt-2">
                <div class="col-md-6">
                    <asp:Label ID="Label9" runat="server" Text="Qualifications:" CssClass="form-label"></asp:Label>
                    <asp:Label ID="lblQualifications" runat="server" CssClass="form-control mt-2"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="Label10" runat="server" Text="Experience:" CssClass="form-label"></asp:Label>
                    <asp:Label ID="lblExperience" runat="server" CssClass="form-control mt-2"></asp:Label>
                </div>
            </div>
            <div class="row mt-2">
                <!-- Company Name -->
                <div class="col-md-6">
                    <asp:Label ID="Label11" runat="server" Text="Required Skills:" CssClass="form-label"></asp:Label>
                    <asp:Label ID="lblRequiredSkills" runat="server" CssClass="form-control mt-2"></asp:Label>

                </div>
                <div class="col-md-6">
                    <asp:Label ID="Label13" runat="server" Text="Contact Email:" CssClass="form-label"></asp:Label>
                    <asp:Label ID="lblContactEmail" runat="server" CssClass="form-control mt-2"></asp:Label>
                </div>
            </div>
            <div class="row mt-2">
                <div class="col-md-6">
                    <asp:Label ID="Label6" runat="server" Text="Application Starts:" CssClass="form-label"></asp:Label>
                    <asp:Label ID="lblApplicationStarts" runat="server" CssClass="form-control mt-2"></asp:Label>

                </div>
                <div class="col-md-6">
                    <asp:Label ID="Label14" runat="server" Text="Application Deadline:" CssClass="form-label"></asp:Label>
                    <asp:Label ID="lblApplicationDeadline" runat="server" CssClass="form-control mt-2"></asp:Label>

                </div>
                <!-- Company Name -->


            </div>
            <%--<div class="job-details-container" runat="server" id="JobDetailsContainer" style="font-family: Calibri; background-color: aliceblue">
                    <h2 style="text-align: center">Job Details</h2>
                    <div class="job-card" style="background-color: aliceblue; padding: 20px; margin: 0 auto; width: 80%;">--%>
            <%-- <h4>Job Title: <span id="lblJobTitle" runat="server"></span></h4>--%>
            <%--  <p><strong>Company Name:</strong> <span id="lblCompanyName" runat="server"></span></p>--%>
            <%-- <p><strong>Location:</strong> <span id="lblJobLocation" runat="server"></span></p>--%>
            <%-- <p><strong>Job Type:</strong> <span id="lblJobType" runat="server"></span></p>--%>
            <%--<p><strong>Annual Salary(CTC):</strong> <span id="lblSalary" runat="server"></span></p>
                <p><strong>Vacancy:</strong> <span id="lblVacancy" runat="server"></span></p>
                <p><strong>Application Deadline:</strong> <span id="lblApplicationDeadline" runat="server"></span></p>
                <p><strong>Description:</strong> <span id="lblJobDescription" runat="server"></span></p>
                <p><strong>Required Skills:</strong> <span id="lblRequiredSkills" runat="server"></span></p>
                <p><strong>Contact Email:</strong> <span id="lblContactEmail" runat="server"></span></p>
                <p><strong>Company Website:</strong> <a id="lnkWebsite" runat="server" target="_blank">Visit</a></p>--%>
            <%-- <div class="actions">
                            <asp:Button ID="EditButton" runat="server" Text="Edit" CssClass="btn btn-primary" OnClick="btnEdit_Click" />
                        </div>
                    </div>
                    <div>
                        <asp:Button ID="Button2" runat="server" Text="Back" CssClass="btn btn-link" OnClick="Button2_Click" />

                    </div>--%>
        </div>
        <br />
    </div>
</asp:Content>
