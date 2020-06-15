<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add_BUY_OFF.aspx.cs" Inherits="DashboardTTS.Webform.Laser.BUY_OFF" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>

   

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >
    <link href="../css/Dashboard.css" rel="stylesheet" />

    <script>

        function moveToEnd(el) {
            if (el == document.activeElement) {
                if (typeof el.selectionStart == "number") {
                    el.selectionStart = el.selectionEnd = el.value.length;
                }
                else if (typeof el.createTextRange != "undefined") {
                    el.focus();
                    var range = el.createTextRange();
                    range.collapse(false);
                    range.select();
                }
            }
        }

        function SetOKcbColor(checkbox){
            if (checkbox.checked == true) {
                // alert('i need green');

                checkbox.background = "red";


                checkbox.setAttribute('background', 'red');

               // checkbox.setAttribute('color', 'red');
                //document.getElementById("cb_Black_Mark_OK").style.color = "blue";

            } else {
              //  checkbox.setAttribute('color', 'white');
              
            }
        }

    </script>

    <div runat="server" id="divMain" style="width: 85%; height: 257px; align-items:center;margin:auto">
            <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 1px; table-layout:fixed; line-height: 10px; vertical-align: 10%;">                            
                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="16"> 
                        <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                        <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Laser Piece Buy-Off Record" Font-Size="12pt" ForeColor="White" meta:resourcekey="lblUserHeaderResource1"/>
                    </td>
                </tr>   
                
                        
                <tr style ="width: 100%">              
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                     <asp:Label ID="lb_Machine_ID" runat="server" Text="Machine ID:" Width="100%" meta:resourcekey="lb_Machine_IDResource1"></asp:Label>
                    </td>                   
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                        <asp:DropDownList ID="ddlMachineID"  runat="server" Width="100%" Height="23px" OnSelectedIndexChanged="ddlMachineID_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;"  colspan="2">
                      <asp:Label ID="lb_JOB_ID" runat="server" Text="JOB ID:" Width="100%" meta:resourcekey="lb_JOB_IDResource1"></asp:Label>
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" >
                      <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_Job_ID" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoPostBack="true" onfocus="moveToEnd(this)" OnTextChanged="txt_Job_ID_TextChanged"></asp:TextBox>
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;"  colspan="2">
                      <asp:Label ID="lb_PartNo" runat="server" Text="Part No:" Width="100%" meta:resourcekey="lb_PartNoResource1"></asp:Label>
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" >
                      <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_Part_No" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"></asp:TextBox>
                    </td>
                     <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;"  colspan="2" >
                      <asp:Label ID="Label8" runat="server" Text="Initial Current:" Width="100%" meta:resourcekey="lb_PartNoResource1"></asp:Label>
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" >
                      <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_current" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"></asp:TextBox>
                    </td>
                </tr>
                <tr style ="width: 100%">     
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 30%; font-family: 'Arial Unicode MS'; height: 50px;" rowspan="2" align="center" colspan="2"> 
                         <asp:Label ID="lblDate" runat="server" Text="Set-Up"  meta:resourcekey="lblDateResource1"></asp:Label>
                    </td>
                    <td style=" border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" colspan="8" align="center">
                        <table style="width:100%;height:100%; border-style:none;">
                            <tr>
                                <td style="width:87.5%" align="center">
                                     <asp:Label ID="Label9" runat="server" Text="Appeararance check with illumination Light" ></asp:Label>
                                </td>
                                <td style="width:12.5%" align="right">
                                    <asp:CheckBox ID="cb_Appera_AllOK" Text="All OK" runat="server"   AutoPostBack="true" OnCheckedChanged="cb_Appera_AllOK_CheckedChanged" Visible="false" />  
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="6" align="center">
                        
                         <table style="width:100%;height:100%; border-style:none;">
                            <tr>
                                <td style="width:83.33%" align="center">
                                      <asp:Label ID="Label15" runat="server" Text="Graphic condition" Width="100%"></asp:Label>
                                </td>
                                <td style="width:16.67%" align="right">
                                    <asp:CheckBox ID="cb_Graph_AllOK" Text="All OK" runat="server"   AutoPostBack="true"  OnCheckedChanged="cb_Graph_AllOK_CheckedChanged"  Visible="false"/>  
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style ="width: 100%">
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" align="center">
                    <asp:Label ID="Label1" runat="server" Text="Black Mark" Width="100%" meta:resourcekey="Label1Resource1"></asp:Label>
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2" align="center">
                     <asp:Label ID="Label2" runat="server" Text="Black Dot" Width="100%" meta:resourcekey="Label2Resource1"></asp:Label>
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2" align="center">
                        <asp:Label ID="Label3" runat="server" Text="Pin Hole" Width="100%" meta:resourcekey="Label3Resource1"></asp:Label>
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2" align="center">
                     <asp:Label ID="Label4" runat="server" Text="Jagged" Width="100%" meta:resourcekey="Label4Resource1"></asp:Label>
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2" align="center">
                     <asp:Label ID="Label5" runat="server" Text="Check Guide" Width="100%" meta:resourcekey="Label5Resource1"></asp:Label>
                    </td>  
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2" align="center">
                     <asp:Label ID="Label6" runat="server" Text="Navitas" Width="100%" meta:resourcekey="Label6Resource1"></asp:Label>
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2" align="center">
                     <asp:Label ID="Label7" runat="server" Text="Smart Scope" Width="100%" meta:resourcekey="Label7Resource1"></asp:Label>
                    </td>
                </tr>
                <tr style ="width: 100%">     
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center" colspan="2">
                    <asp:Label ID="lb_1st" runat="server" Text="1st" Width="100%" style="padding:initial" meta:resourcekey="lb_1stResource1" ></asp:Label>
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                        <asp:CheckBox ID="cb_Black_Mark_OK" Text="OK" runat="server" style="padding:2px 2px 2px 2px;" AutoPostBack="true" OnCheckedChanged="cb_Black_Mark_OK_CheckedChanged" />  
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                        <asp:CheckBox ID="cb_Black_Mark_NG" Text="NG" runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Black_Mark_NG_CheckedChanged" />  
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Black_Dot_OK" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Black_Dot_OK_CheckedChanged" /> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Black_Dot_NG" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Black_Dot_NG_CheckedChanged" /> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Pin_Hole_OK" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Pin_Hole_OK_CheckedChanged" /> 
                    </td>  
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Pin_Hole_NG" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Pin_Hole_NG_CheckedChanged" /> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Jagged_OK" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Jagged_OK_CheckedChanged"/> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Jagged_NG" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Jagged_NG_CheckedChanged"/> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Check_Guled_OK" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Check_Guled_OK_CheckedChanged" /> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                   <asp:CheckBox ID="cb_Check_Guled_NG" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Check_Guled_NG_CheckedChanged"/> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                   <asp:CheckBox ID="cb_Navitas_OK" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Navitas_OK_CheckedChanged"/> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                   <asp:CheckBox ID="cb_Navitas_NG" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Navitas_NG_CheckedChanged" /> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Smart_Scope_OK" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Smart_Scope_OK_CheckedChanged" /> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Smart_Scope_NG" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Smart_Scope_NG_CheckedChanged"/> 
                    </td>
                </tr>
                <tr style ="width: 100%">     
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center" colspan="2">
                    <asp:Label ID="lb_2nd" runat="server" Text="2nd" Width="100%" meta:resourcekey="lb_2ndResource1" Height="16px" ></asp:Label>
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                        <asp:CheckBox ID="cb_Black_Mark_OK1" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Black_Mark_OK1_CheckedChanged"/>  
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                        <asp:CheckBox ID="cb_Black_Mark_NG1" Text="NG" runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Black_Mark_NG1_CheckedChanged"/>  
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Black_Dot_OK1" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Black_Dot_OK1_CheckedChanged" /> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Black_Dot_NG1" Text="NG"  runat="server"  style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Black_Dot_NG1_CheckedChanged"/> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Pin_Hole_OK1" Text="OK"  runat="server"  style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Pin_Hole_OK1_CheckedChanged"/> 
                    </td>  
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Pin_Hole_NG1" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Pin_Hole_NG1_CheckedChanged" /> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Jagged_OK1" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Jagged_OK1_CheckedChanged" /> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Jagged_NG1" Text="NG"  runat="server"  style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Jagged_NG1_CheckedChanged"/> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Check_Guled_OK1" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Check_Guled_OK1_CheckedChanged" /> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                   <asp:CheckBox ID="cb_Check_Guled_NG1" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Check_Guled_NG1_CheckedChanged"/> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                   <asp:CheckBox ID="cb_Navitas_OK1" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Navitas_OK1_CheckedChanged"/> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                   <asp:CheckBox ID="cb_Navitas_NG1" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Navitas_NG1_CheckedChanged"/> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Smart_Scope_OK1" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Smart_Scope_OK1_CheckedChanged" /> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Smart_Scope_NG1" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Smart_Scope_NG1_CheckedChanged" /> 
                    </td>
                </tr>
                <tr style ="width: 100%">     
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center" colspan="2">
                    <asp:Label ID="lb_In_process" runat="server" Text="In-process" Width="115%" meta:resourcekey="lb_In_processResource1" ></asp:Label>
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                        <asp:CheckBox ID="cb_Black_Mark_OK2" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Black_Mark_OK2_CheckedChanged"/>  
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                        <asp:CheckBox ID="cb_Black_Mark_NG2" Text="NG" runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Black_Mark_NG2_CheckedChanged"/>  
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Black_Dot_OK2" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Black_Dot_OK2_CheckedChanged"/> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Black_Dot_NG2" Text="NG"  runat="server"  style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Black_Dot_NG2_CheckedChanged"/> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Pin_Hole_OK2" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Pin_Hole_OK2_CheckedChanged"/> 
                    </td>  
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Pin_Hole_NG2" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Pin_Hole_NG2_CheckedChanged"/> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Jagged_OK2" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Jagged_OK2_CheckedChanged"/> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Jagged_NG2" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Jagged_NG2_CheckedChanged"/> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Check_Guled_OK2" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Check_Guled_OK2_CheckedChanged"/> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                   <asp:CheckBox ID="cb_Check_Guled_NG2" Text="NG"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Check_Guled_NG2_CheckedChanged"/> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                   <asp:CheckBox ID="cb_Navitas_OK2" Text="OK"  runat="server" style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Navitas_OK2_CheckedChanged"/> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                   <asp:CheckBox ID="cb_Navitas_NG2" Text="NG"  runat="server"  style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Navitas_NG2_CheckedChanged"/> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Smart_Scope_OK2" Text="OK"  runat="server"  style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Smart_Scope_OK2_CheckedChanged" /> 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" align="center">
                    <asp:CheckBox ID="cb_Smart_Scope_NG2" Text="NG"  runat="server"  style="padding:2px 2px 2px 2px;"  AutoPostBack="true" OnCheckedChanged="cb_Smart_Scope_NG2_CheckedChanged"/> 
                    </td>
                </tr>
                <tr style ="width: 100%">  
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2">
                        <asp:Label ID="lb_MC_Operator" runat="server" Text="MC Operator:" Width="100%" meta:resourcekey="lb_MC_OperatorResource1"></asp:Label>
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2">
                        <asp:DropDownList  runat="server" ID="ddl_MC_Operator" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" > </asp:DropDownList>
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                        <asp:Label ID="lb_Buyoff_By" runat="server" Text="Buyoff By:" Width="100%" meta:resourcekey="lb_Buyoff_ByResource1"></asp:Label>
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                        <asp:DropDownList  runat="server" ID="ddl_Buyoff_By" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:DropDownList>
                    </td>       
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2">
                        <asp:Label ID="lb_Approved_By" runat="server" Text="Approved By:" Width="100%" meta:resourcekey="lb_Approved_ByResource1"></asp:Label>
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2">
                        <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_Approved_By" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"></asp:TextBox>
                    </td>       
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                        <asp:Label ID="lb_CheckBy" runat="server" Text="Check By:" Width="100%"  Visible="false"></asp:Label>
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                        <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_CheckBy" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" Visible="false"></asp:TextBox>
                    </td>
                </tr>



                <tr style ="width: 100%">  
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="16">
                        <h5><b>Laser Machine Setting</b></h5>
                    </td>
                </tr>


              

                <tr style ="width: 100%">  
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                        Rate:
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                        <asp:DropDownList runat="server" ID="ddlRate" Width="100%" Height="23px"></asp:DropDownList>
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2">
                        Frequency:
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2">
                        <asp:DropDownList runat="server" ID="ddlFrequency" Width="100%" Height="23px"></asp:DropDownList>
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2">
                        Power%:
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="2">
                        <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txtPower" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"></asp:TextBox>
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                        Repeat: 
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 25%; font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                        <asp:DropDownList runat="server" ID="ddlRepeat" Width="100%" Height="23px">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="0" Value="0"></asp:ListItem>
                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
               

               
               
               
             

                <tr style ="width: 100%"> 
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 50%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="8" align="center">
                        <asp:Button ID="btn_Confirm" runat="server" Text="Confirm" OnClick="btn_Confirm_Click" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Height="30px" Width="40%" />
                    </td>
                    <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 50%; font-family: 'Arial Unicode MS'; height: 50px;"colspan="8" align="center">
                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" OnClick="btn_Cancel_Click" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-danger" Height="30px" Width="40%" />
                    </td>
                </tr>


                


           </table>     
        </div>                    
 </asp:Content>
