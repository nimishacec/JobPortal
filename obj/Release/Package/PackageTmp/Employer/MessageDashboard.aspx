<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageDashboard.aspx.cs" Inherits="JobPortalWebApplication.Employer.MessageDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtSubject" runat="server" Placeholder="Subject"></asp:TextBox><br />
<asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="4" Columns="50" Placeholder="Your Message"></asp:TextBox><br />

<!-- Checkboxes to select communication method -->
<asp:CheckBox ID="chkSendInternalMessage" runat="server" Text="Send as Internal Message" /><br />
<asp:CheckBox ID="chkSendEmail" runat="server" Text="Send via Email" /><br />

<!-- Hidden fields for CandidateID and JobID -->
<%--<asp:HiddenField ID="hdnCandidateID" runat="server" Value='<%# Eval("CandidateID") %>' />
<asp:HiddenField ID="hdnJobID" runat="server" Value='<%# Eval("JobID") %>' />--%>

<!-- Send button -->
<asp:Button ID="btnSendMessage" runat="server" Text="Send Message" OnClick="btnSendMessage_Click" />
<asp:Label ID="lblStatus" runat="server" ForeColor="Green" />

        </div>
    </form>
</body>
</html>
