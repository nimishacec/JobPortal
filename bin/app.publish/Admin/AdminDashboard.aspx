<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin1.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="JobPortalWebApplication.Admin.AdminDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="text-align: justify">
    <h2 class="text-center">Admin Details</h2>
      
    <div class="row mb-3">
        <div class="col-md-2">
            <asp:Label ID="lblName" runat="server" Text="<strong>Name:</strong>" CssClass="form-label"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:Label ID="txtname" runat="server" CssClass="form-control-plaintext"></asp:Label>
        </div>
    </div>

    <div class="row mb-3">
        <div class="col-md-2">
            <asp:Label ID="lblUsername" runat="server" Text="<strong>Username:</strong>" CssClass="form-label"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:Label ID="txtusername" runat="server" CssClass="form-control-plaintext"></asp:Label>
        </div>
    </div>

    <div class="row mb-3">
        <div class="col-md-2">
            <asp:Label ID="lblEmail" runat="server" Text="<strong>Email:</strong>" CssClass="form-label"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:Label ID="txtemail" runat="server" CssClass="form-control-plaintext"></asp:Label>
        </div>
    </div>
</div>
    <div class="row">
        <div class="col-md-6 text-center">
            <asp:Button ID="btnEditProfile" class="btn btn-primary" runat="server" Text="Edit Profile" OnClick="btnEditProfile_Click" />
        </div>
    </div>


    
</asp:Content>
