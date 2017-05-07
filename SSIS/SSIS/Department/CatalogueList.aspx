<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="CatalogueList.aspx.cs" Inherits="SSIS.CatalogueList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Catalogue List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
      <script type="text/javascript">
        $(function () {
            $('[id*=GridViewcatalogueList]').footable();
        });
    </script>
    <div class="juambotron">
        <br />
        <center><h2>Catalogue List</h2>
         <asp:Label ID="lblNoCatalogueList" runat="server"></asp:Label>
        </center>
        <asp:GridView ID="GridViewcatalogueList" AutoGenerateColumns="false" horizontalalign="Center" CssClass="footable" runat="server" AllowPaging ="true" PageSize="10" width="80%" OnPageIndexChanging="GridViewcatalogueList_PageIndexChanging">
            <Columns>
                   <asp:BoundField DataField="itemName" HeaderText="item Name" ItemStyle-Width="26.67%"/>
                   <asp:BoundField DataField="itemCategory"  HeaderText="item Catalogue"  ItemStyle-Width="26.67%"/>
                   <asp:BoundField DataField="itemUOM" HeaderText="Unit of Measure" ItemStyle-Width="26.67%"/>
            </Columns>
        </asp:GridView>
        <br />
    </div>
</asp:Content>