<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="RequisitionDetails.aspx.cs" Inherits="SSIS.RequisitionDetails" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Requisition Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>

    <script type="text/javascript">
        $("#btnApprove").click(function () {
            $("#LbStatus").html("Approved");
        });

        $(function () {
            $('[id*=GridViewRequisitionDetails]').footable();
        });
    </script>
    <div class="juambotron">
        <center>
            <h2>Requisition Details: <asp:Label ID="lbRQID" runat="server"></asp:Label></h2>
            <b>Applier:</b> <asp:Label ID="LbEmpName" runat="server" Text=""></asp:Label> &nbsp;&nbsp; <b>Date:</b> <asp:Label ID="LbEmpRequisitionDate" runat="server" Text=""></asp:Label> &nbsp;&nbsp; <b>Status:</b> <asp:Label ID="LbStatus" runat="server" Text=""></asp:Label>
        </center>
        <br />
                    
        <asp:GridView ID="GridViewRequisitionDetails" AutoGenerateColumns="false" horizontalalign="Center" CssClass="footable" runat="server" width="80%">
            <Columns>
                    <asp:BoundField DataField="retrievalItemName" HeaderText="Item Name" ItemStyle-Width="26.67%">
<ItemStyle Width="26.67%"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="26.67%" >
                        <ItemTemplate>
                            <asp:TextBox ID="retrievalQuantity" TextMode="Number"  
                                Text='<%# Eval("retrievalQuantity") %>' runat="server" ></asp:TextBox>
                        </ItemTemplate>

<ItemStyle Width="26.67%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="retrievalUOM" HeaderText="UOM" ItemStyle-Width="26.67%" >
<ItemStyle Width="26.67%"></ItemStyle>
                    </asp:BoundField>
            </Columns>
        </asp:GridView>
        <br />

        <table width="30%" align="center">
            <tr>
                <td>
                    <asp:Label ID="lbEmployeeComments" runat="server" Text="Employee Comments:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="tbEmployeeComments" runat="server" Height="60px" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbDepartmentHeadComments" runat="server" Text="Department Head Comments:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="tbDepartmentHeadComments" runat="server" Height="60px" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
        <asp:Label ID="Lb" runat="server" Text="" align="center"></asp:Label>
                </td>
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
                <td>&nbsp;</td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>