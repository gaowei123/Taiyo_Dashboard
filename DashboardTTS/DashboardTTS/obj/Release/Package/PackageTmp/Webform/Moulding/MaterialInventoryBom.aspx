<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialInventoryBom.aspx.cs" Inherits="DashboardTTS.Webform.Moulding.MaterialInventoryBom"  MasterPageFile="~/Site.Master"%>


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
                    window.location.href = "./MaterialInventoryBom.aspx" + "?CommandName=" + CommandName + "&PartNumberAll=" + PartNumberAll;
                }
            }
            catch (e)
            {
                //alert('exception' + e.message);
            }
            //alert('end func');
        }
    </script>


<div style="width: 60%; align-items:center;margin:auto">

        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                        <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                        <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Moulding Material Bom List" Font-Size="12" ForeColor="White"/>
                    </td>
                </tr>
                <tr style="width:100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                        <asp:Label runat="server" Text="Material No:" Width="100%"></asp:Label>  
                    </td>
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                        <asp:TextBox runat="server" ID="txt_materialPart"  Width="93%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled" ></asp:TextBox>
                    </td>
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                      
                    </td>
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                  
                    </td>
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; width: 20%; height: 50px; ">
                        <asp:Button runat="server" ID="btn_search" Text="Generate"  Width="50%" OnClick="btn_search_Click" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Height="30px" />
                    </td>
                </tr>
                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="4"> 
                        <asp:Label ID="lblResult" runat="server"></asp:Label> 
                    </td>
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="1">
                        <asp:Button  runat="server" ID="btn_Add" Text="Add" Width="50%" Height="30px"  OnClick="btn_Add_Click" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-primary" />
                    </td>
                </tr>

                <tr style ="width: 100%">   
                    <td style = "padding: 25px 25px 25px 25px; border: 1px solid #CCCCCC; width:100%;  font-family: 'Arial Unicode MS'; height: 50px;" colspan="5" aria-hidden="False" aria-multiline="True" aria-multiselectable="True" aria-orientation="vertical" draggable="False">
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
                            <asp:ButtonColumn DataTextField="Material_Part" CommandName="Update" HeaderText="Material No"></asp:ButtonColumn>
                            <asp:BoundColumn DataField="Material_Part" HeaderText="Material No" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ResinType" HeaderText="Resin Type"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Unit_Price_USD" HeaderText="(USD)Per Kgs"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Unit_Price" HeaderText="(SGD)Per Kgs"></asp:BoundColumn>
                           
                            

                            <asp:BoundColumn DataField="Updated_User" HeaderText="Updated By"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Updated_Time" HeaderText="Updated Date"></asp:BoundColumn>                            
                            <%--<asp:ButtonColumn CommandName="Delete" Text="Delete" HeaderText="Delete" ButtonType="PushButton"></asp:ButtonColumn>--%>
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