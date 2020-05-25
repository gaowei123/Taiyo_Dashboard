<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MouldingPartsInventory.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.Moulding.MouldingPartsInventory" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>


<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >

    <script>
        function ConfirmDelete(PartNumberAll, CommandName)
        {
            //alert('in func');
            //alert(PartNumberAll + '    ' + CommandName);
            try
            {
                if (confirm('Are you sure to delete this part?') == true)
                {
                    //alert("./BOMList.aspx" + "?CommandName=" + CommandName + "&PartNumberAll=" + PartNumberAll);
                    window.location.href = "./MouldingPartsInventory.aspx" + "?CommandName=" + CommandName + "&PartNumberAll=" + PartNumberAll;
                }
            }
            catch (e)
            {
                //alert('exception' + e.message);
            }
            //alert('end func');
        }
    </script>


<div style="width:auto; align-items:center;margin:auto">

        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                        <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                        <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Moulding Materials Inventory" Font-Size="12" ForeColor="White"/>
                    </td>
                </tr>
                <tr style="width:100%">
                    <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                            <asp:Label runat="server" Text="From :" Width="100px"></asp:Label>  
                    </td>
                    <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                        <igsch:WebDateChooser ID="infDchFrom" runat="server" Height="23px"   Width="100%" Value="" >  </igsch:WebDateChooser>
                    </td>
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                        <asp:Label runat="server" Text="To :"></asp:Label> 
                     </td>
                    <td style =" border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                        <igsch:WebDateChooser ID="infDchTo" runat="server"  Height="23px"  Width="100%">  </igsch:WebDateChooser>
                    </td> 
                     <td style =" border: 1px solid #CCCCCC; padding: 10px 10px 10px 10px; width: 25%; height: 50px; ">
                        <asp:Button  runat="server" ID="btn_Load" Text="Load" Width="30%" Height="30px"  OnClick="btn_Load_Click" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-primary" />
                        &nbsp;
                        <asp:Button  runat="server" ID="btn_Unload" Text="Unload" Width="30%" Height="30px"  OnClick="btn_Unload_Click" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-primary" />
                        &nbsp;
                        <asp:Button  runat="server" ID="btn_Return" Text="Return" Width="30%" Height="30px"  OnClick="btn_Return_Click" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-primary" />
                    </td>
                </tr>
                              
                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                        <asp:Label runat="server" Text="Material No :" Width="100%"></asp:Label>  
                    </td>       
                    <td style = "padding: 10px 35px 10px 10px; border: 1px solid #CCCCCC; width: 25%;   height: 50px;">
                        <asp:TextBox runat="server" ID="txt_MaterialPart"  Width="93%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                    </td>
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                        <asp:Label runat="server" Text="Detail :" Width="100px"></asp:Label>  
                    </td>
                    <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px;">                                                             
                        <asp:DropDownList ID="ddl_Detail"  runat="server" Width="100%" Height="23px"  >
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style =" border: 1px solid #CCCCCC; padding: 10px 10px 10px 10px; width: 25%; height: 50px; " align='Center' >
                         <asp:Button runat="server" ID="btn_History" Text="History"  Width="45%" OnClick="btn_HistorySearch_Click" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-info" Height="30px" />    
                        &nbsp;&nbsp;&nbsp; <asp:Button runat="server" ID="btn_search" Text="Inventory"  Width="45%" OnClick="btn_search_Click" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Height="30px" />
                    </td>
                </tr>  

                <tr>
                    <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                            <asp:Label runat="server" Text="Event :" Width="100px"></asp:Label>  
                    </td>
                    <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                        <asp:DropDownList ID="ddlEventList"  runat="server" Width="100%" Height="23px"  >
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="Load">Load</asp:ListItem>
                                <asp:ListItem Value="Unload">Unload</asp:ListItem>
                                <asp:ListItem Value="Return">Return</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                   <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px; "colspan="2"> 
                            
                    </td>
                   <td style =" border: 1px solid #CCCCCC; padding: 10px 10px 10px 10px; width: 25%; height: 50px; " align='Center' >
                          </td>
                </tr>

                <tr>
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width:15%;  font-family: 'Arial Unicode MS'; height: 50px;" colspan="5"> 
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
                            <asp:BoundColumn DataField="Material_No" HeaderText="Material No" Visible="false"></asp:BoundColumn>
                            <asp:ButtonColumn CommandName="LinkMaterial_No" DataTextField="Material_No" HeaderText="Material_No" ButtonType="LinkButton"></asp:ButtonColumn>
                            <asp:BoundColumn DataField="ResinType" HeaderText="Resin Type" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="Material_LotNo" HeaderText="Material LotNo"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Supplier" HeaderText="Supplier" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Inventory_Weight" HeaderText="Inventory Weight"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Transaction_Weight" HeaderText="Transaction Weight"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UnitCost" HeaderText="UnitPrice Per Kgs"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TotalCost" HeaderText="Amounts"></asp:BoundColumn>


                            
                            <asp:BoundColumn DataField="User_Name" HeaderText="User Name"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Last_Event" HeaderText="Last Event"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MachineID" HeaderText="Last Unload Machine/Loan"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Load_Time" HeaderText="Load Time"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Updated_Time" HeaderText="Updated Time"></asp:BoundColumn>

                            <asp:TemplateColumn HeaderText="Delete"  ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Button runat="server" Text="×" CssClass="btn-danger" Width="30px"  Height="23px" CommandName="Delete" />
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            
                        </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>
        </table>    
</div>           
</asp:Content>