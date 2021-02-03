<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MachineInformation.aspx.cs" Inherits="DashboardTTS.Webform.Molding.MachineInformation" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >

    <%--2021-2-3, adjust ui style --%>
    <style>
        .mainDiv{
            margin: 0, auto;
            padding-top: 0px;
            font-family: 'Arial Unicode MS';
        }
        table,tr{
            width:100%;
        }       
        td{
            padding:4px;
            border: 1px solid #CCCCCC;
        }
        .title{
            background-color: #003366; 
            font-weight: bold;
            height:50px;
        }
        .title>img{
            width: 15px; 
            height: 15px
        }
        .title>span{
            font-size:18px;
            color:white;
        }
        h4{
            text-align:center;
            font-weight:bold;
        }
    </style>

     <div class="mainDiv">
        <table>
            <tr>
                <td class="title" colspan="2"> 
                    <img src="../../Resources/Images/headericon.gif" alt=""/> 
                    <span>Moulding Machine Information</span>
                </td>
            </tr>


            <%--Main--%>
            <tr>
                <td colspan="2"><h4>Injection Moulding Machine Main Specification</h4></td>
            </tr>
            <tr>
                <td colspan="2"> 
                    <asp:DataGrid runat="server" CssClass="table" Width ="100%" ID ="dg_Main" AutoGenerateColumns="false" >
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                        <Columns>
                            <asp:ButtonColumn  DataTextField="machineID" HeaderText="ID" CommandName="Update"></asp:ButtonColumn>
                            <asp:BoundColumn DataField="machineID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="maker" HeaderText="Maker"></asp:BoundColumn>
                            <asp:BoundColumn DataField="info" HeaderText="Info."></asp:BoundColumn>
                            <asp:BoundColumn DataField="model" HeaderText="Model"></asp:BoundColumn>
                            <asp:BoundColumn DataField="dateOfManu" HeaderText="Date Of<br>Manufacture"></asp:BoundColumn>
                            <asp:BoundColumn DataField="screwDiameter" HeaderText="Screw<br>Diameter"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MaxOPNStroke" HeaderText="Max OPN<br>Stroke"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EJTStroke" HeaderText="EJT<br>Stroke"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TiebarDistance" HeaderText="Tiebar<br>Distance"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MinMoldSize" HeaderText="Min Mold Size<br>(H x V)"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MinMoldThickness" HeaderText="Min Mold<br>Thickness"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Dimensions" HeaderText="M/C Dimensions<br>(L x W x H)"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DateTime" HeaderText="DateTime" Visible="false"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <%--Main--%>


            <%--Injection--%>
            <tr>
                <td colspan="2"><h4>Injection Moulding Machine</h4></td>
            </tr>
            <tr>
                <td colspan="2"> 
                    <asp:DataGrid runat="server" CssClass="table" ID ="dg_Machine" AutoGenerateColumns="false" >
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />           
                        <Columns>
                            <asp:ButtonColumn  DataTextField="machineID" HeaderText="ID" CommandName="Update"></asp:ButtonColumn>
                            <asp:BoundColumn DataField="machineID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="type" HeaderText="Type"></asp:BoundColumn>
                            <asp:BoundColumn DataField="makerModel" HeaderText="Maker/Model"></asp:BoundColumn>
                            <asp:BoundColumn DataField="serialNo" HeaderText="Serial No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="dateOfManu" HeaderText="Date Of Manufacture"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CTRL" HeaderText="CTRL"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DateTime" HeaderText="DateTime" Visible="false"></asp:BoundColumn>                         
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <%--Injection--%>


            <%--Robot Arm--%>
            <tr>
                <td colspan="2"><h4>Robot Arm</h4></td>
            </tr>
            <tr>
                <td colspan="2"> 
                    <asp:DataGrid runat="server" CssClass="table" ID ="dg_RobotArm" AutoGenerateColumns="false" >
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />    
                        <Columns>
                            <asp:ButtonColumn  DataTextField="machineID" HeaderText="ID" CommandName="Update"></asp:ButtonColumn>
                            <asp:BoundColumn DataField="machineID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="model" HeaderText="Model"></asp:BoundColumn>
                            <asp:BoundColumn DataField="serialNo" HeaderText="Serial No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="controllerType" HeaderText="Controller Type"></asp:BoundColumn>
                            <asp:BoundColumn DataField="controllerSerialNo" HeaderText="Controller Serial No"></asp:BoundColumn>
                            <asp:BoundColumn DataField="date" HeaderText="Date"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <%--Robot Arm--%>


            <%--Temperature--%>
            <tr>
                <td><h4>Temperature Controller</h4></td>
                <td><h4>Hopper Dryer</h4></td>
            </tr>
            <tr>
                <td> 
                    <asp:DataGrid runat="server" CssClass="table" ID ="dg_Temp" AutoGenerateColumns="false" >
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                        <Columns>
                            <asp:ButtonColumn  DataTextField="machineID" HeaderText="ID" CommandName="Update"></asp:ButtonColumn>
                            <asp:BoundColumn DataField="machineID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="maker" HeaderText="Maker"></asp:BoundColumn>
                            <asp:BoundColumn DataField="model" HeaderText="Model"></asp:BoundColumn>
                            <asp:BoundColumn DataField="date" HeaderText="Date"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
                <td> 
                    <asp:DataGrid runat="server" CssClass="table" ID ="dg_Dryer" AutoGenerateColumns="false" >
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                        <Columns>
                            <asp:ButtonColumn  DataTextField="machineID" HeaderText="ID" CommandName="Update"></asp:ButtonColumn>
                            <asp:BoundColumn DataField="machineID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="maker" HeaderText="Maker"></asp:BoundColumn>
                            <asp:BoundColumn DataField="model" HeaderText="Model"></asp:BoundColumn>
                            <asp:BoundColumn DataField="date" HeaderText="Date"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <%--Temperature--%>

        </table> 
    </div>
</asp:Content>