<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MoldSchedule.aspx.cs" Inherits="PSI_WEBFORM.MoldSchedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="table-list1">
        <tr>
            <td colspan="6" class="table-title">
                <asp:Literal ID="lrtInsertTitle" runat="server">FILTER CONDITION</asp:Literal>
            </td>
        </tr>

        <tr>
            <td class="td-label">ORGANIGATION</td>
            <td class="td-control">
                <asp:DropDownList ID="ddlORG" runat="server" CssClass="control-dropdownlist">
                    <asp:ListItem Value="-1">-ORG----</asp:ListItem>
                    <asp:ListItem Value="255305">AVF</asp:ListItem>
                    <asp:ListItem Value="260330">AVG</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="td-label">MOLD</td>
            <td class="td-control">
                <asp:DropDownList ID="ddlMold" runat="server" CssClass="control-dropdownlist" Width="200px"></asp:DropDownList>
                 <asp:DropDownList ID="DropDownList1" runat="server" CssClass="control-dropdownlist" Width="70px"></asp:DropDownList>
            </td>
            <td class="td-label"></td>
            <td class="td-control">
               

            </td>
        </tr>
        <tr>
            <td class="td-label">From</td>
            <td class="td-control">
                <asp:Calendar ID="cdStart" runat="server" ShowGridLines="True"></asp:Calendar>
            </td>
            <td class="td-label"></td>
            <td class="td-control">
                <asp:Button ID="btnView" runat="server" Text="View " CssClass="control-button" OnClick="btnView_Click" Width="60px" />
                <asp:Button ID="Button1" runat="server" Text="View ALL" CssClass="control-button" OnClick="btnViewAll_Click" Width="70px" />
                <asp:Button ID="btnAction" runat="server" Text="Update " CssClass="control-button" OnClick="btnAction_Click" Width="68px" />
                <asp:Button ID="btnSave" runat="server" Text="Save " CssClass="control-button" OnClick="btnSave_Click" Width="57px" />
            </td>
        </tr>
        <tr>
            <td colspan="6"></td>

        </tr>
    </table>
    <asp:Literal ID="ltrHTML" runat="server"></asp:Literal>
</asp:Content>
