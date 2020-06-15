<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MouldingLabel.aspx.cs" Inherits="DashboardTTS.Webform.Moulding.MouldingLabel" MasterPageFile="~/Site.Master" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >
    <link href="../css/Dashboard.css" rel="stylesheet" />
    <div style="width: 100%; margin:auto">
        <div> 
            <div>   
                <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
                    <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                        <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                        <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Moulding Escutcheon Label" Font-Size="12" ForeColor="White"></Asp:label>
                    </td>
                </tr>
                                     
                <tr style="width:100%">
                     <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                        <asp:Label runat="server" Text="Shift :" Width="100px"></asp:Label>  
                    </td>
                     
                    <td style ="  width: 25%; border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; height: 50px; vertical-align:middle" >
                      <asp:DropDownList runat="server" ID="ddl_Shift" Width="100%">
                          <asp:ListItem Value="">ALL</asp:ListItem>
                          <asp:ListItem Value="Day">Day</asp:ListItem>
                          <asp:ListItem Value="Night">Night</asp:ListItem>
                      </asp:DropDownList>
                    </td>

                    <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                            <asp:Label runat="server" Text="User Type :" Width="100px"></asp:Label>  
                    </td>
                    <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; vertical-align:middle">
                       <asp:DropDownList runat="server" ID="ddl_UserType" Width="100%">
                           <asp:ListItem Value="">All</asp:ListItem>
                           <asp:ListItem Value="Usage" Selected="true">Usage</asp:ListItem>
                           <asp:ListItem Value="Reject">Reject</asp:ListItem>
                           <asp:ListItem Value="SerialNo">SerialNo</asp:ListItem>
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
                                <asp:DataGrid runat="server" Width ="100%" CssClass="table table-hover" ID ="dg_CheckList" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="false"  >
                                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                <EditItemStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundColumn DataField="SerialNo" HeaderText="SerialNo" ></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Day" HeaderText="Date"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Shift" HeaderText="Shift"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PartNumberAll" HeaderText="PartNumberAll"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="UserName" HeaderText="Operator"></asp:BoundColumn> 
                                        <asp:BoundColumn Visible="false" DataField="UsageQTYSum" HeaderText="Supervisor"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RejectQTYSum" HeaderText="RejectQTYSum" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SerialNoSum" HeaderText="SerialNoSum" Visible="false"></asp:BoundColumn>   
                                                                    

                                        <asp:BoundColumn Visible="false" DataField="UsageQTY08" HeaderText="08:00"></asp:BoundColumn> <%-- 8--%>
                                        <asp:BoundColumn Visible="false" DataField="UsageQTY09" HeaderText="09:00"></asp:BoundColumn>
                                        <asp:BoundColumn Visible="false" DataField="UsageQTY10" HeaderText="10:00"></asp:BoundColumn>
                                        <asp:BoundColumn Visible="false" DataField="UsageQTY11" HeaderText="11:00"></asp:BoundColumn>
                                        <asp:BoundColumn Visible="false" DataField="UsageQTY12" HeaderText="12:00"></asp:BoundColumn>
                                        <asp:BoundColumn Visible="false" DataField="UsageQTY01" HeaderText="01:00"></asp:BoundColumn>
                                        <asp:BoundColumn Visible="false" DataField="UsageQTY02" HeaderText="02:00"></asp:BoundColumn>
                                        <asp:BoundColumn Visible="false" DataField="UsageQTY03" HeaderText="03:00"></asp:BoundColumn>
                                        <asp:BoundColumn Visible="false" DataField="UsageQTY04" HeaderText="04:00"></asp:BoundColumn>
                                        <asp:BoundColumn Visible="false" DataField="UsageQTY05" HeaderText="05:00"></asp:BoundColumn>
                                        <asp:BoundColumn Visible="false" DataField="UsageQTY06" HeaderText="06:00"></asp:BoundColumn>
                                        <asp:BoundColumn Visible="false" DataField="UsageQTY07" HeaderText="07:00"></asp:BoundColumn><%-- 19--%>
                                        
                                        <asp:BoundColumn DataField="RejectQTY08" HeaderText="08:00" Visible="false"></asp:BoundColumn> <%--20--%>
                                        <asp:BoundColumn DataField="RejectQTY09" HeaderText="09:00" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RejectQTY10" HeaderText="10:00" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RejectQTY11" HeaderText="11:00" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RejectQTY12" HeaderText="12:00" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RejectQTY01" HeaderText="01:00" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RejectQTY02" HeaderText="02:00" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RejectQTY03" HeaderText="03:00" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RejectQTY04" HeaderText="04:00" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RejectQTY05" HeaderText="05:00" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RejectQTY06" HeaderText="06:00" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RejectQTY07" HeaderText="07:00" Visible="false"></asp:BoundColumn>  <%--31--%>                                     

                                        <asp:BoundColumn DataField="SerialNo08" HeaderText="08:00" Visible="false"></asp:BoundColumn>   <%--32--%>
                                        <asp:BoundColumn DataField="SerialNo09" HeaderText="09:00" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SerialNo10" HeaderText="10:00" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SerialNo11" HeaderText="11:00" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SerialNo12" HeaderText="12:00" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SerialNo01" HeaderText="01:00" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SerialNo02" HeaderText="02:00" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SerialNo03" HeaderText="03:00" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SerialNo04" HeaderText="04:00" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SerialNo05" HeaderText="05:00" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SerialNo06" HeaderText="06:00" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SerialNo07" HeaderText="07:00" Visible="false"></asp:BoundColumn> <%--43--%>
                                        <asp:BoundColumn DataField="SerialNoEnd" HeaderText="SerialNoEnd" Visible="false"></asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>                    
                    </table>                      
                </div>	
            </div>
        </div>                        
  
 </asp:Content>


