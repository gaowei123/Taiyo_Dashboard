<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MouldingMachineStatus.aspx.cs"  MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.Moulding.MouldingMachineStatus" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
   
    <style>
        .container-fluid{
            max-width: 1300px;
        }
    </style>


    <div class="container-fluid">
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Machine Status</span>
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
                        <label style="width:35%">Machine No:</label>
                        <asp:DropDownList runat="server" ID="ddlMachineNo" CssClass="form-control" Width="60%">
                            <asp:ListItem Value="">ALL</asp:ListItem>
                            <asp:ListItem Value="1">No.1</asp:ListItem>
                            <asp:ListItem Value="2">No.2</asp:ListItem>
                            <asp:ListItem Value="3">No.3</asp:ListItem>
                            <asp:ListItem Value="4">No.4</asp:ListItem>
                            <asp:ListItem Value="5">No.5</asp:ListItem>
                            <asp:ListItem Value="6">No.6</asp:ListItem>
                            <asp:ListItem Value="7">No.7</asp:ListItem>
                            <asp:ListItem Value="8">No.8</asp:ListItem>
                            <asp:ListItem Value="9">No.9</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    
                </div>

                <div class="row form-inline searchingBar ">
                    <div class="col-md-3">
                        <label style="width:35%">Status:</label>
                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control" Width="60%">
                            <asp:ListItem Value="">ALL</asp:ListItem>
                            <asp:ListItem Value="Break Down">Break Down</asp:ListItem>
                            <asp:ListItem Value="Mould Testing">Mould Testing</asp:ListItem>
                            <asp:ListItem Value="Material Testing">Material Testing</asp:ListItem>
                            <asp:ListItem Value="Adjustment">Adjustment</asp:ListItem>
                            <asp:ListItem Value="Change Mould">Change Mould</asp:ListItem>
                            <asp:ListItem Value="No Material">Mc Stop</asp:ListItem>
                            <asp:ListItem Value="Mould Damage">Mould Damage</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3"></div>
                    <div class="col-md-3"></div>
                    <div class="col-md-3" style="padding-right:2%;">
                        <asp:Button ID="Button1" runat="server" Text="Generate" OnClick="btn_Generate_Click" CssClass="btn-success" style="width:100px; height:34px; border-radius:4px; float:right;" />
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
           <h3> <asp:Label ID="lblResult" runat="server"></asp:Label> </h3>
        </div>


        <div class="row">
            <div class="col-md-12 panel panel-default" style="padding-top:10px;padding-bottom:10px;">
                <asp:DataGrid runat="server" ID ="dg_MachineSummary" CssClass="table table-hover" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" Width="100%"  HorizontalAlign="Center"  AutoGenerateColumns="False" >
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775"  />
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundColumn DataField="MachineID" HeaderText="Mc"></asp:BoundColumn>
                        <asp:BoundColumn DataField="sDay" HeaderText="Date"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Shift" HeaderText="Shift"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MachineStatus" HeaderText="Machine Status"></asp:BoundColumn>
                        
                        <asp:BoundColumn DataField="StartTime" HeaderText="Start Time"></asp:BoundColumn>
                        <asp:BoundColumn DataField="EndTime" HeaderText="End Time"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Duration" HeaderText="Duration"></asp:BoundColumn>

                        <asp:BoundColumn DataField="Remark" HeaderText="Remark"></asp:BoundColumn>

                        <asp:BoundColumn DataField="PartNo" HeaderText="Part No"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TotalQty" HeaderText="Total Qty"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PassQty" HeaderText="Pass Qty"></asp:BoundColumn>
                        <asp:BoundColumn DataField="RejQty" HeaderText="Reject Qty"></asp:BoundColumn>
                        <asp:BoundColumn DataField="OPID" HeaderText="OP ID"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
        </div>
    </div>

    
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

</script>

</asp:Content>
