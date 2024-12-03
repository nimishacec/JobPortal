 <%@ Page Title="" Language="C#" MasterPageFile="~/Employer/Employer.Master" AutoEventWireup="true" CodeBehind="ViewJobs.aspx.cs" Inherits="JobPortalWebApplication.Employer.ViewJobs"  EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


    
       <div class="main-panel">
      <div class="content-wrapper">
          <div class="card">
          <div class="card-body">
              <h4 class="card-title">Job Listings</h4>
             
    <div class="table-responsive">
    <asp:GridView ID="GridView1" runat="server" EmptyDataText="No records to display..." CssClass="table table-striped"
        AutoGenerateColumns="False" AllowPaging="True" PageSize="3" BorderWidth="0" OnRowCreated="JobGridView_RowCreated"
        OnPageIndexChanging="GridView1_PageIndexChanging" GridLines="None" >
        
        <Columns>
           <asp:TemplateField HeaderText="SlNo">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
            <asp:BoundField DataField="JobTitle" HeaderText="Job Title" />
            <asp:BoundField DataField="Vacancy" HeaderText="Vacancy" />
            <asp:BoundField DataField="JobLocation" HeaderText="Location" />
            <asp:BoundField DataField="JobType" HeaderText="Type" ControlStyle-CssClass="text-center"/>
            <asp:BoundField DataField="Qualifications" HeaderText="Qualifications" />
            <asp:BoundField DataField="Experience" HeaderText="Experience" />
            <asp:BoundField DataField="Salary" HeaderText="Salary" />
            <asp:TemplateField HeaderText="Application Deadline">
                <ItemTemplate>
                    <%# Convert.ToDateTime(Eval("ApplicationDeadLine")).ToString("dd MMMM yyyy") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Application Start Date">
                <ItemTemplate>
                    <%# Convert.ToDateTime(Eval("ApplicationStartDate")).ToString("dd MMMM yyyy") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:LinkButton ID="editIconBtn" runat="server" CommandName="EditJob" CommandArgument='<%# Eval("JobID") %>' OnClick="btnEdit_Click" CssClass="edit-icon-btn">
        <i class="mdi mdi-lead-pencil"></i>
    </asp:LinkButton>   </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <%--<HeaderStyle BackColor="DarkSlateBlue" ForeColor="AliceBlue" />
        <RowStyle BackColor="#f9f9f9" />--%>
    </asp:GridView>
        </div> </div> </div>
</asp:Content>
