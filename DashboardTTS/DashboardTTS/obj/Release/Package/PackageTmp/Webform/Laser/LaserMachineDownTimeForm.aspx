<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="LaserMachineDownTimeForm.aspx.cs" Inherits="DashboardTTS.Webform.Laser.LaserMachineDownTimeForm" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
    <link href="../Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />  
    <link href="/Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />

    <script src="../js/Dashboard.js"> </script>

    <script type="text/javascript">
        function uploadFile(filePath) {
            if (filePath.length > 0) {
                __doPostBack('btnUploadFile', '');
            }
        }
    </script>
    
     
    <div style="width: 60%;align-items:center;margin:auto">
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="4"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Laser Equipment Preventive Maintenance & Check Form" Font-Size="12" ForeColor="White"/>
                </td>
            </tr>
                        
            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Date (dd/mm/yyy):" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_Date" Width="93%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled" ></asp:TextBox>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Machine ID :" Width="93%"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList ID="ddl_machineNo"  runat="server" Width="93%" Height="23px" BorderWidth="1px"  />
                </td>
            </tr>


            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Start Time: " Width="100%"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <igsch:WebDateChooser ID="infDch_Start" runat="server" Height="23px"   Width="93%" Value="" />
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" >
                    <table style="width:100%; height:100%;">
                        <tr style="width:100%; height:100%;">
                            <td style="width:25%; height:100%;"><asp:DropDownList runat="server" ID="ddl_Start_hh" Width="100%"></asp:DropDownList></td>
                            <td style="width:25%; height:100%;">&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label runat="server" Text="hh" ></asp:Label></td>
                            <td style="width:25%; height:100%;"><asp:DropDownList runat="server" ID="ddl_Start_mm" Width="100%"></asp:DropDownList></td>
                            <td style="width:25%; height:100%;">&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label runat="server" Text="mm" ></asp:Label></td>
                        </tr>
                    </table>
                </td>
                
            </tr>

            <tr>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="End Time: " Width="100%"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <igsch:WebDateChooser ID="infDch_End" runat="server" Height="23px"   Width="93%" Value="" />
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" >
                     <table style="width:100%; height:100%;">
                        <tr style="width:100%; height:100%;">
                            <td style="width:25%; height:100%;"><asp:DropDownList runat="server" ID="ddl_End_hh" Width="100%"></asp:DropDownList></td>
                            <td style="width:25%; height:100%;">&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label runat="server" Text="hh" ></asp:Label></td>
                            <td style="width:25%; height:100%;"><asp:DropDownList runat="server" ID="ddl_End_mm" Width="100%"></asp:DropDownList></td>
                            <td style="width:25%; height:100%;">&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label runat="server" Text="mm" ></asp:Label></td>
                        </tr>
                    </table>
                </td>
                
            </tr>
         

            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Part Running" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_PartRunning" Width="93%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled" ></asp:TextBox>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Attachment :" Width="93%"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:FileUpload runat="server" ID="MyFileUpload" ToolTip="Please click and choose a file"/>
                    <%--<asp:Textbox runat="server" ID="txt_UploadFile" Visible="false"/>--%>
                </td>
            </tr>




            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Break Down Cause: " Width="100%"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 70px;" colspan="3">
                    <asp:TextBox runat="server" ID="txt_BreakDownCause" TextMode="MultiLine" Height="70px" Width="98%"></asp:TextBox>
                </td>
            </tr>

            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Corrective Action Taken: " Width="100%"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 70px;" colspan="3">
                    <asp:TextBox runat="server" ID="txt_Action" TextMode="MultiLine" Height="70px" Width="98%"></asp:TextBox>
                </td>
            </tr>




            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="4" />
            </tr>
           
            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 50%; height:50px; vertical-align:middle"; font-family: 'Arial Unicode MS'; align="center"  colspan="2">
                    <asp:Button  runat="server" Width="50%" Height="30px" Text="Submit" ID="btn_submit" OnClick="btn_submit_Click" CssClass="btn-success"/>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 50%; height:50px; vertical-align:middle"; font-family: 'Arial Unicode MS'; align="center"  colspan="2">
                    <asp:Button  runat="server" Width="50%" Text="Cancel" Height="30px" ID="btn_cancel" OnClick="btn_cancel_Click"  CssClass="btn-danger"/>
                </td>
            </tr>
        </table> 
    </div>

           
    </div>

           
</asp:Content>