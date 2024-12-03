<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewMessage.aspx.cs" Inherits="JobPortalWebApplication.Candidate.ViewMessage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>.chat-box {
    width: 100%;
    height: 500px;
    border: 1px solid #ccc;
    padding: 10px;
    overflow-y: scroll;
    margin-bottom: 20px;
}

.message {
    margin-bottom: 15px;
}

.message strong {
    color: #007bff;
}

.message .timestamp {
    font-size: 0.8em;
    color: #888;
}

.chat-input {
    display: flex;
}

.chat-input input {
    flex: 1;
    margin-right: 10px;
}
</style>
    <script>
        //function loadConversation(employerId) {
        //    $.ajax({
        //        url: '/api/messages/getConversation', // Your API endpoint
        //        type: 'POST',
        //        data: { employerId: employerId },
        //        success: function (response) {
        //            // Update the chat box with the received messages
        //            $('#conversationBox').html(response);
        //        }
        //    });
        }
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
 <div class="container">
            <div class="row">
                <!-- Messages list -->
                <div class="col-md-4">
                    <h3>Messages</h3>
                    <asp:Repeater ID="MessagesListRepeater" runat="server" OnItemCommand="MessageSelected">
                        <ItemTemplate>
                            <div>
                                <asp:LinkButton ID="MessageLink" runat="server" CommandArgument='<%# Eval("SenderID") %>'>
                                    <%# Eval("CompanyName") %>
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
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
    </form>
</body>
</html>
