<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MachineRealStatus.aspx.cs" Inherits="DashboardTTS.Webform.Molding.MachineRealStatus" %>
<%@ Register src="../../UserControl/WebUserControlMouldingMachineStatus.ascx" tagname="WebUserControlMouldingMachineStatus" tagprefix="uc1" %>
 
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
            <span class="titleText">Moulding Machine Real Time</span>
        </div>


        <div class="row">
            <div class="col-md-12 panel panel-default" style=" padding-left:24px; margin-top:10px; margin-bottom:8px;">
                <div class="row">
                    <div class="col-md-3 col-sm-3">
                        <label class="summayFontStyle">Total Utilization:</label>
                        <asp:Label runat="server" ID="lbUtilization" class="summayFontStyle"></asp:Label>
                    </div>
                    <div class="col-md-3 col-sm-3">
                        <label class="summayFontStyle">Total Output:</label>
                        <asp:Label runat="server" ID="lbTotalOutput"  class="summayFontStyle"></asp:Label>
                    </div>
                    <div class="col-md-3 col-sm-3">
                        <label class="summayFontStyle">Total Rej Qty:</label>
                        <asp:Label runat="server" ID="lbTotalRejQty" class="summayFontStyle"></asp:Label>
                    </div>
                    <div class="col-md-3 col-sm-3">
                        <label class="summayFontStyle">Total REJ%:</label>
                        <asp:Label runat="server"  ID="lbTotalRejRate" class="summayFontStyle"></asp:Label>
                    </div>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-md-12 panel panel-default" style="padding:10px 10px 10px 14px;">
                <div runat="server" class="divControl"><uc1:WebUserControlMouldingMachineStatus ID="ucMachineStatus1" MachineNo="Machine 1" runat="server" /></div>
                <div runat="server" class="divControl"><uc1:WebUserControlMouldingMachineStatus ID="ucMachineStatus2" MachineNo="Machine 2" runat="server" /></div>
                <div runat="server" class="divControl"><uc1:WebUserControlMouldingMachineStatus ID="ucMachineStatus3" MachineNo="Machine 3" runat="server" /></div>
                <div runat="server" class="divControl"><uc1:WebUserControlMouldingMachineStatus ID="ucMachineStatus4" MachineNo="Machine 4" runat="server" /></div>
                <div runat="server" class="divControl"><uc1:WebUserControlMouldingMachineStatus ID="ucMachineStatus5" MachineNo="Machine 5" runat="server" /></div>
                <div runat="server" class="divControl"><uc1:WebUserControlMouldingMachineStatus ID="ucMachineStatus6" MachineNo="Machine 6" runat="server" /></div>
                <div runat="server" class="divControl"><uc1:WebUserControlMouldingMachineStatus ID="ucMachineStatus7" MachineNo="Machine 7" runat="server" /></div>
                <div runat="server" class="divControl"><uc1:WebUserControlMouldingMachineStatus ID="ucMachineStatus8" MachineNo="Machine 8" runat="server" /></div>
                <div runat="server" class="divControl"><uc1:WebUserControlMouldingMachineStatus ID="ucMachineStatus9" MachineNo="Machine 9" runat="server" /></div>
            </div>
        </div>
    </div>
 </asp:Content>
