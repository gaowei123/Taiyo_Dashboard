﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MouldingMaintain.aspx.cs" MasterPageFile="~/Site.Master"  Inherits="DashboardTTS.Webform.Moulding.MouldingMaintainence_His" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
    <link href="../Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />  
    <link href="/Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />
     
    <div style="width: 100%;align-items:center;margin:auto">
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Machine Checklist" Font-Size="12" ForeColor="White"/>
                </td>
            </tr>

            <tr>
                 <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                        <asp:Label runat="server" Text="Machine No :" Width="100px"></asp:Label>  
                    </td>
                     
                    <td style ="  width: 25%; border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; height: 50px; vertical-align:middle" >
                      <asp:DropDownList runat="server" ID="ddl_MachineID" Width="100%">
                          <asp:ListItem Value="">ALL</asp:ListItem>
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
                    <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                            <asp:Label runat="server" Text="Period Check :" Width="100px"></asp:Label>  
                    </td>
                    <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; vertical-align:middle">
                          <asp:DropDownList runat="server" ID="ddl_Period" Width="100%">
                              <asp:ListItem Value="">All</asp:ListItem>
                              <asp:ListItem Value="Daily">Daily</asp:ListItem>
                              <asp:ListItem Value="Weekly">Weekly</asp:ListItem>
                              <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                          </asp:DropDownList>
                    </td>
       
                    <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
                        <asp:Button ID="btn_Maintain" runat="server" Text="Checklist" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-primary" Height="30px" Width="50%" OnClick="btn_Maintain_Click"/> 
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_Verify" runat="server" Text="Verify" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-danger" Height="30px" Width="30%" OnClick="btn_Verify_Click"/> 
                    </td>
            </tr>

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="From :" Width="100px"></asp:Label>  
                </td>
                <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                    <igsch:WebDateChooser ID="infDchFrom" runat="server" Height="23px"   Width="100%" Value="" > </igsch:WebDateChooser>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="To :"></asp:Label> 
                </td>
                <td style =" border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                    <igsch:WebDateChooser ID="infDchTo" runat="server"  Height="23px"  Width="100%"> </igsch:WebDateChooser>
                </td>
                                                      
                <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Button ID="btn_generate" runat="server" Text="Generate" Width="50%" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Height="30px"  OnClick="btn_generate_Click"/>                                           
                </td>
            </tr>    

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="5"> 
                    <asp:Label ID="lblResult" runat="server"></asp:Label> 
                </td>
            </tr>


            <tr style="width:100%">    
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="5" >
                    <asp:DataGrid runat="server" CssClass="table table-hover" ID ="dg_Maint" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" Width="100%"  HorizontalAlign="Center" AutoGenerateColumns="False" >
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775"  />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="left" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                         <HeaderStyle HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundColumn DataField="MachineID" HeaderText="Machine ID"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CheckPeriod" HeaderText="Check Period"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CheckItem" HeaderText="Check Item"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MaintainenceDescription" HeaderText="Description of Maintainence"></asp:BoundColumn>
                            <asp:BoundColumn DataField="InspectionDescription" HeaderText="Description of Inspection"></asp:BoundColumn>
                            
                            <asp:BoundColumn DataField="CheckResult" HeaderText="Check Result"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SpareParts" HeaderText="Remarks"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CheckDate" HeaderText="Check Date"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CheckBy" HeaderText="Perform By"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VerifyBy" HeaderText="Verify By"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
                        
         

        </table>
    </div>
</asp:Content>