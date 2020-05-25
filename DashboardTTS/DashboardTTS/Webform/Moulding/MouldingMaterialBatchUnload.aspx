<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MouldingMaterialBatchUnload.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.Moulding.MouldingMaterialBatchUnload" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
    <link href="../Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />  
    <link href="/Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />

    
    <script>
        function Cancel()
        {
            try
            {
                if (confirm('Your action will not save, are you sure?') == true)
                {
                    //alert("./BOMList.aspx" + "?CommandName=" + CommandName + "&PartNumberAll=" + PartNumberAll);
                    window.location.href = "./MouldingMaintain.aspx";
                }
            }
            catch (e)
            {
                alert('exception' + e.message);
            }
        }
    </script>
     
    <div style="width:55%;align-items:center;margin:auto">
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="4"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Moulding Material Batch Unload" Font-Size="12" ForeColor="White"/>
                </td>
            </tr>

             <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 100%; height:50px; vertical-align:middle";  font-family: 'Arial Unicode MS'; align="center" colspan="4" >
                    <Asp:label ID="Label1"   runat="server" Font-Names="Arial Unicode MS"  Text="Choosing Material" Font-Size="12" />
                </td>
             </tr>

           <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Machine ID / Loan:" Width="93%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList ID="ddl_MachineNo"  runat="server" Width="93%" Height="23px" BorderWidth="1px" />
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Material No:" Width="93%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList ID="ddl_MaterialNo"  runat="server" Width="93%" Height="23px" BorderWidth="1px" OnSelectedIndexChanged="ddl_MaterialNo_SelectedIndexChanged" AutoPostBack="true"/>
                </td>
            </tr>

            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Material Lot No :" Width="93%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList ID="ddl_MaterialLotNo"  runat="server" Width="93%" Height="23px" BorderWidth="1px"  OnSelectedIndexChanged="ddl_MaterialLotNo_SelectedIndexChanged" AutoPostBack="true"/>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Load Date:" Width="93%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList ID="ddl_UnloadDate"  runat="server" Width="93%" Height="23px" BorderWidth="1px" OnSelectedIndexChanged="ddl_UnloadDate_SelectedIndexChanged" AutoPostBack="true"/>
                </td>
            </tr>

            <tr style="width:100%">
                 <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Unload Weight :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                        <asp:TextBox runat="server" ID="txt_UnloadWeight" Width="93%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled" ></asp:TextBox>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                   
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                   <asp:Button runat="server" ID="btn_Add" Text="Add" Width="50%" Height="23px" CssClass="btn-primary" OnClick="btn_Add_Click" />
                </td>
            </tr>

             <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 100%; height:50px; vertical-align:middle";  font-family: 'Arial Unicode MS'; align="center" colspan="4" >
                    <Asp:label ID="Label2"   runat="server" Font-Names="Arial Unicode MS"  Text="Unloading Material List" Font-Size="12" />
                </td>
             </tr>

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 80%;   font-family: 'Arial Unicode MS'; height: 50px; margin-left: 40px;" colspan="4">
                  <asp:Label ID="lblResult" runat="server"></asp:Label> 
                    <asp:DataGrid runat="server" CssClass="table table-hover" ID ="dg_MaterialList" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" Width="100%"  HorizontalAlign="Center" AutoGenerateColumns="False" >
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775"  />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="left" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundColumn DataField="MachineID" HeaderText="Machine ID / Loan"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Material_No" HeaderText="Material No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Material_LotNo" HeaderText="Material LotNo"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Load_Time" HeaderText="Load Date"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Transaction_Weight" HeaderText="Unload Weight"></asp:BoundColumn>

                            <asp:TemplateColumn HeaderText="Delete" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btn_delete" CommandName="Delete" Height="20px" Text="×" Index='<%# ((DataGridItem)Container).ItemIndex %>' CssClass="btn-danger" />
                                </ItemTemplate>
                            </asp:TemplateColumn>

                        </Columns>
                        <%--<Columns>
                            <asp:BoundColumn DataField="MachineID" HeaderText="Machine ID" HeaderStyle-Width="10%"></asp:BoundColumn>
                           
                            <asp:TemplateColumn HeaderText="Material No">
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddl_MaterialNo" Width="100%" Height="23px" OnSelectedIndexChanged="ddl_MaterialNo_SelectedIndexChanged" AutoPostBack="true" Index='<%# ((DataGridItem)Container).ItemIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Material Lot No">
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddl_MaterialLotNo" Width="100%"  Height="23px" OnSelectedIndexChanged="ddl_MaterialLotNo_SelectedIndexChanged" AutoPostBack="true" Index='<%# ((DataGridItem)Container).ItemIndex %>' Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Load Date">
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddl_LoadDate" Width="100%"  Height="23px" OnSelectedIndexChanged="ddl_LoadDate_SelectedIndexChanged" AutoPostBack="true" Index='<%# ((DataGridItem)Container).ItemIndex %>' Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Unload Weight" HeaderStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txt_UnloadWeight" Width="100%" Height="23px" Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="More" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="cb_OneMore" OnCheckedChanged="cb_OneMore_CheckedChanged" AutoPostBack="true" Index='<%# ((DataGridItem)Container).ItemIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        
                        </Columns>--%>
                    </asp:DataGrid>
                </td>
            </tr>


            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 50%; height:50px; vertical-align:middle"; font-family: 'Arial Unicode MS'; align="center"  colspan="2">
                    <asp:Button  runat="server" Width="50%" Height="30px" Text="Submit" ID="btn_submit" OnClick="btn_submit_Click" CssClass="btn-success"/>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 50%; height:50px; vertical-align:middle"; font-family: 'Arial Unicode MS'; align="center"  colspan="2">
                    <asp:Button  runat="server" Width="50%" Text="Cancel" Height="30px" ID="btn_cancel" OnClick="btn_cancel_Click"   CssClass="btn-danger"/>
                </td>
            </tr>

        </table>
    </div>
</asp:Content>