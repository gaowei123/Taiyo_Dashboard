<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PQCButtonReport.aspx.cs" Inherits="DashboardTTS.Webform.PQC.PQCButtonReport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Taiyo - Button Report</title>


    <script src="../../plugins/TableFreeze-master/jquery.js"></script>
    <script src="../../plugins/TableFreeze-master/jquery-migrate-1.2.1.js"></script>
    <script src="../../plugins/TableFreeze-master/TableFreeze.js"></script>



    <link href="../../plugins/bootstrap-3.3.7/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../../plugins/bootstrap-datetimepicker-master/js/bootstrap-datetimepicker.js"></script>
    <link href="../../plugins/bootstrap-datetimepicker-master/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />

    <script src="../../Dashboard CSS JS/JS/GlobalConfig.js"></script>
    <script src="../../Dashboard CSS JS/JS/SharedJS.js"></script>
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
                        <div class="form-group searchingBarCol" style="margin:5px;">
                            <label>PQC Date:</label>
                            <asp:TextBox runat="server" ID="txtDateFrom" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd" Width="120"></asp:TextBox>
                        </div>
                        <%--<div class="form-group searchingBarCol" style="margin:5px;">
                            <label>Date To:</label>
                            <asp:TextBox runat="server" ID="txtDateTo" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd" Width="120"></asp:TextBox>
                        </div>--%>

                        <div class="form-group searchingBarCol" style="margin:5px;">
                            <label>Type:</label>
                            <asp:DropDownList runat="server" ID="ddlType" CssClass="form-control" Width="120">
                                <asp:listitem Text="All" Value=""></asp:listitem>
                                <asp:listitem Text="WIP" Value="WIP"></asp:listitem>
                                <asp:listitem Text="Laser" Value="Laser"></asp:listitem>
                            </asp:DropDownList>
                        </div>

                        <%--<div class="form-group searchingBarCol">
                            <label>Model:</label>
                            <asp:DropDownList runat="server" ID="ddlModel" CssClass="form-control" Width="120"></asp:DropDownList>
                        </div>

                        <div class="form-group searchingBarCol" style="margin:5px;">
                            <label>Part No:</label>
                            <asp:TextBox runat="server" ID="txtPartNo" CssClass="form-control" Width="120"></asp:TextBox>
                        </div>
                        <div class="form-group searchingBarCol" style="margin:5px;">
                            <label>Supplier:</label>
                            <asp:DropDownList runat="server" ID="ddlSupplier" CssClass="form-control" Width="120"></asp:DropDownList>
                        </div>
                        <div class="form-group searchingBarCol" style="margin:5px;">
                            <label>Color:</label>
                            <asp:DropDownList runat="server" ID="ddlColor" CssClass="form-control" Width="120"></asp:DropDownList>
                        </div>
                        <div class="form-group searchingBarCol" style="margin:5px;">
                            <label>Coating:</label>
                            <asp:DropDownList runat="server" ID="ddlCoating" CssClass="form-control" Width="120">
                                <asp:listitem Text="All" Value=""></asp:listitem>
                                <asp:listitem Text="One Coat" Value="One Coat"></asp:listitem>
                                <asp:listitem Text="Two Coat" Value="Two Coat"></asp:listitem>
                                <asp:listitem Text="Three Coat" Value="Three Coat"></asp:listitem>
                                <asp:listitem Text="Print Coat" Value="Print Coat"></asp:listitem>
                            </asp:DropDownList>
                        </div>
                        
                        <div class="form-group searchingBarCol" style="margin:5px;">
                            <label>Job No:</label>
                            <asp:TextBox runat="server" ID="txtJobNo" CssClass="form-control" Width="120"></asp:TextBox>
                        </div>--%>

                        <div class="form-group searchingBarCol" style="margin:5px; float:right;">
                            <asp:Button ID="Button1" runat="server" Text="Generate" OnClick="BtnGenerate_Click" CssClass="btn-success" style="width:100px; height:34px; border-radius:4px;" />
                        </div>
                    </div>
                </div>
            </div>


        </div>


        <div id="divTableContainer" style="overflow: auto;" >

            <asp:DataGrid runat="server" Width ="100%" ID ="dgButton" CellPadding="10" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" ForeColor="#333333"  CellSpacing="2" AutoGenerateColumns="false" CssClass="table">
                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                <EditItemStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="left" VerticalAlign="Middle"/>
                <ItemStyle BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" BackColor="#F7F6F3" ForeColor="#333333" Wrap="False"  HorizontalAlign="left" VerticalAlign="Middle"  />
                <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" PageButtonCount="5" />
                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <Columns>

                    <asp:BoundColumn DataField="SN" HeaderText="SN"></asp:BoundColumn>
                    <asp:BoundColumn DataField="model" HeaderText="Model" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="jobID" HeaderText="Job No"></asp:BoundColumn>
                    <asp:BoundColumn DataField="lotNo" HeaderText="Lot No" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="partNo" HeaderText="Part No"></asp:BoundColumn>
                    <asp:BoundColumn DataField="materialNo" HeaderText="Material No"></asp:BoundColumn>
                    <asp:BoundColumn DataField="lotQty" HeaderText="Lot<br/>Qty"></asp:BoundColumn>
                    <asp:BoundColumn DataField="pass" HeaderText="Pass"></asp:BoundColumn>
                    <asp:BoundColumn DataField="rejQty" HeaderText="Rej Qty" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="rejRate" HeaderText="Rej%" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="rejRateDisplay" HeaderText="Rej(%)"></asp:BoundColumn>
                    <%--<asp:BoundColumn DataField="supplier" HeaderText="Supplier" Visible="false"></asp:BoundColumn>--%>
                    <asp:BoundColumn DataField="rejCost" HeaderText="Rej<br/>Amount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>


                    <%--12--%>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Raw_Part_Scratch" HeaderText="(TM)<br/>Raw Part<br/>Scratch"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Oil_Stain" HeaderText="(TM)<br/>Oil Stain"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Dented" HeaderText="(TM)<br/>Dented"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Dust" HeaderText="(TM)<br/>Dust"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Flyout" HeaderText="(TM)<br/>Flyout"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Over_Spray" HeaderText="(TM)<br/>Over<br/>Spray"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Weld_line" HeaderText="(TM)<br/>Weld<br/>line"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Crack" HeaderText="(TM)<br/>Crack"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Gas_mark" HeaderText="(TM)<br/>Gas<br/>mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Sink_mark" HeaderText="(TM)<br/>Sink<br/>mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Bubble" HeaderText="(TM)<br/>Bubble"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_White_dot" HeaderText="(TM)<br/>White<br/>dot"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Black_dot" HeaderText="(TM)<br/>Black<br/>dot"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Red_Dot" HeaderText="(TM)<br/>Red<br/>Dot"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Poor_Gate_Cut" HeaderText="(TM)<br/>Poor<br/>Gate Cut"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_High_Gate" HeaderText="(TM)<br/>High<br/>Gate"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_White_Mark" HeaderText="(TM)<br/>White<br/>Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Drag_mark" HeaderText="(TM)<br/>Drag<br/>mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Foreigh_Material" HeaderText="(TM)<br/>Foreigh<br/>Material"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Double_Claim" HeaderText="(TM)<br/>Double<br/>Claim"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Short_mould" HeaderText="(TM)<br/>Short<br/>mould"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Flashing" HeaderText="(TM)<br/>Flashing"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Pink_Mark" HeaderText="(TM)<br/>Pink<br/>Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Deform" HeaderText="(TM)<br/>Deform"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Damage" HeaderText="(TM)<br/>Damage"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Mould_Dirt" HeaderText="(TM)<br/>Mould<br/>Dirt"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Yellowish" HeaderText="(TM)<br/>Yellowish"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Oil_Mark" HeaderText="(TM)<br/>Oil<br/>Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Printing_Mark" HeaderText="(TM)<br/>Printing<br/>Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Printing_Uneven" HeaderText="(TM)<br/>Printing<br/>Uneven"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Printing_Color_Dark" HeaderText="(TM)<br/>Printing<br/>Color Dark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Wrong_Orietation" HeaderText="(TM)<br/>Wrong<br/>Orietation"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Other" HeaderText="(TM)<br/>Other"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Mould_TotalRej" HeaderText="(TM)<br/>TotalRej"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Mould_TotalRejRate" HeaderText="(TM)<br/>TotalRej%"></asp:BoundColumn>

                    <%--47--%>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Raw_Part_Scratch" HeaderText="(VM)<br/>Raw Part<br/>Scratch"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Oil_Stain" HeaderText="(VM)<br/>Oil Stain"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Dented" HeaderText="(VM)<br/>Dented"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Dust" HeaderText="(VM)<br/>Dust"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Flyout" HeaderText="(VM)<br/>Flyout"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Over_Spray" HeaderText="(VM)<br/>Over<br/>Spray"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Weld_line" HeaderText="(VM)<br/>Weld<br/>line"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Crack" HeaderText="(VM)<br/>Crack"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Gas_mark" HeaderText="(VM)<br/>Gas<br/>mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Sink_mark" HeaderText="(VM)<br/>Sink<br/>mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Bubble" HeaderText="(VM)<br/>Bubble"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_White_dot" HeaderText="(VM)<br/>White<br/>dot"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Black_dot" HeaderText="(VM)<br/>Black<br/>dot"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Red_Dot" HeaderText="(VM)<br/>Red<br/>Dot"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Poor_Gate_Cut" HeaderText="(VM)<br/>Poor<br/>Gate Cut"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_High_Gate" HeaderText="(VM)<br/>High<br/>Gate"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_White_Mark" HeaderText="(VM)<br/>White<br/>Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Drag_mark" HeaderText="(VM)<br/>Drag<br/>mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Foreigh_Material" HeaderText="(VM)<br/>Foreigh<br/>Material"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Double_Claim" HeaderText="(VM)<br/>Double<br/>Claim"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Short_mould" HeaderText="(VM)<br/>Short<br/>mould"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Flashing" HeaderText="(VM)<br/>Flashing"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Pink_Mark" HeaderText="(VM)<br/>Pink<br/>Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Deform" HeaderText="(VM)<br/>Deform"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Damage" HeaderText="(VM)<br/>Damage"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Mould_Dirt" HeaderText="(VM)<br/>Mould<br/>Dirt"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Yellowish" HeaderText="(VM)<br/>Yellowish"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Oil_Mark" HeaderText="(VM)<br/>Oil<br/>Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Printing_Mark" HeaderText="(VM)<br/>Printing<br/>Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Printing_Uneven" HeaderText="(VM)<br/>Printing<br/>Uneven"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Printing_Color_Dark" HeaderText="(VM)<br/>Printing<br/>Color Dark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Wrong_Orietation" HeaderText="(VM)<br/>Wrong<br/>Orietation"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Other" HeaderText="(VM)<br/>Other"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Mould_TotalRej" HeaderText="(VM)<br/>TotalRej"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Mould_TotalRejRate" HeaderText="(VM)<br/>TotalRej%"></asp:BoundColumn>

                    <asp:BoundColumn DataField="MFGDate" HeaderText="MFG Date"></asp:BoundColumn>
                            

                    <%--83--%>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Particle" HeaderText="(P)<br/>Particle"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Fibre" HeaderText="(P)<br/>Fibre"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Many_particle" HeaderText="(P)<br/>Many<br/>particle"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Stain_mark" HeaderText="(P)<br/>Stain<br/>mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Uneven_paint" HeaderText="(P)<br/>Uneven"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Under_coat_uneven_paint" HeaderText="(P)<br/>Under coat<br/>uneven"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Under_spray" HeaderText="(P)<br/>Under<br/>spray"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_White_dot" HeaderText="(P)<br/>White<br/>dot"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Silver_dot" HeaderText="(P)<br/>Silver<br/>dot"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Dust" HeaderText="(P)<br/>Dust"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Paint_crack" HeaderText="(P)<br/>crack"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Bubble" HeaderText="(P)<br/>Bubble"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Scratch" HeaderText="(P)<br/>Scratch"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Abrasion_Mark" HeaderText="(P)<br/>Abrasion<br/>Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Paint_Dripping" HeaderText="(P)<br/>Dripping"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Rough_Surface" HeaderText="(P)<br/>Rough<br/>Surface"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Shinning" HeaderText="(P)<br/>Shinning"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Matt" HeaderText="(P)<br/>Matt"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Paint_Pin_Hole" HeaderText="(P)<br/>Pin<br/>Hole"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Light_Leakage" HeaderText="(P)<br/>Light<br/>Leakage"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_White_Mark" HeaderText="(P)<br/>White<br/>Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Dented" HeaderText="(P)<br/>Dented"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Other" HeaderText="(P)<br/>Other"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Particle_for_laser_setup" HeaderText="(P)<br/>Particle for<br/>laser setup"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Buyoff" HeaderText="(P)<br/>Buyoff"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Shortage" HeaderText="(P)<br/>Shortage"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_TotalRej" HeaderText="(P)<br/>TotalRej"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_TotalRejRate" HeaderText="(P)<br/>TotalRej%"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_SetupRej" HeaderText="(P)<br/>SetupRej"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_SetupRejRate" HeaderText="(P)<br/>SetupRej%"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_QATestRej" HeaderText="(P)QA<br/>Reliability<br/>test Qty"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_QATestRejRate" HeaderText="(P)<br/>Qty%"></asp:BoundColumn>

                    <asp:BoundColumn DataField="paintCoat1st" HeaderText="(P)U/C<br/>1st Coat"></asp:BoundColumn>
                    <asp:BoundColumn DataField="paintMachine1st" HeaderText="(P)1st<br/>Machine"></asp:BoundColumn>
                    <asp:BoundColumn DataField="paintDate1st" HeaderText="(P)1st<br/>Date"></asp:BoundColumn>
                    <asp:BoundColumn DataField="paintCoat2nd" HeaderText="(P)M/C<br/>2nd Coat"></asp:BoundColumn>
                    <asp:BoundColumn DataField="paintMachine2nd" HeaderText="(P)2nd<br/>Machine"></asp:BoundColumn>
                    <asp:BoundColumn DataField="paintDate2nd" HeaderText="(P)2nd<br/>Date"></asp:BoundColumn>
                    <asp:BoundColumn DataField="paintCoat3rd" HeaderText="(P)T/C<br/>3rd Coat"></asp:BoundColumn>
                    <asp:BoundColumn DataField="paintMachine3rd" HeaderText="(P)3rd<br/>Machine"></asp:BoundColumn>
                    <asp:BoundColumn DataField="paintDate3rd" HeaderText="(P)3rd<br/>Date"></asp:BoundColumn>

                    <%--124--%>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Black_Mark" HeaderText="(L)<br/>Black<br/>Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Black_Dot" HeaderText="(L)<br/>Black<br/>Dot"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Graphic_Shift_check_by_PQC" HeaderText="(L)Graphic<br/>Shift check<br/>by PQC"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Graphic_Shift_check_by_MC" HeaderText="(L)Graphic<br/>Shift check<br/>by MC"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Scratch" HeaderText="(L)<br/>Scratch"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Jagged" HeaderText="(L)<br/>Jagged"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Laser_Bubble" HeaderText="(L)<br/>Bubble"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_double_outer_line" HeaderText="(L)double<br/>outer<br/>line"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Pin_hold" HeaderText="(L)<br/>Pin<br/>hold"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Poor_Laser" HeaderText="(L)<br/>Poor"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Burm_Mark" HeaderText="(L)<br/>Burm<br/>Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Stain_Mark" HeaderText="(L)<br/>Stain<br/>Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Graphic_Small" HeaderText="(L)<br/>Graphic<br/>Small"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Double_Laser" HeaderText="(L)<br/>Double"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Color_Yellow" HeaderText="(L)<br/>Color<br/>Yellow"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Crack" HeaderText="(L)<br/>Crack"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Smoke" HeaderText="(L)<br/>Smoke"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Wrong_Orientation" HeaderText="(L)<br/>Wrong<br/>Orientation"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Dented" HeaderText="(L)<br/>Dented"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Other" HeaderText="(L)<br/>Other"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Buyoff" HeaderText="(L)<br/>Buyoff"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Setup" HeaderText="(L)<br/>Setup"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_TotalRej" HeaderText="(L)<br/>TotalRej"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_TotalRejRate" HeaderText="(L)<br/>TotalRej%"></asp:BoundColumn>

                    <asp:BoundColumn DataField="laserMachine" HeaderText="(L)<br/>Machine"></asp:BoundColumn>
                    <asp:BoundColumn DataField="laserOP" HeaderText="(L)OP"></asp:BoundColumn>
                    <asp:BoundColumn DataField="laserDate" HeaderText="(L)Date"></asp:BoundColumn>


                    <%--152--%>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="PQC_Scratch" HeaderText="(O)<br/>PQC<br/>Scratch"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="Over_Spray" HeaderText="(O)<br/>Over<br/>Spray"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="Bubble" HeaderText="(O)<br/>Bubble"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="Oil_Stain" HeaderText="(O)<br/>Oil<br/>Stain"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="Drag_Mark" HeaderText="(O)<br/>Drag<br/>Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="Light_Leakage" HeaderText="(O)<br/>Light<br/>Leakage"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="Light_Bubble" HeaderText="(O)<br/>Light<br/>Bubble"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="White_Dot_in_Material" HeaderText="(O)White Dot<br/>in Material"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="Other" HeaderText="(O)<br/>Other"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="Others_TotalRej" HeaderText="(O)<br/>TotalRej"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="Others_TotalRejRate" HeaderText="(O)<br/>TotalRej%"></asp:BoundColumn>

                    <asp:BoundColumn DataField="InspBy" HeaderText="Insp By"></asp:BoundColumn>
                            
                </Columns>
            </asp:DataGrid>
        </div>
      
    </form>



    <link href="../../plugins/bigautocomplete/jquery.bigautocomplete.css" rel="stylesheet" />
    <script src="../../plugins/bigautocomplete/jquery.bigautocomplete.js"></script>

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

            setAutoComplete($('#txtPartNo'), 'Laser');

            var height = $(window).height() - 120;
            var width = $(window).width() + 10;

            $('#divTableContainer').height(height);
            $('#divTableContainer').width(width);

            
            
            $("#dgButton").FrozenTable(1, 0, 7);

        });

    </script>

</body>
</html>
