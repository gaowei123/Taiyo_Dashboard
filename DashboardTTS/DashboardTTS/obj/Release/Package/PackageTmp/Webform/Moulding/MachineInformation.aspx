<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MachineInformation.aspx.cs" Inherits="DashboardTTS.Webform.Molding.MachineInformation" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
    <link href="../Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />  
    <link href="/Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />

     <script src="../js/Dashboard.js"> </script>

    
     
     <div style="position: relative; width: auto; height: 257px; margin: auto; top: 0px; left: 0px;">
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="4"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lb_Header"   runat="server" Font-Names="Arial Unicode MS"  Text="" Font-Size="12" ForeColor="White"/>
                    </td>
            </tr>

             <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; text-align:center; height: 50px;" colspan="4"> 
                        <asp:Label ID="Label5" runat="server" Text="Injection Moulding Machine Main Specification" Font-Bold="true" Font-Size="Large" ></asp:Label> 
                </td>
                </tr>

                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="5"> 
                        <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dg_Main" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="false" >
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <EditItemStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:ButtonColumn  DataTextField="machineID" HeaderText="ID" CommandName="Update"></asp:ButtonColumn>
                                <asp:BoundColumn DataField="machineID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="maker" HeaderText="Maker"></asp:BoundColumn>
                                <asp:BoundColumn DataField="info" HeaderText="Info."></asp:BoundColumn>
                                <asp:BoundColumn DataField="model" HeaderText="Model"></asp:BoundColumn>
                                <asp:BoundColumn DataField="dateOfManu" HeaderText="Date Of Manufacture"></asp:BoundColumn>
                                <asp:BoundColumn DataField="screwDiameter" HeaderText="Screw Diameter"></asp:BoundColumn>
                                <asp:BoundColumn DataField="MaxOPNStroke" HeaderText="Max OPN Stroke"></asp:BoundColumn>
                                <asp:BoundColumn DataField="EJTStroke" HeaderText="EJT Stroke"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TiebarDistance" HeaderText="Tiebar Distance"></asp:BoundColumn>
                                <asp:BoundColumn DataField="MinMoldSize" HeaderText="Min Mold Size (H x V)"></asp:BoundColumn>
                                <asp:BoundColumn DataField="MinMoldThickness" HeaderText="Min Mold Thickness"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Dimensions" HeaderText="M/C Dimensions (L x W x H)"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DateTime" HeaderText="DateTime" Visible="false"></asp:BoundColumn>
                                
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>


                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; text-align:center; height: 50px;" colspan="4"> 
                        <asp:Label ID="Label1" runat="server" Text="Injection Moulding Machine" Font-Bold="true" Font-Size="Large" ></asp:Label> 
                </td>
                </tr>

                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="5"> 
                        <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dg_Machine" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="false" >
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <EditItemStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:ButtonColumn  DataTextField="machineID" HeaderText="ID" CommandName="Update"></asp:ButtonColumn>
                                <asp:BoundColumn DataField="machineID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="type" HeaderText="Type"></asp:BoundColumn>
                                <asp:BoundColumn DataField="makerModel" HeaderText="Maker/Model"></asp:BoundColumn>
                                <asp:BoundColumn DataField="serialNo" HeaderText="Serial No"></asp:BoundColumn>
                                <asp:BoundColumn DataField="dateOfManu" HeaderText="Date Of Manufacture"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CTRL" HeaderText="CTRL"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DateTime" HeaderText="DateTime" Visible="false"></asp:BoundColumn>

                                <asp:TemplateColumn>
                                <ItemTemplate>

                                </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>



                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; text-align:center; height: 50px;" colspan="4"> 
                        <asp:Label ID="Label2" runat="server" Text="Robot Arm" Font-Bold="true" Font-Size="Large" ></asp:Label> 
                    </td>
                </tr>

                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="4"> 
                    <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dg_RobotArm" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="false" >
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <Columns>
                            <asp:ButtonColumn  DataTextField="machineID" HeaderText="ID" CommandName="Update"></asp:ButtonColumn>
                            <asp:BoundColumn DataField="machineID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="model" HeaderText="Model"></asp:BoundColumn>
                            <asp:BoundColumn DataField="serialNo" HeaderText="Serial No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="controllerType" HeaderText="Controller Type"></asp:BoundColumn>
                            <asp:BoundColumn DataField="controllerSerialNo" HeaderText="Controller Serial No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="date" HeaderText="Date"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
                </tr>


                            
                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;  width:50%;  font-family: 'Arial Unicode MS'; text-align:center; height: 50px;" colspan="2"> 
                        <asp:Label ID="Label3" runat="server" Text="Temperature Controller" Font-Bold="true" Font-Size="Large" ></asp:Label> 
                </td>
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;  width:50%;   font-family: 'Arial Unicode MS'; text-align:center; height: 50px;" colspan="2"> 
                        <asp:Label ID="Label4" runat="server" Text="Hopper Dryer" Font-Bold="true" Font-Size="Large" ></asp:Label> 
                </td>
                </tr>
                           
                          

                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2"> 
                    <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dg_Temp" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="false" >
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <Columns>
                            <asp:ButtonColumn  DataTextField="machineID" HeaderText="ID" CommandName="Update"></asp:ButtonColumn>
                            <asp:BoundColumn DataField="machineID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="maker" HeaderText="Maker"></asp:BoundColumn>
                            <asp:BoundColumn DataField="model" HeaderText="Model"></asp:BoundColumn>
                            <asp:BoundColumn DataField="date" HeaderText="Date"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>

                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2"> 
                    <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dg_Dryer" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="false" >
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <Columns>
                            <asp:ButtonColumn  DataTextField="machineID" HeaderText="ID" CommandName="Update"></asp:ButtonColumn>
                            <asp:BoundColumn DataField="machineID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="maker" HeaderText="Maker"></asp:BoundColumn>
                            <asp:BoundColumn DataField="model" HeaderText="Model"></asp:BoundColumn>
                            <asp:BoundColumn DataField="date" HeaderText="Date"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
                </tr>
                          
                         
            </table> 
             
    </div>
</asp:Content>

