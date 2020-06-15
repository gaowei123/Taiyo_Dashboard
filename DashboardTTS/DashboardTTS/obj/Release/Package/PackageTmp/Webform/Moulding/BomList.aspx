<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="BomList.aspx.cs" Inherits="DashboardTTS.Webform.Moulding.BomList" %>



<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >

    <script>
        function ConfirmDelete(PartNumberAll, CommandName)
        {
            try
            {
                if (confirm('Are you sure to delete this part?') == true)
                {
                    window.location.href = "./BOMList.aspx" + "?CommandName=" + CommandName + "&PartNumberAll=" + PartNumberAll;
                }
            }
            catch (e)
            {
            }
        }
    </script>


<div style="width: auto; align-items:center;margin:auto">

        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                        <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                        <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Moulding Bom List" Font-Size="12" ForeColor="White"/>
                    </td>
                </tr>
                <tr style="width:100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                        <asp:Label runat="server" Text="Part No :" Width="100%"></asp:Label>  
                    </td>
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                        <asp:TextBox runat="server" ID="txt_partNo"  Width="93%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled" ></asp:TextBox>
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
                            <asp:TemplateColumn HeaderText="" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button runat="server" Text="×" Height="20px" CommandName="Delete" CssClass="btn-danger" />
                                </ItemTemplate>
                            </asp:TemplateColumn>

                          
                            <asp:ButtonColumn DataTextField="partNumberAll" CommandName="Update" HeaderText="PartNumberAll"></asp:ButtonColumn>
                            <asp:BoundColumn DataField="partNumberAll" HeaderText="PartNumberAll" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="matPart01" HeaderText="1st Material"></asp:BoundColumn>
                            <asp:BoundColumn DataField="matPart02" HeaderText="2nd Material"></asp:BoundColumn>
                            <asp:BoundColumn DataField="customer" HeaderText="Customer"></asp:BoundColumn>
                            <asp:BoundColumn DataField="model" HeaderText="Model"></asp:BoundColumn>
                            <asp:BoundColumn DataField="jigNo" HeaderText="JigNo"></asp:BoundColumn>
                            <asp:BoundColumn DataField="cavityCount" HeaderText="Cavity Count"></asp:BoundColumn>
                            <asp:BoundColumn DataField="partsWeight" HeaderText="Parts Weight" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="unitCount" HeaderText="Unit Cost(Per Cavity)"></asp:BoundColumn>
                            <asp:BoundColumn DataField="cycleTime" HeaderText="Cycle Time"></asp:BoundColumn>
                            <asp:BoundColumn DataField="userName" HeaderText="UserName"></asp:BoundColumn>
                            <asp:BoundColumn DataField="dateTime" HeaderText="Updated Time"></asp:BoundColumn>
                            <asp:BoundColumn DataField="machineID" HeaderText="Type"></asp:BoundColumn>
                            <asp:BoundColumn DataField="suppiller" HeaderText="Supplier"></asp:BoundColumn>
                            <asp:BoundColumn DataField="mExistFlag01" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="mExistFlag02" Visible="false"></asp:BoundColumn>
                        </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>
        </table> 
   
</div>

           
</asp:Content>