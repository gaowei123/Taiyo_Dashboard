<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MouldingMaintain_Verify.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.Moulding.Moulding" %>


<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
    <link href="../Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />  
    <link href="/Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />


    <div style="width: 100%;align-items:center;margin:auto">
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" > 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Moulding Maintenancen Verify List" Font-Size="12" ForeColor="White"/>
                </td>
            </tr>

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 40px; font-weight: bold;"  align="Center"> 
                    <Asp:label ID="Label1"   runat="server" Font-Names="Arial Unicode MS"  Text="Pending Verify List" Font-Size="14"/>
                </td>
            </tr>

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 40px; font-weight: bold;"  align="right"> 
                    <asp:Button runat="server" ID="btn_VerifyAll" CssClass="btn-success" Text="Verify All" OnClick="btn_VerifyAll_Click" Height="30px" Width="80px" />
                </td>
            </tr>


            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 80%;   font-family: 'Arial Unicode MS'; height: 50px; margin-left: 40px;" >
                    <asp:Label ID="lblResult" runat="server"></asp:Label> 
                    <asp:DataGrid runat="server" CssClass="table table-hover" ID ="dg_VerifyList" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" Width="100%"  HorizontalAlign="Center" AutoGenerateColumns="False" >
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775"  />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="left" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                         <HeaderStyle HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundColumn DataField="CheckPeriod" HeaderText="Check Period" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="CheckItem" HeaderText="Check Item"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MachineID" HeaderText="MachineID"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CheckResult" HeaderText="Check Result"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SpareParts" HeaderText="Remarks"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ChangeTime" HeaderText="Change Time" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CheckDate" HeaderText="Check Date"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CheckBy" HeaderText="Perform By"></asp:BoundColumn>


                            <asp:TemplateColumn HeaderText="Operation" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Button runat="server" Text="Verify" ID="btn_Verify" CssClass="btn-success" CommandName="Verify" Index='<%# ((DataGridItem)Container).ItemIndex %>' Width="60px" Height="25px"/>&nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" Text="Cancel" ID="btn_Cancel" CssClass="btn-danger" CommandName="Cancel" Index='<%# ((DataGridItem)Container).ItemIndex %>' Width="60px"  Height="25px"/>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>

        </table>
    </div>
</asp:Content>