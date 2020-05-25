<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Output.aspx.cs" Inherits="DashboardTTS.Webform.Reports.Output" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
    <link href="../Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />  
    <link href="/Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />

     <script src="../js/Dashboard.js"> </script>

    
     
     <%--<div style="position: relative; width: 1346px; height: 70px; margin: auto; top: 0px; left: 0px;">--%>
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="3"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lb_Header"   runat="server" Font-Names="Arial Unicode MS"  Text="" Font-Size="12" ForeColor="White"/>
                    </td>
            </tr>
            <tr style ="width: 100%"> 
                          
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; text-align:center; height: 50px; " colspan="1" > 
                             
                    </td>
                <td style = "background-position: right; padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; text-align:center; height: 50px; width: auto;" colspan="1" > 
                    <igsch:WebDateChooser ID="infDchFrom" runat="server"   Width="150px" Value="" >
                    </igsch:WebDateChooser>
                </td>  
                                 
                  <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; text-align:center; height: 50px; " colspan="1" > 
                             <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Height="30px" />
                         </td>                             
                  </tr>
            <tr style ="width: 100%"> 
                                 
                
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; text-align:center; height: 50px;" colspan="3"> 
                        <asp:Label ID="Label1" runat="server" Text="Moulding Department" Font-Bold="true" Font-Size="Large" ></asp:Label> 
                </td>
                </tr>

                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="3"> 
                         <%--<asp:Table runat="server" ID ="tbl_Moulding_Day">
                            <asp:TableHeaderRow runat="server">
                                <asp:TableCell runat="server">12345</asp:TableCell>
                                <asp:TableCell runat="server">123123123</asp:TableCell>
                                <asp:TableCell runat="server">4444444</asp:TableCell>
                                <asp:TableCell runat="server">555555</asp:TableCell>
                                <asp:TableCell runat="server">6666666</asp:TableCell>
                                <asp:TableCell runat="server">777777</asp:TableCell>
                                <asp:TableCell runat="server">88888</asp:TableCell>
                                <asp:TableCell runat="server">99999</asp:TableCell>
                                <asp:TableCell runat="server">00000</asp:TableCell>

                            </asp:TableHeaderRow>
                            <asp:TableRow runat="server">
                            </asp:TableRow>
                            <asp:TableRow runat="server">
                            </asp:TableRow>
                            <asp:TableRow runat="server">
                            </asp:TableRow>
                            <asp:TableRow runat="server">
                            </asp:TableRow>
                            <asp:TableRow runat="server">
                            </asp:TableRow>
                            <asp:TableRow runat="server">
                            </asp:TableRow>
                        </asp:Table>--%>
                        <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dg_Moulding_Day" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="3" AutoGenerateColumns="False" ShowFooter="True" >
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <EditItemStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:ButtonColumn  DataTextField="Model" HeaderText="Day_Shift_Total_MC_Used" CommandName="Update" FooterText="Text">
                                    <HeaderStyle Width="200px"  HorizontalAlign="left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:ButtonColumn>
                                <%--<asp:BoundColumn DataField="TechUser" HeaderText="MANPOWER Tech">
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="OpUser" HeaderText="MANPOWER OP">
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AttendanceUser" HeaderText="Attendance">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AnnualLeaveUser" HeaderText="Annual Leave">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MedicalLeaveUser" HeaderText="Medical Leave">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AbsentUser" HeaderText="Absent">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>--%>
                                  <asp:BoundColumn DataField="TargetMCRunning" HeaderText="TargetMC Running">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="ActualMCRunning" HeaderText="ActualMC Running">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ProdHours" HeaderText="PROD HRS">
                                    <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="TargetQty" HeaderText="TARGET pcs">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TotalQty" HeaderText="TOTAL OUTPUT pcs">
                                    <HeaderStyle Width="100px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="PassQty" HeaderText="ACTUAL">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RejectQty" HeaderText="REJ QTY">
                                    <HeaderStyle Width="250px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="RejRate" HeaderText="REJ Rate">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MachineUsedRate" HeaderText="MC Used Rate">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                         <br />
        <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dg_Moulding_Night" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="3" AutoGenerateColumns="False" ShowFooter="True" >
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <EditItemStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:ButtonColumn  DataTextField="Model" HeaderText="Night_Shift_Total_MC_Used" CommandName="Update" FooterText="Text">
                                    <HeaderStyle Width="200px"  HorizontalAlign="left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:ButtonColumn>
                                <%--<asp:BoundColumn DataField="TechUser" HeaderText="MANPOWER Tech">
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="OpUser" HeaderText="MANPOWER OP">
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AttendanceUser" HeaderText="Attendance">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AnnualLeaveUser" HeaderText="Annual Leave">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MedicalLeaveUser" HeaderText="Medical Leave">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AbsentUser" HeaderText="Absent">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>--%>
                                 <asp:BoundColumn DataField="TargetMCRunning" HeaderText="TargetMC Running">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="ActualMCRunning" HeaderText="ActualMC Running">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ProdHours" HeaderText="PROD HRS">
                                    <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="TargetQty" HeaderText="TARGET pcs">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TotalQty" HeaderText="TOTAL OUTPUT pcs">
                                    <HeaderStyle Width="100px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="PassQty" HeaderText="ACTUAL">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RejectQty" HeaderText="REJ QTY">
                                    <HeaderStyle Width="250px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="RejRate" HeaderText="REJ Rate">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MachineUsedRate" HeaderText="MC Used Rate">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
              <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dg_Moulding_QA" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="3" AutoGenerateColumns="False" ShowFooter="True" >
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <EditItemStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:ButtonColumn  DataTextField="Model"  HeaderText="100% Checking" CommandName="Update" FooterText="Text">
                                    <HeaderStyle Width="200px" HorizontalAlign="left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:ButtonColumn>
                                <%--<asp:BoundColumn DataField="TechUser" HeaderText="MANPOWER Tech">
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="OpUser" HeaderText="MANPOWER OP">
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AttendanceUser" HeaderText="Attendance">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AnnualLeaveUser" HeaderText="Annual Leave">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MedicalLeaveUser" HeaderText="Medical Leave">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AbsentUser" HeaderText="Absent">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>--%>
                                <asp:BoundColumn DataField="TargetMCRunning" HeaderText="TargetMC Running">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="ActualMCRunning" HeaderText="ActualMC Running">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ProdHours" HeaderText="PROD HRS">
                                    <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="TargetQty" HeaderText="TARGET pcs">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TotalQty" HeaderText="TOTAL OUTPUT pcs">
                                    <HeaderStyle Width="100px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="PassQty" HeaderText="ACTUAL">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RejectQty" HeaderText="REJ QTY">
                                    <HeaderStyle Width="250px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="RejRate" HeaderText="REJ Rate">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MachineUsedRate" HeaderText="MC Used Rate">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>

                    </td>
                </tr>



                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; text-align:center; height: 50px;" colspan="3"> 
                        <asp:Label ID="Label2" runat="server" Text="Painting Department" Font-Bold="true" Font-Size="Large" ></asp:Label> 
                    </td>
                </tr>

                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="3"> 
              <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dg_Painting" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="False" ShowFooter="True" >
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <EditItemStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:ButtonColumn  DataTextField="Model"  HeaderText="Day_Shift_Total_MC_Used" CommandName="Update" FooterText="Text">
                                    <HeaderStyle Width="200px" HorizontalAlign="left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:ButtonColumn>
                                <%--<asp:BoundColumn DataField="TechUser" HeaderText="MANPOWER Tech">
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="OpUser" HeaderText="MANPOWER OP">
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AttendanceUser" HeaderText="Attendance">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AnnualLeaveUser" HeaderText="Annual Leave">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MedicalLeaveUser" HeaderText="Medical Leave">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AbsentUser" HeaderText="Absent">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>--%>
                                <asp:BoundColumn DataField="TargetMCRunning" HeaderText="TargetMC Running">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="ActualMCRunning" HeaderText="ActualMC Running">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ProdHours" HeaderText="PROD HRS">
                                    <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="TargetQty" HeaderText="TARGET">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TotalQty" HeaderText="TOTAL OUTPUT Tray">
                                    <HeaderStyle Width="100px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="PassQty" HeaderText="ACTUAL pcs">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RejectQty" HeaderText="REJ QTY">
                                    <HeaderStyle Width="250px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="RejRate" HeaderText="REJ Rate">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MachineUsedRate" HeaderText="MC Used Rate">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>

                </td>
                </tr>

            
                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; text-align:center; height: 50px;" colspan="3"> 
                        <asp:Label ID="Label3" runat="server" Text="Laser Department" Font-Bold="true" Font-Size="Large" ></asp:Label> 
                    </td>
                </tr>

                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="3"> 
              <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dg_Laser_Day" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="False" ShowFooter="True" >
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <EditItemStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:ButtonColumn  DataTextField="Model"  HeaderText="Day_Shift_Total_MC_Used" CommandName="Update" FooterText="Text">
                                    <HeaderStyle Width="200px" HorizontalAlign="left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:ButtonColumn>
                                <%--<asp:BoundColumn DataField="TechUser" HeaderText="MANPOWER Tech">
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="OpUser" HeaderText="MANPOWER OP">
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AttendanceUser" HeaderText="Attendance">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AnnualLeaveUser" HeaderText="Annual Leave">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MedicalLeaveUser" HeaderText="Medical Leave">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AbsentUser" HeaderText="Absent">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>--%>
                                <asp:BoundColumn DataField="TargetMCRunning" HeaderText="TargetMC Running">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="ActualMCRunning" HeaderText="ActualMC Running">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ProdHours" HeaderText="PROD HRS">
                                    <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="TargetQty" HeaderText="TARGET">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TotalQty" HeaderText="TOTAL OUTPUT">
                                    <HeaderStyle Width="100px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="PassQty" HeaderText="ACTUAL">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RejectQty" HeaderText="REJ QTY">
                                    <HeaderStyle Width="250px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="RejRate" HeaderText="REJ Rate">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MachineUsedRate" HeaderText="MC Used Rate">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                        <br />
