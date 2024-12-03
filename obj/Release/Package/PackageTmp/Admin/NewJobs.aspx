<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin1.Master" AutoEventWireup="true" CodeBehind="NewJobs.aspx.cs" Inherits="JobPortalWebApplication.Admin.NewJobs" EnableEventValidation="false" EnableSessionState="True" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />

    <!-- jQuery and Select2 JS -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js">

    </script>
    <style type="text/css">
        .dropbtn {
            background-color: #04AA6D;
            color: white;
            padding: 16px;
            font-size: 16px;
            border: none;
            cursor: pointer;
        }

            .dropbtn:hover, .dropbtn:focus {
                background-color: #3e8e41;
            }

        #myInput {
            box-sizing: border-box;
            background-image: url('searchicon.png');
            background-position: 14px 12px;
            background-repeat: no-repeat;
            font-size: 16px;
            padding: 14px 20px 12px 45px;
            border: none;
            border-bottom: 1px solid #ddd;
        }

            #myInput:focus {
                outline: 3px solid #ddd;
            }

        .dropdown {
            position: relative;
            display: inline-block;
        }

        .dropdown-content {
            display: none;
            position: absolute;
            background-color: #f6f6f6;
            min-width: 230px;
            overflow: auto;
            border: 1px solid #ddd;
            z-index: 1;
        }

            .dropdown-content a {
                color: black;
                padding: 12px 16px;
                text-decoration: none;
                display: block;
            }

        .dropdown a:hover {
            background-color: #ddd;
        }

        .show {
            display: block;
        }










        .select2-container .select2-selection--single {
            height: 34px !important;
        }

        .select2-container--default .select2-selection--single {
            border: 1px solid #ccc !important;
            border-radius: 0px !important;
        }

        .switch {
            position: relative;
            display: inline-block;
            width: 50px;
            height: 20px;
        }

            .switch input {
                display: none;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #2196F3;
            transition: 0.4s;
            border-radius: 34px; /* Rounded corners */
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 16px;
                width: 20px;
                left: 4px;
                bottom: 2px;
                background-color: white;
                transition: 0.4s;
                border-radius: 50%; /* Rounded knob */
            }

        input:checked + .slider {
            background-color: #4CAF50; /* Green background when ON */
        }

            input:checked + .slider:before {
                transform: translateX(26px); /* Move the knob to the right */
            }

        /* Label for "ON" and "OFF" */
        .on-text, .off-text {
            position: absolute;
            font-size: 10px;
            color: white;
            font-weight: bold;
            top: 50%;
            transform: translateY(-50%);
        }

        .on-text {
            left: 5px; /* Position for the ON label */
        }

        .off-text {
            right: 5px; /* Position for the OFF label */
        }

        input:checked + .slider .off-text {
            display: none;
        }

        input:not(:checked) + .slider .on-text {
            display: none;
        }

        /* Optional: Container for better positioning */
        .switch-container {
            /*  margin-top: 2px;*/
            /*  margin-bottom: 5px;*/
            text-align: left;
        }

        /* Modal/Confirmation Box Styles */
        .modal {
            display: none;
            position: fixed;
            z-index: 1;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5); /* Dark background */
        }

        .modal-content {
            background-color: #fefefe;
            margin: 15% auto;
            padding: 20px;
            border: 1px solid #888;
            width: 300px;
            text-align: center;
        }

        button {
            margin: 10px;
            padding: 10px;
            background-color: #4CAF50;
            color: white;
            border: none;
            cursor: pointer;
            border-radius: 5px;
        }

            button:hover {
                background-color: #45a049;
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

        .toggle-container {
            display: flex;
            align-items: center;
        }

        .toggle {
            position: relative;
            display: inline-block;
            width: 60px;
            height: 34px;
        }

            .toggle input {
                opacity: 0;
                width: 0;
                height: 0;
            }

            .toggle .slider {
                position: absolute;
                cursor: pointer;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                background-color: #ccc;
                transition: .4s;
                border-radius: 34px;
            }

                .toggle .slider:before {
                    position: absolute;
                    content: "";
                    height: 26px;
                    width: 26px;
                    left: 4px;
                    bottom: 4px;
                    background-color: white;
                    transition: .4s;
                    border-radius: 50%;
                }

        input:checked + .slider {
            background-color: #2196F3;
        }

            input:checked + .slider:before {
                transform: translateX(26px);
            }

        /* Optional styles for label */
        label {
            margin-left: 10px;
        }

        }

        .form-control::placeholder {
            color: lightgray; /* Change this to your desired light color */
            opacity: 1;
        }

        @media only screen and (max-width: 600px) {
            .job-details {
                display: block !important;
            }
        }
            
    </style>
    <%--  <style>
    
        .dropdown {
            position: relative;
            display: inline-block;
            width: 200px;
        }

        .dropdown-content {
            display: none;
            position: absolute;
            background-color: white;
            min-width: 100%;
            border: 1px solid #ccc;
            z-index: 1;
            max-height: 200px;
            overflow-y: auto;

        }

        .dropdown-content input {
            padding: 10px;
            width: 100%;
            box-sizing: border-box;
            border: 1px solid #ccc;
            border-bottom: none; 
        }

        .dropdown-content a {
            color: black;
            padding: 12px 16px;
            text-decoration: none;
            display: block;
        }

        .dropdown-content a:hover {
            background-color: #f1f1f1;
        }

        .dropdown:hover .dropdown-content {
            display: block;
        }
    </style>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="row">
        <div class="col-lg-12 grid-margin stretch-card">
            <div class="card">
                <div class="container mt-4">
                    <div class="row">
                        <%--  <input type="text" id="txtname" runat="server" />--%>
                        <%-- <div class="dropdown">
            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Search company..." onkeyup="filterFunction()"></asp:TextBox>
            <div class="dropdown-content" id="dropdownContent">
                <a href="#" class="item">Apple Inc.</a>
                <a href="#" class="item">Microsoft Corporation</a>
                <a href="#" class="item">Google LLC</a>
                <a href="#" class="item">Amazon.com, Inc.</a>
                <a href="#" class="item">Facebook, Inc.</a>
                <a href="#" class="item">Tesla, Inc.</a>
                <a href="#" class="item">Netflix, Inc.</a>
                <a href="#" class="item">Adobe Inc.</a>
                <a href="#" class="item">IBM Corporation</a>
                <a href="#" class="item">Samsung Electronics</a>
            </div>
        </div>--%>


                        <!-- Select dropdown with Select2 -->
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="form-control select2"></asp:DropDownList>



                                <%-- <label for="dropdownSelect">Search By JobTitle</label>--%>

                                <%--  <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="js-example-basic-single w-100 form-control"></asp:DropDownList>
                                --%> <%--<option value="AL">Alabama</option>
                                    <option value="WY">Wyoming</option>
                                    <option value="AM">America</option>
                                    <option value="CA">Canada</option>
                                    <option value="RU">Russia</option>--%>
                            </div>

                        </div>


                        <!-- Search input box -->
                        <div class="col-md-4">
                            <div class="input-group">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Placeholder="Search by JobTitle or Email or Phone Number"></asp:TextBox>
                            </div>
                        </div>

                        <!-- Search button -->
                        <div class="col-auto">
                            <div class="input-group-append">
                                <asp:Button ID="serachbtn" OnClick="btnSearch_Click" runat="server" Text="Search" CssClass="btn btn-primary form-control" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="card-body">
                    <h4 class="card-title">Jobs List</h4>
                    <p class="card-description">
                        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-2" />
                    </p>
                    <div class="row flex-grow-1 d-flex justify-content-center align-items-center">
                        <div class="col-12">
                            <div class="row jobs-head">
                                <div class="col-md-1 d-none d-sm-block">SlNo</div>
                                <div class="col-md-2 d-none d-sm-block">Job Title</div>
                                <%-- <div class="col-md-2 d-none d-sm-block">Job Type</div>--%>
                                <div class="col-md-2 d-none d-sm-block">Company Name</div>
                                <div class="col-md-2 d-none d-sm-block">ApplicationDeadline</div>
                                <div class="col-md-2 d-none d-sm-block">Status</div>
                                <div class="col-md-1 d-none d-sm-block">Verify</div>
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
                                                <b class="d-inline d-sm-none">Job Title: </b><%# Eval("JobTitle") %>
                                            </div>
                                            <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">Company Name: </b><%# Eval("CompanyName") %>
                                            </div>

                                            <%-- <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">Job Type: </b><%# Eval("JobType") %>
                                            </div>--%>
                                            <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">Application Deadline: </b><%# Eval("ApplicationDeadline") %>
                                            </div>
                                            <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">Status: </b>


                                                <%# 
                                            Eval("VerificationStatus").ToString() == "REJECTED" 
                                            ? "<div class='progress'><div class='badge badge-danger' role='progressbar' style='width: 80%;' aria-valuenow='50' aria-valuemin='0' aria-valuemax='100'>Rejected</div></div>" 
                                              : Eval("VerificationStatus").ToString() == "APPROVED" 
                                              ? "<div class='progress'><div class='badge badge-success' role='progressbar' style='width: 80%;' aria-valuenow='50' aria-valuemin='0' aria-valuemax='50'>Approved</div></div>"                                       
                                            : DateTime.Now > Convert.ToDateTime(Eval("ApplicationDeadline"))
                                            ? "<div class='progress'><div class='badge badge-primary' role='progressbar' style='width: 50%;' aria-valuenow='50' aria-valuemin='0' aria-valuemax='50'>Completed</div></div>" 
                                            : "<div class='progress'><div class='badge badge-warning' role='progressbar' style='width: 50%;' aria-valuenow='50' aria-valuemin='0' aria-valuemax='50'>In Progress</div></div>"
                                                %>
                                            </div>
                                            <%-- <asp:HiddenField ID="hfAntiForgeryToken" runat="server" />--%>


                                            <div class="col-md-1 column">
                                                <b class="d-inline d-sm-none">Verify: </b>
                                                <!-- Toggle Button with Labels -->
                                                <div class="switch-container">
                                                    <!-- Assume the job status is fetched from the server, if job status is "approved" the toggle is checked -->
                                                    <label class="switch">
                                                        <input type="checkbox" class="toggle-btn" data-jobid='<%# Eval("JobID") %>' onchange="openConfirmation(this)"
                                                            id="toggleSwitch" <%# Eval("VerificationStatus").ToString() == "APPROVED" ? "checked" : "" %> />
                                                        <!-- Use 'checked' if job status is approved -->
                                                        <span class="slider">
                                                            <span class="on-text">ON</span>
                                                            <span class="off-text">OFF</span>
                                                        </span>
                                                    </label>
                                                </div>

                                                <!-- Custom Confirmation Box -->
                                                <div id="confirmationBox" class="modal">
                                                    <div class="modal-content">
                                                        <p id="confirmationText"></p>
                                                        <button id="confirmBtn" onclick="confirmStatusChange()">Yes</button>
                                                        <button onclick="closeConfirmation()">No</button>
                                                    </div>
                                                </div>
                                            </div>
                                            <%-- <div class="toggle-container">
        <label for="jobToggle"><%--Job Status:</label>
        <label class="toggle">
            <input type="checkbox" id="jobToggle" data-jobid='<%# Eval("JobID") %>' onchange="toggleStatus(this)">
            <span class="slider"></span>
        </label>
        <span class="status-label" id="statusLabel"></span>
    </div>
                                                </div>--%>
                                            <%--   <div class="toggle-container">
        <label for="jobToggle">Job Status:</label>
        <label class="toggle">
            <input type="checkbox" id="jobToggle" data-jobid="3016" onclick="toggleStatus(this)">
            <span class="slider"></span>
        </label>
    </div>
