<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobPost.aspx.cs" MasterPageFile="~/Employer/Employer.Master" Inherits="JobPortalWebApplication.Employer.JobPost" EnableEventValidation="false" %>

<asp:Content ID="content2" runat="server" ContentPlaceHolderID="head">
    <style>
        .bold-label {
            font-weight: bold;
        }
    </style>
    

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">
        function validateDateComparison(sender, args) {
            var startDate = document.getElementById('<%= txtApplicationStartDate.ClientID %>').value;
    var endDate = document.getElementById('<%= txtApplicationDeadline.ClientID %>').value;

            // Parse the dates
            var start = new Date(startDate);
            var end = new Date(endDate);

            // Compare the dates
            if (start && end && end > start) {
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
    <div style="background-color: aliceblue">
        <h3 style="text-align: center">Post New Jobs</h3>
        <div class="container mt-5">
            <div class="row">
                <div class="row mb-3">
                    <div class="col-md-2">
                        <asp:Label ID="lblJobTitle" runat="server" Text="Job Title:" CssClass="bold-label"></asp:Label>
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtJobTitle" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvJobTitle" runat="server" ControlToValidate="txtJobTitle"
                            ErrorMessage="Job Title is required" CssClass="text-danger" Display="Dynamic" />
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-2">
                        <asp:Label ID="lblVacancy" runat="server" Text="No. Of Vacancy:" CssClass="bold-label"></asp:Label>
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtVacancy" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvVacancy" runat="server" ControlToValidate="txtVacancy"
                            ErrorMessage="No. of Vacancy is required" CssClass="text-danger" Display="Dynamic" />
                        <asp:RangeValidator ID="rvVacancy" runat="server" ControlToValidate="txtVacancy"
                            MinimumValue="1" MaximumValue="1000" Type="Integer"
                            ErrorMessage="Vacancy must be between 1 and 1000" CssClass="text-danger" Display="Dynamic" />
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-2">
                        <asp:Label ID="lblJobDescription" runat="server" Text="Job Description:" CssClass="bold-label"></asp:Label>
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtJobDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                        <asp:RequiredFieldValidator ID="rfvJobDescription" runat="server" ControlToValidate="txtJobDescription"
                            ErrorMessage="Job Description is required" CssClass="text-danger" Display="Dynamic" />
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-2">
                        <asp:Label ID="lblQualifications" runat="server" Text="Qualifications:" CssClass="bold-label"></asp:Label>
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtQualifications" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvQualifications" runat="server" ControlToValidate="txtQualifications"
                            ErrorMessage="Qualifications are required" CssClass="text-danger" Display="Dynamic" />
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-2">
                        <asp:Label ID="lblExperience" runat="server" Text="Experience:" CssClass="bold-label"></asp:Label>
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtExperience" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvExperience" runat="server" ControlToValidate="txtExperience"
                            ErrorMessage="Experience is required" CssClass="text-danger" Display="Dynamic" />
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-2">
                        <asp:Label ID="lblRequiredSkills" runat="server" Text="Required Skills:" CssClass="bold-label"></asp:Label>
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtRequiredSkills" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvRequiredSkills" runat="server" ControlToValidate="txtRequiredSkills"
                            ErrorMessage="Required Skills are required" CssClass="text-danger" Display="Dynamic" />
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-2">
                        <asp:Label ID="lblJobLocationId" runat="server" Text="Job Location:" CssClass="bold-label"></asp:Label>
                    </div>
                    <div class="col-md-8">
                        <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvJobLocation" runat="server" ControlToValidate="ddlJobLocation"
                            InitialValue="" ErrorMessage="Job Location is required" CssClass="text-danger" Display="Dynamic" />
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-2">
                        <asp:Label ID="lblJobTypeId" runat="server" Text="Job Type:" CssClass="bold-label"></asp:Label>
                    </div>
                    <div class="col-md-8">
                        <asp:DropDownList ID="ddlJobTypes" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvJobType" runat="server" ControlToValidate="ddlJobTypes"
                            InitialValue="" ErrorMessage="Job Type is required" CssClass="text-danger" Display="Dynamic" />
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-2">
                        <asp:Label ID="lblSalary" runat="server" Text="Annual Salary (CTC):" CssClass="bold-label"></asp:Label>
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvSalary" runat="server" ControlToValidate="txtSalary"
                            ErrorMessage="Salary is required" CssClass="text-danger" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revSalary" runat="server" ControlToValidate="txtSalary"
                            ValidationExpression="^\d+(\.\d{1,2})?$" ErrorMessage="Please enter a valid salary amount"
                            CssClass="text-danger" Display="Dynamic" />
                    </div>
                </div>

               
                                     
                <div class="row mb-3">
                    <div class="col-md-2">
                        <asp:Label ID="lblApplicationStartDate" runat="server" Text="Application Start Date:" CssClass="bold-label"></asp:Label>
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtApplicationStartDate" runat="server" CssClass="form-control" TextMode="Date" />
                        <asp:RequiredFieldValidator ID="rfvApplicationStartDate" runat="server" ControlToValidate="txtApplicationStartDate"
                            ErrorMessage="Application Start Date is required" CssClass="text-danger" Display="Dynamic" />
                        <asp:CustomValidator ID="cvStartDateValidation" runat="server"
    ControlToValidate="txtApplicationStartDate"
    ErrorMessage="Application Start Date must be greater than today's date"
    CssClass="text-danger"
    Display="Dynamic"
    OnServerValidate="cvStartDateValidation_ServerValidate"
    ClientValidationFunction="ValidateStartDate" />

                    </div>
                </div>
                 <div class="row mb-3">
     <div class="col-md-2">
         <asp:Label ID="lblApplicationDeadline" runat="server" Text="Application Deadline:" CssClass="bold-label"></asp:Label>
     </div>
     <div class="col-md-8">
         <asp:TextBox ID="txtApplicationDeadline" runat="server" CssClass="form-control" TextMode="Date" />
         <asp:RequiredFieldValidator ID="rfvApplicationDeadline" runat="server" ControlToValidate="txtApplicationDeadline"
             ErrorMessage="Application Deadline is required" CssClass="text-danger" Display="Dynamic" />
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
                   <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" CausesValidation="true" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" CausesValidation="false" />

                </div>
            </div>


        </div>
    </div>
                     <div>   <asp:Button ID="Button2" runat="server" Text="Back" OnClientClick="window.history.back(); return false;"  CssClass="btn btn-link"/>
</div>
</asp:Content>


