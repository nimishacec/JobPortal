<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewMessage.aspx.cs" Inherits="JobPortalWebApplication.Employer.ViewMessage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script>
        $(document).ready(function() {
    $('#messageTab a').on('click', function (e) {
        e.preventDefault();
        $(this).tab('show');
    });
});

    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container">
    <!-- Tab Navigation -->
   <%-- <ul class="nav nav-tabs" id="messageTab" role="tablist">
        <li class="nav-item">
            <a class="nav-link active" id="received-tab" data-toggle="tab" href="#received" role="tab" aria-controls="received" aria-selected="true">Received Messages</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" id="sent-tab" data-toggle="tab" href="#sent" role="tab" aria-controls="sent" aria-selected="false">Sent Messages</a>
        </li>
    </ul>--%>

    <!-- Tab Content -->
  <div class="container">
            <div class="row">
                <!-- Messages list -->
                <div class="col-md-4">
                    <h3>Messages</h3>
                   <asp:Repeater ID="MessagesListRepeater" runat="server" OnItemCommand="MessageSelected">
    <ItemTemplate>
        <div>
            <asp:LinkButton ID="MessageLink" runat="server" CommandArgument='<%# Eval("SenderID") %>'>
                <%# Eval("SenderName") %>
            </asp:LinkButton>
        </div>
    </ItemTemplate>
</asp:Repeater>

<!-- Placeholder to show if no messages are present -->
<asp:Panel ID="NoMessagesPanel" runat="server" Visible="false">
    <p>No messages available at this time.</p>
</asp:Panel>

                </div>

                <!-- Chat box -->
                <div class="col-md-8">
                    <div>
                        <asp:Label ID="lblSelectedEmployer" runat="server" Text="Select a conversation to view the messages" Font-Bold="true"></asp:Label>
                        <div id="conversationBox">
                            <asp:Repeater ID="ConversationRepeater" runat="server">
                                <ItemTemplate>
                                    <p><strong><%# Eval("SenderName") %>:</strong> <%# Eval("MessageContent") %></p>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div>
                            <asp:TextBox ID="txtMessageContent" runat="server" TextMode="MultiLine" Rows="3" Width="100%"></asp:TextBox>
                            <asp:Button ID="btnSendMessage" runat="server" Text="Send" OnClick="btnSendMessage_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</div>


       
    </form>
</body>
</html>
