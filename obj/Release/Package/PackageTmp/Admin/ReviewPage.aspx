<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin1.Master" AutoEventWireup="true" CodeBehind="ReviewPage.aspx.cs" Inherits="JobPortalWebApplication.Admin.ReviewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="text-align:center;background-color:aliceblue">
        <div class="row">
           <div class="col-md-6">
                <h2>Job Details</h2>
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered"
                        EmptyDataText="No records to display..." AutoGenerateColumns="False" >
                        <Columns>
                            <asp:BoundField DataField="FieldName" HeaderText="Field Name">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FieldValue" HeaderText="Field Value">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
      
      
           <div class="col-md-6">
                <h2>Job Edit Requests</h2>
                <div class="table-responsive">
                    <asp:GridView ID="GridView2" runat="server" CssClass="table table-hover table-bordered"
                        AutoGenerateColumns="False" >
                        <Columns>
                            <asp:BoundField DataField="FieldName" HeaderText="Field Name">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FieldValue" HeaderText="Field Value">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                           
                        </Columns>
                    </asp:GridView>
                        </div>
              </div>
              </div>

            <asp:Button ID="btnApprove" runat="server" Text="Approve" CommandName="Verify" CssClass="btn btn-primary" OnClick="btnApprove_Click" />
<asp:Button ID="Button1" runat="server" Text="Reject" CommandName="Reject" CssClass="btn btn-primary" OnClick="btnReject_Click" />
  </div>
          <div>   <asp:Button ID="Button2" runat="server" Text="Back" OnClientClick="window.history.back(); return false;"  CssClass="btn btn-link"/>
</div>
</asp:Content>
