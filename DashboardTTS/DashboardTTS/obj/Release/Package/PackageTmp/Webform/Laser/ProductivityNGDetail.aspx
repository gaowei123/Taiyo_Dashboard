<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductivityNGDetail.aspx.cs" Inherits="DashboardTTS.Webform.ProductivityNGDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >

    <div class="container-fluid" style="max-width: 1500px;" >
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Laser Productivity Material Detail</span>
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
                        <label style="width:35%">Part No:</label>
                        <asp:TextBox runat="server" ID="txtPartNo" CssClass="form-control" Width="60%" AutoCompleteType="Disabled"></asp:TextBox>

                        
                    </div>
                    
                    
                </div>



                <div class="row form-inline searchingBar ">
                    <div class="col-sm-3">
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
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3">
                        <label style="width:35%">Job No:</label>
                        <asp:TextBox runat="server" ID="txtJobNo" CssClass="form-control" Width="60%"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
               
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
            <div class="col-sm-12 panel panel-default">
                <asp:DataGrid runat="server" ID ="dg_NG" AutoGenerateColumns="false" CssClass="table" Width ="100%" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" style="margin-top:10px;">
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />

                    <Columns >
                        <asp:BoundColumn DataField="machineID"      HeaderText="Machine No"     Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="shift"          HeaderText="Shift"          Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="customer"       HeaderText="Customer"       Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="module"         HeaderText="Model"          Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="partNumber"     HeaderText="Part Nmber"     Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="jobNumber"      HeaderText="Job Number"     Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="startTime"      HeaderText="Start Time"     Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="endTime"        HeaderText="End Time"       Visible="false"></asp:BoundColumn> <%--7--%>

                        <asp:BoundColumn DataField="machineID_temp" HeaderText="Machine No"     HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        <asp:BoundColumn DataField="shift_temp"     HeaderText="Shift"          HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        <asp:BoundColumn DataField="customer_temp"  HeaderText="Customer"       HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="module_temp"    HeaderText="Model"          HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        <asp:BoundColumn DataField="partNumber_temp" HeaderText="Part Nmber"     HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        <asp:BoundColumn DataField="jobNumber_temp" HeaderText="Job Number"     HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        <asp:BoundColumn DataField="startTime_temp" HeaderText="Start Time"></asp:BoundColumn>
                        <asp:BoundColumn DataField="endTime_temp"   HeaderText="End Time"></asp:BoundColumn>


                        <asp:BoundColumn DataField="materialPart"   HeaderText="Material Part"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        <asp:BoundColumn DataField="OK"             HeaderText="OK Qty"></asp:BoundColumn>
                        <asp:BoundColumn DataField="NG"             HeaderText="NG Qty"></asp:BoundColumn>
                        <asp:BoundColumn DataField="output" HeaderText="Output"></asp:BoundColumn>
                        <asp:BoundColumn DataField="NGRate" HeaderText="Rej%"></asp:BoundColumn>
                        <asp:BoundColumn DataField="UpdatedBy" HeaderText="Updated By"></asp:BoundColumn>

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
