<%@ Page Title="" Language="C#" MasterPageFile="~/Candidate/Candidate.Master" AutoEventWireup="true" CodeBehind="FilteredResults.aspx.cs" Inherits="JobPortalWebApplication.Candidate.FilteredResults" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
         .grid-view-container {
            margin-top: 20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row grid-view-container">
                <div class="col-md-12">
                    <asp:Repeater ID="rptAppliedJobs" runat="server">
    <ItemTemplate>
        <div class="job-list-item" style="background-color: #f9f9f9; border: 1px solid #ddd; margin-bottom: 10px; padding: 10px;">
            <h3><%# Eval("JobTitle") %></h3>
            <a href='<%# "JobDetails.aspx?JobID=" + Eval("JobID") %>' class="btn btn-info">View Job Details</a>
            <p><strong>Job Description:</strong> <%# Eval("JobDescription") %></p>
            <p><strong>Company Name:</strong> <%# Eval("CompanyName") %></p>
            <p><strong>Job Location:</strong> <%# Eval("JobLocation") %></p>
            <p><strong>Annual Salary:</strong> <%# Eval("Salary") %></p>
             <p><strong>RequiredSkills:</strong> <%# Eval("RequiredSkills") %></p>

            <p><strong>ApplyBefore:</strong> <%# Eval("ApplicationDeadline") %></p>
          

        </div>
    </ItemTemplate>
</asp:Repeater>
                </div>
        </div>
    <asp:Label ID="lblNoJobs" runat="server" Text="No jobs found based on your search...." Visible="false" ForeColor="Red"></asp:Label>
</asp:Content>
