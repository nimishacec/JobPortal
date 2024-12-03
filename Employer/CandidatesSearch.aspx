<%@ Page Title="" Language="C#" MasterPageFile="~/Employer/Employer.Master" AutoEventWireup="true" CodeBehind="CandidatesSearch.aspx.cs" Inherits="JobPortalWebApplication.Employer.CandidatesSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
     
    <h3>Search Candidates</h3>
 <div class="form-group">
     <label for="jobTitle">Job Title</label>
     <asp:DropDownList ID="ddlJobTitle" runat="server" CssClass="form-control" ></asp:DropDownList>
     <asp:TextBox ID="txtOtherJobTitle" runat="server" CssClass="form-control" Placeholder="Enter custom job title" Visible="False" />
 </div>

 <div class="form-group">
     <label for="location">Location</label>
     <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="form-control">
     </asp:DropDownList>
 </div>
 <div class="form-group">
     <label for="skills">Skills</label>
     <asp:TextBox ID="txtSkills" runat="server" CssClass="form-control" Placeholder="Enter skills"></asp:TextBox>
 </div>
 <div class="form-group">
     <label for="experience">Experience</label>
     <asp:DropDownList ID="ddlExperience" runat="server" CssClass="form-control">
         <asp:ListItem Text="Select Experience" Value="" />
         <asp:ListItem Text="0-1 years" Value="between 0 and 1" />
         <asp:ListItem Text="1-3 years" Value="between 1 and 3" />
         <asp:ListItem Text="3-5 years" Value="between 3 and 5" />
         <asp:ListItem Text="5+ years" Value="more than 5" />
     </asp:DropDownList>
 </div>
 <div class="form-group">
     <label for="education">Education</label>
     <asp:TextBox ID="txtEducation" runat="server" CssClass="form-control" Placeholder="Enter Qualifications"></asp:TextBox>
 </div>
                            <div class="sorting-controls">
                            <asp:Label ID="lblSortBy" Text="SortBy" runat="server"></asp:Label><br />
    <asp:DropDownList ID="ddlSortBy" runat="server" AutoPostBack="True"  CssClass="form-control">
        <asp:ListItem Text="Relevance" Value="Relevance" ></asp:ListItem>
        <asp:ListItem Text="Salary" Value="Salary"></asp:ListItem>
        <asp:ListItem Text="Location" Value="Location"></asp:ListItem>
    </asp:DropDownList>

    <asp:RadioButtonList ID="rblSortOrder" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"  CssClass="form-control">
        <asp:ListItem Text="Ascending" Value="ASC" Selected="True"></asp:ListItem>
        <asp:ListItem Text="Descending" Value="DESC" ></asp:ListItem>
    </asp:RadioButtonList>
</div>
    <div>
        <asp:Button ID="btnclick" runat="server"  Text="Search" CssClass="form-group btn-primary" OnClick="btnSearchCandidates_Click" />
    </div>
                     <div style="text-align:right">   <asp:Button ID="Button2" runat="server" Text="Back" OnClientClick="window.history.back(); return false;"  CssClass="btn btn-link"/>
</div>
</asp:Content>
