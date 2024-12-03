<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CandidateSearchResults.aspx.cs" MasterPageFile="~/Employer/Employer.Master" Inherits="JobPortalWebApplication.Employer.CandidateSearchResults" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <div style="display: flex; justify-content: center;">
    <div style="width: 100%;"> <!-- Adjust width as needed -->
    <div class="table table-bordered" style="align-content:center">
  <asp:GridView ID="gvCandidates" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="3" 
    OnPageIndexChanging="gvCandidates_PageIndexChanging" CssClass="grid-view-vertical">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <table class="table table-striped">
                    <tr>
                        <td><strong>Candidate Name:</strong></td>
                        <td><%# Eval("CandidateName") %></td>
                    </tr>
                    <tr>
                        <td><strong>Email:</strong></td>
                        <td><%# Eval("EmailAddress") %></td>
                    </tr>
                    <tr>
                        <td><strong>Phone:</strong></td>
                        <td><%# Eval("MobileNumber") %></td>
                    </tr>
                    <tr>
                        <td><strong>Skills:</strong></td>
                        <td><%# Eval("Skills") %></td>
                    </tr>
                    <tr>
                        <td><strong>Highest Education Level:</strong></td>
                        <td><%# Eval("HighestEducationLevel") %></td>
                    </tr>
                    <!-- Add more rows here for additional fields -->
                </table>
                <hr />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
         </div> </div>
        </div>
                 <div>   <asp:Button ID="Button2" runat="server" Text="Back" OnClientClick="window.history.back(); return false;"  CssClass="btn btn-link"/>
</div>
</asp:Content>









