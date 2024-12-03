<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin1.Master" AutoEventWireup="true" CodeBehind="JobStatics.aspx.cs" Inherits="JobPortalWebApplication.Admin.JobStatics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
      .loading-spinner {
    border: 8px solid #f3f3f3;
    border-top: 8px solid #3498db;
    border-radius: 50%;
    width: 40px;
    height: 40px;
    animation: spin 1s linear infinite;
    margin: 0 auto;
}

@keyframes spin {
    0% { transform: rotate(0deg); }
    100% { transform: rotate(360deg); }
}

.metric p {
    font-size: 20px;
    margin-top: 10px;
}


    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      
  <main class="container mt-5">
    <div class="statistics-container p-4" style="max-width: 800px; margin: auto; background-color: #f8f9fa; border-radius: 8px; box-shadow: 0 2px 10px rgba(0,0,0,0.1);">
        <section class="overview row text-center">
            <div class="col-md-3 col-sm-6 metric mb-3">
                <h4>Total Employers</h4>
                <asp:Label ID="litTotalEmployers" runat="server" />
            </div>
            <div class="col-md-3 col-sm-6 metric mb-3">
                <h4>Total Candidates</h4>
                <asp:Label ID="litTotalCandidates" runat="server" />
            </div>
            <div class="col-md-3 col-sm-6 metric mb-3">
                <h4>Total Job Postings</h4>
                <asp:Label ID="litTotalJobPostings" runat="server" />
            </div>
            <div class="col-md-3 col-sm-6 metric mb-3">
                <h4>Total Applications</h4>
                <asp:Label ID="litTotalApplications" runat="server" />
            </div>
        </section>
    </div>
</main>

</asp:Content>
