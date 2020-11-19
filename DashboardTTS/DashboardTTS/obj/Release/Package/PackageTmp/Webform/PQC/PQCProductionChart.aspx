<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PQCProductionChart.aspx.cs" Inherits="DashboardTTS.Webform.PQC.PQCProductionChart" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>


 <asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 


    <div style="position: relative; width: 80%; height: auto; margin: auto">
										
  
    <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">

        <tr style ="width: 100%">
            <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="PQC Production Chart" Font-Size="12" ForeColor="White"/>
            </td>
        </tr>

        <tr style ="width: 100%">
            <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                <asp:Label runat="server" Text="Report Type :" Width="100%" />
            </td>
            <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px;">
                <asp:DropDownList ID="ddlReportType"  runat="server" Width="100%" Height="23px" >
                    <asp:ListItem Value="Daily">Daily</asp:ListItem>
                    <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                    <asp:ListItem Value="Yearly">Yearly</asp:ListItem>
                    <asp:ListItem Value="PartNumber">Part No</asp:ListItem>
                    <asp:ListItem Value="Model">Model</asp:ListItem>
                    <asp:ListItem Value="Customer">Customer</asp:ListItem>
                    <asp:ListItem Value="Type">Type</asp:ListItem>
                    <asp:ListItem Value="Rejection">Rejection</asp:ListItem>
                    <asp:ListItem Value="PIC">PIC</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                <asp:Label runat="server" Text="Year:" Width="100%" />
            </td>
            <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                <asp:DropDownList ID="ddlYear"  runat="server" Width="100%" name="listYear" Height="23px" />
            </td>                                
            <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
            </td>
        </tr>

        <tr>
            <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                <asp:Label runat="server" Text="Date From :" Width="100px"></asp:Label>
            </td>
            <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                <igsch:WebDateChooser ID="infDchFrom" runat="server"   Width="100%" Value="" />
            </td>

            <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                <asp:Label runat="server" Text="Date To :"></asp:Label> 
            </td>
            <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                <igsch:WebDateChooser ID="infDchTo" runat="server"   Width="100%" />
            </td>
            <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
                
            </td>
        </tr>

        <tr>
            <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                <asp:Label runat="server" Text="Part No :" Width="100%" />
            </td>
            <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                <asp:TextBox runat="server" ID="txtPartNo" Height="23px" Width="100%"></asp:TextBox>
            </td>

            <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                <asp:Label runat="server" Text="Model :" Width="100%"></asp:Label> 
            </td>
            <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                <asp:TextBox runat="server" ID="txtModel" Height="23px" Width="100%"></asp:TextBox>
            </td>
            <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
                
            </td>
        </tr>

        <tr style ="width: 100%">
            <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                <asp:Label runat="server" Text="Customer :" Width="100%"></asp:Label>
            </td>
            <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                <asp:TextBox runat="server" ID="txtCustomer" Height="23px" Width="100%"></asp:TextBox>
            </td>
            <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                <asp:Label runat="server" Text="PIC :" Width="100%"></asp:Label> 
            </td>
            <td style =" border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                <asp:TextBox runat="server" ID="txtPIC" Height="23px" Width="100%"></asp:TextBox>
            </td>
            <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
                <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Height="30px" />
            </td>
        </tr>




                                                      
        <tr id="trChart" style ="width: 100%">
            <td style = "padding: 25px 25px 25px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="5" aria-hidden="False" aria-multiline="True" aria-multiselectable="True" aria-orientation="vertical" draggable="false"> 
                <asp:Label ID="lblResult" runat="server"></asp:Label> 
                <asp:Chart ID="ProdChart"  runat="server" Width="1400px" Height="500px" BackColor="245, 245, 250" BorderlineColor="Black" ImageType="Bmp" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)">
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

