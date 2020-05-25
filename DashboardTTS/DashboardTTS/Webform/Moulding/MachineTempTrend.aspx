<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MachineTempTrend.aspx.cs"  MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.Moulding.MachineTempTrend" %>

<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     
<link href="../Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />  


<div style="width: 1154px;margin: auto">

        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Moulding Temperature Trend Chart" Font-Size="12" ForeColor="White"></Asp:label>
                </td>
            </tr>
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="From :" Width="100px"></asp:Label>  
                </td>
                <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                    <igsch:WebDateChooser ID="infDchFrom" runat="server"   Width="100%" Value="" > </igsch:WebDateChooser>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="To :"></asp:Label> 
                </td>
            <td style =" border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                <igsch:WebDateChooser ID="infDchTo" runat="server"   Width="100%"></igsch:WebDateChooser>
            </td>
                                                      
            <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
            </td>
            </tr>

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                <asp:Label runat="server" Text="Machine :" Width="100px"></asp:Label>  
                </td>
                <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                <asp:DropDownList ID="ddl_Machine"  runat="server" Width="100%" Height="23px"  > 
                    <asp:ListItem Value="1" Selected ="True">Machine 1</asp:ListItem>
                    <asp:ListItem Value="2">No.2</asp:ListItem>
                    <asp:ListItem Value="3">No.3</asp:ListItem>
                    <asp:ListItem Value="4">No.4</asp:ListItem>
                    <asp:ListItem Value="5">No.5</asp:ListItem>
                    <asp:ListItem Value="6">No.6</asp:ListItem>
                    <asp:ListItem Value="7">No.7</asp:ListItem>
                    <asp:ListItem Value="8">No.8</asp:ListItem>
                    <asp:ListItem Value="9">No.9</asp:ListItem>
                </asp:DropDownList>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                </td>
                <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px;">
                </td>
                                                        
                                                      
                 <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; width: 20%; height: 50px; ">
                    <asp:Button runat="server" ID="btn_Generate" Text="Generate"  Width="50%"  OnClick="btn_Generate_Click"  BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Height="30px" />
                 </td>
            </tr>
            <tr style ="width: 100%">
                <td style = "padding: 25px 25px 25px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="5"> 
                <asp:Label ID="lblResult" runat="server"></asp:Label> 
                </td>
            </tr>
                        
            <tr style ="width: 100%">
                <td style = "padding: 25px 25px 25px 25px; border: 1px solid #CCCCCC; width:100%;  font-family: 'Arial Unicode MS'; height: 50px;" colspan="5" aria-hidden="False" aria-multiline="True" aria-multiselectable="True" aria-orientation="vertical" draggable="false"> 
                    <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dg_TempInfo" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="false" >
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundColumn DataField="LineNo" HeaderText="Zone"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Setting" HeaderText="Setting Temperature" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="AVG" HeaderText="AVG Temperature" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="Max" HeaderText="Max Temperature"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Min" HeaderText="Min Temperature"></asp:BoundColumn>
                            </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
                                          
            <tr id="trChart" style ="width: 100%">
                <td style = "padding: 25px 25px 25px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="5" aria-hidden="False" aria-multiline="True" aria-multiselectable="True" aria-orientation="vertical" draggable="false"> 
                    <asp:Chart ID="TempChart"  runat="server" Width="1000px" Height="800px" BackColor="245, 245, 250" BorderlineColor="Black" ImageType="Bmp" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)">
                        <Series>
                            <asp:Series Name="Series1" ChartArea="ChartArea1" >
                                <Points>
                                    <asp:DataPoint AxisLabel="#VALX #VAL" CustomProperties="LabelStyle=Center" XValue="1" YValues="50" />
                                    <asp:DataPoint XValue="2" YValues="40" />
                                    <asp:DataPoint XValue="3" YValues="60" />
                                    <asp:DataPoint XValue="4" YValues="80" />
                                    <asp:DataPoint XValue="5" YValues="90" />
                                </Points>
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1">
                                <AxisX LabelAutoFitStyle="IncreaseFont, DecreaseFont, StaggeredLabels, LabelsAngleStep90">
                                    <LabelStyle Angle="-90" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>                           
                    <%--    <Legends>
                            <asp:Legend  Docking="Top" BackColor="Transparent"  Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Name="Default" >                               
                            </asp:Legend>
                        </Legends>--%>
                    </asp:Chart>
                </td>
            </tr>
                                                      
        </table> 

</div>
                     
</asp:Content>
