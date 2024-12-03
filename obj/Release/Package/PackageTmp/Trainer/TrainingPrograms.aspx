<%@ Page Title="" Language="C#" MasterPageFile="~/Trainer/Trainer.Master" AutoEventWireup="true" CodeBehind="TrainingPrograms.aspx.cs" Inherits="JobPortalWebApplication.Trainer.TrainingPrograms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
   
        <div class="container">
            <h2>Training Program</h2>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" />

            <div class="form-group">
                <label for="txtProgramName">Program Name:</label>
                <asp:TextBox ID="txtProgramName" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvProgramName" runat="server" ControlToValidate="txtProgramName" ErrorMessage="Program Name is required" CssClass="text-danger" />
            </div>

            <div class="form-group">
                <label for="txtDescription">Description:</label>
                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
            </div>

            <div class="form-group">
                <label for="txtStartDate">Start Date:</label>
                <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" TextMode="Date"/>
                <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate" ErrorMessage="Start Date is required" CssClass="text-danger" />
                <asp:RegularExpressionValidator ID="revStartDate" runat="server" ControlToValidate="txtStartDate" ErrorMessage="Invalid date format" ValidationExpression="\d{4}-\d{2}-\d{2}" CssClass="text-danger" />
            </div>

            <div class="form-group">
                <label for="txtEndDate">End Date:</label>
                <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" TextMode="Date" />
                <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ControlToValidate="txtEndDate" ErrorMessage="End Date is required" CssClass="text-danger" />
                <asp:RegularExpressionValidator ID="revEndDate" runat="server" ControlToValidate="txtEndDate" ErrorMessage="Invalid date format" ValidationExpression="\d{4}-\d{2}-\d{2}" CssClass="text-danger" />
            </div>

            <div class="form-group">
                <label for="ddlDeliveryMode">Delivery Mode:</label>
                <asp:DropDownList ID="ddlDeliveryMode" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Select Delivery Mode" Value="" />
                    <asp:ListItem Text="Online" Value="Online" />
                    <asp:ListItem Text="In-Person" Value="In-Person" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvDeliveryMode" runat="server" ControlToValidate="ddlDeliveryMode" InitialValue="" ErrorMessage="Delivery Mode is required" CssClass="text-danger" />
            </div>

            <div class="form-group">
                <label for="txtPrice">Price:</label>
                <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ControlToValidate="txtPrice" ErrorMessage="Price is required" CssClass="text-danger" />
                <asp:RegularExpressionValidator ID="revPrice" runat="server" ControlToValidate="txtPrice" ErrorMessage="Invalid price format" ValidationExpression="^\d+(\.\d{1,2})?$" CssClass="text-danger" />
            </div>

            <div class="form-group">
                <asp:Button ID="btnSubmit" runat="server" Text="Add Program" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
            </div>
        </div>
    

</asp:Content>
