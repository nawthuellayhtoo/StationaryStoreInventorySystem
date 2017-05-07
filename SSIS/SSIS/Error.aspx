<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="SSIS.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Error
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div class="juambotron">
        <br />
        <center><h2>Error</h2></center>
        <br />
        <center>You do not have permission to access this page. <a href='javascript:history.go(-1)'>Return to the previous page</a>.</center>
    </div>
</asp:Content>
