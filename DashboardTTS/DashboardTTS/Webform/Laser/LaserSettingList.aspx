<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LaserSettingList.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.Laser.LaserVisionSettingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >

    <div class="container container-fluid">
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Machine Parameters</span>
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
                        <label style="width:35%">Part No:</label>
                        <asp:TextBox runat="server" ID="txtPartNo" CssClass="form-control" Width="60%" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <label style="width:35%">Machine No:</label>
                        <asp:DropDownList runat="server" ID="ddlMachineNo" CssClass="form-control" Width="60%"></asp:DropDownList>
                    </div>
                </div>

                <div class="row form-inline searchingBar ">
                    <div class="col-sm-3"></div>
                    <div class="col-sm-3"></div>
                    <div class="col-sm-3"></div>
                    <div class="col-sm-3" align="right" style="padding-right:2%;">
                        <asp:Button ID="Button1" runat="server" Text="Generate" OnClick="btn_Generate_Click" CssClass="btn-success" style="width:100px; height:34px; border-radius:4px;" />
                    </div>
                </div>

            </div>
        </div>

        <div class="row">
           <h3> <asp:Label ID="lblResult" runat="server" /> </h3>
        </div>


        <div class="row">
            <div class="col-sm-12 panel panel-default" style="padding-top:10px;padding-bottom:10px;">
                <asp:DataGrid runat="server" ID ="dgVisionList" AutoGenerateColumns="false" CssClass="table" Width ="100%" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2">
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="left" VerticalAlign="Middle" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="left" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundColumn DataField="jobNumber" HeaderText="Job No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="partNumber" HeaderText="Part No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="machineID" HeaderText="Machine"></asp:BoundColumn>
                            <asp:BoundColumn DataField="power" HeaderText="Power%"></asp:BoundColumn>
                            <asp:BoundColumn DataField="rate" HeaderText="Rate"></asp:BoundColumn>
                            <asp:BoundColumn DataField="frequency" HeaderText="Frequency"></asp:BoundColumn>
                            <asp:BoundColumn DataField="repeat" HeaderText="Repeat"></asp:BoundColumn>
                            <asp:BoundColumn DataField="dateTime" HeaderText="Date Time"></asp:BoundColumn>
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
        jQuery(function ($) {
            $(function () {
                setAutoComplete($('#MainContent_txtPartNo'), 'Laser');
            });
        });
    </script>


</asp:Content>
