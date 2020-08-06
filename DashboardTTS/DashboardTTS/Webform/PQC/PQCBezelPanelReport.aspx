<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PQCBezelPanelReport.aspx.cs" Inherits="DashboardTTS.Webform.PQC.PQCBezelPanelReport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title runat="server" id="title"></title>


    <script src="../../plugins/TableFreeze-master/jquery.js"></script>
    <script src="../../plugins/TableFreeze-master/jquery-migrate-1.2.1.js"></script>
    <script src="../../plugins/TableFreeze-master/TableFreeze.js"></script>



    <link href="../../plugins/bootstrap-3.3.7/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../../plugins/bootstrap-datetimepicker-master/js/bootstrap-datetimepicker.js"></script>
    <link href="../../plugins/bootstrap-datetimepicker-master/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />


    <link href="../../Dashboard CSS JS/CSS/SharedCSS.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        
        <div class="container container-fluid" style="width:100%;" >
            <div class="row titleRow">
                <img class="titleImg" src="../../Resources/Images/headericon.gif" />
                <span class="titleText"><asp:label runat="server" ID="lblUserHeader"/></span>
            </div>

            <div class="row">
                <div class="col-md-12 panel panel-default" style=" margin-bottom:4px;margin-top:6px;">
                    <div class="form-inline" role="form">

                        <div class="form-group searchingBarCol" style="margin:5px 10px 5px 5px;">
                            <label>PQC Date:</label>
                            <asp:TextBox runat="server" ID="txtDateFrom" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd"></asp:TextBox>
                        </div>
                       <%-- <div class="form-group searchingBarCol" style="margin:5px 10px 5px 5px;">
                            <label>Date To:</label>
                            <asp:TextBox runat="server" ID="txtDateTo" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd"></asp:TextBox>
                        </div>--%>

                        <div class="form-group searchingBarCol" style="margin:5px 10px 5px 5px;">
                            <label><asp:label runat="server" ID="lbBezelPanelName"/></label>
                            <asp:DropDownList runat="server" ID="ddlNumber" CssClass="form-control">
                                <asp:ListItem Value="">All</asp:ListItem>
                                <asp:ListItem Value="257">257</asp:ListItem>
                                <asp:ListItem Value="830">830</asp:ListItem>
                                <asp:ListItem Value="831">831</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <%--<div class="form-group searchingBarCol" style="margin:5px 10px 5px 5px;">
                            <label>MFG Date:</label>
                            <asp:TextBox runat="server" ID="txtMFGDate" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd" ></asp:TextBox>
                        </div>
                        
                        <div class="form-group searchingBarCol" style="margin:5px 10px 5px 5px;">
                            <label>Paint Date:</label>
                            <asp:TextBox runat="server" ID="txtPaintDate" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd"></asp:TextBox>
                        </div>
                        
                        <div class="form-group searchingBarCol" style="margin:5px 10px 5px 5px;">
                            <label>PIC:</label>
                            <asp:TextBox runat="server" ID="txtPIC" CssClass="form-control"></asp:TextBox>
                        </div>--%>

                        <div class="form-group searchingBarCol" style="margin:5px 10px 5px 5px; float:right;">
                            <asp:Button ID="Button1" runat="server" Text="Generate" OnClick="BtnGenerate_Click" CssClass="btn-success" style="width:100px; height:34px; border-radius:4px;" />
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div id="divTableContainer" style="overflow: auto;" >
            <asp:DataGrid runat="server" Width ="100%" ID ="dgBezelPanel" CellPadding="10" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" ForeColor="#333333"  CellSpacing="2" AutoGenerateColumns="true" CssClass="table">
                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                <EditItemStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="left" VerticalAlign="Middle"/>
                <ItemStyle BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" BackColor="#F7F6F3" ForeColor="#333333" Wrap="False"  HorizontalAlign="left" VerticalAlign="Middle"  />
                <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" PageButtonCount="5" />
                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <Columns></Columns>
            </asp:DataGrid>
        </div>
      

    </form>




     <script type='text/javascript'>
        $('.formDateTimePicker').datetimepicker({
            weekStart: 1,
            todayBtn: 1,    
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            minView: 2,
            forceParse: 0,
            initialDate: new Date()
        });


        $(document).ready(function () {

            var height = $(window).height() - 120;
            var width = $(window).width() +10;

            $('#divTableContainer').height(height);
            $('#divTableContainer').width(width);



            $("#dgBezelPanel").FrozenTable(4, 0, 1);
        });


      
       
    </script>

</body>
</html>
