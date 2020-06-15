<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="LaserSparePartsUsageHis.aspx.cs" Inherits="DashboardTTS.Webform.Laser.LaserSparePartsUsageHis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >


    <div class="container">
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Laser Spare Parts Usage History</span>
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
                        <label style="width:35%">Part Name:</label>
                        <asp:DropDownList runat="server" ID="ddlPartName" CssClass="form-control" Width="60%"></asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label style="width:35%">Action:</label>
                        <asp:DropDownList runat="server" ID="ddlAction" CssClass="form-control" Width="60%">
                           <asp:ListItem Text="All" Value=""></asp:ListItem>
                           <asp:ListItem Text="IN" Value="IN"></asp:ListItem>
                           <asp:ListItem Text="OUT" Value="OUT"></asp:ListItem>
                           <asp:ListItem Text="Delete" Value="Delete"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row form-inline searchingBar ">
                    <div class="col-md-3"></div>
                    <div class="col-md-3"></div>
                    <div class="col-md-3"></div>
                    <div class="col-md-3" align="right" style="padding-right:2%;">
                        <asp:Button ID="Button1" runat="server" Text="Generate" OnClick="btn_Generate_Click" CssClass="btn-success" style="width:100px; height:34px; border-radius:4px;" />
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
           <h3> <asp:Label ID="lblResult" runat="server" /> </h3>
        </div>

         <div class="row">
            <div class="col-md-12 panel panel-default" style="padding-top:10px;padding-bottom:10px;">
                <asp:DataGrid runat="server" ID ="dg_CheckList" AutoGenerateColumns="false" CssClass="table" Width ="100%" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2">
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="center" VerticalAlign="Middle" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"/>
                    <Columns>
                        <asp:BoundColumn DataField="lastUpdatedTime" HeaderText="DateTime"></asp:BoundColumn>
                        <asp:BoundColumn DataField="sparePartsName" HeaderText="Spare Parts"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MachineID" HeaderText="Machine"></asp:BoundColumn>
                        <asp:BoundColumn DataField="action" HeaderText="Action"></asp:BoundColumn>
                        <asp:BoundColumn DataField="usage" HeaderText="quantity"></asp:BoundColumn>
                        <asp:BoundColumn DataField="balance" HeaderText="Balance"></asp:BoundColumn>
                        <asp:BoundColumn DataField="lastUpdatedBy" HeaderText="Action By"></asp:BoundColumn>
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