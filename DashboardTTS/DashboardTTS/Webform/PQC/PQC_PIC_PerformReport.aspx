<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PQC_PIC_PerformReport.aspx.cs" Inherits="DashboardTTS.Webform.PQC.PQC_PIC_PerformReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container" style=" max-width:1500px;">
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">PIC Performance Report</span>
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
                        <label style="width:35%">PIC:</label>
                        <asp:TextBox runat="server" ID="txtPIC" CssClass="form-control" Width="60%"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label style="width:35%">Job No:</label>
                        <asp:TextBox runat="server" ID="txtJobNo" CssClass="form-control" Width="60%"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                       
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
            <div class="col-md-12 panel panel-default" style="padding-top:10px;padding-bottom:10px;">
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
                        <asp:BoundColumn DataField="userID" HeaderText="PIC"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MRPQty" HeaderText="MRP Qty"></asp:BoundColumn>
                        <asp:BoundColumn DataField="LaserBtn" HeaderText="Laser Btn"></asp:BoundColumn>
                        <asp:BoundColumn DataField="WIPBtn" HeaderText="WIP Btn"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Packing" HeaderText="Packing"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MouldingRej" HeaderText="Moulding Rej" ></asp:BoundColumn>
                        <asp:BoundColumn DataField="PaintingRej" HeaderText="Painting Rej"></asp:BoundColumn>
                        <asp:BoundColumn DataField="LaserRej" HeaderText="Laser Rej" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="OthersRej" HeaderText="Others Rej"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TotalRej" HeaderText="Total Rej"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Output" HeaderText="Output"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TotalSeconds" HeaderText="Used Time"></asp:BoundColumn>
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
