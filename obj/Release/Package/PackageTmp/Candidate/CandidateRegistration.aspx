<%@ Page AutoEventWireup="true" CodeBehind="CandidateRegistration.aspx.cs" Inherits="JobPortalWebApplication.Candidate.CandidateRegistration" Language="C#" %>

<!DOCTYPE html>



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
                <meta charset="utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1" />
     <style>
        .hidden {
            display: none;
        }
        .navbar-custom {
    background-color: darkslateblue; /* Example color */
}
    </style>
       <title></title>
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>

</head>
      <nav class="navbar navbar-expand-lg navbar-custom">
     <h3 class="navbar-brand" href="#" style="color:white; margin-left:10px">Job Portal</h3>
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
<body>
    <form id="form1" runat="server">
    <div class="d-flex justify-content-center align-items-center vh-100" style="background-color:aliceblue;">
    <div class="p-4 rounded" style="background-color: white; max-width: 500px; width: 100%;">
    <div class="row mb-3">
        <div class="col-md-4">
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
             <div class="row mb-3">
                <div class="col-md-8 d-flex justify-content-center">
            <asp:Button ID="Button1" runat="server" Text="Verify Email Address" CssClass="btn btn-primary" OnClick="Button1_Click" /> &nbsp;
              <asp:Button ID="Button4" runat="server" Text="Login" CssClass="btn btn-success" OnClick="ButtonLogin_Click"  CausesValidation="false"/>
            <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Red"  CssClass="form-text text-danger"></asp:Label>
        </div>
    </div>
         
    
    <!-- Registration Section -->
    
<div class="container mt-4" id="registrationFields" runat="server" visible="false">
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="txtOTP">OTP</label>
                    <asp:TextBox ID="txtOTP" runat="server" CssClass="form-control"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="tfvOtp" runat="server" ControlToValidate="txtOTP" ErrorMessage="OTP is required." CssClass="text-danger" Display="Dynamic" />
          
                </div>
                <div class="form-group">
                    <label for="txtFirstName">First Name</label>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="rvfFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="Name is required." CssClass="text-danger" Display="Dynamic" />
          
                </div>
                <div class="form-group">
                    <label for="txtLastName">Last Name</label>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName" ErrorMessage="Name is required." CssClass="text-danger" Display="Dynamic" />
          
                </div>
                <div class="form-group">
                    <label for="txtPassword">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required."
CssClass="text-danger" Display="Dynamic" /></br>
                </div>
                <div class="form-group">
                    <label for="txtConfirmPassword">Confirm Password</label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                 
      <asp:RequiredFieldValidator ID="rfvCPassword" runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="Confirm Password is required."
     CssClass="text-danger" Display="Dynamic" />
            
               <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="txtPassword"
                    ControlToValidate="txtConfirmPassword" ErrorMessage="Passwords do not match."
                    CssClass="text-danger" Display="Dynamic" /><br />
        </div></div>
                </div>
                <div class="form-group">
                    <label for="txtMobileNumber">Mobile Number</label>
                    <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control"></asp:TextBox>
                            
<asp:RequiredFieldValidator ID="rfvMobileNumber" runat="server" ControlToValidate="txtMobileNumber" ErrorMessage="Mobile Number is required." CssClass="text-danger" Display="Dynamic" />
         <asp:RegularExpressionValidator ID="revMobileNumber" runat="server" ControlToValidate="txtMobileNumber" ValidationExpression="^\d{10}$" ErrorMessage="Enter a valid 10-digit mobile number."
             CssClass="text-danger" Display="Dynamic" /><br />
                </div>
              
           
    
    
 <div class="row mb-3">
     <div class="col-md-8 d-flex justify-content-center">
      <asp:Button ID="ButtonRegister" runat="server" Text="Register" CssClass="btn btn-success" OnClick="ButtonRegister_Click" />
            <asp:Button ID="Button2" runat="server" Text="Login" CssClass="btn btn-success" OnClick="ButtonLogin_Click" CausesValidation="false" />
              </div>
</div>
        </div>
        </div>
     </div>
    </form>
</body>
</html>
