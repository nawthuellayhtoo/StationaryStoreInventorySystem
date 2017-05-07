<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Report3.aspx.cs" Inherits="SSIS.Store.Report3" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <center><h2>Report</h2>
        <div class="content">
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
        </div></center>
    <br />
    <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div align="center">
        <rsweb:ReportViewer ID="ReportViewer1" 
            runat="server" 
                Font-Names="Verdana" 
                Font-Size="8pt" 
                WaitMessageFont-Names="Verdana" 
                WaitMessageFont-Size="14pt"
                Width="60%" 
                Height="720px">
            <localreport reportpath="SupCategoryPrice.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet3" />
                </DataSources>
            </localreport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="SSIS.ReportDSTableAdapters."></asp:ObjectDataSource>
        </div>
</asp:Content>
