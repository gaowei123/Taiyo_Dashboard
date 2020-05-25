<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InventoryReport.aspx.cs" Inherits="DashboardTTS.Webform.InventoryReport" %>


<asp:Content runat="server" ContentPlaceHolderID ="MainContent"  >
    <style >
        .container-fluid{
            max-width: 1400px;
        }
    </style>


    <div class="container-fluid">
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Laser Job Order Report</span>
        </div>

         <div class="row">
            <div class="col-md-12 panel panel-default searchingPanel" >
                <div class="row form-inline searchingBar ">
                    <div class="col-md-3"></div>
                    <div class="col-md-3"></div>
                    <div class="col-md-3"></div>
                    <div class="col-md-3">
                        <label style="width:35%">Part No:</label>
                        <asp:TextBox runat="server" ID="txtPartNo" CssClass="form-control" Width="60%" AutoCompleteType="Disabled" OnTextChanged="txtPartNo_TextChanged" AutoPostBack="true"></asp:TextBox>
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
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775"  />
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="left" VerticalAlign="Middle" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None"  HorizontalAlign="left" VerticalAlign="Middle" />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundColumn DataField="Customer"  HeaderText="Customer"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Module"  HeaderText="Model"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PartNumber"  HeaderText="Part No"></asp:BoundColumn>
                        <asp:ButtonColumn DataTextField="JobNumber"  CommandName="LinkJobDetail"   HeaderText="Job No"></asp:ButtonColumn>
                        <asp:BoundColumn DataField="JobNumber" HeaderText="Job No" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MRPSet_PCS" HeaderText="MRP Qty" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn DataField="BeforeLaser" HeaderText="Before Qty" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn DataField="AfterLaser" HeaderText="After Qty" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:ButtonColumn DataTextField="JobCount" CommandName="LinkJobDetail" HeaderText="Job Count" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:ButtonColumn>
                        <asp:BoundColumn DataField="Hourly" HeaderText="Hourly Qty" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn DataField="SET_PCS_CycleTime" HeaderText="Set(Pcs)" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Visible="true"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MFGDate" HeaderText="MFG Date"></asp:BoundColumn>
                        <asp:BoundColumn DataField="EstProcessTime" HeaderText="Est Time" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="BomExistFlag" HeaderText="Bom List"></asp:BoundColumn>
                  
                    
                        <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" Visible="False">
                            <ItemTemplate>
                                <asp:Button ID="btn_GetRowNo" Runat="server" Text="" Index='<%# ((DataGridItem)Container).ItemIndex %>'/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateColumn>

                        <asp:BoundColumn DataField="ID"  HeaderText="S/N" Visible="false"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>

            </div>
        </div>

  </div>

    <link href="../../plugins/bigautocomplete/jquery.bigautocomplete.css" rel="stylesheet" />
    <script src="../../plugins/bigautocomplete/jquery.bigautocomplete.js"></script>
    <script type="text/javascript">
        jQuery(function($){  $(function(){
            setAutoComplete($('#MainContent_txtPartNo'), 'Laser');
        }); });

    </script>
           
</asp:Content>