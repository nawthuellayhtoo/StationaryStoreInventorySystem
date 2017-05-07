<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="CreateRequisition.aspx.cs" Inherits="SSIS.CreateRequisition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Create Requisition
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
          <script type="text/javascript">
        $(function () {
            $('[id*=GridViewCreateRequisition]').footable();
        });
    </script>
    <div class="juambotron">
        <br />
        <center><h2>Create Requisition</h2></center>
        <br />
        <table width="50%" align="center">
            <tr>
                <td>
                    <asp:Label align="right" ID="lbItemName" runat="server" Text="Item Name : "></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlItemName" runat="server" DataSourceID="SqlDataSource1" DataTextField="ItemName" DataValueField="ItemName">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SA43Team2StoreDBConnectionString %>" SelectCommand="SELECT [ItemName] FROM [InventoryStock]"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td style="height: 26px">
                    <asp:Label ID="lbQuantity" runat="server" Text="Quantity : "></asp:Label></td>
                <td style="height: 26px">
                    <asp:TextBox ID="tbQuantity" TextMode="Number" min="0" max="1000" step="1" runat="server" OnTextChanged="tbQuantity_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorForQuantity" runat="server" ControlToValidate="tbQuantity" ErrorMessage="Cannot be 0 lah!" ForeColor="Red"></asp:RequiredFieldValidator>
                    
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    
                    <asp:Label ID="LbStatus" runat="server" ForeColor="Red"></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnAdd" class="btn btn-default btn-md" runat="server" Text="Add" OnClick="btnAdd_Click" /></td>
            </tr>
        </table>

        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

        <br />

        <asp:GridView ID="GridViewCreateRequisition" OnRowDeleting="GridViewCreateRequisition_RowDeleting" width="80%" AutoGenerateColumns="false" horizontalalign="Center" CssClass="footable" runat="server">
            <Columns>
                <asp:BoundField DataField="updateItemName" HeaderText="Item Name" ItemStyle-Width="26.67%">
                    <ItemStyle Width="26.67%"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="updateQuantity"  HeaderText="Quantity"  ItemStyle-Width="26.67%">
                    <ItemStyle Width="26.67%"></ItemStyle>
                </asp:BoundField>
                <asp:CommandField ShowDeleteButton="True" ButtonType="Button"  ItemStyle-Width="26.67%"/>               
            </Columns>
        </asp:GridView>
        <table width="50%" align="center">
            <tr>
                <td>
                    <asp:Label ID="lbEmployeeComments" runat="server" Text="Comments:"></asp:Label></td>
                <td>
                    <textarea id="tbEmployeeComments" name="tbEmployeeComments"></textarea></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="BtbSubmit" class="btn btn-default btn-md" runat="server" Text="Submit" OnClick="BtbSubmit_Click" />
                    <asp:Label ID="LabelSubmit" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>