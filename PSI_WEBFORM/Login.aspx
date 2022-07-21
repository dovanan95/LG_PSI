<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PSI_WEBFORM.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="./Content/css.css" rel="stylesheet" />
    <script src="./Scripts/jquery-1.8.2.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="header">
                <div id ="banner">

                </div>
                <ul id="nav">
                    <li><a href="./Index.aspx">STOCK STATUS</a></li>
                    <li><a href="./PartStockUpdate.aspx">UPDATE STOCK</a></li>
                    <li><a href="./MoldSchedule.aspx">MOLD SCHEDULE</a></li>
                    <li><a href="./PartAdd.aspx">PART MANAGEMENT</a></li>
                    <li><a href="./MoldAdd.aspx">MOLD MANAGEMENT</a></li>
                </ul>
            </div>
            <div id="body">
                <table style="margin-top: 30px; margin-left: 50px; margin-bottom: 300px;">

                    <tr>
                        <td colspan="3" class="table-list1 table-title">LOGIN</td>
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
                        <td class="td-label">PASSWORD</td>
                        <td class="td-control">
                            <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="control-input" runat="server"></asp:TextBox></td>
                        <td class="td-tip"></td>
                    </tr>
                    <tr>
                        <td class="td-label"></td>
                        <td class="td-control">
                            <asp:Button ID="btnLogin" runat="server" Text="LOGIN" OnClick="btnLogin_Click" /></td>

                        <td class="td-tip"></td>
                    </tr>

                </table>
            </div>
        </div>
    </form>
</body>
</html>
