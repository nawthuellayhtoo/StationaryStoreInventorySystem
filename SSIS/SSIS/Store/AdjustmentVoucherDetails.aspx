<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AdjustmentVoucherDetails.aspx.cs" Inherits="SSIS.AdjustmentVoucherDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Adjustment Voucher Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
      <script type="text/javascript">
        $(function () {
            $('[id*=gvInventoryAdjustmentDetailsList]').footable();
        });
    </script>
    <div class="juambotron">
        <center>
                <h2>Adjustment Voucher Details: 
                <asp:Label ID="lbVoucherID" runat="server"></asp:Label></h2>
        <div class="content">
            <div class="row">
                <div class="col-xs-3 col-sm-3 col-md-3">&nbsp;</div>
                <div class="col-xs-3 col-sm-3 col-md-3">
                    <asp:Label ID="lbApplier" runat="server" Text="Applied By:"></asp:Label>
                    <asp:Label ID="lbApplierName" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:Label>
                </div>
                <div class="col-xs-3 col-sm-3 col-md-3">
                    <asp:Label ID="lbDate" runat="server" Text="Date:"></asp:Label>
                    <asp:Label ID="lbDateValue" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:Label>
                </div>
                <div class="col-xs-3 col-sm-3 col-md-3">&nbsp;</div>
            </div>
            <div class="row">
                <div class="col-xs-3 col-sm-3 col-md-3">&nbsp;</div>
                <div class="col-xs-3 col-sm-3 col-md-3">
                    <asp:Label ID="lbCost" runat="server" Text="Cost: $"></asp:Label>
                    <asp:Label ID="lbCostValue" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:Label>
                </div>
                <div class="col-xs-3 col-sm-3 col-md-3">
                    <asp:Label ID="lbStatus" runat="server" Text="Status:"></asp:Label>
                    <asp:Label ID="lbStatusValue" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:Label>
                </div>
                <div class="col-xs-3 col-sm-3 col-md-3">&nbsp;</div>
            </div>
        </div>
        </center>
          <br />
          <br />
        <asp:GridView 
            ID="gvInventoryAdjustmentDetailsList" 
            runat="server" 
            ShowHeaderWhenEmpty="false"
            CssClass="footable"
            AutoGenerateColumns="true" 
            AllowPaging="true" 
            PageSize="7">
        </asp:GridView>
        <br />
        <div align="center">
            <asp:Button ID="btnBack" class="btn btn-default btn-md" runat="server" Text="Back" OnClick="btnBack_Click" />
            <asp:Button ID="btnApprove" class="btn btn-default btn-md" runat="server" Text="Acknowledge" BackColor="#00CC00" OnClick="btnApprove_Click" />
        </div>
    </div>
</asp:Content>