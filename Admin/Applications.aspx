<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin1.Master" AutoEventWireup="true" CodeBehind="Applications.aspx.cs" Inherits="JobPortalWebApplication.Admin.Applications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: left">
    <asp:Button ID="Button2" runat="server" Text="Back" OnClientClick="window.history.back(); return false;" CssClass="btn btn-link" />
</div> 
    <div class="col-12 grid-margin">
     <div class="card">
         <div class="card-body">
             <h4 class="card-title">Application Details</h4>


             <p class="card-description">
             </p>
             <%--    <asp:ValidationSummary ID="vsSummary" runat="server" ForeColor="Red" HeaderText="Please correct the following errors:" />--%>

             <div class="row">
                 <!-- Company Name -->
                 <div class="col-md-6">
                     <asp:Label ID="Label1" runat="server" Text="Candidate Name:" CssClass="form-label"></asp:Label>
                     <asp:Label ID="lblCandidateName" runat="server" CssClass="form-control mt-2"></asp:Label>

                 </div>
                 <div class="col-md-6">
                     <asp:Label ID="Label8" runat="server" Text="CandidateEmailAddress:" CssClass="form-label"></asp:Label>
                     <asp:Label ID="lblCandidateEmailAddress" runat="server" CssClass="form-control mt-2"></asp:Label>

                 </div>
             </div>
             <div class="row mt-2">
                 <div class="col-md-6">
                     <asp:Label ID="Label2" runat="server" Text="Company Name:" CssClass="form-label"></asp:Label>
                     <asp:Label ID="lblCompanyName" runat="server" CssClass="form-control mt-2"></asp:Label>
                 </div>



                 <!-- Company Name -->
                 <div class="col-md-6">
                     <asp:Label ID="Label3" runat="server" Text="Job Title:" CssClass="form-label"></asp:Label>
                     <asp:Label ID="lblJobTitle" runat="server" CssClass="form-control mt-2"></asp:Label>

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
                 <asp:Label ID="Label9" runat="server" Text="HighestEducationLevel:" CssClass="form-label"></asp:Label>
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
                 <asp:Label ID="Label11" runat="server" Text="Application Date:" CssClass="form-label"></asp:Label>
                 <asp:Label ID="lblApplicationDate" runat="server" CssClass="form-control mt-2"></asp:Label>

             </div>
             <div class="col-md-6">
                 <asp:Label ID="Label13" runat="server" Text="Application Deadline:" CssClass="form-label"></asp:Label>
                 <asp:Label ID="lblApplicationDeadline" runat="server" CssClass="form-control mt-2"></asp:Label>
             </div>
         </div>
        

         </div>        </div>
        <br />
    </div>
</asp:Content>

