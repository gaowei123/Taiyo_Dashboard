<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RejDetail.aspx.cs" Inherits="DashboardTTS.Webform.Moulding.RejDetail" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >
    <link href="../css/Dashboard.css" rel="stylesheet" />
    <div style="width: 85%; margin:auto">
        <div> 
            <div>   
                <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
                    <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                        <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" />
                        <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Moulding All Information" Font-Size="12" ForeColor="White"></Asp:label>
                    </td>
                </tr>
                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                        <asp:Label runat="server" Text="Machine No :" Width="100px"></asp:Label>  
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
                                <asp:ListItem Value="9">No.9</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                        <asp:Label runat="server" Text="Part No :" Width="100px"></asp:Label> 

                        </td>
                                              
                        <td style ="  width: 25%; border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; height: 50px; vertical-align:middle" >
                        <asp:DropDownList ID="ddlPartNo"  runat="server" Width="100%" Height="23px"  >
                                <asp:ListItem Value="">ALL</asp:ListItem>
                                <asp:ListItem Value="6910-6894 T2">6910-6894 T2</asp:ListItem>
                                <asp:ListItem Value="6910-6453">6910-6453</asp:ListItem>
                                <asp:ListItem Value="6910-6355">6910-6355</asp:ListItem>
                                <asp:ListItem Value="6910-6356">6910-6356</asp:ListItem>
                                <asp:ListItem Value="6910-6357">6910-6357</asp:ListItem>
                                <asp:ListItem Value="6910-6454 T2">6910-6454 T2</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                                                      
                    <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MeS'; height: 50px;">
                    </td>
                </tr>
            
             

                <tr style="width:100%">
                    <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                           <asp:Label runat="server" Text="Reject Type:" Width="100px"></asp:Label>  
                            
                    </td>
                    <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; vertical-align:middle">
                         <asp:DropDownList runat="server" ID="ddl_RejType" Width="100%"></asp:DropDownList>

                       
                    </td>
                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                         <asp:Label runat="server" Text="Model:" Width="100px"></asp:Label>  
                        </td>

                    <td style ="  width: 25%; border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; height: 50px; vertical-align:middle" >
                        <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_module" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                    </td>
                                                      
                    <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
                    
                    </td>
                </tr>
                                <tr style="width:100%">
                    <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                           <asp:Label runat="server" Text="Type:" Width="100px"></asp:Label>  
                            
                    </td>
                    <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; vertical-align:middle">
                         <asp:DropDownList runat="server" ID="ddl_Type" Width="100%"></asp:DropDownList>

                       
                    </td>
                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                         <asp:Label runat="server" Text="Shift:" Width="100px"></asp:Label>  
                        </td>

                    <td style ="  width: 25%; border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; height: 50px; vertical-align:middle" >
                        <asp:DropDownList runat="server" ID="ddl_Shift" Width="100%" ></asp:DropDownList>
                    </td>
                                                      
                    <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
                    
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
                            <asp:Button ID="btn_generate" runat="server" Text="Generate" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Height="30px"  OnClick="btn_generate_Click"/>                                           
                </td>
                </tr>    
                            
                <tr style ="width: 100%">
                    <td style = "padding: 25px 25px 25px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="5"> 
                        <asp:Label ID="lblResult" runat="server"></asp:Label> 
                    </td>
                </tr>
                                                      
                        <tr id="trChart" style ="width: 100%">
                            <td style = "padding: 25px 25px 25px 25px; border: 1px solid #CCCCCC; width:100%;  font-family: 'Arial Unicode MS'; height: 50px;" colspan="5" aria-hidden="False" aria-multiline="True" aria-multiselectable="True" aria-orientation="vertical" draggable="false"> 
                                <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dg_RejDetail" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="false" >
                                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                <EditItemStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" />
                                <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                                <HeaderStyle HorizontalAlign="Left" />
                                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundColumn DataField="ID" HeaderText="ID"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Day" HeaderText="Date"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Shift" HeaderText="Shift"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MachineID" HeaderText="MachineID"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="jigNo" HeaderText="jigNo"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Type" HeaderText="Type"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Model" HeaderText="Model" Visible ="false"></asp:BoundColumn>                                     
                                        <asp:ButtonColumn CommandName="LinkModel" DataTextField="Model" HeaderText="Model" ButtonType="LinkButton"></asp:ButtonColumn>
                                        <asp:BoundColumn DataField="PartNumberAll" HeaderText="PartNumberAll" Visible ="false"></asp:BoundColumn>
                                        <asp:ButtonColumn CommandName="LinkPartNumberAll" DataTextField="PartNumberAll" HeaderText="PartNumberAll" ButtonType="LinkButton"></asp:ButtonColumn>                                        
                                        <asp:BoundColumn DataField="PartNumber" HeaderText="PartNumber"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="rejectQty" HeaderText="RejQty"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="rejectCost" HeaderText="RejectCost"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="defectCode" HeaderText="RejType"></asp:BoundColumn>    
                                        <asp:BoundColumn DataField="OPID" HeaderText="OPID"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ChkerID" HeaderText="ChkerID"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="OutPutQTY" HeaderText="OutPutQTY"></asp:BoundColumn>                                             
                                        <asp:BoundColumn DataField="RejectionRate" HeaderText="RejectionRate"></asp:BoundColumn>                             
                                    </Columns>
                                </asp:DataGrid> 
                            </td>
                        </tr>                    
                    </table>                      
                </div>	
            </div>
        </div>                        
 </asp:Content>

