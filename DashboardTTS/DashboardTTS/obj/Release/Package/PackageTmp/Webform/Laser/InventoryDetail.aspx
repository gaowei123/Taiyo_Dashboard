<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InventoryDetail.aspx.cs" Inherits="DashboardTTS.Webform.InventoryDetail" %>


<asp:Content runat="server" ContentPlaceHolderID ="MainContent"  >
  
    <style >
        .container-fluid{
            max-width: 1500px;
        }
    </style>


    <div class="container-fluid">
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Laser Job Order Detail Report</span>
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
                        <label style="width:35%">Part No:</label>
                        <asp:TextBox runat="server" ID="txtPartNo" CssClass="form-control" Width="60%" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>

                    <div class="col-md-3">
                        <label style="width:35%">Job No:</label>
                        <asp:TextBox runat="server" ID="txtJobNo" CssClass="form-control" Width="60%"></asp:TextBox>
                    </div>
                </div>

                <div class="row form-inline searchingBar ">
                    <div class="col-md-3">
                        <label style="width:35%">Part Status:</label>
                        <asp:DropDownList ID="ddlStatus"  runat="server" CssClass="form-control" Width="60%">
                            <asp:ListItem Value="ALL">ALL</asp:ListItem>
                            <asp:ListItem Value="Pending">Pending</asp:ListItem>
                            <asp:ListItem Value="Inprocess">Inprocess</asp:ListItem>
                            <asp:ListItem Value="Complete">Complete</asp:ListItem>
                            <asp:ListItem Value="NoComplete" Selected="True">NoComplete</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3"></div>
                    <div class="col-md-3"></div>
                    <div class="col-md-3" align="right" style="padding-right:2%;">
                        <asp:Button ID="Button1" runat="server" Text="Generate" OnClick="btn_generate_Click" CssClass="btn-success" style="width:100px; height:34px; border-radius:4px;" />
                    </div>
                </div>
            </div>
        </div>



        <div class="row">
           <h3> <asp:Label ID="lblResult" runat="server" /> </h3>
        </div>

        <div class="row">
            <div class="col-md-12 panel panel-default">
                <asp:DataGrid runat="server" ID ="dg_inventoryDetail" AutoGenerateColumns="false" CssClass="table" Width ="100%" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" style="margin-top:10px;">
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <Columns >
                        <asp:BoundColumn DataField="id"  HeaderText="S/N"></asp:BoundColumn>
                        <asp:BoundColumn DataField="jobNumber"  HeaderText="Job No" ></asp:BoundColumn>
                        <asp:BoundColumn DataField="partNumber"  HeaderText="Part No"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MRPSet_PCS" HeaderText="MRP Qty" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn DataField="BeforeLaser" HeaderText="Before Qty" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn DataField="AfterLaser" HeaderText="After Qty" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn DataField="pqcQuantity" HeaderText="Shortage" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ></asp:BoundColumn>
                        <asp:BoundColumn DataField="setUpQty" HeaderText="SetUp Qty" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ></asp:BoundColumn>
                        <asp:BoundColumn DataField="buyOffQty" HeaderText="BuyOff Qty" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ></asp:BoundColumn>
                        <asp:BoundColumn DataField="startOnTime" HeaderText="MFG Time"></asp:BoundColumn>
                        <asp:BoundColumn DataField="status" HeaderText="Status"></asp:BoundColumn>
                        <asp:BoundColumn DataField="estProcessTime" HeaderText="Est Time"></asp:BoundColumn>
                        <asp:BoundColumn DataField="bomExistFlag" HeaderText="Bom Exist"></asp:BoundColumn>
                        <asp:BoundColumn DataField="dateTime" HeaderText="Date Time"></asp:BoundColumn>
                                       
                        <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" HeaderText="">
                            <ItemStyle  VerticalAlign="Middle"></ItemStyle>
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" Runat="server" Text="×" Height="20px" CssClass="btn-danger" CommandName="delete" Index='<%# ((DataGridItem)Container).ItemIndex %>'/>
                            </ItemTemplate>
                        </asp:TemplateColumn>
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
