<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MouldingDeliveryOperatingHis.aspx.cs" Inherits="DashboardTTS.Webform.Moulding.MouldingDeliveryOperatingHis" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" >

    <link href="../css/Dashboard.css" rel="stylesheet" />

    <div style="width: 70%; height: 257px; align-items:center;margin:auto">
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Moulding Delivery History" Font-Size="12" ForeColor="White" />
                </td>
            </tr>
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="Part No :" Width="100px"></asp:Label>  
                </td>
                <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px;">
                    <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_PartNo" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="Job No :" Width="100px" />
                </td>
                <td style ="  width: 25%; border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; height: 50px; vertical-align:middle" >
                    <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_JobNo" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                </td>
                <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MeS'; height: 50px;">
                </td>
            </tr>
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="Lot No :" Width="100px"></asp:Label>  
                </td>
                <td style =" border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                    <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_Lotno" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" ></asp:TextBox>
                </td>   
                <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                </td>
                <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                </td>                       
                <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Button ID="btn_ScanJob" runat="server" Text="Scan Job"  OnClick="btn_ScanJob_Click" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-primary" Width="50%" Height="30px" />
                </td>
            </tr>

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; width: 15%; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:Label runat="server" Text="From :" Width="100px" />
                </td>
                <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                    <igsch:WebDateChooser ID="infDchFrom" runat="server" Height="23px"   Width="100%" Value="" > </igsch:WebDateChooser>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                   <asp:Label runat="server" Text="To :"></asp:Label> 
                </td>
                <td style =" border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 25%; height: 50px; ">
                    <igsch:WebDateChooser ID="infDchTo" runat="server"  Height="23px"  Width="100%">
                    </igsch:WebDateChooser>
                </td>                          
                <td style = "padding: 10px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Button ID="btn_generate" runat="server" Text="Generate"  OnClick="btn_generate_Click" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Width="50%" Height="30px" />
                </td>
            </tr>
                                                      
            <tr id="trChart" style ="width: 100%">
                <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="5"> 
                   <h3><asp:Label ID="lblResult" runat="server" CssClass="col-xs-11"></asp:Label></h3>    
                    <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dg_PaintingScanJobList" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="false" >
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="left" VerticalAlign="Middle" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="left" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="partNumber" HeaderText="PartNumber"></asp:BoundColumn>
                            <asp:BoundColumn DataField="jobNumber" HeaderText="JobNumber"></asp:BoundColumn>
                            <asp:BoundColumn DataField="lotNo" HeaderText="Lot No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="inQuantity" HeaderText="MRP Qty(PCS)"></asp:BoundColumn>
                            <asp:BoundColumn DataField="sendingTo" HeaderText="Sending To"></asp:BoundColumn>
                            <asp:BoundColumn DataField="remark" HeaderText="Description"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MFGDate" HeaderText="MFG Date"></asp:BoundColumn>
                            <asp:BoundColumn DataField="updatedtime" HeaderText="Scan Date"></asp:BoundColumn>
                            <asp:BoundColumn DataField="status" HeaderText="Status" Visible="true"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>              
                </td>
            </tr>                  
        </table>
    </div>                        
    </div>
</asp:Content>
