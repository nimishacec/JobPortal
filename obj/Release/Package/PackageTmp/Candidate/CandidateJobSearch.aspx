<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Candidate/Candidate.Master" CodeBehind="CandidateJobSearch.aspx.cs"  Inherits="JobPortalWebApplication.Candidate.CandidateJobSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
   <div class="container mt-5">
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="lblKeyword" runat="server" Text="Keyword:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtKeyword" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblLocation" runat="server" Text="Location:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row mt-3">
            <div class="col-md-6">
                <asp:Label ID="lblMinSalary" runat="server" Text="Minimum Salary:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtMinSalary" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblMaxSalary" runat="server" Text="Maximum Salary:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtMaxSalary" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row mt-3">
            <div class="col-md-12">
                <asp:Label ID="lblRequiredSkills" runat="server" Text="Required Skills:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtRequiredSkills" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row mt-3">
            <div class="col-md-12 text-right">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
            </div>
        </div>
    </div>
      
</asp:Content>
