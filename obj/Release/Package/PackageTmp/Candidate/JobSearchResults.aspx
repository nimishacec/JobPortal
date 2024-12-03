<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Candidate/Candidate.Master" CodeBehind="JobSearchResults.aspx.cs" Inherits="JobPortalWebApplication.Candidate.JobSearchResults" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container-fluid">
        <asp:GridView ID="gvJobResults" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="False" AllowPaging="true" PageSize="5"
            OnPageIndexChanging="gvJobResults_PageIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="Job">
                    <ItemTemplate>
                        <%# Eval("FieldName") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Details">
                    <ItemTemplate>
                        <%# Eval("FieldValue") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="text-right">
            <asp:Button ID="btnApply" runat="server" Text="Apply" CssClass="btn btn-primary" OnClick="btnApply_Click" />
        </div>
    </div>

</asp:Content>

