<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin1.Master" AutoEventWireup="true" CodeBehind="ApplicationApproval.aspx.cs" Inherits="JobPortalWebApplication.Admin.ApplicationApproval" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js">

    </script>
    <style type="text/css">
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
    border-radius: 34px;  /* Rounded corners */
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
    border-radius: 50%;  /* Rounded knob */
}

input:checked + .slider {
   /* background-color: #4CAF50;*/ /* Green background when ON */
}

input:checked + .slider:before {
    transform: translateX(26px);  /* Move the knob to the right */
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12 grid-margin stretch-card">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Application List</h4>
                    <p class="card-description">
                    </p>
                    <div class="row flex-grow-1 d-flex justify-content-center align-items-center">
                        <div class="col-12">
                            <div class="row jobs-head">
                                <div class="col-md-1 d-none d-sm-block">SlNo</div>
                                <div class="col-md-2 d-none d-sm-block">Application Code</div>
                                <%--  <div class="col-md-2 d-none d-sm-block">Candidate Name</div>      --%>
                                <div class="col-md-2 d-none d-sm-block">Job Title</div>
                                <div class="col-md-2 d-none d-sm-block">Company Name</div>
                                <%-- <div class="col-md-2 d-none d-sm-block">Phone Number</div>--%>
                                <%--    <div class="col-md-2 d-none d-sm-block">Application Date</div>--%>
                                <div class="col-md-2 d-none d-sm-block">Application Status</div>
                                   <div class="col-md-1 d-none d-sm-block">Verify </div>
                                <div class="col-md-1 d-none d-sm-block">Action</div>
                            </div>
                            <div class="jobs-content">
                                <asp:Repeater ID="rptJobs" runat="server">
                                    <ItemTemplate>
                                        <div class="row job-row" data-jobid='<%# Eval("JobID") %>'>
                                            <div class="col-md-1 column">
                                                <b class="d-inline d-sm-none">SlNo: </b><%# ((currentPage - 1) * pageSize) + Container.ItemIndex + 1 %>
                                            </div>
                                            <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">Application Code: </b><%# Eval("ApplicationCode") %>
                                            </div>
                                            <%-- <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">Candidate Name: </b><%# Eval("CandidateName") %>
                                            </div>--%>
                                            <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">Job Title: </b><%# Eval("JobTitle") %>
                                            </div>
                                            <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">Company Name: </b><%# Eval("CompanyName") %>
                                            </div>
                                            <%--  <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">Phone Number: </b><%# Eval("CandidatePhoneNumber") %>
                                            </div>--%>
                                            <%--  <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">Application Date: </b><%# Eval("ApplicationDate") %>
                                            </div>--%>
                                            <div class="col-md-2 column">
                                                <b class="d-inline d-sm-none">Application Status: </b><%# 
                                            Eval("ApplicationStatus").ToString() == "REJECTED" 
                                            ? "<div class='progress'><div class='badge badge-danger' role='progressbar' style='width: 100%;' aria-valuenow='100' aria-valuemin='0' aria-valuemax='100'>Rejected</div></div>" 
                                            : Eval("ApplicationStatus").ToString() == "DISABLED" 
                                            ? "<div class='progress'><div class='badge badge-secondary disabled-progress' role='progressbar' style='width: 100%;' aria-valuenow='100' aria-valuemin='0' aria-valuemax='100'>Disabled</div></div>" 
                                            : Eval("ApplicationStatus").ToString() == "APPROVED" 
                                            ?  "<div class='progress'><div class='badge badge-primary' role='progressbar' style='width: 100%;' aria-valuenow='100' aria-valuemin='0' aria-valuemax='100'>Approved</div></div>" 
                                             :"<div class='progress'><div class='badge badge-warning' role='progressbar' style='width: 100%;' aria-valuenow='50' aria-valuemin='0' aria-valuemax='100'>In Progress</div></div>"
                             
                                                %>
                                            </div>
                                            <div class="col-md-1 column">
                                                <b class="d-inline d-sm-none">Disable: </b>
                                                <div class="switch-container">
    <!-- Assume the job status is fetched from the server, if job status is "approved" the toggle is checked -->
    <label class="switch">
        <input type="checkbox" class="toggle-btn" data-jobid='<%# Eval("ApplicationCode") %>' onchange="openConfirmation(this)" 
               id="toggleSwitch"  <%# Eval("ApplicationStatus").ToString() == "ENABLE" ? "checked" : "" %> /> <!-- Use 'checked' if job status is approved -->
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
                                                <%--<asp:LinkButton ID="btnDisable" runat="server" Text="Disable" CommandArgument='<%# Eval("ApplicationCode") %>' OnClick="btnDisable_Click" CssClass="btn btn-outline-danger btn-sm"></asp:LinkButton>--%>

                                            
                                            <div class="col-md-1 action-block d-none d-sm-block">
                                                <div class="dropdown">
                                                    <i class="mdi mdi-dots-vertical showdropdown"></i>
                                                    <div class="dropdown-menu" style="">
                                                        <a class="dropdown-item viewtask cursor" data-taskid="6" data-target="#viewTaskModal" data-toggle="modal" onclick="viewApplication('<%# Eval("ApplicationCode") %>');"><i class="bx bx-show me-1"></i>View</a>
                                                        <a class="dropdown-item" href="#"><i class="bx bx-edit-alt me-1"></i>Edit</a>
                                                        <a class="dropdown-item" href="javascript:void(0);" onclick="confirmDelete('<%# Eval("JobID") %>');"><i class="bx bx-trash me-1"></i>Delete</a>

                                                    </div>
                                                    <i class="mdi mdi-arrow-down showdetails"></i>
                                                </div>
                                            </div>

                                            <div id="jobDetailsSection" class="job-details">
                                                <div class="row">
                                                    <div class="col-md-4 column"><b>Candidate Name: </b><%# Eval("CandidateName") %></div>
                                                    <div class="col-md-4 column"><b>Contact Number</b>: <%# Eval("CandidatePhoneNumber") %></div>
                                                    <div class="col-md-4 column"><b>Application Date: </b><%# Eval("ApplicationDate") %></div>
                                                    <%--<div class="col-md-4 column"><b>Company Description</b>:<%# Eval("CompanyDescription")</div>
                                       <div class="col-md-4 column"><b>Company Size</b>: <%# Eval("CompanySize") %></div>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </div>
                        </div>
                    </div>
              
   

    <%--  <div class="text-right" style="margin-top: 10px;">
           <asp:Button ID="btnVerify" runat="server" Text="Verify"  CssClass="btn btn-primary" OnClick="btnVerify_Click" />
       
     
        </div>--%>



    <div class="pagination-controls">
        <asp:LinkButton ID="lnkPrevious" runat="server" Text="Previous" OnClick="lnkPrevious_Click" CssClass="btn btn-primary"></asp:LinkButton>
        <asp:Label ID="lblPageInfo" runat="server" CssClass="page-info"></asp:Label>
        <asp:LinkButton ID="lnkNext" runat="server" Text="Next" OnClick="lnkNext_Click" CssClass="btn btn-primary"></asp:LinkButton>
    </div>
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
        function viewApplication(applicationCode) {
            window.location.href = 'Applications.aspx?ApplicationCode=' +(applicationCode);
        }
        var currentJobID = null;
        var currentStatus = null;
        var currentToggleElement = null; // Store the toggle element to update after confirmation

        // Open custom confirmation box
        function openConfirmation(element) {
            currentJobID = element.getAttribute("data-jobid");
            currentToggleElement = element; // Save the reference to the current toggle switch
            currentStatus = element.checked ? 'ENABLE' : 'DISABLE';

            // Show confirmation box
            document.getElementById("confirmationBox").style.display = "block";
            document.getElementById("confirmationText").innerText = "Are you sure you want to " + currentStatus + " this job?";
        }

        // Close the confirmation box
        function closeConfirmation() {
            document.getElementById("confirmationBox").style.display = "none";
            // Reset the toggle button to the previous state if "No" is clicked
            currentToggleElement.checked = !currentToggleElement.checked;
        }

        // When "Yes" is clicked in the confirmation box
        function confirmStatusChange() {
            // Send the update to the server
            updateJobStatus(currentJobID, currentStatus);
            closeConfirmation(); // Close the confirmation box after updating
        }
        function updateJobStatus(jobID, status) {
            // Use a simple fetch API call to update the status without AJAX
            fetch(`ApplicationApproval.aspx?ApplicationCode=${jobID}&Action=${status}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    jobID: jobID,
                    action: status
                })
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        console.log('Job status updated successfully');
                        currentToggleElement.checked = (status === 'APPROVED');
                    } else {
                        alert('Failed to update job status. Please try again.');
                        console.log('Failed to update job status');
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                });
            if (status === 'APPROVED') {
                currentToggleElement.checked = true;  // Set the switch to ON
            } else if (status === 'REJECTED') {
                currentToggleElement.checked = false; // Set the switch to OFF
            }
        }
    </script>
</asp:Content>
<%--  <asp:Button ID="Button1" runat="server" Text="Verify" 
            CommandName="SelectApplication" CssClass="btn btn-primary" 
            CommandArgument='<%# Eval("ApplicationID") %>' 
            OnCommand="btnVerify_Command" />--%>
 
           
