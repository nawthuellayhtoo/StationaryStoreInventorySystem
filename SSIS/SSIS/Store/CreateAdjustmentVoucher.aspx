<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="CreateAdjustmentVoucher.aspx.cs" Inherits="SSIS.CreateAdjustmentVoucher" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Create Adjustment Voucher
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <script type="text/javascript">
        $(function () {
            $('[id*=gvInventoryAdjustmentList]').footable();
        });
    </script>
    <div class="juambotron">
        <center><h2>Create Adjustment Voucher</h2></center>
        <br />
        <table width="50%" align="center" style="border-collapse: separate; border-spacing: 10px;">
            <tr>
                <td>
                    <asp:Label ID="lbCategory" runat="server" Text="Category:"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlCategory" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbItemName" runat="server" Text="Item Name:"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlItemName" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 28px">
                    <asp:Label ID="lbUOM" runat="server" Text="UOM:"></asp:Label></td>
                <td style="height: 28px">
                    <asp:TextBox ID="tbUOM" runat="server" Enabled="False">Box</asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbSupplier" runat="server" Text="Supplier:"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlSupplier" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="height: 28px">
                    <asp:Label ID="lbQuantity" runat="server" Text="Quantity:"></asp:Label></td>
                <td style="height: 28px">
                    <asp:TextBox ID="tbQuantity" TextMode="Number" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbReason" runat="server" Text="Reason:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="tbReason" runat="server" MaxLength="250" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnAdd" CssClass="btn btn-default btn-md" runat="server" Text="Add" OnClick="btnAdd_Click" />
                </td>
            </tr>
            <tr>
                <td>
        <asp:RequiredFieldValidator 
            ID="rfvReason" 
            ControlToValidate="tbReason" 
            Style="color: Red" 
            runat="server"
            Display="None"
            ErrorMessage="Please write reason.">
        </asp:RequiredFieldValidator>
                </td>
                <td><asp:ValidationSummary ID="validationSummary" forecolor="Red" runat="server" /></td>             
            </tr>
        </table>
        <%--Validator--%>
        <asp:RequiredFieldValidator 
            ID="rfvCategory" 
            ControlToValidate="ddlCategory" 
            Style="color: Red" 
            InitialValue="NA" 
            runat="server"
            Display="None" 
            ErrorMessage="Please select a Category.">
        </asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator 
            ID="rfvItemName" 
            ControlToValidate="ddlItemName" 
            Style="color: Red" 
            InitialValue="NA" 
            runat="server"
            Display="None" 
            ErrorMessage="Please select an Item.">
        </asp:RequiredFieldValidator>
        <asp:RangeValidator 
            ID="rvQuantity" 
            Style="color: Red" 
            Type="Integer"
            MinimumValue="-10000" 
            MaximumValue="10000" 
            ControlToValidate="tbQuantity"
            Display="None" 
            runat="server" 
            ErrorMessage="Please input value between -10000 to 10000.">
        </asp:RangeValidator><br />
        <asp:CompareValidator 
            ID="cvQuantity" 
            ControlToValidate="tbQuantity"
            runat="server" 
            Operator="NotEqual" 
            Type="Integer" 
            ValueToCompare="0"
            Display="None" 
            ErrorMessage="Quantity cannot be 0." 
            Style="color: Red">
        </asp:CompareValidator><br />
        <asp:RequiredFieldValidator 
            ID="rfvQuantity" 
            ControlToValidate="tbQuantity"
            runat="server" 
            Style="color: Red" 
            Display="None" 
            ErrorMessage="Please enter Quantity to adjust.">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator 
            ID="revReason" 
            Style="color: Red"
            Display="None" 
            ControlToValidate="tbReason"
            runat="server" 
            ValidationExpression="^-?[a-zA-Z0-9'@&#$!*()+=.^\w\s]{0,150}$" 
            ErrorMessage="Please enter not more than 150 characters.">
        </asp:RegularExpressionValidator>
        <%--Validator--%>
        <asp:GridView ID="gvInventoryAdjustmentList" EmptyDataText="" ShowHeaderWhenEmpty="false" CssClass="footable" AutoGenerateColumns="false" runat="server" OnRowCommand="gvInventoryAdjustmentList_RowCommand">
            <Columns>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lbPrice" runat="server" Text='<%# Eval("Price")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lbItemNumber" runat="server" Text='<%# Eval("ItemNumber")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                <asp:BoundField DataField="SupplierId" HeaderText="Supplier" />
                <asp:BoundField DataField="QuantityAdjustment" HeaderText="Quantity" />
                <asp:BoundField DataField="Uom" HeaderText="UOM" />
                <asp:BoundField DataField="Reason" HeaderText="Reason" />
                <asp:ButtonField CommandName="Remove" ButtonType="Button" Text="Delete" />
            </Columns>
        </asp:GridView>

            <div align="center">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-default btn-md" runat="server" CausesValidation="false" Text="Submit" OnClick="btnSubmit_Click"/>
            </div>
    </div>
</asp:Content>