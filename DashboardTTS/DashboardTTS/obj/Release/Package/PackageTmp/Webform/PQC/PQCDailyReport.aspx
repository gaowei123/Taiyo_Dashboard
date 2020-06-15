<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PQCDailyReport.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.PQC.PQCDailyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <style >
        .container-fluid{
            max-width: 1800px;
        }
    </style>



    <div class="container-fluid">
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Daily PQC Report</span>
        </div>

        <div class="row">
            <div class="col-md-12 panel panel-default searchingPanel" >

                <div class="row form-inline searchingBar ">
                    <div class="col-md-3">
                        <label style="width:35%">Date From:</label>
                        <asp:TextBox runat="server" ID="txtDateFrom" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd" Width="60%"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label style="width:35%">Date To:</label>
                        <asp:TextBox runat="server" ID="txtDateTo" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd" Width="60%"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label style="width:35%">Shift:</label>
                        <asp:DropDownList runat="server" ID="ddlShift" CssClass="form-control" Width="60%">
                            <asp:ListItem Value="">All</asp:ListItem>
                            <asp:ListItem Value="Day">Day</asp:ListItem>
                            <asp:ListItem Value="Night">Night</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label style="width:35%">Part No:</label>
                        <asp:TextBox runat="server" ID="txtPartNo" CssClass="form-control" Width="60%" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                </div>

                <div class="row form-inline searchingBar ">
                    <div class="col-md-3">
                        <label style="width:35%">Station:</label>
                        <asp:DropDownList runat="server" ID="ddlStation" CssClass="form-control" Width="60%" >
                            <asp:ListItem Text="All" Value=""></asp:ListItem>
                            <asp:ListItem Text="Station 1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Station 2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Station 3" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Station 4" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Station 5" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Station 6" Value="6"></asp:ListItem>
                            <asp:ListItem Text="Station 7" Value="7"></asp:ListItem>
                            <asp:ListItem Text="Station 8" Value="8"></asp:ListItem>
                            <asp:ListItem Text="Station 11" Value="11"></asp:ListItem>
                            <asp:ListItem Text="Station 12" Value="12"></asp:ListItem>
                            <asp:ListItem Text="Station 13" Value="13"></asp:ListItem>
                            <asp:ListItem Text="Station 14" Value="14"></asp:ListItem>
                            <asp:ListItem Text="Station 15" Value="15"></asp:ListItem>
                            <asp:ListItem Text="Station 16" Value="16"></asp:ListItem>
                            <asp:ListItem Text="Station 17" Value="17"></asp:ListItem>
                            <asp:ListItem Text="Station 21" Value="21"></asp:ListItem>
                            <asp:ListItem Text="Station 22" Value="22"></asp:ListItem>
                            <asp:ListItem Text="Station 23" Value="23"></asp:ListItem>
                            <asp:ListItem Text="Station 24" Value="24"></asp:ListItem>
                            <asp:ListItem Text="Station 25" Value="25"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label style="width:35%">Process:</label>
                        <asp:DropDownList runat="server" ID="ddlProcess" CssClass="form-control" Width="60%">
                        <asp:ListItem Text="All" Value=""></asp:ListItem>
                        <asp:ListItem Text="Laser" Value="LASER"></asp:ListItem>
                        <asp:ListItem Text="WIP" Value="WIP"></asp:ListItem>
                        <asp:ListItem Text="PACKING" Value="PACKING"></asp:ListItem>
                    </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label style="width:35%">PIC:</label>
                        <asp:TextBox runat="server" ID="txtPIC" CssClass="form-control" Width="60%"></asp:TextBox>
                    </div>
                    <div class="col-md-3" align="right" style="padding-right:2%;">
                        <asp:Button ID="Button1" runat="server" Text="Generate" OnClick="BtnGenerate_Click" CssClass="btn-success" style="width:100px; height:34px; border-radius:4px;" />
                    </div>
                </div>

            </div>
        </div>

        <div class="row">
           <h3> <asp:Label ID="lblResult" runat="server" /> </h3>
        </div>


        <div class="row">
            <div class="col-md-12 panel panel-default" style="padding:10px 0px 10px 0px">
                <asp:DataGrid runat="server" CssClass="table table-hover" Width="100%" ID="dgPQCDailyReport" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="false">
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="left" VerticalAlign="Middle" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="left" />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundColumn DataField="SN" HeaderText="S/N"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Day" HeaderText="Date"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Shift" HeaderText="Shift"></asp:BoundColumn>
                      
                        <asp:BoundColumn DataField="Station" HeaderText="Station"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PIC" HeaderText="PIC"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PartNumber" HeaderText="Part No" ></asp:BoundColumn>
                        <asp:BoundColumn DataField="JobNumber" HeaderText="Jobnumber"></asp:BoundColumn>
                        <asp:BoundColumn DataField="lotNo" HeaderText="Lot No" Visible="false"></asp:BoundColumn>
                        <asp:ButtonColumn  DataTextField="lotNo" HeaderText="Lot No" CommandName="LinkDetail" ></asp:ButtonColumn>
                        <asp:BoundColumn DataField="MrpQty" HeaderText="MRP Qty"></asp:BoundColumn>
                        <asp:BoundColumn DataField="LaserBtn" HeaderText="Laser Btn" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn DataField="WIPBtn" HeaderText="WIP Btn" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Packing" HeaderText="Packing" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>

                        <asp:ButtonColumn DataTextField="MouldingRej" HeaderText="Moulding Rej" CommandName="LinkMouldingDetail" ItemStyle-HorizontalAlign="Center"></asp:ButtonColumn>
                        <asp:ButtonColumn DataTextField="PaintingRej" HeaderText="Painting Rej" CommandName="LinkPaintingDetail" ItemStyle-HorizontalAlign="Center"></asp:ButtonColumn>
                        <asp:ButtonColumn DataTextField="LaserRej" HeaderText="Laser Rej" CommandName="LinkLaserDetail" ItemStyle-HorizontalAlign="Center"></asp:ButtonColumn>
                        <asp:ButtonColumn DataTextField="OthersRej" HeaderText="Others Rej" CommandName="LinkOthersDetail" ItemStyle-HorizontalAlign="Center"></asp:ButtonColumn>
                  
                        <asp:BoundColumn DataField="rejectQty" HeaderText="Total REJ" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TotalQty" HeaderText="Output"></asp:BoundColumn>
                        <asp:BoundColumn DataField="StartTime" HeaderText="Start Time"></asp:BoundColumn>
                        <asp:BoundColumn DataField="StopTime" HeaderText="End Time"></asp:BoundColumn>
                        <asp:BoundColumn DataField="totalSecond" HeaderText="Used Time"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
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

        jQuery(function($){  $(function(){
            setAutoComplete($('#MainContent_txtPartNo'), 'Laser');
        }); });

    </script>




   
</asp:Content>
