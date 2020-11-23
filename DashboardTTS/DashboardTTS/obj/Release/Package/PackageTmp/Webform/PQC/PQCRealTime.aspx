<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PQCRealTime.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.PQC.PQCRealTime" %>
<%@ Register src="../../UserControl/WebUserControlPQCStatus.ascx" tagname="WebUserControlPQCStatus" tagprefix="uc1" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<meta http-equiv="Refresh" content="30" />--%>
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


                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucOnline1"  runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucOnline2"  runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucWIP1"  runat="server" /></div> 
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucWIP2"  runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPacking1"  runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPacking2"  runat="server" /></div>
                
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucOnline3"  runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucOnline4"  runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucWIP3"  runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucWIP4"  runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPacking3"  runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPacking4"  runat="server" /></div>

                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucOnline5"  runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucOnline6"  runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucWIP5"  runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucWIP6"  runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPacking5"  runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucPacking6"  runat="server" /></div>

                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucOnline7"  runat="server" /></div>
                <div class="divUC"><uc1:WebUserControlPQCStatus ID="ucOnline8"  runat="server" /></div>                
               
            </div>
        </div>
    </div>




    <script type="text/javascript">        
        var strDate = dateFormat('dd/MM/yyyy HH:mm',  new Date());
        $('#lbTime').text(strDate);
    </script>

 </asp:Content>