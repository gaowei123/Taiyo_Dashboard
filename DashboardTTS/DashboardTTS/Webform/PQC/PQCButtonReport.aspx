<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PQCButtonReport.aspx.cs" Inherits="DashboardTTS.Webform.PQC.PQCButtonReport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Taiyo - Button Report</title>
    <link href="../../plugins/bootstrap-3.3.7/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../plugins/bootstrap-datetimepicker-master/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="../../Dashboard CSS JS/CSS/SharedCSS.css" rel="stylesheet" />

    <style>
        #divTableContainer{
            overflow: auto;
        }
        #ddlType,#txtDateFrom{
            width:120px;
        }
        .m5{
            margin:5px;
        }
        .mt_mb{
            margin-top:6px;
            margin-bottom:4px;
        }
        .fr{
            float:right;
        }
        #Button1{
            width:100px; 
            height:34px; 
            border-radius:4px;
        }
        #dgButton{
            width:100%;
            border: 1px solid #000;
        }
        td{
            white-space:nowrap;
        }
   
    </style>
</head>
<body>
    <form id="form1" runat="server">        
        <div class="container-fluid">
            <div class="row titleRow">
                <img class="titleImg" src="../../Resources/Images/headericon.gif" />
                <span class="titleText"><asp:Label runat="server" ID="lbHeader"></asp:Label></span>
            </div>
            <div class="row">
                <div class="col-md-12 panel panel-default mt_mb">
                    <div class="form-inline" role="form">
                        <div class="form-group searchingBarCol m5">
                            <label>PQC Date:</label>
                            <asp:TextBox runat="server" ID="txtDateFrom" CssClass="form-control" data-date-format="yyyy-mm-dd"></asp:TextBox>
                        </div>
                        <div class="form-group searchingBarCol m5">
                            <label>Type:</label>
                            <asp:DropDownList runat="server" ID="ddlType" CssClass="form-control">
                                <asp:listitem Text="All" Value=""></asp:listitem>
                                <asp:listitem Text="WIP" Value="WIP"></asp:listitem>
                                <asp:listitem Text="Laser" Value="Laser"></asp:listitem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group searchingBarCol fr m5">
                            <asp:Button ID="Button1" runat="server" Text="Generate" OnClick="BtnGenerate_Click" CssClass="btn-success" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="divTableContainer">
            <asp:DataGrid runat="server" ID ="dgButton" AutoGenerateColumns="false" CssClass="table">
                <HeaderStyle BorderColor="Black" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="left" VerticalAlign="Middle" Height="96" />
                <ItemStyle BorderColor="Black" BackColor="#F7F6F3" HorizontalAlign="left" VerticalAlign="Middle" Height="35"/>
                <Columns>
                    <asp:BoundColumn DataField="SN" HeaderText="SN"></asp:BoundColumn>
                    <asp:BoundColumn DataField="model" HeaderText="Model" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="jobID" HeaderText="Job No"></asp:BoundColumn>
                    <asp:BoundColumn DataField="lotNo" HeaderText="Lot No" Visible="true"></asp:BoundColumn>
                    <asp:BoundColumn DataField="partNo" HeaderText="Part No"></asp:BoundColumn>
                    <asp:BoundColumn DataField="materialNo" HeaderText="Material No"></asp:BoundColumn>
                    <asp:BoundColumn DataField="lotQty" HeaderText="Lot<br/>Qty"></asp:BoundColumn>
                    <asp:BoundColumn DataField="pass" HeaderText="Pass<br/>Qty"></asp:BoundColumn>
                    <asp:BoundColumn DataField="rejQty" HeaderText="Rej Qty" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="rejRate" HeaderText="Rej%" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="rejRateDisplay" HeaderText="Rej<br/>Qty(%)"></asp:BoundColumn>
                    <asp:BoundColumn DataField="rejCost" HeaderText="Rej<br/>Amount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>


                    <%--12--%>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Raw_Part_Scratch" HeaderText="(TM)<br/>Raw<br/>Part<br/>Scratch"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Oil_Stain" HeaderText="(TM)<br/>Oil<br/>Stain"></asp:BoundColumn>
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
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Mould_TotalRej" HeaderText="(TM)<br/>Total<br/>Rej"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="TTS_Mould_TotalRejRate" HeaderText="(TM)<br/>Total<br/>Rej%"></asp:BoundColumn>

                    <%--47--%>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Raw_Part_Scratch" HeaderText="(VM)<br/>Raw<br/>Part<br/>Scratch"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Oil_Stain" HeaderText="(VM)<br/>Oil<br/>Stain"></asp:BoundColumn>
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
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Mould_TotalRej" HeaderText="(VM)<br/>Total<br/>Rej"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ddebf7" ItemStyle-HorizontalAlign="Center" DataField="Vendor_Mould_TotalRejRate" HeaderText="(VM)<br/>Total<br/>Rej%"></asp:BoundColumn>

                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="MFGDate"  HeaderText="MFG<br/>Date"></asp:BoundColumn>
                            

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
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Particle_for_laser_setup" Visible="false" HeaderText="(P)<br/>Particle for<br/>laser setup"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Buyoff" Visible="false" HeaderText="(P)<br/>Buyoff"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Shortage" HeaderText="(P)<br/>Shortage"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_TotalRej" HeaderText="(P)<br/>Total<br/>Rej"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_TotalRejRate" HeaderText="(P)<br/>Total<br/>Rej%"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_Setup" HeaderText="(P)<br/>SetupRej"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_SetupRejRate" HeaderText="(P)<br/>SetupRej%"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_QA" Visible="false" HeaderText="(P)QA<br/>Reliability<br/>test Qty"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_QATestRejRate" Visible="false" HeaderText="(P)<br/>QA<br/>Qty%"></asp:BoundColumn>

                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="paintCoat1st" HeaderText="(P)<br/>U/C<br/>1st<br/>Coat"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="paintMachine1st" HeaderText="(P)<br/>1st<br/>Machine"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="paintDate1st" HeaderText="(P)<br/>1st<br/>Date"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="paintCoat2nd" HeaderText="(P)<br/>M/C<br/>2nd<br/>Coat"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="paintMachine2nd" HeaderText="(P)<br/>2nd<br/>Machine"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="paintDate2nd" HeaderText="(P)<br/>2nd<br/>Date"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="paintCoat3rd" HeaderText="(P)<br/>T/C<br/>3rd<br/>Coat"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="paintMachine3rd" HeaderText="(P)<br/>3rd<br/>Machine"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="paintDate3rd" HeaderText="(P)<br/>3rd<br/>Date"></asp:BoundColumn>

                    <%--124--%>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Black_Mark" HeaderText="(L)<br/>Black<br/>Mark"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Black_Dot" HeaderText="(L)<br/>Black<br/>Dot"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Graphic_Shift_check_by_PQC" HeaderText="(L)Graphic<br/>Shift check<br/>by PQC"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_Graphic_Shift_check_by_MC" HeaderText="(L)<br/>Vision<br/>NG"></asp:BoundColumn>
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
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_TotalRej" HeaderText="(L)<br/>Total<br/>Rej"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e2efda" ItemStyle-HorizontalAlign="Center" DataField="Laser_TotalRejRate" HeaderText="(L)<br/>Total<br/>Rej%"></asp:BoundColumn>

                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="laserMachine" HeaderText="(L)<br/>Machine"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="laserOP" HeaderText="(L)<br/>OP"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="laserDate" HeaderText="(L)<br/>Date"></asp:BoundColumn>


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
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="Others_TotalRej" HeaderText="(O)<br/>Total<br/>Rej"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6fa" ItemStyle-HorizontalAlign="Center" DataField="Others_TotalRejRate" HeaderText="(O)<br/>Total<br/>Rej%"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_QA" HeaderText="(O)<br/>QA<br/>Qty"></asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-BackColor="#fff2cc" ItemStyle-HorizontalAlign="Center" DataField="Paint_QATestRejRate" HeaderText="(O)<br/>QA<br/>Qty%"></asp:BoundColumn>



                    <asp:BoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="InspBy" HeaderText="Insp<br/>By"></asp:BoundColumn>                            
                </Columns>
            </asp:DataGrid>
        </div>      
    </form>

    <script src="../../plugins/TableFreeze-master/jquery.js"></script>
    <script src="../../plugins/TableFreeze-master/jquery-migrate-1.2.1.js"></script>
    <script src="../../plugins/TableFreeze-master/TableFreeze.js"></script>
    <script src="../../plugins/bootstrap-datetimepicker-master/js/bootstrap-datetimepicker.js"></script>
    <script src="../../Dashboard CSS JS/JS/GlobalConfig.js"></script>
    <script src="../../Dashboard CSS JS/JS/SharedJS.js"></script>
    <script type='text/javascript'>
        $('#txtDateFrom').datetimepicker({
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
            
            var type = getUrlParam('Description');
            if (type === 'BUTTON') {
                document.title = 'Taiyo - Button Report';
            } else {
                document.title = 'Taiyo - Total Report';
            }

            var height = $(window).height() - 120;
            var width = $(window).width() + 10;
            $('#divTableContainer').height(height);
            $('#divTableContainer').width(width);

            $("#dgButton").FrozenTable(1, 0, 8);

            let oTable = document.querySelector('#oDivL_dgButton>table');
            oTable.style.height = '';
        });


        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象  
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数   
            if (r != null) return unescape(r[2]); return null; //返回参数值  
        }
    </script>

</body>
</html>
