<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AdjustmentVoucherList.aspx.cs" Inherits="SSIS.AdjustmentVoucherList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Adjustment Voucher List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <script type="text/javascript">
        $(function () {
            $('[id*=gvInventoryAdjustmentList]').footable();
        });
    </script>
    <br />
    <div class="juambotron">
        <center><h2>Adjustment Voucher List</h2>
<asp:Label ID="lblNoVoucher" runat="server"></asp:Label>
        </center>
        <br />
        <asp:GridView ID="gvInventoryAdjustmentList" EmptyDataText="" ShowHeaderWhenEmpty="false" CssClass="footable" AutoGenerateColumns="false
            " runat="server" AllowPaging ="true" PageSize="5" OnPageIndexChanging="gvInventoryAdjustmentList_PageIndexChanging">
            <Columns>
                <asp:HyperLinkField HeaderText="Voucher ID" DataTextField="VoucherId" DataNavigateUrlFields="VoucherId" DataNavigateUrlFormatString="~/Store/AdjustmentVoucherDetails.aspx?VoucherId={0}"/>
                <asp:BoundField DataField="EmployeeName" HeaderText="Applier" />
                <asp:BoundField DataField="VoucherDate" HeaderText="Date" />
                <asp:BoundField DataField="TotalPrice" HeaderText="Cost($)" />
                <asp:BoundField DataField="AdjustmentStatus" HeaderText="Status" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
