<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PaintingDeliveryOperatingHis.aspx.cs" Inherits="DashboardTTS.Webform.Painting.PaintingDeliveryRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >


    <div class="container container-fluid" style="width:75%;">
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Painting Delivery History</span>
        </div>


        <div class="row">
            <div class="col-md-12 panel panel-default searchingPanel" >

                <div class="row form-inline searchingBar ">
                    <div class="col-md-3">
                        <label style="width:35%">Part No:</label>
                        <asp:TextBox runat="server" ID="txtPartNo" CssClass="form-control" Width="60%" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label style="width:35%">Job No:</label>
                        <asp:TextBox runat="server" ID="txtJobNo" CssClass="form-control" Width="60%" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label style="width:35%">Lot No:</label>
                        <asp:TextBox runat="server" ID="txtLotNo" CssClass="form-control" Width="60%" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label style="width:35%">Sending To:</label>
                        <asp:DropDownList runat="server" ID="ddlSendingTo" CssClass="form-control" Width="60%">
                            <asp:ListItem Value="">All</asp:ListItem>
                            <asp:ListItem Value="Laser">Laser</asp:ListItem>
                            <asp:ListItem Value="PQC">PQC</asp:ListItem>
                            <asp:ListItem Value="Print">Print</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    
                    
                </div>

                <div class="row form-inline searchingBar ">
                    <div class="col-md-3">
                        <label style="width:35%">Date From:</label>
                        <asp:TextBox runat="server" ID="txtDateFrom" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd" Width="60%"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label style="width:35%">Date To:</label>
                        <asp:TextBox runat="server" ID="txtDateTo" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd" Width="60%"></asp:TextBox>
                    </div>
                    <div class="col-md-3"></div>
                    <div class="col-md-3" align="right" style="padding-right:2%;">
                        <asp:Button ID="Button1" runat="server" Text="Generate" OnClick="btn_generate_Click" CssClass="btn-success" style="width:100px; height:34px; border-radius:4px;" />&nbsp;&nbsp;
                        <asp:Button ID="Button2" runat="server" Text="Scan Job" OnClick="btn_ScanJob_Click" CssClass="btn-primary" style="width:100px; height:34px; border-radius:4px;" />
                    </div>
                </div>

            </div>
        </div>

        <div class="row">
           <h3> <asp:Label ID="lblResult" runat="server" /> </h3>
        </div>


        <div class="row">
            <div class="col-md-12 panel panel-default" style="padding-top:10px;padding-bottom:10px;">
                <asp:DataGrid runat="server" ID ="dg_PaintingScanJobList" AutoGenerateColumns="false" CssClass="table" Width ="100%" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" >
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="left" VerticalAlign="Middle" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="left" />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" ></asp:BoundColumn>
                        <asp:BoundColumn DataField="partNumber" HeaderText="PartNumber"></asp:BoundColumn>
                        <asp:BoundColumn DataField="jobNumber" HeaderText="JobNumber"></asp:BoundColumn>
                        <asp:BoundColumn DataField="lotNo" HeaderText="Lot No"></asp:BoundColumn>
                        <asp:BoundColumn DataField="inQuantity" HeaderText="MRP Qty(PCS)"></asp:BoundColumn>
                        <asp:BoundColumn DataField="paintRejQty" HeaderText="Paint RejQty"></asp:BoundColumn>
                        <asp:BoundColumn DataField="paintProcess" HeaderText="Process"></asp:BoundColumn>
                        <asp:BoundColumn DataField="sendingTo" HeaderText="Sending To"></asp:BoundColumn>
                        <asp:BoundColumn DataField="remark" HeaderText="Description"></asp:BoundColumn>
                        <asp:BoundColumn DataField="dateTime" HeaderText="MFG Date"></asp:BoundColumn>
                        <asp:BoundColumn DataField="updatedtime" HeaderText="Scan Date"></asp:BoundColumn>
                        <asp:BoundColumn DataField="status" HeaderText="Status" Visible="true"></asp:BoundColumn>
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
