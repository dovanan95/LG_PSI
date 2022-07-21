<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Editpart.aspx.cs" Inherits="PSI_WEBFORM.Editpart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <table>
        <tr>
            <td>PART NUMBER</td>
            <td><asp:TextBox ID="txtPartNumber" runat="server" CssClass="control-input"></asp:TextBox></td>
        </tr>
       <tr>
           <td>VENDER</td>
           <td><asp:DropDownList ID="DropDownList1" runat="server" CssClass="control-dropdownlist" Width="70px"></asp:DropDownList></td>
       </tr>
       <tr>
            <td><asp:Button ID="btnSave" runat="server" Text="Save " CssClass="control-button" OnClick="btnSave_Click" Width="57px" /></td>
           
       </tr>
   </table>
         
    <%-- PART NUMBER
            
                <asp:TextBox ID="txtPartNumber" runat="server" CssClass="control-input"></asp:TextBox>
     
            VENDER
            
                 <asp:DropDownList ID="DropDownList1" runat="server" CssClass="control-dropdownlist" Width="70px"></asp:DropDownList>
            <asp:Button ID="btnSave" runat="server" Text="Save " CssClass="control-button" OnClick="btnSave_Click" Width="57px" />--%>

     
</asp:Content>