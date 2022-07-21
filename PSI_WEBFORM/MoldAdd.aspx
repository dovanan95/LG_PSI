<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MoldAdd.aspx.cs" Inherits="PSI_WEBFORM.MoldAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(function () {

            $("#tblMold [id*=chkHeader]").click(function () {

                if ($(this).is(":checked")) {

                    $("#tblMold [id*=chkRow]").attr("checked", "checked");

                } else {

                    $("#tblMold [id*=chkRow]").removeAttr("checked");
                }

            });

            $("#tblMold [id*=chkRow1]").click(function () {

                if ($("#tblMold [id*=chkRow]").length == $("#tblMold [id*=chkRow]:checked").length) {

                    $("#tblMold [id*=chkHeader]").attr("checked", "checked");

                } else {

                    $("#tblMold [id*=chkHeader]").removeAttr("checked");

                }

            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="table-list1">
        <tr>
            <td colspan="6" class="table-title">
                <asp:Literal ID="lrtInsertTitle" runat="server">ADD MOLD</asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="td-label">MOLD ID</td>
            <td class="td-control">
                <asp:TextBox ID="txtMoldID" runat="server" CssClass="control-input"></asp:TextBox></td>
            <td class="td-tip"></td>
            <td class="td-label">MOLD NAME</td>
            <td class="td-control">
                <asp:TextBox ID="txtMoldName" runat="server" CssClass="control-input"></asp:TextBox></td>
            <td class="td-tip"></td>
        </tr>
        <tr>
            <td class="td-label">CAPACITY</td>
            <td class="td-control">
                <asp:TextBox ID="txtCapacity" runat="server" CssClass="control-input">0</asp:TextBox></td>
            <td class="td-tip"></td>
            <td class="td-label">MACHINE</td>
            <td class="td-control">
                <asp:TextBox ID="txtMachine" runat="server" CssClass="control-input" OnTextChanged="txtMachine_TextChanged"></asp:TextBox>

            </td>
            <td class="td-tip"></td>
        </tr>

        <tr>
            <td class="td-label">ORGANIZATION</td>
            <td class="td-control">
                <asp:DropDownList ID="ddlDropDownORG" runat="server" CssClass="control-dropdownlist" OnSelectedIndexChanged="ddlDropDownORG_SelectedIndexChanged">
                    <asp:ListItem Value="-1">-ORG----</asp:ListItem>
                    <asp:ListItem Value="255305">AVF</asp:ListItem>
                    <asp:ListItem Value="260330">AVG</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="td-tip"></td>
            <td class="td-label">VENDER</td>
            <td class="td-control">
                <asp:TextBox ID="txtVender" runat="server" CssClass="control-input"></asp:TextBox>
            </td>
            <td class="td-tip"></td>
        </tr>
        <tr>
            <td class="td-label"></td>
            <td class="td-control">
                <asp:Button ID="btnInsert" runat="server" Text="Insert" CssClass="control-button" OnClick="btnInsert_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="control-button" OnClick="btnClear_Click" />
            </td>
            <td class="td-tip"></td>
            <td class="td-label"></td>
            <td class="td-control">

                
            </td>
            <td class="td-tip"></td>
        </tr>

        <tr>
            <td colspan="6" class="table-title">
                <asp:Literal ID="ltrTitleListPartLocal" runat="server">PART MANAGEMENT LIST</asp:Literal>

            </td>
        </tr>
        <tr>

            <td colspan="6">
                <asp:Repeater ID="rpMold" runat="server" OnItemCommand="rpMold_ItemCommand" DataSourceID="SqlDataSource1">
                    <HeaderTemplate>
                        <table class="table-list" id="tblMold">
                            <tr>
                                <th>
                                    <asp:CheckBox ID="chkHeader" runat="server" /></th>
                                <th>MOLD CODE</th>
                                <th>MOLD NAME</th>
                                <th>VENDER</th>
                                <th>CAPACITY</th>
                                <th>MACHINE</th>
                                <th>ACTIVE</th>
                                 <th></th>
                            </tr>
                    </HeaderTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkRow" runat="server" /></td>
                            <td>
                                <asp:Literal ID="ltrMoldCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MOLD_CODE")%>'></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="ltrMoldName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MOLD_NAME")%>'></asp:Literal>
                                
                            </td>
                            <td>
                                <asp:Literal ID="ltrVender" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "VENDER")%>'></asp:Literal>
                                
                            </td>
                            <td>
                                <asp:Literal ID="ltrCapa" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CAPACITY")%>'></asp:Literal>
                                
                            </td>
                            <td>
                                <asp:Literal ID="ltrMachine" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MACHINE")%>'></asp:Literal>
                                
                            </td>
                            <td>
                                <asp:Literal ID="ltrActive" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ACTIVE")%>'></asp:Literal>
                                
                            </td>
                            <td>
                                <asp:Literal ID="ltrOrg" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ORG_ID")%>'></asp:Literal>
                                
                            </td>
                              <td>
                                <a href="./MoldAdd.aspx?MoldCode=<%#DataBinder.Eval(Container.DataItem, "MOLD_CODE")%>">Edit</a>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkRow" runat="server" /></td>
                            <td>
                                <asp:Literal ID="ltrMoldCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MOLD_CODE")%>'></asp:Literal>
                                
                            </td>
                            <td>
                                <%--<%#DataBinder.Eval(Container.DataItem, "MOLD_NAME")%>--%>
                                <asp:Literal ID="ltrMoldName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MOLD_NAME")%>'></asp:Literal>
                            </td>
                            <td>
                                <%--<%#DataBinder.Eval(Container.DataItem, "VENDER")%>--%>
                                <asp:Literal ID="ltrVender" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "VENDER")%>'></asp:Literal>
                            </td>
                            <td>
                                <%--<%#DataBinder.Eval(Container.DataItem, "CAPACITY")%>--%>
                                <asp:Literal ID="ltrCapa" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CAPACITY")%>'></asp:Literal>
                            </td>
                            <td>
                                <%--<%#DataBinder.Eval(Container.DataItem, "MACHINE")%>--%>
                                <asp:Literal ID="ltrMachine" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MACHINE")%>'></asp:Literal>
                            </td>
                            <td>
                                <%--<%#DataBinder.Eval(Container.DataItem, "ACTIVE")%>--%>
                                <asp:Literal ID="ltrActive" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ACTIVE")%>'></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="ltrOrg" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ORG_ID")%>'></asp:Literal>
                                
                            </td>
                            <td>
                                <a href="./MoldAdd.aspx?MoldCode=<%#DataBinder.Eval(Container.DataItem, "MOLD_CODE")%>">Edit</a>
                            </td>
                        </tr>

                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Style="height: 26px; color: red;" OnClick="btnDelete_Click" OnClientClick="return confirm('Do you want to delete?');" />
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td>&nbsp;</td>
            <td></td>
        </tr>
    </table>
</asp:Content>
