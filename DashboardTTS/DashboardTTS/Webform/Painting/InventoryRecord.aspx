<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InventoryRecord.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.Painting.InventoryRecord" %>


<asp:Content runat="server" ContentPlaceHolderID ="MainContent">
    <script>
        //js 主动失去焦点来触发 asp textbox 的 ontextchanged 事件
        function LoseFuces(obj) {
            if (obj.value.length == 13) {
                obj.blur();
            }
        }
    </script>

     <div style="width: 70%; align-items:center;margin:auto">

        <table style =" padding:0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="4"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Painting Delivery Record" Font-Size="12" ForeColor="White"/>

                    <asp:Label runat="server" ID="lbProcess" Visible="false"/>
                    <asp:Label runat="server" ID="lbRejQty" Visible="false"/>
                </td>
            </tr>
            

            <tr style ="width: 100%">   
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    Added Lot :&nbsp;<asp:Label runat="server" ID="lb_AddedCount" ></asp:Label> 
                </td>
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;"  >
                     
                </td>
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    Job No :
                </td>
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_JobID" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled" oninput="LoseFuces(this)" OnTextChanged="txt_JobID_TextChanged" AutoPostBack="true"></asp:TextBox>
                </td>
            </tr>

            <tr style ="width: 100%">
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <table style ="width: 100%" runat="server" ID="tbLotNo">
                        <tr style="100%">
                            <td style="width:40%;">Lot No :</td>
                            <td style="width:60%;"><asp:TextBox runat="server" ID="txt_LotNo" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <table style ="width: 100%" runat="server" ID="tbPartNo">
                        <tr style="100%">
                            <td style="width:40%;">Part No :</td>
                            <td style="width:60%;"><asp:TextBox runat="server" ID="txt_PartNumber" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled"></asp:TextBox></td>
                        </tr>
                    </table>
                    
                </td>
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;"  >
                    <table style ="width: 100%" runat="server" ID="tbInQty">
                        <tr style="100%">
                            <td style="width:40%;">In Quantity:</td>
                            <td style="width:60%;"><asp:TextBox runat="server" ID="txt_InQuantity" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled"></asp:TextBox></td>
                        </tr>
                    </table>
                    
                </td>
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;"  >
                    <asp:CheckBox runat="server" ID="cb_Print" Text="Print" AutoPostBack="true" OnCheckedChanged="cb_Print_CheckedChanged" /> 
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btn_add" Text="Add" Width="50%" UseSubmitBehavior="false"  BorderStyle="Solid" BorderWidth="1px" CssClass="btn-primary" Height="30px" OnClick="btn_add_Click"  />
                </td>
            </tr>


            <tr style ="width: 100%">
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="4"  >
                    <h4>Added Lot Infomation</h4>
                </td>
            </tr>

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;  font-weight: bold;" colspan="4">
                    <asp:DataGrid runat="server" ID ="dg_AddedInventoryList" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" Width="100%" CssClass="table table-striped"  HorizontalAlign="Center"  AutoGenerateColumns="False" >
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775"  />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundColumn DataField="jobNumber"  HeaderText="Job No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="lotNo" HeaderText="Lot No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="partNumber"  HeaderText="Part No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="inquantity" HeaderText="In Quantity"></asp:BoundColumn>
                            <asp:BoundColumn DataField="paintRej" HeaderText="Rej Qty"></asp:BoundColumn>
                            <asp:BoundColumn DataField="startTime" HeaderText="MFG Time"></asp:BoundColumn>
                            <asp:BoundColumn DataField="pqcQuantity" HeaderText="WIP REJ" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="sendingTo" HeaderText="Sending To"></asp:BoundColumn>
                            <asp:BoundColumn DataField="paintProcess" HeaderText="Process"></asp:BoundColumn>

                            <asp:BoundColumn DataField="description" HeaderText="Description"></asp:BoundColumn>
                            <asp:BoundColumn DataField="updatedTime" HeaderText="Updated Time"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table> 
    </div>
</asp:Content>