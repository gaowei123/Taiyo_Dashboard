<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MouldingPartsMovingDetail.aspx.cs" Inherits="DashboardTTS.Webform.Moulding.MouldingPartsMovingDetail" %>
<%@ Register assembly="Infragistics2.WebUI.WebDateChooser.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>
<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
    <link href="../Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />  
<link href="/Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />

<script src="../js/Dashboard.js"> </script>

    
     
    <div style="width: 1154px;align-items:center;margin:auto">
  
     
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="4"> 
                <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Moulding Parts Movement Detail" Font-Size="12" ForeColor="White"/>
                </td>
            </tr>


            <tr style="width:100%">    
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Parts No :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: auto;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="1">
                        <asp:DropDownList ID="ddlPartNo"  runat="server" Width="100%" Height="23px"  >
                        </asp:DropDownList> </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Transfer To :" Width="100%"></asp:Label>  
                </td>
                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                     <asp:DropDownList ID="ddlTransferTo"  runat="server" Width="100%" Height="23px"  >
                                <asp:ListItem Value="1">Painting Dept</asp:ListItem>
                                <asp:ListItem Value="2">Silkprint</asp:ListItem>
                                <asp:ListItem Value="3">Summer Weli</asp:ListItem>
                                <asp:ListItem Value="4">store</asp:ListItem>
                        </asp:DropDownList>
                </td>
            </tr>
                        
         

            <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Quantity :" Width="100%"></asp:Label>  
                </td>
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_Quantity" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled"   ></asp:TextBox>
                </td>
                             
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                <asp:Label runat="server" Text="Opr ID :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                <asp:TextBox runat="server" ID="txt_OprID" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled"   ></asp:TextBox>
                </td>
                               
            </tr>

       
             <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Production Date :" Width="100%"></asp:Label>  
                </td>
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">  
                    <igsch:webdatechooser ID="infProductionDate" runat="server" Height="23px"   Width="100%" Value="" >
                        </igsch:webdatechooser>
                </td>
                             
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Transfer Date :" Width="100%"></asp:Label>  
                </td>
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">  
                    <igsch:webdatechooser ID="infTransferDate" runat="server" Height="23px"   Width="100%" Value="" >
                        </igsch:webdatechooser>
                </td>
                               
            </tr>

            <tr style="width:100%;">
                 <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Annealing Process :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                        <asp:DropDownList ID="AnnealingProcesslist"  runat="server" Width="100%" Height="23px" OnSelectedIndexChanged="AnnealingProcesslist_SelectedIndexChanged"  AutoPostBack="true" >
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="2">No</asp:ListItem>
                        </asp:DropDownList>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Annealing Temperature :" Width="100%"></asp:Label>  
                </td>
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                        <asp:DropDownList ID="AnnealingTemperaturelist"  runat="server" Width="100%" Height="23px"  >                                
                                <asp:ListItem Value="2">80</asp:ListItem>
                                <asp:ListItem Value="3">90</asp:ListItem>
                                <asp:ListItem Value="4">100</asp:ListItem>
                                <asp:ListItem Value="5">120</asp:ListItem>
                        </asp:DropDownList>
                </td>  
            
            </tr>

              <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Annealing Transfer From :" Width="100%"></asp:Label>  
                </td>
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_AnnealingTransferFrom" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled"   ></asp:TextBox>
                </td>   
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Annealing Transfer To :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_AnnealingTransferTo" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled"   ></asp:TextBox>
                </td>
            </tr>







            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 50%; height:50px; vertical-align:middle"; font-family: 'Arial Unicode MS'; align="center"  colspan="2">
                    <asp:Button  runat="server" Width="50%" Height="30px" Text="Submit" ID="btn_submit" OnClick="btn_submit_Click" CssClass="btn-success"/>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 50%; height:50px; vertical-align:middle"; font-family: 'Arial Unicode MS'; align="center"  colspan="2">
                    <asp:Button  runat="server" Width="50%" Text="Cancel" Height="30px" ID="btn_cancel"  OnClick="btn_cancel_Click" CssClass="btn-danger"/>
                </td>
            </tr>
                          

        </table>                                
      
    </div>
           

           
</asp:Content>