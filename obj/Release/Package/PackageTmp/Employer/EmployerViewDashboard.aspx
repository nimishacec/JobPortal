<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployerViewDashboard.aspx.cs" MasterPageFile="~/Employer/Employer.Master" Inherits="JobPortalWebApplication.Employer.EmployerViewDashboard" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../vendors/feather/feather.css" />
    <link rel="stylesheet" href="../vendors/ti-icons/css/themify-icons.css" />
    <link rel="stylesheet" href="../vendors/css/vendor.bundle.base.css" />
    <link rel="stylesheet" href="../../vendors/mdi/css/materialdesignicons.min.css">
    <!-- endinject -->
    <!-- Plugin css for this page -->
    <link rel="stylesheet" href="../vendors/datatables.net-bs4/dataTables.bootstrap4.css" />
    <link rel="stylesheet" href="../vendors/ti-icons/css/themify-icons.css" />
    <link rel="stylesheet" type="text/css" href="../Script/js/select.dataTables.min.css" />
    <!-- End plugin css for this page -->
    <!-- inject:css -->
    <link rel="stylesheet" href="../Content/css/vertical-layout-light/style.css" />
    <!-- endinject -->
    <link rel="shortcut icon" href="../Content/images/favicon.png" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="main-panel">
        <div class="content-wrapper">
            <div class="card">
             <%--  <h2 style="text-align:center">Employee Details</h2>--%>

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderWidth="0">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                               
                                <div class="row mt-5">
                                    <div class="col-md-11 mt-1" style="position: relative;">
                                        <asp:Image ID="Image2" runat="server" Width="200px" Height="100px"
                                            ImageUrl='<%# Eval("CompanyLogoURL") %>'
                                            Style="position: absolute; top: 0; right: 0;" />
                                    </div>
                                </div>
                                <div class="row ms-4">
                                    <div class="col-md-4">
                                        <strong>CompanyID:</strong>
                                    </div>
                                    <div class="col-md-8">
                                        <%# Eval("EmployeeId") %>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <strong>Company Name:</strong>
                                    </div>

                                    <div class="col-md-4">
                                        <%# Eval("CompanyName") %>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-4">
                                            <strong>Company Registration Number:</strong>
                                        </div>
                                        <div class="col-md-8">
                                            <%# Eval("CompanyRegistrationNumber") %>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <strong>Email:</strong>
                                        </div>
                                        <div class="col-md-8">
                                            <%# Eval("CompanyEmail") %>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <strong>Phone Number:</strong>
                                        </div>
                                        <div class="col-md-8">
                                            <%# Eval("CompanyPhoneNumber") %>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <strong>Website URL:</strong>
                                        </div>
                                        <div class="col-md-8">
                                            <%# Eval("CompanyWebsiteUrl") %>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <strong>Physical Address:</strong>
                                        </div>
                                        <div class="col-md-8">
                                            <%# Eval("PhysicalAddress") %>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <strong>Company Description:</strong>
                                        </div>
                                        <div class="col-md-8">
                                            <%# Eval("CompanyDescription") %>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <strong>Industry Type:</strong>
                                        </div>
                                        <div class="col-md-8">
                                            <%# Eval("IndustryType") %>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <strong>Company Size:</strong>
                                        </div>
                                        <div class="col-md-8">
                                            <%# Eval("CompanySize") %>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <strong>Contact Person Name:</strong>
                                        </div>
                                        <div class="col-md-8">
                                            <%# Eval("ContactPersonName") %>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <strong>Contact Person Email:</strong>
                                        </div>
                                        <div class="col-md-8">
                                            <%# Eval("ContactPersonEmail") %>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <strong>Contact Person Phone Number:</strong>
                                        </div>
                                        <div class="col-md-8">
                                            <%# Eval("ContactPersonPhoneNumber") %>
                                        </div>
                                    </div>
                                    <%--<div class="row">
                    <div class="col-md-4">
                        <strong>Company Logo:</strong>
                    </div>
                    <div class="col-md-8">
                        <asp:Image ID="imgCompanyLogo" runat="server" Width="200px" Height="150px" ImageUrl='<%# Eval("CompanyLogoURL") %>' />
                    </div>
                </div>--%>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <strong>Agreement to Terms:</strong>
                                        </div>
                                        <div class="col-md-8">
                                            <%# Eval("AgreementToTerms") %>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <strong>Training and Placement Program:</strong>
                                        </div>
                                        <div class="col-md-8">
                                            <%# Eval("TrainingAndPlacementProgram") %>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <strong>Plan ID:</strong>
                                        </div>
                                        <div class="col-md-8">
                                            <%# Eval("PlanId") %>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <%-- <strong>Actions:</strong>--%>
                                        </div>
                                        <%-- <div class="col-md-8">
                        <asp:Button ID="btnEditProfile" runat="server" Text="Edit Profile" CssClass="btn btn-primary" CommandArgument='<%# Eval("EmployeeId") %>' OnClick="btnEditProfile_Click" />
                    </div>--%>
                                    </div>
                                     </div>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>


            </div>


        </div>
    </div>


</asp:Content>
