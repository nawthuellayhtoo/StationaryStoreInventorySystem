<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ChangeCollectionPoint.aspx.cs" Inherits="SSIS.ChangeCollectionPoint" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Change Collection Point
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
   <div class="juambotron">
        <br />
        <center><h2>Change Collection Point</h2></center>
        <br />
         <table width="50%" align="center">
             <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Current Collection Point:"></asp:Label>
                    <br />
                    <br />
                </td>
                <td>
                      <asp:Label ID="lblCurrentCollectionPoint" runat="server"></asp:Label>   
                     <br />     
                    <br />
                </td>
            </tr>
                <tr>
                <td>
                    <asp:Label ID="lbCollectionPoint" runat="server" Text="Collection Point: "></asp:Label></td>
                <td>
                    <asp:RadioButtonList ID="rblCollectionPoint" AutoPostBack="true" runat="server">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbCs" runat="server"  Text="Collection Time : "></asp:Label></td>
                <td>
                    <asp:TextBox ID="tbDepName"  AutoPostBack="true" runat="server" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnUpdate" class="btn btn-default btn-md" runat="server" Text="Update" OnClick="btnUpdate_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
