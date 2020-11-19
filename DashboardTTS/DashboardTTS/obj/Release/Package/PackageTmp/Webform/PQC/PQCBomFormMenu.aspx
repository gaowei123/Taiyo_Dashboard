<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PQCBomFormMenu.aspx.cs" Inherits="DashboardTTS.Webform.PQCBomFormMenu" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
     
    <div style="width: 80%;align-items:center;margin:auto">
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="4"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="PQC BOM Form Menu" Font-Size="12" ForeColor="White"/>
                </td>
            </tr>
                        
            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Part No :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtPartNo" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="txt_partNo_TextChanged"></asp:TextBox>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Cycle Time(s) :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtcycleTime" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  ></asp:TextBox>
                </td>
            </tr>

            <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Customer :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtCustomer" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled" />
                </td>      
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Unit Cost :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtUnitCost" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  ></asp:TextBox>
                </td>
            </tr>

            <tr style="width:100%;">
               <%-- <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Cycle Time :" Width="100%"></asp:Label>  
                </td>   
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_cycleTime" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled" ></asp:TextBox>
                </td>--%>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Supplier :" Width="100%"></asp:Label>  
                </td>   
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtSupplier" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled" ></asp:TextBox>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Model :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtModel" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled" ></asp:TextBox>
                </td>
            </tr>

            <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Type :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList runat="server" ID="ddlType" Width="100%" Height="23px"></asp:DropDownList>
                </td>
                
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Description :" Width="100%"></asp:Label>  
                </td>   
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList runat="server" ID="ddlDescription" Height="23px" Width="100%">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                        <asp:ListItem Text="BUTTON" Value="BUTTON"></asp:ListItem>
                        <asp:ListItem Text="LENS" Value="LENS"></asp:ListItem>
                        <asp:ListItem Text="BEZEL" Value="BEZEL"></asp:ListItem>
                        <asp:ListItem Text="PANEL" Value="PANEL"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr style="width:100%">
                 <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;">     
                    <asp:Label runat="server" Text="Number :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">     
                    <asp:TextBox runat="server" ID="txtNumber" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled" ></asp:TextBox>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Color :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList runat="server" ID="ddlColor" Width="100%" Height="23px"></asp:DropDownList>
                </td>
            </tr>


            <tr style="width:100%">
                 <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;">     
                    <asp:Label runat="server" Text="Ship To :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">     
                    <asp:DropDownList runat="server" ID="ddlShipTo" Width="100%" Height="23px"></asp:DropDownList>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Coating :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList runat="server" ID="ddlCoating" Height="23px" Width="100%">
                        <asp:listitem Text="" Value=""></asp:listitem>
                        <asp:listitem Text="One Coat" Value="One Coat"></asp:listitem>
                        <asp:listitem Text="Two Coat" Value="Two Coat"></asp:listitem>
                        <asp:listitem Text="Three Coat" Value="Three Coat"></asp:listitem>
                        <asp:listitem Text="Print Coat" Value="Print Coat"></asp:listitem>
                    </asp:DropDownList>
                </td>
                
            </tr>


            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Process :" Width="100%"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 50%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" >
                    <asp:Label runat="server" ID="lbProcess"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">     
                    <asp:DropDownList ID="ddlProcess"  runat="server" Width="50%" Height="23px" BorderWidth="1px" OnSelectedIndexChanged="ddlProcess_SelectedIndexChanged"  AutoPostBack="true" /> &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" Width="20%" Height="23px" Runat="server" Text="Cancel" CssClass="btn-danger" OnClick="btnCancel_Click" />
                </td>
            </tr>


             <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 53px;" >
                    <asp:Label runat="server" Text="Remarks :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 80%;   font-family: 'Arial Unicode MS'; height: 53px;" colspan="3">
                    <asp:TextBox ID="txt_remark" runat="server" TextMode="MultiLine" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="92px" AutoCompleteType="Disabled" ></asp:TextBox>
                </td>
            </tr>


            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="4" >
                    <asp:Label runat="server" Text="Material Detail Info:" Font-Bold="true" Width="100%"></asp:Label>  
                </td>
            </tr>



            <tr style="width:100%">
              
                <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="4">
                    <asp:DataGrid runat="server" CssClass="table table-hover" ID="dg_MaterialPart" Width="100%" AutoGenerateColumns = False>
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775"  />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center"  Height="60px"/>
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundColumn DataField="sn" HeaderText="S/N" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="materialPartNo" HeaderText="Material Part"></asp:BoundColumn>
                            <asp:BoundColumn DataField="materialName" HeaderText="Material Name"></asp:BoundColumn>
                            <asp:BoundColumn DataField="module" HeaderText="Module"></asp:BoundColumn>
                            <asp:BoundColumn DataField="partCount" HeaderText="Part Count"></asp:BoundColumn>
                            <asp:BoundColumn DataField="outerBoxQty" HeaderText="Outer Box Qty"></asp:BoundColumn>
                            <asp:BoundColumn DataField="packingTrays" HeaderText="Packing Trays"></asp:BoundColumn>


                            <asp:BoundColumn DataField="imagePath" Visible="false"></asp:BoundColumn>

                            <asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Material Img">
                                <ItemTemplate>
                                    <asp:Image runat="server" ID="imgMaterialPart"  Width="50px" heigth="50px"/>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                                           
                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" HeaderText="Delete" Visible="true" >
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Button ID="btn_Delete" CommandName="Delete" Width="30px" Height="20px" Runat="server" Text="×" CssClass="btn-danger" Index='<%# ((DataGridItem)Container).ItemIndex %>'/>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:BoundColumn DataField="imageAbsolutePath" Visible="false"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>

            <tr style="width:100%" >
                <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 80%;   font-family: 'Arial Unicode MS'; height: 50px; vertical-align:middle" colspan="4">
                    <table style="width:100%">
                        <tr >
                            <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 9%;   font-family: 'Arial Unicode MS'; ">
                                <asp:Label runat="server" Text=" S/N :" ></asp:Label>
                            </td>
                            <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 5%;   font-family: 'Arial Unicode MS'; ">
                                <asp:TextBox runat="server" ID="txt_sn" BorderStyle="Solid" BorderWidth="1px" Height="23px" Width="40px" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 13%;   font-family: 'Arial Unicode MS'; ">
                                <asp:Label runat="server" Text="MaterialPart :"> </asp:Label>
                            </td>
                            <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; ">
                               <asp:TextBox runat="server" ID="txt_materialPart" BorderStyle="Solid" BorderWidth="1px" Height="23px"  Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 10%;   font-family: 'Arial Unicode MS'; ">
                                <asp:Label runat="server" Text="PartCount :" ></asp:Label>
                            </td>
                            <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 5%;   font-family: 'Arial Unicode MS'; ">
                                <asp:TextBox runat="server" ID="txt_partCount" BorderStyle="Solid" BorderWidth="1px" Width="40px" Height="23px" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 10%;   font-family: 'Arial Unicode MS'; ">
                                <asp:Label runat="server" Text="Module :" ></asp:Label>
                            </td>
                            <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; ">
                               <asp:TextBox runat="server" ID="txtModule" BorderStyle="Solid" BorderWidth="1px" Width="100%" Height="23px" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 9%;   font-family: 'Arial Unicode MS'; ">
                                <asp:Label runat="server" Text="Outer Box Qty :"> </asp:Label>
                                
                            </td>
                            <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 5%;   font-family: 'Arial Unicode MS'; ">
                                <asp:TextBox runat="server" ID="txtOuterBoxQty" BorderStyle="Solid" BorderWidth="1px" Width="40px" Height="23px"  AutoCompleteType="Disabled"></asp:TextBox>
                                
                            </td>
                            <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 13%;   font-family: 'Arial Unicode MS'; ">
                                <asp:Label runat="server" Text="Material Name :" ></asp:Label>
                            </td>
                            <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; ">
                               <asp:TextBox runat="server" ID="txtMaterialName" BorderStyle="Solid" BorderWidth="1px" Height="23px" Width="100%"  AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 10%;   font-family: 'Arial Unicode MS'; ">
                                <asp:Label runat="server" Text="Packing Trays :" ></asp:Label>
                            </td>
                            <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 5%;   font-family: 'Arial Unicode MS'; ">
                                <asp:TextBox runat="server" ID="txtPackingTrays" BorderStyle="Solid" BorderWidth="1px" Width="40px" Height="23px" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 10%;   font-family: 'Arial Unicode MS'; ">
                                <label>Part Img:</label>
                                <asp:FileUpload runat="server"  ID="fpImg" />
                            </td>
                            <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; " align="right">
                                <asp:Button  runat="server" Height="30px" Text="Add" ID="btn_add" Width="50%"   OnClick="btn_add_Click" CssClass="btn-success"/>
                            </td>
                        </tr>

                    </table>
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