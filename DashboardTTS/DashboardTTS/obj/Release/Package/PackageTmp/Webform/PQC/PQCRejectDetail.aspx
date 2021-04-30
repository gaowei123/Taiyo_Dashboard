<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PQCRejectDetail.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.PQC.PQCRejectDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >   

<style>
    label{
        width:35%
    }
</style>


<div class="container-fluid" style="max-width:1500px;">
    <div class="row titleRow">
        <img class="titleImg" src="../../Resources/Images/headericon.gif" />
        <span class="titleText">Rejection Detail Report</span>
    </div>


    <div class="row">
        <div class="col-sm-12 panel panel-default searchingPanel" >
            <div class="row form-inline searchingBar ">
                <div class="col-sm-3">
                    <label>Date From:</label>
                    <asp:TextBox runat="server" ID="txtDateFrom" CssClass="form-control" data-date-format="yyyy-mm-dd" Width="60%"></asp:TextBox>
                </div>
                <div class="col-sm-3">
                    <label style="width:35%">Date To:</label>
                    <asp:TextBox runat="server" ID="txtDateTo" CssClass="form-control" data-date-format="yyyy-mm-dd" Width="60%"></asp:TextBox>
                </div>
                <div class="col-sm-3">
                    <label>Part No:</label>
                    <asp:TextBox runat="server" ID="txtPartNo" CssClass="form-control"  Width="60%"></asp:TextBox>
                </div>
                <div class="col-sm-3">
                    <label>Station:</label>
                    <asp:DropDownList runat="server"  ID="ddlStation" CssClass="form-control" Width="60%">
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
            </div>

            <div class="row form-inline searchingBar ">
                <div class="col-sm-3">
                    <label>Defect Type:</label>
                    <asp:DropDownList ID="ddlRejType"  runat="server" CssClass="form-control" Width="60%" OnSelectedIndexChanged="ddlRejType_SelectedIndexChanged" AutoPostBack="true" >
                        <asp:ListItem Value="">ALL</asp:ListItem>
                        <asp:ListItem Value="Paint">Paint</asp:ListItem>
                        <asp:ListItem Value="Mould">Mould</asp:ListItem>
                        <asp:ListItem Value="Paint">Paint</asp:ListItem>
                        <asp:ListItem Value="Others">Others</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-sm-3">
                    <label style="width:35%">Defect Code:</label>
                    <asp:DropDownList runat="server" ID="ddlDefectCode" CssClass="form-control" Width="60%"></asp:DropDownList>    
                </div>
                <div class="col-sm-3">
                    <label style="width:35%">Job No:</label>
                    <asp:TextBox runat="server" ID="txtJobNo" CssClass="form-control" Width="60%"></asp:TextBox>
                </div>
                <div class="col-sm-3" style="text-align:right; padding-right:2%;">                                        
                    <asp:Button ID="btn_generate" runat="server" Text="Generate" OnClick="btn_generate_Click" CssClass="btn-success" style="width:100px; height:34px; border-radius:4px;" />
                </div>
            </div>

        </div>
    </div>

    <div class="row">
        <h3> <asp:Label runat="server" ID="lbResult"></asp:Label></h3>
    </div>

    <div class="row">
        <div class="col-sm-12 panel panel-default" style="padding-top:10px;padding-bottom:10px;">
            <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dg_RejDetail" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="false" >
                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                <EditItemStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" />
                <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                <HeaderStyle HorizontalAlign="Left" />
                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <Columns>                            
                    <asp:BoundColumn DataField="Day" HeaderText="Day"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Shift" HeaderText="Shift"></asp:BoundColumn>
                    <asp:BoundColumn DataField="touchPC" HeaderText="Station"></asp:BoundColumn>
                    <asp:BoundColumn DataField="model" HeaderText="Model"></asp:BoundColumn>
                    <asp:BoundColumn DataField="partNumber" HeaderText="Part No"></asp:BoundColumn>
                    <asp:BoundColumn DataField="JobNumber" HeaderText="Job No"></asp:BoundColumn>
                    <asp:BoundColumn DataField="materialNo" HeaderText="Material No"></asp:BoundColumn>
                    <asp:BoundColumn DataField="RejType" HeaderText="Dept"></asp:BoundColumn>
                    <asp:BoundColumn DataField="RejCode" HeaderText="Rej Code"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TotalQty" HeaderText="Total Qty"></asp:BoundColumn>
                    <asp:BoundColumn DataField="RejQty" HeaderText="Rej Qty"></asp:BoundColumn>
                    <asp:BoundColumn DataField="RejRate" HeaderText="REJ%"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Operator" HeaderText="PIC"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DateTime" HeaderText="Date Time"></asp:BoundColumn>
                </Columns>
            </asp:DataGrid>
        </div>
    </div>
</div>

    <link href="../../plugins/bigautocomplete/jquery.bigautocomplete.css" rel="stylesheet" />
    <script src="../../plugins/bigautocomplete/jquery.bigautocomplete.js"></script>
<script>
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
            setAutoComplete($('#MainContent_txtPartNo'), 'PQC');
        });
    });
</script>


</asp:Content>