<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AdminReg.aspx.cs" Inherits="JobPortalWebApplication.Admin.AdminReg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="" style="background-color:aliceblue">
    <h3 class="text-center">Register</h3>
   
   <div class="d-flex justify-content-center align-items-center vh-100" style="background-color:aliceblue;">
 <div class="p-4 rounded" style="background-color: white; max-width: 500px; width: 100%;">
        <div class="row md-3">
            <div class="col-4">
                <asp:Label ID="lblFirstName" runat="server" Text="Name:" CssClass="form-label"></asp:Label>
                 </div>
                 <div class="col-8">
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rvfFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="Name is required." CssClass="text-danger" Display="Dynamic" />
                    </br>
            </div>
        </div>
        <div class="row md-3">
      <div class="col-4">
                <asp:Label ID="lblUserName" runat="server" Text="User Name:" CssClass="form-label"></asp:Label>
           </div>
 <div class="col-8">
                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
      <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName" ErrorMessage="User Name is required." CssClass="text-danger" Display="Dynamic" />
                </br>
            </div>
        </div>
         <div class="row md-3">
      <div class="col-4">
                <asp:Label ID="Label1" runat="server" Text="EmailAddress" CssClass="form-label"></asp:Label>
           </div>
 <div class="col-8">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
     <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required." CssClass="text-danger" Display="Dynamic" />

<asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ValidationExpression="^[\w\.-]+@[\w\.-]+\.\w+$"
    ErrorMessage="Invalid email format." CssClass="text-danger"
    Display="Dynamic" /></br>
            </div>
        </div>
        <div class="row md-3">
      <div class="col-4">
                <asp:Label ID="lblPassword" runat="server" Text="Password:" CssClass="form-label"></asp:Label>
           </div>
 <div class="col-8">
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
      <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required."
     CssClass="text-danger" Display="Dynamic" /></br>
            </div>
        </div>
          <div class="row mb-3">
      <div class="col-4">
                <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password:" CssClass="form-label"></asp:Label>
           </div>
 <div class="col-8">
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
      <asp:RequiredFieldValidator ID="rfvCPassword" runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="Confirm Password is required."
     CssClass="text-danger" Display="Dynamic" />
            
               <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="txtPassword"
                    ControlToValidate="txtConfirmPassword" ErrorMessage="Passwords do not match."
                    CssClass="text-danger" Display="Dynamic" /><br />
        </div></div>
        <div class="row mb-3">
      <div class="col-4">
                <asp:Label ID="lblMobileNumber" runat="server" Text="Mobile Number:" CssClass="form-label"></asp:Label>
           </div>
 <div class="col-8">
                <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control"></asp:TextBox>
       <asp:RequiredFieldValidator ID="rfvMobileNumber" runat="server" ControlToValidate="txtMobileNumber" ErrorMessage="Mobile Number is required." CssClass="text-danger" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revMobileNumber" runat="server" ControlToValidate="txtMobileNumber" ValidationExpression="^\d{10}$" ErrorMessage="Enter a valid 10-digit mobile number."
                    CssClass="text-danger" Display="Dynamic" /><br />
            </div>
        </div>
         <div class="row mb-3">
      <div class="col-6">
                <asp:Button ID="ButtonRegister" runat="server" Text="Register" class="btn btn-primary" OnClick="ButtonRegister_Click" />
          </div>
               <div class="col-6">
          <asp:Button ID="Button1" runat="server" Text="Login" class="btn btn-primary" OnClick="ButtonLogin_Click" CausesValidation="false" />
                <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Red" Visible="true"></asp:Label>
            </div>
        </div>
    </div>    </div>    </div>




</asp:Content>
