<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MouldingDailyReport.aspx.cs" Inherits="DashboardTTS.Webform.Reports.MouldingDailyReport" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
    <link href="../Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />  
    <link href="/Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />

    <script src="../js/Dashboard.js"> </script>


    <style>
        .Test{
            width:auto;
            height:auto;
        }
    </style>


    <table style =" padding: 0px 0px 0px 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
        <tr style ="width: 100%">
            <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="1"> 
                <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                <Asp:label ID="lb_Header"   runat="server" Font-Names="Arial Unicode MS"  Text="" Font-Size="12" ForeColor="White"/>
            </td>
        </tr>
        <tr style ="width: 100%"> 
            <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; text-align:center; height: 50px; " colspan="1" > 
                <table>
                    <tr>
                        <td width="150px">
                            <igsch:WebDateChooser ID="infDchFrom" runat="server"  Value="" ></igsch:WebDateChooser>
                         
                        </td>
                        <td>
                            <asp:Button ID="btnGenerate" runat="server" Text="Report Check And Verify By" OnClick="btnGenerate_Click" BorderStyle="Solid" BorderWidth="1px" Height="30px" CssClass="btn-success" />
                            
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnDayCheck" runat="server" Text="Day Shift Supervisor" OnClick="btnDayCheck_Click" BorderStyle="Solid" BorderWidth="1px" Height="30px" CssClass="btn-success" />
                            
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnNightCheck" runat="server" Text="Night Shift Supervisor" OnClick="btnNightCheck_Click" BorderStyle="Solid" BorderWidth="1px" Height="30px" CssClass="btn-success" />

                        </td>
                    </tr>
                </table> 
            </td>      
        </tr>
        <tr style ="width: 100%"> 
            <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; text-align:center; height: 50px;" colspan="1"> 
                <asp:Label ID="Label1" runat="server" Text="Moulding Department" Font-Bold="true" Font-Size="Large" ></asp:Label>
            </td>
        </tr>

        <tr style ="width: 100%">
            <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="1"> 
                <asp:DataGrid runat="server" CssClass="table table-hover" Width ="700px" ID ="dg_Moulding_Total" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="3" AutoGenerateColumns="False" >
                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundColumn DataField="Item" HeaderText=""></asp:BoundColumn>
                        <asp:BoundColumn DataField="Total QTY" HeaderText="Total QTY"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Total Pass" HeaderText="Total Pass"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Total Reject" HeaderText="Total Reject"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Total RejRate" HeaderText="Total RejRate"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Rej Costing" HeaderText="Rej Cost">
                            <ItemStyle  ForeColor ="Red"  Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:BoundColumn>
                    </Columns>
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                </asp:DataGrid>
                        
           
                <asp:DataGrid runat="server" CssClass="table table-hover" style ="line-height: 15px; width: 100%"  ID ="dg_Moulding_Day" CellPadding="10" ForeColor="#333333" GridLines="Vertical"  CellSpacing="6"  AutoGenerateColumns="False" AllowSorting="True" BorderColor="Black" BorderWidth="1px" >
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"   />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="False" ForeColor="White" Wrap="True"  HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="Solid" BorderColor="Gray" VerticalAlign="Middle" HorizontalAlign="Center" BorderWidth="1px"   />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <Columns> 
                        <asp:BoundColumn HeaderStyle-Width="40px" DataField="MachineID" HeaderText="MC NO"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="100px" DataField="Model" HeaderText="Model"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="200px" DataField="PartNo" HeaderText="PartNo"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="80px" DataField="JigNo" HeaderText="JigNo"></asp:BoundColumn>

                        <asp:ButtonColumn HeaderStyle-Width="10px" DataTextField="MCRunHours" HeaderText="MC Run Hours" CommandName="LinkDetail"></asp:ButtonColumn>
                        <%--<asp:BoundColumn HeaderStyle-Width="10px" DataField="MCRunHours" HeaderText="MC Run Hours"></asp:BoundColumn>--%>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="CavityCount" HeaderText="Cavity Count"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="TotalShots" HeaderText="Total Shots"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="TotalPassPCS" HeaderText="Total Pass Pcs"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="RejectQtyPCS" HeaderText="Reject Qty Pcs">
                            <ItemStyle BackColor="#FFCBB3" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="ProductionRejRate" HeaderText="Prodn RejRate">
                            <ItemStyle BackColor="#FFCBB3" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:BoundColumn>


                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="White Dot" HeaderText="White Dot"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Scratches" HeaderText="Scratches"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Dented Mark" HeaderText="Dented Mark"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Shinning Dot" HeaderText="Shinning Dot"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Black Mark" HeaderText="Black Mark"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Sink Mark" HeaderText="Sink Mark"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Flow Mark" HeaderText="Flow Mark"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="High Gate" HeaderText="High Gate"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Silver Steak" HeaderText="Silver Steak"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Black Dot" HeaderText="Black Dot"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Oil Stain" HeaderText="Oil Stain"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Flow Line" HeaderText="Flow Line"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Over - Cut" HeaderText="Over Cut"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Crack" HeaderText="Crack"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Short Mold" HeaderText="Short Mold"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Stain Mark" HeaderText="Stain Mark"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Weld Line" HeaderText="Weld Line"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Flashes" HeaderText="Flashes"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Foreign Materials" HeaderText="Foreign Materials"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Drag" HeaderText="Drag"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Material Bleed" HeaderText="Material Bleed"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Bent" HeaderText="Bent"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Defrom" HeaderText="Defrom"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Gas Mark" HeaderText="Gas Mark"></asp:BoundColumn>


                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="IPQC Buyoff" HeaderText="IPQC Buyoff"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="MC Adjust Scrap Short" HeaderText="Purging Material (Kgs)"></asp:BoundColumn>
                        <%--<asp:BoundColumn HeaderStyle-Width="10px" DataField="MCAdjustment" HeaderText="MC Adjust Scrap (Shots)"></asp:BoundColumn>--%>

                        <%--<asp:BoundColumn HeaderStyle-Width="10px" DataField="SetupRejectPCS" HeaderText="Setup Reject Pcs">
                            <ItemStyle BackColor="#FFCBB3" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:BoundColumn>--%>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="MCAdjustment" HeaderText="MC Adjust Scrap (Shots)">
                            <ItemStyle BackColor="#FFCBB3" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="SetupRejRate" HeaderText="Setup RejRate">
                            <ItemStyle BackColor="#FFCBB3" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:BoundColumn>

                        <asp:BoundColumn DataField="Operator" HeaderText="Operator"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="100px" DataField="Inspd By" HeaderText="Verify By"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="200px" DataField="Remarks" HeaderText="Remarks" Visible="false"></asp:BoundColumn>
                    </Columns>
                        
                </asp:DataGrid>
                <br />
                <asp:DataGrid runat="server"  CssClass="table table-hover" style ="line-height: 15px; width: 100%"  ID ="dg_Moulding_Night" CellPadding="10" ForeColor="#333333" GridLines="Vertical"  CellSpacing="6"  AutoGenerateColumns="False" AllowSorting="True" BorderColor="Black" BorderWidth="1px" >
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"   />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="False" ForeColor="White" Wrap="True"  HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="Solid" BorderColor="Gray" VerticalAlign="Middle" HorizontalAlign="Center" BorderWidth="1px"   />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <Columns> 
                        <asp:BoundColumn HeaderStyle-Width="40px"  DataField="MachineID" HeaderText="MC NO"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="100px" DataField="Model" HeaderText="Model"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="200px" DataField="PartNo" HeaderText="PartNo"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="80px"  DataField="JigNo" HeaderText="JigNo"></asp:BoundColumn>

                        <asp:ButtonColumn HeaderStyle-Width="10px" DataTextField="MCRunHours" HeaderText="MC Run Hours" CommandName="LinkDetail"></asp:ButtonColumn>
                        <%--<asp:BoundColumn HeaderStyle-Width="10px" DataField="MCRunHours" HeaderText="MC Run Hours"></asp:BoundColumn>--%>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="CavityCount" HeaderText="Cavity Count"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="TotalShots" HeaderText="Total Shots"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="TotalPassPCS" HeaderText="Total Pass Pcs"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="RejectQtyPCS" HeaderText="Reject Qty Pcs">
                            <ItemStyle BackColor="#FFCBB3" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="ProductionRejRate" HeaderText="Prodn RejRate">
                            <ItemStyle BackColor="#FFCBB3" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:BoundColumn>

                     
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="White Dot" HeaderText="White Dot"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Scratches" HeaderText="Scratches"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Dented Mark" HeaderText="Dented Mark"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Shinning Dot" HeaderText="Shinning Dot"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Black Mark" HeaderText="Black Mark"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Sink Mark" HeaderText="Sink Mark"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Flow Mark" HeaderText="Flow Mark"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="High Gate" HeaderText="High Gate"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Silver Steak" HeaderText="Silver Steak"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Black Dot" HeaderText="Black Dot"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Oil Stain" HeaderText="Oil Stain"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Flow Line" HeaderText="Flow Line"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Over - Cut" HeaderText="Over Cut"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Crack" HeaderText="Crack"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Short Mold" HeaderText="Short Mold"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Stain Mark" HeaderText="Stain Mark"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Weld Line" HeaderText="Weld Line"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Flashes" HeaderText="Flashes"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Foreign Materials" HeaderText="Foreign Materials"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Drag" HeaderText="Drag"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Material Bleed" HeaderText="Material Bleed"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Bent" HeaderText="Bent"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Defrom" HeaderText="Defrom"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="Gas Mark" HeaderText="Gas Mark"></asp:BoundColumn>

                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="IPQC Buyoff" HeaderText="IPQC Buyoff"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="MC Adjust Scrap Short" HeaderText="Purging Material (Kgs)"></asp:BoundColumn>
                        <%--<asp:BoundColumn HeaderStyle-Width="10px" DataField="MCAdjustment" HeaderText="MC Adjust Scrap (Shots)"></asp:BoundColumn>

                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="SetupRejectPCS" HeaderText="Setup Reject Pcs">
                            <ItemStyle BackColor="#FFCBB3" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:BoundColumn>--%>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="MCAdjustment" HeaderText="MC Adjust Scrap (Shots)">
                            <ItemStyle BackColor="#FFCBB3" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="10px" DataField="SetupRejRate" HeaderText="Setup RejRate">
                            <ItemStyle BackColor="#FFCBB3" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:BoundColumn>

                        <asp:BoundColumn DataField="Operator" HeaderText="Operator"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="100px" DataField="Inspd By" HeaderText="Verify By"></asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-Width="200px" DataField="Remarks" HeaderText="Remarks" Visible="false"></asp:BoundColumn> 
                    </Columns>
                        
                </asp:DataGrid>
                <br />
            </td>
        </tr>
                         
    </table>


</asp:Content>

