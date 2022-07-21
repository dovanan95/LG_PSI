<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PartStockUpdate.aspx.cs" Inherits="PSI_WEBFORM.PartStockUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table class="table-list1" id="tblParts">
        <tr>
            <td colspan="6" class="table-title">UPDATE STOCK</td>
        </tr>
        <tr>
            <td style="width:100px;">ORG</td>
            <td style="width:200px;"><asp:DropDownList ID="ddlDropDownORG" runat="server" CssClass="control-dropdownlist">
                <asp:ListItem Value="-1">-ORG----</asp:ListItem>
                <asp:ListItem Value="255305">AVF</asp:ListItem>
                <asp:ListItem Value="260330">AVG</asp:ListItem>
                </asp:DropDownList></td>
            <td>
                <asp:Button ID="btnView" runat="server" Text="SEARCH" OnClick="btnView_Click" />
            </td>
        </tr>
    </table>
    <asp:Repeater ID="rpPartStock" runat="server">
        <HeaderTemplate>
            <table class="table-list">
                <tr>
                    <th>PART NUMBER</th>
                    <th>PART NAME</th>
                    <th>TOOL INFO</th>
                    <th>STOCK : <%# GetCurrentDateTime()%></th>
                </tr>
        </HeaderTemplate>
        <AlternatingItemTemplate>
            <tr>
                <td>
                    <asp:Literal ID="ltrPartNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PARTNUMBER")%>'></asp:Literal></td>
                <td><%#DataBinder.Eval(Container.DataItem, "PARTNAME")%></td>
                 <td>
                    <asp:TextBox ID="txtToolInfo"   runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TOOL_MSG")%>' CssClass="control-input"></asp:TextBox></td>
                <td>
                    <asp:TextBox ID="txtStock" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "STOCK_QTY")%>' CssClass="control-input"></asp:TextBox></td>
            </tr>

        </AlternatingItemTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Literal ID="ltrPartNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PARTNUMBER")%>'></asp:Literal></td>
                <td><%#DataBinder.Eval(Container.DataItem, "PARTNAME")%></td>
                <td>
                    <asp:TextBox ID="txtToolInfo"   runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TOOL_MSG")%>' CssClass="control-input"></asp:TextBox></td>
                <td>
                    <asp:TextBox ID="txtStock"   runat="server" Text='<%# CheckStockDate(Eval("STOCK_QTY").ToString(), Eval("STOCK_DATE").ToString())%>' CssClass="control-input"></asp:TextBox></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Button ID="btnSave" runat="server" Text="UPDATE STOCK" OnClick="btnSave_Click" />
</asp:Content>
