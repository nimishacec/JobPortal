<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="JobPortalWebApplication.Candidate.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Bootstrap CSS -->
<link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />

<!-- Bootstrap JS -->
<script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!-- Button to open the modal -->
<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#messageModal">
    Open Messages
</button>

<!-- Message Modal -->
<div class="modal fade" id="messageModal" tabindex="-1" role="dialog" aria-labelledby="messageModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="messageModalLabel">Messages</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <!-- Message display area -->
                <asp:Repeater ID="rptMessages" runat="server">
                    <ItemTemplate>
                        <div class="message">
                            <strong><%# Eval("SenderName") %>:</strong> <%# Eval("MessageContent") %><br />
                            <small><%# Eval("DateSent", "{0:MM/dd/yyyy HH:mm}") %></small>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <!-- Message sending form -->
                <div class="form-group">
                    <label for="messageContent">Send a Message:</label>
                    <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSendMessage" runat="server" Text="Send" OnClick="btnSendMessage_Click" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>
</div>

        </div>
    </form>
</body>
</html>
