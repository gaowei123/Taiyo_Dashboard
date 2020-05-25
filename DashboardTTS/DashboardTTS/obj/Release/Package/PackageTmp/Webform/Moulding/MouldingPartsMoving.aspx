<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MouldingPartsMoving.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.Moulding.MouldingPartsMoving" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >
    <link href="../css/Dashboard.css" rel="stylesheet" />
    <div style="width: auto; height: 257px; align-items:center;margin:auto">
        <div> 
            <div>   
                <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
                    <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                        <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Moulding Parts Movement" Font-Size="12" ForeColor="White"></Asp:label>
                    </td>
                </tr>
            
             

                <tr style="width:100%">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                        <asp:Label runat="server" Text="Transfer To :" Width="100px"></asp:Label>  
                    </td>
                    <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px;">
                                                             
                        <asp:DropDownList ID="ddlTransferTo"  runat="server" Width="100%" Height="23px"  >
                                <asp:ListItem Value="">ALL</asp:ListItem>
                                <asp:ListItem Value="1">Painting Dept</asp:ListItem>
                                <asp:ListItem Value="2">Silkprint</asp:ListItem>
                                <asp:ListItem Value="3">Summer Weli</asp:ListItem>
                                <asp:ListItem Value="4">store</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                     <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                        <asp:Label runat="server" Text="Part No :" Width="100px"></asp:Label> 

                    </td>
                     
                    <td style ="  width: 25%; border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; height: 50px" >
                        <asp:DropDownList ID="ddlPartNo"  runat="server" Width="100%" Height="23px"  >
                                <asp:ListItem Value="">ALL</asp:ListItem>
                                <asp:ListItem Value="1">Painting Dept</asp:ListItem>
                                <asp:ListItem Value="2">Silkprint</asp:ListItem>
                                <asp:ListItem Value="3">Summer Weli</asp:ListItem>
                                <asp:ListItem Value="4">store</asp:ListItem>
                        </asp:DropDownList>
                    </td>


       
                    <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
                          <asp:Button ID="btn_add" runat="server" Text="Add" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Height="30px"  OnClick="btn_add_Click" Width="88px"/>                                           
                    </td>
                </tr>
                              
                    <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                            <asp:Label runat="server" Text="From :" Width="100px"></asp:Label>  
                    </td>
                    <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                        <igsch:WebDateChooser ID="infDchFrom" runat="server" Height="23px"   Width="100%" Value="" >
                        </igsch:WebDateChooser>
                    </td>
                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                        <asp:Label runat="server" Text="To :"></asp:Label> 

                        </td>
                    <td style =" border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                            <igsch:WebDateChooser ID="infDchTo" runat="server"  Height="23px"  Width="100%">
                        </igsch:WebDateChooser></td>
                                                      
                    <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
                            <asp:Button ID="btn_generate" runat="server" Text="Generate" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Height="30px"  OnClick="btn_generate_Click" Width="88px"/>                                           
                </td>
                </tr> 
                            
                <tr style ="width: 100%">
                    <td style = "padding: 25px 25px 25px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="5"> 
                        <asp:Label ID="lblResult" runat="server"></asp:Label> 
                    </td>
                </tr>                                                                         
                    <tr style ="width: 100%">   
                    <td style = "padding: 25px 25px 25px 25px; border: 1px solid #CCCCCC; width:100%;  font-family: 'Arial Unicode MS'; height: 50px;" colspan="5" aria-hidden="False" aria-multiline="True" aria-multiselectable="True" aria-orientation="vertical" draggable="false">
                        <asp:DataGrid runat="server" CssClass="table table-hover" ID ="dg_BOMList" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" Width="100%"  HorizontalAlign="Center" AutoGenerateColumns="False" >
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775"  />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="left" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                         <HeaderStyle HorizontalAlign="Left" />
                        <Columns>  
                                        <asp:TemplateColumn HeaderText="Delete" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button runat="server" Text="×" Height="20px" CommandName="Delete" CssClass="btn-danger" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Material_Part" HeaderText="PartNumber"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Transfer_To" HeaderText="Transfer_To"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Transfer_Date" HeaderText="Transfer_Date"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Quantity" HeaderText="Quantity"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Opr_ID" HeaderText="Opr_ID"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Production_Date" HeaderText="Production_Date"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Annealing_Process" HeaderText="Annealing_Process"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Annealing_Date_From" HeaderText="Annealing_Date_From" ></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Annealing_Date_To" HeaderText="Annealing_Date_To"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Annealing_Temperature" HeaderText="Annealing_Temperature"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Update_User" HeaderText="Update_User"></asp:BoundColumn>                            
                        </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>                                       
                    </table>                      
                </div>	
            </div>
        </div>                        
 </asp:Content>
