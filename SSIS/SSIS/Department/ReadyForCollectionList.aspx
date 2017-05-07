<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ReadyForCollectionList.aspx.cs" Inherits="SSIS.ReadyForCollectionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Ready For Collection List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script>
        $(document).ready(function () {
            $('#cbAll').change(function () {
                var GridView2 = document.getElementById("<%=GridView1.ClientID %>");
                for (i = 1; i < GridView2.rows.length; i++) {
                    GridView2.rows[i].cells[5].getElementsByTagName("INPUT")[0].checked = $(this).prop('checked');
                }
            });
            $('#cbAll').trigger('change');
        });
    </script>
    <script type="text/javascript">
        function RemoveValidationDuplicates(validationGroup1) {
            Page_ClientValidate(validationGroup1);

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
        }
        $(function () {
            $('[id*=GridView1]').footable();
        });
    </script>

    <div class="juambotron">
        <br />
        <center><h2>Ready For Collection List</h2>
             <asp:Label ID="lblStatus" align="center" runat="server"></asp:Label>
           <br />            
        </center>

        <div align="center">
            <table>
                <tr>
                    <td align="left">
                        <asp:CheckBox ID="cbAll" ClientIDMode="Static"  align="center" runat="server" Text="ReceiveAll" OnCheckedChanged="cbAll_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GridView1" ViewStateMode="Enabled" AutoGenerateColumns="false" horizontalalign="Center" CssClass="footable" runat="server">
                            <Columns>
                                <asp:BoundField DataField="ItemName" ReadOnly="true" HeaderText="Item Name" />
                                <asp:BoundField DataField="ItemNumber" ReadOnly="true" HeaderText="Item Number" />
                                <asp:BoundField DataField="ItemUOM" ReadOnly="true" HeaderText="UOM" />
                                <asp:BoundField DataField="OrderQuantity" ReadOnly="true" HeaderText="Ordered Quantity" />
                                <asp:BoundField DataField="DisbursementQuantity" HeaderText="Disbursement Quantity" />
                                <asp:TemplateField HeaderText="Disbursement Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbDisbursementQty" runat="server" type="number" Text='<%#Eval ("DisbursementQuantity") %>' ToolTip="Change the disbursed quantity if necessary" min="0" max='<%# Eval("DisbursementQuantity") %>'></asp:TextBox>
                                        <asp:CompareValidator ID="CvDisbursementValue" runat="server" ControlToValidate="tbDisbursementQty" ValueToCompare='<%#Eval ("DisbursementQuantity") %>' Type="Integer" Operator="LessThanEqual" ErrorMessage="Disbursed qty cannot be more that alloted" ForeColor="Red" ValidationGroup="validationGroupName1">*</asp:CompareValidator>
                                        <asp:CompareValidator ID="cvDisubursementQty2" runat="server" ControlToValidate="tbDisbursementQty" ValueToCompare="0" Type="Integer" Operator="GreaterThanEqual" ErrorMessage="Disbursed qty cannot be negative" ForeColor="Red" ValidationGroup="validationGroupName1">*</asp:CompareValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="OutstandingQuantity" ReadOnly="true" HeaderText="OutstandingQuantity" />
                                <asp:BoundField DataField="RequisitionId" ReadOnly="true" HeaderText="RequisitionId" />

                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:CheckBox ClientIDMode="Static"  ID="choose" runat="server" Text="Receive" />
                                    </ItemTemplate>

                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>

        <table width="10%" align="center">

            <tr>
                <td>
                    <asp:Button ID="btnUpdate" class="btn btn-default btn-md" runat="server" Text="Update" OnClick="btnUpdate_Click" OnClientClick="RemoveValidationDuplicates('validationGroupName1')" />
                    <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" EnableClientScript="true" runat="server" ForeColor="Red" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
