<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PartList.aspx.cs" Inherits="PSI_WEBFORM.PartList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td class="td-label">PARTNUMBER</td>
            <td class="td-control">
                <asp:TextBox ID="txtPartNumber" runat="server" CssClass="control-input"></asp:TextBox></td>
            <td class="td-tip"></td>
            <td class="td-label">MODEL</td>
            <td class="td-control">
                <asp:TextBox ID="txtModel" runat="server" CssClass="control-input"></asp:TextBox></td>
            <td class="td-tip"></td>
        </tr>
        <tr>
            <td class="td-label">LEVEL</td>
            <td class="td-control">
                <asp:DropDownList ID="ddlLevel" runat="server" CssClass="control-dropdownlist">
                    <asp:ListItem Value="-1">All</asp:ListItem>
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                </asp:DropDownList>    
            </td>
            <td class="td-tip"></td>
            <td class="td-label"></td>
            <td class="td-control" colspan="2">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="control-button" OnClick="btnSearch_Click" />
                 <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="control-button" OnClick="btnAdd_Click" />

            </td>
        </tr>
    </table>

    <div id="list-data">
        <asp:Repeater ID="rpData" runat="server">
            <HeaderTemplate>
                <table class="table-list">
                    <tr>
                        <th>CHECK</th>
                        <th>MODEL</th>
                        <th>PARNUMBER</th>
                        <th>PARTNAME</th>
                         <th></th>
                    </tr>
            </HeaderTemplate>
            <AlternatingItemTemplate>
            </AlternatingItemTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:CheckBox ID="ckCheck" runat="server" /></td>
                    <td>
                        <%#DataBinder.Eval(Container.DataItem, "PRODID")%>
                    </td>
                    <td>
                        <%#DataBinder.Eval(Container.DataItem, "CHILD_MTRLID")%>
                    </td>
                    <td>
                        <%#DataBinder.Eval(Container.DataItem, "CHILD_MTRLNAME")%>
                    </td>
                     <td>
                         <a href="./PartMoldMapping.aspx?PartNumber=<%#DataBinder.Eval(Container.DataItem, "CHILD_MTRLNAME")%>"
                        
                    </td>
                </tr>

            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

    </div>
</asp:Content>
