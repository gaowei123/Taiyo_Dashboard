<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MouldingMonthlyMaterial.aspx.cs" Inherits="DashboardTTS.Webform.Moulding.MouldingMonthlyMaterial" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" >

    <div style="width: auto; height: 257px; align-items:center;margin:auto">
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Moulding Material Monthly Report" Font-Size="12" ForeColor="White"></Asp:label>
                </td>
            </tr>

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="Year" Width="100%"></asp:Label>
                </td>
                <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px;">
                    <asp:DropDownList runat="server" ID="ddlYear" Width="100%" Height="23px"></asp:DropDownList>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                  
                </td>       
                <td style = "padding: 10px 35px 10px 10px; border: 1px solid #CCCCCC; width: 25%;   height: 50px;">
                    
                </td>
                <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MeS'; height: 50px;" align="center" >
                    <asp:Button runat="server" ID="btnGenerate" Text="Generate"  Width="45%" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Height="30px" OnClick="btnGenerate_Click" />
                </td>
            </tr>



            <tr>
                <td style = "padding: 10px 25px 25px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="3" valign="top">
                    <h4 style="width:100%;text-align:center">Material Monthly Usage</h4> 
                    <asp:DataGrid runat="server" Width ="100%" CssClass="table table-hover" ID ="dgMaterialMonthly" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="false" >
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center"  VerticalAlign="Middle" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <Columns>
                            <asp:ButtonColumn DataTextField="Month" HeaderText="Month" CommandName="LinkDetail"></asp:ButtonColumn>
                            <asp:BoundColumn DataField="LoadKgs" HeaderText="Load(Kgs)"></asp:BoundColumn> 
                            <asp:BoundColumn DataField="UnLoadKgs" HeaderText="Unload(Kgs)"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ReturnKgs" HeaderText="Return(Kgs)"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UsageKgs" HeaderText="Usage(Kgs)"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UsageCost" HeaderText="Usage Cost(SGD)"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
                <td style = "padding: 10px 25px 25px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" valign="top">
                    <h4 style="width:100%;text-align:center">(<asp:Label runat="server" ID="lbMonth"></asp:Label>)  &nbsp;&nbsp;&nbsp;   Materials Purchased And Used Detail      </h4>
                    <asp:DataGrid runat="server" Width ="100%" CssClass="table table-hover" ID ="dgMaterialDetail" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="false" style="line-height:14px">
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center"  VerticalAlign="Middle" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundColumn DataField="Material_No" HeaderText="Material No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LoadKgs" HeaderText="Material Purchased(KGS)"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LoadCost" HeaderText="Material Cost(SGD)"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UsedKgs" HeaderText="Material Used(KGS)"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UsedSGD" HeaderText="Material Used Cost(SGD)"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>