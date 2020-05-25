<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LaserEquipmentCheckForm.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.Laser.LaserEquipmentCheckForm" %>


<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
    <link href="../Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />  
    <link href="/Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />

    <script src="../js/Dashboard.js"> </script>

    
     
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


            <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" rowspan="2">
                    <asp:Label runat="server" Text="Vision Check Result" Width="100%"></asp:Label>  
                </td>   
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Detect OK sample :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" >
                    <asp:RadioButton runat="server" ID="rbt_DetectOK_OK" Text="OK" Width="15%" GroupName="deteckOK" Checked="false" />
                    <asp:RadioButton runat="server" ID="rbt_DetectOK_NG" Text="NG" Width="15%" GroupName="deteckOK" Checked="false"/>
                </td>
            </tr>
            <tr style="width:100%;">
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Detect NG sample :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" >
                    <asp:RadioButton runat="server" ID="rbt_DetectNG_OK" Text="OK" Width="15%" GroupName="deteckNG"  Checked="false"/>
                    <asp:RadioButton runat="server" ID="rbt_DetectNG_NG" Text="NG" Width="15%" GroupName="deteckNG"  Checked="false"/>
                </td>
            </tr>


            <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" rowspan="3">
                    <asp:Label runat="server" Text="Tower Light Condition" Width="100%"></asp:Label>  
                </td>   
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Running/Auto Green :" Width="100%"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" >
                    <asp:RadioButton runat="server" ID="rbt_Green_OK" Text="OK" Width="15%" GroupName="Green" Checked="false"/>
                    <asp:RadioButton runat="server" ID="rbt_Green_NG" Text="NG" Width="15%" GroupName="Green" Checked="false"/>
                </td>
            </tr>
            <tr style="width:100%;">
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Running/Auto Yellow :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" >
                    <asp:RadioButton runat="server" ID="rbt_Yellow_OK" Text="OK" Width="15%" GroupName="Yellow" Checked="false"/>
                    <asp:RadioButton runat="server" ID="rbt_Yellow_NG" Text="NG" Width="15%" GroupName="Yellow" Checked="false"/>
                </td>
            </tr>
            <tr style="width:100%;">
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Fail/Fault Red :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" >
                    <asp:RadioButton runat="server" ID="rbt_Red_OK" Text="OK" Width="15%" GroupName="Red" Checked="false"/>
                    <asp:RadioButton runat="server" ID="rbt_Red_NG" Text="NG" Width="15%" GroupName="Red" Checked="false"/>
                </td>
            </tr>


            <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" rowspan="2">
                    <asp:Label runat="server" Text="Lonizer Blower" Width="100%"></asp:Label>  
                </td>   
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Production Before Blowing :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" >
                    <asp:TextBox runat="server" ID="txt_ProductBeforeBlowing" Width="25%"></asp:TextBox>
                </td>
            </tr>
            <tr style="width:100%;">
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Production After Blowing :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2" >
                    <asp:TextBox runat="server" ID="txt_ProductAfterBlowing" Width="25%"></asp:TextBox>
                </td>
            </tr>


            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Vacuum Filter Bag" Width="100%"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:Label runat="server" Text="Clean/Replace/Weekly :" Width="60%" ></asp:Label>
                    <asp:CheckBox runat="server" ID="cb_FilterBagReplace" Text="" />
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Done By :" Width="93%"></asp:Label>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:DropDownList ID="ddl_DoneBy"  runat="server" Width="93%" Height="23px" BorderWidth="1px"  />
                </td>
            </tr>

            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="4" />
            </tr>
           
            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 50%; height:50px; vertical-align:middle"; font-family: 'Arial Unicode MS'; align="center"  colspan="2">
                    <asp:Button  runat="server" Width="50%" Height="30px" Text="Submit" ID="btn_submit"  OnClick="btn_submit_Click" CssClass="btn-success"/>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 50%; height:50px; vertical-align:middle"; font-family: 'Arial Unicode MS'; align="center"  colspan="2">
                    <asp:Button  runat="server" Width="50%" Text="Cancel" Height="30px" ID="btn_cancel"   OnClick="btn_cancel_Click"  CssClass="btn-danger"/>
                </td>
            </tr>
        </table> 
    </div>

           
    </div>

           
    </div>

           
</asp:Content>