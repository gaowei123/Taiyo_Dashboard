<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BuyOffRecordForClient.aspx.cs" Inherits="DashboardTTS.Webform.PQC.BuyOffRecordForClient" %>
  
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8; "/>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no" />
    <title>PQC Buyoff Record</title>
   

    <link href="../../plugins/bootstrap-datetimepicker-master/sample in bootstrap v3/bootstrap/css/bootstrap.min.css" rel="stylesheet" media="screen" />
    <link href="../../plugins/bootstrap-datetimepicker-master/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../../plugins/bootstrap-datetimepicker-master/sample in bootstrap v3/jquery/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="../../plugins/bootstrap-datetimepicker-master/sample in bootstrap v3/bootstrap/js/bootstrap.js"></script>
    <script type="text/javascript" src="../../plugins/bootstrap-datetimepicker-master/js/bootstrap-datetimepicker.js"></script>
    <script type="text/javascript" src="../../plugins/bootstrap-datetimepicker-master/js/locales/bootstrap-datetimepicker.zh-CN.js"></script>

    <link href="../../Dashboard CSS JS/CSS/SharedCSS.css" rel="stylesheet" />



    <style>
        .tr{
            width:100%;
        }
        .tableMain{
            width: 100%; 
            border-collapse: separate; 
            border-spacing: 10px; 
            table-layout:fixed; 
            line-height: 10px; 
            vertical-align: 10%;
        }
        .tdBig{
            padding: 0px 10px 0px 10px; 
            border: 1px solid #CCCCCC; 
            font-family: 'Arial Unicode MS'; 
            height: 38px;
        }
        .tdSmall{
            padding: 3px 3px 3px 3px; 
            border: 1px solid #CCCCCC; 
            font-family: 'Arial Unicode MS';
            height: 30px; 
        }
    </style>


