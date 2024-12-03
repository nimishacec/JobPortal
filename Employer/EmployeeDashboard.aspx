<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeDashboard.aspx.cs" MasterPageFile="~/Employer/Employer.Master" Inherits="JobPortalWebApplication.Employer.EmployeeDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
     
       <div class="container mt-5" style="background-color:aliceblue">
    <h3 style="text-align:center"> </h3>
    <asp:ValidationSummary ID="vsSummary" runat="server" ForeColor="Red" HeaderText="Please correct the following errors:" />

    <div class="row">
        <!-- Company Name -->
        <div class="col-md-6">
            <asp:Label ID="lblCompanyName" runat="server" Text="<strong>Company Name:</strong>" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtCompanyName" 
                ErrorMessage="Company Name is required." CssClass="text-danger" />
        </div>
        
        <!-- Company Registration Number -->
        <div class="col-md-6">
            <asp:Label ID="lblCompanyRegNumber" runat="server" Text="<strong>Company Registration Number: </strong>" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtCompanyRegNumber" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCompanyRegNumber" runat="server" ControlToValidate="txtCompanyRegNumber" 
                ErrorMessage="Company Registration Number is required." CssClass="text-danger" />
        </div>
    </div>

    <div class="row">
        <!-- Email -->
        <div class="col-md-6">
            <asp:Label ID="lblEmail" runat="server" Text=" <strong>Email: </strong>" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" 
                ErrorMessage="Email is required." CssClass="text-danger" />
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="Invalid email format." CssClass="text-danger" 
                ValidationExpression="\w+@\w+\.\w+" />
        </div>
        
        <!-- Phone Number -->
        <div class="col-md-6">
            <asp:Label ID="lblPhone" runat="server" Text=" <strong>Phone Number: </strong>" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone" 
                ErrorMessage="Phone Number is required." CssClass="text-danger" />
        </div>
    </div>

    <div class="row">
        <!-- Website URL -->
        <div class="col-md-6">
            <asp:Label ID="lblWebsiteURL" runat="server" Text=" <strong>Website URL: </strong>" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtWebsiteURL" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        
        <!-- Physical Address -->
        <div class="col-md-6">
            <asp:Label ID="lblPhysicalAddress" runat="server" Text=" <strong>Physical Address: </strong>" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtPhysicalAddress" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
    </div>

    <div class="row">
        <!-- Company Description -->
        <div class="col-md-12">
            <asp:Label ID="lblCompanyDescription" runat="server" Text="<strong>Company Description:</strong>" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtCompanyDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
        </div>
    </div>

    <div class="row">
        <!-- Industry Type -->
        <div class="col-md-6">
            <asp:Label ID="lblIndustryType" runat="server" Text=" <strong> Industry Type:</strong>" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtIndustryType" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        
        <!-- Company Size -->
        <div class="col-md-6">
            <asp:Label ID="lblCompanySize" runat="server" Text="<strong>Company Size:</strong>" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtCompanySize" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
    </div>

    <div class="row">
        <!-- Company Logo -->
        <div class="col-md-6">
            <asp:Label ID="blcompanyLogo" runat="server" Text="<strong>Company Logo:</strong>" CssClass="form-label"></asp:Label>
            <asp:FileUpload ID="fuCompanyLogo" runat="server" CssClass="form-control-file" />
            <br />
            <asp:Button ID="btnSaveProfile" runat="server" Text="Upload" OnClick="btnSaveProfile_Click" CssClass="btn btn-primary mt-3" />
            
            <asp:Image ID="imgCurrentLogo" runat="server" CssClass="img-thumbnail" AlternateText="Current Company Logo" Width="250px" Height="150px" />
        </div>
    </div>

    <div class="row">
        <!-- Contact Person Name -->
        <div class="col-md-6">
            <asp:Label ID="lblContactPersonName" runat="server" Text="<strong>Contact Person Name:</strong>" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtContactPersonName" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvContactPersonName" runat="server" ControlToValidate="txtContactPersonName" 
                ErrorMessage="Contact Person Name is required." CssClass="text-danger" />
        </div>
        
        <!-- Contact Person Email -->
        <div class="col-md-6">
            <asp:Label ID="lblContactPersonEmail" runat="server" Text="<strong> Contact Person Email:</strong>" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtContactPersonEmail" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvContactPersonEmail" runat="server" ControlToValidate="txtContactPersonEmail" 
                ErrorMessage="Contact Person Email is required." CssClass="text-danger" />
            <asp:RegularExpressionValidator ID="revContactPersonEmail" runat="server" ControlToValidate="txtContactPersonEmail"
                ErrorMessage="Invalid email format." CssClass="text-danger" 
                ValidationExpression="\w+@\w+\.\w+" />
        </div>
    </div>

    <div class="row">
        <!-- Contact Person Phone Number -->
        <div class="col-md-6">
            <asp:Label ID="lblContactPersonPhoneNumber" runat="server" Text="<strong>Contact Person Phone Number: </strong>" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtContactPersonPhoneNumber" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvContactPersonPhoneNumber" runat="server" ControlToValidate="txtContactPersonPhoneNumber" 
                ErrorMessage="Contact Person Phone Number is required." CssClass="text-danger" />
        </div>
    </div>

    <div class="row">
        <!-- Agreement to Terms -->
        <div class="col-md-6">
            <asp:Label ID="lblAgreementToTerms" runat="server" Text="<strong>Agreement to Terms:</strong>" CssClass="form-label"></asp:Label>
            <asp:CheckBox ID="chkAgreementToTerms" runat="server" CssClass="form-check-input" />
            
        </div>
       

<!-- Validation Script -->
<script runat="server">
    protected void cvAgreementToTerms_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = chkAgreementToTerms.Checked;
    }
</script>
        <!-- Training and Placement Program -->
        <div class="col-md-6">
            <asp:Label ID="lblTrainingAndPlacementProgram" runat="server" Text="<strong>Training and Placement Program:</strong>" CssClass="form-label"></asp:Label>
            <asp:CheckBox ID="chkTrainingProgram" runat="server" CssClass="form-check-input" />
        </div>
    </div>

    <div class="row">
        <!-- Plan Type -->
        <div class="col-md-6">
            <asp:Label ID="lblPlanId" runat="server" Text="<strong>Plan Type:</strong>" CssClass="form-label"></asp:Label>
            <asp:DropDownList ID="ddlPlanId" runat="server" CssClass="form-control"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvPlanId" runat="server" ControlToValidate="ddlPlanId" 
                InitialValue="0" ErrorMessage="Please select a plan type." CssClass="text-danger" />
        </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="Button1_Click" /><br />
            <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </div>
</div>
                 <div style="text-align:right">   <asp:Button ID="Button2" runat="server" Text="Back" OnClientClick="window.history.back(); return false;"  CssClass="btn btn-link"/>
</div>
    
</asp:Content>
