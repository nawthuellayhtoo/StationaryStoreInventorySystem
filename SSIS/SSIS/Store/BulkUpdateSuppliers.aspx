<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="BulkUpdateSuppliers.aspx.cs" Inherits="SSIS.BulkUpdateSuppliers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Bulk Update Suppliers
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
        <script type="text/javascript">
        $(function () {
            $('[id*=GridView1]').footable();
        });
    </script>
<div class="juambotron">
         <br />
        <center>
                <h2>
                    Bulk Update Suppliers
                </h2>
            </center>
        <br />
        <table width="25%" align="center" class="table-responsive">
            <tr>
                <td>
                    <asp:FileUpload ID="fuSuppliers" runat="server" /></td>
                <td>
                    <asp:Button ID="btnUpdate" class="btn btn-default btn-md" runat="server" Text="Update" OnClick="btnUpdate_Click" /></td>              
            </tr>
        </table>
   <center><asp:Label ID="lblStatus" runat="server"></asp:Label><br />
       <asp:Label ID="Label1" runat="server"></asp:Label> 
   </center> 
        <asp:GridView ID="GridView1" runat="server" horizontalalign="Center" CssClass="footable" AutoGenerateColumns="false" AllowPaging ="true" PageSize="7" OnPageIndexChanging="GridView1_PageIndexChanging">
             <Columns>
                <asp:BoundField DataField="ItemNumber" HeaderText="Item #" />
                <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                <asp:BoundField DataField="ItemCategory" HeaderText="Category" />
                <asp:BoundField DataField="Supplier1" HeaderText="Supplier1" />
                <asp:BoundField DataField="Supplier2" HeaderText="Supplier2" />
                <asp:BoundField DataField="Supplier3" HeaderText="Supplier3" />
                <asp:BoundField DataField="Price1" HeaderText="Price1 $" />
                <asp:BoundField DataField="Price2" HeaderText="Price2 $" />
                <asp:BoundField DataField="Price3" HeaderText="Price3 $" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>