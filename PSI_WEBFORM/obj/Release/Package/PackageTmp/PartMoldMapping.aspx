<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PartMoldMapping.aspx.cs" Inherits="PSI_WEBFORM.PartMoldMapping" %>

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
    <table>
        <tr>
            <td colspan="6">
                <asp:Literal ID="lrtInsertTitle" runat="server">ADD MOLD</asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="td-label">PART NUMBER</td>
            <td class="td-control">
                <asp:TextBox ID="txtPartNumber" runat="server" CssClass="control-input"></asp:TextBox></td>
            <td class="td-tip"></td>
            <td class="td-label">MOLD NAME</td>
            <td class="td-control">
                <asp:DropDownList ID="ddlMold" runat="server" CssClass="control-dropdownlist" OnSelectedIndexChanged="ddlMold_SelectedIndexChanged"></asp:DropDownList>
                <td class="td-tip"></td>
        </tr>
        <tr>
            <td class="td-label">RATE</td>
            <td class="td-control">
                <asp:TextBox ID="txtRate" runat="server" CssClass="control-input"></asp:TextBox></td>
            <td class="td-tip"></td>
            <td class="td-label"></td>
            <td class="td-control">

                <asp:Button ID="btnMapping" runat="server" Text="Mapping" CssClass="control-button" OnClick="btnMapping_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="control-button" />
            </td>
            <td class="td-tip"></td>
        </tr>

        <tr>
            <td colspan="6">
                <asp:Literal ID="ltrTitleListPartLocal" runat="server">MODL MAPPING</asp:Literal>

            </td>
        </tr>
        <tr>

            <td colspan="6">
                <div class="list-data">
                    <asp:Repeater ID="rpPartMold" runat="server">
                        <HeaderTemplate>
                            <table class="table-list" id="tblMold">
                                <tr>
                                    <th>
                                        <asp:CheckBox ID="chkHeader" runat="server" /></th>
                                    <th>PART NUMBER</th>
                                    <th>PART NAME</th>
                                    <th>MOLD CODE</th>
                                    <th>MOLD NAME</th>
                                    <th>RATE</th>
                                    <th>CAPACITY</th>
                                    <th>ACTIVE</th>
                                </tr>
                        </HeaderTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkRow" runat="server" /></td>
                                <td>
                                    <asp:Literal ID="ltrPartNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PARTNUMBER")%>'></asp:Literal>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "PARTNAME")%>
                                </td>
                                <td>
                                    <asp:Literal ID="ltrMoldCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MOLD_CODE")%>'></asp:Literal>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "MOLD_NAME")%>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RATE")%>'></asp:TextBox>
                                    <asp:Literal ID="ltrRate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RATE")%>' Visible="false"></asp:Literal>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "CAPACITY")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "ACTIVE")%>
                                </td>
                                <td>
                                    <a href="./PartMoldMapping.aspx?PartNumber=<%#DataBinder.Eval(Container.DataItem, "PARTNUMBER")%>&MoldCode=<%#DataBinder.Eval(Container.DataItem, "MOLD_CODE")%>">Edit</a>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkRow" runat="server" /></td>
                                <td>
                                    <asp:Literal ID="ltrPartNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PARTNUMBER")%>'></asp:Literal>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "PARTNAME")%>
                                </td>
                                <td>
                                    <asp:Literal ID="ltrMoldCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MOLD_CODE")%>'></asp:Literal>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "MOLD_NAME")%>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RATE")%>'></asp:TextBox>
                                    <asp:Literal ID="ltrRate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RATE")%>' Visible="false"></asp:Literal>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "CAPACITY")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "ACTIVE")%>
                                </td>
                                <td>
                                    <a href="./PartMoldMapping.aspx?PartNumber=<%#DataBinder.Eval(Container.DataItem, "PARTNUMBER")%>&MoldCode=<%#DataBinder.Eval(Container.DataItem, "MOLD_CODE")%>">Edit</a>
                                </td>
                            </tr>

                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td>
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Style="height: 26px; color: red;" OnClientClick="return confirm('Do you want to delete?');" OnClick="btnDelete_Click" /></td>
            <td></td>
        </tr>
    </table>
</asp:Content>
