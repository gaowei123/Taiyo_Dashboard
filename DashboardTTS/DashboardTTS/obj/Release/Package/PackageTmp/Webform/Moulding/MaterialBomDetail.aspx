<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialBomDetail.aspx.cs" Inherits="DashboardTTS.Webform.Moulding.MaterialBomDetail" MasterPageFile="~/Site.Master"%>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
    <link href="../Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />  
    <link href="/Resources/Stylesheets/StyleCSS.css" rel="stylesheet" type="text/css" />
    <script src="../js/Dashboard.js"> </script>

    <script type="text/javascript">

        $("input[name='keleyicom']").val()



        function autoCalculateUSD() {
            
            var SGD = new Number($("#MainContent_txtUnitPrice_SGD").val());
            var ExchangeRate = new Number($("#MainContent_txtExchangeRate").val());
            var MakeUp = new Number($("#MainContent_txtMakeUp").val());

            MakeUp = MakeUp / 100;


            var USD = (SGD + SGD * MakeUp) * ExchangeRate;


            //$("#MainContent_txtUnitPrice_USD").val(USD);

            $("#MainContent_txtUnitPrice_USD").attr({
                "value": USD
            });
        }



    </script>

    <div style="width:60%;align-items:center;margin:auto">
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">
            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="4"> 
                <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Moulding Material BOM Detail" Font-Size="12" ForeColor="White"/>
                </td>
            </tr>
                                 
            <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Material No :" Width="100%"></asp:Label>  
                </td>
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_materialPart" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled"   ></asp:TextBox>
                </td>
                             
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    Unit Price (USD):
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtUnitPrice_USD" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled"  Enabled="false"></asp:TextBox>
                </td>
                               
            </tr>

            <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Resin Type :" Width="100%"></asp:Label>  
                </td>
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txt_ResinType" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled"   ></asp:TextBox>
                </td>
                             
               

                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    Unit Price (SGD):
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                    <asp:TextBox runat="server" ID="txtUnitPrice_SGD" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px"  AutoCompleteType="Disabled" oninput="autoCalculateUSD();" ></asp:TextBox>
                </td>
                               
            </tr>

            <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Exchange Rate :" Width="100%"></asp:Label>  
                </td>
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtExchangeRate" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled" oninput="autoCalculateUSD();"></asp:TextBox>
                </td>
                        
                 <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Make Up (%):" Width="100%"></asp:Label>  
                </td>
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;">
                    <asp:TextBox runat="server" ID="txtMakeUp" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="23px" AutoCompleteType="Disabled" oninput="autoCalculateUSD();"  ></asp:TextBox>
                </td>     
                
            </tr>
       
             

            <tr style="width:100%;">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 20%;   font-family: 'Arial Unicode MS'; height: 50px;" >
                    <asp:Label runat="server" Text="Remarks :" Width="100%"></asp:Label>  
                </td>
               <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 30%;   font-family: 'Arial Unicode MS'; height: 50px;" colspan="3">
                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txt_remarks" Width="100%" BorderStyle="Solid" BorderWidth="1px" Height="60px" AutoCompleteType="Disabled"   ></asp:TextBox>
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