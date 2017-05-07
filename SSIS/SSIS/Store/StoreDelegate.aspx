<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="StoreDelegate.aspx.cs" Inherits="SSIS.StoreDelegate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Store Delegate
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <div class="juambotron">
        <br />
        <center>
                <h2>
                    Store Delegate
                </h2>
            </center>
        <br />
        <table width="25%" align="center" class="table-responsive">
            <tr>
                <td>
                    <asp:Label ID="lbDelegate" runat="server" Text="Delegate:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="tbDelegate" Enabled="false" runat="server" Text="Tan Ah Kow"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbStartDate" runat="server" Text="Start Date : "></asp:Label></td>
                <td>
                    <asp:TextBox ID="tbStartDate" TextMode="Date" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbEndDate" runat="server" Text="End Date : "></asp:Label></td>
                <td>
                    <asp:TextBox ID="tbEndDate" TextMode="Date" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnActivate" class="btn btn-default btn-md" runat="server" Text="Activate" BackColor="#00CC00" OnClick="btnActivate_Click" />
                    <asp:Button ID="btnDeactivate" class="btn btn-default btn-md" runat="server" Text="Deactivate" BackColor="Red" OnClick="btnDeactivate_Click" CausesValidation="false" />
                </td>
            </tr>
        </table>
        <table width="25%" align="center" class="table-responsive">
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><asp:validationsummary forecolor="Red" runat="server" id="validationSummary">
                </asp:validationsummary></td>
            </tr>
        </table>

    </div>

    <asp:CompareValidator 
        ID="compareValidatorEarlier" 
        runat="server"
        ControlToCompare="tbStartDate" 
        CultureInvariantValues="true"
        Display="None" 
        EnableClientScript="true"
        ControlToValidate="tbEndDate"
        ErrorMessage="Start date must be earlier than end date"
        Type="Date"
        Operator="GreaterThanEqual"></asp:CompareValidator>

    <asp:CompareValidator 
        ID="compareValidatorSameDate" 
        runat="server"
        ControlToCompare="tbStartDate" 
        CultureInvariantValues="true"
        Display="None" 
        EnableClientScript="true"
        ControlToValidate="tbEndDate"
        ErrorMessage="Start date cannot be same as end date"
        Type="Date" 
        Operator="NotEqual"></asp:CompareValidator>

    <asp:CompareValidator runat="server"
        ID="compareValidatorStartToday"
        Display="None"
        ErrorMessage="Start date cannot be earlier than today"
        ControlToValidate="tbStartDate" 
        Type="date"
        Operator="GreaterThanEqual"
        ValueToCompare="<%# DateTime.Today.ToShortDateString() %>" />

    <asp:CompareValidator runat="server"
        ID="compareValidatorEndToday"
        Display="None"
        ErrorMessage="End date cannot be earlier than today"
        ControlToValidate="tbEndDate" 
        Type="date"
        Operator="GreaterThan"
        ValueToCompare="<%# DateTime.Today.ToShortDateString() %>" />

    <asp:RequiredFieldValidator 
        runat="server" 
        ID="requiredValidatorStartDate"
        Display="None"
        ControlToValidate="tbStartDate"
        ErrorMessage="Please enter start date" /> 

    <asp:RequiredFieldValidator 
        runat="server" 
        ID="requiredValidatorEndDate"
        Display="None"
        ControlToValidate="tbEndDate"
        ErrorMessage="Please enter end date" />

</asp:Content>