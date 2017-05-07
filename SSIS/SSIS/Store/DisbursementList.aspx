<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="DisbursementList.aspx.cs" Inherits="SSIS.DisbursementList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Disbursement List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
      <script type="text/javascript">
        $(function () {
            $('[id*=gvDisbursement]').footable();
        });
    </script>
    <div class="juambotron">
        <center><h2>Disbursement List</h2></center>
        <br />
        <table style="width: 50%;" align="center" style="border-collapse: separate; border-spacing: 10px;">
            <tr>
                <td style="height: 28px">
                    <asp:Label ID="lbDepartment" runat="server" Text="Department:"></asp:Label>
                </td>
                <td style="height: 28px">
                    <asp:DropDownList ID="ddlDepartment" runat="server" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="True" ToolTip="Select the department" >
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td style="height: 28px">
                    <asp:Label ID="lbDepartmentRepresentative" runat="server" Text="Department Representative:"></asp:Label>
                </td>
                <td style="height: 28px">
                    <asp:TextBox ID="tbDeptRep" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 28px">
                    <asp:Label ID="lbCollectionPoint" runat="server" Text="Collection Point"></asp:Label>
                </td>
                <td style="height: 28px">
                    <asp:TextBox ID="tbCollectionPoint" Enabled="false" runat="server">University Hospital</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 28px">
                    <asp:Label ID="lbCollectionTime" runat="server" Text="Collection Time"></asp:Label>
                </td>
                <td style="height: 28px">
                    <asp:TextBox ID="tbCollectionTime" Enabled="false" runat="server">11:00am</asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <center><asp:Label ID="lblDisbursementInfo" runat="server" ></asp:Label></center>
        <asp:GridView ID="gvDisbursement" AutoGenerateColumns="false" horizontalalign="Center" CssClass="footable" runat="server" AllowPaging ="true" PageSize="5" OnPageIndexChanging="gvDisbursement_PageIndexChanging">
                <Columns>
                <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                <asp:BoundField DataField="ItemUOM" HeaderText="UOM" />
                <asp:BoundField  datafield="OrderQuantity" HeaderText="Ordered Quantity"/>
                <asp:BoundField  datafield="OutstandingQuantity" HeaderText="Previous Outstanding"/>
                <asp:BoundField  datafield="DisbursementQuantity" HeaderText="Quantity to be Disbursed"/>
            </Columns>
         </asp:GridView>
    </div>
</asp:Content>