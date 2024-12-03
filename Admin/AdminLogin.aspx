<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="JobPortalWebApplication.Admin.AdminLogin" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Skydash Admin</title>
    <!-- plugins:css -->
    <link rel="stylesheet" href="../vendors/feather/feather.css">
    <link rel="stylesheet" href="../vendors/ti-icons/css/themify-icons.css">
    <link rel="stylesheet" href="../vendors/css/vendor.bundle.base.css">
    <!-- endinject -->
    <!-- Plugin css for this page -->
    <!-- End plugin css for this page -->
    <!-- inject:css -->
    <link rel="stylesheet" href="../Content/css/vertical-layout-light/style.css">
    <!-- endinject -->
    <link rel="shortcut icon" href="../Content/images/favicon.png" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-scroller">
        <div class="container-fluid page-body-wrapper full-page-wrapper">
            <div class="content-wrapper d-flex align-items-center auth px-0">
                <div class="row w-100 mx-0">
                    <div class="col-lg-4 mx-auto">
                        <div class="auth-form-light text-left py-5 px-4 px-sm-5">
                            <div class="brand-logo">
                                <img src="../Content/images/logo.svg" alt="logo">
                            </div>
                            <h4>Hello! let's get started</h4>
                            <h6 class="font-weight-light">Sign in to continue.</h6>
                            <form action="/admin/login" method="post" class="pt-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtusername" runat="server" CssClass="form-control form-control-lg" placeholder="Username" />

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtusername" ErrorMessage="Email is required." CssClass="text-danger" Display="Dynamic" />

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtusername" ValidationExpression="^[\w\.-]+@[\w\.-]+\.\w+$"
                                        ErrorMessage="Invalid email format." CssClass="text-danger"
                                        Display="Dynamic" />
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtpass" runat="server" TextMode="Password" CssClass="form-control form-control-lg" placeholder="Password"/>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtpass" ErrorMessage="Password is required."
                                        CssClass="text-danger" Display="Dynamic" />
                                </div>
                                     <div >
      <p id="lblStatus" runat="server" style="color: red; display: none;"></p>

</div>
                                <div class="mt-3">
                                     <asp:Button ID="btnLogin" runat="server" Text="SIGN IN" OnClick="btnLogin_Click" CssClass="btn btn-block btn-primary btn-lg font-weight-medium auth-form-btn" />
                                    </div>
                                <div class="my-2 d-flex justify-content-between align-items-center">
                                    <div class="form-check">
                                        <label class="form-check-label text-muted">
                                            <input type="checkbox" class="form-check-input">
                                            Keep me signed in
                                        </label>
                                    </div>
                                    <a href="#" class="auth-link text-black">Forgot password?</a>
                                </div>
                                <div class="mb-2">
                                    <button type="button" class="btn btn-block btn-facebook auth-form-btn">
                                        <i class="ti-facebook mr-2"></i>Connect using facebook
                                    </button>
                                </div>
                                <div class="text-center mt-4 font-weight-light">
                                    Don't have an account? <a href="register.html" class="text-primary">Create</a>
                                </div>
                                
                            </form>
                        </div>
                    </div>
                </div>
            </div>
            <!-- content-wrapper ends -->
        </div>
        <!-- page-body-wrapper ends -->
    </div>
    <!-- container-scroller -->
    <!-- plugins:js -->
    <script src="../vendors/js/vendor.bundle.base.js"></script>
    <!-- endinject -->
    <!-- Plugin js for this page -->
    <!-- End plugin js for this page -->
    <!-- inject:js -->
    <script src="../Scripts/js/off-canvas.js"></script>
    <script src="../Scripts/js/hoverable-collapse.js"></script>
    <script src="../Scripts/js/template.js"></script>
    <script src="../Scripts/js/settings.js"></script>
    <script src="../Scripts/js/todolist.js"></script>






















 <%--   <div class="d-flex justify-content-center align-items-center vh-100" style="background-color: aliceblue;">
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
                    <asp:Button ID="btnLogin1" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn btn-success" />
                </div>
                <div class="col-4">
                    <asp:Button ID="Button2" runat="server" Text="Forgot Password?" OnClick="btnResetPwd_Click" CssClass="btn btn-warning" />
                </div>
                <%-- <div class="col-6">
                    <asp:Button ID="Button1" runat="server" Text="Register" OnClick="btnRegister_Click" CssClass="btn btn-primary" CausesValidation="false" />
                </div>--%>
   <%--         </div>

        </div>


        <asp:Label ID="lblStatus" runat="server" ForeColor="Red" Visible="false"></asp:Label>--%>
</asp:Content>
