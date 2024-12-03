<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordResetRequest.aspx.cs" MasterPageFile="~/Home.Master" Inherits="JobPortalWebApplication.Admin.PasswordResetRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-control d-flex justify-content-center align-items-center" style="margin-top: 10px; background-color: lavender; height: 100vh;">

        <div class="row">
            <div class="col-12">
                <div class="text-center">
                    <div style="text-align: center;">
                        <asp:TextBox ID="txtEmail" runat="server" Placeholder="Enter your email" CssClass="form-control mb-3" Style="width: 300px; margin: 0 auto;" />
                    </div>
                    <div style="text-align: center;">
                        <asp:Button ID="btnRequestReset" runat="server" Text="Reset Password" CssClass="btn btn-primary mb-3" OnClick="btnRequestReset_Click" />
                    </div>
                    <div style="text-align: center;">
                        <asp:Label ID="lblMessage" runat="server" Visible="false" ForeColor="DarkGreen"></asp:Label>
                    </div>

                </div>
            </div>
        </div>

    </div>

</asp:Content>

