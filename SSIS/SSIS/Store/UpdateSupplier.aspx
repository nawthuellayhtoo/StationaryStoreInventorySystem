<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="UpdateSupplier.aspx.cs" Inherits="SSIS.UpdateSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Update Supplier
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <div class="juambotron">
        <center><h2>Update Supplier</h2></center>
        <br />
        <div class="row">
            <div class="col-xs-3 col-sm-5 col-md-5"></div>
            <div class="col-xs-9 col-sm-7 col-md-7" style="align-items: center">
                <b>
                    <asp:Label ID="lbCategory" runat="server" Text="Category:"></asp:Label></b>
                <asp:DropDownList ID="ddlCategory" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </div>
            <div class="col-xs-3 col-sm-5 col-md-5"></div>
            <div class="col-xs-9 col-sm-7 col-md-7">
                <asp:Label ID="lbItemName" runat="server" Text="Item Name:"></asp:Label>
                <asp:DropDownList ID="ddlItemName" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <br />

        <div class="row">
            <div class="col-xs-3 col-sm-1 col-md-1"></div>
            <div class="col-xs-9 col-sm-3 col-md-3" style="align-items: center">
                <b>
                    <asp:Label ID="lbSupplier1" runat="server" Text="Supplier 1 ID"></asp:Label></b>
                <asp:DropDownList ID="ddlSupplier1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSupplier1_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-xs-3 col-sm-1 col-md-1"></div>
            <div class="col-xs-9 col-sm-3 col-md-3">
                <asp:Label ID="lbSupplier1Name" runat="server" Text="Name"></asp:Label>
                <asp:TextBox ID="tbSupplier1Name" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-xs-3 col-sm-1 col-md-1"></div>
            <div class="col-xs-9 col-sm-3 col-md-3">
                <asp:Label ID="lbSupplier1Price" runat="server" Text="Price $"></asp:Label>
                <asp:TextBox ID="tbSupplier1Price" runat="server" type="number"></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-xs-3 col-sm-1 col-md-1"></div>
            <div class="col-xs-9 col-sm-3 col-md-3">
                <b>
                    <asp:Label ID="lbSupplier2" runat="server" Text="Supplier 2 ID"></asp:Label></b>
                <asp:DropDownList ID="ddlSupplier2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSupplier2_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col-xs-3 col-sm-1 col-md-1"></div>
            <div class="col-xs-9 col-sm-3 col-md-3">
                <asp:Label ID="lbSupplier2Name" runat="server" Text="Name"></asp:Label>
                <asp:TextBox ID="tbSupplier2Name" runat="server" Enable="false" Enabled="False"></asp:TextBox>
            </div>
            <div class="col-xs-3 col-sm-1 col-md-1"></div>
            <div class="col-xs-9 col-sm-3 col-md-3">
                <asp:Label ID="lbSupplier2Price" runat="server" Text="Price $"></asp:Label>
                <asp:TextBox ID="tbSupplier2Price" runat="server" type="number"></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-xs-3 col-sm-1 col-md-1"></div>
            <div class="col-xs-9 col-sm-3 col-md-3">
                <b>
                    <asp:Label ID="lbSupplier3" runat="server" Text="Supplier 3"></asp:Label></b>
                <asp:DropDownList ID="ddlSupplier3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSupplier3_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col-xs-3 col-sm-1 col-md-1"></div>
            <div class="col-xs-9 col-sm-3 col-md-3">
                <asp:Label ID="lbSupplier3Name" runat="server" Text="Name"></asp:Label>
                <asp:TextBox ID="tbSupplier3Name" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-xs-3 col-sm-1 col-md-1"></div>
            <div class="col-xs-9 col-sm-3 col-md-3">
                <asp:Label ID="lbSupplier3Price" runat="server" Text="Price $"></asp:Label>
                <asp:TextBox ID="tbSupplier3Price" runat="server" type="number"></asp:TextBox>
            </div>
        </div>
        <br />

        <table width="40%" align="center">
            <tr align="center">
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="120px" class="btn btn-default btn-md" OnClick="btnSave_Click" />
                </td>
            </tr>
            <tr align="center">
                <td>
                    <asp:ValidationSummary ForeColor="Red" runat="server" ID="validationSummary"></asp:ValidationSummary>
                </td>
            </tr>
        </table>



        <asp:CompareValidator
            ID="cv1"
            runat="server"
            ControlToCompare="tbSupplier1Name"
            CultureInvariantValues="true"
            Display="None"
            EnableClientScript="true"
            ControlToValidate="tbSupplier2Name"
            ErrorMessage="Supplier 1 cannot be the same as Supplier 2"
            Operator="NotEqual"></asp:CompareValidator>

        <asp:CompareValidator
            ID="cv2"
            runat="server"
            ControlToCompare="tbSupplier1Name"
            CultureInvariantValues="true"
            Display="None"
            EnableClientScript="true"
            ControlToValidate="tbSupplier3Name"
            ErrorMessage="Supplier 1 cannot be the same as Supplier 3"
            Operator="NotEqual"></asp:CompareValidator>

        <asp:CompareValidator
            ID="cv3"
            runat="server"
            ControlToCompare="tbSupplier2Name"
            CultureInvariantValues="true"
            Display="None"
            EnableClientScript="true"
            ControlToValidate="tbSupplier3Name"
            ErrorMessage="Supplier 2 cannot be the same as Supplier 3"
            Operator="NotEqual"></asp:CompareValidator>
</asp:Content>
