<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="ChangeUser.aspx.cs" Inherits="PSI_WEBFORM.ChangeUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
                <table style="margin-top: 30px; margin-left: 50px; margin-bottom: 300px;">

                    <tr>
                        <td colspan="3" class="table-list1 table-title">CHANGE PASSWORD</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lbMessage" runat="server" BorderColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label">USER</td>
                        <td class="td-control">
                            <asp:TextBox ID="txtUser" CssClass="control-input" runat="server"></asp:TextBox></td>
                        <td class="td-tip"></td>
                    </tr>
                    <tr>
                        <td class="td-label">OLD PASSWORD</td>
                        <td class="td-control">
                            <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="control-input" runat="server"></asp:TextBox></td>
                        <td class="td-tip"></td>
                    </tr>
                    <tr>
                        <td class="td-label">NEW PASSWORD</td>
                        <td class="td-control">
                            <asp:TextBox ID="txtNewPassword" TextMode="Password" CssClass="control-input" runat="server"></asp:TextBox></td>
                        <td class="td-tip"></td>
                    </tr>
                    <tr>
                        <td class="td-label">CONFIRM PASSWORD</td>
                        <td class="td-control">
                            <asp:TextBox ID="txtconfirmPass" TextMode="Password" CssClass="control-input" runat="server"></asp:TextBox></td>
                        <td class="td-tip"></td>
                    </tr>
                    <tr>
                        <td class="td-label"></td>
                        <td class="td-control">
                            <asp:Button ID="btnSave" runat="server" Text="SAVE" OnClick="btnSAVE_Click" /></td>

                        <td class="td-tip"></td>
                    </tr>

                </table>
            
 </asp:Content>

