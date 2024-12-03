<%@ Page Title="" Language="C#" MasterPageFile="~/Candidate/Candidate.Master" AutoEventWireup="true" CodeBehind="JobAlerts.aspx.cs" Inherits="JobPortalWebApplication.Candidate.JobAlerts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
   

<div class="container mt-5">
   
        <div class="row mb-3">
            <div class="col-md-6">
                <label for="jobTitle" class="form-label">Job Title:</label>
                <asp:DropDownList ID="ddlJobTitle" runat="server"  CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-6">
                <label for="location" class="form-label">Location:</label>
              <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="form-control">
 </asp:DropDownList>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-6">
                <label for="industry" class="form-label">Required Skills:</label>
                <input type="text" class="form-control" id="requiredskills" name="requiredskills" placeholder="Enter Required Skills">
            </div>
            <div class="col-md-6">
                <label for="salaryRange" class="form-label">Minimum Salary:</label>
                <asp:TextBox ID="txtminSalary" runat="server" Placeholder="Enter  Minimum Salary" CssClass="form-control" > </asp:TextBox>
               
            </div>
             <div class="col-md-6">
     <label for="salaryRange" class="form-label">Maximum Salary:</label>
     <asp:TextBox ID="txtmaxSalary" runat="server" Placeholder="Enter  Maximum Salary" CssClass="form-control" > </asp:TextBox>
 </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-6">
                <label for="frequency" class="form-label">Notification Frequency:</label>
                <asp:DropDownList ID="ddlFrequency" runat="server" CssClass="form-control">
    <asp:ListItem Value="Daily" Text="Daily" />
    <asp:ListItem Value="Weekly" Text="Weekly" />
</asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 text-end">
                <asp:Button ID="btnJobAlert"  runat="server" CssClass="btn btn-primary" Text="Save Alert" OnClick="btnJobAlert_click" />
                
            </div>
        </div>
   
</div>

</asp:Content>
