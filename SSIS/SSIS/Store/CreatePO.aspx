<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="CreatePO.aspx.cs" Inherits="SSIS.CreatePO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Create Purchase Order
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <script type="text/javascript">
        $(function () {
            $('[id*=gvCreatePurchaseOrderList]').footable();
        });
    </script>
    <div class="juambotron">
        <center><h2>Create Purchase Order</h2></center>
        <br />
        <table width="50%" align="center" style="border-collapse: separate; border-spacing: 10px;">
            <tr>
                <td>
                    <asp:Label ID="lbCategory" runat="server" Text="Category:"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlCategory" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbItemName" runat="server" Text="Item Name:"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlItemName" runat="server" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="height: 28px">
                    <asp:Label ID="lbUOM" runat="server" Text="UOM:"></asp:Label></td>
                <td style="height: 28px">
                    <asp:TextBox ID="tbUOM" runat="server" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbSupplier" runat="server" Text="Supplier:"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlSupplier" runat="server" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbPrice" runat="server" Text="Price:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="tbPrice" runat="server" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="height: 28px">
                    <asp:Label ID="lbQuantity" runat="server" Text="Quantity:"></asp:Label></td>
                <td style="height: 28px">
                    <asp:TextBox ID="tbQuantity" TextMode="Number" min="1" max="10000" step="1" runat="server" ValidationGroup="createValidation"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbQuantity" Display="Dynamic" ErrorMessage="*required" ForeColor="Red" ValidationGroup="createValidation"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbQuantity" Display="Dynamic" ErrorMessage="*Must be a number" ForeColor="Red" ValidationGroup="createValidation" Type="Integer"
                        Operator="DataTypeCheck"></asp:CompareValidator>
                </td>
            </tr>

            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnAdd" class="btn btn-default btn-md" runat="server" Text="Add" OnClick="btnAdd_Click" ValidationGroup="createValidation" /></td>
            </tr>
        </table>
        <br />
        <div align="center">
            <asp:GridView ID="gvCreatePurchaseOrderList" runat="server" AutoGenerateColumns="false" HorizontalAlign="Center" CssClass="footable" Width="80%" ViewStateMode="Enabled" OnRowDeleting="gvCreatePurchaseOrderList_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                    <asp:BoundField DataField="ItemUOM" HeaderText="UOM" />
                    <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
                    <asp:BoundField DataField="Price" HeaderText="Price" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:BoundField DataField="TotalPrice" HeaderText="Total Price" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <br />
        <table align="center" width="30%">
            <tr>
                <td>
                    <asp:Label ID="lbClerkComments" runat="server" Text="Comments : "></asp:Label></td>
                <td>
                    <textarea id="tbClerkComments" name="tbClerkComments"></textarea>
                </td>
                <tr>
                    <td>
                        <asp:Label ID="lbDeliveryDate" runat="server" Text="Expected Delivery Date:"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="tbDeliveryDate" TextMode="Date" runat="server" BackColor="White" ValidationGroup="deliveryDateValidation"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbDeliveryDate" ErrorMessage="*required" ForeColor="Red" ValidationGroup="deliveryDateValidation"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:CompareValidator
                        runat="server"
                        ID="compareValidatorStartToday"
                        ErrorMessage="Delivery date cannot be earlier than today"
                        ForeColor="Red"
                        ControlToValidate="tbDeliveryDate"
                        Type="date"
                        Operator="GreaterThanEqual"
                        ValidationGroup="deliveryDateValidation"
                        ValueToCompare="<%# DateTime.Today.ToShortDateString() %>" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnSubmit" class="btn btn-default btn-md" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="deliveryDateValidation" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>