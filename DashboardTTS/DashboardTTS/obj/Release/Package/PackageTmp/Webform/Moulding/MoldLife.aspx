<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoldLife.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.Molding.MoldLife" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >

    <script>

       

        function setVisible(obj) {


            var btn = obj.parentElement.children[1];

            btn.style.visibility = "visible";

        }

    </script>

    <div style="width: auto; align-items:center;margin:auto">
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="5"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Mould Life & Mould Maintenance" Font-Size="12" ForeColor="White"/>
                </td>
            </tr>

            <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width:15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Mould ID :" Width="100%" style="margin-bottom: 0px"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList runat="server" ID="ddl_MouldChase" Width="100%"/> 
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    &nbsp;
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    &nbsp;
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Button runat="server" ID="btn_Generate" CssClass="btn-success" Height="30px" Text="Generate" Width="40%" OnClick="btn_Generate_Click"/>
                        &nbsp;&nbsp;
                    <asp:Button runat="server" ID="btn_Clean" CssClass="btn-danger" Height="30px" Text="Reset" Width="40%" OnClick="btn_Clean_Click"/>
                </td>
            </tr>

            <tr  id="trCleanItem"   style ="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width:15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Clean Item :" Width="100%" style="margin-bottom: 0px" Height="16px"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList runat="server" ID="ddl_CleaningItem" Width="100%"/>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Mould Needs to repair :" Width="179px" Height="16px"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 25%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:DropDownList runat="server" ID="ddl_OutSideRepair" Width="100%"> 
                        <asp:ListItem Value="No" Selected="True">No</asp:ListItem>
                        <asp:ListItem Value="Koei tool" >Koei tool</asp:ListItem>
                        <asp:ListItem Value="CCM" >CCM</asp:ListItem>      
                    </asp:DropDownList>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;">
                </td>
            </tr>

            <tr  id="trCleanItem"   style ="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width:15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Remarks :" Width="100px"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;" colspan="3">
                    <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txt_Remarks" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="66px" placeholder ="" TextMode="MultiLine"  ></asp:TextBox>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Button runat="server" ID="btnOK" Height="30px" Text="Submit" Width="40%" OnClick="btnOK_Click"/>
                        &nbsp;
                    <asp:Button runat="server" ID="btnCancel" Height="30px" Text="Cancel" Width="40%" OnClick="btnCancel_Click"/>
                </td>
            </tr>
            
            <tr style="width:100%;">
                <td  style = "padding: 10px 10px 10px 25px; text-align:center; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; font-weight: bold;" colspan="5" >
                    <asp:Label  runat="server" Text=" Mold Life "  Font-Names="Arial Unicode MS"  Font-Size="12" Width="100%" ></asp:Label>
                </td>
            </tr>

            <tr style ="width: 100%">   
                <td style = "padding: 25px 25px 25px 25px; border: 1px solid #CCCCCC; width:100%;  font-family: 'Arial Unicode MS'; height: 50px;" colspan="5" aria-hidden="False" aria-multiline="True" aria-multiselectable="True" aria-orientation="vertical" draggable="false">
                <asp:Label runat="server" ID="lblResult_ModeLife" Visible="false"></asp:Label>
                <asp:DataGrid runat="server" CssClass="table table-hover" ID ="dg_MoldLife" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" Width="100%"  HorizontalAlign="Center" AutoGenerateColumns="False" >
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775"  />
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="left" />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle HorizontalAlign="Left" />
                    <Columns>
                        <asp:BoundColumn DataField="MoldID" HeaderText="Mold ID" Visible="true"></asp:BoundColumn>
                        <%--<asp:BoundColumn DataField="MachineID" HeaderText="MachineID"></asp:BoundColumn>--%>
                        <%--<asp:BoundColumn DataField="PartNumberAll" HeaderText="PartNumberAll"></asp:BoundColumn>--%>
                        <asp:TemplateColumn HeaderText="MouldLife">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtMouldLife" Height="20px" Width="70"  AutoCompleteType="None" oninput="setVisible(this)" AutoPostBack="false"></asp:TextBox>
                                <asp:Button runat="server" ID="btnSubmit" Height="23px" Text="√" CommandName="submitMouldLife" CssClass="btn-danger" style="visibility:hidden" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="MouldLife" HeaderText="MouldLife" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Accumulate" HeaderText="Accumulate"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Clean1Qty" HeaderText="Next Accumulation Shots"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Clean1Time" HeaderText="Last Maint. Date"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Clean1TimeBy" HeaderText="Maint. Performed By"></asp:BoundColumn> 
                    </Columns>
                </asp:DataGrid>
                </td>
            </tr>

            <tr style="width:100%;">
                <td  style = "padding: 10px 10px 10px 25px; text-align:center; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; font-weight: bold;" colspan="5" >
                    <asp:Label  runat="server" Text=" Maintenance Record "  Font-Names="Arial Unicode MS"  Font-Size="12" Width="100%" ></asp:Label>  
                </td>
            </tr>
                
            <tr style ="width: 100%">   
                <td style = "padding: 25px 25px 25px 25px; border: 1px solid #CCCCCC; width:100%;  font-family: 'Arial Unicode MS'; height: 50px;" colspan="5" aria-hidden="False" aria-multiline="True" aria-multiselectable="True" aria-orientation="vertical" draggable="false">
                    
                <asp:Label runat="server" ID="lblResult_HisList" Visible="false"></asp:Label>

                <asp:DataGrid runat="server" ID ="dg_HisList" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" Width="100%"  HorizontalAlign="Center" AutoGenerateColumns="False" CssClass="table">
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775"  />
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="left" />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle HorizontalAlign="Left" />
                    <Columns>
                        <asp:BoundColumn DataField="MoldID" HeaderText="Mold ID" Visible="true"></asp:BoundColumn>
                        <%--<asp:BoundColumn DataField="MachineID" HeaderText="MachineID"></asp:BoundColumn>--%>
                        <%--<asp:BoundColumn DataField="PartNumberAll" HeaderText="PartNumberAll"></asp:BoundColumn>--%>
                        <asp:BoundColumn DataField="Accumulate" HeaderText="Accumulate"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Clean1Qty" HeaderText="Shots During Maintenance"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Clean1Time" HeaderText="Maint Date"></asp:BoundColumn>
                        <%--<asp:BoundColumn DataField="ChangeBy" HeaderText="Clean By"></asp:BoundColumn>--%>
                        <%--<asp:BoundColumn DataField="CreateTime" HeaderText="CreateTime"></asp:BoundColumn>--%>
                        <asp:BoundColumn DataField="Clean1TimeBy" HeaderText="Maint. Performed By"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Clean5TimeBy" HeaderText="Outside Repair"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Clean4TimeBy" HeaderText="Remarks"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
                </td>
            </tr>
        </table> 
    </div>
</asp:Content>