</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row titleRow"  style="margin-top:10px;">
                <span class="titleText">PAINTING BUY-OFF RECORD</span>
                <asp:TextBox runat="server" ID="txtUserName"  Visible="False"></asp:TextBox>
                <asp:Label runat="server" ID="lbCheckProcess" Visible="false"></asp:Label>
            </div>

            <div class="row">
                <div class="col-md-12 panel panel-default" style="margin-top:10px;padding:0px;">
                    <table class="tableMain"> 
                        <tr> 
                            <td class="tdSmall">Job No:</td>
                            <td class="tdSmall">
                                <asp:TextBox runat="server" ID="txtJobNumber" AutoCompleteType="None" Width="100%" Height="23px" ></asp:TextBox>
                            </td>

                            <td class="tdSmall">Setup Rej Qty:</td>
                            <td class="tdSmall">
                                <asp:TextBox runat="server" ID="txtSetupRejQty" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                            </td>

                            <td class="tdSmall">QA Reliability Test Qty:</td>
                            <td class="tdSmall">
                                <asp:TextBox runat="server" ID="txtQATestQty" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr> 
                            <td class="tdSmall">MFG Date:</td>
                            <td class="tdSmall">
                                <asp:TextBox runat="server" ID="txtMFGDate" AutoCompleteType="Disabled" CssClass="form-control formDateTimePicker" Height="23px" type="text" value=""  data-date-format="yyyy-mm-dd"   Width="100%" ></asp:TextBox>
                            </td>

                            <td class="tdSmall">Temperature Front:</td>
                            <td class="tdSmall">
                                <asp:TextBox runat="server" ID="txtTempFront" AutoCompleteType="Disabled" Width="100%" Height="23px"></asp:TextBox>
                            </td>
                            <td class="tdSmall">Temperature Rear:</td>
                            <td class="tdSmall">
                                <asp:TextBox runat="server" ID="txtTempRear" AutoCompleteType="Disabled" Width="100%" Height="23px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr> 
                            <td class="tdSmall">Humidity Front:</td>
                            <td class="tdSmall">
                                <asp:TextBox runat="server" ID="txtHumidityFront" AutoCompleteType="Disabled" Width="100%" Height="23px"></asp:TextBox>
                            </td>
                            <td class="tdSmall">Humidity Rear:</td>
                            <td class="tdSmall">
                                <asp:TextBox runat="server" ID="txtHumidityRear" AutoCompleteType="Disabled" Width="100%" Height="23px"></asp:TextBox>
                            </td>
                            <td class="tdSmall"></td>
                            <td class="tdSmall"></td>
                        </tr>

                        <tr> 
                            <td class="tdSmall">Annealing Date:</td>
                            <td class="tdSmall">
                               <asp:TextBox runat="server" ID="txtAnnealingDate" AutoCompleteType="Disabled" CssClass="form-control formDateTimePicker" Height="23px" type="text" value=""  data-date-format="yyyy-mm-dd"   Width="100%" ></asp:TextBox>
                            </td>
                            <td class="tdSmall" colspan="2">
                                &nbsp;<asp:TextBox runat="server" ID="txtAnnealingTime" AutoCompleteType="Disabled" Height="23px" Width="100%"></asp:TextBox>
                            </td>                           
                            <td class="tdSmall"></td>
                            <td class="tdSmall"></td>
                        </tr>




                        <tr> 
                            <td class="tdBig" colspan="2" align="center">
                                <h4><b>UNDER COAT</b></h4>
                            </td>
                            <td class="tdBig" colspan="2" align="center">
                                <h4><b>MIDDLE COAT</b></h4>
                            </td>
                            <td class="tdBig" colspan="2" align="center">
                                <h4><b>TOP COAT</b></h4>
                            </td>
                        </tr>
                

                        <tr> 
                            <td class="tdSmall">Date:</td>
                             <td class="tdSmall"> 
                                <asp:TextBox runat="server" ID="txtUnderCoatDate"   AutoCompleteType="Disabled"     CssClass="form-control formDateTimePicker" type="text" value="" data-date-format="yyyy-mm-dd" Height="23px" Width="100%" ></asp:TextBox>
                             </td>

                            <td class="tdSmall">Date:</td>
                            <td class="tdSmall">
                                <asp:TextBox runat="server" ID="txtMiddleCoatDate"  AutoCompleteType ="Disabled"    CssClass="form-control formDateTimePicker" type="text" value="" data-date-format="yyyy-mm-dd" Height="23px" Width="100%" ></asp:TextBox>   
                            </td>

                            <td class="tdSmall">Date:</td>
                            <td class="tdSmall">
                                <asp:TextBox runat="server" ID="txtTopCoatDate"     AutoCompleteType="Disabled"     CssClass="form-control formDateTimePicker" type="text" value="" data-date-format="yyyy-mm-dd" Height="23px" Width="100%" ></asp:TextBox>   
                            </td>
                        </tr>
                        <tr> 
                           <td class="tdSmall">Running Time:</td>
                           <td class="tdSmall">
                                <table style="width:100%">
                                    <tr>
                                        <td style="width:45%;"><asp:DropDownList runat="server" ID="ddlUnderCoatRunTimeHH" Width="100%" Height="23px"></asp:DropDownList></td>
                                        <td style="width:10%" align="center">:</td>
                                        <td style="width:45%;"><asp:DropDownList runat="server" ID="ddlUnderCoatRunTimeMM" Width="100%" Height="23px"></asp:DropDownList></td>
                                    </tr>
                                </table>
                            </td>

                           <td class="tdSmall">Running Time:</td>
                           <td class="tdSmall">
                                <table style="width:100%">
                                    <tr>
                                        <td style="width:45%;"><asp:DropDownList runat="server" ID="ddlMiddleCoatRunTimeHH" Width="100%" Height="23px"></asp:DropDownList></td>
                                        <td style="width:10%" align="center">:</td>
                                        <td style="width:45%;"><asp:DropDownList runat="server" ID="ddlMiddleCoatRunTimeMM" Width="100%" Height="23px"></asp:DropDownList></td>
                                    </tr>
                                </table>
                            </td>

                            <td class="tdSmall">Running Time:</td>
                            <td class="tdSmall">
                                <table style="width:100%">
                                    <tr>
                                        <td style="width:45%;"><asp:DropDownList runat="server" ID="ddlTopCoatRunTimeHH" Width="100%" Height="23px"></asp:DropDownList></td>
                                        <td style="width:10%" align="center">:</td>
                                        <td style="width:45%;"><asp:DropDownList runat="server" ID="ddlTopCoatRunTimeMM" Width="100%" Height="23px"></asp:DropDownList></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr> 
                            <td class="tdSmall">Machine No:</td>
                            <td class="tdSmall">
                                <asp:DropDownList runat="server" ID="ddlUnderCoatMachineNo" Width="100%" Height="23px"></asp:DropDownList>
                            </td>

                            <td class="tdSmall">Machine No:</td>
                            <td class="tdSmall">
                                <asp:DropDownList runat="server" ID="ddlMiddleCoatMachineNo" Width="100%" Height="23px"></asp:DropDownList>
                            </td>

                            <td class="tdSmall">Machine No:</td>
                            <td class="tdSmall">
                                <asp:DropDownList runat="server" ID="ddlTopCoatMachineNo" Width="100%" Height="23px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr> 
                            <td class="tdSmall">Paint Lot:</td>
                            <td class="tdSmall">
                                <asp:TextBox runat="server" ID="txtUnderCoatPaintLot" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                            </td>

                            <td class="tdSmall">Paint Lot:</td>
                            <td class="tdSmall">
                                <asp:TextBox runat="server" ID="txtMiddleCoatPaintLot" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                            </td>

                            <td class="tdSmall">Paint Lot:</td>
                            <td class="tdSmall">
                                <asp:TextBox runat="server" ID="txtTopCoatPaintLot" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr> 
                           <td class="tdSmall">Thinners Lot:</td>
                           <td class="tdSmall">
                                <asp:TextBox runat="server" ID="txtUnderCoatThinnersLot" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                            </td>

                           <td class="tdSmall">Thinners Lot:</td>
                           <td class="tdSmall">
                                <asp:TextBox runat="server" ID="txtMiddleCoatThinnersLot" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                            </td>

                           <td class="tdSmall">Thinners Lot:</td>
                           <td class="tdSmall">
                                <asp:TextBox runat="server" ID="txtTopCoatThinnersLot" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr> 
                            <td class="tdSmall">Painting Thickness:</td>
                            <td class="tdSmall">
                                <asp:TextBox runat="server" ID="txtUnderCoatThickness" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                            </td>

                            <td class="tdSmall">Painting Thickness:</td>
                            <td class="tdSmall">
                                <asp:TextBox runat="server" ID="txtMiddleCoatThickness" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                            </td>

                            <td class="tdSmall">Painting Thickness:</td>
                            <td class="tdSmall">
                                <asp:TextBox runat="server" ID="txtTopCoatThickness" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr> 
                            <td class="tdSmall">Painting PIC:</td>
                            <td class="tdSmall">
                                <asp:DropDownList runat="server" ID="ddlUnderCoatPIC" Width="100%" Height="23px"></asp:DropDownList>
                            </td>

                            <td class="tdSmall">Painting PIC:</td>
                            <td class="tdSmall">
                                <asp:DropDownList runat="server" ID="ddlMiddleCoatPIC" Width="100%" Height="23px"></asp:DropDownList>
                            </td>

                            <td class="tdSmall">Painting PIC:</td>
                            <td class="tdSmall">
                                <asp:DropDownList runat="server" ID="ddlTopCoatPIC" Width="100%" Height="23px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr> 
                           <td class="tdSmall">Oven In Time:</td>
                           <td class="tdSmall">
                                <table style="width:100%">
                                    <tr>
                                        <td style="width:45%;"><asp:DropDownList runat="server" ID="ddlUnderCoatOvenTimeHH" Width="100%" Height="23px"></asp:DropDownList></td>
                                        <td style="width:10%" align="center">:</td>
                                        <td style="width:45%;"><asp:DropDownList runat="server" ID="ddlUnderCoatOvenTimeMM" Width="100%" Height="23px"></asp:DropDownList></td>
                                    </tr>
                                </table>
                            </td>

                           <td class="tdSmall">Oven In Time:</td>
                           <td class="tdSmall">
                                <table style="width:100%">
                                    <tr>
                                        <td style="width:45%;"><asp:DropDownList runat="server" ID="ddlMiddleCoatOvenTimeHH" Width="100%" Height="23px"></asp:DropDownList></td>
                                        <td style="width:10%" align="center">:</td>
                                        <td style="width:45%;"><asp:DropDownList runat="server" ID="ddlMiddleCoatOvenTimeMM" Width="100%" Height="23px"></asp:DropDownList></td>
                                    </tr>
                                </table>
                            </td>

                           <td class="tdSmall">Oven In Time:</td>
                           <td class="tdSmall">
                                <table style="width:100%">
                                    <tr>
                                        <td style="width:45%;"><asp:DropDownList runat="server" ID="ddlTopCoatOvenTimeHH" Width="100%" Height="23px"></asp:DropDownList></td>
                                        <td style="width:10%" align="center">:</td>
                                        <td style="width:45%;"><asp:DropDownList runat="server" ID="ddlTopCoatOvenTimeMM" Width="100%" Height="23px"></asp:DropDownList></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>


                        <tr> 
                            <td class="tdBig" colspan="3" align="center">
                                <asp:Button ID="btn_Confirm" runat="server" Text="Confirm" OnClick="btn_Confirm_Click"  CssClass="btn-success" Height="30px" Width="40%" />
                            </td>
                            <td class="tdBig" colspan="3" align="center">
                                <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" OnClick="btn_Cancel_Click"  CssClass="btn-danger" Height="30px" Width="40%"  />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

        </div>
    </form>

    <script type="text/javascript">
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
    </script>
  



</body>
</html>