<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatusPage.aspx.cs" Inherits="JobPortalWebApplication.Trainer.StatusPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="refresh" content="5;url=TrainerViewDash.aspx" />
    <style>
        .text-success {
            color: green;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:center; margin-top: 50px;">
            <asp:Label ID="lblStatusMessage" runat="server" CssClass="text-success"></asp:Label>
        </div>
    </form>
    </body>
    </html>
