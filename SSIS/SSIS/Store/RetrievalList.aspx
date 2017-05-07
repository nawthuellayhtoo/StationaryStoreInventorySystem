
<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="RetrievalList.aspx.cs" Inherits="SSIS.RetrievalList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Retrieval List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
<script language="javascript" type="text/javascript">
function RemoveValidationDuplicates(validationGroup) {
    Page_ClientValidate(validationGroup);

    if (typeof (Page_ValidationSummaries) == "undefined")
        return;
    var summary, sums, s;
    for (sums = 0; sums < Page_ValidationSummaries.length; sums++) {
        summary = Page_ValidationSummaries[sums];
        summary.style.display = "none";
        if (!Page_IsValid) {

            if (summary.showsummary != "False") {
                summary.style.display = "";
                if (typeof (summary.displaymode) != "string") {
                    summary.displaymode = "BulletList";
                }
                switch (summary.displaymode) {
                    case "List":
                        headerSep = "<br/>";
                        first = "";
                        pre = "";
                        post = "<br/>";
                        final = "";
                        break;
                    case "BulletList":
                    default:
                        headerSep = "";
                        first = "<ul>";
                        pre = "<li>";
                        post = "</li>";
                        final = "</ul>";
                        break;
                    case "SingleParagraph":
                        headerSep = " ";
                        first = "";
                        pre = "";
                        post = " ";
                        final = "<br/>";
                        break;
                }
                s = "";
                if (typeof (summary.headertext) == "string") {
                    s += summary.headertext + headerSep;
                }
                s += first;

                for (i = 0; i < Page_Validators.length; i++) {
                    if (!Page_Validators[i].isvalid && typeof (Page_Validators[i].errormessage) == "string") {
                        var tempstr = pre + Page_Validators[i].errormessage + post;
                        var isExist = s.search(tempstr);
                        if (isExist == -1)
                            s += pre + Page_Validators[i].errormessage + post;
                    }
                }
                s += final;
                summary.innerHTML = s;
                window.scrollTo(0, 0);
            }
        }
    }
} $(function () {
    $('[id*=gvRetrivalList]').footable();
});
$(function () {
    $('[id*=gvBreakeDownByDept]').footable();
});
</script>
    <div class="juambotron">
        <center><h2>Retrieval List</h2>
            <asp:Label ID="lblListInfo" runat="server">
            </asp:Label>
        </center>
        <br />
           <asp:GridView ID="gvRetrivalList" runat="server" AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound" DataKeyNames="ItemName" horizontalalign="Center" CssClass="footable" >
              <Columns>
               <asp:BoundField  datafield="Bin" HeaderText="BIN#"/>
                <asp:BoundField  datafield="ItemName" HeaderText="Stationery Description" HtmlEncode="false"/>
                 <asp:BoundField  datafield="Needed" HeaderText="Needed" />
                  <asp:TemplateField HeaderText="Retrieved">
                            <ItemTemplate>
                                    <asp:TextBox ID="tbTotalActual" type="number" runat="server" Text="" ToolTip="Enter the retrived quantity" datafield="Retrieved"  OnTextChanged="tbTotalActual_TextChanged" AutoPostBack="True" min="0" max='<%# Eval("Needed") %>'></asp:TextBox>
                                   <asp:CompareValidator ID="cvTotalActual" runat="server" ControlToValidate="tbTotalActual" ValueToCompare='<%# Eval("Needed") %>' Type="Integer" Operator="LessThanEqual"  ErrorMessage="Retrived Qty should not be more that needed" ForeColor="Red" ValidationGroup="validationGroupName">*</asp:CompareValidator>
                                   <asp:CompareValidator ID="CvTotalActual2" runat="server" ControlToValidate="tbTotalActual" ValueToCompare="0" Type="Integer" Operator="GreaterThanEqual"  ErrorMessage="Retrived qty cannot be negative" ForeColor="Red" ValidationGroup="validationGroupName">*</asp:CompareValidator>
                            </ItemTemplate>
                   </asp:TemplateField>
                 <asp:TemplateField HeaderText="BreakDown By Department">
                    <ItemTemplate>
                        <asp:GridView ID="gvBreakeDownByDept" runat="server"  AutoGenerateColumns="False" horizontalalign="Center" CssClass="footable">
                           <Columns>
                                <asp:BoundField  datafield="Dep" HeaderText="Dept Name" HtmlEncode="false"/>
                               <asp:BoundField  datafield="Outstanding" HeaderText="Outstanding"/>
                               <asp:BoundField  datafield="Needed" HeaderText="Needed"/>
                               <asp:TemplateField HeaderText="Actual">
                                  <ItemTemplate>
                                      <asp:TextBox ID="tbActual" runat="server" type="number" Text='<%#Eval ("Actual") %>' ToolTip="Change the actual quantity if necessary" OnTextChanged="tbActual_TextChanged" min="0" max='<%# Eval("Needed") %>'></asp:TextBox>
                                      <asp:CompareValidator ID="cvActual" runat="server" ControlToValidate="tbActual" ValueToCompare='<%# Eval("Needed") %>' Type="Integer" Operator="LessThanEqual"  ErrorMessage="Disburded Qty should not be more than needed" ForeColor="Red" ValidationGroup="validationGroupName">*</asp:CompareValidator>
                                      <asp:CompareValidator ID="CvTotalActual2" runat="server" ControlToValidate="tbActual" ValueToCompare="0" Type="Integer" Operator="GreaterThanEqual"  ErrorMessage="Disbursed qty cannot be negative" ForeColor="Red" ValidationGroup="validationGroupName">*</asp:CompareValidator>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               </Columns>
                        </asp:GridView>                    
                        </ItemTemplate>                  
                 </asp:TemplateField>  
                  
               </Columns>
             </asp:GridView>
        <div align="center">            
            <asp:Button ID="btnUpdate" class="btn btn-default btn-md" runat="server" Text="Update" OnClick="btnUpdate_Click" OnClientClick="RemoveValidationDuplicates('validationGroupName')" />
            <asp:ValidationSummary id="valSum" DisplayMode="BulletList"  EnableClientScript="true"  runat="server" ForeColor="Red"/> 
        </div>
    </div>
</asp:Content>