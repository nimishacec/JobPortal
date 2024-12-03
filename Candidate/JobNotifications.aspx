<%@ Page Title="" Language="C#" MasterPageFile="~/Candidate/Candidate.Master" AutoEventWireup="true" CodeBehind="JobNotifications.aspx.cs" Inherits="JobPortalWebApplication.Candidate.JobNotifications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="container" style="margin-top:30px">  
        <h3 style="text-align:center"> New Job Notifications</h3>
    <asp:Repeater ID="notificationsRepeater" runat="server">
    <HeaderTemplate>
        <table class="table table-striped">
            <thead>
                <tr>
                    <th>Job Title</th>
                    <th>CompanyName</th>
                    <th>Location</th>
                    <th>Salary</th>
                </tr>
            </thead>
            <tbody>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td>
                <asp:HyperLink ID="jobTitleLink" runat="server" NavigateUrl='<%# Eval("JobID", "JobDetails.aspx?JobID={0}") %>'>   
           
                    <%# Eval("JobTitle") %></td>
                    <td> <%#  Eval("CompanyName") %></td>
                    <td><%# Eval("JobLocation") %></td>
                  <td> <%#  Eval("Salary") %></td>
              
            </asp:HyperLink>
          
        </tr>
    </ItemTemplate>
    <FooterTemplate>
            </tbody>
        </table>
    </FooterTemplate>
</asp:Repeater>
</div>
    <%--<asp:GridView ID="gvJobAlerts" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
    <Columns>
        <asp:BoundField DataField="JobTitle" HeaderText="Job Title" />
        <asp:BoundField DataField="Location" HeaderText="Location" />
        <asp:BoundField DataField="Industry" HeaderText="Industry" />
        <asp:BoundField DataField="SalaryRange" HeaderText="Salary Range" />
        <asp:BoundField DataField="PostedDate" HeaderText="Posted Date" DataFormatString="{0:MM/dd/yyyy}" />
        <asp:HyperLinkField DataNavigateUrlFields="JobID" DataNavigateUrlFormatString="~/JobDetails.aspx?JobID={0}" Text="View Details" />
    </Columns>
</asp:GridView>--%>

</asp:Content>
