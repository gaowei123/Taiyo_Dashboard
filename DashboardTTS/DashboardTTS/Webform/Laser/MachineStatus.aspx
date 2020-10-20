<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MachineStatus.aspx.cs" Inherits="DashboardTTS.Webform.MachineStatus"  %>
<%@ Register src="../../UserControl/WebUserControlMachineStatus.ascx" tagname="WebUserControlMachineStatus" tagprefix="uc1" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <meta http-equiv="Refresh" content="30" />

    <style>
        .summayFontStyle{
            color:#003162; 
            font-weight:bold; 
            font-size:large;
            line-height:35px;
        }
        .divControl{
            width:auto; 
            display:inline-block; 
            float: left; 
            margin:5px 5px 5px 5px
        }
    </style>

    <div class="container"> 
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Laser Machine Real Time</span>
            <label class="titleText" style="float:right;" id="lbTime"></label>           
        </div>
        <div class="row">
            <div class="col-sm-12 panel panel-default" style="padding:10px 10px 10px 14px; margin-top:10px;">
                <div runat="server" class="divControl"><uc1:WebUserControlMachineStatus ID="ucMachineStatus1" runat="server" /></div>
                <div runat="server" class="divControl"><uc1:WebUserControlMachineStatus ID="ucMachineStatus2" runat="server" /></div>
                <div runat="server" class="divControl"><uc1:WebUserControlMachineStatus ID="ucMachineStatus3" runat="server" /></div>
                <div runat="server" class="divControl"><uc1:WebUserControlMachineStatus ID="ucMachineStatus4" runat="server" /></div>
                <div runat="server" class="divControl"><uc1:WebUserControlMachineStatus ID="ucMachineStatus5" runat="server" /></div>
                <div runat="server" class="divControl"><uc1:WebUserControlMachineStatus ID="ucMachineStatus6" runat="server" /></div>
                <div runat="server" class="divControl"><uc1:WebUserControlMachineStatus ID="ucMachineStatus7" runat="server" /></div>
                <div runat="server" class="divControl"><uc1:WebUserControlMachineStatus ID="ucMachineStatus8" runat="server" /></div>
            </div>
        </div>
    </div>

    <script type="text/javascript">        
        var strDate = dateFormat('dd/MM/yyyy HH:mm',  new Date());
        $('#lbTime').text(strDate);
    </script>

</asp:Content>
