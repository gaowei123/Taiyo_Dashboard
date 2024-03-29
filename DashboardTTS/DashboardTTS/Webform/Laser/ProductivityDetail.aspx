﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductivityDetail.aspx.cs" Inherits="DashboardTTS.Webform.ProductivityDetail"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >

    <div class="container-fluid" style="max-width:1700px;">
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Laser Productivity Detail Report</span>
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
                        <label style="width:35%">Model:</label>
                        <asp:TextBox runat="server" ID="txtModel" CssClass="form-control" Width="60%" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                </div>

                <div class="row form-inline searchingBar ">
                    <div class="col-sm-3">
                        <label style="width:35%">Part No:</label>
                        <asp:TextBox runat="server" ID="txtPartNo" CssClass="form-control" Width="60%" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <label style="width:35%">Machine No:</label>
                        <asp:DropDownList runat="server" ID="ddlMachineNo" CssClass="form-control" Width="60%">
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
                    <div class="col-sm-3">
                        <label style="width:35%">Job No:</label>
                        <asp:TextBox runat="server" ID="txtJobNo" CssClass="form-control" Width="60%"></asp:TextBox>
                    </div>
                    <div class="col-sm-3" align="right" style="padding-right:2%;">
                        <asp:Button ID="Button1" runat="server" Text="Generate" OnClick="btnGenerate_Click" CssClass="btn-success" style="width:100px; height:34px; border-radius:4px;" />
                    </div>
                </div>

            </div>
        </div>

        <div class="row">
           <h3> <asp:Label ID="lblResult" runat="server" /> </h3>
        </div>


        <div class="row">
            <div class="col-sm-12 panel panel-default" style="padding-top:10px;padding-bottom:10px;">
                <asp:DataGrid runat="server" ID ="dgJob" AutoGenerateColumns="false" CssClass="table" Width ="100%" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" >
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="left" VerticalAlign="Middle" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="left" />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundColumn DataField="jobNo" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="shift" HeaderText="Shift"></asp:BoundColumn>
                        <asp:BoundColumn DataField="machineID" HeaderText="MachineID"></asp:BoundColumn>
                        <asp:BoundColumn DataField="model" HeaderText="Model"></asp:BoundColumn>
                        <asp:ButtonColumn  DataTextField="jobNo" HeaderText="JobNumber" CommandName="LaserJobMaintain"></asp:ButtonColumn>
                        <asp:BoundColumn DataField="partNo" HeaderText="Part No" ></asp:BoundColumn>
                        <asp:BoundColumn DataField="startTime" HeaderText="Start Time"></asp:BoundColumn>
                        <asp:BoundColumn DataField="endTime" HeaderText="End Time"></asp:BoundColumn>
                        <asp:BoundColumn DataField="time" HeaderText="Time"></asp:BoundColumn>
                        <asp:BoundColumn DataField="displayOK" HeaderText="OK Qty" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:ButtonColumn DataTextField="displayNG" CommandName="Link" HeaderText="NG Qty" ItemStyle-HorizontalAlign="Center"></asp:ButtonColumn>
                        <asp:BoundColumn DataField="displayNG" HeaderText="NG" Visible="false" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn DataField="displayOutput" HeaderText="Output" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn DataField="displayRejRate" HeaderText="Rej%" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn DataField="displaySetup" HeaderText="Setup Qty" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn DataField="displayMRP" HeaderText="MRP Qty" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>                         
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