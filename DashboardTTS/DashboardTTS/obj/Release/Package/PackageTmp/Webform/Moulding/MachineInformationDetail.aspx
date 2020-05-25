<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="MachineInformationDetail.aspx.cs" Inherits="DashboardTTS.Webform.Molding.MachineInformationDetail" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
<link href="../Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />  
<link href="/Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />

<script src="../js/Dashboard.js"> </script>

    <div style="position: relative; width: 1125px; height: 257px; margin: auto; top: 0px; left: 0px;">


        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Moulding Machine Detail Information" Font-Size="12" ForeColor="White"/>
                </td>
            </tr>
                              
            <tr style="width:100%" Class="tr_Machine">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                   <asp:Label runat="server" Text="MachineID :"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList runat="server" ID="ddl_machineID" Width="100%">
                        <asp:ListItem Value="1">Machine 1</asp:ListItem>
                        <asp:ListItem Value="2">Machine 2</asp:ListItem>
                        <asp:ListItem Value="3">Machine 3</asp:ListItem>
                        <asp:ListItem Value="4">Machine 4</asp:ListItem>
                        <asp:ListItem Value="5">Machine 5</asp:ListItem>
                        <asp:ListItem Value="6">Machine 6</asp:ListItem>
                        <asp:ListItem Value="7">Machine 7</asp:ListItem>
                        <asp:ListItem Value="8">Machine 8</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Visible="false" Text="Type :" ID="lb_Machine_Type"></asp:Label>
                    <asp:Label runat="server" Visible="false" Text="Model :" ID="lb_RobotArm_Model"></asp:Label>
                    <asp:Label runat="server" Visible="false" Text="Maker :" ID="lb_Temp_Dryer_Maker"></asp:Label>
                    <asp:Label runat="server" Visible="false" Text="Maker :" ID="lb_Main_Maker"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox AutoCompleteType="Disabled" runat="server" Visible="false" ID="txt_Machine_Type" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                    <asp:TextBox AutoCompleteType="Disabled" runat="server" Visible="false" ID="txt_RobotArm_Model" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                    <asp:TextBox AutoCompleteType="Disabled" runat="server" Visible="false" ID="txt_Temp_Dryer_Maker" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                    <asp:TextBox AutoCompleteType="Disabled" runat="server" Visible="false" ID="txt_Main_Maker" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                </td>
            </tr>

            <tr style="width:100%" Class="tr_Machine">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                   <asp:Label runat="server" Visible="false" Text="Maker/Model :" ID="lb_Machine_MakerModel"></asp:Label>
                   <asp:Label runat="server" Visible="false" Text="Serial No :" ID="lb_RobotArm_SerialNo"></asp:Label>
                   <asp:Label runat="server" Visible="false" Text="Model :" ID="lb_Temp_Dryer_Model"></asp:Label>
                   <asp:Label runat="server" Visible="false" Text="Model :" ID="lb_Main_Model"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox AutoCompleteType="Disabled" Visible="false" runat="server" ID="txt_Machine_MakerModel" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                    <asp:TextBox AutoCompleteType="Disabled" Visible="false" runat="server" ID="txt_RobotArm_SerialNo" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                    <asp:TextBox AutoCompleteType="Disabled" Visible="false" runat="server" ID="txt_Temp_Dryer_Model" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                    <asp:TextBox AutoCompleteType="Disabled" Visible="false" runat="server" ID="txt_Main_Model" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Visible="false" Text="SerialNo :" ID="lb_Machine_SerialNo"></asp:Label>
                    <asp:Label runat="server" Visible="false"  Text="Controller Type :" ID="lb_RobotArm_ControllerType"></asp:Label>     
                    <asp:Label runat="server" Visible="false"  Text="Date :" ID="lb_Temp_Dryer_Date"></asp:Label>
                    <asp:Label runat="server" Visible="false"  Text="Date :" ID="lb_Main_Info"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox AutoCompleteType="Disabled" runat="server" Visible="false" ID="txt_Machine_SerialNo" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox> 
                    <asp:TextBox AutoCompleteType="Disabled" runat="server" Visible="false" ID="txt_RobotArm_ControllerType" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox> 
                    <asp:TextBox AutoCompleteType="Disabled" runat="server" Visible="false" ID="txt_Temp_Dryer_Date" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox> 
                    <asp:TextBox AutoCompleteType="Disabled" runat="server" Visible="false" ID="txt_Main_Info" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox> 
                </td>
            </tr>

            <tr style="width:100%" Class="tr_Machine">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                   <asp:Label runat="server" Visible="false" ID="lb_Machine_ManufactureDate" Text="ManufactureDate :"></asp:Label>
                   <asp:Label runat="server" Visible="false" ID="lb_RobotArm_ControllerSerialNo" Text="Controller SerialNo :"></asp:Label>
                     <asp:Label runat="server" Visible="false" ID="lb_Main_DateOfManu" Text="Date Of Manu :"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                   <igsch:WebDateChooser ID="infDch_Machine" Visible="false" runat="server" Height="23px"   Width="100%" Value="" ></igsch:WebDateChooser>
                   <asp:TextBox AutoCompleteType="Disabled" runat="server" Visible="false" ID="txt_RobotArm_ControllerSerialNo" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox> 
                   <igsch:WebDateChooser ID="infDch_Main" Visible="false" runat="server" Height="23px"   Width="100%" Value="" ></igsch:WebDateChooser>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Visible="false" Text="CTRL :" ID="lb_Machine_CTRL"></asp:Label>
                    <asp:Label runat="server" Visible="false" Text="Date :" ID="lb_RobotArm_Date"></asp:Label>
                    <asp:Label runat="server" Visible="false" Text="Date :" ID="lb_Main_ScrewDiameter"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox AutoCompleteType="Disabled" Visible="false" runat="server" ID="txt_Machine_CTRL" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>  
                    <asp:TextBox AutoCompleteType="Disabled" Visible="false" runat="server" ID="txt_RobotArm_Date" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>  
                    <asp:TextBox AutoCompleteType="Disabled" Visible="false" runat="server" ID="txt_Main_ScrewDiameter" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>         
                </td>
            </tr>


            <tr style="width:100%" Class="tr_Machine">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                   <asp:Label runat="server" Visible="false" ID="lb_Main_MaxOPNStroke" Text="Max OPN Stroke :"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox AutoCompleteType="Disabled" Visible="false" runat="server" ID="txt_Main_MaxOPNStroke" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Visible="false" Text="EJT Stroke :" ID="lb_Main_EJTStroke"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox AutoCompleteType="Disabled" Visible="false" runat="server" ID="txt_Main_EJTStroke" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>             
                </td>
            </tr>

            <tr style="width:100%" Class="tr_Machine">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                   <asp:Label runat="server" Visible="false" ID="lb_Main_TiebarDistance" Text="Tiebar Distance :"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox AutoCompleteType="Disabled" Visible="false" runat="server" ID="txt_Main_TiebarDistance" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Visible="false" Text="Min Mold Size :" ID="lb_Main_MinMoldSize"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox AutoCompleteType="Disabled" Visible="false" runat="server" ID="txt_Main_MinMoldSize" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>             
                </td>
            </tr>
            <tr style="width:100%" Class="tr_Machine">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                   <asp:Label runat="server" Visible="false" ID="lb_Main_MinMoldThickness" Text="Min Mold Thickness :"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox AutoCompleteType="Disabled" Visible="false" runat="server" ID="txt_Main_MinMoldThickness" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Visible="false" Text="M/C Dimensions :" ID="lb_Main_Dimensions"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox AutoCompleteType="Disabled" Visible="false" runat="server" ID="txt_Main_Dimensions" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>             
                </td>
            </tr>

            <tr style="width:100%">
              <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="4"></td>
            </tr>
              
            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;" align="center" colspan="2" >
                    <asp:Button  runat="server" Width="50%" Height="30px" Text="Submit" ID="btn_submit" OnClick="btn_submit_Click" CssClass="btn-success"/>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;" align="center" colspan="2">
                    <asp:Button  runat="server" Width="50%" Text="Cancel" Height="30px" ID="btn_cancel" OnClick="btn_cancel_Click"  CssClass="btn-danger"/>
                </td>
            </tr>            
            
            
                           
        </table> 
    </div>

</asp:Content>
