<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MachineCapability.aspx.cs" Inherits="DashboardTTS.Webform.MachineCapability" StyleSheetTheme="" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">





     <div class="container container-fluid" >
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Laser Capacity calculator</span>
        </div>


        <div class="row">
            <div class="col-md-12 panel panel-default searchingPanel" >
                <div class="row form-inline searchingBar ">
                    <div class="col-md-3">
                        <label style="width:60%">Required MC Qty:</label>
                        <asp:DropDownList runat="server" ID="ddlMachineNo" CssClass="form-control" Width="35%">
                            <asp:ListItem Value="1">1</asp:ListItem>
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="3">3</asp:ListItem>
                            <asp:ListItem Value="4">4</asp:ListItem>
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="6">6</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                            <asp:ListItem Selected="True" Value="8">8</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6"></div>
                    <div class="col-md-3" align="right">
                        <asp:Button ID="Button1" runat="server" Text="Generate" OnClick="btnGenerate_Click" CssClass="btn-success" style="width:100px; height:34px; border-radius:4px;" /> &nbsp;&nbsp;
                        <asp:Button ID="btn_Edit" runat="server" Text="Edit" OnClick="btn_Edit_Click" CssClass="btn-primary" style="width:50px; height:34px; border-radius:4px;" />
                    </div>
                </div>
            </div>
        </div>

         <div class="row">
            <div class="col-md-12 panel panel-default searchingPanel" >
                <div class="row form-inline searchingBar ">
                    <div class="col-md-1-5" align="center">
                        <label>Month/Year</label>
                    </div>
                    <div class="col-md-1-5" align="center">
                        <label>Working Days</label>
                    </div>
                    <div class="col-md-1-5" align="center">
                        <label>Capacity% (08:00 - 17:45)</label>
                    </div>
                    <div class="col-md-1-5" align="center">
                        <label>Capacity% (08:00 - 20:00)</label>
                    </div>
                    <div class="col-md-1-5" align="center">
                        <label>Capacity% (08:00 - 08:00)</label>
                    </div>
                </div>

                <div class="row form-inline searchingBar ">
                    <div class="col-md-1-5" align="center">
                        <asp:DropDownList runat="server" ID="ddlMonth" CssClass="form-control" Width="35%">
                            <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                            <asp:ListItem Text="May" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                            <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                            <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                            <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                            <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                            <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                            <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                        </asp:DropDownList> &nbsp;&nbsp;
                        <asp:DropDownList runat="server" ID="ddlYear" CssClass="form-control" Width="40%"></asp:DropDownList>
                    </div>
                    <div class="col-md-1-5" align="center">
                        <asp:TextBox runat="server" ID="txtWorkingDays" CssClass="form-control" Width="40%" AutoCompleteType="Disabled"></asp:TextBox>  
                    </div>
                    <div class="col-md-1-5" align="center">
                        <asp:Label runat="server" ID="lbCapacity8H" CssClass="form-control" Width="85%" style="text-align:center" Enabled="false"></asp:Label>  
                    </div>
                    <div class="col-md-1-5" align="center">
                        <asp:Label runat="server" ID="lbCapacity12H" CssClass="form-control" Width="85%" style="text-align:center" Enabled="false"></asp:Label>  
                    </div>
                    <div class="col-md-1-5" align="center">
                        <asp:Label runat="server" ID="lbCapacity24H" CssClass="form-control" Width="85%" style="text-align:center" Enabled="false"></asp:Label> 
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 panel panel-default" style="padding-top:10px;padding-bottom:10px;">
                <asp:DataGrid runat="server" ID ="dg_partList" AutoGenerateColumns="false" CssClass="table" Width ="100%" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2">
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775"  />
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="center" />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundColumn DataField="module" HeaderText="Model" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        <asp:BoundColumn DataField="partNumber" HeaderText="Part No"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        <asp:BoundColumn DataField="materialCount" HeaderText="" Visible ="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="outputPerHour" HeaderText="Hourly Output"></asp:BoundColumn>
                        <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" HeaderText="Monthly Order Qty"  Visible="true">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtOrderQty" style="text-align:center" Width="80%" AutoCompleteType="Disabled"  ></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" HeaderText="Process Hour">
                            <ItemTemplate>
                                <asp:label runat="server" ID="lbSpendHours" style="text-align:center" ></asp:label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="cycleTimePerPCS" HeaderText="Cycle Time" Visible="false"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
        </div>
    </div>


 </asp:Content>
