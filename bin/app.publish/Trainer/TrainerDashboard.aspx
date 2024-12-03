<%@ Page Title="" Language="C#" MasterPageFile="~/Trainer/Trainer.Master" AutoEventWireup="true" CodeBehind="TrainerDashboard.aspx.cs" Inherits="JobPortalWebApplication.Trainer.TrainerDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container" style="background-color:bisque">
        <h2 style="text-align:center"> Profile Details</h2>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="lblCompanyName" runat="server" CssClass="control-label" Text=" <strong>Company Name:</strong>"></asp:Label>
                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblCompanyRegNumber" runat="server" CssClass="control-label" Text="<strong>Company Registration Number:</strong>"></asp:Label>
                    <asp:TextBox ID="txtCompanyRegNumber" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblEmail" runat="server" CssClass="control-label" Text="<strong>Email:</strong>"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblPhone" runat="server" CssClass="control-label" Text="<strong>Phone Number:</strong>"></asp:Label>
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblWebsiteURL" runat="server" CssClass="control-label" Text="<strong>Website URL:</strong>"></asp:Label>
                    <asp:TextBox ID="txtWebsiteURL" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblPhysicalAddress" runat="server" CssClass="control-label" Text="<strong>Physical Address:</strong>"></asp:Label>
                    <asp:TextBox ID="txtPhysicalAddress" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblCompanyDescription" runat="server" CssClass="control-label" Text="<strong>Company Description:</strong>"></asp:Label>
                    <asp:TextBox ID="txtCompanyDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="lblIndustryType" runat="server" CssClass="control-label" Text="<strong>Industry Type:</strong>"></asp:Label>
                    <asp:TextBox ID="txtIndustryType" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblCompanySize" runat="server" CssClass="control-label" Text="<strong>Company Size:</strong>"></asp:Label>
                    <asp:TextBox ID="txtCompanySize" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="row">
    <!-- Company Logo -->
    <div class="col-md-6">
        <asp:Label ID="blcompanyLogo" runat="server" Text="<strong>Company Logo:</strong>" CssClass="form-label"></asp:Label>
        <asp:FileUpload ID="fuCompanyLogo" runat="server" CssClass="form-control-file" />
        
        <asp:Button ID="btnSaveProfile" runat="server" Text="Upload" OnClick="btnSaveProfile_Click" CssClass="btn btn-primary mt-3" />
        
        <asp:Image ID="imgCurrentLogo" runat="server" CssClass="img-thumbnail" AlternateText="Current Company Logo" Width="250px" Height="150px" />
    </div>
 
                <div class="form-group">
                    <asp:Label ID="lblContactPersonName" runat="server" CssClass="control-label" Text="<strong>Contact Person Name:</strong>"></asp:Label>
                    <asp:TextBox ID="txtContactPersonName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblContactPersonEmail" runat="server" CssClass="control-label" Text="<strong>Contact Person Email:</strong>"></asp:Label>
                    <asp:TextBox ID="txtContactPersonEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblContactPersonPhoneNumber" runat="server" CssClass="control-label" Text="<strong>Contact Person Phone Number:</strong>"></asp:Label>
                    <asp:TextBox ID="txtContactPersonPhoneNumber" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblAgreementToTerms" runat="server" CssClass="control-label" Text="<strong>Agreement to Terms:</strong>"></asp:Label>
                    <asp:CheckBox ID="chkAgreementToTerms" runat="server" CssClass="form-check-input" Enabled="true" />
                </div>
                <div class="form-group">
                    <asp:Label ID="lblAreaofSpecialization" runat="server" CssClass="control-label" Text="<strong>Area of Specialization:</strong>"></asp:Label>
                       <asp:TextBox ID="txtAreaofSpecialization" runat="server" CssClass="form-control" ></asp:TextBox>
                    
                </div>
                <div class="form-group">
                    <asp:Label ID="lblPlanId" runat="server" CssClass="control-label" Text="<strong>Plan Type:</strong>"></asp:Label>
                    <asp:DropDownList ID="ddlPlanId" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="form-group text-center">
           <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="Button1_Click" />
            <br />
            <asp:Label ID="lblStatus" runat="server" ForeColor="Red" Visible="false"></asp:Label>
        </div>
    </div>


</asp:Content>


