<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="POList.aspx.cs" Inherits="SSIS.POList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Purchase Order List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
        <script type="text/javascript">
        $(function () {
            $('[id*=gvPurchaseOrderList]').footable();
        });
    </script>
<div class="juambotron">
    <center><h2>Purchase Order List</h2>
     <asp:Label ID="lblNoPOList" runat="server"></asp:Label>
    </center>
    <br />
    <div align="center">
          <br />
   <asp:GridView ID="gvPurchaseOrderList" runat="server" Width="80%"  horizontalalign="Center" CssClass="footable" AutoGenerateColumns="false" AllowPaging ="true" PageSize="5" OnPageIndexChanging="gvPurchaseOrderList_PageIndexChanging">
       <Columns>
           <asp:HyperLinkField HeaderText="Purchase Order ID" DataTextField="POID" DataNavigateUrlFields="poId" DataNavigateUrlFormatString="~/Store/PODetails.aspx?poId={0}"/>
           <asp:BoundField DataField="DeliveryDate" HeaderText="Delivery Date" DataFormatString="{0:d}" />
           <asp:BoundField DataField="DateOfOrder" HeaderText="Date Of Order" DataFormatString="{0:d}" />
           <asp:BoundField DataField="Status" HeaderText="Status"/>
           <asp:BoundField DataField="ApprovalDate" HeaderText="Date Of Approval/Rejection" DataFormatString="{0:d}" />       
       </Columns>
 </asp:GridView>
    </div>
</div>
</asp:Content>