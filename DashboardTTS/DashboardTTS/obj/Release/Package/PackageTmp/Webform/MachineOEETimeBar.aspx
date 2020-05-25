<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MachineOEETimeBar.aspx.cs" Inherits="DashboardTTS.Webform.MachineOEETimeBar" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.UltraWebChart" tagprefix="igchart" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.UltraChart.Resources.Appearance" tagprefix="igchartprop" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.UltraChart.Data" tagprefix="igchartdata" %>
<%@ Register src="../UserControl/WebUserControlTimeBar.ascx" tagname="WebUserControlTimeBar" tagprefix="uc1" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" >
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Laser Operation Status Time Bar</span>
        </div>


        <div class="row">
            <div class="col-md-12 panel panel-default searchingPanel" style="padding:10px;">
                <div class="row form-inline searchingBar">
                    <div class="col-md-3">
                        <label style="width:35%">Date From:</label>
                        <asp:TextBox runat="server" ID="txtDateFrom" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd" Width="60%"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label style="width:35%">Date To:</label>
                        <asp:TextBox runat="server" ID="txtDateTo" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd" Width="60%"></asp:TextBox>
                    </div>

                    <div class="col-md-3">
                        <label style="width:35%">Machine No:</label>
                        <asp:DropDownList ID="ddlMachineNo"  runat="server" CssClass="form-control" Width="60%">
                            <asp:ListItem Value="">All</asp:ListItem>
                            <asp:ListItem Value="1">No.1</asp:ListItem>
                            <asp:ListItem Value="2">No.2</asp:ListItem>
                            <asp:ListItem Value="3">No.3</asp:ListItem>
                            <asp:ListItem Value="4">No.4</asp:ListItem>
                            <asp:ListItem Value="5">No.5</asp:ListItem>
                            <asp:ListItem Value="6">No.6</asp:ListItem>
                            <asp:ListItem Value="7">No.7</asp:ListItem>
                            <asp:ListItem Value="8">No.8</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-3" align="right" style="padding-right:2%;">
                        <asp:Button ID="Button1" runat="server" Text="Generate" OnClick="btnGenerate_Click" CssClass="btn-success" style="width:100px; height:34px; border-radius:4px;" />
                    </div>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-md-12" align="right">
                <asp:Label ID="Label7" Text ="&nbsp;&nbsp;" ForeColor="Orange" BackColor ="Orange" runat="server"/>&nbsp;<asp:Label ID="Label8" Text ="Setup" Font-Bold="true" runat="server"></asp:Label>
                &nbsp;&nbsp;&nbsp; 
                <asp:Label ID="Label5" Text ="&nbsp;&nbsp;" ForeColor ="Brown"  BackColor ="Brown" runat="server"/>&nbsp;<asp:Label ID="Label6" Text ="Maintainence" Font-Bold="true" runat="server"></asp:Label>
                &nbsp;&nbsp;&nbsp; 
                <asp:Label ID="Label26" Text ="&nbsp;&nbsp;" ForeColor ="Green"  BackColor ="Green" runat="server"/>&nbsp;<asp:Label ID="Label25" Text ="Operating" Font-Bold="true" runat="server"></asp:Label>
                &nbsp;&nbsp;&nbsp; 
                <asp:Label ID="Label28" Text ="&nbsp;&nbsp;" ForeColor ="MediumPurple"  BackColor ="MediumPurple" runat="server"></asp:Label>&nbsp; <asp:Label ID="Label27" Text ="Buyoff" Font-Bold="true" runat="server"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label30" Text ="&nbsp;&nbsp;" ForeColor ="yellow"  BackColor ="yellow" runat="server"> </asp:Label>&nbsp; <asp:Label ID="Label29" Text ="No Schedule" Font-Bold="true" runat="server"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label3" Text ="&nbsp;&nbsp;" ForeColor ="blue"  BackColor ="blue" runat="server"></asp:Label>&nbsp; <asp:Label ID="Label4" Text ="Testing" Font-Bold="true" runat="server"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label2" Text ="&nbsp;&nbsp;" ForeColor ="Red"  BackColor ="Red" runat="server"></asp:Label>  &nbsp; <asp:Label ID="Label1" Text ="Break Down" Font-Bold="true" runat="server"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label32" Text ="&nbsp;&nbsp;" ForeColor ="#3C3C3C"  BackColor ="#3C3C3C" runat="server"></asp:Label>&nbsp; <asp:Label ID="Label31" Text ="Shut Down" Font-Bold="true" runat="server"></asp:Label>  
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 panel panel-default " style="padding-top:10px; padding-bottom:10px;">
                <uc1:WebUserControlTimeBar ID="WebUserControlTimeBar1" runat="server"/> 
                <uc1:WebUserControlTimeBar ID="WebUserControlTimeBar2" runat="server"/>
                <uc1:WebUserControlTimeBar ID="WebUserControlTimeBar3" runat="server"/>
                <uc1:WebUserControlTimeBar ID="WebUserControlTimeBar4" runat="server"/>
                <uc1:WebUserControlTimeBar ID="WebUserControlTimeBar5" runat="server"/>
                <uc1:WebUserControlTimeBar ID="WebUserControlTimeBar6" runat="server"/>
                <uc1:WebUserControlTimeBar ID="WebUserControlTimeBar7" runat="server"/>
                <uc1:WebUserControlTimeBar ID="WebUserControlTimeBar8" runat="server"/>
            </div>
        </div>
    </div>



    <script type="text/javascript">
        $('#MainContent_txtDateFrom').datetimepicker({
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            minView: 2,
            forceParse: 0,
            initialDate: new Date()
        });

        $('#MainContent_txtDateTo').datetimepicker({
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
