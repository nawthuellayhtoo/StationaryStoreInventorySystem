<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master" CodeBehind="Report1.aspx.cs" Inherits="SSIS.Store.Report1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <center><h2>Report</h2></center>
             <div align="center">
            <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-3">
                    <asp:Label ID="lbDept" runat="server" Text="Department  :"></asp:Label>
                    <asp:Label ID="lbDeptValue" runat="server"></asp:Label>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-3">
                    <asp:Label ID="lbStartDate" runat="server" Text="Start Date  :"></asp:Label>
                    <asp:Label ID="lbStartDateValue" runat="server"></asp:Label>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-3">
                    <asp:Label ID="lbEndDate" runat="server" Text="End Date  :"></asp:Label>
                    <asp:Label ID="lbEndDateValue" runat="server"></asp:Label>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-3">
                    <asp:Label ID="lbGenerateDate" runat="server" Text="Current Date  :"></asp:Label>
                    <asp:Label ID="lbGenerateDateValue" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <br />
    <div align="center">
         <rsweb:ReportViewer 
        ID="ReportViewer1" 
        runat="server" 
        Font-Names="Verdana" 
        Font-Size="8pt" 
        WaitMessageFont-Names="Verdana" 
        WaitMessageFont-Size="14pt" 
        Width="60%" 
        Height="720px" 
        DocumentMapWidth = "25%">
        <LocalReport ReportPath="CategoryQtyReport.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="SSIS.ReportDSTableAdapters."></asp:ObjectDataSource>
    </div>
    </asp:Content>
