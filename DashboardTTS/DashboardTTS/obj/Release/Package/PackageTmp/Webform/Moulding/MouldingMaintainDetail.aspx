<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MouldingMaintainDetail.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.MouldingMaintainDetail" %>


<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
    <link href="../Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />  
    <link href="/Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />

    
    <script>
        function Cancel()
        {
            try
            {
                if (confirm('Your action will not be saved, are you sure?') == true)
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
     
    <div style="width: 100%;align-items:center;margin:auto">
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="4"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Moulding Maintenancen Detail" Font-Size="12" ForeColor="White"/>
                </td>
            </tr>

             <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                        <asp:Label runat="server" Text="Check Period :" Width="100px"></asp:Label>  
                    </td>
                    <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 30%; height: 50px;">
                        <asp:DropDownList ID="ddl_CheckPeriod"  runat="server" Width="100%" Height="23px"   > </asp:DropDownList>
                    </td>
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%; font-family: 'Arial Unicode MS'; height: 50px;"> 
                       
                    </td>
                    <td style ="  width: 30%; border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; height: 50px; vertical-align:middle" >
                        <asp:Button runat="server" ID="btn_generate" Text="Generate" CssClass="btn-success"  Width="40%" Height="30px" OnClick="btn_generate_Click"/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btn_Add" Text="Add Item" CssClass="btn-primary"  Width="40%" Height="30px" OnClick="btn_Add_Click"/>
                    </td>
              </tr>


            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 80%;   font-family: 'Arial Unicode MS'; height: 50px; margin-left: 40px;" colspan="4">
                  <asp:Label ID="lblResult" runat="server"></asp:Label> 
                    <asp:DataGrid runat="server"  CssClass="table table-hover" ID ="dg_CheckItemList" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" Width="100%"  HorizontalAlign="Center" AutoGenerateColumns="False" >
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775"  />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="left" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                         <HeaderStyle HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundColumn DataField="CheckPeriod" HeaderText="Check Period" HeaderStyle-Width="15%"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CheckItem" HeaderText="Check Item" HeaderStyle-Width="35%"  Visible="false"></asp:BoundColumn>
                            <asp:ButtonColumn DataTextField="CheckItem" HeaderText="Check Item" HeaderStyle-Width="35%" CommandName="LinkDetail"></asp:ButtonColumn>

                            <asp:TemplateColumn HeaderText="MachineID">
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddl_MachineID" Width="60%" >
                                        <asp:ListItem Value="1,2,3,4,5,6,7,8">All</asp:ListItem>
                                        <asp:ListItem Value="1">No.1</asp:ListItem>
                                        <asp:ListItem Value="2">No.2</asp:ListItem>
                                        <asp:ListItem Value="3">No.3</asp:ListItem>
                                        <asp:ListItem Value="4">No.4</asp:ListItem>
                                        <asp:ListItem Value="5">No.5</asp:ListItem>
                                        <asp:ListItem Value="6">No.6</asp:ListItem>
                                        <asp:ListItem Value="7">No.7</asp:ListItem>
                                        <asp:ListItem Value="8">No.8</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Check Result" HeaderStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddl_CheckResult">
                                        <asp:ListItem>OK</asp:ListItem>
                                        <asp:ListItem>NG</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                                    <asp:Label runat="server">Remarks:</asp:Label>
                                    <asp:TextBox runat="server" ID="txt_SpareParts"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Perform By">
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddl_CheckBy" Width="60%"> </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Checked" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="4%">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="cb_Update"/>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>


            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 50%; height:50px; vertical-align:middle"; font-family: 'Arial Unicode MS'; align="center"  colspan="2">
                    <asp:Button  runat="server" Width="50%" Height="30px" Text="Submit" ID="btn_submit" OnClick="btn_submit_Click" CssClass="btn-success"/>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 50%; height:50px; vertical-align:middle"; font-family: 'Arial Unicode MS'; align="center"  colspan="2">
                    <asp:Button  runat="server" Width="50%" Text="Cancel" Height="30px" ID="btn_cancel"  OnClick="btn_cancel_Click" CssClass="btn-danger"/>
                </td>
            </tr>

        </table>
    </div>
</asp:Content>



