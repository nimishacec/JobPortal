<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin1.Master" AutoEventWireup="true" CodeBehind="JobVerification.aspx.cs" Inherits="JobPortalWebApplication.Admin.JobVerification" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .bg-warning {
            background-color: #edf2c4; /* Yellow background for highlighting */
            font-weight: bold;
        }

        .job-description {
            display: block; /* Ensures it behaves like a block element */
            padding: 10px; /* Optional padding */
            border: 1px solid #ccc; /* Optional border for visibility */

            border-radius: 5px; /* Rounded corners */
            max-height: 300px; /* Optional max height */
            overflow-y: auto; /* Enables scrolling if content exceeds max height */
            white-space: pre-wrap; /* Preserves whitespace and line breaks */
        }
        .modal-backdrop {
            background-color: rgba(0, 0, 0, 0.5); /* Semi-transparent black */
        }

        /* Modal dialog */
        .modal-content {
            border-radius: 8px; /* Rounded corners */
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.3); /* Subtle shadow */
        }

        /* Modal title */
        .modal-title {
            font-size: 1.5rem; /* Larger title */
            font-weight: bold; /* Bold text */
            color: #343a40; /* Dark gray color */
        }

        /* Modal body */
        .modal-body {
            font-size: 1rem; /* Standard font size */
            color: #495057; /* Darker text color */
        }

        /* Modal footer buttons */
        .modal-footer .btn {
            border-radius: 5px; /* Rounded buttons */
            padding: 10px 20px; /* More padding */
        }

        .modal-footer .btn-primary {
            background-color: #007bff; /* Bootstrap primary color */
            border: none; /* Remove border */
        }

        .modal-footer .btn-primary:hover {
            background-color: #0056b3; /* Darker on hover */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="text-align: left">
        <asp:Button ID="Button2" runat="server" Text="Back" OnClientClick="window.history.back(); return false;" CssClass="btn btn-link" />
    </div>

    <div class="col-12 grid-margin">

        <div class="card">
            <div class="card-body">
                <h4 class="card-title">Job Details</h4>


                <p class="card-description">
                </p>
                <%--    <asp:ValidationSummary ID="vsSummary" runat="server" ForeColor="Red" HeaderText="Please correct the following errors:" />--%>

                <div class="row">
                    <!-- Company Name -->
                    <div class="col-md-6">
                        <asp:Label ID="Label1" runat="server" Text="Job Title:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblJobTitle" runat="server" CssClass="form-control mt-2"></asp:Label>

                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label8" runat="server" Text="Job Description:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblJobDescription" runat="server" class="form-control mt-2 job-description" Style="overflow-wrap: break-word; word-wrap: break-word;"></asp:Label>


                    </div>
                </div>
                <div class="row mt-2">
                    <div class="col-md-6">
                        <asp:Label ID="Label2" runat="server" Text="Company Name:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblCompanyName" runat="server" CssClass="form-control mt-2"></asp:Label>
                    </div>



                    <!-- Company Name -->
                    <div class="col-md-6">
                        <asp:Label ID="Label3" runat="server" Text="Job Location:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblJobLocation" runat="server" CssClass="form-control mt-2"></asp:Label>

                    </div>
                </div>
                <div class="row mt-2">
                    <div class="col-md-6">
                        <asp:Label ID="Label5" runat="server" Text="Job Type:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblJobType" runat="server" CssClass="form-control mt-2"></asp:Label>
                    </div>

                    <!-- Company Name -->
                    <div class="col-md-6">
                        <asp:Label ID="Label4" runat="server" Text="Annual Salary(CTC):" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblSalary" runat="server" CssClass="form-control mt-2"></asp:Label>

                    </div>
                </div>

                <div class="row mt-2">
                    <div class="col-md-6">
                        <asp:Label ID="Label7" runat="server" Text="Vacancy:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblVacancy" runat="server" CssClass="form-control mt-2"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label12" runat="server" Text="Company Website:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lnkWebsite" runat="server" CssClass="form-control mt-2"></asp:Label>
                    </div>
                    <!-- Company Name -->

                </div>
                <div class="row mt-2">
                    <div class="col-md-6">
                        <asp:Label ID="Label9" runat="server" Text="Qualifications:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblQualifications" runat="server" CssClass="form-control mt-2"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label10" runat="server" Text="Experience:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblExperience" runat="server" CssClass="form-control mt-2"></asp:Label>
                    </div>
                </div>
                <div class="row mt-2">
                    <!-- Company Name -->
                    <div class="col-md-6">
                        <asp:Label ID="Label11" runat="server" Text="Required Skills:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblRequiredSkills" runat="server" CssClass="form-control mt-2"></asp:Label>

                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label13" runat="server" Text="Contact Email:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblContactEmail" runat="server" CssClass="form-control mt-2"></asp:Label>
                    </div>
                </div>
                <div class="row mt-2">
                    <div class="col-md-6">
                        <asp:Label ID="Label6" runat="server" Text="Application Starts:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblApplicationStarts" runat="server" CssClass="form-control mt-2"></asp:Label>

                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label14" runat="server" Text="Application Deadline:" CssClass="form-label"></asp:Label>
                        <asp:Label ID="lblApplicationDeadline" runat="server" CssClass="form-control mt-2"></asp:Label>

                    </div>
                    <!-- Company Name -->


                </div>
                <%--<div class="job-details-container" runat="server" id="JobDetailsContainer" style="font-family: Calibri; background-color: aliceblue">
                 <h2 style="text-align: center">Job Details</h2>
                 <div class="job-card" style="background-color: aliceblue; padding: 20px; margin: 0 auto; width: 80%;">--%>
                <%-- <h4>Job Title: <span id="lblJobTitle" runat="server"></span></h4>--%>
                <%--  <p><strong>Company Name:</strong> <span id="lblCompanyName" runat="server"></span></p>--%>
                <%-- <p><strong>Location:</strong> <span id="lblJobLocation" runat="server"></span></p>--%>
                <%-- <p><strong>Job Type:</strong> <span id="lblJobType" runat="server"></span></p>--%>
                <%--<p><strong>Annual Salary(CTC):</strong> <span id="lblSalary" runat="server"></span></p>
             <p><strong>Vacancy:</strong> <span id="lblVacancy" runat="server"></span></p>
             <p><strong>Application Deadline:</strong> <span id="lblApplicationDeadline" runat="server"></span></p>
             <p><strong>Description:</strong> <span id="lblJobDescription" runat="server"></span></p>
             <p><strong>Required Skills:</strong> <span id="lblRequiredSkills" runat="server"></span></p>
             <p><strong>Contact Email:</strong> <span id="lblContactEmail" runat="server"></span></p>
             <p><strong>Company Website:</strong> <a id="lnkWebsite" runat="server" target="_blank">Visit</a></p>--%>
                <%-- <div class="actions">
                         <asp:Button ID="EditButton" runat="server" Text="Edit" CssClass="btn btn-primary" OnClick="btnEdit_Click" />
                     </div>
                 </div>
                 <div>
                     <asp:Button ID="Button2" runat="server" Text="Back" CssClass="btn btn-link" OnClick="Button2_Click" />

                 </div>--%>
            </div>
             <div class="modal" id="confirmationModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
         <%-- <div class="modal-header">--%>
            <h6 class="modal-title" id="exampleModalLabel">Approval Confirmation</h6>
           <%-- <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>--%>
         <%-- </div>--%>
          <div class="modal-body">
            The Job Edit request has been Approved.
          </div>
          <div class="modal-footer">
          <%--  <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>--%>
            <button type="button" class="btn btn-primary" id="btnRedirect">OK</button>
          </div>
        </div>
      </div>
    </div>
            <div class=" d-flex justify-content-center">
                <asp:Button ID="btnVerify" runat="server" Text="Approve" CssClass="btn btn-primary" OnClick="btnVerify_Click" />

            </div>
        </div>
      

    </div>
     <script type="text/javascript">
        $(document).ready(function() {
            $('#btnRedirect').on('click', function() {
                window.location.href = 'JobEditApproval.aspx'; // Redirect to the page
            });
        });
    </script>
</asp:Content>
