<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PQCRejectDetail.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.PQC.PQCRejectDetail" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >   
    <div style="width: 100%; height: 257px; align-items:center;margin:auto">

        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" />
                    <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="PQC Rejection Detail Report" Font-Size="12" ForeColor="White"></Asp:label>
                </td>
            </tr>

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="Station :" Width="100px"></asp:Label>  
                </td>
                <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px;">
                    <asp:DropDownList runat="server"  ID="ddlMachineType" Width="100%"  Height="23px">
                            <asp:ListItem Value="">All</asp:ListItem>
                            <asp:ListItem Value="1">Online1(Sta1)</asp:ListItem>
                            <asp:ListItem Value="2">Online2(Sta2)</asp:ListItem>
                            <asp:ListItem Value="3">Online3(Sta3)</asp:ListItem>
                            <asp:ListItem Value="4">Online4(Sta4)</asp:ListItem>
                            <asp:ListItem Value="5">Online5(Sta5)</asp:ListItem>
                            <asp:ListItem Value="6">Online6(Sta6)</asp:ListItem>
                            <asp:ListItem Value="7">Online7(Sta7)</asp:ListItem>
                            <asp:ListItem Value="8">Online8(Sta8)</asp:ListItem>

                            <asp:ListItem Value="16">WIP1(Sta16)</asp:ListItem>
                            <asp:ListItem Value="17">WIP2(Sta17)</asp:ListItem>
                            <asp:ListItem Value="14">WIP3(Sta14)</asp:ListItem>
                            <asp:ListItem Value="15">WIP4(Sta15)</asp:ListItem>
                            <asp:ListItem Value="11">WIP5(Sta11)</asp:ListItem>
                            <asp:ListItem Value="13">WIP6(Sta13)</asp:ListItem>

                            <asp:ListItem Value="25">Packing1(Sta25)</asp:ListItem>
                            <asp:ListItem Value="23">Packing2(Sta23)</asp:ListItem>
                            <asp:ListItem Value="22">Packing3(Sta22)</asp:ListItem>
                            <asp:ListItem Value="21">Packing4(Sta21)</asp:ListItem>
                            <asp:ListItem Value="24">Packing5(Sta24)</asp:ListItem>
                            <asp:ListItem Value="12">Packing6(Sta12)</asp:ListItem>
                        </asp:DropDownList>
                </td>

                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="Dept :" Width="100px"></asp:Label> 
                </td>     
                <td style ="  width: 25%; border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; height: 50px; vertical-align:middle" >
                    <asp:DropDownList ID="ddlRejType"  runat="server" Width="100%" Height="23px"  OnSelectedIndexChanged="ddlRejType_SelectedIndexChanged" AutoPostBack="true" >
                        <asp:ListItem Value="">ALL</asp:ListItem>
                        <asp:ListItem Value="Paint">Paint</asp:ListItem>
                        <asp:ListItem Value="Mould">Mould</asp:ListItem>
                        <asp:ListItem Value="Paint">Paint</asp:ListItem>
                        <asp:ListItem Value="Others">Others</asp:ListItem>
                    </asp:DropDownList>
                </td>
                                                      
                <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MeS'; height: 50px;">
                </td>
            </tr>
           
            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="Defect Code:" Width="100px"></asp:Label>
                </td>
                <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; vertical-align:middle">
                    <asp:DropDownList runat="server" ID="ddlRejCode" Width="100%"></asp:DropDownList>    
                </td>

                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="Part No :" Width="100px"></asp:Label>  
                </td>
                <td style ="  width: 25%; border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; height: 50px; vertical-align:middle" >
                    <asp:TextBox runat="server" ID="txtPartNo" Width="100%" Height="23px" AutoCompleteType="Disabled"></asp:TextBox>
                </td>
                                                      
                <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
                    
                </td>
            </tr>
            
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="From :" Width="100px"></asp:Label>  
                </td>
                <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                    <igsch:WebDateChooser ID="infDchFrom" runat="server" Height="23px"   Width="100%" Value="" />
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="To :"></asp:Label> 
                </td>
                <td style =" border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                    <igsch:WebDateChooser ID="infDchTo" runat="server"  Height="23px"  Width="100%" />
                </td>
                                                      
                <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Button ID="btn_generate" runat="server" Text="Generate" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Height="30px"  OnClick="btn_generate_Click"/>                                           
                </td>
            </tr>    
                            
            <tr style ="width: 100%">
                <td style = "padding: 25px 25px 25px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="5"> 
                    <h3><asp:Label ID="lbResult" runat="server" CssClass="col-xs-11"></asp:Label></h3>   
                </td>
            </tr>

            <tr style ="width: 100%">
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
                            
                            <asp:BoundColumn DataField="Day" HeaderText="Day"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Shift" HeaderText="Shift"></asp:BoundColumn>
                            <asp:BoundColumn DataField="touchPC" HeaderText="Station"></asp:BoundColumn>

                            <asp:BoundColumn DataField="model" HeaderText="Model"></asp:BoundColumn>
                            <asp:BoundColumn DataField="partNumber" HeaderText="Part No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="JobNumber" HeaderText="Job No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="lotNo" HeaderText="Lot No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="materialNo" HeaderText="Material No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RejType" HeaderText="Dept"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RejCode" HeaderText="Rej Code"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TotalQty" HeaderText="Total Qty"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RejQty" HeaderText="Rej Qty"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RejRate" HeaderText="REJ%"></asp:BoundColumn>
                            
                            <asp:BoundColumn DataField="Operator" HeaderText="PIC"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DateTime" HeaderText="Date Time"></asp:BoundColumn>

                        </Columns>
                    </asp:DataGrid> 
                </td>
            </tr>

        </table>
    </div>
</asp:Content>

