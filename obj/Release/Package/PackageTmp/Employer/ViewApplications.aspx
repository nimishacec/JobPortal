<%@ Page Title="" Language="C#" MasterPageFile="~/Employer/Employer.Master" AutoEventWireup="true" CodeBehind="ViewApplications.aspx.cs" Inherits="JobPortalWebApplication.Employer.ViewApplications" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="text-align: center">Applications</h2>

  <asp:Repeater ID="rptAppliedJobs" runat="server" OnItemDataBound="rptAppliedJobs_ItemDataBound">
    <ItemTemplate>
        <div class="job-list-item" style="background-color: #f9f9f9; border: 1px solid #ddd; margin-bottom: 10px; padding: 10px;">
            <h3><%# Eval("JobTitle") %></h3>
            <a href='<%# ResolveUrl("~/Employer/JobDetails.aspx?JobID=" + Eval("JobID")) %>' class="btn btn-info">View Job Details</a>      
            <p><strong>Application Code:</strong> <%# Eval("ApplicationCode") %></p>
          <div class="row">
    <div class="col-md-8">
        <p><strong>Candidate Name:</strong> <%# Eval("CandidateName") %></p>
    </div>
    <div class="col-md-4 text-right">
        <asp:Button ID="btnSendMessage" runat="server" Text="Send Message" CssClass="btn btn-primary" CommandArgument='<%# Eval("CandidateID") + "," + Eval("JobID") %>' CommandName="SendMessage"  OnClick="btnSendMessage_Click"/>
    </div>
</div>

            <p><strong>Candidate Email:</strong> <%# Eval("CandidateEmailAddress") %></p>
            <p><strong>HighestEducationLevel:</strong> <%# Eval("HighestEducationLevel") %></p>
            <p><strong>Candidate PhoneNumber:</strong> <%# Eval("CandidatePhoneNumber") %></p>
            <p><strong>Applied Date:</strong> <%# Eval("ApplicationDate") %></p>
            <p><strong>Application Status:</strong> <%# Eval("ApplicationStatus") %></p>
            <p><strong>Resume:</strong>                   
              <a href='<%# ResolveUrl("~/Employer/DownloadFile.aspx?file=" + Eval("ResumePath").ToString() + "&CandidateID=" + Eval("CandidateID").ToString()) %>' target="_blank">Download Resume</a>
            </p>
            <asp:Button ID="btnApprove" runat="server" Text="Approve" 
                        CommandName="Verify" 
                        CommandArgument='<%# Eval("ApplicationID") + "," + Eval("JobID") %>' 
                        CssClass="btn btn-primary" OnClick="btnApprove_Click"  />
            <asp:Button ID="btnReject" runat="server" Text="Reject" 
                        CommandName="Reject" 
                        CommandArgument='<%# Eval("ApplicationID") + "," + Eval("JobID") %>' 
                        CssClass="btn btn-primary" OnClick="btnReject_Click"  />
        </div>
    </ItemTemplate>
</asp:Repeater>

       <asp:Label ID="lblNoJobs" runat="server" Text="You have not recieved any Job Applications." Visible="false" ForeColor="Red"></asp:Label>

</asp:Content>
