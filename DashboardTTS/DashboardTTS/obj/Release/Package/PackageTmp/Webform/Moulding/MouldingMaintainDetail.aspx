<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MouldingMaintainDetail.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.MouldingMaintainDetail" %>
<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
    <style>
        input{
            height:30px;
        }

        /*在最外面的div设置字体, 后续元素会继承改字体属性*/
        .mainDiv{
            font-family:'Arial Unicode MS';
            width: 100%;
            margin:0 auto
        }
        .mainDiv>table{
            width: 100%; 
            border-collapse: separate; 
            border-spacing: 10px;
        }
        .mainDiv>table>tbody>tr{
            width:100%;
        }
        /*设置每一行每一列*/
        .mainDiv>table>tbody>tr>td{
            border: 1px solid #CCCCCC;
            padding: 10px 10px 10px 25px;
            height: 50px;
        }
        /*选中第一行tr中的td,设置背景和文字*/
        .mainDiv>table>tbody>tr:nth-child(1)>td{
            background-color: #003366; 
            font-weight: bold;
            color:white;
            font-size:18px;
        }
        /*选中第一行tr中的td的img,设置长宽*/
        .mainDiv>table>tbody>tr:nth-child(1)>td>img{
            width: 15px; 
            height: 15px;
        }
        /*选中最后一行tr中的td,让按钮居中*/
        .mainDiv>table>tbody>tr:nth-last-child(1) >td{
            text-align:center;
        }

        .w2{
            width: 20%;
        }
        .w3{
            width: 20%;
        }   
        .w4{
            width:40%;
        }
        .w5{
            width:50%;
        }  
    </style>
     
    <div class="mainDiv">
        <table>
            <tbody>
                <tr>
                    <td colspan="4"> 
                        <img src="../../Resources/Images/headericon.gif" alt="" style=""/> 
                        <Asp:label ID="lblUserHeader" runat="server" Text="Moulding Maintenancen Detail"/>
                    </td>
                </tr>
                <tr>
                    <td class="w2">Check Period :</td>
                    <td class="w3">
                        <asp:DropDownList ID="ddl_CheckPeriod" runat="server" Height="30px" Width="100%"></asp:DropDownList>
                    </td>
                    <td class="w2"></td>
                    <td class="w3">
                        <asp:Button runat="server" ID="btn_generate" Text="Generate" CssClass="btn-success w4" OnClick="btn_generate_Click"/> 
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btn_Add" Text="Add Item" CssClass="btn-primary w4" OnClick="btn_Add_Click"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblResult" runat="server"></asp:Label> 

                        <asp:DataGrid runat="server" CssClass="table" ID ="dg_CheckItemList" GridLines="None" Width="100%" AutoGenerateColumns="False" >
                            <AlternatingItemStyle BackColor="White"/>
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"  Height="30px" HorizontalAlign="Left" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="left" />                           
                            <Columns>
                                <asp:BoundColumn DataField="CheckPeriod" HeaderText="Check Period" HeaderStyle-Width="15%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CheckItem" HeaderText="Check Item" HeaderStyle-Width="35%"  Visible="false"></asp:BoundColumn>
                                <asp:ButtonColumn DataTextField="CheckItem" HeaderText="Check Item" HeaderStyle-Width="35%" CommandName="LinkDetail"></asp:ButtonColumn>

                                <asp:TemplateColumn HeaderText="MachineID">
                                    <ItemTemplate>
                                        <asp:DropDownList runat="server" ID="ddl_MachineID" Width="60%" >
                                            <asp:ListItem Value="1,2,3,4,5,6,7,8,9">All</asp:ListItem>
                                            <asp:ListItem Value="1">No.1</asp:ListItem>
                                            <asp:ListItem Value="2">No.2</asp:ListItem>
                                            <asp:ListItem Value="3">No.3</asp:ListItem>
                                            <asp:ListItem Value="4">No.4</asp:ListItem>
                                            <asp:ListItem Value="5">No.5</asp:ListItem>
                                            <asp:ListItem Value="6">No.6</asp:ListItem>
                                            <asp:ListItem Value="7">No.7</asp:ListItem>
                                            <asp:ListItem Value="8">No.8</asp:ListItem>
                                            <asp:ListItem Value="9">No.9</asp:ListItem>
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
                <tr>
                    <td colspan="2">
                        <asp:Button  runat="server" Text="Submit" ID="btn_submit" OnClick="btn_submit_Click" CssClass="btn-success w5"/>
                    </td>
                    <td colspan="2">
                        <asp:Button  runat="server" Text="Cancel" ID="btn_cancel" OnClick="btn_cancel_Click" CssClass="btn-danger w5"/>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>

    <script>
        function Cancel(){
            if (confirm('Your action will not be saved, are you sure?') == true) 
                window.location.href = "./MouldingMaintain.aspx";
        }
    </script>
</asp:Content>