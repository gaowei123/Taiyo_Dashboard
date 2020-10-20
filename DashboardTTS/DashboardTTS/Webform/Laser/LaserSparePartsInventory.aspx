<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="LaserSparePartsInventory.aspx.cs" Inherits="DashboardTTS.Webform.Laser.LaserSparePartsInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >

    <div class="container container-fluid"  >
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Spare Parts Record</span>
        </div>

        <div class="row">
            <div class="col-md-12 panel panel-default searchingPanel" >
                <div class="row form-inline searchingBar ">
                    <div class="col-sm-3">
                        <%--<label style="width:35%">Part Name:</label>
                        <asp:TextBox runat="server" ID="txtPartName" CssClass="form-control" Width="60%" AutoCompleteType="Disabled"></asp:TextBox>--%>
                    </div>
                     <div class="col-sm-3" align="right" style="padding-right:2%;">
                        <%--<asp:Button ID="btn_Generate" runat="server" Text="Generate" OnClick="btn_Generate_Click" CssClass="btn-success" style="width:100px; height:34px; border-radius:4px;"/>--%>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label runat="server" ID="lbSparePart" ></asp:Label> &nbsp;&nbsp;
                        <asp:Label runat="server" ID="lbCurQty_Text" Text="Current Qty:" ></asp:Label>
                        <asp:Label runat="server" ID="lbCurQty"  ></asp:Label>&nbsp;&nbsp;
                        <asp:TextBox runat="server" ID="txtQty" CssClass="form-control" Width="20%"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnLoad" runat="server" Text="IN" OnClick="btnLoad_Click" CssClass="btn-primary" style="width:50px; height:34px; border-radius:4px;"/> &nbsp;
                        <asp:Button ID="btnUnload" runat="server" Text="OUT" OnClick="btnUnload_Click"  CssClass="btn-primary" style="width:50px; height:34px; border-radius:4px;" />
                    </div>
                   
                </div>
            </div>
        </div>

        



        <div class="row">
            <div class="col-sm-12 panel panel-default" style="padding-top:10px;padding-bottom:10px;">
                 <h3> <asp:Label ID="lblResult" runat="server" /> </h3>
                <asp:DataGrid runat="server" ID ="dgSparePartsInventory" AutoGenerateColumns="false" CssClass="table" Width ="100%" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2">
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="center" VerticalAlign="Middle" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"/>
                        <Columns>
                            <asp:BoundColumn DataField="sparePartsName" HeaderText="Parts Name" Visible="false"></asp:BoundColumn>
                            <asp:ButtonColumn DataTextField="sparePartsName" HeaderText="Parts Name" CommandName="ShowAction" HeaderStyle-Width="35%"></asp:ButtonColumn>
                            <asp:BoundColumn DataField="quantity" HeaderText="Balance" HeaderStyle-Width="10%"></asp:BoundColumn >
                            <asp:BoundColumn DataField="lastUpdatedTime" HeaderText="Last Updated Time" HeaderStyle-Width="30%"></asp:BoundColumn>
                            <asp:BoundColumn DataField="lastUpdatedBy" HeaderText="Last Updated By" HeaderStyle-Width="20%"></asp:BoundColumn>

                            <asp:TemplateColumn HeaderText="" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnDelete" Width="70%" Height="23px" Text="×"  CssClass="btn-danger" CommandName="Delete"/>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>

                    <asp:DataGrid runat="server" ID ="dgAdd" AutoGenerateColumns="false" CssClass="table" Width ="100%" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" style="margin-top:10px;">
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="center" VerticalAlign="Middle" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"/>
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="false"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Parts Name"  ItemStyle-Width="35%">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtPartsName" Width="80%" AutoCompleteType="None"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Current Qty" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtCurQty" Width="90%" AutoCompleteType="None"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                           
                            <asp:TemplateColumn HeaderText="" ItemStyle-Width="30%">
                                 <ItemTemplate>
                                   
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="" HeaderStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnAdd" Width="50%" Height="23px" Text="Add" CssClass="btn-success" CommandName="Add"/>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>

            </div>
        </div>

    </div>

</asp:Content>