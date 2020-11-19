<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Buy_Off_List.aspx.cs" Inherits="DashboardTTS.Webform.Laser.Buy_Off_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid" style="max-width: 1400px;">
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Laser Buy-Off List</span>
        </div>

        <div class="row">
            <div class="col-sm-12 panel panel-default searchingPanel">
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
                        <label style="width:35%">Machine No:</label>
                        <asp:DropDownList ID="ddlMachineNo"  runat="server" CssClass="form-control" Width="60%" >
                            <asp:ListItem Selected="True" Value="">ALL</asp:ListItem>
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
                        <asp:TextBox runat="server" ID="txtJobNo" CssClass="form-control" Width="60%" placeholder ="1,2,3,4,5.....,30"></asp:TextBox>
                    </div>
                </div>


                <div class="row form-inline searchingBar ">
                    <div class="col-sm-3"></div>
                    <div class="col-sm-3"></div>
                    <div class="col-sm-3"></div>
                    <div class="col-sm-3" align="right" style="padding-right:2%;">
                        <asp:Button ID="Button2" runat="server" Text="Generate" OnClick="btn_Search_Click" CssClass="btn-success" style="width:100px; height:34px; border-radius:4px;" />&nbsp;
                        <asp:Button ID="Button1" runat="server" Text="Add" OnClick="btn_Add_Click" CssClass="btn-primary" style="width:100px; height:34px; border-radius:4px;" />
                    </div>
                </div>

            </div>
        </div>


        
        <div class="row">
           <h3> <asp:Label ID="lblResult" runat="server" /> </h3>
        </div>


        <div class="row">
            <div class="col-sm-12 panel panel-default">
                <asp:DataGrid runat="server" ID="dg_Buyoff_List" AutoGenerateColumns="false" CssClass="table" Width ="100%" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" style="margin-top:10px;">
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775"  />
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundColumn DataField="BUYOFF_ID" HeaderText="Buy-off ID" Visible="false"></asp:BoundColumn>
                        <asp:ButtonColumn DataTextField="JOB_ID" HeaderText="JobNumber" CommandName="ShowBuyoffReport"></asp:ButtonColumn>
                        <asp:BoundColumn DataField="JOB_ID" HeaderText="Job ID" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PART_NO" HeaderText="PartNumber"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MACHINE_ID" HeaderText="Machine ID"></asp:BoundColumn>      
                        <asp:BoundColumn DataField="MC_OPERATOR" HeaderText="MC Operator" Visible="false"></asp:BoundColumn>      
                        <asp:BoundColumn DataField="BUYOFF_BY" HeaderText="Buyoff By" Visible="false"></asp:BoundColumn>      
                        <asp:BoundColumn DataField="APPROVED_BY" HeaderText="Approved By" Visible="false"></asp:BoundColumn>      
                        <asp:BoundColumn DataField="CHECK_BY" HeaderText="Check By" Visible="false"></asp:BoundColumn>           
                        <asp:BoundColumn DataField="DATE_TIME" HeaderText="Date Time"></asp:BoundColumn>
                                 
                        <asp:BoundColumn DataField="BLACK_MARK_1ST" HeaderText="BLACK_MARK"></asp:BoundColumn><%--10--%>
                        <asp:BoundColumn DataField="BLACK_DOT_1ST" HeaderText="BLACK_DOT"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PIN_HOLE_1ST" HeaderText="PIN_HOLE"></asp:BoundColumn>     
                        <asp:BoundColumn DataField="JAGGED_1ST" HeaderText="JAGGED"></asp:BoundColumn>     
                        <asp:BoundColumn DataField="CHECK_GULED_1ST" HeaderText="Check Guide"></asp:BoundColumn>    
                        <asp:BoundColumn DataField="NAVITAS_1ST" HeaderText="Navitas"></asp:BoundColumn>      
                        <asp:BoundColumn DataField="SMART_SCOPE_1ST" HeaderText="Smart Scope"></asp:BoundColumn> <%--16--%>
                                   
                        <asp:BoundColumn DataField="BLACK_MARK_2ND" HeaderText="BLACK_MARK"></asp:BoundColumn>   <%--17--%>  
                        <asp:BoundColumn DataField="BLACK_DOT_2ND" HeaderText="BLACK_DOT"></asp:BoundColumn>     
                        <asp:BoundColumn DataField="PIN_HOLE_2ND" HeaderText="PIN_HOLE" ></asp:BoundColumn>     
                        <asp:BoundColumn DataField="JAGGED_2ND" HeaderText="JAGGED" ></asp:BoundColumn>     
                        <asp:BoundColumn DataField="CHECK_GULED_2ND" HeaderText="SIZE" ></asp:BoundColumn>     
                        <asp:BoundColumn DataField="NAVITAS_2ND" HeaderText="POSITION" ></asp:BoundColumn>     
                        <asp:BoundColumn DataField="SMART_SCOPE_2ND" HeaderText="VISION" ></asp:BoundColumn>   <%--23--%>  

                        <asp:BoundColumn DataField="BLACK_MARK_IN" HeaderText="BLACK_MARK" ></asp:BoundColumn>   <%--24--%>
                        <asp:BoundColumn DataField="BLACK_DOT_IN" HeaderText="BLACK_DOT" ></asp:BoundColumn>     
                        <asp:BoundColumn DataField="PIN_HOLE_IN" HeaderText="PIN_HOLE" ></asp:BoundColumn>     
                        <asp:BoundColumn DataField="JAGGED_IN" HeaderText="JAGGED" ></asp:BoundColumn>     
                        <asp:BoundColumn DataField="CHECK_GULED_IN" HeaderText="SIZE" ></asp:BoundColumn>     
                        <asp:BoundColumn DataField="NAVITAS_IN" HeaderText="POSITION"  ></asp:BoundColumn>     
                        <asp:BoundColumn DataField="SMART_SCOPE_IN" HeaderText="VISION" ></asp:BoundColumn>  <%--30--%>

                        <asp:BoundColumn DataField="CurrentPower" HeaderText="CURRENT" ></asp:BoundColumn>
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
