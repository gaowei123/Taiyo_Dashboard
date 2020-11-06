<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MachineRealStatus.aspx.cs" Inherits="DashboardTTS.Webform.Molding.MachineRealStatus" %>
<%@ Register src="../../UserControl/UcMoulding.ascx" tagname="UcMoulding" tagprefix="uc1" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <meta http-equiv="Refresh" content="30" />
    <style>
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
            <div class="col-md-12 panel panel-default" style="padding:10px 10px 10px 14px; margin-top:10px;">
                <div class="divControl"><uc1:UcMoulding ID="uc1" runat="server" /></div>
                <div class="divControl"><uc1:UcMoulding ID="uc2" runat="server" /></div>
                <div class="divControl"><uc1:UcMoulding ID="uc3" runat="server" /></div>
                <div class="divControl"><uc1:UcMoulding ID="uc4" runat="server" /></div>
                <div class="divControl"><uc1:UcMoulding ID="uc5" runat="server" /></div>
                <div class="divControl"><uc1:UcMoulding ID="uc6" runat="server" /></div>
                <div class="divControl"><uc1:UcMoulding ID="uc7" runat="server" /></div>
                <div class="divControl"><uc1:UcMoulding ID="uc8" runat="server" /></div>
                <div class="divControl"><uc1:UcMoulding ID="uc9" runat="server" /></div>
            </div>
        </div>
    </div>
 </asp:Content>