<asp:DataGrid runat="server" Width ="100%" CssClass="table table-hover" ID ="dg_Laser_Night" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="3" AutoGenerateColumns="False" ShowFooter="True" >
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <EditItemStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:ButtonColumn  DataTextField="Model"  HeaderText="Night_Shift_Total_MC_Used" CommandName="Update" FooterText="Text">
                                    <HeaderStyle Width="200px" HorizontalAlign="left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:ButtonColumn>
                                <%--<asp:BoundColumn DataField="TechUser" HeaderText="MANPOWER Tech">
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="OpUser" HeaderText="MANPOWER OP">
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AttendanceUser" HeaderText="Attendance">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AnnualLeaveUser" HeaderText="Annual Leave">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MedicalLeaveUser" HeaderText="Medical Leave">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AbsentUser" HeaderText="Absent">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>--%>
                                <asp:BoundColumn DataField="TargetMCRunning" HeaderText="TargetMC Running">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="ActualMCRunning" HeaderText="ActualMC Running">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ProdHours" HeaderText="PROD HRS">
                                    <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="TargetQty" HeaderText="TARGET">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TotalQty" HeaderText="TOTAL OUTPUT">
                                    <HeaderStyle Width="100px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="PassQty" HeaderText="ACTUAL">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RejectQty" HeaderText="REJ QTY">
                                    <HeaderStyle Width="250px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="RejRate" HeaderText="REJ Rate">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MachineUsedRate" HeaderText="MC Used Rate">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                </td>
                </tr>

                    <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; text-align:center; height: 50px;" colspan="3"> 
                        <asp:Label ID="Label4" runat="server" Text="PQC Department Checking" Font-Bold="true" Font-Size="Large" ></asp:Label> 
                    </td>
                </tr>

                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="3"> 
              <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dg_PQC" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="False" ShowFooter="True" >
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <EditItemStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:ButtonColumn  DataTextField="Model"  HeaderText="PQC Department Checking" CommandName="Update" FooterText="Text">
                                    <HeaderStyle Width="200px" HorizontalAlign="left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:ButtonColumn>
                                <%--<asp:BoundColumn DataField="TechUser" HeaderText="MANPOWER Tech">
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="OpUser" HeaderText="MANPOWER OP">
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AttendanceUser" HeaderText="Attendance">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AnnualLeaveUser" HeaderText="Annual Leave">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MedicalLeaveUser" HeaderText="Medical Leave">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AbsentUser" HeaderText="Absent">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>--%>
                                <asp:BoundColumn DataField="TargetMCRunning" HeaderText="TargetMC Running">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="ActualMCRunning" HeaderText="ActualMC Running">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ProdHours" HeaderText="PROD HRS">
                                    <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="TargetQty" HeaderText="TARGET">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TotalQty" HeaderText="TOTAL OUTPUT">
                                    <HeaderStyle Width="100px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="PassQty" HeaderText="ACTUAL">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RejectQty" HeaderText="REJ QTY">
                                    <HeaderStyle Width="250px" />
                                </asp:BoundColumn>
                                  <asp:BoundColumn DataField="RejRate" HeaderText="REJ Rate">
                                      <HeaderStyle Width="60px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MachineUsedRate" HeaderText="MC Used Rate">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>

                </td>
                </tr>


                            
                <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                        &nbsp;</td>

                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                        &nbsp;</td>
                    
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                        &nbsp;</td>
                </tr>
                          
                         
            </table>  
</asp:Content>

