<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PartAdd.aspx.cs" Inherits="PSI_WEBFORM.PartAdd" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="table-list1">
        <tr>
            <td colspan="6" class="table-title">
                <asp:Literal ID="Literal1" runat="server">SEARCH PART</asp:Literal>

            </td>
        </tr>
        <tr>
            <td class="td-label" style="width:100px;">PARTNUMBER</td>
            <td class="td-control" style="width:200px;">
                <asp:TextBox ID="txtPartNumber" runat="server" CssClass="control-input"></asp:TextBox></td>
            <td class="td-tip">
                <asp:Button ID="btnSearch" runat="server" Text="Search BOM" CssClass="control-button" OnClick="btnSearch_Click" /></td>
            <td class="td-label"></td>
            <td class="td-control"></td>
            <td class="td-tip"></td>
        </tr>
    </table>
    
    <asp:Panel ID="pnBOM" runat="Server">

        <table class="table-list1" runat="server" id="tbBOM">
            <tr>
                <td colspan="6">
                    <asp:Literal ID="lrtPartBOMTitle" runat="server">PART BOM LIST</asp:Literal>
                </td>
            </tr>
            <tr>
                <td colspan="6">

                    <asp:GridView ID="grd_vendor" runat="server" AutoGenerateColumns="false" OnRowDataBound="onRowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="No." >
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkRox" AutoPostBack="true"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CHILD_MTRLID" HeaderText="PARTNUMBER">
                                <%--<HeaderStyle BackColor="#0b325c" Width="150px"></HeaderStyle>--%>
                            </asp:BoundField>
	                    <asp:BoundField DataField="CHILD_MTRLNAME" HeaderText="PARTNAME">
                                <%--<HeaderStyle BackColor="#0b325c" Width="150px"></HeaderStyle>--%>
                            </asp:BoundField>
                            <asp:BoundField DataField="PLAN_LEVEL" HeaderText="PLAN LEVEL">
                                <%--<HeaderStyle BackColor="#0b325c" Width="150px"></HeaderStyle>--%>
                            </asp:BoundField>
	                    <asp:BoundField DataField="ORG_ID" HeaderText="ORG ID">
                                <%--<HeaderStyle BackColor="#0b325c" Width="150px"></HeaderStyle>--%>
                            </asp:BoundField>
                            <asp:BoundField DataField="CORP_ID" HeaderText="ORG NAME">
                                <%--<HeaderStyle BackColor="#0b325c" Width="150px"></HeaderStyle>--%>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Vendor">
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="cboVendor"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        </asp:GridView>
                    <%--<asp:Repeater ID="rpData" runat="server" OnItemCommand="rpData_ItemCommand">
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
                                    <asp:DropDownList runat="server" ID="cbovendor"></asp:DropDownList>
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
                                    <asp:DropDownList runat="server" ID="cbovendor1" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>

                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>--%>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click" Style="height: 26px" /></td>
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
                <asp:Literal ID="ltrTitleListPartLocal" runat="server">PART MANAGEMENT LIST</asp:Literal>

            </td>
        </tr>
        <tr>
            <td class="td-label" style="width:100px;">PARTNUMBER</td>
            <td class="td-control" style="width:200px;">
                <asp:TextBox ID="txtPartNumberLocal" runat="server" CssClass="control-input"></asp:TextBox></td>
            <td class="td-tip">
                <asp:Button ID="btnSeachLocal" runat="server" Text="Search Local" CssClass="control-button" OnClick="btnSeachLocal_Click"  /></td>
            <td class="td-label"></td>
            <td class="td-control"></td>
            <td class="td-tip"></td>
        </tr>
        <tr>

            <td colspan="6">
                <asp:Repeater ID="rpPartLocal" runat="server" OnItemCommand="rpData_ItemCommand">
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
                                <th>VENDER</th>
                                <th></th>
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
                                <asp:Literal ID="ltrPartName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PARTNAME")%>'></asp:Literal>
                                
                            </td>
                            <td>
                                <asp:Literal ID="ltrPlanLevel" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PLAN_LEVEL")%>'></asp:Literal>
                                
                            </td>
                            <td>
                                <asp:Literal ID="ltrOrg" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ORG_ID")%>'></asp:Literal>
                                
                            </td>
                            <td>
                                <asp:Literal ID="ltrActive" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ACTIVE")%>'></asp:Literal>
                                
                            </td>
                            <td>
                                <asp:Literal ID="ltrVender" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "VENDER")%>'></asp:Literal>
                                
                            </td>
                            <td>
                                <a href="./PartMoldMapping.aspx?PartNumber=<%#DataBinder.Eval(Container.DataItem, "PARTNUMBER")%>">MOLD MAPPING</a>
                            </td>
                            <td>
                                <a href="./Editpart.aspx?PartNumber=<%#DataBinder.Eval(Container.DataItem, "PARTNUMBER")%>">Edit</a>
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
                                <asp:Literal ID="ltrPartName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PARTNAME")%>'></asp:Literal>
                                <%--<%#DataBinder.Eval(Container.DataItem, "PARTNAME")%>--%>
                            </td>
                            <td>
                                <asp:Literal ID="ltrPlanLevel" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PLAN_LEVEL")%>'></asp:Literal>
                               <%-- <%#DataBinder.Eval(Container.DataItem, "PLAN_LEVEL")%>--%>
                            </td>
                            <td>
                                <asp:Literal ID="ltrOrg" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ORG_ID")%>'></asp:Literal>
                               <%-- <%#DataBinder.Eval(Container.DataItem, "ORG_ID")%>--%>
                            </td>
                            <td>
                                <asp:Literal ID="ltrActive" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ACTIVE")%>'></asp:Literal>
                                <%--<%#DataBinder.Eval(Container.DataItem, "ACTIVE")%>--%>
                            </td>
                            <td>
                                <asp:Literal ID="ltrVender" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "VENDER")%>'></asp:Literal>
                                <%--<%#DataBinder.Eval(Container.DataItem, "VENDER")%>--%>
                            </td>
                            <td>
                                <a href="./PartMoldMapping.aspx?PartNumber=<%#DataBinder.Eval(Container.DataItem, "PARTNUMBER")%>">MOLD MAPPING</a>
                            </td>
                            <td>
                                <a href="./Editpart.aspx?PartNumber=<%#DataBinder.Eval(Container.DataItem, "PARTNUMBER")%>">Edit</a>
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
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Style="height: 26px; color: red;" OnClick="btnDelete_Click" OnClientClick="return confirm('Do you want to delete?');" /></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
    </table>
   <%-- <script type="text/javascript">
        function CheckOne(obj) {
            var grid = obj.parentNode.parentNode.parentNode;
            var inputs = grid.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                        inputs[i].checked = false;
                    }
                }
            }
        }

    </script>--%>
</asp:Content>
