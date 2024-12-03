<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployerViewDashboard.aspx.cs" MasterPageFile="~/Admin/Admin1.Master" Inherits="JobPortalWebApplication.Admin.EmployerViewDashboard" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <style>
    
</style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    
     <div style="text-align: left">
     <a href="EmployeeList.aspx">Back</a>
 </div>
            <div class="card">
                     <h3 class="text-center mt-3"><strong>Employee Details</strong></h3>          

                                <div class="row">
                                    <div class="col-md-11" style="position: relative;">
                                        <asp:Image ID="imgCompanyLogo" runat="server" Width="200px" Height="90px"
                                            ImageUrl='<%# Eval("CompanyLogoURL") %>'
                                            Style="position: absolute; top: 3 auto; right: 0;" />
                                        <asp:Label ID="lal" Text="Logo"></asp:Label>
                                    </div>
                                </div>
              <br />
                             <%--   <div class="row mt-5">
                                      <div class="row ms-4">
                                    <div class="col-md-4">
                                        <strong>CompanyID:</strong>
                                    </div>
                                            </div>
                                    <div class="col-md-8">
                                      <asp:Label ID="lblCompanyID" runat="server" />
                                    </div>
                                </div>--%>
                                   
                <div class="row mt-5">
                                   <div class="row ms-4">
                                    <div class="col-md-4">
                                        <strong>Company Name:</strong>
                                    </div>

                                    <div class="col-md-4">
                                      <asp:Label ID="lblCompanyName" runat="server" />
                                    </div>
                             </div>
                                       </div>
                                           <hr />
                                       <div class="row ms-4">
                                        <div class="col-md-4">
                                            <strong>Registration Number:</strong>
                                        </div>
                                        <div class="col-md-8">
                                          <asp:Label ID="lblCompanyRegNumberValue" runat="server" />
                                        </div>
                                    </div>
                  <hr />
                                     <div class="row ms-4">
                                        <div class="col-md-4">
                                            <strong>Email:</strong>
                                        </div>
                                        <div class="col-md-8">
                                           <asp:Label ID="lblEmailValue" runat="server" />
                                        </div>
                                    </div>
                  <hr />
                                     <div class="row ms-4">
                                        <div class="col-md-4">
                                            <strong>Phone Number:</strong>
                                        </div>
                                        <div class="col-md-8">
                                         <asp:Label ID="lblPhoneValue" runat="server" />
                                        </div>
                                    </div>
                  <hr />
                                      <div class="row ms-4">
                                        <div class="col-md-4">
                                            <strong>Website URL:</strong>
                                        </div>
                                        <div class="col-md-8">
                                          <asp:Label ID="lblWebsiteURLValue" runat="server" />
                                        </div>
                                    </div>  <hr />
                                     <div class="row ms-4">
                                        <div class="col-md-4">
                                            <strong>Physical Address:</strong>
                                        </div>
                                        <div class="col-md-8">
                                          <asp:Label ID="lblPhysicalAddressValue" runat="server" />
                                        </div>
                                    </div>  <hr />
                                      <div class="row ms-4">
                                        <div class="col-md-4">
                                            <strong>Company Description:</strong>
                                        </div>
                                        <div class="col-md-8">
                                           <asp:Label ID="lblCompanyDescriptionValue" runat="server" />
                                        </div>
                                    </div>  <hr />
                                      <div class="row ms-4">
                                        <div class="col-md-4">
                                            <strong>Industry Type:</strong>
                                        </div>
                                        <div class="col-md-8">
                                          <asp:Label ID="lblIndustryTypeValue" runat="server" />
                                        </div>
                                    </div>  <hr />
                                     <div class="row ms-4">
                                        <div class="col-md-4">
                                            <strong>Company Size:</strong>
                                        </div>
                                        <div class="col-md-8">
                                           <asp:Label ID="lblCompanySizeValue" runat="server" />
                                        </div>
                                    </div>  <hr />
                                      <div class="row ms-4">
                                        <div class="col-md-4">
                                            <strong>Contact Person Name:</strong>
                                        </div>
                                        <div class="col-md-8">
                                           <asp:Label ID="lblContactPersonNameValue" runat="server" />
                                        </div>
                                    </div>  <hr />
                                     <div class="row ms-4">
                                        <div class="col-md-4">
                                            <strong>Contact Person Email:</strong>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:Label ID="lblContactPersonEmailValue" runat="server" />
                                        </div>
                                    </div>  <hr />
                                       <div class="row ms-4">
                                        <div class="col-md-4">
                                            <strong>Contact Person Phone Number:</strong>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:Label ID="lblContactPersonPhoneNumberValue" runat="server" />
                                        </div>
                                    </div>  <hr />
                                    <%--<div class="row">
                    <div class="col-md-4">
                        <strong>Company Logo:</strong>
                    </div>
                    <div class="col-md-8">
                        <asp:Image ID="imgCompanyLogo" runat="server" Width="200px" Height="150px" ImageUrl='<%# Eval("CompanyLogoURL") %>' />
                    </div>
                </div>--%>
                                    <div class="row ms-4">
                                        <div class="col-md-4">
                                            <strong>Agreement to Terms:</strong>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:Label ID="lblAgreementToTermsValue" runat="server" />
                                        </div>
                                    </div>  <hr />
                                     <div class="row ms-4">
                                        <div class="col-md-4">
                                            <strong>Training and Placement Program:</strong>
                                        </div>
                                        <div class="col-md-8">
                                           <asp:Label ID="lblTrainingAndPlacementProgramValue" runat="server" />
                                        </div>
                                    </div>  <hr />
                                      <div class="row ms-4">
                                        <div class="col-md-4">
                                            <strong>Plan ID:</strong>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:Label ID="lblPlanIdValue" runat="server" />
                                        </div>
                                    </div>  <hr />
                                     <div class="row ms-4">
                                        <div class="col-md-4">
                                            <%-- <strong>Actions:</strong>--%>
                                        </div>
                                        <%-- <div class="col-md-8">
                        <asp:Button ID="btnEditProfile" runat="server" Text="Edit Profile" CssClass="btn btn-primary" CommandArgument='<%# Eval("EmployeeId") %>' OnClick="btnEditProfile_Click" />
                    </div>--%>
                                    </div>
                            

               <br />
            </div>
         
</asp:Content>
