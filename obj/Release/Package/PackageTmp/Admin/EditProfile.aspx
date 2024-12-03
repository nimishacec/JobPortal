<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin1.Master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="JobPortalWebApplication.Admin.EditProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
        <div class="container mt-5">
            <div class="row justify-content-center">
                 <div class="row mb-3">
     <div class="col-4">
         <asp:Label ID="Label2" runat="server" Text="<strong>Name:</strong>" CssClass="form-label"></asp:Label>
     </div>
     <div class="col-6">
         <asp:TextBox ID="txtName" runat="server"  CssClass="form-control"></asp:TextBox>
        
     </div>
 </div>
            <div class="row mb-3">
                <div class="col-4">
                    <asp:Label ID="Label1" runat="server" Text="<strong>Email Address:</strong>" CssClass="form-label"></asp:Label>
                </div>
                <div class="col-6">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                        ValidationExpression="^[\w\.-]+@[\w\.-]+\.\w+$" ErrorMessage="Invalid email format."
                        CssClass="text-danger" Display="Dynamic" />
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-4">
                    <asp:Label ID="lblPassword" runat="server" Text="<strong>Username:</strong>" CssClass="form-label"></asp:Label>
                </div>
                <div class="col-6">
                    <asp:TextBox ID="txtuseranme" runat="server"  CssClass="form-control"></asp:TextBox>
                   
                </div>
            </div>
            
           
            </div>
            <div class="row mb-3">
                <div class="col-4">
                    <asp:Button ID="ButtonRegister" runat="server" Text="Update Profile" class="btn btn-primary" OnClick="ButtonRegister_Click" />
                </div>
                <div class="col-6">
                    <asp:Button ID="Button1" runat="server" Text="Cancel" class="btn btn-secondary" OnClick="ButtonCancel_Click" CausesValidation="false" />
                    <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Red" Visible="true"></asp:Label>
                </div>
            </div>
                   <div>   <asp:Button ID="Button2" runat="server" Text="Back" OnClientClick="window.history.back(); return false;"  CssClass="btn btn-link"/>
</div>
  
</div>
</asp:Content>
