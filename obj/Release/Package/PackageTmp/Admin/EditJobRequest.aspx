<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin1.Master" AutoEventWireup="true" CodeBehind="EditJobRequest.aspx.cs" Inherits="JobPortalWebApplication.Admin.EditJobRequest" %>


<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function validateDateComparison(sender, args) {
            var startDate = document.getElementById('<%= txtApplicationStartDate.ClientID %>').value;
            var endDate = document.getElementById('<%= txtApplicationDeadline.ClientID %>').value;

            // Parse the dates
            var start = new Date(startDate);
            var end = new Date(endDate);

            // Compare the dates
            if (start && end && end >= start) {
                args.IsValid = true;
            } else {
                args.IsValid = false;
            }
        }
        function ValidateStartDate(sender, args) {
            var startDate = new Date(document.getElementById('<%= txtApplicationStartDate.ClientID %>').value);
            var today = new Date();

            // Compare the dates (set hours to 0 to ignore the time part)
            today.setHours(0, 0, 0, 0);
            if (startDate > today) {
                args.IsValid = true;
            } else {
                args.IsValid = false;
            }
        }
    </script>
    <div class="row">
        <div class="col-lg-12 grid-margin stretch-card">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">EditJobs </h4>
                    <p class="card-description">
                        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-2" />
                    </p>
                    <div class="row mt-1">
                        <div class="col-md-6">
                            <asp:Label ID="lblJobTitle" runat="server" Text="Job Title:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtJobTitle" runat="server" CssClass="form-control mt-2" />
                            <asp:RequiredFieldValidator ID="rfvJobTitle" runat="server" ControlToValidate="txtJobTitle"
                                ErrorMessage="Job Title is required." CssClass="text-danger" />
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label1" runat="server" Text="Company Name:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control mt-2" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCompanyName"
                                ErrorMessage="Company Name is required." CssClass="text-danger" />
                        </div>
                    </div>
                    <div class="row mt-2">

                        <div class="col-md-6">
                            <asp:Label ID="lblJobDescription" runat="server" Text="Job Description:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtJobDescription" runat="server" CssClass="form-control mt-2" TextMode="MultiLine" Rows="4" />
                            <asp:RequiredFieldValidator ID="rfvJobDescription" runat="server" ControlToValidate="txtJobDescription"
                                ErrorMessage="Job Description is required." CssClass="text-danger" />
                        </div>


                        <div class="col-md-6">
                            <asp:Label ID="lblVacancy" runat="server" Text="No. Of Vacancy:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtVacancy" runat="server" CssClass="form-control mt-2" />
                            <asp:RequiredFieldValidator ID="rfvVacancy" runat="server" ControlToValidate="txtVacancy"
                                ErrorMessage="Number of Vacancies is required." CssClass="text-danger" />
                            <asp:RangeValidator ID="rvVacancy" runat="server" ControlToValidate="txtVacancy"
                                ErrorMessage="Please enter a valid number between 1 and 1000." MinimumValue="1" MaximumValue="1000"
                                Type="Integer" CssClass="text-danger" />
                        </div>

                    </div>
                    <div class="row mt-2">
                        <div class="col-md-6">
                            <asp:Label ID="lblQualifications" runat="server" Text="Qualifications:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtQualifications" runat="server" CssClass="form-control mt-2" />
                            <asp:RequiredFieldValidator ID="rfvQualifications" runat="server" ControlToValidate="txtQualifications"
                                ErrorMessage="Qualifications are required." CssClass="text-danger" />
                        </div>




                        <div class="col-md-6">
                            <asp:Label ID="lblExperience" runat="server" Text="Experience:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtExperience" runat="server" CssClass="form-control mt-2"  />
                            <asp:RequiredFieldValidator ID="rfvExperience" runat="server" ControlToValidate="txtExperience"
                                ErrorMessage="Experience details are required." CssClass="text-danger" />
                        </div>
                    </div>
                    <div class="row mt-2">
                        <div class="col-md-6">
                            <asp:Label ID="lblRequiredSkills" runat="server" Text="Required Skills:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtRequiredSkills" runat="server" CssClass="form-control mt-2" />
                            <asp:RequiredFieldValidator ID="rfvRequiredSkills" runat="server" ControlToValidate="txtRequiredSkills"
                                ErrorMessage="Required Skills are required." CssClass="text-danger" />
                        </div>




                        <div class="col-md-6">
                            <asp:Label ID="lblJobLocationId" runat="server" Text="Job Location:" CssClass="form-label"></asp:Label>
                            <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="form-control mt-2"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvJobLocation" runat="server" ControlToValidate="ddlJobLocation"
                                InitialValue="0" ErrorMessage="Job Location is required." CssClass="text-danger" />
                        </div>
                    </div>
                    <div class="row mt-2">
                        <div class="col-md-6 mb-3">
                            <asp:Label ID="lblJobTypeId" runat="server" Text="Job Type:" CssClass="form-label"></asp:Label>
                            <asp:DropDownList ID="ddlJobTypes" runat="server" CssClass="form-control mt-2"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvJobType" runat="server" ControlToValidate="ddlJobTypes"
                                InitialValue="0" ErrorMessage="Job Type is required." CssClass="text-danger" />
                        </div>



                        <div class="col-md-6">
                            <asp:Label ID="lblSalary" runat="server" Text="Annual Salary(CTC):" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control mt-2" />
                            <asp:RequiredFieldValidator ID="rfvSalary" runat="server" ControlToValidate="txtSalary"
                                ErrorMessage="Annual Salary is required." CssClass="text-danger" />
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-6">

                            <asp:Label ID="lblApplicationStartDate" runat="server" Text="Application Start Date:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtApplicationStartDate" runat="server" CssClass="form-control mt-2" TextMode="Date" />
                            <asp:RequiredFieldValidator ID="rfvApplicationStartDate" runat="server" ControlToValidate="txtApplicationStartDate"
                                ErrorMessage="Application Start Date is required." CssClass="text-danger" />
                            <asp:CustomValidator ID="cvStartDateValidation" runat="server"
                                ControlToValidate="txtApplicationStartDate"
                                ErrorMessage="Application Start Date must be greater than today's date"
                                CssClass="text-danger"
                                Display="Dynamic"
                                OnServerValidate="cvStartDateValidation_ServerValidate"
                                ClientValidationFunction="ValidateStartDate" />
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblApplicationDeadline" runat="server" Text="Application Deadline:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtApplicationDeadline" runat="server" CssClass="form-control mt-2" TextMode="Date" />
                            <asp:RequiredFieldValidator ID="rfvApplicationDeadline" runat="server" ControlToValidate="txtApplicationDeadline"
                                ErrorMessage="Application Deadline is required." CssClass="text-danger" />
                        </div>
                    </div>
                    <asp:CustomValidator ID="cvDateComparison" runat="server"
                        ControlToValidate="txtApplicationDeadline"
                        ErrorMessage="Application Deadline must be greater than Application Start Date"
                        CssClass="text-danger"
                        Display="Dynamic"
                        OnServerValidate="cvDateComparison_ServerValidate"
                        ClientValidationFunction="validateDateComparison"
                        EnableClientScript="true" />

                    <div class="form-group text-center">
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" OnClick="btnEdit_Click" CausesValidation="true" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" CausesValidation="false" />

                    </div>

                    <div class="form-group text-center">
                        <asp:Label ID="lblStatusMessage" runat="server" CssClass="text-info"></asp:Label>
                    </div>
                </div>

            </div>
</asp:Content>
