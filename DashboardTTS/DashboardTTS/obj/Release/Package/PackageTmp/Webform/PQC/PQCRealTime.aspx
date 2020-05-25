<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PQCRealTime.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.PQC.PQCRealTime" %>
<%@ Register src="../../UserControl/WebUserControlPQCStatus.ascx" tagname="WebUserControlPQCStatus" tagprefix="uc1" %>
 
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
            margin:5px;
        }
    </style>


    <div class="container"> 
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText"><asp:Label runat="server" ID="lbHearText"></asp:Label></span>
        </div>


        <div class="row">
            <div class="col-md-12 panel panel-default" style=" padding-left:24px; margin-top:10px; margin-bottom:8px;">
                <div class="row">
                    <div class="col-md-3 col-sm-3">
                        <label class="summayFontStyle">Total Output:</label>
                        <asp:Label runat="server" ID="lbOutput"  class="summayFontStyle"></asp:Label>
                    </div>
                    <div class="col-md-3 col-sm-3">
                        <label class="summayFontStyle">Total Rej Qty:</label>
                        <asp:Label runat="server" ID="lbRejQty" class="summayFontStyle"></asp:Label>
                    </div>
                    <div class="col-md-3 col-sm-3">
                        <label class="summayFontStyle">Total REJ%:</label>
                        <asp:Label runat="server"  ID="lbRejRate" class="summayFontStyle"></asp:Label>
                    </div>
                    <div class="col-md-3 col-sm-3"></div>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-md-12 panel panel-default" style="padding:10px 10px 10px 14px;">
                <div runat="server" ID="div1" class="divControl"><uc1:WebUserControlPQCStatus ID="ucPQC1" runat="server" /></div>   <%--online1--%>
                <div runat="server" ID="div2" class="divControl"><uc1:WebUserControlPQCStatus ID="ucPQC2" runat="server" /></div>   <%--online2--%>
                <div runat="server" ID="div3" class="divControl"><uc1:WebUserControlPQCStatus ID="ucPQC3" runat="server" /></div>   <%--online3--%>
                <div runat="server" ID="div4" class="divControl"><uc1:WebUserControlPQCStatus ID="ucPQC4" runat="server" /></div>   <%--online4--%>
                <div runat="server" ID="div5" class="divControl"><uc1:WebUserControlPQCStatus ID="ucPQC5" runat="server" /></div>   <%--online5--%>
                <div runat="server" ID="div6" class="divControl"><uc1:WebUserControlPQCStatus ID="ucPQC6" runat="server" /></div>   <%--online6--%>
                <div runat="server" ID="div7" class="divControl"><uc1:WebUserControlPQCStatus ID="ucPQC7" runat="server" /></div>   <%--online7--%>
                <div runat="server" ID="div8" class="divControl"><uc1:WebUserControlPQCStatus ID="ucPQC8" runat="server" /></div>   <%--online8--%>
                       

                <div runat="server" ID="div16" class="divControl"><uc1:WebUserControlPQCStatus ID="ucPQC16" runat="server" /></div>   <%--wip1--%>
                <div runat="server" ID="div17" class="divControl"><uc1:WebUserControlPQCStatus ID="ucPQC17" runat="server" /></div>   <%--wip2--%>
                <div runat="server" ID="div14" class="divControl"><uc1:WebUserControlPQCStatus ID="ucPQC14" runat="server" /></div>   <%--wip3--%>
                <div runat="server" ID="div15" class="divControl"><uc1:WebUserControlPQCStatus ID="ucPQC15" runat="server" /></div>   <%--wip4--%>
                <div runat="server" ID="div11" class="divControl"><uc1:WebUserControlPQCStatus ID="ucPQC11" runat="server" /></div>   <%--wip5--%>
                <div runat="server" ID="div13" class="divControl"><uc1:WebUserControlPQCStatus ID="ucPQC13" runat="server" /></div>   <%--wip6--%>
               
                
                <div runat="server" ID="div25" class="divControl"><uc1:WebUserControlPQCStatus ID="ucPQC25" runat="server" /></div>   <%--packing1--%>
                <div runat="server" ID="div23" class="divControl"><uc1:WebUserControlPQCStatus ID="ucPQC23" runat="server" /></div>   <%--packing2--%>
                <div runat="server" ID="div22" class="divControl"><uc1:WebUserControlPQCStatus ID="ucPQC22" runat="server" /></div>   <%--packing3--%>
                <div runat="server" ID="div21" class="divControl"><uc1:WebUserControlPQCStatus ID="ucPQC21" runat="server" /></div>   <%--packing4--%>
                <div runat="server" ID="div24" class="divControl"><uc1:WebUserControlPQCStatus ID="ucPQC24" runat="server" /></div>   <%--packing5--%>
                <div runat="server" ID="div12" class="divControl"><uc1:WebUserControlPQCStatus ID="ucPQC12" runat="server" /></div>   <%--packing6--%>
                
            </div>
        </div>
    </div>
 </asp:Content>