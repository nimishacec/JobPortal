<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin1.Master" AutoEventWireup="true" CodeBehind="EmployeeList.aspx.cs" Inherits="JobPortalWebApplication.Admin.EmployeeList" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <style type="text/css">

.confirm-dialog {
    display: none; /* Hidden by default */
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(0, 0, 0, 0.5);
    justify-content: center;
    align-items: center;
    z-index: 1000;
}

/* Dialog box styling */
.confirm-dialog-content {
    background: #fff;
    padding: 20px;
    border-radius: 8px;
    text-align: center;
    width: 300px;
    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}

/* Buttons styling */
.confirm-btn, .cancel-btn {
    margin: 10px;
    padding: 8px 12px;
    border: none;
    border-radius: 4px;
    cursor: pointer;
}

.confirm-btn {
    background-color: #d9534f;
    color: #fff;
}

.cancel-btn {
    background-color: #6c757d;
    color: #fff;
}
        .row.jobs-head {
            color: #000;
            font-weight: bold;
            font-size: 14px;
            padding: 15px 0px;
        }

        .jobs-content {
            font-size: 14px;
        }

            .jobs-content .row {
                padding: 10px 0px;
            }

            .jobs-content .job-row:nth-child(odd) {
                background: #F5F7FF;
                border-bottom: 1px solid #CED4DA;
                border-top: 1px solid #CED4DA;
            }

        .action-block {
            display: flex;
            justify-content: space-evenly;
            align-items: center;
            flex-wrap: nowrap;
            flex-direction: row;
            align-content: center;
        }

        .job-details {
            width: 100%;
            padding: 0px 15px;
            display: none;
        }

        .job-row .column {
            padding: 10px 20px;
        }

        .showdetails {
            cursor: pointer;
        }

        .showdropdown {
            cursor: pointer;
        }

        .dropdown-menu {
            right: 0;
            left: auto;
        }

        @media only screen and (max-width: 600px) {
            .job-details {
                display: block !important;
            }
        }
        /* Ensure the GridView takes up the full width of its container */
        /*.gridview-container {
            width: 100%;
            overflow-x: auto;*/ /* Allows horizontal scrolling if needed */
        /*}

        .table {
            width: 100%;
            table-layout: auto;*/ /* Allows the table to adjust column widths automatically */
        /*}*/
    </style>
    <link rel="stylesheet" href="../../vendors/mdi/css/materialdesignicons.min.css">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-lg-12 grid-margin stretch-card">
            <div class="card">
                <div class="container mt-4">
                    <div class="row justify-content-end">
                        <div class="col-auto">
                            <div class="input-group">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Placeholder="Search Here !!"></asp:TextBox>

                                <div class="input-group-append">
                                    <asp:Button ID="serachbtn" OnClick="btnSearch_Click" runat="server" Text="Search" CssClass="btn btn-primary" />
                                    <%-- <%--<%-- <button class="btn btn-primary" type="button" onclick="btnSearch_Click();">
                     Search
                </button>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="card-body">
                    <h4 class="card-title">Employee List</h4>
                    <p class="card-description">
                        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-2" />
                    </p>
                    <div class="row flex-grow-1 d-flex justify-content-center align-items-center">
                        <div class="col-12">
                            <div class="row jobs-head">
                                <div class="col-md-1 d-none d-sm-block">SlNo</div>
                                <div class="col-md-2 d-none d-sm-block">Company Name</div>
                                <div class="col-md-2 d-none d-sm-block">RegNumber</div>
                                <div class="col-md-2 d-none d-sm-block">Company Email</div>
                                <div class="col-md-2 d-none d-sm-block">PhoneNumber</div>
                                <div class="col-md-2 d-none d-sm-block">WebsiteUrl</div>
                                <%-- <div class="col-md-2 d-none d-sm-block">Address</div>--%>
                                <div class="col-md-1 d-none d-sm-block">Action</div>
                            </div>
                            <div class="jobs-content">
                                <asp:Repeater ID="rptJobs" runat="server">
                                    <ItemTemplate>
                                        <div class="row job-row" data-jobid='<%# Eval("EmployeeID") %>'>
                                            <div class="col-md-1 column">
                                                <b class="d-inline d-sm-none">SlNo: </b><%# ((currentPage - 1) * pageSize) + Container.ItemIndex + 1 %>
                                            </div>

                                            <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">Company Name: </b><%# Eval("CompanyName") %>
                                            </div>
                                            <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">RegNumber: </b><%# Eval("CompanyRegistrationNumber") %>
                                            </div>
                                            <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">Company Email: </b><%# Eval("CompanyEmail") %>
                                            </div>
                                            <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">PhoneNumber: </b><%# Eval("CompanyPhoneNumber") %>
                                            </div>
                                            <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">WebsiteUrl: </b><%# Eval("CompanyWebsiteUrl") %>
                                            </div>
                                            <%-- <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">Address: </b><%# Eval("PhysicalAddress") %>
                                            </div>--%>

                                            <div class="col-md-1 action-block d-none d-sm-block">
                                                <div class="dropdown">
                                                    <i class="mdi mdi-dots-vertical showdropdown"></i>
                                                    <div class="dropdown-menu" style="">
                                                        <a class="dropdown-item viewtask cursor" data-taskid="6" data-target="#viewTaskModal" data-toggle="modal"
                                                            onclick="viewEmployee('<%# Encrypt(Eval("EmployeeID").ToString()) %>'); return false;">
                                                            <i class="bx bx-show me-1"></i>View
                </a>
                                                        <a class="dropdown-item" href="#" onclick="editEmployee('<%# Encrypt(Eval("EmployeeID").ToString()) %>');"><i class="bx bx-edit-alt me-1"></i>Edit</a>
                                                        <a class="dropdown-item" href="javascript:void(0);" onclick="openConfirmDialog('<%# Eval("EmployeeID") %>');"><i class="bx bx-trash me-1"></i>Delete</a>
                                                       
                                                    </div>
                                                
                                                <i class="mdi mdi-arrow-down showdetails"></i>
                                            </div>
                                                 <div id="confirmDialog" class="confirm-dialog">
     <div class="confirm-dialog-content">
         <p>Are you sure you want to delete this Employee?</p>
         <button class="confirm-btn" onclick="confirmDelete()">Yes, delete</button>
         <button class="cancel-btn" onclick="closeConfirmDialog()">Cancel</button>
     </div>
 </div>
                                       </div>

                                        <div id="jobDetailsSection" class="job-details">
                                            <div class="row">
                                                <div class="col-md-4 column"><b>Contact Name</b>: <%# Eval("ContactPersonName") %></div>
                                                <div class="col-md-4 column"><b>Contact Number</b>: <%# Eval("ContactPersonPhoneNumber") %></div>
                                                <div class="col-md-4 column"><b>Physical Address</b>: <%# Eval("PhysicalAddress") %></div>
                                                <div class="col-md-4 column"><b>Company Description</b>:<%# Eval("CompanyDescription") %></div>
                                                <div class="col-md-4 column"><b>Company Size</b>: <%# Eval("CompanySize") %></div>
                                            </div>
                                        </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </div>
                        </div>
                    </div>
                </div>


                <div class="pagination-controls">
                    <asp:LinkButton ID="lnkPrevious" runat="server" Text="Previous" OnClick="lnkPrevious_Click" CssClass="btn btn-primary"></asp:LinkButton>
                    <asp:Label ID="lblPageInfo" runat="server" CssClass="page-info"></asp:Label>
                    <asp:LinkButton ID="lnkNext" runat="server" Text="Next" OnClick="lnkNext_Click" CssClass="btn btn-primary"></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        $(document).ready(function () {

            $('.showdetails').click(function () {

                $(this).closest('.job-row').find('.job-details').slideToggle(300);
            });

        });
        $('.showdropdown').click(function () {
            $('.dropdown-menu').hide();
            $(this).closest('.job-row').find('.dropdown-menu').toggle();
        });
        $(document).on('click', function (event) {
            if (!$(event.target).closest('.dropdown').length) {
                $('.dropdown-menu').hide();  // Hide the dropdown menu
            }
        });
        //function confirmDelete(employeeID) {
        //    if (confirm("Are you sure you want to delete this employee?")) {

        //        window.location.href = "EmployeeList.aspx?deleteID=" + employeeID;
        //    }
        //}
      /*  let employeeToDelete = null;*/ // Variable to store the EmployeeID to delete

        function openConfirmDialog(employeeID) {
           
            window.employeeToDelete = employeeID; // Store EmployeeID for the current delete action
            document.getElementById("confirmDialog").style.display = "flex"; // Show the dialog
        }

        function closeConfirmDialog() {
            document.getElementById("confirmDialog").style.display = "none"; // Hide the dialog
        }
        function confirmDelete() {
            // Perform deletion here or redirect
            window.location.href = 'EmployeeList.aspx?deleteID=' + window.employeeToDelete;
            closeConfirmDialog();
        }
        //function confirmDelete() {
        //    // Here, add logic to handle the deletion, such as a redirect or AJAX request
        //    // Example: Redirect to delete action
        //    window.location.href = 'EmployeeList.aspx?EmployeeID=' + window.employeeToDelete;
        //    closeConfirmDialog(); // Close the dialog after deletion
        //}

        function viewEmployee(encryptedEmployeeId) {
            window.location.href = 'EmployerViewDashboard.aspx?EmployeeID=' + encodeURIComponent(encryptedEmployeeId);
        }
        function editEmployee(encryptedEmployeeId) {

            window.location.href = 'EmployeeDashboard.aspx?EmployeeID=' + encodeURIComponent(encryptedEmployeeId);
        }

    </script>


</asp:Content>
