<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="BomFormMenu.aspx.cs" Inherits="DashboardTTS.Webform.BomFormMenu" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
    <link href="../Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />  
    <link href="/Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />

    <script src="../js/Dashboard.js"> </script>

    
     
    <div style="width: 1154px;align-items:center;margin:auto">
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="4"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Laser BOM Form Menu" Font-Size="12" ForeColor="White"/>
                </td>
            </tr>
                        
            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Part No :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_partNo" Width="93%" BorderStyle="Solid" BorderWidth="1px" Height="23px" OnTextChanged="txt_partNo_TextChanged" AutoPostBack="true" AutoCompleteType="Disabled" ></asp:TextBox>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Machine ID :" Width="93%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList ID="ddl_machineNo"  runat="server" Width="93%" Height="23px" BorderWidth="1px"  />
                </td>
            </tr>

            <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Block Count :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_blockCount" Width="93%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled"  ></asp:TextBox>
                </td>      
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Unit Count :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_unitCount" Width="93%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  ></asp:TextBox>
                </td>         
            </tr>

            <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Cycle Time :" Width="100%"></asp:Label>  
                </td>   
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_cycleTime" Width="93%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled" ></asp:TextBox>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Model :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_module" Width="93%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled" ></asp:TextBox>
                </td>
            </tr>

            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Type :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList runat="server" ID="ddl_type"  Width="93%"></asp:DropDownList>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;">     
                    <asp:Label runat="server" Text="Customer :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">     
                    <asp:TextBox runat="server" ID="txt_customer" Width="93%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled" ></asp:TextBox>
                </td>
            </tr>

            <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Lighting :" Width="100%"></asp:Label>  
                </td>   
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_lighting" Width="93%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled" ></asp:TextBox>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Camera :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_Camera" Width="93%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled" ></asp:TextBox>
                </td>
            </tr>

            <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Current Power :" Width="100%"></asp:Label>  
                </td>   
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_power" Width="93%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled" ></asp:TextBox>
                </td>

                
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Supplier :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_Supplier" Width="93%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled" ></asp:TextBox>
                </td>
            </tr>

            <tr style="width:100%;">
                
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Number :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtNumber" Height="23px" Width="93%" BorderStyle="Solid" BorderWidth="1px" AutoCompleteType="Disabled"></asp:TextBox>
                </td>

                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                   
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                   
                </td>
               
            </tr>


            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Part Material information: " Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="3">
                    <asp:DataGrid runat="server" ID="dg_MaterialPart" Width="100%" AutoGenerateColumns = False CssClass="table table-hover">
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775"  />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundColumn DataField="sn" HeaderText="S/N" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="partNumber" HeaderText="Part No" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="materialPartNo" HeaderText="Material Part"></asp:BoundColumn>
                            <asp:BoundColumn DataField="partCount" HeaderText="Part Count"></asp:BoundColumn>
                                           
                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" HeaderText="Delete" Visible="true">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Button ID="btn_Delete" CommandName="Delete" Width="30px" Height="20px" Runat="server" Text="×" CssClass="btn-danger" Index='<%# ((DataGridItem)Container).ItemIndex %>'/>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>

            <tr style="width:100%" >
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Add a Part Material:" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 80%;   font-family: 'Arial Unicode MS'; height: 50px; vertical-align:middle" colspan="3">
                    <asp:Label runat="server" Text=" S/N :" Width="4%" ></asp:Label>         &nbsp; &nbsp; 
                    <asp:TextBox runat="server" ID="txt_sn" Width="10%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled"></asp:TextBox>      &nbsp; &nbsp; &nbsp;  &nbsp;
                    <asp:Label runat="server" Text="MaterialPart :" Width="11%" ></asp:Label>        &nbsp; &nbsp; 
                    <asp:TextBox runat="server" ID="txt_materialPart" Width="20%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled"></asp:TextBox>   &nbsp; &nbsp; &nbsp;  &nbsp; 
                    <asp:Label runat="server" Text="PartCount :" Width="10%"></asp:Label>           &nbsp;&nbsp;
                    <asp:TextBox runat="server" ID="txt_partCount" Width="10%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled"></asp:TextBox>    &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; 
                    <asp:Button  runat="server" Width="10%" Height="30px" Text="Add" ID="btn_add"   OnClick="btn_add_Click" CssClass="btn-success"/>
                </td>
            </tr>

            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 53px;" >
                    <asp:Label runat="server" Text="Remarks :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 80%;   font-family: 'Arial Unicode MS'; height: 53px;" colspan="3">
                    <asp:TextBox ID="txt_remark" runat="server" TextMode="MultiLine" Width="98%" BorderStyle="Solid" BorderWidth="1px" Height="92px" AutoCompleteType="Disabled" ></asp:TextBox>
                </td> 
            </tr>
            <tr style ="width: 100%">   
                <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;  font-weight: bold;" colspan="4">
                </td>
            </tr>
            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 50%; height:50px; vertical-align:middle"; font-family: 'Arial Unicode MS'; align="center"  colspan="2">
                    <asp:Button  runat="server" Width="50%" Height="30px" Text="Submit" ID="btn_submit"  OnClick="btn_submit_Click" CssClass="btn-success"/>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 50%; height:50px; vertical-align:middle"; font-family: 'Arial Unicode MS'; align="center"  colspan="2">
                    <asp:Button  runat="server" Width="50%" Text="Cancel" Height="30px" ID="btn_cancel"   OnClick="btn_cancel_Click"  CssClass="btn-danger"/>
                </td>
            </tr>
        </table> 
    </div>

           
</asp:Content>