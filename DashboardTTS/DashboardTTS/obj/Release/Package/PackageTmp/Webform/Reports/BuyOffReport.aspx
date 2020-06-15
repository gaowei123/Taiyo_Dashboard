<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="BuyOffReport.aspx.cs" Inherits="DashboardTTS.Webform.Reports.BuyOffReport" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server" >






    <div runat="server" id="divMain" style="width: auto; height: auto; align-items:center;margin:auto">
            <table style ="padding: 0px 10px 0px 10px; width: 100%; border-collapse: separate; border-spacing: 1px; table-layout:fixed; line-height: 10px; vertical-align: 10%;">  
                
                <%--title--%>                           
                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;"> 
                        <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                        <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Buy off Report" Font-Size="12pt" ForeColor="White"/>
                    </td>
                </tr>
                <%--title--%>



                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px; "> 
                        <table>
                            <tr>
                                <td style="width:50px;padding: 0px 10px 0px 10px;">
                                    <asp:Label runat="server" Text="Date: "></asp:Label>
                                </td>
                                <td style="width:150px;padding: 0px 10px 0px 10px;">
                                    <asp:TextBox runat="server" ID="txtDate" Width="100%"  Height="23px" AutoCompleteType="Disabled"  CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd" AutoPostBack="true" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                                </td>
                                <td style="width:200px;padding: 0px 10px 0px 10px;">
                                    <asp:DropDownList runat="server" ID="ddlJobList" Width="200px" Height="23px" AutoPostBack="true" OnSelectedIndexChanged="ddlJobList_SelectedIndexChanged"></asp:DropDownList>
                                </td>

                                <asp:TextBox runat="server" ID="txtJobnumber" Visible="false"></asp:TextBox>
                            </tr>
                        </table>
                    </td>
                </tr>



                <%--LASER 1st PIECE BUY-OFF RECORD--%>
                <tr style ="width: 100%">
                    <td > 
                        <table style =" width: 100%; padding: 0px; border-collapse: separate; border-spacing: 1px; table-layout:fixed; line-height: 10px; vertical-align: 10%;">

                            <tr style ="width: 100%">              
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 35px;" colspan="16">
                                     <label runat="server" Font-Names="Arial Unicode MS"  Font-Size="12pt" ForeColor="White">LASER 1st PIECE BUY-OFF RECORD</label>
                                </td>
                            </tr>
                            <tr style ="width: 100%">              
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                                    <asp:Label runat="server" Text="Machine ID:" ></asp:Label> &nbsp;<asp:Label ID="lbMachineID" runat="server" Width="50%"></asp:Label>
                                </td>                   
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;" colspan="4">
                                    <asp:Label  runat="server" Text="Lot No:"></asp:Label> &nbsp;<asp:Label ID="lbLotNo" runat="server" Text="" Width="50%"></asp:Label>
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;"  colspan="4">
                                    <asp:Label  runat="server" Text="Lot Qty (PCS):"></asp:Label> &nbsp;<asp:Label ID="lblLotQty" runat="server" Text="" Width="50%"></asp:Label>
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;" colspan="4" >
                                    <asp:Label runat="server" Text="Part No:"></asp:Label> &nbsp;<asp:Label ID="lbPartNo" runat="server" Text="" Width="70%"></asp:Label>
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;"  colspan="2">
                                    <asp:Label runat="server" Text="Current:"></asp:Label> &nbsp; <asp:Label ID="lbCurrent" runat="server" Text="" Width="50%"></asp:Label>
                                </td>
                               
                            </tr>
                            <tr style ="width: 100%">     
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 30%; font-family: 'Arial Unicode MS'; height: 50px;" rowspan="2" align="center" colspan="2"> 
                                     <asp:Label runat="server" Text="Set-Up" ></asp:Label>
                                </td>
                                <td style=" border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" colspan="8" align="center">
                                    <asp:Label runat="server" Text="Appeararance check with illumination Light" ></asp:Label>
                                </td>
                                <td style="border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="6" align="center">
                                    <asp:Label runat="server" Text="Graphic condition" Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr style ="width: 100%">
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" align="center">
                                    <asp:Label  runat="server" Text="Black Mark" Width="100%"></asp:Label>
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2" align="center">
                                    <asp:Label  runat="server" Text="Black Dot" Width="100%" ></asp:Label>
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2" align="center">
                                    <asp:Label  runat="server" Text="Pin Hole" Width="100%"></asp:Label>
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2" align="center">
                                    <asp:Label  runat="server" Text="Jagged" Width="100%" ></asp:Label>
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2" align="center">
                                    <asp:Label runat="server" Text="Check Guide" Width="100%" ></asp:Label>
                                </td>  
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2" align="center">
                                    <asp:Label runat="server" Text="Navitas" Width="100%" ></asp:Label>
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2" align="center">
                                    <asp:Label  runat="server" Text="Smart Scope" Width="100%" ></asp:Label>
                                </td>
                            </tr>

                            <tr style ="width: 100%">     
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center" colspan="2">
                                <asp:Label runat="server" Text="1st" Width="100%" style="padding:initial" ></asp:Label>
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                    <asp:CheckBox ID="cb_Black_Mark_OK" Text="OK" runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false" />  
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                    <asp:CheckBox ID="cb_Black_Mark_NG" Text="NG" runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false"  /> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Black_Dot_OK" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false" /> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Black_Dot_NG" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"   Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Pin_Hole_OK" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"   Enabled="false" /> 
                                </td>  
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Pin_Hole_NG" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false" /> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Jagged_OK" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"   Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Jagged_NG" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Check_Guled_OK" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                               <asp:CheckBox ID="cb_Check_Guled_NG" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;" Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                               <asp:CheckBox ID="cb_Navitas_OK" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                               <asp:CheckBox ID="cb_Navitas_NG" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Smart_Scope_OK" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;" Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Smart_Scope_NG" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false"/> 
                                </td>
                            </tr>
                            <tr style ="width: 100%">     
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center" colspan="2">
                                <asp:Label runat="server" Text="2nd" Width="100%"  Height="16px" ></asp:Label>
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                    <asp:CheckBox ID="cb_Black_Mark_OK1" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false"/>  
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                    <asp:CheckBox ID="cb_Black_Mark_NG1" Text="NG" runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false"/>  
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Black_Dot_OK1" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;" Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Black_Dot_NG1" Text="NG"  runat="server"  style="padding:2px 2px 2px 2px;"  Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Pin_Hole_OK1" Text="OK"  runat="server"  style="padding:2px 2px 2px 2px;" Enabled="false"/> 
                                </td>  
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Pin_Hole_NG1" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false" /> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Jagged_OK1" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;" Enabled="false" /> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Jagged_NG1" Text="NG"  runat="server"  style="padding:2px 2px 2px 2px;"  Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Check_Guled_OK1" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false" /> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                               <asp:CheckBox ID="cb_Check_Guled_NG1" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;" Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                               <asp:CheckBox ID="cb_Navitas_OK1" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                               <asp:CheckBox ID="cb_Navitas_NG1" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Smart_Scope_OK1" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false" /> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Smart_Scope_NG1" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false"/> 
                                </td>
                            </tr>
                            <tr style ="width: 100%">     
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center" colspan="2">
                                <asp:Label runat="server" Text="In-process" Width="115%" ></asp:Label>
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                    <asp:CheckBox ID="cb_Black_Mark_OK2" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false"/>  
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                    <asp:CheckBox ID="cb_Black_Mark_NG2" Text="NG" runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false"/>  
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Black_Dot_OK2" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Black_Dot_NG2" Text="NG"  runat="server"  style="padding:2px 2px 2px 2px;" Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Pin_Hole_OK2" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false"/> 
                                </td>  
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Pin_Hole_NG2" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"  Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Jagged_OK2" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;" Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Jagged_NG2" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;" Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Check_Guled_OK2" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;" Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                               <asp:CheckBox ID="cb_Check_Guled_NG2" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;" Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                               <asp:CheckBox ID="cb_Navitas_OK2" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;" Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                               <asp:CheckBox ID="cb_Navitas_NG2" Text="NG"  runat="server"  style="padding:2px 2px 2px 2px;" Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Smart_Scope_OK2" Text="OK"  runat="server"  style="padding:2px 2px 2px 2px;" Enabled="false"/> 
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                                <asp:CheckBox ID="cb_Smart_Scope_NG2" Text="NG"  runat="server"  style="padding:2px 2px 2px 2px;" Enabled="false"/> 
                                </td>
                            </tr>

                            <tr style ="width: 100%">  
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2">
                                    <asp:Label runat="server" Text="MC Operator:" Width="100%"></asp:Label>
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2">
                                    <asp:Label ID="lbMCOperator" runat="server" Text="" Width="100%" ></asp:Label>
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                                    <asp:Label runat="server" Text="Buyoff By:" Width="100%"></asp:Label>
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                                    <asp:Label ID="lbBuyoffBy" runat="server" Text="" Width="100%" ></asp:Label>
                                </td>       
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2">
                                    <asp:Label runat="server" Text="Approved By:" Width="100%" ></asp:Label>
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2">
                                    <asp:Label ID="lbApprovedBy" runat="server" Text="" Width="100%" ></asp:Label>
                                </td>       
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                                    <asp:Label runat="server" Text="Check By:" Width="100%" ></asp:Label>
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                                    <asp:Label ID="lbCheckBy" runat="server" Text="" Width="100%"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <%--LASER 1st PIECE BUY-OFF RECORD--%>



                <tr style ="width: 100%">
                    <td>
                        <table style =" width: 100%; padding: 0px; border-collapse: separate; border-spacing: 1px; table-layout:fixed; line-height: 10px; vertical-align: 10%;">

                            <tr style ="width: 100%">              
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 35px;" colspan="16">
                                     <label runat="server" Font-Names="Arial Unicode MS"  Font-Size="12pt" ForeColor="White">Laser Machine Setting</label>
                                </td>
                            </tr>
                            <tr style ="width: 100%">              
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;" colspan="4">
                                    Rate: <asp:Label  runat="server" ID="lbRate"></asp:Label>
                                </td>                   
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;" colspan="4">
                                    Frequency: <asp:Label  runat="server" ID="lbFrequency"></asp:Label>
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;"  colspan="4">
                                    Power%: <asp:Label  runat="server" ID="lbPower"></asp:Label>%
                                </td>
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;" colspan="4" >
                                    Repeat: <asp:Label  runat="server" ID="lbRepeat"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>


               



                <%--PQC INSPECTION RECORD FOR MOULDING--%>
                <tr style ="width: 100%">
                    <td > 
                        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 1px;  table-layout:fixed; line-height: 10px; vertical-align: 10%;">

                            <tr style ="width: 100%">              
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 35px;" colspan="16">
                                     <label runat="server" Font-Names="Arial Unicode MS"  Font-Size="12pt" ForeColor="White">PQC INSPECTION RECORD FOR MOULDING</label>
                                </td>
                            </tr>

                            <tr style ="width: 100%">              
                               <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="16"> 
                                    <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dgPQCMouldingInspection" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" >
                                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                        <EditItemStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <Columns> 
                                           
                                        </Columns>
                                    </asp:DataGrid>
                                   <br/>
                                   <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dgPQCMouldingInspection02" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" >
                                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                        <EditItemStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <Columns> 
                                           
                                        </Columns>
                                    </asp:DataGrid>
                                </td>
                            </tr>
                            
                        </table>
                    </td>
                </tr>
                <%--PQC INSPECTION RECORD FOR MOULDING--%>


                
                <%--PQC INSPECTION RECORD FOR PAINTING--%>
                <tr style ="width: 100%">
                    <td > 
                        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 1px; table-layout:fixed; line-height: 10px; vertical-align: 10%; ">  

                            <tr style ="width: 100%">              
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 35px;" colspan="16">
                                     <label runat="server" Font-Names="Arial Unicode MS"  Font-Size="12pt" ForeColor="White">PQC INSPECTION RECORD FOR PAINTING</label>
                                </td>
                            </tr>

                            <tr style ="width: 100%">              
                               <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;" colspan="16"> 
                                   <asp:DataGrid runat="server" CssClass="table table-hover" ID ="dgPQCPaintingInspection" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" Width ="100%" style="line-height:12px">
                                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                        <EditItemStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />


                                    </asp:DataGrid>
                                </td>
                            </tr>
                            
                        </table>
                    </td>
                </tr>
                <%--PQC INSPECTION RECORD FOR PAINTING--%>



                <%--PQC INSPECTION RECORD FOR LASER--%>
                <tr style ="width: 100%">
                    <td >
                        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 1px; table-layout:fixed; line-height: 10px; vertical-align: 10%; ">

                            <tr style ="width: 100%">              
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 35px;" colspan="16">
                                     <label runat="server" Font-Names="Arial Unicode MS"  Font-Size="12pt" ForeColor="White">PQC INSPECTION RECORD FOR LASER</label>
                                </td>
                            </tr>

                            <tr style ="width: 100%">              
                               <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="16"> 
                                    <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dgPQCLaserInspection" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" >
                                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                        <EditItemStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <Columns> 
                                           
                                        </Columns>
                                    </asp:DataGrid>
                                </td>
                            </tr>
                            
                        </table>
                    </td>
                </tr>
                <%--PQC INSPECTION RECORD FOR LASER--%>

                <%--PQC INSPECTION RECORD FOR OTHERS--%>
                <tr style ="width: 100%">
                    <td >
                        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 1px; table-layout:fixed; line-height: 10px; vertical-align: 10%; ">

                            <tr style ="width: 100%">              
                                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 35px;" colspan="16">
                                     <label runat="server" Font-Names="Arial Unicode MS"  Font-Size="12pt" ForeColor="White">PQC INSPECTION RECORD FOR OTHERS</label>
                                </td>
                            </tr>

                            <tr style ="width: 100%">              
                               <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="16"> 
                                    <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dgPQCOthersInspection" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" >
                                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                        <EditItemStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <Columns> 
                                           
                                        </Columns>
                                    </asp:DataGrid>
                                </td>
                            </tr>
                            
                        </table>
                    </td>
                </tr>
                <%--PQC INSPECTION RECORD FOR OTHERS--%>


           </table>
    </div>



    <link href="../../plugins/bootstrap-datetimepicker-master/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <script src="../../plugins/bootstrap-datetimepicker-master/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../../plugins/bootstrap-datetimepicker-master/js/locales/bootstrap-datetimepicker.zh-CN.js"></script>

    <script type="text/javascript">
        $('.formDateTimePicker').datetimepicker({
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            minView: 2,
            forceParse: 0,
            initialDate: new Date()
        });
    </script>



 </asp:Content>