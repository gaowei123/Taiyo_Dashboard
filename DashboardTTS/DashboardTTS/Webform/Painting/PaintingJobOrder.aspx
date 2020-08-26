<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaintingJobOrder.aspx.cs" Inherits="DashboardTTS.Webform.Painting.PaintingJobOrder" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>


<asp:Content runat="server" ContentPlaceHolderID ="MainContent"  >

<div style="width: 1550px; align-items:center;margin:auto">
  
    <div>
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Painting Job Order Report" Font-Size="12" ForeColor="White"/>
                </td>
            </tr>
            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Part No :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:TextBox runat="server" ID="txt_partNo" Width="93%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled" ></asp:TextBox>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Customer :" Width="100%"></asp:Label>   
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_Customer" Width="93%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled" ></asp:TextBox>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Button runat="server" ID="btn_scan" Text="Scan Job"  Width="50%" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-primary" Height="30px" OnClick="btn_scan_Click"/>
                </td>
            </tr>
            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="4" >
                   
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Button runat="server" ID="btn_generate" Text="Generate"  Width="50%" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Height="30px" OnClick ="btn_generate_Click" />
                </td>
            </tr>

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: auto;" colspan="5"> 

                <h3><asp:Label ID="lblResult" runat="server" CssClass="col-xs-11"></asp:Label></h3>

                <asp:DataGrid runat="server" CssClass="table table-hover" ID ="dg_inventoryDetail" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" Width="100%"  HorizontalAlign="Center"  AutoGenerateColumns="False" >
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775"  />
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="left" VerticalAlign="Middle" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None"  HorizontalAlign="left" VerticalAlign="Middle" />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />

                    <Columns>
                        <asp:BoundColumn DataField="jobNumber" HeaderText="JobNumber"></asp:BoundColumn>
                        <asp:BoundColumn DataField="partNumber" HeaderText="PartNumber"></asp:BoundColumn>
                        <asp:BoundColumn DataField="description" HeaderText="Description"></asp:BoundColumn>
                        <asp:BoundColumn DataField="quantity" HeaderText="Quantity"></asp:BoundColumn>
                        <asp:BoundColumn DataField="pqcQuantity" HeaderText="PqcQuantity"></asp:BoundColumn>
                        <asp:BoundColumn DataField="startOnTime" HeaderText="StartOnTime"></asp:BoundColumn>
                        <asp:BoundColumn DataField="dateTime" HeaderText="DateTime" Visible="true"></asp:BoundColumn>
                        <asp:BoundColumn DataField="day" HeaderText="Day" Visible="true"></asp:BoundColumn>
                        <asp:BoundColumn DataField="month" HeaderText="Month"></asp:BoundColumn>
                        <asp:BoundColumn DataField="year" HeaderText="Year"></asp:BoundColumn>
                        <asp:BoundColumn DataField="showFlag" HeaderText="ShowFlag" Visible="true"></asp:BoundColumn>
                        <asp:BoundColumn DataField="setUpQTY" HeaderText="SetUpQTY"></asp:BoundColumn>
                        <asp:BoundColumn DataField="buyOffQty" HeaderText="BuyOffQty"></asp:BoundColumn>
                        <asp:BoundColumn DataField="lotNo" HeaderText="LotNo"></asp:BoundColumn>
                    </Columns>

                </asp:DataGrid>
                </td>
            </tr>

        </table> 
    </div>
</div> 
           
</asp:Content>