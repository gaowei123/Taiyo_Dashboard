<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MouldingInventoryTransfer.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.Moulding.MouldingInventoryTransfer" %>


<asp:Content runat="server" ContentPlaceHolderID ="MainContent"  ID="Test" >
   
    <link href="../Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />  
    <link href="/Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />
    <script src="../js/Dashboard.js">
    </script>

    <script>
        function LoseFuces(obj) {

            var text = obj.value;
            var lastChar = text.substring(text.length - 1);

            if (lastChar == "(" || lastChar == "W" || lastChar == ")") {
                obj.value = text.substring(0,text.length-1)
            }

            //js 主动失去焦点来触发 asp textbox 的 ontextchanged 事件
            if (obj.value.length == 13) {
                obj.blur();
            }
        }

        function CheckValidation(objFrom, objTo, dateType) {
            try {
                //alert('objto: ' + objTo);

                var textValue = objFrom.value;

                if (textValue == '') {
                    return;
                }

                if (dateType == 'sn') {
                    var re = /^[0-9]*$/;
                    if (!re.test(textValue)) {
                        alert("Please key in number !");
                        objFrom.value = "";
                        objFrom.focus();
                        return;
                    }else{

                        if (textValue.length > 3) {
                            objFrom.value = textValue.substring(0, 3);
                        }

                    }

                    
                    return;
                }


                if (textValue.length > 2) {
                    objFrom.value = textValue.substring(0, 2);
                    if (objTo != null) {
                        //objTo.value = textValue.substring(3, 1);
                        objTo.focus();
                    }
                } else {
                   // alert(textValue + 'length <=2');

                    var re = /^[0-9]*$/;
                    if (!re.test(textValue)) {
                        alert("Please key in number !");
                        objFrom.value = "";
                        objFrom.focus();
                        return;
                    }
                    
                    var date = parseInt(textValue);
                    if (dateType == 'day') {
                        if (date > 31) {
                            alert("Out of range,Please key in 01-31 !");
                            objFrom.value = '';
                            objFrom.focus();
                            return;
                        }
                    } else if (dateType == 'month') {
                        if (date > 12) {
                            alert("Out of range,Please key in 01-12 !");
                            objFrom.value = '';
                            objFrom.focus();
                            return;
                        }
                    }

                 
                    //alert('text length: '+ textValue.length)

                    if (objTo != null && textValue.length == 2) {
                       // alert('come in');
                        objTo.focus();
                    }
                }

            } catch (e) {
                alert(e);
            }
        }


    </script>

     <div style="width: 70%; align-items:center;margin:auto">

        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="4"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="" Font-Size="12" ForeColor="White"/>
                </td>
            </tr>
            

            <tr style ="width: 100%">   
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 10%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Add Lot Count :" Width="100%"></asp:Label>  
                </td>
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;"  >
                     <asp:Label runat="server" ID="lb_AddedCount" Width="100%"></asp:Label>  
                </td>
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Job No :" Width="100%"></asp:Label>
                </td>
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_JobID" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled" oninput="LoseFuces(this)" OnTextChanged="txt_JobID_TextChanged" AutoPostBack="true"></asp:TextBox>
                </td>
            </tr>

            <tr style ="width: 100%">   
                
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" ID="lb_Lotno" Text="Lot No :" ></asp:Label> 
                    <asp:TextBox runat="server" ID="txt_LotNo" Width="60%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled"></asp:TextBox>
                </td>
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 15%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" ID="lb_partno" Text="Part No :" ></asp:Label> 
                    <asp:TextBox runat="server" ID="txt_PartNumber" Width="60%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled"></asp:TextBox>
                </td>
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;"  >
                     <asp:Label runat="server" ID="lb_InQuantity" Text="In Quantity:" ></asp:Label> 
                    <asp:TextBox runat="server" ID="txt_InQuantity" Width="60%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled"></asp:TextBox>
                </td>
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;"  >
                    <asp:CheckBox runat="server" ID="cb_Print" Text="Print" AutoPostBack="true" OnCheckedChanged="cb_Print_CheckedChanged" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btn_add" Text="Add" Width="50%" UseSubmitBehavior="false"  BorderStyle="Solid" BorderWidth="1px" CssClass="btn-primary" Height="30px" OnClick="btn_add_Click" />
                </td>
            </tr>

            <tr style ="width: 100%">
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="4"  >
                    <asp:Label  runat="server" Text="Added Lot Infomation" Font-Size="Large" ></asp:Label> 
                </td>
            </tr>

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;  font-weight: bold;" colspan="4">
                    <asp:DataGrid runat="server" CssClass="table table-hover" ID ="dg_AddedInventoryList" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" Width="100%"  HorizontalAlign="Center"  AutoGenerateColumns="False" >
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
                            <asp:BoundColumn DataField="startTime" HeaderText="MFG Time"></asp:BoundColumn>
                            <asp:BoundColumn DataField="pqcQuantity" HeaderText="WIP REJ" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="sendingTo" HeaderText="Sending To"></asp:BoundColumn>
                            <asp:BoundColumn DataField="description" HeaderText="Description"></asp:BoundColumn>
                            <asp:BoundColumn DataField="updatedTime" HeaderText="Updated Time"></asp:BoundColumn>

                          
                            <%--<asp:TemplateColumn ItemStyle-HorizontalAlign="Center" HeaderText="Delete" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Button ID="btn_Delete" Runat="server" Text="×" CommandName="Delete" Index='<%# ((DataGridItem)Container).ItemIndex %>' CssClass="btn-danger"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateColumn>--%>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>

            <%--<tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; vertical-align:middle; height: 50px; width:50%"  align="center"  colspan="2" > 
                    <asp:Button runat="server" ID="btn_generate" Text="Generate"  Width="50%" UseSubmitBehavior="false" OnClientClick="if (typeof(Page_ClientValidate) == 'function') { if (Page_ClientValidate() == false) { return false; }};this.value='Process...';this.disabled=true;"  BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Height="30px" OnClick="btn_generate_Click"/>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; vertical-align:middle;  height: 50px; width:50%"  align="center"   colspan="2"> 
                    <asp:Button runat="server" ID="btn_cancel" Text="Cancel"  Width="50%" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-danger" Height="30px"  OnClick="btn_cancel_Click"/>
                </td>
            </tr>--%>


            </table> 
              
    </div>
    
           

    
           
</asp:Content>