<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchCandidate.aspx.cs"   MasterPageFile="~/Employer/Employer.Master" Inherits="JobPortalWebApplication.Employer.SearchCandidate" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

<style>
    
</style>
    </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    
      <div class="container col-md-6 mt-4"  style="background-color:aliceblue">
    <div class="row mb-3">
        <div class="col-md-3 mt-3">
            <asp:Label ID="lblSkill" runat="server" CssClass="form-label">Core Skills</asp:Label>
        </div>
        <div class="col-md-6 mt-3">
            <asp:DropDownList ID="ddlCoreSkill" runat="server" DataTextField="CoreSkills" DataValueField="Id" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>

    <div class="row mb-3">
        <div class="col-md-3">
            <asp:Label ID="Label1" runat="server" CssClass="form-label">Soft Skills</asp:Label>
        </div>
        <div class="col-md-6">
            <asp:DropDownList ID="ddlSoftSkill" runat="server" DataTextField="SoftSkills" DataValueField="Id" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>

    <div class="row mb-3">
        <div class="col-md-3">
            <asp:Label ID="Label2" runat="server" CssClass="form-label">Min Skill Percentage</asp:Label>
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="txtminskillpercent" runat="server" PlaceHolder="Min Skill Percentage" CssClass="form-control"></asp:TextBox>
             <asp:RequiredFieldValidator ID="rfvtxtminskillpercent" runat="server" ControlToValidate="txtminskillpercent" ErrorMessage="Min Skill Percentage is required." CssClass="text-danger" />
 <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtmaxskillpercent" MinimumValue="0" MaximumValue="100" Type="Integer" ErrorMessage="Min Skill Percentage must be between 0 and 100." CssClass="text-danger" />
        
        </div>
    </div>

    <div class="row mb-3">
        <div class="col-md-3">
            <asp:Label ID="Label3" runat="server" CssClass="form-label">Max Skill Percentage</asp:Label>
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="txtmaxskillpercent" runat="server" PlaceHolder="Max Skill Percentage" CssClass="form-control" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvmaxskillpercent" runat="server" ControlToValidate="txtmaxskillpercent" ErrorMessage="Max Skill Percentage is required." CssClass="text-danger" />
            <asp:RangeValidator ID="rvMinSkillPercent" runat="server" ControlToValidate="txtmaxskillpercent" MinimumValue="0" MaximumValue="100" Type="Integer" ErrorMessage="Max Skill Percentage must be between 0 and 100." CssClass="text-danger" />
        </div>
    </div>

    <div class="row">
        <div class="col-md-6 text-center">
            <asp:Button ID="Button1" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" />
        </div>
    </div>
</div>
                 <div  style="text-align:right">   <asp:Button ID="Button2" runat="server" Text="Back" OnClientClick="window.history.back(); return false;"  CssClass="btn btn-link"/>
</div>
    
</asp:Content>
