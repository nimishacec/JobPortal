<%@ Page Title="" Language="C#" MasterPageFile="~/Candidate/Candidate.Master" AutoEventWireup="true" CodeBehind="ViewAppliedJobs.aspx.cs" Inherits="JobPortalWebApplication.Candidate.ViewAppliedJobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="text-align:center">Applied Jobs</h2>

    <asp:Repeater ID="rptAppliedJobs" runat="server">
        <ItemTemplate>
            <div class="job-list-item" style="background-color: #f9f9f9; border: 1px solid #ddd; margin-bottom: 10px; padding: 10px;">
                <h3><%# Eval("JobTitle") %></h3>
                <a href='<%# "JobDetails.aspx?JobID=" + Eval("JobID") %>' class="btn btn-info">View Job Details</a>
                <p><strong>Job Description:</strong> <%# Eval("CompanyName") %></p>
                <p><strong>Company Name:</strong> <%# Eval("CompanyName") %></p>
                <p><strong>Job Location:</strong> <%# Eval("JobLocation") %></p>
                <p><strong>Annual Salary:</strong> <%# Eval("Salary") %></p>


                <p><strong>LastDate:</strong> <%# Eval("ApplicationDeadline") %></p>
                <p><strong>ApplicationStatus:</strong> <%# Eval("ApplicationStatus") %></p>

            </div>
        </ItemTemplate>
    </asp:Repeater>

    <asp:Label ID="lblNoJobs" runat="server" Text="You have not applied to any jobs." Visible="false" ForeColor="Red"></asp:Label>
         <div>   <asp:Button ID="Button1" runat="server" Text="Back" OnClientClick="window.history.back(); return false;"  CssClass="btn btn-link"/>
</div>
</asp:Content>
