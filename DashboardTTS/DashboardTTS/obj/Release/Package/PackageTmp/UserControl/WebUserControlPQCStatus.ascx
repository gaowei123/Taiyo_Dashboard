<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlPQCStatus.ascx.cs" Inherits="DashboardTTS.UserControl.WebUserControlPQCStatus" %>

<style type="text/css" aria-orientation="horizontal" >
             .auto-style1 {
                 width: 150px;
                 align-content:center;
                 align-items:center;
                 vertical-align:central;
                 font-size:16px; 
                 height:20px;
             }
             .auto-style3 {
                 width:100px;
                 height:21px;
                   font-size:12px;
             }
             .auto-style4 {
                 width:160px;
                  height:21px;
                   font-size:12px;
             }
         </style>
     
<asp:Panel runat="server" Width="370px" BorderColor="#000066" BorderStyle="Solid" BorderWidth="3px" ID ="pnlAll" style="border-radius:4px;"  >
    <table  style="background-position: left bottom; width:410px; height: 203px; font-family: 'Arial Unicode MS'; background-attachment: inherit;">
        <tr>
            <td align="center" class="auto-style1"> 
                <asp:Label ID="lbStation" Font-Bold="True" ForeColor="#003162"  runat="server" Text=""></asp:Label>
            </td>  
            <td class="auto-style3" align="right" style="padding: 1px 10px 1px 1px"  >
                <asp:Label Font-Bold="True" ForeColor="#003162" runat="server" Text="Lot No" ></asp:Label>
            </td>
            <td class="auto-style4"  >
                <asp:Label ID="lbLotno" runat="server" Text="" ></asp:Label>
            </td>
        </tr>

        <tr> 
            <td align="center" class="auto-style1">
                <asp:Label ID="lbStatus" Font-Bold="True" ForeColor="#003162" runat="server" Font-Size="14px"  style="line-height:20px" Text="ShutDown"  Width="120px" BackColor="Gray" BorderStyle="Solid" BorderWidth="1px"></asp:Label> 
            </td>
            <td class="auto-style3" align="right" style="padding: 1px 10px 1px 1px"  >  
                <asp:Label Font-Bold="True" ForeColor="#003162" runat="server" Text="Job No"></asp:Label>
            </td>          
            <td class="auto-style4"  >  
                <asp:Label ID="lbJobNumber" runat="server" Text=""></asp:Label>
            </td>
        </tr>        
                        
        <tr>                         
            <td class="auto-style1" rowspan="7" > 
                <asp:Image ID="imgLogo"  runat="server"  Width="160" Height ="150" />
            </td>
            <td class="auto-style3" align="right" style="padding: 1px 10px 1px 1px" >  
                <asp:Label Font-Bold="True" ForeColor="#003162" runat="server" Text="Part No"></asp:Label>
            </td>          
            <td class="auto-style4" >  
                <asp:Label ID="lbPartNo" runat="server" Text=""></asp:Label>
            </td>          
        </tr>
              
        <tr>                         
            <td class="auto-style3" align="right" style="padding: 1px 10px 1px 1px"  > 
                <asp:Label ID="lblTotal" Font-Bold="True" ForeColor="#003162" runat="server" Text="MRP Total"></asp:Label>
            </td>          
            <td class="auto-style4"  > 
                <asp:Label ID="lbLotQty" runat="server" Text=""></asp:Label>
            </td>          
        </tr>  
                              
        <tr>                         
            <td class="auto-style3" align="right" style="padding: 1px 10px 1px 1px"  > 
                <asp:Label Font-Bold="True" ForeColor="#003162" runat="server" Text="OK"></asp:Label>
            </td>          
            <td class="auto-style4" > 
                <asp:Label ID="lbOK" runat="server" Text=""></asp:Label>
            </td>          
        </tr>     

        <tr>
            <td class="auto-style3" align="right" style="padding: 1px 10px 1px 1px" > 
                <asp:Label Font-Bold="True" ForeColor="red" runat="server" Text="NG"></asp:Label>
            </td>
            <td class="auto-style4" > 
                <asp:Label ID="lbNG" runat="server" Text=""></asp:Label>
            </td>
        </tr>

        <tr>
            <td class="auto-style3" align="right" style="padding: 1px 10px 1px 1px"  >
                <asp:Label Font-Bold="True" ForeColor="#003162" runat="server" Text="REJ Rate" ToolTip="REJ Rate = NG / Total QTY"></asp:Label>
            </td>
            <td class="auto-style4"  >
                <asp:Label ID="lbRejRate" runat="server" Text="0%" ToolTip="REJ Rate = NG / Total QTY"></asp:Label>
            </td>
        </tr>    

        <tr>    
            <td class="auto-style3"  align="right" style="padding: 1px 10px 1px 1px" > 
                <asp:Label Font-Bold="True" ForeColor="#003162" runat="server" Text="REJ PPM" ToolTip="REJ PPM = REJRate * 10000"></asp:Label> 
            </td>
            <td class="auto-style4"  > 
                <asp:Label ID="lbRejPPM" runat="server" Text="0" ToolTip="REJ PPM = REJRate * 10000"></asp:Label>
            </td>
        </tr> 

        <tr>    
            <td class="auto-style3" align="right" style="padding: 1px 10px 1px 1px" >
                <asp:Label Font-Bold="True" ForeColor="#003162" runat="server" Text="Operator" ToolTip=""></asp:Label> 
            </td>
            <td class="auto-style4" >
                <asp:Label ID="lbOperator" runat="server"  ></asp:Label> 
            </td>
        </tr>



              
    </table>
</asp:Panel>