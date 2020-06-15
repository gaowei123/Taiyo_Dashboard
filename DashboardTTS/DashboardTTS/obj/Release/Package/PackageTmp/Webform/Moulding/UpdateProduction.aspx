<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="UpdateProduction.aspx.cs" Inherits="DashboardTTS.Webform.Molding.UpdateProduction" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" >
    <link href="../css/Dashboard.css" rel="stylesheet" />
    <div style="width: 90%; margin:auto">
        <div> 
            <div>   
                <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
                    <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                        <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Update Moulding Production" Font-Size="12" ForeColor="White"></Asp:label>
                    </td>
                </tr>
                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                        Machine No :
                    </td>
                    <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px;">
                        <asp:DropDownList ID="ddlMachineID"  runat="server" Width="100%" Height="23px"  >
                                <asp:ListItem Value="">ALL</asp:ListItem>
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
                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                            Part No :
                        </td>                                              
                        <td style ="  width: 25%; border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; height: 50px; vertical-align:middle" >
                                <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_PartNo" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                    </td>                                                      
                    <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MeS'; height: 50px;">
                    </td>
                </tr>            
                <tr style="width:100%">
                     <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                        Shift :
                    </td>                    
                    <td style ="  width: 25%; border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; height: 50px; vertical-align:middle" >
                      <asp:DropDownList runat="server" ID="ddl_Shift" Width="100%">
                          <asp:ListItem Value="">ALL</asp:ListItem>
                          <asp:ListItem Value="Day">Day</asp:ListItem>
                          <asp:ListItem Value="Night">Night</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                    <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                        Model :
                    </td>
                    <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; vertical-align:middle">
                        <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_module" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                    </td>       
                    <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
                        <asp:Button runat="server" ID="btn_Update" Text="Update" CssClass="btn-primary"  Height="30px"  Width="50%" OnClick="btn_Update_Click" />
                    </td>
                </tr>                              
                    <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                        From :
                    </td>
                    <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                        <igsch:WebDateChooser ID="infDchFrom" runat="server" Height="23px"   Width="100%" Value="" >
                        </igsch:WebDateChooser>
                    </td>
                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                            To :
                        </td>
                    <td style =" border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                            <igsch:WebDateChooser ID="infDchTo" runat="server"  Height="23px"  Width="100%">
                        </igsch:WebDateChooser></td>                                                      
                    <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
                            <asp:Button ID="btn_generate" runat="server" Text="Generate" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Height="30px"  Width="50%" OnClick="btn_generate_Click"/>                                           
                </td>
                </tr>                                
                <tr style ="width: 100%">
                    <td style = "padding: 25px 25px 25px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="5"> 
                        <asp:Label ID="lblResult" runat="server"></asp:Label> 
                    </td>
                </tr>                                                      
                        <tr id="trChart" style ="width: 100%">
                            <td style = "padding: 25px 25px 25px 25px; border: 1px solid #CCCCCC; width:100%;  font-family: 'Arial Unicode MS'; height: 50px;" colspan="5" aria-hidden="False" aria-multiline="True" aria-multiselectable="True" aria-orientation="vertical" draggable="false"> 
                                <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dg_Report" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="false" >
                                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                <EditItemStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" />
                                <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundColumn DataField="ID" HeaderText="ID"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Day" HeaderText="Date"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Shift" HeaderText="Shift"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MachineID" HeaderText="MachineID"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Model" HeaderText="Model"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Type" HeaderText="Type"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PartNumberAll" HeaderText="PartNumberAll" Visible="true"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PartNumber" HeaderText="PartNumber" Visible="false"></asp:BoundColumn>
                                        <asp:ButtonColumn CommandName="LinkPartNumber" DataTextField="PartNumber" HeaderText="PartNumber" ButtonType="LinkButton"></asp:ButtonColumn>
                                        <asp:BoundColumn DataField="JigNo" HeaderText="JigNo"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Status" HeaderText="Status"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="StartTime" HeaderText="StartTime" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="TargetQty" HeaderText="Plan Qty"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Accumulate" HeaderText="Accumulate"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="NeedProductionTime" HeaderText="Remains Time"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Output" HeaderText="Output(Shots)"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="OK" HeaderText="OK"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="NG" HeaderText="NG" Visible="false"></asp:BoundColumn>
                                        <asp:ButtonColumn DataTextField="NG" CommandName="Link" HeaderText="NG"></asp:ButtonColumn>
                                        <asp:BoundColumn DataField="QCNGQTY" HeaderText="IPQC Rej" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>  
                                        <asp:BoundColumn DataField="RejRate" HeaderText="RejRate"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Time" HeaderText="Run Time"></asp:BoundColumn>                                        
                                        <asp:BoundColumn DataField="cavityCount" HeaderText="SetupRej" Visible="false"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="JigNo" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID ="txt_JigNo" Height="20px" ></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="OutPut" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID ="txt_OutPut" Height="20px" ></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="OK" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID ="txt_OK" Height="20px" ></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="NG" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID ="txt_NG" Height="20px" ></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Update" Visible="false">
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="btn_Submit" CssClass="btn-success" Text="√" Height="20px" Width="40px" CommandName="Update" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Delete" Visible="false">
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="btn_Delete" CssClass="btn-success" Text="X" Height="20px" Width="40px" CommandName="Delete" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    <%--<asp:BoundColumn DataField="MRP" HeaderText="PQM"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="UserName" HeaderText="Operator"></asp:BoundColumn>--%>
                                   </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </div>	
            </div>
        </div>
    
 </asp:Content>
