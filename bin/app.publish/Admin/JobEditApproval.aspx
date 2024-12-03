<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin1.Master" AutoEventWireup="true" CodeBehind="JobEditApproval.aspx.cs" Inherits="JobPortalWebApplication.Admin.JobEditApproval" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
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

#myInput:focus {outline: 3px solid #ddd;}

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

.dropdown a:hover {background-color: #ddd;}

.show {display: block;}










        .select2-container .select2-selection--single{
    height:34px !important;
}
.select2-container--default .select2-selection--single{
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
    background-color: #4CAF50; /* Green background when ON */
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
                body {
    overflow-y: auto;
}

.table-responsive {
    min-height: 100%;
}
        @media only screen and (max-width: 600px) {
            .job-details {
                display: block !important;
            }
        /* Firefox lowers the opacity by default */

    
      

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="row">
        <div class="col-lg-12 grid-margin stretch-card">
            <div class="card">
              <div class="card-body">
     <h4 class="card-title"> Job Edit Requests</h4>
                   <p class="card-description">
                         <asp:Label ID="Label1" runat="server" CssClass="text-danger mt-2" />
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
                                               <%#  Eval("RequestStatus").ToString() == "REJECTED" 
? "<div class='progress'><div class='badge badge-danger' role='progressbar' style='width: 80%;' aria-valuenow='50' aria-valuemin='0' aria-valuemax='100'>Rejected</div></div>" 
  :  Eval("RequestStatus").ToString() == "APPROVED" ?
    "<div class='progress'><div class='badge badge-primary' role='progressbar' style='width: 80%;' aria-valuenow='50' aria-valuemin='0' aria-valuemax='50'>Approved</div></div>"   
    :"<div class='progress'><div class ='badge badge-warning' role='progressbar' style='width: 80%;' aria-valuenow='50' aria-valuemin='0' aria-valuemax='50'>In Progress</div></div>"
                                              
                                            %>

                                             <%--   <%# 
                                            Eval("RequestStatus").ToString() == "REJECTED" 
                                            ? "<div class='progress'><div class ='badge badge-danger' role='progressbar' style='width: 80%;' aria-valuenow='50' aria-valuemin='0' aria-valuemax='100'>Rejected</div></div>" 
                                              : Eval("RequestStatus").ToString() == "APPROVED" 
                                              ? "<div class='progress'><div class ='badge badge-success' role='progressbar' style='width: 80%;' aria-valuenow='50' aria-valuemin='0' aria-valuemax='50'>Approved</div></div>"                                       
                                            : DateTime.Now >Convert.ToDateTime(Eval("ApplicationDeadline"))
                                            ? "<div class='progress'><div class ='badge badge-primary' role='progressbar' style='width: 50%;' aria-valuenow='50' aria-valuemin='0' aria-valuemax='50'>Completed</div></div>" 
                                            : "<div class='progress'><div class ='badge badge-warning' role='progressbar' style='width: 50%;' aria-valuenow='50' aria-valuemin='0' aria-valuemax='50'>In Progress</div></div>"
                                                %>--%>
                                            </div>
                                          <%-- <asp:HiddenField ID="hfAntiForgeryToken" runat="server" />--%>


                                            <div class="col-md-1 column">
                                                <b class="d-inline d-sm-none">Verify: </b>
                                                <!-- Toggle Button with Labels -->
<div class="switch-container">
    <!-- Assume the job status is fetched from the server, if job status is "approved" the toggle is checked -->
    <label class="switch">
        <input type="checkbox" class="toggle-btn" data-jobid='<%# Eval("RequestID") %>' onchange="openConfirmation(this)" 
               id="toggleSwitch"  <%# Eval("RequestStatus").ToString() == "APPROVED" ? "checked" : "" %> /> <!-- Use 'checked' if job status is approved -->
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
       </div>                                        <%-- <div class="toggle-container">
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
                                                                onclick="viewJobs('<%# (Eval("RequestID").ToString()) %>'); return false;">
                                                                <i class="bx bx-show me-1"></i>View
                                                             </a>
                                                            <a class="dropdown-item" href="#" onclick="editJobs('<%# (Eval("RequestID").ToString()) %>');"><i class="bx bx-edit-alt me-1"></i>Edit</a>
                                                            <a class="dropdown-item" href="javascript:void(0);" onclick="confirmDelete('<%# Eval("RequestID") %>');">
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
    function confirmDelete(RequestID) {
        if (confirm("Are you sure you want to delete this Request?")) {
            
            window.location.href = "JobEditApproval.aspx?deleteID=" + RequestID;
        }
    }
    function viewJobs(RequestID) {
        window.location.href = 'JobVerification.aspx?RequestID=' + (RequestID);
    }
    function editJobs(RequestID) {
       
        window.location.href = 'EditJobs.aspx?RequestID=' + (RequestID);
    }

</script>

        
     
 <%-- <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" EmptyDataText="No records to display..." AutoGenerateColumns="False" AllowPaging="true" PageSize="5"
    OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="RequestID,EmployeeID,JobID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCommand="GridView1_RowCommand">
    <Columns>
      
        <asp:BoundField DataField="SLNo" HeaderText="SLNo">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
       
        <%--<asp:BoundField DataField="RequestID" HeaderText="Request ID">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
         <asp:BoundField DataField="EmployeeID" HeaderText="Employee ID">
     <ItemStyle HorizontalAlign="Center" />
 </asp:BoundField>
              <%-- <asp:BoundField DataField="JobID" HeaderText="Job ID">
    <ItemStyle HorizontalAlign="Center" />
</asp:BoundField>
       
        <asp:BoundField DataField="JobTitle" HeaderText="Job Title">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
      
        <asp:BoundField DataField="Vacancy" HeaderText="No Of Vacancy">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
      
       <%-- <asp:BoundField DataField="JobDescription" HeaderText="Job Description">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
       
        <asp:BoundField DataField="Qualifications" HeaderText="Qualifications">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
     
        <asp:BoundField DataField="Experience" HeaderText="Experience">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
     
       <%-- <asp:BoundField DataField="RequiredSkills" HeaderText="New Required Skills">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
   
        <asp:BoundField DataField="JobLocation" HeaderText="New Job Location">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
     
        <asp:BoundField DataField="Salary" HeaderText="Annual Salary(CTC): ">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
   
       <%-- <asp:BoundField DataField="CompanyName" HeaderText="New Company Name">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>-
       <%-- <asp:BoundField DataField="JobType" HeaderText="New Job Type">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
  <asp:BoundField DataField="Address" HeaderText="New Address">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
     <asp:BoundField DataField="ApplicationDeadline" HeaderText="Application Deadline">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
     <asp:BoundField DataField="ContactEmail" HeaderText="New Contact Email">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
       
        <asp:BoundField DataField="Website" HeaderText="New Website">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
       
        <asp:BoundField DataField="ApplicationStartDate" HeaderText="Application Start Date">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
       
        <asp:BoundField DataField="RequestStatus" HeaderText="Request Status">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        
        <%--<asp:BoundField DataField="RequestDate" HeaderText="Request Date">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
       
       <%-- <asp:BoundField DataField="Industry" HeaderText="New Industry">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
    
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnApprove" runat="server" Text="Verify" CommandName="Verify" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  CssClass="btn btn-primary" />
                 
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
        </div>
          </div> </div> </div> 
      <asp:Label ID="lblMessage" runat="server" CssClass="" ></asp:Label>
   
            <%--  <div style>   <asp:Button ID="Button2" runat="server" Text="Back" OnClientClick="window.history.back(); return false;"  CssClass="btn btn-link"/>
</div>
    --%>
</asp:Content>
