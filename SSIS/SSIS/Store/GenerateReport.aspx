<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="GenerateReport.aspx.cs" Inherits="SSIS.GenerateReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Generate Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
 <div class="juambotron">
        <br />
        <center>
                <h2>
                    Generate Report
                </h2>
            </center>
        <br />
        <div class="content" style="width:550px; margin:0 auto;">
            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-6"></div>
                <div class="col-xs-6 col-sm-8 col-md-12">
                    <asp:Label ID="lbReportType" runat="server" Text="Report Type" Font-Underline="True"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-6"></div>
                <div class="col-xs-6 col-sm-8 col-md-12 radio" style="align-content:center">
                    <asp:RadioButton ID="rbReport1" GroupName="Group1"
                         runat="server"
                         AutoPostBack="true"
                         Text="Supplier - Stationery Ordered Group By Category (Quantity)"
                         OnCheckedChanged="Group1_CheckedChanged"
                         checked="true"/>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-6"></div>
                <div class="col-xs-6 col-sm-8 col-md-12 radio" style="align-content:center">
                    <asp:RadioButton ID="rbReport3" GroupName="Group1"
                         runat="server"
                         AutoPostBack="true"
                         Text="Supplier - Stationery Ordered Group By Category (Price)"
                         OnCheckedChanged="Group1_CheckedChanged"/>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1 col-sm-2 col-md-6"></div>
                <div class="col-xs-4 col-sm-8 col-md-12 radio" style="align-content:center">
                    <asp:RadioButton ID="rbReport2" GroupName="Group1"
                         runat="server"
                         AutoPostBack="true"
                         Text="Department - Stationery Requested Group By Category (Quantity)" 
                         OnCheckedChanged="Group1_CheckedChanged"/>  
                </div>
            </div>
        </div>
        <br />

        <table align="center" class="table-responsive" style="border-collapse: separate; border-spacing: 10px;">
            <tr>
                <td style="height: 28px">
                    <asp:Label ID="lbDeptType" runat="server" Text="Department Name : "></asp:Label></td>
                <td style="height: 28px">
                    <asp:DropDownList ID="ddlDept" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 28px">
                    <asp:Label ID="lbStartDate" runat="server" Text="Start Date : "></asp:Label></td>
                <td style="height: 28px">          
                    <asp:TextBox ID="tbStartDate" runat="server" TextMode="Date"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 28px">
                    <asp:Label ID="lbEndDate" runat="server" Text="End Date : "></asp:Label></td>
                <td style="height: 28px"> 
                    <asp:TextBox ID="tbEndDate" runat="server" TextMode="Date"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <table align="center" class="table-responsive">
            <tr>
                <td style="height: 28px">
                    <asp:Button ID="btnGenerate" class="btn btn-default btn-md" runat="server" Text="Generate Report" OnClick="btnGenerate_Click" /></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="height: 28px">
                    <asp:ValidationSummary ID="ValidationSummary1" forecolor="Red" runat="server" />
                </td>
            </tr>
        </table>
        <asp:RequiredFieldValidator 
            ID="rfvStartDate" 
            runat="server" 
            ControlToValidate="tbStartDate"
            Style="color: Red"
            Display="None" 
            ErrorMessage="Please input a valid date.">
        </asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator 
            ID="rfvEndDate" 
            runat="server"
            ControlToValidate="tbEndDate"
            Style="color: Red"
            Display="None" 
            ErrorMessage="Please input a valid date.">
        </asp:RequiredFieldValidator>
        <asp:CompareValidator 
            ID="cvDate" 
            runat="server" 
            ErrorMessage="Start date must be before End date."
            ControlToValidate = "tbStartDate" 
            ControlToCompare = "tbEndDate" 
            Operator = "LessThan"
            Display="None"  
            Type = "Date">
        </asp:CompareValidator>
    </div>
    </asp:Content>