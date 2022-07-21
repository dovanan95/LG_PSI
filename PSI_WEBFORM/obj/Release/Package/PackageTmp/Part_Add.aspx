<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Part_Add.aspx.cs" Inherits="PSI_WEBFORM.Part_Add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(function () {

            $("#tblPartsGMES [id*=chkHeader]").click(function () {

                if ($(this).is(":checked")) {

                    $("#tblPartsGMES [id*=chkRow]").attr("checked", "checked");

                } else {

                    $("#tblPartsGMES [id*=chkRow]").removeAttr("checked");
                }

            });

            $("#tblPartsGMES [id*=chkRow1]").click(function () {

                if ($("#tblPartsGMES [id*=chkRow]").length == $("#tblPartsGMES [id*=chkRow]:checked").length) {

                    $("#tblPartsGMES [id*=chkHeader]").attr("checked", "checked");

                } else {

                    $("#tblPartsGMES [id*=chkHeader]").removeAttr("checked");

                }

            });

            $("#tblPartsLOCAL [id*=chkHeader1]").click(function () {

                if ($(this).is(":checked")) {

                    $("#tblPartsLOCAL [id*=chkRow1]").attr("checked", "checked");

                } else {

                    $("#tblPartsLOCAL [id*=chkRow1]").removeAttr("checked");

                }

            });

            $("#tblPartsLOCAL [id*=chkRow1]").click(function () {

                if ($("#tblPartsLOCAL [id*=chkRow1]").length == $("#tblPartsLOCAL [id*=chkRow1]:checked").length) {

                    $("#tblPartsLOCAL [id*=chkHeader1]").attr("checked", "checked");

                } else {

                    $("#tblPartsLOCAL [id*=chkHeader1]").removeAttr("checked");

                }

            });

        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="table-list1">
        <tr>
            <td colspan="6" class="table-title">
                <asp:Literal ID="Literal2" runat="server">SEARCH PART</asp:Literal>

            </td>
        </tr>
        <tr>
            <td class="td-label" style="width:100px;">PARTNUMBER</td>
            <td class="td-control" style="width:200px;">
                <asp:TextBox ID="TextBox1" runat="server" CssClass="control-input"></asp:TextBox></td>
            <td class="td-tip">
                <asp:Button ID="Button1" runat="server" Text="Search BOM" CssClass="control-button" OnClick="btnSearch_Click" /></td>
            <td class="td-label"></td>
            <td class="td-control"></td>
            <td class="td-tip"></td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="Server">

        <table class="table-list1" runat="server" id="Table1">
            <tr>
                <td colspan="6">
                    <asp:Literal ID="Literal3" runat="server">PART BOM LIST</asp:Literal>

                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="rpData_ItemCommand">
                        <HeaderTemplate>
                            <table class="table-list" id="tblPartsGMES">
                                <tr>
                                    <th>
                                        <asp:CheckBox ID="chkHeader" runat="server" /></th>
                                    <th>PARNUMBER</th>
                                    <th>PARTNAME</th>
                                    <th>PLAN LEVEL</th>
                                    <th>ORG ID</th>
                                    <th>ORG NAME</th>
                                    <th>VENDER</th>
                                </tr>
                        </HeaderTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkRow" runat="server" /></td>
                                <td>
                                    <asp:Literal ID="ltrChild_MTRLID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CHILD_MTRLID")%>'></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="ltrChild_MTRLNAME" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CHILD_MTRLNAME")%>'></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="ltrPlan_Level" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PLAN_LEVEL")%>'></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="ltr_Org_ID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ORG_ID")%>'></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="ltr_Corp_ID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CORP_ID")%>'></asp:Literal>
                                </td>
                                <td>
                                   <asp:DropDownList runat="server" ID="cbovendor" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkRow" runat="server" /></td>
                                <td>
                                    <asp:Literal ID="ltrChild_MTRLID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CHILD_MTRLID")%>'></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="ltrChild_MTRLNAME" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CHILD_MTRLNAME")%>'></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="ltrPlan_Level" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PLAN_LEVEL")%>'></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="ltr_Org_ID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ORG_ID")%>'></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="ltr_Corp_ID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CORP_ID")%>'></asp:Literal>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList12" runat="server" AutoPostBack="True" Width="50px" CssClass="control-dropdownlist5">
                                    </asp:DropDownList>
                                </td>
                            </tr>

                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="Insert" OnClick="btnInsert_Click" Style="height: 26px" /></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>

                <td></td>
            </tr>
        </table>
    </asp:Panel>

    <table class="table-list1">
        <tr>
            <td colspan="6" class="table-title">
                <asp:Literal ID="Literal4" runat="server">PART MANAGEMENT LIST</asp:Literal>

            </td>
        </tr>
        <tr>
            <td class="td-label" style="width:100px;">PARTNUMBER</td>
            <td class="td-control" style="width:200px;">
                <asp:TextBox ID="TextBox2" runat="server" CssClass="control-input"></asp:TextBox></td>
            <td class="td-tip">
                <asp:Button ID="Button3" runat="server" Text="Search Local" CssClass="control-button" OnClick="btnSeachLocal_Click"  /></td>
            <td class="td-label"></td>
            <td class="td-control"></td>
            <td class="td-tip"></td>
        </tr>
        <tr>

            <td colspan="6">
                <asp:Repeater ID="Repeater2" runat="server" OnItemCommand="rpData_ItemCommand">
                    <HeaderTemplate>
                        <table class="table-list" id="tblPartsLOCAL">
                            <tr>
                                <th>
                                    <asp:CheckBox ID="chkHeader1" runat="server" /></th>
                                <th>PART NUMBER</th>
                                <th>PART NAME</th>
                                <th>PLAN LEVEL</th>
                                <th>ORG</th>
                                <th>ACTIVE</th>
                                <th></th>
                            </tr>
                    </HeaderTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkRow1" runat="server" /></td>
                            <td>
                                <asp:Literal ID="ltrPartNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PARTNUMBER")%>'></asp:Literal>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "PARTNAME")%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "PLAN_LEVEL")%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "ORG_ID")%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "ACTIVE")%>
                            </td>
                            <td>
                                <a href="./PartMoldMapping.aspx?PartNumber=<%#DataBinder.Eval(Container.DataItem, "PARTNUMBER")%>">MOLD MAPPING</a>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkRow1" runat="server" /></td>
                            <td>
                                <asp:Literal ID="ltrPartNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PARTNUMBER")%>'></asp:Literal>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "PARTNAME")%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "PLAN_LEVEL")%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "ORG_ID")%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "ACTIVE")%>
                            </td>
                            <td>
                                <a href="./PartMoldMapping.aspx?PartNumber=<%#DataBinder.Eval(Container.DataItem, "PARTNUMBER")%>">MOLD MAPPING</a>
                            </td>
                        </tr>

                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button4" runat="server" Text="Delete" Style="height: 26px; color: red;" OnClick="btnDelete_Click" OnClientClick="return confirm('Do you want to delete?');" /></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
    </table>
</asp:Content>
