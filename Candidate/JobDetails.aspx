<%@ Page Title="" Language="C#" MasterPageFile="~/Candidate/Candidate.Master" AutoEventWireup="true" CodeBehind="JobDetails.aspx.cs" Inherits="JobPortalWebApplication.Candidate.JobDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="job-details-container" runat="server" id="JobDetailsContainer" style="font-family:Calibri;background-color:aliceblue">
            <h2 style="text-align:center">Job Details</h2>
            <div class="job-card" style="background-color:aliceblue;padding:20px; margin: 0 auto; width: 80%;">
                <h4>Job Title: <span id="lblJobTitle" runat="server"></span></h4>
                <p><strong>Company Name:</strong> <span id="lblCompanyName" runat="server"></span></p>
                <p><strong>Location:</strong> <span id="lblJobLocation" runat="server"></span></p>
                <p><strong>Job Type:</strong> <span id="lblJobType" runat="server"></span></p>
                <p><strong>Annual Salary(CTC):</strong> <span id="lblSalary" runat="server"></span></p>
                <p><strong>Vacancy:</strong> <span id="lblVacancy" runat="server"></span></p>
                <p><strong>Application Deadline:</strong> <span id="lblApplicationDeadline" runat="server"></span></p>
                <p><strong>Description:</strong> <span id="lblJobDescription" runat="server"></span></p>
                <p><strong>Required Skills:</strong> <span id="lblRequiredSkills" runat="server"></span></p>
                <p><strong>Contact Email:</strong> <span id="lblContactEmail" runat="server"></span></p>
                <p><strong>Company Website:</strong> <a id="lnkWebsite" runat="server" target="_blank">Visit</a></p>
                <div class="actions">
                    <asp:Button ID="ApplyButton" runat="server" Text="Apply Now" CssClass="btn btn-primary" OnClick="btnApply_Click" />
                </div>
               
            </div>
        </div>

     <div>   <asp:Button ID="Button1" runat="server" Text="Back" OnClientClick="window.history.back(); return false;" />
</div>
</asp:Content>
