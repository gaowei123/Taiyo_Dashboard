<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductivityChart.aspx.cs" Inherits="DashboardTTS.Webform.ProductivityChart" StyleSheetTheme="" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">  
  

    <div class="container container-fluid" >
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Laser Productivity Chart</span>
        </div>




        <div class="row">
            <div class="col-sm-12 panel panel-default searchingPanel" >
                <div class="row form-inline searchingBar ">
                    <div class="col-sm-3">
                        <label style="width:35%">Report Type:</label>
                        <asp:DropDownList runat="server" ID="ddlReportType" CssClass="form-control" Width="60%">
                            <asp:ListItem Value="Daily">Daily</asp:ListItem>
                            <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                            <asp:ListItem Value="Yearly">Yearly</asp:ListItem>
                            <asp:ListItem Value="Machine">Machine</asp:ListItem>
                            <asp:ListItem Value="Model">Model</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3">
                        <label style="width:35%">Year:</label>
                        <asp:DropDownList runat="server" ID="ddlYear" CssClass="form-control" Width="60%"></asp:DropDownList>
                    </div>
                    <div class="col-sm-3">
                        <label style="width:35%">Date From:</label>
                        <asp:TextBox runat="server" ID="txtDateFrom" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd" Width="60%"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <label style="width:35%">Date To:</label>
                        <asp:TextBox runat="server" ID="txtDateTo" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd" Width="60%"></asp:TextBox>
                    </div>
                </div>


                <div class="row form-inline searchingBar ">
                    <div class="col-sm-3">
                        <label style="width:35%">Shift:</label>
                        <asp:DropDownList runat="server" ID="ddlShift" CssClass="form-control" Width="60%">
                            <asp:ListItem Value="">All</asp:ListItem>
                            <asp:ListItem Value="Day">Day</asp:ListItem>
                            <asp:ListItem Value="Night">Night</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3"></div>
                    <div class="col-sm-3"></div>
                    <div class="col-sm-3" align="right" style="padding-right:2%">
                        <asp:Button ID="Button1" runat="server" Text="Generate" OnClick="btnGenerate_Click" CssClass="btn-success" style="width:100px; height:34px; border-radius:4px;" />
                    </div>
                </div>              
            </div>
        </div>


        <div class="row">
           <h3> <asp:Label ID="lblResult" runat="server" /> </h3>
        </div>

        <div class="row">
            <div class="col-sm-12 panel panel-default" style="padding-top:10px; padding-bottom:10px;">
                <asp:Chart ID="ProdChart"  runat="server" Width="1140px" Height="600px" BackColor="245, 245, 250" BorderlineColor="Black" ImageType="Bmp" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)">
                    <Series>
                        <asp:Series Name="Series1" ChartArea="ChartArea1">
                            <Points>
                                <asp:DataPoint AxisLabel="#VALX #VAL" CustomProperties="LabelStyle=Center" XValue="1" YValues="50" />
                                <asp:DataPoint XValue="2" YValues="40" />
                                <asp:DataPoint XValue="3" YValues="60" />
                                <asp:DataPoint XValue="4" YValues="80" />
                                <asp:DataPoint XValue="5" YValues="90" />
                            </Points>
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisX LabelAutoFitStyle="IncreaseFont, DecreaseFont, StaggeredLabels, LabelsAngleStep90">
                                <LabelStyle Angle="-90" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>                
                </asp:Chart>
            </div>
        </div>
    </div>
    


    <link href="../../plugins/bigautocomplete/jquery.bigautocomplete.css" rel="stylesheet" />
    <script src="../../plugins/bigautocomplete/jquery.bigautocomplete.js"></script>
  
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

        jQuery(function ($) {
            $(function () {
                setAutoComplete($('#MainContent_txtPartNo'), 'Laser');
            });
        });
    </script>
                    
</asp:Content>
