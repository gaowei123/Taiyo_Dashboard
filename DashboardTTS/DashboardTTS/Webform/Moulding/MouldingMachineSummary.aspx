<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MouldingMachineSummary.aspx.cs"  MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.Moulding.MouldingMachineSummary" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
    <link href="../Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />  
    <link href="/Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />


    
<div style="width: auto; height: 257px; align-items:center;margin:auto">
    <table style =" padding: 0px 10px 5px 10px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; align-content:center;align-items:center;align-self:center; line-height: 10px; vertical-align: 10%;">
        <tr style ="width: 100%">
            <td style = "padding: 5px 10px 5px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Font-Size="12" Text="Moulding Machine Summary Report" ForeColor="White"/>
            </td>
        </tr>

        <tr style="width:100%">
            <td style = "padding: 5px 10px 5px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                <asp:Label runat="server" Text="Date From :" Width="93%" Height="16px"></asp:Label>  
            </td>
            <td style = "padding: 5px 10px 5px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                <igsch:WebDateChooser ID="infDchFrom" runat="server" Height="23px"   Width="100%" Value="" > </igsch:WebDateChooser>
            </td>
            <td style = "padding: 5px 10px 5px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                <asp:Label runat="server" Text="Date To :" Width="93%" Height="16px"></asp:Label>  
            </td>
            <td style = "padding: 5px 10px 5px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                <igsch:WebDateChooser ID="infDchTo" runat="server" Height="23px"   Width="100%" Value="" > </igsch:WebDateChooser>
            </td>

            <td style = "padding: 5px 10px 5px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; width: 20%; height: 50px; ">
                <asp:CheckBox ID="ckbExceptWeekend" Text="Exclude Weekend" runat="server" />
            </td>
        </tr>

        <tr style="width:100%">
            <td style = "padding: 5px 10px 5px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                <asp:Label runat="server" Text="Shift :" Width="93%" Height="16px"></asp:Label>  
            </td>
            <td style = "padding: 5px 10px 5px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                <asp:DropDownList ID="ddl_shift"  runat="server" Width="100%"  BorderWidth="1px" Height="23px" >
                    <asp:ListItem Value="" Selected="True">All</asp:ListItem>
                    <asp:ListItem Value="Day">Day</asp:ListItem>
                    <asp:ListItem Value="Night">Night</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style = "padding: 5px 10px 5px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                <asp:Label runat="server" Text="Date not in:" Width="100px"></asp:Label>  
            </td>
            <td style = "padding: 5px 10px 5px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_DateNotIn" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" placeholder ="1/2/3/4/5...../30"  ></asp:TextBox>
            </td>
            <td style = "padding: 5px 10px 5px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; width: 20%; height: 50px; ">
                <asp:Button runat="server" ID="btn_Generate" Text="Generate"  Width="50%" OnClick="btn_Generate_Click" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Height="30px" />
            </td>
        </tr>


        <tr style ="width: 100%">   
            <td style = "padding: 5px 10px 0px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;  font-weight: bold;" colspan="5">

            <asp:Label ID="lblResult" runat="server"></asp:Label>

            <asp:DataGrid runat="server" CssClass="table table-hover" ID ="dg_MachineSummary" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" Width="100%"  HorizontalAlign="Center"  AutoGenerateColumns="False" >
            <AlternatingItemStyle BackColor="White" ForeColor="#284775"  />
            <EditItemStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
            <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <Columns>
                <asp:BoundColumn DataField="MachineID" HeaderText="MachineID"></asp:BoundColumn>
   

                <asp:BoundColumn DataField="Running" HeaderText="Running"></asp:BoundColumn>
                <asp:ButtonColumn CommandName="LinkAdjustment" DataTextField="Adjustment" HeaderText="Adjustment" ButtonType="LinkButton"  HeaderStyle-ForeColor="black"></asp:ButtonColumn>
                <asp:BoundColumn DataField="NoWIP" HeaderText="No Schedule"></asp:BoundColumn>
                <asp:ButtonColumn CommandName="LinkMould_Testing" DataTextField="Mould_Testing" HeaderText="Mould Testing"  ButtonType="LinkButton" HeaderStyle-ForeColor="black" ></asp:ButtonColumn>
                <asp:ButtonColumn CommandName="LinkMaterial_Testing" DataTextField="Material_Testing" HeaderText="Material Testing"  ButtonType="LinkButton" HeaderStyle-ForeColor="black"></asp:ButtonColumn>
                <asp:ButtonColumn CommandName="LinkChange_Model" DataTextField="Change_Model" HeaderText="Change Model" ButtonType="LinkButton" HeaderStyle-ForeColor="black"></asp:ButtonColumn>
                <asp:BoundColumn DataField="No_Operator" HeaderText="No Operator"></asp:BoundColumn>
                <asp:ButtonColumn CommandName="No_Material" DataTextField="No_Material" HeaderText="M/C Stop" ButtonType="LinkButton" HeaderStyle-ForeColor="black"></asp:ButtonColumn>
                <asp:ButtonColumn CommandName="LinkDamage_Mould" DataTextField="Damage_Mould" HeaderText="Mould Damage" ButtonType="LinkButton" HeaderStyle-ForeColor="black"></asp:ButtonColumn>
                <asp:ButtonColumn CommandName="LinkMachine_Break" DataTextField="Machine_Break" HeaderText="Machine Break Down" ButtonType="LinkButton" HeaderStyle-ForeColor="black"></asp:ButtonColumn>
                <asp:BoundColumn DataField="Break_Time" HeaderText="Meal"></asp:BoundColumn>                            
                <asp:BoundColumn DataField="ShutDown" HeaderText="Weekend/P.H"></asp:BoundColumn>

                <asp:BoundColumn DataField="Utilization" HeaderText="Utilization"></asp:BoundColumn>
            </Columns>
            </asp:DataGrid>
            </td>
        </tr>



        <tr id="trChart" style ="width: 100%">
                <td style = "padding: 0px 10px 10px 10px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="3" aria-hidden="False" aria-multiline="True" aria-multiselectable="True" aria-orientation="vertical" draggable="false" align="center"> 
                    
                    <asp:Chart ID="ProdChart"  runat="server" Width="1000px" Height="330px" BackColor="245, 245, 250" BorderlineColor="Black" ImageType="Bmp" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)">
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
            <td style = "padding: 0px 10px 10px 10px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" aria-hidden="False" aria-multiline="True" aria-multiselectable="True" aria-orientation="vertical" draggable="false" align="center"> 
                    <asp:Chart ID="ProdChart2"  runat="server" Width="1000px" Height="330px" BackColor="245, 245, 250" BorderlineColor="Black" ImageType="Bmp" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)">
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
