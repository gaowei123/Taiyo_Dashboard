<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LaserPQCTotalReport.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.Reports.LaserPQCTotalReport" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >

    <script type="text/javascript" src="../../javascript/jquery.js"></script>
    <script type="text/javascript" src="../../javascript/jquery-migrate-1.2.1.js"></script>
    <script type="text/javascript" src="../../javascript/TableFreeze.js"></script>
   

    <div style="width: auto; height: 257px; align-items:center; margin:auto">
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
            
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="9"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="" Font-Size="12" ForeColor="White" />
                </td>
            </tr>

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 150px;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="Job No :" Width="100%" />
                </td>
                <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 250px; height: 50px;">
                    <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txtJobNo" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 150px; font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="Part No:" Width="100%" />
                </td>                       
                <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; height: 50px; Width:250px; vertical-align:middle" >
                    <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txtPartNumber" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                </td>    
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 150px; font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="Model :" Width="100%"></asp:Label> 
                </td>
                <td style =" border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 250px; height: 50px; ">
                    <asp:DropDownList runat="server" ID="ddlModel" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"></asp:DropDownList>
                </td>

                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 150px; font-family: 'Arial Unicode MS'; height: 50px;"> 
                    Total Rej Cost:
                </td>
                <td style =" border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 250px; height: 50px; ">
                    <asp:Label runat="server" ID="lbCost" Width="100%"></asp:Label>
                </td>
                <td style = "padding: 10px; border: 1px solid #CCCCCC; width: auto; font-family: 'Arial Unicode MeS'; height: 50px;" colspan="1">

                </td>
            </tr>

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 150px; font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="Type :" Width="100%"></asp:Label> 
                </td>
                <td style =" border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 250px; height: 50px; ">
                    <asp:DropDownList runat="server" ID="ddlType" Height="23px" Width="100%"></asp:DropDownList>
                </td>       
                
                <td style = "padding: 10px 10px 10px 25px; width: 150px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="Description :" Width="100%" />
                </td>
                <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 250px; height: 50px; ">
                    <asp:DropDownList runat="server" ID="ddlDescription" Height="23px" Width="100%">
                        <asp:ListItem Text="All" Value=""></asp:ListItem>
                        <asp:ListItem Text="BUTTON" Value="BUTTON"></asp:ListItem>
                        <asp:ListItem Text="LENS" Value="LENS"></asp:ListItem>
                        <asp:ListItem Text="BEZEL" Value="BEZEL"></asp:ListItem>
                        <asp:ListItem Text="PANEL" Value="PANEL"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style = "padding: 10px 10px 10px 25px; width: 150px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="Date From:" Width="100%" />
                </td>
                <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 250px; height: 50px; ">
                    <igsch:WebDateChooser ID="infDchFrom" runat="server" Height="23px"  Width="100%" Value=""/>
                </td>
                <td style = "padding: 10px 10px 10px 25px; width: 150px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="Date To:" Width="100%" />
                </td>
                <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 250px; height: 50px; ">
                    <igsch:WebDateChooser ID="infDchTo" runat="server" Height="23px"  Width="100%" Value=""/>
                </td>
               
                <td style = "padding: 10px; border: 1px solid #CCCCCC; width: auto; font-family: 'Arial Unicode MS'; height: 50px;" colspan="1">
                   
                </td>
            </tr>

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 150px; font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="Supplier :" Width="100%"></asp:Label>
                </td>
                <td style =" border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 250px; height: 50px; ">
                    <asp:DropDownList runat="server" ID="ddlSupplier" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"></asp:DropDownList>
                </td>             
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 150px; font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="Coating :" Width="100%"></asp:Label> 
                </td>
                <td style =" border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 250px; height: 50px; ">
                    <asp:DropDownList runat="server" ID="ddlCoating" Height="23px" Width="100%">
                        <asp:listitem Text="All" Value=""></asp:listitem>
                        <asp:listitem Text="One Coat" Value="One Coat"></asp:listitem>
                        <asp:listitem Text="Two Coat" Value="Two Coat"></asp:listitem>
                        <asp:listitem Text="Three Coat" Value="Three Coat"></asp:listitem>
                        <asp:listitem Text="Print Coat" Value="Print Coat"></asp:listitem>
                    </asp:DropDownList>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 150px; font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="Color :" Width="100%"></asp:Label> 
                </td>
                <td style =" border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 250px; height: 50px; ">
                    <asp:DropDownList runat="server" ID="ddlColor" Height="23px"  Width="100%"></asp:DropDownList>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 150px; font-family: 'Arial Unicode MS'; height: 50px;"> 
                    
                </td>
                <td style =" border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 250px; height: 50px; ">

                </td>
                <td style = "padding: 10px; border: 1px solid #CCCCCC; width: auto; font-family: 'Arial Unicode MS'; height: 50px;" colspan="3">
                     <asp:Button ID="BtnGenerate" runat="server" Text="Generate"  BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Height="30px" OnClick="BtnGenerate_Click" />
                </td>
            </tr>

           
                                          
           
                                                      
            <tr id="trChart" style ="width: 100%">
                <td style = " border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px; " colspan="9"> 
                    <h3><asp:Label ID="lblResult" runat="server" CssClass="col-xs-11"></asp:Label></h3>   
                    <div style="overflow: scroll; width:1840px;height:560px;">
                        <asp:DataGrid runat="server" Width ="100%" ID ="dgPQCLaserTotalReport" CellPadding="10" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px" CellSpacing="2" AutoGenerateColumns="false" style="line-height:15px" >
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <EditItemStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="left" VerticalAlign="Middle"/>
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="left" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundColumn DataField="jobNumber" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SN" HeaderText="SN"></asp:BoundColumn>
                                <asp:BoundColumn DataField="model" HeaderText="Model"></asp:BoundColumn>
                                <asp:ButtonColumn  DataTextField="LotNo" HeaderText="Lot No" CommandName="linkBuyoff"></asp:ButtonColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                   
                </td>
            </tr>


        </table>
    </div>    
    
     <script type='text/javascript'>
	    $(document).ready(function(){
	 
	        //para1: 固定title的行数
	        //para2: 固定底部的行数
            //para3: 固定左侧的行数
	        $("#MainContent_dgPQCLaserTotalReport").FrozenTable(1, 0, 5);
		

	    });
    </script>
                        
    </div>
                        
</asp:Content>