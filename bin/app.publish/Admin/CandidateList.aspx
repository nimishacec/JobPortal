<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin1.Master" AutoEventWireup="true" CodeBehind="CandidateList.aspx.cs" Inherits="JobPortalWebApplication.Admin.CandidateList" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
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
    </style>
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
                    <h4 class="card-title">Candidate List</h4>
                    <p class="card-description">
                         <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-2" />
                    </p>
                    <div class="row flex-grow-1 d-flex justify-content-center align-items-center">
                        <div class="col-12">
                            <div class="row jobs-head">
                                <div class="col-md-1 d-none d-sm-block">SlNo</div>
                                <div class="col-md-2 d-none d-sm-block">First Name</div>
                                <div class="col-md-2 d-none d-sm-block">Last Name</div>
                                <div class="col-md-2 d-none d-sm-block">EmailID</div>
                                <div class="col-md-2 d-none d-sm-block">Phone Number</div>
                                <div class="col-md-2 d-none d-sm-block">Highest EducationLevel</div>
                                <%-- <div class="col-md-2 d-none d-sm-block">Address</div>--%>
                                <div class="col-md-1 d-none d-sm-block">Action</div>
                            </div>
                            <div class="jobs-content">
                                <asp:Repeater ID="gvCandidates" runat="server">
                                    <ItemTemplate>
                                        <div class="row job-row" data-jobid='<%# Eval("CandidateID") %>'>
                                            <div class="col-md-1 column">
                                                <b class="d-inline d-sm-none">SlNo: </b><%# ((currentPage - 1) * pageSize) + Container.ItemIndex + 1 %>
                                            </div>

                                            <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">First Name: </b><%# Eval("FirstName") %>
                                            </div>
                                            <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">Last Name: </b><%# Eval("LastName") %>
                                            </div>
                                            <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">EmailID: </b><%# Eval("EmailAddress") %>
                                            </div>
                                            <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">Phone Number: </b><%# Eval("MobileNumber") %>
                                            </div>
                                            <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">Highest EducationLevel: </b><%# Eval("HighestEducationLevel") %>
                                            </div>
                                            <%-- <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">Address: </b><%# Eval("PhysicalAddress") %>
                                            </div>--%>

                                            <div class="col-md-1 action-block d-none d-sm-block">
                                                <div class="dropdown">
                                                    <i class="mdi mdi-dots-vertical showdropdown"></i>
                                                    <div class="dropdown-menu" style="">
                                                        <a class="dropdown-item viewtask cursor" data-taskid="6" data-target="#viewTaskModal" data-toggle="modal" onclick="viewCandidate('<%# Encrypt(Eval("CandidateID").ToString()) %>');"><i class="bx bx-show me-1"></i>View</a>
                                                        <a class="dropdown-item" href="#" onclick="editCandidate('<%# Encrypt(Eval("CandidateID").ToString()) %>');"><i class="bx bx-edit-alt me-1"></i>Edit</a>
                                                        <a class="dropdown-item" href="javascript:void(0);" onclick="confirmDelete('<%# Eval("CandidateID") %>');"><i class="bx bx-trash me-1"></i>Delete</a>
                                                    </div>
                                                    <i class="mdi mdi-arrow-down showdetails"></i>
                                                </div>
                                            </div>

                                            <div id="jobDetailsSection" class="job-details">
                                                <div class="row">
                                                    <div class="col-md-4 column"><b>Address</b>: <%# Eval("Address") %></div>
                                                    <div class="col-md-4 column"><b>City</b>: <%# Eval("City") %></div>
                                                    <div class="col-md-4 column"><b>State/Province</b>: <%# Eval("StateOrProvince") %></div>
                                                    <div class="col-md-4 column"><b>Postal/Zip Code</b>:<%# Eval("PostalOrZipCode") %></div>
                                                    <div class="col-md-4 column"><b>Country</b>: <%# Eval("Country") %></div>
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
        function confirmDelete(Candidate) {
            if (confirm("Are you sure you want to delete this Candidate?")) {
                // Redirect to the server-side page with the employee ID as a query string
                window.location.href = "CandidateList.aspx?deleteID=" + Candidate;
            }
        }
        function viewCandidate(encryptedCandidateId) {
         
            window.location.href = 'CandidateViewDashboard.aspx?CandidateID=' + encodeURIComponent(encryptedCandidateId);
        }
        function editCandidate(encryptedCandidateId) {

            window.location.href = 'CandidateDashboard.aspx?CandidateID=' + encodeURIComponent(encryptedCandidateId);
        }
    </script>



</asp:Content>
