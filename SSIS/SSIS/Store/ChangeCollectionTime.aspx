<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ChangeCollectionTime.aspx.cs" Inherits="SSIS.Store.ChangeCollectionTime" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div class="juambotron">
        <br />
        <center><h2>Change Collection Time</h2></center>
        <br />
        <div class="row">
            <div class="col-xs-0 col-sm-1 col-md-3"></div>
            <div class="col-xs-3 col-sm-3 col-md-3">
                <asp:Label ID="lbCollectionPoint" runat="server" Text="Collection Point: "></asp:Label>
            </div>
            <div class="col-xs-3 col-sm-3 col-md-3">  
                <asp:DropDownList ID="DropDownList1"  AutoPostBack="true" runat="server"  OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-0 col-sm-1 col-md-3"></div>
            <div class="col-xs-3 col-sm-3 col-md-3">
                <asp:Label ID="lbCs" runat="server"  Text="Collection Time : "></asp:Label>
            </div>
            <div class="col-xs-3 col-sm-3 col-md-3">      
                <asp:DropDownList ID="DropDownList2"   runat="server">
                </asp:DropDownList>
            </div>
        </div>
        <table width="50%" align="center">           
            <tr>
                <td class="modal-sm" style="width: 241px"></td>
                <asp:ScriptManager ID="ScriptManager" runat="server">
                </asp:ScriptManager>
                <td>
                    <asp:Button ID="btnUpdate" class=" btn" runat="server" Text="Update" Width="92px" OnClick="btnUpdate_Click"  /></td>
            </tr>
        </table>
    </div>
</asp:Content>
