<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Productivity.aspx.cs" Inherits="DashboardTTS.Webform.Reports.Productivity" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
    <link href="../Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />  
    <link href="/Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />

     <script src="../js/Dashboard.js"> </script>

    
    
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lb_Header"   runat="server" Font-Names="Arial Unicode MS"  Text="" Font-Size="12" ForeColor="White"/>
                </td>
            </tr>

            <!--search date-->
            <tr style ="width: 100%"> 
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; text-align:center; height: 50px; "> 
                    <table>
                        <tr>
                            <td style="width:150px">
                                <igsch:WebDateChooser ID="infDchFrom" runat="server"  Value="" />
                            </td>
                            <td>
                                <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click" BorderStyle="Solid" BorderWidth="1px" Height="30px" CssClass="btn-success" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <!--moulding title-->
            <tr style ="width: 100%"> 
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; text-align:center; height: 50px;"> 
                    <asp:Label ID="Label1" runat="server" Text="Moulding Department" Font-Bold="true" Font-Size="Large" ></asp:Label>
                </td>
            </tr>
            <!--moulding datagrid-->
            <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                        <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dgMouldingDay" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="False" >
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <EditItemStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundColumn DataField="ProductType"  HeaderText="Day Shift" HeaderStyle-Width="10%" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="ManPower" HeaderText="Man Power" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Attendance" HeaderText="Attendance" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="AttendRate" HeaderText="Attend%" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TargetHR" HeaderText="Target Prod HRs" HeaderStyle-Width="5%"></asp:BoundColumn>

                                <asp:BoundColumn DataField="ActualHR" HeaderText="Actual Prod HRs"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TargetQty" HeaderText="Target Shots"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TotalQty" HeaderText="Total Shots"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ActualQty" HeaderText="Actual Shots"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RejectQty" HeaderText="Reject QTY"></asp:BoundColumn>
                                <asp:BoundColumn DataField="BuyoffQty" HeaderText="IPQC Buy Off"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RejRate" HeaderText="Type REJ%"></asp:BoundColumn>

                                <asp:BoundColumn DataField="Utilization" HeaderText="Utilization%" HeaderStyle-Width="5%"></asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                        <br />
                         <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dgMouldingNight" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="False" >
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <EditItemStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundColumn DataField="ProductType"  HeaderText="Night Shift" HeaderStyle-Width="10%" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="ManPower" HeaderText="Man Power" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Attendance" HeaderText="Attendance" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="AttendRate" HeaderText="Attend%" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TargetHR" HeaderText="Target Prod HRs" HeaderStyle-Width="5%"></asp:BoundColumn>  
                                <asp:BoundColumn DataField="ActualHR" HeaderText="Actual Prod HRs"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TargetQty" HeaderText="Target Shots"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TotalQty" HeaderText="Total Shots"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ActualQty" HeaderText="Actual Shots"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RejectQty" HeaderText="Reject QTY"></asp:BoundColumn>
                                <asp:BoundColumn DataField="BuyoffQty" HeaderText="IPQC Buy Off"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RejRate" HeaderText="Type REJ%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Utilization" HeaderText="Utilization%" HeaderStyle-Width="5%"></asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>


            <!--painting title-->
                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; text-align:center; height: 50px; width=70%" > 
                        <table style="width:100%">
                            <tr style="width:100%">
                                <td style="width:80%">
                                    <asp:Label ID="Label2" runat="server" Text="Painting Department" Font-Bold="true" Font-Size="Large" ></asp:Label> 
                                </td>
                                <td style="width:15%">
                                    <asp:Label runat="server"  ID="lbMessagepainting"></asp:Label>
                                </td>
                                <td style="width:5%">
                                    <asp:Button runat="server" Height="23px" Width="60px" ID="btnUpdatePainting" OnClick="btnUpdatePainting_Click" CssClass="btn-success" Text="Update" />
                                    <asp:Button runat="server" Height="23px" Width="60px" ID="btnEditPainting" OnClick="btnEditPainting_Click" CssClass="btn-danger" Text="Edit" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            <!--painting datagrid-->
                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                        <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dgPaintingDay" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="False" >
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <EditItemStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundColumn DataField="ProductType"  HeaderText="Day Shift" HeaderStyle-Width="10%" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="ManPower" HeaderText="Man Power" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Attendance" HeaderText="Attendance" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="AttendRate" HeaderText="Attend%" HeaderStyle-Width="5%"></asp:BoundColumn>

                                <asp:BoundColumn DataField="TargetHR" HeaderText="Target Prod HRs" HeaderStyle-Width="5%"></asp:BoundColumn>  
                                <asp:BoundColumn DataField="ActualHR" HeaderText="Actual Prod HRs" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="TargetQty" HeaderText="Target Tray" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="TotalQty" HeaderText="Total Tray"></asp:BoundColumn> 
                                <asp:BoundColumn DataField="ActualQty" HeaderText="Actual QTY" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="RejectQty" HeaderText="Reject QTY" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RejRate" HeaderText="Type REJ%" Visible="false" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="Utilization" HeaderText="Utilization%" HeaderStyle-Width="5%"></asp:BoundColumn>
                                

                                <asp:TemplateColumn HeaderText="Target Prod HRs" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtTargetHR" AutoCompleteType="Disabled" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Actual Prod HRs">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtActualHR" AutoCompleteType="Disabled" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Target Tray">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtTargetQty" AutoCompleteType="Disabled" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Total Tray">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtTotalQty" AutoCompleteType="Disabled" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Actual QTY">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtActualQty" AutoCompleteType="Disabled" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Reject QTY" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtRejectQty" AutoCompleteType="Disabled" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>

            <!--laser title-->
                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; text-align:center; height: 50px;" > 
                        <asp:Label ID="Label3" runat="server" Text="Laser Department" Font-Bold="true" Font-Size="Large" ></asp:Label> 
                    </td>
                </tr>
            <!--laser datagrid-->
                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" > 
                         <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dg_Laser_Day" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="False" >
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <EditItemStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundColumn DataField="ProductType"  HeaderText="Day Shift" HeaderStyle-Width="10%" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="ManPower" HeaderText="Man Power" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Attendance" HeaderText="Attendance" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="AttendRate" HeaderText="Attend%" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TargetHR" HeaderText="Target Prod HRs" HeaderStyle-Width="5%"></asp:BoundColumn>  
                                <asp:BoundColumn DataField="ActualHR" HeaderText="Actual Prod HRs" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="TargetQty" HeaderText="Target QTY" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="TotalQty" HeaderText="Total QTY"></asp:BoundColumn> 
                                <asp:BoundColumn DataField="ActualQty" HeaderText="Actual QTY" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="RejectQty" HeaderText="Reject QTY" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="RejRate" HeaderText="Type REJ%" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="Utilization" HeaderText="Utilization%" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Productivity" HeaderText="Productivity%" Visible="false"></asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                        <br />
                         <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dg_Laser_Night" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="False" >
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <EditItemStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundColumn  DataField="ProductType"  HeaderText="Night Shift" HeaderStyle-Width="10%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ManPower" HeaderText="Man Power" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Attendance" HeaderText="Attendance" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="AttendRate" HeaderText="Attend%" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TargetHR" HeaderText="Target Prod HRs" HeaderStyle-Width="5%"></asp:BoundColumn>  
                                <asp:BoundColumn DataField="ActualHR" HeaderText="Actual Prod HRs" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="TargetQty" HeaderText="Target QTY" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="TotalQty" HeaderText="Total QTY" ></asp:BoundColumn> 
                                <asp:BoundColumn DataField="ActualQty" HeaderText="Actual QTY" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="RejectQty" HeaderText="Reject QTY" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="RejRate" HeaderText="Type REJ%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Utilization" HeaderText="Utilization%" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Productivity" HeaderText="Productivity%" Visible="false"></asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>

            <!--pqc title-->
                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; text-align:center; height: 50px;"> 
                        <table style="width:100%">
                            <tr style="width:100%">
                                <td style="width:80%">
                                    <asp:Label ID="Label4" runat="server" Text="PQC Department" Font-Bold="true" Font-Size="Large" ></asp:Label> 
                                </td>
                                <%--<td style="width:15%">
                                    <asp:Label runat="server"  ID="lbMessagePQC"></asp:Label>
                                </td>
                                <td style="width:5%">
                                    <asp:Button runat="server" Height="23px" Width="60px" ID="btnUpdatePQC" OnClick="btnUpdatePQC_Click" CssClass="btn-success" Text="Update" />
                                    <asp:Button runat="server" Height="23px" Width="60px" ID="btnEditPQC" OnClick="btnEditPQC_Click" CssClass="btn-danger" Text="Edit" />
                                </td>--%>
                            </tr>
                        </table>
                    </td>
                </tr>
            <!--pqc datagrid-->
                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" > 
                        <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dgPQCDay" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="False" >
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <EditItemStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundColumn DataField="ProductType"  HeaderText="Day Shift" HeaderStyle-Width="10%" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="ManPower" HeaderText="Man Power" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Attendance" HeaderText="Attendance" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="AttendRate" HeaderText="Attend%" HeaderStyle-Width="5%"></asp:BoundColumn>

                                <asp:BoundColumn DataField="TargetHR" HeaderText="Target Prod HRs" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ActualHR" HeaderText="Actual Prod HRs" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="TargetQty" HeaderText="Target QTY"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TotalQty" HeaderText="Total QTY" ></asp:BoundColumn> 
                                <asp:BoundColumn DataField="ActualQty" HeaderText="Actual QTY" ></asp:BoundColumn>

                                <asp:BoundColumn DataField="MouldRejCount" HeaderText="Mould Rej" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="PaintRejCount" HeaderText="Paint/Laser Rej" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="LaserRejCount" HeaderText="Laser Rej" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="OthersRejCount" HeaderText="Vendor Rej" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RejRate" HeaderText="REJ%"></asp:BoundColumn>



                               <%--<asp:TemplateColumn HeaderText="Target Prod HRs" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtTargetHR" AutoCompleteType="Disabled" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Actual Prod HRs">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtActualHR" AutoCompleteType="Disabled" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Target QTY">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtTargetQty" AutoCompleteType="Disabled" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Total QTY">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtTotalQty" AutoCompleteType="Disabled" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Actual QTY">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtActualQty" AutoCompleteType="Disabled" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Mould Rej">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtMouldRej" AutoCompleteType="Disabled"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn HeaderText="Paint/Laser Rej">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtPaintRej" AutoCompleteType="Disabled"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn HeaderText="Laser Rej" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtLaserRej" AutoCompleteType="Disabled"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn HeaderText="Vendor Rej" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtVendorRej" AutoCompleteType="Disabled"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn HeaderText="Print Rej" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtPrintRej" AutoCompleteType="Disabled"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>--%>


                            </Columns>
                        </asp:DataGrid>
                        <br />
                         <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dgPQCNight" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="False" >
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <EditItemStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundColumn DataField="ProductType"  HeaderText="Night Shift" HeaderStyle-Width="10%" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="ManPower" HeaderText="Man Power" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Attendance" HeaderText="Attendance" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="AttendRate" HeaderText="Attend%" HeaderStyle-Width="5%"></asp:BoundColumn>

                                <asp:BoundColumn DataField="TargetHR" HeaderText="Target Prod HRs" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ActualHR" HeaderText="Actual Prod HRs" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="TargetQty" HeaderText="Target QTY"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TotalQty" HeaderText="Total QTY" ></asp:BoundColumn> 
                                <asp:BoundColumn DataField="ActualQty" HeaderText="Actual QTY" ></asp:BoundColumn>

                                <asp:BoundColumn DataField="MouldRejCount" HeaderText="Mould Rej" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="PaintRejCount" HeaderText="Paint/Laser Rej" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="LaserRejCount" HeaderText="Laser Rej" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="OthersRejCount" HeaderText="Vendor Rej" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RejRate" HeaderText="REJ%"></asp:BoundColumn>



                                <%--<asp:TemplateColumn HeaderText="Target Prod HRs" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtTargetHR" AutoCompleteType="Disabled" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Actual Prod HRs">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtActualHR" AutoCompleteType="Disabled" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Target QTY">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtTargetQty" AutoCompleteType="Disabled" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Total QTY">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtTotalQty" AutoCompleteType="Disabled" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Actual QTY">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtActualQty" AutoCompleteType="Disabled" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>


                                
                                <asp:TemplateColumn HeaderText="Mould Rej">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtMouldRej" AutoCompleteType="Disabled"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn HeaderText="Paint/Laser Rej">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtPaintRej" AutoCompleteType="Disabled"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn HeaderText="Laser Rej" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtLaserRej" AutoCompleteType="Disabled"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn HeaderText="Vendor Rej" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtVendorRej" AutoCompleteType="Disabled"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn HeaderText="Print Rej" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtPrintRej" AutoCompleteType="Disabled"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>--%>


                            </Columns>
                        </asp:DataGrid>

                    </td>
                </tr>


                            
               <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; text-align:center; height: 50px;" >
                        <table style="width:100%">
                            <tr style="width:100%">
                                <td style="width:80%">
                                    <asp:Label ID="Label5" runat="server" Text="Assambly Department" Font-Bold="true" Font-Size="Large" ></asp:Label>  
                                </td>
                                <td style="width:15%">
                                    <asp:Label runat="server"  ID="lbMessageAssy"></asp:Label>
                                </td>
                                <td style="width:5%">
                                    <asp:Button runat="server" Height="23px" Width="60px" ID="btnUpdateAssy" OnClick="btnUpdateAssy_Click" CssClass="btn-success" Text="Update" />
                                    <asp:Button runat="server" Height="23px" Width="60px" ID="btnEditAssy" OnClick="btnEditAssy_Click" CssClass="btn-danger" Text="Edit" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

            <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                        <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dgAssyDay" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="False" >
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <EditItemStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundColumn DataField="ProductType"  HeaderText="Day Shift" HeaderStyle-Width="10%" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="ManPower" HeaderText="Man Power" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Attendance" HeaderText="Attendance" HeaderStyle-Width="5%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="AttendRate" HeaderText="Attend%" HeaderStyle-Width="5%"></asp:BoundColumn>
                                
                                <asp:BoundColumn DataField="TargetHR" HeaderText="Target Prod HRs" HeaderStyle-Width="5%"></asp:BoundColumn>  
                                <asp:BoundColumn DataField="ActualHR" HeaderText="Actual Prod HRs" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="TargetQty" HeaderText="Target QTY" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="TotalQty" HeaderText="Total QTY" ></asp:BoundColumn> 
                                <asp:BoundColumn DataField="ActualQty" HeaderText="Actual QTY" ></asp:BoundColumn>
                               
                                <asp:BoundColumn DataField="bezelRejQty" HeaderText="Bezel Rej" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="coverRejQty" HeaderText="Cover Rej" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="escRejQty" HeaderText="ESC Rej" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="btnRejQty" HeaderText="Btn Rej" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="indicatorRejQty" HeaderText="Indicator Rej" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="RejRate" HeaderText="REJ%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Utilization" HeaderText="Utilization%" HeaderStyle-Width="5%"></asp:BoundColumn>



                                <asp:TemplateColumn HeaderText="Target Prod HRs" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtTargetHR" AutoCompleteType="Disabled" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Actual Prod HRs">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtActualHR" AutoCompleteType="Disabled" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Target QTY">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtTargetQty" AutoCompleteType="Disabled" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Total QTY">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtTotalQty" AutoCompleteType="Disabled" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Actual QTY">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtActualQty" AutoCompleteType="Disabled" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Bezel Rej">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtRejBezel" AutoCompleteType="Disabled" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn HeaderText="Cover Rej">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtRejectCover" AutoCompleteType="Disabled" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn HeaderText="ESC Rej">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtRejectESC" AutoCompleteType="Disabled" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn HeaderText="Btn Rej">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtRejectBtn" AutoCompleteType="Disabled" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn HeaderText="Indicator Rej">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtRejectIndicator" AutoCompleteType="Disabled" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>  
</asp:Content>

