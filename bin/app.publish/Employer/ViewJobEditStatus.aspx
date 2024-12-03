<%@ Page Title="" Language="C#" MasterPageFile="~/Employer/Employer.Master" AutoEventWireup="true" CodeBehind="ViewJobEditStatus.aspx.cs" Inherits="JobPortalWebApplication.Employer.ViewJobEditStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .status-approved {
    background-color: green;
    color: white;
}

.status-rejected {
    background-color: red;
    color: white;
}

.status-pending {
    background-color: yellow;
    color: black;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
 <div style="background-color:aliceblue">
    <h3 style="text-align: center"> Job Edit Status</h3>

 <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" EmptyDataText="No records to display..." AutoGenerateColumns="False" AllowPaging="true" PageSize="5"
    OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="JobID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" >
    <Columns>
        <asp:BoundField DataField="SLNo" HeaderText="SLNo">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="JobID" HeaderText="JobID">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="JobTitle" HeaderText="JobTitle">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="JobDescription" HeaderText="JobDescription">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
      
       
       
       <asp:TemplateField HeaderText="Request Status">
    <ItemStyle HorizontalAlign="Center" />
    <ItemTemplate>
        <asp:Label ID="lblRequestStatus" runat="server" 
            Text='<%# Eval("RequestStatus") %>'
            CssClass='<%# GetStatusCssClass(Eval("RequestStatus").ToString()) %>'>
        </asp:Label>
    </ItemTemplate>
</asp:TemplateField>      
        
       

    </Columns>

    <HeaderStyle BackColor="BlueViolet" ForeColor="White" />
</asp:GridView>
     </div>
            
   
</asp:Content>
