<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PQCPackingLiveReport.aspx.cs" Inherits="DashboardTTS.Webform.PQC.PQCPackingLiveReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >

    <div class="container-fluid" style="max-width: 1600px;">
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Packing Live Report</span>    
            <asp:Label runat="server" ID="lbTrackingID" Visible="false"></asp:Label>
        </div>



        <div class="row">
            <div class="col-sm-12 panel panel-default searchingPanel" >

                <div class="row form-inline searchingBar ">
                    <div class="col-sm-3">
                        <label style="width:35%">Date From:</label>
                        <asp:TextBox runat="server" ID="txtDateFrom" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd" Width="60%"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <label style="width:35%">Date To:</label>
                        <asp:TextBox runat="server" ID="txtDateTo" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd" Width="60%"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <label style="width:35%">Shift:</label>
                        <asp:DropDownList runat="server" ID="ddlShift" CssClass="form-control" Width="60%">
                            <asp:ListItem Value="">All</asp:ListItem>
                            <asp:ListItem Value="Day">Day</asp:ListItem>
                            <asp:ListItem Value="Night">Night</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3">
                        <label style="width:35%">Part No:</label>
                        <asp:TextBox runat="server" ID="txtPartNo" CssClass="form-control" Width="60%" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                </div>

                <div class="row form-inline searchingBar ">
                    <div class="col-sm-3">
                        <label style="width:35%">Station:</label>
                        <asp:DropDownList runat="server" ID="ddlStation" CssClass="form-control" Width="60%">
                            <asp:ListItem Value="">All</asp:ListItem>
                            <asp:ListItem Value="1">Online1(Sta1)</asp:ListItem>
                            <asp:ListItem Value="2">Online2(Sta2)</asp:ListItem>
                            <asp:ListItem Value="3">Online3(Sta3)</asp:ListItem>
                            <asp:ListItem Value="4">Online4(Sta4)</asp:ListItem>
                            <asp:ListItem Value="5">Online5(Sta5)</asp:ListItem>
                            <asp:ListItem Value="6">Online6(Sta6)</asp:ListItem>
                            <asp:ListItem Value="7">Online7(Sta7)</asp:ListItem>
                            <asp:ListItem Value="8">Online8(Sta8)</asp:ListItem>

                            <asp:ListItem Value="16">WIP1(Sta16)</asp:ListItem>
                            <asp:ListItem Value="17">WIP2(Sta17)</asp:ListItem>
                            <asp:ListItem Value="14">WIP3(Sta14)</asp:ListItem>
                            <asp:ListItem Value="15">WIP4(Sta15)</asp:ListItem>
                            <asp:ListItem Value="11">WIP5(Sta11)</asp:ListItem>
                            <asp:ListItem Value="13">WIP6(Sta13)</asp:ListItem>

                            <asp:ListItem Value="25">Packing1(Sta25)</asp:ListItem>
                            <asp:ListItem Value="23">Packing2(Sta23)</asp:ListItem>
                            <asp:ListItem Value="22">Packing3(Sta22)</asp:ListItem>
                            <asp:ListItem Value="21">Packing4(Sta21)</asp:ListItem>
                            <asp:ListItem Value="24">Packing5(Sta24)</asp:ListItem>
                            <asp:ListItem Value="12">Packing6(Sta12)</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3">
                        <label style="width:35%">Job No:</label>
                        <asp:TextBox runat="server" ID="txtJobNo" CssClass="form-control" Width="60%"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <label style="width:35%">Lot No:</label>
                        <asp:TextBox runat="server" ID="txtLotNo" CssClass="form-control" Width="60%"></asp:TextBox>
                    </div>
                    
                    <div class="col-sm-3" align="right" style="padding-right:2%;">
                        <asp:Button ID="Button2" runat="server" Text="Generate" OnClick="btnGenerate_Click" CssClass="btn-success" style="width:100px; height:34px; border-radius:4px;" />
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
           <h3> <asp:Label ID="lblResult" runat="server" /> </h3>
        </div>




        <div class="row">
            <div class="col-sm-12 panel panel-default" style="padding-top:10px;padding-bottom:10px;">
                <asp:DataGrid runat="server" ID ="dgPacking" AutoGenerateColumns="false" CssClass="table" Width ="100%" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" >
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="left" VerticalAlign="Middle" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="left" />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundColumn DataField="trackingID" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Shift" HeaderText="Shift"></asp:BoundColumn>
                        <asp:BoundColumn DataField="machineID" HeaderText="Station"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PartNumber" HeaderText="PartNumber"></asp:BoundColumn>
                        <asp:BoundColumn DataField="jobId" HeaderText="JobNumber" Visible="false"></asp:BoundColumn>
                        <asp:ButtonColumn DataTextField="jobId" HeaderText="JobNumber" CommandName="jobMaintain"></asp:ButtonColumn>
                        <asp:BoundColumn DataField="lotNo" HeaderText="Lot No"></asp:BoundColumn>
                        <asp:BoundColumn DataField="processes" HeaderText="Processes"></asp:BoundColumn>
                        <asp:BoundColumn DataField="nextViFlag" HeaderText="IsComplete"></asp:BoundColumn>
                        <asp:BoundColumn DataField="StartTime" HeaderText="Start Time"></asp:BoundColumn>
                        <asp:BoundColumn DataField="StopTime" HeaderText="End Time"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Time" HeaderText="Time"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Status" HeaderText="Status"></asp:BoundColumn>
                        <asp:BoundColumn DataField="acceptQty" HeaderText="OK Qty" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn DataField="rejectQty" HeaderText="NG Qty" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TotalQty" HeaderText="Output" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn DataField="RejRate" HeaderText="REJ%" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn DataField="userID" HeaderText="Operator"></asp:BoundColumn>
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
