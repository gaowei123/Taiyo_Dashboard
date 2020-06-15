<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PQCButtonReport.aspx.cs" Inherits="DashboardTTS.Webform.PQC.PQCButtonReport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Taiyo Dashboard</title>


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
                            <label>Date From:</label>
                            <asp:TextBox runat="server" ID="txtDateFrom" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd" Width="120"></asp:TextBox>
                        </div>
                        <div class="form-group searchingBarCol" style="margin:5px;">
                            <label>Date To:</label>
                            <asp:TextBox runat="server" ID="txtDateTo" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd" Width="120"></asp:TextBox>
                        </div>

                        <div class="form-group searchingBarCol" style="margin:5px;">
                            <label>Type:</label>
                            <asp:DropDownList runat="server" ID="ddlType" CssClass="form-control" Width="120">
                                <asp:listitem Text="All" Value=""></asp:listitem>
                                <asp:listitem Text="WIP" Value="WIP"></asp:listitem>
                                <asp:listitem Text="Laser" Value="Laser"></asp:listitem>
                            </asp:DropDownList>
                        </div>

                        <div class="form-group searchingBarCol">
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
                        </div>

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
                    <asp:BoundColumn DataField="model" HeaderText="Model"></asp:BoundColumn>
                    <asp:BoundColumn DataField="lotNo" HeaderText="Lot No"></asp:BoundColumn>
                    <asp:BoundColumn DataField="partNo" HeaderText="Part No"></asp:BoundColumn>
                    <asp:BoundColumn DataField="materialNo" HeaderText="Material No"></asp:BoundColumn>
                    <asp:BoundColumn DataField="lotQty" HeaderText="Lot Qty"></asp:BoundColumn>
                    <asp:BoundColumn DataField="pass" HeaderText="Pass"></asp:BoundColumn>
                    <asp:BoundColumn DataField="rejQty" HeaderText="Rej Qty"></asp:BoundColumn>
                    <asp:BoundColumn DataField="rejRate" HeaderText="Rej%"></asp:BoundColumn>
                    <asp:BoundColumn DataField="supplier" HeaderText="Supplier"></asp:BoundColumn>


                          
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Raw_Part_Scratch" HeaderText="(TM)Raw Part Scratch"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Oil_Stain" HeaderText="(TM)Oil Stain"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Dented" HeaderText="(TM)Dented"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Dust" HeaderText="(TM)Dust"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Flyout" HeaderText="(TM)Flyout"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Over_Spray" HeaderText="(TM)Over Spray"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Weld_line" HeaderText="(TM)Weld line"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Crack" HeaderText="(TM)Crack"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Gas_mark" HeaderText="(TM)Gas mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Sink_mark" HeaderText="(TM)Sink mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Bubble" HeaderText="(TM)Bubble"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_White_dot" HeaderText="(TM)White dot"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Black_dot" HeaderText="(TM)Black dot"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Red_Dot" HeaderText="(TM)Red Dot"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Poor_Gate_Cut" HeaderText="(TM)Poor Gate Cut"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_High_Gate" HeaderText="(TM)High Gate"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_White_Mark" HeaderText="(TM)White Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Drag_mark" HeaderText="(TM)Drag mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Foreigh_Material" HeaderText="(TM)Foreigh Material"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Double_Claim" HeaderText="(TM)Double Claim"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Short_mould" HeaderText="(TM)Short mould"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Flashing" HeaderText="(TM)Flashing"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Pink_Mark" HeaderText="(TM)Pink Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Deform" HeaderText="(TM)Deform"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Damage" HeaderText="(TM)Damage"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Mould_Dirt" HeaderText="(TM)Mould Dirt"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Yellowish" HeaderText="(TM)Yellowish"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Oil_Mark" HeaderText="(TM)Oil Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Printing_Mark" HeaderText="(TM)Printing Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Printing_Uneven" HeaderText="(TM)Printing Uneven"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Printing_Color_Dark" HeaderText="(TM)Printing Color Dark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Wrong_Orietation" HeaderText="(TM)Wrong Orietation"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Other" HeaderText="(TM)Other"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Mould_TotalRej" HeaderText="(TM)TotalRej"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Mould_TotalRejRate" HeaderText="(TM)TotalRej%"></asp:BoundColumn>

                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Raw_Part_Scratch" HeaderText="(VM)Raw Part Scratch"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Oil_Stain" HeaderText="(VM)Oil Stain"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Dented" HeaderText="(VM)Dented"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Dust" HeaderText="(VM)Dust"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Flyout" HeaderText="(VM)Flyout"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Over_Spray" HeaderText="(VM)Over Spray"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Weld_line" HeaderText="(VM)Weld line"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Crack" HeaderText="(VM)Crack"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Gas_mark" HeaderText="(VM)Gas mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Sink_mark" HeaderText="(VM)Sink mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Bubble" HeaderText="(VM)Bubble"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_White_dot" HeaderText="(VM)White dot"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Black_dot" HeaderText="(VM)Black dot"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Red_Dot" HeaderText="(VM)Red Dot"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Poor_Gate_Cut" HeaderText="(VM)Poor Gate Cut"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_High_Gate" HeaderText="(VM)High Gate"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_White_Mark" HeaderText="(VM)White Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Drag_mark" HeaderText="(VM)Drag mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Foreigh_Material" HeaderText="(VM)Foreigh Material"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Double_Claim" HeaderText="(VM)Double Claim"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Short_mould" HeaderText="(VM)Short mould"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Flashing" HeaderText="(VM)Flashing"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Pink_Mark" HeaderText="(VM)Pink Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Deform" HeaderText="(VM)Deform"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Damage" HeaderText="(VM)Damage"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Mould_Dirt" HeaderText="(VM)Mould Dirt"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Yellowish" HeaderText="(VM)Yellowish"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Oil_Mark" HeaderText="(VM)Oil Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Printing_Mark" HeaderText="(VM)Printing Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Printing_Uneven" HeaderText="(VM)Printing Uneven"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Printing_Color_Dark" HeaderText="(VM)Printing Color Dark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Wrong_Orietation" HeaderText="(VM)Wrong Orietation"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Other" HeaderText="(VM)Other"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Mould_TotalRej" HeaderText="(VM)TotalRej"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fce4d6" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Mould_TotalRejRate" HeaderText="(VM)TotalRej%"></asp:BoundColumn>

                    <asp:BoundColumn DataField="MFGDate" HeaderText="MFG Date"></asp:BoundColumn>
                            

                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Particle" HeaderText="(P)Particle"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Fibre" HeaderText="(P)Fibre"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Many_particle" HeaderText="(P)Many particle"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Stain_mark" HeaderText="(P)Stain mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Uneven_paint" HeaderText="(P)Uneven (P)"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Under_coat_uneven_paint" HeaderText="(P)Under coat uneven (P)"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Under_spray" HeaderText="(P)Under spray"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_White_dot" HeaderText="(P)White dot"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Silver_dot" HeaderText="(P)Silver dot"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Dust" HeaderText="(P)Dust"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Paint_crack" HeaderText="(P)(P) crack"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Bubble" HeaderText="(P)Bubble"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Scratch" HeaderText="(P)Scratch"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Abrasion_Mark" HeaderText="(P)Abrasion Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Paint_Dripping" HeaderText="(P)(P) Dripping"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Rough_Surface" HeaderText="(P)Rough Surface"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Shinning" HeaderText="(P)Shinning"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Matt" HeaderText="(P)Matt"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Paint_Pin_Hole" HeaderText="(P)(P) Pin Hole"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Light_Leakage" HeaderText="(P)Light Leakage"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_White_Mark" HeaderText="(P)White Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Dented" HeaderText="(P)Dented"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Other" HeaderText="(P)Other"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Particle_for_laser_setup" HeaderText="(P)Particle for laser setup"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Buyoff" HeaderText="(P)Buyoff"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Shortage" HeaderText="(P)Shortage"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_TotalRej" HeaderText="(P)TotalRej"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_TotalRejRate" HeaderText="(P)TotalRej%"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_SetupRej" HeaderText="(P)SetupRej"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_SetupRejRate" HeaderText="(P)SetupRej%"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_QATestRej" HeaderText="(P)QA Reliability test Qty"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_QATestRejRate" HeaderText="(P)Qty%"></asp:BoundColumn>

                    <asp:BoundColumn DataField="paintCoat1st" HeaderText="(P)U/C 1st Coat"></asp:BoundColumn>
                    <asp:BoundColumn DataField="paintMachine1st" HeaderText="(P)1st Machine"></asp:BoundColumn>
                    <asp:BoundColumn DataField="paintDate1st" HeaderText="(P)1st Date"></asp:BoundColumn>
                    <asp:BoundColumn DataField="paintCoat2nd" HeaderText="(P)M/C 2nd Coat"></asp:BoundColumn>
                    <asp:BoundColumn DataField="paintMachine2nd" HeaderText="(P)2nd Machine"></asp:BoundColumn>
                    <asp:BoundColumn DataField="paintDate2nd" HeaderText="(P)2nd Date"></asp:BoundColumn>
                    <asp:BoundColumn DataField="paintCoat3rd" HeaderText="(P)T/C 3rd Coat"></asp:BoundColumn>
                    <asp:BoundColumn DataField="paintMachine3rd" HeaderText="(P)3rd Machine"></asp:BoundColumn>
                    <asp:BoundColumn DataField="paintDate3rd" HeaderText="(P)3rd Date"></asp:BoundColumn>


                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Black_Mark" HeaderText="(L)Black Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Black_Dot" HeaderText="(L)Black Dot"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Graphic_Shift_check_by_PQC" HeaderText="(L)Graphic Shift check by PQC"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Graphic_Shift_check_by_MC" HeaderText="(L)Graphic Shift check by MC"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Scratch" HeaderText="(L)Scratch"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Jagged" HeaderText="(L)Jagged"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Laser_Bubble" HeaderText="(L)(L) Bubble"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_double_outer_line" HeaderText="(L)double outer line"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Pin_hold" HeaderText="(L)Pin hold"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Poor_Laser" HeaderText="(L)Poor (L)"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Burm_Mark" HeaderText="(L)Burm Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Stain_Mark" HeaderText="(L)Stain Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Graphic_Small" HeaderText="(L)Graphic Small"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Double_Laser" HeaderText="(L)Double (L)"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Color_Yellow" HeaderText="(L)Color Yellow"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Crack" HeaderText="(L)Crack"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Smoke" HeaderText="(L)Smoke"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Wrong_Orientation" HeaderText="(L)Wrong Orientation"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Dented" HeaderText="(L)Dented"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Other" HeaderText="(L)Other"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Buyoff" HeaderText="(L)Buyoff"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Setup" HeaderText="(L)Setup"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_TotalRej" HeaderText="(L)TotalRej"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_TotalRejRate" HeaderText="(L)TotalRej%"></asp:BoundColumn>

                    <asp:BoundColumn DataField="laserMachine" HeaderText="(L)Machine"></asp:BoundColumn>
                    <asp:BoundColumn DataField="laserOP" HeaderText="(L)OP"></asp:BoundColumn>
                    <asp:BoundColumn DataField="laserDate" HeaderText="(L)Date"></asp:BoundColumn>


                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="PQC_Scratch" HeaderText="(O)PQC Scratch"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="Over_Spray" HeaderText="(O)Over Spray"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="Bubble" HeaderText="(O)Bubble"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="Oil_Stain" HeaderText="(O)Oil Stain"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="Drag_Mark" HeaderText="(O)Drag Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="Light_Leakage" HeaderText="(O)Light Leakage"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="Light_Bubble" HeaderText="(O)Light Bubble"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="White_Dot_in_Material" HeaderText="(O)White Dot in Material"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="Other" HeaderText="(O)Other"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="Others_TotalRej" HeaderText="(O)TotalRej"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="Others_TotalRejRate" HeaderText="(O)TotalRej%"></asp:BoundColumn>

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

            
            
            $("#dgButton").FrozenTable(1, 0, 9);

        });

    </script>

</body>
</html>