</div>--%>
                                            <%--  <label class="switch">
        <input type="checkbox" id="approveToggle" data-jobid='<%# Eval("JobID") %>' onchange="toggleStatus(this)">
        <span class="slider round"></span>
    </label>--%>
                                            <%-- 
                                                    <div class="form-check form-switch">
                                                        <input class="form-check-input" type="checkbox" id="toggleSwitch_<%# Eval("JobID") %>" onclick="toggleStatus('<%# Eval("JobID") %>', this.checked ? 'Approve' : 'Reject')" checked>
                                                        <label class="form-label" for="toggleSwitch_<%# Eval("JobID") %>">Approve</label>
                                                    </div>--%>
                                            <%-- <div class="btn-group" role="group">
                                                <!-- Approve Button -->
                                                <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-outline-success btn-sm" Text="Approve" OnClick="btnApprove_Click" CommandArgument='<%# Eval("JobID") %>' />

                                                <!-- Reject Button -->
                                                <asp:Button ID="btnReject" runat="server" CssClass="btn btn-outline-danger btn-sm" Text="Reject" OnClick="btnReject_Click" CommandArgument='<%# Eval("JobID") %>' />
                                            </div>--%>

                                            <%--   <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CommandArgument='<%# Eval("JobID") %>' OnClick="btnReject_Click" CssClass="btn btn-outline-danger btn-sm"></asp:LinkButton>--%>
                                            <%-- </div>
                                                </div>--%>

                                            <div class="col-md-1 action-block d-none d-sm-block">
                                                <div class="dropdown">
                                                    <i class="mdi mdi-dots-vertical showdropdown"></i>
                                                    <div class="dropdown-menu" style="">
                                                        <a class="dropdown-item viewtask cursor" data-taskid="6" data-target="#viewTaskModal" data-toggle="modal"
                                                            onclick="viewJobs('<%# Encrypt(Eval("JobID").ToString()) %>'); return false;">
                                                            <i class="bx bx-show me-1"></i>View
                                                             </a>
                                                        <a class="dropdown-item" href="#" onclick="editJobs('<%# Encrypt(Eval("JobID").ToString()) %>');"><i class="bx bx-edit-alt me-1"></i>Edit</a>
                                                        <a class="dropdown-item" href="javascript:void(0);" onclick="confirmDelete('<%# Eval("JobID") %>');">
                                                            <i class="bx bx-trash me-1"></i>Delete
                                                              </a>
                                                        <%-- <asp:Button ID="btnConfirmDelete" runat="server" Text="Confirm Delete" OnClick="btnConfirmDelete_Click" Visible="false" />--%>

                                                        <div class="modal fade" id="confirmationModal" tabindex="-1" role="dialog" aria-labelledby="confirmationModalLabel" aria-hidden="true">
                                                            <div class="modal-dialog" role="document">
                                                                <div class="modal-content">
                                                                    <div class="modal-header">
                                                                        <h5 class="modal-title" id="confirmationModalLabel">Confirm Deletion</h5>
                                                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                            <span aria-hidden="true">&times;</span>
                                                                        </button>
                                                                    </div>
                                                                    <div class="modal-body">
                                                                        Are you sure you want to delete this job?
                       
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                                                                        <button type="button" class="btn btn-danger" onclick="deleteJob()">Delete</button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <i class="mdi mdi-arrow-down showdetails"></i>
                                                </div>
                                            </div>

                                            <div id="jobDetailsSection" class="job-details">
                                                <div class="row">

                                                    <div class="col-md-4 column"><b>Job Location</b>: <%# Eval("JobLocation") %></div>
                                                    <div class="col-md-4 column"><b>Company Email</b>: <%# Eval("CompanyEmail") %></div>
                                                    <div class="col-md-4 column"><b>Salary</b>: <%# Eval("Salary") %></div>
                                                    <div class="col-md-4 column"><b>Company PhoneNumber</b>:<%# Eval("CompanyPhoneNumber") %></div>
                                                    <%--   <div class="col-md-4 column"><b>Company Size</b>: <%# Eval("CompanyEmail") %></div>--%>
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




    <%--    <div class="table-responsive-lg" style="max-height: 500px; overflow-y: auto;">
            <asp:GridView ID="gvJobs" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-responsive" AllowPaging="True" PageSize="10" OnPageIndexChanging="gvJobs_PageIndexChanging" >
                <Columns>
                     <asp:BoundField DataField="SLNO" HeaderText="SLNo" />
                   <%-- <asp:BoundField DataField="JobID" HeaderText="Job ID" />--%>
    <%-- <asp:BoundField DataField="JobTitle" HeaderText="Job Title"  ControlStyle-CssClass="text-center"/>
                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                    <asp:BoundField DataField="JobLocation" HeaderText="Location" />
                    <asp:BoundField DataField="JobType" HeaderText="Job Type" />
                    <asp:BoundField DataField="ApplicationDeadline" HeaderText="Application Deadline"  />
                    <asp:BoundField DataField="Salary" HeaderText="Salary" />
                     <asp:BoundField DataField="VerificationStatus" HeaderText="VerificationStatus" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlView" runat="server" 
                                NavigateUrl='<%# ResolveUrl("JobDetails.aspx?JobID=" + Eval("JobID")) %>' 
                                Text="View Details" CssClass="btn btn-outline-info btn-sm">
                            </asp:HyperLink>
                              <asp:Button ID="btnApprove" runat="server" CommandArgument='<%# Eval("JobID") %>' Text="Approve" CssClass="btn btn-outline-primary btn-sm" OnClick="btnApprove_Click" /> 
                            <asp:Button ID="btnReject" runat="server" CommandArgument='<%# Eval("JobID") %>' Text="Reject" CssClass="btn btn-outline-danger btn-sm" OnClick="btnReject_Click" /> 
                            <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Eval("JobID") %>' Text="Delete" CssClass="btn btn-warning btn-sm" OnClick="btnDelete_Click" />
                       
                               </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
           <asp:Label ID="lblNoJobs" runat="server" Text="You have not recieved any Job Postings." Visible="false" ForeColor="Red"></asp:Label>
        </div>--%>
    <%-- </div>--%>
   <%-- <asp:HiddenField ID="hfAntiForgeryToken" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= hfAntiForgeryToken.ClientID %>').val('<%= GenerateAntiForgeryToken() %>'); // Set token value
    });
