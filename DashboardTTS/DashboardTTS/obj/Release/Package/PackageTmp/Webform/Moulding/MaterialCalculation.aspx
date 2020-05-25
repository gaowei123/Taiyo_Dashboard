<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MaterialCalculation.aspx.cs" Inherits="DashboardTTS.Webform.Molding.MaterialCalculation" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
    <link href="../Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />  
    <link href="/Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />

     <script src="../js/Dashboard.js"> </script>

    
     
     <div style=" width: 100%; height: 257px; margin: auto; top: 0px; left: 0px;">
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lb_Header"   runat="server" Font-Names="Arial Unicode MS"  Text="Moulding Production Calculator" Font-Size="12" ForeColor="White"/>
                </td>
            </tr>

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width:100%;  font-family: 'Arial Unicode MS'; text-align:center;" colspan="5"> 
                    <table  style ="width: 100%">
                        <tr  style ="width: 100%">
                            <td  style ="width:33.33%; border: 1px solid #CCCCCC;border-color:black;padding:10px,20px,10px,20px;">
                                <table  style ="width: 100%">
                                    <%--Calculation For Part Count Per Hour--%>
                                    <tr style ="width: 100%">
                                        <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; text-align:center; height: 50px;" colspan="2" > 
                                            <asp:Label ID="Label2" runat="server" Text="Calculation For Part Count Per Hour" Font-Bold="true" Font-Size="16px" ></asp:Label> 
                                        </td>
                                    </tr>
                                    <tr style="width:100%;">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width:60%;   font-family: 'Arial Unicode MS'; height: 50px;" align="left">
                                            <asp:Label runat="server" Text="Cycle time (seconds) :" Width="100%" style="margin-bottom: 0px"></asp:Label>
                                        </td>
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 40%;   font-family: 'Arial Unicode MS'; height: 50px;">
                                            <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_cycletime03" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                                        </td>                             
                                    </tr>
                                    <tr style="width:100%;">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width:60%;   font-family: 'Arial Unicode MS'; height: 50px;" align="left">
                                            <asp:Label runat="server" Text="Mold cavitation :" Width="100%" style="margin-bottom: 0px"></asp:Label>
                                        </td>
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 40%;   font-family: 'Arial Unicode MS'; height: 50px;">
                                            <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_mold03" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style ="width:100%;">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                                            
                                        </td>
                                    </tr>
                                    <tr style ="width:100%;">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                                            
                                        </td>
                                    </tr>
                                    <tr style ="width:100%;">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                                            
                                        </td>
                                    </tr>
                                    <tr style ="width:100%;">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" align="left">
                                            <asp:Label ID="lblresult03" runat="server" ForeColor="Red"></asp:Label> 
                                        </td>
                                    </tr>
                                    <tr style="width:100%;">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 100%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" align="center">
                                            <asp:Button runat="server" ID="Button3" CssClass="btn-success" Height="30px" Text="Generate" Width="30%" OnClick="btn_Generate03_Click" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button runat="server" ID="Button4" CssClass="btn-danger" Height="30px" Text="Reset" Width="30%" OnClick="btn_Clean03_Click"/>
                                        </td>
                                    </tr>
                                    <%--Calculation For Part Count Per Hour--%>
                                </table>
                            </td>
                            <td style ="width:33.33%;border: 1px solid #CCCCCC;border-color:black;padding:10px,20px,10px,20px;">
                                <table  style ="width: 100%">
                                    <%--Calculation For Number Of Hours Requied To Make X Parts--%>
                                    <tr style ="width: 100%">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; text-align:center; height: 50px;" colspan="2"> 
                                            <asp:Label ID="Label5" runat="server" Text="Calculation For Number Of Hours Requied To Make X Parts" style="line-height:1" Font-Bold="true" Font-Size="16px" ></asp:Label> 
                                        </td>
                                    </tr>
                                    <tr style="width:100%;">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width:60%;   font-family: 'Arial Unicode MS'; height: 50px;" align="left">
                                            <asp:Label runat="server" Text="Quantity of parts required :" Width="100%" style="margin-bottom: 0px"></asp:Label>
                                        </td>
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 40%;   font-family: 'Arial Unicode MS'; height: 50px;">
                                            <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_Quantity" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                                        </td>                             
                                    </tr>
                                    <tr style="width:100%;">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width:60%;   font-family: 'Arial Unicode MS'; height: 50px;" align="left">
                                            <asp:Label runat="server" Text="Cycle Time (seconds) :" Width="100%" style="margin-bottom: 0px"></asp:Label>
                                        </td>
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 40%;   font-family: 'Arial Unicode MS'; height: 50px;">
                                            <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_cycletime" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                                        </td>                             
                                    </tr>
                                    <tr style="width:100%;">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width:60%;   font-family: 'Arial Unicode MS'; height: 50px;" align="left">
                                            <asp:Label runat="server" Text="Mold cavitation :" Width="100%" style="margin-bottom: 0px"></asp:Label>
                                        </td>
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 40%;   font-family: 'Arial Unicode MS'; height: 50px;">
                                            <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_Mold" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                                        </td>                             
                                    </tr>
                                     <tr style ="width:100%;">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                                            
                                        </td>
                                    </tr>
                                    <tr style ="width:100%;">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                                            
                                        </td>
                                    </tr>
                                    <tr style ="width:100%;"> 
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" align="left">
                                            <asp:Label ID="lblResult" runat="server"  ForeColor="Red"></asp:Label> 
                                        </td>
                                    </tr>
                                    <tr style="width:100%;">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 100%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" align="center">
                                            <asp:Button runat="server" ID="btn_Generate" CssClass="btn-success" Height="30px" Text="Generate" Width="30%" OnClick="btn_Generate01_Click" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button runat="server" ID="btn_Clean" CssClass="btn-danger" Height="30px" Text="Reset" Width="30%" OnClick="btn_Clean01_Click"/>
                                        </td>
              
                                    </tr>
                                    <%--Calculation For Number Of Hours Requied To Make X Parts--%>
                                </table>
                            </td>
                            <td style ="width:33.33%;border: 1px solid #CCCCCC;border-color:black;">
                                <table  style ="width: 100%">
                                    <%--Calculation For Resin Material Requirement--%>
                                    <tr style ="width: 100%">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; text-align:center; height: 50px;" colspan="2"> 
                                            <asp:Label ID="Label1" runat="server" Text="Calculation For Resin Material Requirement" Font-Bold="true" style="line-height:1" Font-Size="16px" ></asp:Label> 
                                        </td>
                                    </tr>
                                    <tr style="width:100%;">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width:60%;   font-family: 'Arial Unicode MS'; height: 50px;" align="left">
                                            <asp:Label runat="server" Text="Preferred unit of measurement :" Width="100%" style="margin-bottom: 0px"></asp:Label>
                                        </td>
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 40%;   font-family: 'Arial Unicode MS'; height: 50px;">
                                            <asp:Label runat="server" Text="Kilograms" Width="100%" style="margin-bottom: 0px"></asp:Label>
                                        </td>                             
                                    </tr>
                                    <tr style="width:100%;">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width:60%;   font-family: 'Arial Unicode MS'; height: 50px;" align="left">
                                            <asp:Label runat="server" Text="Quantity of parts required :" Width="100%" style="margin-bottom: 0px"></asp:Label>
                                        </td>
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 40%;   font-family: 'Arial Unicode MS'; height: 50px;">
                                            <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_Quantity02" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="width:100%;">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width:60%;   font-family: 'Arial Unicode MS'; height: 50px;" align="left">
                                            <asp:Label runat="server" Text="Part weight(grams) :" Width="100%" style="margin-bottom: 0px"></asp:Label>
                                        </td>
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 40%;   font-family: 'Arial Unicode MS'; height: 50px;">
                                            <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_weight" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                                        </td>                             
                                    </tr>
                                    <tr style="width:100%;">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width:60%;   font-family: 'Arial Unicode MS'; height: 50px;" align="left">
                                            <asp:Label runat="server" Text="Mold cavitation :" Width="100%" style="margin-bottom: 0px"></asp:Label>
                                        </td>
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 40%;   font-family: 'Arial Unicode MS'; height: 50px;">
                                            <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_mold02" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                                        </td>                             
                                    </tr>
                                    <tr style="width:100%;">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width:60%;   font-family: 'Arial Unicode MS'; height: 50px;" align="left">
                                            <asp:Label runat="server" Text="Runner weight per shot(zero for hotrunner) :" Width="100%" style="margin-bottom: 0px"></asp:Label>
                                        </td>
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 40%;   font-family: 'Arial Unicode MS'; height: 50px;">
                                            <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_runnweight" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                                        </td>                             
                                    </tr>
                                    <tr style ="width:100%;">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 100%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" align="left">
                                            <asp:Label ID="lblRelust02" runat="server"  ForeColor="Red"></asp:Label> 
                                        </td>
                                    </tr>
                                    <tr style="width:100%;">
                                        <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 100%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" align="center">
                                            <asp:Button runat="server" ID="Button1" CssClass="btn-success" Height="30px" Text="Generate" Width="30%" OnClick="btn_Generate02_Click" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button runat="server" ID="Button2" CssClass="btn-danger" Height="30px" Text="Reset" Width="30%" OnClick="btn_Clean02_Click"/>
                                        </td>
                                    </tr>
                                    <%--Calculation For Resin Material Requirement--%>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr style ="width: 100%; margin-top:20px;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="Label3"   runat="server" Font-Names="Arial Unicode MS"  Text="Monthly Resins Material Calculator" Font-Size="12" ForeColor="White"/>
                </td>
            </tr>

           <%-- <tr style="width:100%">
                <td style = "padding: 20px 10px 10px 10px; border: 1px solid #CCCCCC; width:100%;  font-family: 'Arial Unicode MS'; text-align:center;" colspan="5">
                    <h4><b>Monthly Resins Material Calculator</b></h4>
                </td>
            </tr>--%>


            <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 70%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="3" >
                    <h5><b>Date: <asp:Label runat="server" ID="lbDate"></asp:Label></b></h5>
                </td>
               
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                    <asp:Button runat="server" ID="btnGenerateBudget" Text="Generate" CssClass="btn-success" Height="30px" OnClick="btnGenerateBudget_Click" />&nbsp;
                    <asp:Button runat="server" ID="btnClear" Text="Reset" CssClass="btn-danger" Height="30px" OnClick="btnClear_Click" />&nbsp;
                    <asp:Button runat="server" ID="btnEdit" Text="Edit" CssClass="btn-primary" Height="30px" OnClick="btnEdit_Click" />
                </td>        
            </tr>


            
            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width:100%;  font-family: 'Arial Unicode MS'; text-align:center;" colspan="3" valign="top">
                    <asp:DataGrid runat="server" Width ="100%" ID ="dgPartList" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="false" CssClass="table">
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundColumn DataField="partNumberAll" HeaderText="Parts No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="jigNo" HeaderText="JigNo"></asp:BoundColumn>
                            <asp:BoundColumn DataField="cavityCount" HeaderText="Cavity"></asp:BoundColumn>  
                            <asp:BoundColumn DataField="matPart01" HeaderText="1st Material"></asp:BoundColumn>
                            <asp:BoundColumn DataField="matPart02" HeaderText="2nd Material"></asp:BoundColumn>

                            <asp:BoundColumn DataField="materialWeight01" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="materialWeight02" Visible="false"></asp:BoundColumn> 
                            <asp:BoundColumn DataField="material1stUnitPrice" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="material2ndUnitPrice" Visible="false"></asp:BoundColumn> 
                            <asp:BoundColumn DataField="cycleTime" Visible="false"></asp:BoundColumn>
                            
                                                                 
                            <asp:TemplateColumn HeaderText="Order Qty">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID ="txtOrderQty" Height="20px" Width="80px" ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            
                        </Columns>
                    </asp:DataGrid>
                </td>

                <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width:100%;  font-family: 'Arial Unicode MS'; text-align:center;" colspan="2" valign="top" >
                    <asp:DataGrid runat="server" Width ="100%" ID ="dgMaterialResult" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="false"  CssClass="table">
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundColumn DataField="MaterialNo" HeaderText="Material No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MaterialKgs" HeaderText="Material Require"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MaterailCost" HeaderText="Material Cost"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EstTime" HeaderText="Estimate Complete Time"></asp:BoundColumn> 
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>      
        </table> 
             
    </div>
</asp:Content>

