<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="DepartmentDelegate.aspx.cs" Inherits="SSIS.DepartmentDelegate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Department Delegate
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <div class="juambotron">
        <br />
        <center>
                <h2>
                    Department Delegate
                </h2>
            </center>
        <br />
        <table width="25%" align="center" class="table-responsive">
            <tr>
                <td>
                    <asp:label id="lbEmployee" runat="server" text="Employee : "></asp:label>
                </td>
                <td>
                    <asp:dropdownlist id="ddlEmployee" runat="server" AutoPostBack="True">
                    </asp:dropdownlist>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:label id="lbStartDate" runat="server" text="Start Date : "></asp:label>
                </td>
                <td>
                    <asp:textbox id="tbStartDate" textmode="Date" runat="server"></asp:textbox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:label id="lbEndDate" runat="server" text="End Date : "></asp:label>
                </td>
                <td>
                    <asp:textbox id="tbEndDate" textmode="Date" runat="server"></asp:textbox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:button id="btnActivate" class="btn btn-default btn-md" runat="server" text="Activate" BackColor="#00CC00" onclick="btnActivate_Click" />
                    <asp:button id="btnDeactivate" class="btn btn-default btn-md" runat="server" text="Deactivate" BackColor="Red" onclick="btnDeactivate_Click" CausesValidation="false" />
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