</script>--%>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js"></script>
   <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
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
        function confirmDelete(jobID) {
            if (confirm("Are you sure you want to delete this Job?")) {

                window.location.href = "NewJobs.aspx?deleteID=" + jobID;
            }
        }
        function viewJobs(encryptedJobId) {
            window.location.href = 'JobDetails.aspx?JobID=' + encodeURIComponent(encryptedJobId);
        }
        function editJobs(encryptedJobId) {

            window.location.href = 'EditJobs.aspx?JobID=' + encodeURIComponent(encryptedJobId);
        }

        var currentJobID = null;
        var currentToggleElement = null;

        // Open custom confirmation box
        function openConfirmation(element) {
            currentJobID = element.getAttribute("data-jobid");
            currentToggleElement = element; // Save the reference to the current toggle switch

            // Determine the intended action based on the toggle state
            const intendedAction = element.checked ? 'APPROVE' : 'REJECT';

            // Show confirmation box
            document.getElementById("confirmationBox").style.display = "block";
            document.getElementById("confirmationText").innerText = "Are you sure you want to " + intendedAction + " this job?";
        }

        // Close the confirmation box
        function closeConfirmation() {
            document.getElementById("confirmationBox").style.display = "none";
        }

        // When "Yes" is clicked in the confirmation box
        function confirmStatusChange() {
            // Determine the current status based on toggle
            const currentStatus = currentToggleElement.checked ? 'APPROVED' : 'REJECTED';

            // Optimistically update the UI immediately
         //  updateStatusUI(currentStatus, currentToggleElement);

            // Send the update to the server
            updateJobStatus(currentJobID, currentStatus, currentToggleElement);

            // Close the confirmation box after updating
            closeConfirmation();
        }


       
        function updateJobStatus(jobID, status, element) {
            console.log("AJAX function called with jobID: " + jobID + " and status: " + status + "and element" + element);
            $.ajax({
                type: "POST",
                url: "NewJobs.aspx/UpdateJobStatus",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ JobId: jobID, Action: status }), // Replace with actual values
                dataType: "json",
                success: function (response) {
                    console.log("Response:", response);
                    alert("success", result.data)
                },
                error: function (xhr, status, error) {
                    console.error("Error:", xhr.responseText);
                    alert(error);
                    //alert("ERROR", xhr.status)
                }
            });
            //$.ajax({
            //    type: "POST",
            //    url:  "NewJobs.aspx/UpdateJobStatus", // Correcting the URL format
            //    data: JSON.stringify({ jobID: jobID, action: status }),
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    xhrFields: {
            //        withCredentials: true // Ensures cookies (like ASP.NET_SessionId) are sent with the request
            //    },      
            //    success: function (result,status,xhr) {
            //    //    try {
            //    //        console.log("AJAX request successful:", result);
            //    //        // Try to parse the response as JSON
            //    //        var jsonResponse = JSON.parse(result);
            //    //        alert("success", result.data)
            //    //        if (jsonResponse.success) {
            //    //            alert("Job status updated successfully!");
            //    //        } else {
            //    //            alert("Error: " + jsonResponse.error);
            //    //        }
            //    //    } catch (e) {
            //    //        console.error('Error parsing response:', e);
            //    //        alert('An error occurred. Server might have returned an HTML error page.');
            //    //    }
            //    //},
            //    //error: function (xhr, status, error) {
            //    //    // Handle error responses
            //    //    console.error('AJAX error:', error);
            //    //    if (xhr.responseText.startsWith("<!DOCTYPE")) {
            //    //        alert('The server returned an error page, not JSON.');
            //    //    } else {
            //    //        alert('AJAX call failed: ' + error);
            //    //    }
            //    //}
            //        console.log("AJAX request successful:", response);
            //        if (response.d === "Success") {
            //            alert("Job status updated successfully.");
            //          //  updateProgressBar(status);
            //        } else {
            //            alert("Failed to update job status: " + response.d);
            //        }
            //    },
            //    error: function (xhr, status, error) {
            //        console.error("Error occurred. Status:", xhr.status);
            //        console.error("Error details:", error);
                    
            //        alert("An error occurred while updating the job status. Status: " + xhr.status);
            //    }
            //});
        }



       
        function  updateProgressBar(status) {
            var progressBar = $("#progressBar");
            if (status === 'APPROVED') {
                progressBar.html('<div class="badge badge-success">Approved</div>');
            } else if (status === 'REJECTED') {
                progressBar.html('<div class="badge badge-danger">Rejected</div>');
            }
        }


        //var currentJobID = null;
        //var currentToggleElement = null; // Store the toggle element to update after confirmation

        //// Open custom confirmation box
        //function openConfirmation(element) {
        //    currentJobID = element.getAttribute("data-jobid");
        //    currentToggleElement = element; // Save the reference to the current toggle switch

        //    // Determine the intended action based on the toggle state
        //    const intendedAction = element.checked ? 'APPROVED' : 'REJECTED';

        //    // Show confirmation box
        //    document.getElementById("confirmationBox").style.display = "block";
        //    document.getElementById("confirmationText").innerText = "Are you sure you want to " + intendedAction + " this job?";
        //}

        //// Close the confirmation box
        //function closeConfirmation() {
        //    document.getElementById("confirmationBox").style.display = "none";
        //}

        //// When "Yes" is clicked in the confirmation box
        //function confirmStatusChange() {
        //    // Determine the current status based on toggle
        //    const currentStatus = currentToggleElement.checked ? 'APPROVED' : 'REJECTED';

        //    // Optimistically update the UI immediately
        // //  updateStatusUI(currentStatus, currentToggleElement);

        //    // Send the update to the server
        //    updateJobStatus(currentJobID, currentStatus, currentToggleElement);

        //    // Close the confirmation box after updating
        //    closeConfirmation();
        //}

        //function updateJobStatus(jobID, status) {
        //    // Disable the toggle switch to prevent double-clicking
        //    currentToggleElement.disabled = true;

        //    // Send the fetch request to update the job status on the server
        //    fetch(`NewJobs.aspx?JobID=${jobID}&Action=${status}`, {
        //        method: 'POST',
        //        headers: {
        //            'Content-Type': 'application/json'
        //        },
        //        body: JSON.stringify({
        //            jobID: jobID,
        //            action: status
        //        })
        //    })
        //        .then(response => response.json()) // Parse the JSON response
        //        .then(data => {
        //            if (!data.success) {
        //                // Handle unsuccessful response, revert the UI
        //                alert('Failed to update job status. Reverting changes.');
        //                revertToggle();
        //            }
        //        })
        //        .catch(error => {
        //            console.error('Error:', error);
        //            // Revert the UI if there was an error
        //            revertToggle();
        //        })
        //        .finally(() => {
        //            // Re-enable the toggle switch after completion
        //            currentToggleElement.disabled = false;
        //        });
        //}

        //// Update the UI immediately based on the toggle change
        //function updateStatusUI(status, toggleElement) {
        //    // Update the toggle switch state
        //    toggleElement.checked = (status === 'APPROVED');

        //    // Locate the progress bar container (assuming it's right next to the toggle)
        //    let progressBarContainer = toggleElement.closest('.col-md-1').previousElementSibling;

        //    // Update the progress bar and label based on the new status
        //    if (status === 'APPROVED') {
        //        progressBarContainer.innerHTML = `
        //    <div class='progress'>
        //        <div class='badge badge-success' role='progressbar' style='width: 80%;' aria-valuenow='50' aria-valuemin='0' aria-valuemax='100'>
        //            Approved
        //        </div>
        //    </div>`;
        //    } else if (status === 'REJECTED') {
        //        progressBarContainer.innerHTML = `
        //    <div class='progress'>
        //        <div class='badge badge-danger' role='progressbar' style='width: 80%;' aria-valuenow='50' aria-valuemin='0' aria-valuemax='100'>
        //            Rejected
        //        </div>
        //    </div>`;
        //    }
        //}

        //// Revert the status UI if the server call fails
        //function revertToggle() {
        //    currentToggleElement.checked = !currentToggleElement.checked; // Revert the toggle state
        //    const revertedStatus = currentToggleElement.checked ? 'APPROVED' : 'REJECTED'; // Get the reverted status
        //    updateStatusUI(revertedStatus, currentToggleElement); // Update the UI
        //}


        // Update the job status in the database
        //function updateJobStatus(jobID, status) {
        //    // In a real-world scenario, you would make a server request here
        //    // For example, you can use the fetch API to send the data to the server

        //    console.log("Job ID: " + jobID + ", Status: " + status);

        //    // Simulate a successful update
        //    alert("Job status updated to: " + status);
        //}

        //function toggleStatus(element) {
        //    var jobID = element.getAttribute("data-jobid");
        //    var status = element.checked ? 'APPROVED' : 'REJECTED';

        //    // Update status label text
        //   // document.getElementById("statusLabel").innerText = status;

        //    // Send the update request to the server
        //    updateJobStatus(jobID, status);
        //}

        //function updateJobStatus(jobID, status, currentToggleElement) {
        //    // Fetch API call to update the job status on the server
        //    fetch(`NewJobs.aspx?JobID=${jobID}&Action=${status}`, {
        //        method: 'POST',
        //        headers: {
        //            'Content-Type': 'application/json'
        //        },
        //        body: JSON.stringify({
        //            jobID: jobID,
        //            action: status
        //        })
        //    })
        //        .then(response => response.json()) // Parse the JSON response
        //        .then(data => {
        //            if (data.success) {
        //                console.log('Job status updated successfully');
        //                // Update the UI after a successful response
        //                updateStatusUI(status, currentToggleElement);
        //            } else {
        //                alert('Failed to update job status. Please try again.');
        //                // Revert the toggle switch if the update fails
        //                currentToggleElement.checked = !currentToggleElement.checked;
        //            }
        //        })
        //        .catch(error => {
        //            console.error('Error:', error);
        //            // Revert the toggle switch on error
        //            currentToggleElement.checked = !currentToggleElement.checked;
        //        });
        //}

        //// Update the UI dynamically based on the new status
        //function updateStatusUI(status, toggleElement) {
        //    // Locate the progress bar container (assuming it's right next to the toggle)
        //    let progressBarContainer = toggleElement.closest('.col-md-1').previousElementSibling;

        //    // Update the progress bar and label based on the new status
        //    if (status === 'APPROVED') {
        //        progressBarContainer.innerHTML = `
        //    <div class='progress'>
        //        <div class='badge badge-success' role='progressbar' style='width: 80%;' aria-valuenow='50' aria-valuemin='0' aria-valuemax='100'>
        //            Approved
        //        </div>
        //    </div>`;
        //    } else if (status === 'REJECTED') {
        //        progressBarContainer.innerHTML = `
        //    <div class='progress'>
        //        <div class='badge badge-danger' role='progressbar' style='width: 80%;' aria-valuenow='50' aria-valuemin='0' aria-valuemax='100'>
        //            Rejected
        //        </div>
        //    </div>`;
        //    }
        //}
        //function updateJobStatus(jobID, status, currentToggleElement) {
        //    // Fetch API call to update the job status via POST
        //    fetch('NewJobs.aspx/UpdateJobStatus', {
        //        method: 'POST',
        //        headers: {
        //            'Content-Type': 'application/json'
        //        },
        //        body: JSON.stringify({
        //            jobID: jobID,
        //            action: status
        //        })
        //    })
        //        .then(response => {
        //            if (!response.ok) {
        //                throw new Error('Failed to update job status');
        //            }
        //            return response.json();
        //        })
        //        .then(data => {
        //            if (data.success) {
        //                console.log('Job status updated successfully');
        //                // Update toggle switch based on the response
        //                currentToggleElement.checked = (status === 'APPROVED');
        //            } else {
        //                alert('Failed to update job status. Please try again.');
        //                console.log('Failed to update job status');
        //            }
        //        })
        //        .catch(error => {
        //            console.error('Error:', error);
        //            alert('An error occurred while updating the job status.');
        //        });
        //}

        //function toggleStatus(element) {
        //     Get JobID from data attribute
        //    var jobID = element.getAttribute("data-jobid");
        //    var action = element.checked ? 'APPROVED' : 'REJECTED';

        //     Confirmation dialog
        //    if (confirm("Are you sure you want to " + action + " this Job?")) {
        //         Redirect to NewJobs.aspx with JobID and Action as query parameters
        //        window.location.href = "NewJobs.aspx?JobID=" + jobID + "&Action=" + action;
        //    } else {
        //         If user cancels, revert the checkbox to its original state
        //        element.checked = !element.checked;
        //    }
        //}
        //function updateJobStatus(JobID, status, currentToggleElement) {
        //    $.ajax({
        //        type: "POST",
        //        url: "NewJobs.aspx/GetData",
        //        data: JSON /*JSON.stringify({ jobID: JobID, action: status })*/,
        //        contentType: "application/json; charset=utf-8",
        //        xhrFields: {
        //            withCredentials: true // Include cookies for session
        //        },
        //        dataType: "json",
        //        success: function (response) {
        //            if (response.d.success) {
        //                console.log('Job status updated successfully');
        //            } else {
        //                if (response.d.message === "Unauthorized access. Please log in.") {
        //                    // Redirect to login or show login modal
        //                    window.location.href = "AdminLogin.aspx";
        //                } else {
        //                    alert(response.d.message);
        //                }
        //            }
        //        },
        //        error: function (xhr, status, error) {
        //            if (xhr.status === 401) {
        //                // Handle unauthorized access
        //                console.error('Error 401:', error);
        //                alert('Session expired. Please log in again.' + error);
        //                window.location.href = "AdminLogin.aspx"; // Redirect to login page
        //            } else {
        //                console.error('Error:', error);
        //            }
        //        }
        //    });

        //    // Change the toggle switch state immediately
        //    if (status === 'APPROVED') {
        //        currentToggleElement.checked = true;  // Set the switch to ON
        //    } else if (status === 'REJECTED') {
        //        currentToggleElement.checked = false; // Set the switch to OFF
        //    }
        //}


        $(document).ready(function () {
            $('.searchable-dropdown').select2({
                placeholder: 'Select an option',
                allowClear: true
            });
        });
       <%--<%-- $(document).ready(function () {
            $('.select2').select2();
            // Initialize Select2 for the company dropdown
           <%-- $('#<%= ddlCompanyName.ClientID %>').select2({
               placeholder: 'Select a company',
               allowClear: true
           });
       });--%>
        function myFunction() {
            document.getElementById("myDropdown").classList.toggle("show");
        }

        function filterFunction() {
            const input = document.getElementById("myInput");
            const filter = input.value.toUpperCase();
            const div = document.getElementById("myDropdown");
            const a = div.getElementsByTagName("a");
            for (let i = 0; i < a.length; i++) {
                txtValue = a[i].textContent || a[i].innerText;
                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    a[i].style.display = "";
                } else {
                    a[i].style.display = "none";
                }
            }
        }

 
        // Function to filter dropdown items based on the search input
        function filterFunction() {
            var input, filter, a, i;
            input = document.getElementById('<%= txtSearch.ClientID %>');
            filter = input.value.toUpperCase();
            a = document.getElementsByClassName("item");

            for (i = 0; i < a.length; i++) {
                if (a[i].innerHTML.toUpperCase().indexOf(filter) > -1) {
                    a[i].style.display = "";
                } else {
                    a[i].style.display = "none";
                }
            }
        }

        // Close the dropdown if the user clicks outside of it
        window.onclick = function (event) {
            if (!event.target.matches('.form-control')) {
                var dropdowns = document.getElementsByClassName("dropdown-content");
                for (var i = 0; i < dropdowns.length; i++) {
                    dropdowns[i].style.display = "none";
                }
            }
        };
    </script>
</asp:Content>
