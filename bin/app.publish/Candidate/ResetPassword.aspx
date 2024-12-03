<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" MasterPageFile="~/Home.Master" Inherits="JobPortalWebApplication.Candidate.ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <div class="d-flex justify-content-center align-items-center" style="height: 100vh;">
    <div class="form-control" style="max-width: 400px; text-align: center; padding: 20px;">
        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" Placeholder="Enter new password" CssClass="form-control mb-3" />
        <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" CssClass="btn btn-primary mb-3" OnClick="btnResetPassword_Click" />
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
    </div>
</div>

    </asp:Content>