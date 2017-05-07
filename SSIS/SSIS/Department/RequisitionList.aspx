<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="RequisitionList.aspx.cs" Inherits="SSIS.RequisitionList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Requisition List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
<script type="text/javascript">
        $(function () {
            $('[id*=GridViewRequisitionList]').footable();
        });
</script>
  <div class="juambotron">
        <center><h2>Requisition List</h2>
            <asp:Label ID="LbNoRequisition" runat="server"></asp:Label>
        </center>
        <br />
        <asp:GridView ID="GridViewRequisitionList"  AutoGenerateColumns="false" horizontalalign="Center" CssClass="footable" runat="server" AllowPaging ="true" PageSize="5" width="80%" OnPageIndexChanging="GridViewRequisitionList_PageIndexChanging">
               <Columns>
                   <asp:HyperLinkField HeaderText="Requisition ID" DataTextField="retrievalrequisitionId" DataNavigateUrlFields="retrievalrequisitionId" DataNavigateUrlFormatString="../Department/RequisitionDetails.aspx?retrievalrequisitionId={0}" ItemStyle-Width="20%"/>
                   <asp:BoundField DataField="retrievalempName" HeaderText="Employee Name" ItemStyle-Width="20%"/>
                   <asp:BoundField DataField="retrievalempRequisitionDate"  HeaderText="Employee Requisition Date"  ItemStyle-Width="20%"/>
                   <asp:BoundField DataField="retrievalstatus" HeaderText="Status" ItemStyle-Width="20%"/>
                </Columns>
        </asp:GridView>
    </div>
</asp:Content>