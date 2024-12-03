<%@ Page Title="" Language="C#" MasterPageFile="~/Candidate/Candidate.Master" AutoEventWireup="true" CodeBehind="ViewAllJobs.aspx.cs" Inherits="JobPortalWebApplication.Candidate.ViewAllJobs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div style="margin: 50px;">
   <h2 style="text-align:center">All Jobs</h2>
    <asp:GridView ID="gvJobs" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" AlternatingRowStyle-BorderStyle="Solid" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvJobs_PageIndexChanging">
    <Columns>
        <asp:BoundField DataField="JobID" HeaderText="Job ID" />
        <asp:TemplateField HeaderText="Job Title">
            <ItemTemplate>
                <asp:HyperLink ID="hlJobTitle" runat="server" 
                    NavigateUrl='<%# "JobDetails.aspx?JobID=" + Eval("JobID") %>' 
                    Text='<%# Eval("JobTitle") %>'></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
        <asp:BoundField DataField="JobLocation" HeaderText="Location" />
        <asp:BoundField DataField="JobType" HeaderText="Job Type" />
         <asp:BoundField DataField="ApplicationDeadLine" HeaderText="LastDate to Apply" />
           <asp:BoundField DataField="RequiredSkills" HeaderText="Required Skills" />
         <asp:TemplateField HeaderText="Status" ItemStyle-ForeColor="#006600">
            <ItemTemplate>
                <%# GetAppliedStatus(Eval("JobID")) %>
            </ItemTemplate>
        </asp:TemplateField>
      
    </Columns>
</asp:GridView>
        </div>
</asp:Content>
