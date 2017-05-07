<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AssignDepartmentRepresentative.aspx.cs" Inherits="SSIS.AssignDepartmentRepresentative" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">
    Assign Department Representative
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
            <div class="juambotron"><br /><center><h2>Assign Department Representative</h2></center><br />
        <table width="33%" align="center">
            <tr>
                <td><asp:Label ID="lbCurrentRep" runat="server" Text="Current Department Representative: "></asp:Label></td>
                <td><asp:TextBox ID="tbCurrentRep" runat="server" Enabled="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lbAssignRep" runat="server" Text="Assign Department Representative: "></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlAssignRep" runat="server">
                        <asp:ListItem Selected="True" Value="Tony Tan"></asp:ListItem>
                        <asp:ListItem Value="Mary Jane"></asp:ListItem>
                        <asp:ListItem Value="John Doe"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><asp:Button ID="btnAssign" class="btn btn-default btn-md" runat="server" Text="Assign" OnClick="btnAssign_Click"/></td>
            </tr>
        </table>

    </div>
</asp:Content>