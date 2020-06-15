<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductivityDetail.aspx.cs" Inherits="DashboardTTS.Webform.ProductivityDetail"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >

    <style >
        .container-fluid{
            max-width: 1700px;
        }
    </style>

    <div class="container-fluid">
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Laser Productivity Detail Report</span>
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
                        <label style="width:35%">Model:</label>
                        <asp:TextBox runat="server" ID="txtModel" CssClass="form-control" Width="60%" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                </div>

                <div class="row form-inline searchingBar ">
                    <div class="col-md-3">
                        <label style="width:35%">Part No:</label>
                        <asp:TextBox runat="server" ID="txtPartNo" CssClass="form-control" Width="60%" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label style="width:35%">Machine No:</label>
                        <asp:DropDownList runat="server" ID="ddlMachineNo" CssClass="form-control" Width="60%">
                            <asp:ListItem Value="ALL">ALL</asp:ListItem>
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
                    <div class="col-md-3">
                        <label style="width:35%">Job No:</label>
                        <asp:TextBox runat="server" ID="txtJobNo" CssClass="form-control" Width="60%"></asp:TextBox>
                    </div>
                    <div class="col-md-3" align="right" style="padding-right:2%;">
                        <asp:Button ID="Button1" runat="server" Text="Generate" OnClick="btnGenerate_Click" CssClass="btn-success" style="width:100px; height:34px; border-radius:4px;" />
                    </div>
                </div>

            </div>
        </div>

        <div class="row">
           <h3> <asp:Label ID="lblResult" runat="server" /> </h3>
        </div>


        <div class="row">
            <div class="col-md-12 panel panel-default" style="padding-top:10px;padding-bottom:10px;">
                <asp:DataGrid runat="server" ID ="dgJob" AutoGenerateColumns="false" CssClass="table" Width ="100%" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" >
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="left" VerticalAlign="Middle" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="left" />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Shift" HeaderText="Shift"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MachineID" HeaderText="MachineID"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Customer" HeaderText="Customer" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Module" HeaderText="Model"></asp:BoundColumn>
                        <asp:ButtonColumn  DataTextField="JobNumber" HeaderText="JobNumber" CommandName="LaserJobMaintain"></asp:ButtonColumn>
                        <asp:BoundColumn DataField="PartNumber" HeaderText="Part No" ></asp:BoundColumn>
                        <asp:ButtonColumn  DataTextField="PartNumber" HeaderText="PartNumber" CommandName="UpdateBom" Visible="false"></asp:ButtonColumn>
                        <asp:BoundColumn DataField="CycleTime" HeaderText="Cycle Time"  Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="BlockCount" HeaderText="BlockCount"  Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="UnitCount" HeaderText="Unit Count"  Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="StartTime" HeaderText="Start Time"></asp:BoundColumn>
                        <asp:BoundColumn DataField="EndTime" HeaderText="End Time"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Time" HeaderText="Time"></asp:BoundColumn>
                        <asp:BoundColumn DataField="OK_Set" HeaderText="OK Qty" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:ButtonColumn DataTextField="NG_Set" CommandName="Link" HeaderText="NG Qty" ItemStyle-HorizontalAlign="Center"></asp:ButtonColumn>
                        <asp:BoundColumn DataField="NG_Set" HeaderText="NG" Visible="false" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Output_Set" HeaderText="Output" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn DataField="RejRate_Set" HeaderText="Rej%" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Setup" HeaderText="Setup Qty" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Total_Set" HeaderText="MRP Qty" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="SetTotal" HeaderText="SetTotal" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="SetRejRate" HeaderText="SetRejRate" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DateTime" HeaderText="DateTime" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Performance" HeaderText="Performance"  ItemStyle-HorizontalAlign="Center" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="JobNumber" HeaderText="JobNumber" Visible="false"></asp:BoundColumn>
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