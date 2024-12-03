<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainerLogin.aspx.cs" Inherits="JobPortalWebApplication.Trainer.TrainerLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta charset="utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1" />
        <title></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <style>
        .navbar-custom {
            background-color: darkslateblue; /* Example color */
        }
    </style>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>


</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-light navbar-custom">
        <a class="navbar-brand" href="#" style="color:white">Job Portal</a>
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
                    <div class="col-3">
                        <label for="txtEmail">Email:</label>
                    </div>
                    <div class="col-8">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required." CssClass="text-danger" Display="Dynamic" />

<asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ValidationExpression="^[\w\.-]+@[\w\.-]+\.\w+$"
    ErrorMessage="Invalid email format." CssClass="text-danger"
    Display="Dynamic" />
                    </div>
                </div>
                <div class="row mb-3">
                    <div class="col-3">
                        <label for="txtPassword">Password:</label>
                    </div>
                    <div class="col-8">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required."
CssClass="text-danger" Display="Dynamic" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-2">
                     <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn btn-success" />

                    </div>
                    <div class="col-4">
 <asp:Button ID="Button2" runat="server" Text="ForgotPassword??" OnClick="btnResetPwd_Click" CssClass="btn btn-warning" />

</div>
                    <div class="col-6">
                        <asp:Button ID="Button1" runat="server" Text="Register" OnClick="btnRegister_Click" CssClass="btn btn-primary" />
                       
                    </div>
                </div>
            </div>

        </div>



        <asp:Label ID="lblStatus" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </form>
</body>
</html>
