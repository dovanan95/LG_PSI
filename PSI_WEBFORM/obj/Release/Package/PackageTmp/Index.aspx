<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PSI_WEBFORM.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style6 {
            width: 240px;
            height: 45px;
        }
        .auto-style8 {
            width: 260px;
            height: 45px;
        }
        .auto-style9 {
            width: 270px;
            height: 45px;
        }
        .auto-style10 {
            width: 120px;
            height: 45px;
        }
        .auto-style11 {
            width: 178px;
            height: 45px;
        }
        .auto-style12 {
            width: 350px;
            height: 45px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="table-list1">
        <tr>
            <td colspan="6" class="table-title">
                <asp:Literal ID="lrtInsertTitle" runat="server">STOCK STATUS</asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="auto-style12">ORGANIZATION CODE :
                <asp:DropDownList ID="ddlDropDownORG" runat="server" CssClass="control-dropdownlist" OnSelectedIndexChanged="ddlDropDownORG_SelectedIndexChanged" Width="100px">
                <asp:ListItem Value="-1">----ORG----</asp:ListItem>
                <asp:ListItem Value="255305">AVF</asp:ListItem>
                <asp:ListItem Value="260330">AVG</asp:ListItem>
                </asp:DropDownList>
                </td>
            <td class="auto-style9">
                
                TOOL INFO :
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" CssClass="control-dropdownlist" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="100px">
                    <asp:ListItem>ALL</asp:ListItem>
              
                </asp:DropDownList>
                </td>
            <td class="auto-style6"> VENDER :

                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" CssClass="control-dropdownlist" Width="100px">    
                <asp:ListItem Value="-1">--VENDER--</asp:ListItem>
                <asp:ListItem Value="DY">Dong Yang</asp:ListItem>
                <asp:ListItem Value="DJ">Dong Jin</asp:ListItem>
                <asp:ListItem Value="HM">Hanmi</asp:ListItem>
                <asp:ListItem Value="DA">Dong A</asp:ListItem>
                <asp:ListItem Value="HS">HaengSung</asp:ListItem>
                <asp:ListItem Value="SL">SL</asp:ListItem>
                <asp:ListItem Value="VL">Van Long</asp:ListItem>
                <asp:ListItem Value="PD">Phuong Dong</asp:ListItem>
                <asp:ListItem Value="CM">Comet</asp:ListItem>
                <asp:ListItem Value="HL">Halla</asp:ListItem>
                <asp:ListItem Value="HN">Hanam</asp:ListItem>
                <asp:ListItem Value="IN">Inavina</asp:ListItem>
                <asp:ListItem Value="4P">4P Electronics </asp:ListItem>
                </asp:DropDownList>
                </td>
            <td class="auto-style10">
                <asp:Button ID="Button2" runat="server" Text="VIEW STATUS" CssClass="control-button" OnClick="btnView1_Click" Width="168px" />
            
                
            </td>
            <td class="auto-style11"> 
                <asp:Button ID="Button1" runat="server" Text="LOAD DATA" OnClick="Button1_Click" />    
              
                </td>
            <td>
                
                
            </td>
            <td class="auto-style11">
                <%-- <asp:Button ID="btnView" runat="server" Text="VIEW STOCK STATUS" CssClass="control-button" OnClick="btnView_Click" Width="168px" />
                --%>
                
            </td>
            <td class="auto-style8">
              
                
            </td>
            
        </tr>
    </table>

    <%-- <tr>
            <td colspan="6" class="auto-style1">

                <table class="table-shedule">
                    <tr>
                        <td>Part Name</td>
                        <td>Part Number</td>
                        <td></td>
                        <td></td>
                        <td class="d1 day">22-Nov</td>
                        <td class="d2 day">23-Nov</td>
                        <td class="d3 day">24-Nov</td>
                        <td class="d4 day">25-Nov</td>
                        <td class="d5 day">26-Nov</td>
                        <td class="d6 day">27-Nov</td>
                        <td class="d7 day">28-Nov</td>
                        <td class="d8 day">29-Nov</td>
                        <td class="d9 day">30-Nov</td>
                        <td class="d10 day">1-Dec</td>
                        <td class="d11 day">2-Dec</td>
                        <td class="d12 day">3-Dec</td>
                        <td class="d13 day">4-Dec</td>
                        <td class="d14 day">6-Dec</td>
                        <td class="d15 day">7-Dec</td>
                        <td class="d16 day">8-Dec</td>
                        <td class="d17 day">9-Dec</td>
                        <td class="d18 day">10-Dec</td>
                        <td class="d19 day">11-Dec</td>
                        <td class="d20 day">12-Dec</td>
                        <td class="d21 day">13-Dec</td>
                        <td class="d22 day">14-Dec</td>
                        <td class="d23 day">15-Dec</td>
                        <td class="d24 day">16-Dec</td>
                        <td class="d25 day">17-Dec</td>
                        <td class="d26 day">18-Dec</td>
                        <td class="d27 day">19-Dec</td>
                        <td class="d28 day">20-Dec</td>
                        <td class="d29 day">21-Dec</td>
                        <td class="d30 day">22-Dec</td>
                    </tr>
                    <tr>
                        <td>Outer Tub FL 4.3</td>
                        <td>AJQ74053923</td>
                        <td>LG Plan</td>
                        <td></td>
                        <td class="p1 plan">1360</td>
                        <td class="p2 plan">1360</td>
                        <td class="p3 plan">1360</td>
                        <td class="p4 plan">1360</td>
                        <td class="p5 plan">1360</td>
                        <td class="p6 plan">1360</td>
                        <td class="p7 plan">1360</td>
                        <td class="p8 plan">1360</td>
                        <td class="p9 plan">1360</td>
                        <td class="p10 plan">1360</td>
                        <td class="p11 plan">1360</td>
                        <td class="p12 plan">1360</td>
                        <td class="p13 plan">1360</td>
                        <td class="p17 plan">1360</td>
                        <td class="p15 plan">1360</td>
                        <td class="p16 plan">1360</td>
                        <td class="p17 plan">1360</td>
                        <td class="p18 plan">1360</td>
                        <td class="p19 plan">1360</td>
                        <td class="p20 plan">1360</td>
                        <td class="p21 plan">1360</td>
                        <td class="p22 plan">1360</td>
                        <td class="p23 plan">1360</td>
                        <td class="p24 plan">1360</td>
                        <td class="p25 plan">1360</td>
                        <td class="p26 plan">1360</td>
                        <td class="p27 plan">1360</td>
                        <td class="p28 plan">1360</td>
                        <td class="p29 plan">1360</td>
                        <td class="p30 plan">1360</td>
                    </tr>
                    <tr>
                        <td>C/Time 120"/Cavity1</td>
                        <td>1800 A-06</td>
                        <td>Capcity</td>
                        <td></td>
                        <td class="c1 capa">1200</td>
                        <td class="c2 capa">1200</td>
                        <td class="c3 capa">1200</td>
                        <td class="c4 capa">1200</td>
                        <td class="c5 capa">1200</td>
                        <td class="c6 capa">1200</td>
                        <td class="c7 capa">1200</td>
                        <td class="c8 capa">1200</td>
                        <td class="c9 capa">1200</td>
                        <td class="c10 capa">1200</td>
                        <td class="c11 capa">1200</td>
                        <td class="c12 capa">1200</td>
                        <td class="c13 capa">1200</td>
                        <td class="c14 capa">1200</td>
                        <td class="c15 capa">1200</td>
                        <td class="c16 capa">1200</td>
                        <td class="c17 capa">1200</td>
                        <td class="c18 capa">1200</td>
                        <td class="c19 capa">1200</td>
                        <td class="c20 capa">1200</td>
                        <td class="c21 capa">1200</td>
                        <td class="c22 capa">1200</td>
                        <td class="c23 capa">1200</td>
                        <td class="c24 capa">1200</td>
                        <td class="c25 capa">1200</td>
                        <td class="c26 capa">1200</td>
                        <td class="c27 capa">1200</td>
                        <td class="c28 capa">1200</td>
                        <td class="c29 capa">1200</td>
                        <td class="c30 capa">1200</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>Stock</td>
                        <td>1588</td>
                        <td class="s1 stock">1,588</td>
                        <td class="s2 stock">1588</td>
                        <td class="s3 stock">1588</td>
                        <td class="s4 stock">1588</td>
                        <td class="s5 stock">1588</td>
                        <td class="s6 stock">1588</td>
                        <td class="s7 stock">1588</td>
                        <td class="s8 stock">1,588</td>
                        <td class="s9 stock">1588</td>
                        <td class="s10 stock">1588</td>
                        <td class="s11 stock">1588</td>
                        <td class="s12 stock">1588</td>
                        <td class="s13 stock">1588</td>
                        <td class="s14 stock">1588</td>
                        <td class="s15 stock">1,588</td>
                        <td class="s16 stock">1588</td>
                        <td class="s17 stock">1588</td>
                        <td class="s18 stock">1588</td>
                        <td class="s19 stock">1588</td>
                        <td class="s20 stock">1588</td>
                        <td class="s21 stock">1588</td>
                        <td class="s22 stock">1,588</td>
                        <td class="s23 stock">1588</td>
                        <td class="s24 stock">1588</td>
                        <td class="s25 stock">1588</td>
                        <td class="s26 stock">1588</td>
                        <td class="s27 stock">1588</td>
                        <td class="s28 stock">1,588</td>
                        <td class="s28 stock">1588</td>
                        <td class="s30 stock">1588</td>
                    </tr>

                </table>
            </td>
        </tr>
    --%>

    <asp:Literal ID="ltrHTML" runat="server"></asp:Literal>

</asp:Content>
