<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MouldingMaterialInventory.aspx.cs"  MasterPageFile="~/Site.Master"  Inherits="DashboardTTS.Webform.Moulding.MouldingMaterialInventory" %>


<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
    <link href="../Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />  
<link href="/Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />

<script src="../js/Dashboard.js"> </script>

    
     
    <div style="width: 600px;align-items:center;margin:auto">
  
     
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="2"> 
                <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Moulding - Material Inventory" Font-Size="12" ForeColor="White"/>
                </td>
            </tr>

            <tr style="width:100%">    
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 40%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Material No :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 60%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:TextBox runat="server" ID="txt_Material_Part" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled" Columns="2" ></asp:TextBox>
                </td>
            </tr>                       

            <tr style="width:100%">    
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 40%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Material Lot No :" Width="100%"></asp:Label>  
                </td>
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 60%;   font-family: 'Arial Unicode MS'; height: 50px;">
                     <asp:TextBox runat="server" ID="txt_Material_Batch" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled" ></asp:TextBox> 
                </td>
            </tr> 
             <tr style="width:100%">    
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 40%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Customer :" Width="100%"></asp:Label>  
                </td>
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 60%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                     <asp:TextBox runat="server" ID="txt_Customer" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled" Enabled="False" >NA</asp:TextBox> 
                </td>
            </tr> 
            <tr style="width:100%">    
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 40%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    Current Inventory Weight</td>
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 60%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                     <asp:TextBox runat="server" ID="txt_InventoryWeight" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled" Enabled="False" ></asp:TextBox> 
                </td>
            </tr> 

            <tr style="width:100%">    
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 40%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    Machine ID&nbsp;</td>
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 60%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:DropDownList runat="server" ID="ddl_machineID" Width="100%">
                        <asp:ListItem Value="1">Machine 1</asp:ListItem>
                        <asp:ListItem Value="2">Machine 2</asp:ListItem>
                        <asp:ListItem Value="3">Machine 3</asp:ListItem>
                        <asp:ListItem Value="4">Machine 4</asp:ListItem>
                        <asp:ListItem Value="5">Machine 5</asp:ListItem>
                        <asp:ListItem Value="6">Machine 6</asp:ListItem>
                        <asp:ListItem Value="7">Machine 7</asp:ListItem>
                        <asp:ListItem Value="8">Machine 8</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr> 

            <tr style="width:100%">    
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 40%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    Transaction Weight</td>
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 60%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                     <asp:TextBox runat="server" ID="txt_Weight" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled" ></asp:TextBox> 
                </td>
            </tr> 

            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 50%; height:50px; vertical-align:middle"; font-family: 'Arial Unicode MS'; align="center" colspan="2" >
                    <asp:Button  runat="server" Width="30%" Height="30px" Text="Submit" ID="btn_submit" OnClick="btn_submit_Click" CssClass="btn-success"/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button  runat="server" Width="30%" Text="Cancel" Height="30px" ID="btn_cancel"  OnClick="btn_cancel_Click" CssClass="btn-danger"/>
                </td>
            </tr>
                          
        </table>                                
      
    </div>
           

           
</asp:Content>
