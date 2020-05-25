<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MouldingProductivityChart.aspx.cs" Inherits="DashboardTTS.Webform.Moulding.MouldingProductivityChart" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>



<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div style="position: relative; width: 60%; height: auto; margin: auto">
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="LMMS Dashboard" Font-Size="12" ForeColor="White"></Asp:label>
                </td>
            </tr>
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="Machine No :" Width="100px"></asp:Label>  
                </td>
                <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px;">
                    <asp:DropDownList ID="ddlMachineID"  runat="server" Width="100%" Height="23px"  >
                        <asp:ListItem Value="" Selected="True">ALL</asp:ListItem>
                        <asp:ListItem Value="1">No.1</asp:ListItem>
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
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="Shift :"></asp:Label> 
                </td>
                <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                    <asp:DropDownList runat="server" ID="ddl_shift" Width="100%">
                        <asp:ListItem Value="" Selected="True">ALL</asp:ListItem>
                        <asp:ListItem Value="Day" >Day</asp:ListItem>
                        <asp:ListItem Value="Night" >Night</asp:ListItem>
                    </asp:DropDownList>
                </td>                    
                <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
                </td>
            </tr>
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="From :" Width="100px"></asp:Label>  
                </td>
                <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                    <igsch:WebDateChooser ID="infDchFrom" runat="server"   Width="100%" Value="" ></igsch:WebDateChooser>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="To :"></asp:Label>
                </td>
                <td style =" border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                    <igsch:WebDateChooser ID="infDchTo" runat="server"   Width="100%"></igsch:WebDateChooser>
                </td>                            
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
                    <asp:Chart ID="ProdChart"  runat="server" Width="1000px" Height="500px" BackColor="245, 245, 250" BorderlineColor="Black" ImageType="Bmp" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)">
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
                    </asp:Chart>
                </td>
            </tr>
                                                      
    </table> 
    </div>
</asp:Content>
