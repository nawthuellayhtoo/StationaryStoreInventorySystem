<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="PODetails.aspx.cs" Inherits="SSIS.PODetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Purchase Order Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
<script type="text/javascript">
        $(function () {
            $('[id*=gvPurchaseOrderList]').footable();
        });
</script>
     <div class="juambotron">
        <center>
        <h2>Purchase Order Number: <asp:Label ID="lblPOID" runat="server"></asp:Label></h2>
            <b>Applied By:</b> <asp:Label ID="lblName" runat="server"></asp:Label> &nbsp;&nbsp; <b>Date Of Order:</b> <asp:Label ID="lblOrderDate" runat="server"></asp:Label> &nbsp;&nbsp; <b>Status:</b> <asp:Label ID="lblStatus" runat="server"></asp:Label>
        </center>
        <br />
         <div align="center">

   <asp:GridView ID="gvPurchaseOrderList" runat="server"  CssClass="footable" Width="80%" OnRowEditing="gvPurchaseOrderList_RowEditing" OnRowCancelingEdit="gvPurchaseOrderList_RowCancelingEdit" OnRowUpdating="gvPurchaseOrderList_RowUpdating" OnRowDeleting="gvPurchaseOrderList_RowDeleting" horizontalalign="Center" AutoGenerateColumns="false">
       <Columns>         
           <asp:TemplateField HeaderText="Item Number">
               <EditItemTemplate>
                   <asp:Label ID="lblItemNumber" runat="server" Text='<%# Eval("ItemNumber") %>'></asp:Label>
               </EditItemTemplate>
               <ItemTemplate>
                   <asp:Label ID="lblItemNumber" runat="server" Text='<%# Bind("ItemNumber") %>'></asp:Label>  
               </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Item Name">
               <EditItemTemplate>
                   <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
               </EditItemTemplate>
               <ItemTemplate>
                   <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Supplier">
               <EditItemTemplate>
                   <asp:Label ID="lblSupplier" runat="server" Text='<%# Eval("Supplier") %>'></asp:Label>
               </EditItemTemplate>
               <ItemTemplate>
                   <asp:Label ID="lblSupplier" runat="server" Text='<%# Bind("Supplier") %>'></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Quantity">
               <EditItemTemplate>
                   <asp:TextBox ID="txtQuantity" TextMode="Number" runat ="server" Text='<%# Bind("Quantity") %>'></asp:TextBox>
               </EditItemTemplate>
               <ItemTemplate>
                   <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Unit Of Measure">
               <EditItemTemplate>
                   <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("ItemUOM") %>'></asp:Label>
               </EditItemTemplate>
               <ItemTemplate>
                   <asp:Label ID="lblUOM" runat="server" Text='<%# Bind("ItemUOM") %>'></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Unit Price ($)">
               <EditItemTemplate>
                   <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
               </EditItemTemplate>
               <ItemTemplate>
                   <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Total Price ($)">
               <EditItemTemplate>
                   <asp:Label ID="lblTotalPrice" runat="server" Text='<%# Eval("TotalPrice") %>'></asp:Label>
               </EditItemTemplate>
               <ItemTemplate>
                   <asp:Label ID="lblTotalPrice" runat="server" Text='<%# Bind("TotalPrice") %>'></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>
            <asp:TemplateField>  
                    <ItemTemplate>  
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" />  
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" OnClientClick = "Confirm()"/> 
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="Update"/>  
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel"/>  
                    </EditItemTemplate>  
                </asp:TemplateField>
       </Columns>

 </asp:GridView>
             <br />
    </div>
        <table width="20%" align="center">
            <tr>
                <td>
                    <asp:Label ID="lbClerkComments" runat="server" Text="Clerk Comments:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="tbClerkComments" runat="server" Height="60px" Width="250px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbSupervisorComments" runat="server" Text="Supervisor Comments:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="tbSupervisorComments" runat="server" Height="100px" Width="250px"></asp:TextBox></td>
            </tr>
        </table>
        <br />

        <table width="20%" align="center">
            <tr>
                <td>
                    <asp:Button ID="btnBack" class="btn btn-default btn-md" runat="server" Text="Back" OnClick="btnBack_Click" /></td>
                <td>
                    <asp:Button ID="btnResubmit" class="btn btn-default btn-md" runat="server" Text="Resubmit" OnClick="btnResubmit_Click" /></td>
                <td>
                    <asp:Button ID="btnReject" class="btn btn-default btn-md" runat="server" Text="Reject" BackColor="Red" OnClick="btnReject_Click" /></td>
                <td>
                    <asp:Button ID="btnApprove" class="btn btn-default btn-md" runat="server" Text="Approve" BackColor="#00CC00" OnClick="btnApprove_Click" /></td>
            </tr>
        </table>
       <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    </div>
</asp:Content>