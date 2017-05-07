<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="InventoryList.aspx.cs" Inherits="SSIS.InventoryList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Inventory List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <script type="text/javascript">
        $(function () {
            $('[id*=gvInventoryList]').footable();
        });
    </script>
   <div class="juambotron">
        <br />
        <center>
                <h2>
                    Inventory List
                </h2>
               <asp:Label ID="lblNoInventoryList" runat="server" ></asp:Label>
        </center>
        <br />
        <asp:GridView ID="gvInventoryList" horizontalalign="Center" CssClass="footable" runat="server" AutoGenerateColumns="false" AllowPaging ="true" PageSize="7" OnPageIndexChanging="gvInventoryList_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="Bin" HeaderText="Bin #" />
                <asp:BoundField DataField="ItemNumber" HeaderText="Item #" />
                <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                <asp:BoundField DataField="ItemCategory" HeaderText="Category" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="ItemUOM" HeaderText="UOM" />
                <asp:BoundField DataField="Price1" HeaderText="Price $" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>