<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MouldingProductRecordChart.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.Moulding.MouldingProductRecordChart" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


<div style="width: 80%;margin: auto">
 
    <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
        <tr style ="width: 100%">
            <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Production Record" Font-Size="12" ForeColor="White"></Asp:label>
            </td>
        </tr>
        <tr style ="width: 100%">
            <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                <asp:Label runat="server" Text="From :" Width="100px"></asp:Label>  
            </td>
            <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                <igsch:WebDateChooser ID="infDchFrom" runat="server"   Width="100%" Value="" >  </igsch:WebDateChooser>
            </td>
            <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                <asp:Label runat="server" Text="To :"></asp:Label> 
            </td>
            <td style =" border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                <igsch:WebDateChooser ID="infDchTo" runat="server"   Width="100%">  </igsch:WebDateChooser></td>
            <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
                
            </td>
        </tr>
        <tr style ="width: 100%">
            <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                <asp:Label runat="server" Text="Data Type :" Width="100px"></asp:Label>  
            </td>
            <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                <asp:DropDownList ID="ddl_dataType"  runat="server" Width="100%" Height="23px"  > 
                    <asp:ListItem  Value="">All</asp:ListItem>
                    <asp:ListItem Value="Running">Running</asp:ListItem>
                    <asp:ListItem Value="Adjustment">Adjustment</asp:ListItem>
                    <asp:ListItem Value="NoWIP">NoWIP</asp:ListItem>
                    <asp:ListItem Value="Mould_Testing">Mould Testing</asp:ListItem>
                    <asp:ListItem Value="Material_Testing">Material Testing</asp:ListItem>
                    <asp:ListItem Value="Change_Model">Change Model</asp:ListItem>
                    <asp:ListItem Value="No_Operator">No Operator</asp:ListItem>
                    <asp:ListItem Value="No_Material">No Material</asp:ListItem>
                    <asp:ListItem Value="Break_Time">Break Time</asp:ListItem>
                    <asp:ListItem Value="ShutDown">ShutDown</asp:ListItem>
                    <asp:ListItem Value="Damage_Mould">Damage Mould</asp:ListItem>
                    <asp:ListItem Value="Machine_Break">Machine Break</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                <asp:Label runat="server" Text="Shift :" Width="100%"></asp:Label>  
            </td>
            <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px;">
                <asp:DropDownList ID="ddl_Shift"  runat="server" Width="100%" Height="23px"  > 
                    <asp:ListItem Value="ALL">ALL</asp:ListItem>
                    <asp:ListItem Value="Day">Day</asp:ListItem>
                    <asp:ListItem Value="Night" >Night</asp:ListItem> 
                </asp:DropDownList>
            </td>                           
            <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
                       
            </td>
        </tr>
        <tr>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                <asp:Label runat="server" Text="MachineID:" Width="100px"></asp:Label>  
            </td>
            <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px;">
                <asp:DropDownList ID="ddL_MachineID"  runat="server" Width="100%" Height="23px"  > 
                    <asp:ListItem Value="">All</asp:ListItem>
                    <asp:ListItem Value="1">No.1</asp:ListItem>
                    <asp:ListItem Value="2">No.2</asp:ListItem>
                    <asp:ListItem Value="3">No.3</asp:ListItem>
                    <asp:ListItem Value="4">No.4</asp:ListItem>
                    <asp:ListItem Value="5">No.5</asp:ListItem>
                    <asp:ListItem Value="6">No.6</asp:ListItem>
                    <asp:ListItem Value="7">No.7</asp:ListItem>
                    <asp:ListItem Value="8">No.8</asp:ListItem>
                </asp:DropDownList>
            </td>                
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;">    </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;">    </td>
                <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
                <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Height="30px" />
            </td>
        </tr>
                                
                                                      
        <tr style ="width: 100%">
            <td style = "padding: 25px 25px 25px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="5"> 
                <asp:Label ID="lblResult" runat="server"></asp:Label> 
            </td>
        </tr>
                                                      
        <tr id="trChart" style ="width: 100%">
            <td style = "padding: 25px 25px 25px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="5" aria-hidden="False" aria-multiline="True" aria-multiselectable="True" aria-orientation="vertical" draggable="false" align="center"> 
                <asp:Chart ID="ProdChart"  runat="server" Width="1400"  Height="500px" BackColor="245, 245, 250" BorderlineColor="Black" ImageType="Bmp" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)">
                    <Series>
                        <asp:Series Name="Series1" ChartArea="ChartArea1">
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
                                                                  
                    <Legends>
                        <asp:Legend  Docking="Top" BackColor="Transparent"  Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Name="Legend1" >
                        </asp:Legend>   
                    </Legends>
                    
                </asp:Chart>
            </td>
        </tr>                      
    </table> 

</div>                        
</asp:Content>