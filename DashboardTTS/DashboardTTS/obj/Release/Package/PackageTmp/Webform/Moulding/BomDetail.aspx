<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="BomDetail.aspx.cs" Inherits="DashboardTTS.Webform.Moulding.BomDetail" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
    <link href="../Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />  
<link href="/Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />

<script src="../js/Dashboard.js"> </script>

    
     
    <div style="width: 1154px;align-items:center;margin:auto">
  
     
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="4"> 
                <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Moulding BOM Menu" Font-Size="12" ForeColor="White"/>
                </td>
            </tr>


            <tr style="width:100%">    
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="PartNumberAll :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: auto;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                    <asp:TextBox runat="server" ID="txt_PartNumberAll" Width="85%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoPostBack="true" Enabled="false" AutoCompleteType="Disabled" Columns="2" ></asp:TextBox>
                    <asp:Button runat="server" ID="Btn_edit" Width="10%" CssClass="btn-primary" Height="23px" Text="Edit" style="float:right"  OnClick="btn_edit_Click" Visible="false" />
                </td>

                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                     <asp:TextBox runat="server" ID="txt_part" Width="60%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled" ></asp:TextBox> 
                     <asp:Button runat="server" ID="btn_addPart" Width="30%" CssClass="btn-primary" Height="23px" Text="Add Part" style="float:right"  OnClick="btn_addPart_Click" />
                </td>
            </tr>
                        
         

            <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Model :" Width="100%"></asp:Label>  
                </td>
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_model" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled"   ></asp:TextBox>
                </td>
                             
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                <asp:Label runat="server" Text="Jig No :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                <asp:TextBox runat="server" ID="txt_jigNo" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled"   ></asp:TextBox>
                </td>
                               
            </tr>

       
             <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="1st Material No :" Width="100%"></asp:Label>  
                </td>
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtMaterial01" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled"   ></asp:TextBox>
                </td>
                             
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="2nd Material No :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtMaterial02" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled"   ></asp:TextBox>
                </td>
            </tr>

            <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="1st Material Weight (g/pcs):" Width="100%"></asp:Label>  
                </td>
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtMaterial01Weight" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled"   ></asp:TextBox>
                </td>
                             
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="2nd Material Weight (g/pcs):" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtMaterial02Weight" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled"   ></asp:TextBox>
                </td>
            </tr>

              <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Cavity Count :" Width="100%"></asp:Label>  
                </td>
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_cavityCount" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled"   ></asp:TextBox>
                </td>   
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <%--<asp:Label runat="server" Text="Parts Weight :" Width="100%"></asp:Label>  --%>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <%--<asp:TextBox runat="server" ID="txt_partsWeight" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled"   ></asp:TextBox>--%>
                </td>
            </tr>

            <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Customer :" Width="100%"></asp:Label>  
                </td>
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_customer" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled"   ></asp:TextBox>
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Cycle Time :" Width="100%"></asp:Label>  
                </td>
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_cycleTime" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled"   ></asp:TextBox>
                </td>   
            
            </tr>

            <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Unit Cost (per pcs):" Width="100%"></asp:Label>  
                </td>
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_unit" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled" Enabled="false"  ></asp:TextBox>
                </td>
                 <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Type :" Width="100%"></asp:Label>  
                </td>
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                       <asp:DropDownList ID="ddlType"  runat="server" Width="100%" Height="23px"  />
                </td>
            
            </tr>

              <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Supplier :" Width="100%"></asp:Label>  
                </td>
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                       <asp:DropDownList ID="ddlSuppiller"  runat="server" Width="100%" Height="23px"  />
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Remarks :" Width="100%"></asp:Label>  
                </td>
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="1">
                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txt_remarks" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="60px" AutoCompleteType="Disabled"   ></asp:TextBox>
                </td>   
            </tr>

            <tr runat="server" id="test" style="width:100%" visible="false">    
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="PartNumberAll :" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: auto;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="2">
                    <asp:TextBox runat="server" ID="txt_newPartAllNumber" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoPostBack="true" Enabled="false" AutoCompleteType="Disabled" Columns="2" ></asp:TextBox>
                </td>

                <td  style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                     <asp:TextBox runat="server" ID="txt_newPartNumber" Width="60%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled" ></asp:TextBox> 
                     <asp:Button runat="server" ID="btn_addNewPart" Width="30%" CssClass="btn-primary" Height="23px" Text="Add Part" style="float:right"  OnClick="btn_addNewPart_Click" />
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