<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatusPage.aspx.cs" Inherits="JobPortalWebApplication.Candidate.StatusPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <meta http-equiv="refresh" content="5;url=CandidateViewDashboard.aspx" />
 <style>
     .text-success {
         color: green;
     }
     .alert-info {
    color: red;
}
 </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center; margin-top: 50px;">
        <asp:Label ID="lblStatusMessage" runat="server" CssClass="text-success"></asp:Label>
        <asp:Label ID="Label1" runat="server" CssClass="alert-info"></asp:Label>
    </div>
</form>
</body>
</html>
