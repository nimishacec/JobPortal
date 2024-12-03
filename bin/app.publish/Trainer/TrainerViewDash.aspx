<%@ Page Title="" Language="C#" MasterPageFile="~/Trainer/Trainer.Master" AutoEventWireup="true" CodeBehind="TrainerViewDash.aspx.cs" Inherits="JobPortalWebApplication.Trainer.TrainerViewDash" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .gridview-background {
            background-color: aliceblue; /* Replace with your desired color code */
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div style="background-color: aliceblue">
        <h2 style="text-align: center">Trainer Details</h2>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">

            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-md-11 mt-1" style="position: relative;">
                                <asp:Image ID="Image2" runat="server" Width="200px" Height="90px"
                                    ImageUrl='<%# Eval("CompanyLogoURL") %>'
                                    Style="position: absolute; top: 0; right: 0;" />
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-md-4">
                                <strong>Company Name:</strong>
                            </div>
                            <div class="col-md-8">
                                <%# Eval("CompanyName") %>
                            </div>
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
                        <div class="row">
                            <div class="col-md-4">
                                <strong>Company Logo:</strong>
                            </div>
                            <div class="col-md-8">
                                <asp:Image ID="imgCompanyLogo" runat="server" Width="50px" Height="50px" ImageUrl='<%# Eval("CompanyLogoURL") %>' />
                            </div>
                        </div>
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
                                <strong>Area of Specialization:</strong>
                            </div>
                            <div class="col-md-8">
                                <%# Eval("AreaofSpecialization") %>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <strong>Plan ID:</strong>
                            </div>
                            <div class="col-md-8">
                                <%# Eval("PlanType") %>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <strong>Actions:</strong>
                            </div>
                            <div class="col-md-8">
                                <asp:Button ID="btnEditProfile" runat="server" Text="Edit Profile" CssClass="btn btn-primary" CommandArgument='<%# Eval("TrainerId") %>' OnClick="btnEditProfile_Click" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
</asp:Content>
