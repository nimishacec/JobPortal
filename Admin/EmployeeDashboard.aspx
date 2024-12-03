<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeDashboard.aspx.cs" MasterPageFile="~/Admin/Admin1.Master" Inherits="JobPortalWebApplication.Admin.EmployeeDashboard" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: left">
    <asp:Button ID="Button2" runat="server" Text="Back" OnClientClick="window.history.back(); return false;" CssClass="btn btn-link" />
</div>
    <div class="col-12 grid-margin">
        <div class="card">
            <div class="card-body">
                <h4 class="card-title">Horizontal Two column</h4>
               

                    <p class="card-description">
                        Personal info
                    </p>
                    <asp:ValidationSummary ID="vsSummary" runat="server" ForeColor="Red" HeaderText="Please correct the following errors:" />

                    <div class="row">
                        <!-- Company Name -->
                        <div class="col-md-6">
                            <asp:Label ID="lblCompanyName" runat="server" Text="Company Name:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control mt-2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtCompanyName"
                                ErrorMessage="Company Name is required." CssClass="text-danger" />
                        </div>

                        <!-- Company Registration Number -->
                        <div class="col-md-6">
                            <asp:Label ID="lblCompanyRegNumber" runat="server" Text="Registration Number:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtCompanyRegNumber" runat="server" CssClass="form-control mt-2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCompanyRegNumber" runat="server" ControlToValidate="txtCompanyRegNumber"
                                ErrorMessage="Company Registration Number is required." CssClass="text-danger" />
                        </div>
                    </div>

                    <div class="row">
                        <!-- Email -->
                        <div class="col-md-6">
                            <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mt-2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="Email is required." CssClass="text-danger" />
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="Invalid email format." CssClass="text-danger"
                                ValidationExpression="\w+@\w+\.\w+" />
                        </div>

                        <!-- Phone Number -->
                        <div class="col-md-6">
                            <asp:Label ID="lblPhone" runat="server" Text="Phone Number:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control mt-2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone"
                                ErrorMessage="Phone Number is required." CssClass="text-danger" />
                        </div>
                    </div>

                    <div class="row">
                        <!-- Website URL -->
                        <div class="col-md-6">
                            <asp:Label ID="lblWebsiteURL" runat="server" Text="Website URL:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtWebsiteURL" runat="server" CssClass="form-control mt-2"></asp:TextBox>
                        </div>

                        <!-- Physical Address -->
                        <div class="col-md-6">
                            <asp:Label ID="lblPhysicalAddress" runat="server" Text="Physical Address: " CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtPhysicalAddress" runat="server" CssClass="form-control mt-2"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mt-3">
                        <!-- Company Description -->
                        <div class="col-md-12">
                            <asp:Label ID="lblCompanyDescription" runat="server" Text="Company Description:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtCompanyDescription" runat="server" CssClass="form-control mt-2" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mt-3">
                        <!-- Industry Type -->
                        <div class="col-md-6">
                            <asp:Label ID="lblIndustryType" runat="server" Text=" Industry Type:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtIndustryType" runat="server" CssClass="form-control mt-2"></asp:TextBox>
                        </div>

                        <!-- Company Size -->
                        <div class="col-md-6">
                            <asp:Label ID="lblCompanySize" runat="server" Text="Company Size:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtCompanySize" runat="server" CssClass="form-control mt-2"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mt-3">
                        <div class="form-group">
                            <label>Logo</label>
                            <input type="file" name="img[]" class="file-upload-default">
                            <div class="input-group col-xs-12">
                                <input type="text" class="form-control file-upload-info" disabled placeholder="Upload Image">
                                <span class="input-group-append">
                                   <asp:Button ID="btnSaveProfile" runat="server" Text="Upload" OnClick="btnSaveProfile_Click" CssClass="btn btn-primary mr-3" />
                                </span>
                            </div>
                            <br />

                            <asp:FileUpload ID="fuCompanyLogo" runat="server" CssClass="form-control-file" /><br />
                            <asp:Label ID="blcompanyLogo" runat="server" Text="Company Logo:" CssClass="form-label"></asp:Label>
                            <asp:Image ID="imgCurrentLogo" runat="server" CssClass="img-thumbnail" AlternateText="Current Company Logo" Width="250px" Height="150px" />

                        </div>
                    </div>
                    <!-- Company Logo -->



                    <div class="row mt-3">
                        <!-- Contact Person Name -->
                        <div class="col-md-6">
                            <asp:Label ID="lblContactPersonName" runat="server" Text="Contact Person Name:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtContactPersonName" runat="server" CssClass="form-control mt-2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvContactPersonName" runat="server" ControlToValidate="txtContactPersonName"
                                ErrorMessage="Contact Person Name is required." CssClass="text-danger" />
                        </div>

                        <!-- Contact Person Email -->
                        <div class="col-md-6">
                            <asp:Label ID="lblContactPersonEmail" runat="server" Text="Contact Person Email:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtContactPersonEmail" runat="server" CssClass="form-control mt-2"></asp:TextBox>
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
                            <asp:Label ID="lblContactPersonPhoneNumber" runat="server" Text="Contact Person Phone Number: " CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtContactPersonPhoneNumber" runat="server" CssClass="form-control mt-2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvContactPersonPhoneNumber" runat="server" ControlToValidate="txtContactPersonPhoneNumber"
                                ErrorMessage="Contact Person Phone Number is required." CssClass="text-danger" />
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblPlanId" runat="server" Text="Plan Type:" CssClass="form-label"></asp:Label>
                            <asp:DropDownList ID="ddlPlanId" runat="server" CssClass="form-control mt-2"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvPlanId" runat="server" ControlToValidate="ddlPlanId"
                                InitialValue="0" ErrorMessage="Please select a plan type." CssClass="text-danger" />
                        </div>
                    </div>

                    <div class="row">
                        <!-- Agreement to Terms -->
                        <div class="col-md-2">
                            <asp:Label ID="Label1" runat="server" Text="Agreement to Terms:" CssClass="form-label"></asp:Label>
                            <asp:CheckBox ID="chkAgreementTerms" runat="server" CssClass="form-check-input ms-3" />

                            <%-- <asp:CheckBox ID="chkAgreementToTerms" runat="server" CssClass="form-check-input ms-3"/>--%>
                        </div>


                        <!-- Validation Script -->
                        
                        <!-- Training and Placement Program -->
                        <div class="col-md-6">
                            <asp:Label ID="Label2" runat="server" Text="Training Program:" CssClass="form-label"></asp:Label>
                            <asp:CheckBox ID="chkPlacement" runat="server" CssClass="form-check-input ms-3" />
                        </div>
                    </div>

                    <div class="row mt-3">
                        <!-- Plan Type -->

                    </div>

                   <div class="form-group mt-3 d-flex justify-content-center">
                         <asp:HiddenField ID="hiddenFieldCandidateId" runat="server" />
                        <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSave_Click">
         <i class="mdi mdi-content-save-all"></i>Save Profile
 </asp:LinkButton>&nbsp;
                         <%--   <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btn btn-primary mr-2" OnClick="Button1_Click" />--%>
                              <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-secondary" OnClick="btnCancel_Click" CausesValidation="false" />
                             <br />

                            <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
                        </div>

                    </div>
                    
            </div>
           
        </div>
</asp:Content>
