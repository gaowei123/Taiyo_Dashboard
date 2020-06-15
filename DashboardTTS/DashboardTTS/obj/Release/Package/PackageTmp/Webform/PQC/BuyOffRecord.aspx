<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="BuyOffRecord.aspx.cs" Inherits="DashboardTTS.Webform.PQC.BuyOffRecord" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>

   

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >
    <link href="../css/Dashboard.css" rel="stylesheet" />



    <script>
        function LoseFuces(obj) {
            if (obj.value.length == 13) {
                obj.blur();
            }
        }
    </script>


    <div runat="server" id="divMain" style="width: 85%; height: 257px; align-items:center;margin:auto">
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 1px; table-layout:fixed; line-height: 10px; vertical-align: 10%;">                            
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="6"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="PAINTING_1st PIECE BUY-OFF RECORD" Font-Size="12pt" ForeColor="White" meta:resourcekey="lblUserHeaderResource1"/>
                </td>
            </tr> 

            <tr style ="width: 100%"> 
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Job No:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtJobNumber" AutoCompleteType="None" Width="100%" Height="23px" OnTextChanged="txtJobNumber_TextChanged" oninput="LoseFuces(this)" AutoPostBack="true"></asp:TextBox>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Setup Rej Qty:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtSetupRejQty" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                </td>

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="QA Reliability Test Qty:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtQATestQty" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                </td>
            </tr>

            
            <tr style ="width: 100%"> 

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width:50%; font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" align="center">
                    <span><h4><b>UNDER COAT</b></h4></span>
                </td>

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width:50%;  font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" align="center">
                    <span><h4><b>MIDDLE COAT</b></h4></span>
                </td>

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width:50%; font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" align="center">
                    <span><h4><b>TOP COAT</b></h4></span>
                </td>
               
            </tr>  
                

            <tr style ="width: 100%"> 
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Date:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <igsch:WebDateChooser ID="infUnderCoatDate" runat="server" Height="23px" Width="100%"  Value="" />
                </td>

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Date:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <igsch:WebDateChooser ID="infMiddleCoatDate" runat="server" Height="23px" Width="100%"  Value="" />
                </td>

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Date:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;">
                    <igsch:WebDateChooser ID="infTopCoatDate" runat="server" Height="23px" Width="100%"  Value="" />
                </td>
            </tr>
            
            <tr style ="width: 100%"> 
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Running Time:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <table style="width:100%">
                        <tr style="width:100%">
                            <td style="width:45%;height:100%"><asp:DropDownList runat="server" ID="ddlUnderCoatRunTimeHH" Width="100%" Height="23px"></asp:DropDownList></td>
                            <td style="width:10%" align="center">:</td>
                            <td style="width:45%;height:100%"><asp:DropDownList runat="server" ID="ddlUnderCoatRunTimeMM" Width="100%" Height="23px"></asp:DropDownList></td>
                        </tr>
                    </table>
                </td>

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Running Time:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <table style="width:100%">
                        <tr style="width:100%">
                            <td style="width:45%;height:100%"><asp:DropDownList runat="server" ID="ddlMiddleCoatRunTimeHH" Width="100%" Height="23px"></asp:DropDownList></td>
                            <td style="width:10%" align="center">:</td>
                            <td style="width:45%;height:100%"><asp:DropDownList runat="server" ID="ddlMiddleCoatRunTimeMM" Width="100%" Height="23px"></asp:DropDownList></td>
                        </tr>
                    </table>
                </td>

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Running Time:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;">
                    <table style="width:100%">
                        <tr style="width:100%">
                            <td style="width:45%;height:100%"><asp:DropDownList runat="server" ID="ddlTopCoatRunTimeHH" Width="100%" Height="23px"></asp:DropDownList></td>
                            <td style="width:10%" align="center">:</td>
                            <td style="width:45%;height:100%"><asp:DropDownList runat="server" ID="ddlTopCoatRunTimeMM" Width="100%" Height="23px"></asp:DropDownList></td>
                        </tr>
                    </table>
                </td>
            </tr>

            

            <tr style ="width: 100%"> 
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Machine No:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList runat="server" ID="ddlUnderCoatMachineNo" Width="100%" Height="23px"></asp:DropDownList>
                </td>

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Machine No:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList runat="server" ID="ddlMiddleCoatMachineNo" Width="100%" Height="23px"></asp:DropDownList>
                </td>

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Machine No:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList runat="server" ID="ddlTopCoatMachineNo" Width="100%" Height="23px"></asp:DropDownList>
                </td>
            </tr>

            <%--<tr style ="width: 100%"> 
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Coat:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtUnderCoat" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                </td>

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Coat:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtMiddleCoat" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                </td>

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Coat:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtTopCoat" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                </td>
            </tr>--%>

            <tr style ="width: 100%"> 
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Paint Lot:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtUnderCoatPaintLot" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                </td>

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Paint Lot:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtMiddleCoatPaintLot" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                </td>

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Paint Lot:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtTopCoatPaintLot" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                </td>
            </tr>

            <tr style ="width: 100%"> 
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Thinners Lot:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtUnderCoatThinnersLot" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                </td>

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Thinners Lot:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtMiddleCoatThinnersLot" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                </td>

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Thinners Lot:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtTopCoatThinnersLot" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                </td>
            </tr>

            <tr style ="width: 100%"> 
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Painting Thickness:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtUnderCoatThickness" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                </td>

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Painting Thickness:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtMiddleCoatThickness" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                </td>

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Painting Thickness:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtTopCoatThickness" AutoCompleteType="None" Width="100%" Height="23px"></asp:TextBox>
                </td>
            </tr>

            <tr style ="width: 100%"> 
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Painting PIC:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList runat="server" ID="ddlUnderCoatPIC" Width="100%" Height="23px"></asp:DropDownList>
                </td>

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Painting PIC:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList runat="server" ID="ddlMiddleCoatPIC" Width="100%" Height="23px"></asp:DropDownList>
                </td>

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Painting PIC:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList runat="server" ID="ddlTopCoatPIC" Width="100%" Height="23px"></asp:DropDownList>
                </td>
            </tr>

            <tr style ="width: 100%"> 
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Oven In Time:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <table style="width:100%">
                        <tr style="width:100%">
                            <td style="width:45%;height:100%"><asp:DropDownList runat="server" ID="ddlUnderCoatOvenTimeHH" Width="100%" Height="23px"></asp:DropDownList></td>
                            <td style="width:10%" align="center">:</td>
                            <td style="width:45%;height:100%"><asp:DropDownList runat="server" ID="ddlUnderCoatOvenTimeMM" Width="100%" Height="23px"></asp:DropDownList></td>
                        </tr>
                    </table>
                </td>

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Oven In Time:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC;  font-family: 'Arial Unicode MS'; height: 50px;">
                    <table style="width:100%">
                        <tr style="width:100%">
                            <td style="width:45%;height:100%"><asp:DropDownList runat="server" ID="ddlMiddleCoatOvenTimeHH" Width="100%" Height="23px"></asp:DropDownList></td>
                            <td style="width:10%" align="center">:</td>
                            <td style="width:45%;height:100%"><asp:DropDownList runat="server" ID="ddlMiddleCoatOvenTimeMM" Width="100%" Height="23px"></asp:DropDownList></td>
                        </tr>
                    </table>
                </td>

                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Oven In Time:"></asp:Label>
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px;">
                    <table style="width:100%">
                        <tr style="width:100%">
                            <td style="width:45%;height:100%"><asp:DropDownList runat="server" ID="ddlTopCoatOvenTimeHH" Width="100%" Height="23px"></asp:DropDownList></td>
                            <td style="width:10%" align="center">:</td>
                            <td style="width:45%;height:100%"><asp:DropDownList runat="server" ID="ddlTopCoatOvenTimeMM" Width="100%" Height="23px"></asp:DropDownList></td>
                        </tr>
                    </table>
                </td>
            </tr>



            <tr style ="width: 100%"> 
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width:50%; font-family: 'Arial Unicode MS'; height: 50px;" colspan="3" align="center">
                    <asp:Button ID="btn_Confirm" runat="server" Text="Confirm" OnClick="btn_Confirm_Click" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-success" Height="30px" Width="40%" />
                </td>
                <td style="padding: 10px 10px 10px 10px; border: 1px solid #CCCCCC; width:50%;  font-family: 'Arial Unicode MS'; height: 50px;" colspan="3" align="center">
                    <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" OnClick="btn_Cancel_Click" BorderStyle="Solid" BorderWidth="1px" CssClass="btn-danger" Height="30px" Width="40%" />
                </td>
            </tr>


        </table>    
	</div>
                    
    </div>
                    
 </asp:Content>
