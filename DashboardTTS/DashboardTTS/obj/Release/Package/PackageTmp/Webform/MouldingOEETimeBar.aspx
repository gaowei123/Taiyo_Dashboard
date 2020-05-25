<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MouldingOEETimeBar.aspx.cs" Inherits="DashboardTTS.Webform.MouldingOEETimeBar" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.UltraWebChart" tagprefix="igchart" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.UltraChart.Resources.Appearance" tagprefix="igchartprop" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.UltraChart.Data" tagprefix="igchartdata" %>
<%@ Register src="../UserControl/WebUserControlTimeBar.ascx" tagname="WebUserControlTimeBar" tagprefix="uc1" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
       
<link href="../Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />  
<link href="/Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />


    <div style="width: 1650px; height: 257px; align-items:center;margin:auto">
        <div style =" width: 100%">
            <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                    <img src="../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="LMMS Dashboard" Font-Size="12" ForeColor="White"></Asp:label>
                    </td>
                </tr>
                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="Machine No :" Width="100px"></asp:Label>  
                    </td>
                    <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px;">
                        <asp:DropDownList ID="ddlMachineID"  runat="server" Width="100%" Height="23px"  >
                            <asp:ListItem Value="">All</asp:ListItem>
                            <asp:ListItem Value="1">No.1</asp:ListItem>
                            <asp:ListItem Value="2">No.2</asp:ListItem>
                            <asp:ListItem Value="3">No.3</asp:ListItem>
                            <asp:ListItem Value="4">No.4</asp:ListItem>
                            <asp:ListItem Value="5">No.5</asp:ListItem>
                            <asp:ListItem Value="6">No.6</asp:ListItem>
                            <asp:ListItem Value="7">No.7</asp:ListItem>
                            <asp:ListItem Value="8">No.8</asp:ListItem>
                            <asp:ListItem Value="9">No.9</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                <%--    <asp:Label runat="server" Text="Part No :" Width="100px"></asp:Label> --%>
                    </td>
                    <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                  <%--  <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txtPartNo" Width="100%" BorderStyle="Solid" BorderWidth="1px"  ></asp:TextBox>--%>
                    </td>                       
                    <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
                    </td>
                </tr>
                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                        <asp:Label runat="server" Text="From :" Width="100px"></asp:Label>  
                    </td>
                    <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                        <igsch:webdatechooser ID="infDchFrom" runat="server"   Width="100%" Value="" > </igsch:webdatechooser>
                    </td>
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="To :"></asp:Label> 

                    </td>
                    <td style =" border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                        <igsch:webdatechooser ID="infDchTo" runat="server"   Width="100%"></igsch:webdatechooser></td>
                                                      
                    <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Height="30px" />
                    </td>
                </tr>
                <tr>

                    <td align="right" style = "padding: 25px 25px 25px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="5"> <%-- ForeColor ="Green"  BackColor ="green"--%>
                        <asp:Label ID="lb_Color_Running" Text ="&nbsp;&nbsp;"  runat="server"/>&nbsp;<asp:Label ID="lb_Text_Running" Font-Bold="true" runat="server"></asp:Label>
                        &nbsp;&nbsp;&nbsp; 
                        <asp:Label ID="lb_Color_Adjustment" Text ="&nbsp;&nbsp;"  runat="server"/>&nbsp;<asp:Label ID="lb_Text_Adjustment" Font-Bold="true" runat="server"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lb_Color_Mould_Testing" Text ="&nbsp;&nbsp;"  runat="server"/>&nbsp;<asp:Label ID="lb_Text_Mould_Testing" Font-Bold="true" runat="server"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lb_Color_Material_Testing" Text ="&nbsp;&nbsp;"  runat="server"/>&nbsp;<asp:Label ID="lb_Text_Material_Testing" Font-Bold="true" runat="server"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lb_Color_Change_Model" Text ="&nbsp;&nbsp;"  runat="server"/>&nbsp;<asp:Label ID="lb_Text_Change_Model" Font-Bold="true" runat="server"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lb_Color_No_Schedule" Text ="&nbsp;&nbsp;"  runat="server"/>&nbsp;<asp:Label ID="lb_Text_No_Schedule" Font-Bold="true" runat="server"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lb_Color_No_Operator" Text ="&nbsp;&nbsp;"  runat="server"/>&nbsp;<asp:Label ID="lb_Text_No_Operator" Font-Bold="true" runat="server"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lb_Color_No_Material" Text ="&nbsp;&nbsp;"  runat="server"/>&nbsp;<asp:Label ID="lb_Text_No_Material" Font-Bold="true" runat="server"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lb_Color_Break_Time" Text ="&nbsp;&nbsp;"  runat="server"/>&nbsp;<asp:Label ID="lb_Text_Break_Time" Font-Bold="true" runat="server"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lb_Color_ShutDown" Text ="&nbsp;&nbsp;"  runat="server"/>&nbsp;<asp:Label ID="lb_Text_ShutDown" Font-Bold="true" runat="server"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lb_Color_Login_Out" Text ="&nbsp;&nbsp;"  runat="server"/>&nbsp;<asp:Label ID="lb_Text_Login_Out" Font-Bold="true" runat="server"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lb_Color_BreakDown" Text ="&nbsp;&nbsp;"  runat="server"/>&nbsp;<asp:Label ID="lb_Text_BreakDown" Font-Bold="true" runat="server"></asp:Label>
                    </td>
                </tr>   
                <tr id="trChart" style ="width: 100%">
                    <td align="center" style = "padding: 25px 25px 25px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="5" aria-hidden="False" aria-multiline="True" aria-multiselectable="True" aria-orientation="vertical" draggable="false">
                        <uc1:WebUserControlTimeBar ID="WebUserControlTimeBar1" runat="server"    /> 
                        <uc1:WebUserControlTimeBar ID="WebUserControlTimeBar2" runat="server"    />
                        <uc1:WebUserControlTimeBar ID="WebUserControlTimeBar3" runat="server"    />
                        <uc1:WebUserControlTimeBar ID="WebUserControlTimeBar4" runat="server"    />
                        <uc1:WebUserControlTimeBar ID="WebUserControlTimeBar5" runat="server"    />
                        <uc1:WebUserControlTimeBar ID="WebUserControlTimeBar6" runat="server"    />
                        <uc1:WebUserControlTimeBar ID="WebUserControlTimeBar7" runat="server"    />
                        <uc1:WebUserControlTimeBar ID="WebUserControlTimeBar8" runat="server"    />
                        <uc1:WebUserControlTimeBar ID="WebUserControlTimeBar9" runat="server"    />
                     </td>
                </tr>
                                                      
            </table> 
        </div>
    </div>
</asp:Content>
