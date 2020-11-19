<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PQCDefectCodeList.aspx.cs" Inherits="DashboardTTS.Webform.PQC.PQCDefectCodeList" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
    
    <div style =" width: 50%;margin:auto;">
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="PQC Defect Code List" Font-Size="12" ForeColor="White"/>
                </td>
            </tr>

            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Defect Type :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList runat="server" ID="ddlDefectType" Width="100%" Height="23px" ></asp:DropDownList>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Defect Code :" Width="100%"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtDefectCode" Width="100%" AutoCompleteType="Disabled" Height="23px" ></asp:TextBox>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; width: 20%; height: 50px; ">
                    <asp:Button runat="server" ID="btn_search" Text="Generate"  Width="70%" OnClick="btn_search_Click" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Height="30px" />
                </td>
            </tr>

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="4"> 
                    <asp:Label ID="lblResult" runat="server"></asp:Label> 
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="1">
                    <asp:Button  runat="server" ID="btn_Add" Text="Add" Width="70%" Height="30px"  OnClick="btn_Add_Click" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-primary" />
                </td>
            </tr>

            <tr style ="width: 100%">   
                    <td style = "padding: 25px 25px 25px 25px; border: 1px solid #CCCCCC; width:100%;  font-family: 'Arial Unicode MS'; height: 50px;" colspan="5" aria-hidden="False" aria-multiline="True" aria-multiselectable="True" aria-orientation="vertical" draggable="false">
                    <asp:DataGrid runat="server" CssClass="table table-hover" ID ="dg_BOMList" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" Width="100%"  HorizontalAlign="Center" AutoGenerateColumns="False" >
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775"  />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundColumn DataField="partNumber" HeaderText="PartNumber" Visible="false"></asp:BoundColumn>
                            <asp:ButtonColumn DataTextField="partNumber" HeaderText="PartNumber" CommandName="LinkDetailPage"></asp:ButtonColumn>
                            <asp:BoundColumn DataField="customer" HeaderText="Customer"></asp:BoundColumn>
                            <asp:BoundColumn DataField="model" HeaderText="Model"></asp:BoundColumn>
                            <asp:BoundColumn DataField="blockCount" HeaderText="Block Count"></asp:BoundColumn>
                            <asp:BoundColumn DataField="unitCount" HeaderText="Unit Count"></asp:BoundColumn>
                           
                            <asp:BoundColumn DataField="dateTime" HeaderText="Update Time"></asp:BoundColumn>

                            <asp:TemplateColumn HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:Button runat="server" CommandName="Delete" Text="×" Height="23px" CssClass="btn-danger"/>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>

        </table> 
    </div>
</asp:Content>