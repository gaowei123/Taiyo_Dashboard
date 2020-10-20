<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PQCRealTime.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.PQC.PQCRealTime" %>
<%@ Register src="../../UserControl/WebUserControlPQCStatus.ascx" tagname="WebUserControlPQCStatus" tagprefix="uc1" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <meta http-equiv="Refresh" content="30" />
    <style>
        .divUC {
            width: auto;
            display: inline-block;
            float: left;
            margin:1px 3px 1px 3px;
        }
    </style>

    <div class="container-fluid" style="max-width:1250px;"> 
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">PQC Station Real Time</span>
            <label class="titleText" style="float:right;" id="lbTime"></label>  
        </div>
        <div class="row">
            <div class="col-sm-12 panel panel-default" style="padding:5px;">
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC1"  runat="server" /></div>  
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC2"  runat="server" /></div>  
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC3"  runat="server" /></div>  
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC4"  runat="server" /></div>  
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC5"  runat="server" /></div>  
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC6"  runat="server" /></div>  
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC7"  runat="server" /></div>  
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC8"  runat="server" /></div>  
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC9"  runat="server" /></div> 
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC10" runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC11" runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC12" runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC13" runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC14" runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC15" runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC16" runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC17" runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC18" runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC19" runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC20" runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC21" runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC22" runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC23" runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPQC24" runat="server" /></div>
            </div>
        </div>
    </div>
    <script type="text/javascript">        
        var strDate = dateFormat('dd/MM/yyyy HH:mm',  new Date());
        $('#lbTime').text(strDate);
    </script>
 </asp:Content>