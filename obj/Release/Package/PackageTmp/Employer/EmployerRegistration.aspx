<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployerRegistration.aspx.cs" Inherits="JobPortalWebApplication.Employer.EmployerRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
            <meta charset="utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1" />
    
    <title></title>
       
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
<style>
    hidden {
         display: none;
     }  
    .navbar-custom {
        background-color: darkslateblue; /* Example color */
    }
</style>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>

</head>
<body>
     <nav class="navbar navbar-expand-lg navbar-custom">
     <h3 class="navbar-brand" href="#" style="color:white;  margin-left:10px">Job Portal</h3>
     <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
         <span class="navbar-toggler-icon"></span>
     </button>
     <div class="collapse navbar-collapse" id="navbarNav">
         <ul class="navbar-nav me-auto mb-2 mb-lg-0">
   <li class="nav-item">
   
  <a class="nav-link" href="/HomePage.aspx" style="color: white;">Home</a>
</li>
     </ul>
     </div>
 </nav>
    <form id="form1" runat="server">
   <div class="d-flex justify-content-center align-items-center vh-100" style="background-color:aliceblue;">
    <div class="p-4 rounded" style="background-color: white; max-width: 500px; width: 100%;">
         <div class="row mb-3">
            <div class="col-md-4 text-end">
                <asp:Label ID="Label1" runat="server" Text="Email Address:" CssClass="form-label"></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                    
    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required." CssClass="text-danger" Display="Dynamic" />

<asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ValidationExpression="^[\w\.-]+@[\w\.-]+\.\w+$"
    ErrorMessage="Invalid email format." CssClass="text-danger"
    Display="Dynamic" />
            </div>
        </div>

        <!-- Buttons Section -->
        <div class="row mb-3 justify-content-center">
            <div class="col-md-8">
                <asp:Button ID="Button1" runat="server" Text="Verify Email Address" CssClass="btn btn-primary mx-2" OnClick="Button1_Click" />
                <asp:Button ID="Button5" runat="server" Text="Login" CssClass="btn btn-success mx-2" OnClick="EmployerLogin_Click" CausesValidation="false" />
            </div>
        </div>

        <!-- Status Label -->
        <div class="row mb-3 justify-content-center">
            <div class="col-md-12">
                <asp:Label ID="lblStatus" runat="server" Text="Label" ForeColor="Red" CssClass="form-text" Visible="false"></asp:Label>
            </div>
        </div>
    </div>

    <!-- Registration Fields Section -->
  <div class="container mt-4" id="registrationFields" runat="server" visible="false">
    <div class="row mb-3">
        <div class="col-md-4 text-end">
            <asp:Label ID="Labelotp" runat="server" Text="OTP:" CssClass="form-label"></asp:Label>
        </div>
        <div class="col-md-8">
            <asp:TextBox ID="txtOTP" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvOTP" runat="server" ControlToValidate="txtOTP"
                ErrorMessage="OTP is required." CssClass="text-danger" Display="Dynamic" />
        </div>
    </div>

    <!-- Company Name Field -->
    <div class="row mb-3">
        <div class="col-md-4 text-end">
            <asp:Label ID="lblCompanyName" runat="server" Text="Company Name:" CssClass="form-label"></asp:Label>
        </div>
        <div class="col-md-8">
            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtCompanyName"
                ErrorMessage="Company Name is required." CssClass="text-danger" Display="Dynamic" />
        </div>
    </div>

    <!-- Password Field -->
    <div class="row mb-3">
        <div class="col-md-4 text-end">
            <asp:Label ID="lblPassword" runat="server" Text="Password:" CssClass="form-label"></asp:Label>
        </div>
        <div class="col-md-8">
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                ErrorMessage="Password is required." CssClass="text-danger" Display="Dynamic" />
        </div>
    </div>

    <!-- Confirm Password Field -->
    <div class="row mb-3">
        <div class="col-md-4 text-end">
            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password:" CssClass="form-label"></asp:Label>
        </div>
        <div class="col-md-8">
            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                ErrorMessage="Confirm Password is required." CssClass="text-danger" Display="Dynamic" />
            <asp:CompareValidator ID="cvPasswords" runat="server" ControlToValidate="txtConfirmPassword"
                ControlToCompare="txtPassword" ErrorMessage="Passwords do not match." CssClass="text-danger" Display="Dynamic" />
        </div>
    </div>

    <!-- Mobile Number Field -->
    <div class="row mb-3">
        <div class="col-md-4 text-end">
            <asp:Label ID="lblMobileNumber" runat="server" Text="Mobile Number:" CssClass="form-label"></asp:Label>
        </div>
        <div class="col-md-8">
            <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvMobileNumber" runat="server" ControlToValidate="txtMobileNumber"
                ErrorMessage="Mobile Number is required." CssClass="text-danger" Display="Dynamic" />
            <asp:RegularExpressionValidator ID="revMobileNumber" runat="server" ControlToValidate="txtMobileNumber"
                ValidationExpression="^\d{10}$" ErrorMessage="Enter a valid 10-digit mobile number."
                CssClass="text-danger" Display="Dynamic" />
        </div>
    </div>

    <!-- Terms and Conditions Section -->
    <div class="row mb-3">
        <div class="col-md-4 text-end">
            <label class="form-label">Agree to Terms and Conditions:</label>
        </div>
        <div class="col-md-8">
            <asp:RadioButton ID="rbtnYes" runat="server" GroupName="TermsConditions" Text="Yes" CssClass="form-check-input" Checked="true" />
            <asp:RadioButton ID="rbtnNo" runat="server" GroupName="TermsConditions" Text="No" CssClass="form-check-input" />
            <%--<asp:CustomValidator ID="cvTerms" runat="server" ControlToValidate="rbtnYes"
                ErrorMessage="You must agree to the terms and conditions." CssClass="text-danger" Display="Dynamic"
                OnServerValidate="cvTerms_ServerValidate" />--%>
        </div>
    </div>

    <!-- Register and Login Buttons -->
    <div class="row mb-3 justify-content-center">
        <div class="col-md-8">
            <asp:Button ID="ButtonRegister" runat="server" Text="Register" CssClass="btn btn-success mx-2" OnClick="ButtonRegister_Click" />
            <asp:Button ID="Button2" runat="server" Text="Login" CssClass="btn btn-success mx-2" OnClick="EmployerLogin_Click" />
        </div>
    </div>
</div>

</form>

</body>
</html>
