<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlMachineStatus.ascx.cs" Inherits="DashboardTTS.UserControl.WebUserControlMachineStatus" %> 
     
        
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
                 width:85px;
                 height:21px;
                   font-size:12px;
             }
             .auto-style4 {
                 width:175px;
                  height:21px;
                   font-size:12px;
             }
         </style>
     
        <asp:Panel runat="server" Width="370px" BorderColor="#000066" BorderStyle="Solid" BorderWidth="3px" ID ="pnlAll" style="border-radius:4px;" >
         <table  style="background-position: left bottom; width:410px; height: 203px; font-family: 'Arial Unicode MS'; background-attachment: inherit;">
             <tr>
                 <td align="center" class="auto-style1"> 
                     <asp:Label ID="lblMachineNo" Font-Bold="True" ForeColor="#003162"  runat="server" Text="Machine 8"></asp:Label>
                 </td>  
                 <td class="auto-style3" align="right" style="padding: 1px 10px 1px 1px"  >
                     <asp:Label ID="lblPartNo" Font-Bold="True" ForeColor="#003162" runat="server" Text="PartNo"></asp:Label>
                 </td>
                 <td class="auto-style4"  >
                    <asp:Label ID="lblPartNo0" runat="server" Text=""></asp:Label>
                 </td>
             </tr>
             <tr> <td align="center" class="auto-style1"      >
                    <asp:Label ID="lblStatus" Font-Bold="True" ForeColor="#003162" runat="server" Font-Size="14px"  style="line-height:20px" Text="ShutDown"  Width="100px" BackColor="Gray" BorderStyle="Solid" BorderWidth="1px"></asp:Label> 
               </td>
                 <td class="auto-style3" align="right" style="padding: 1px 10px 1px 1px"  > <asp:Label   Font-Bold="True" ForeColor="#003162" runat="server" Text="Lot No"></asp:Label>   </td>          
                 <td class="auto-style4"  ><asp:Label ID="lbLotNo" runat="server" Text=""></asp:Label>  </td>          
             </tr>       
             <tr>    
                 <td class="auto-style1" rowspan="8" > 
                     <asp:Image ID="imgLogo"  runat="server"  Width="150" Height ="150" />
                 </td>
                 <td class="auto-style3" align="right" style="padding: 1px 10px 1px 1px" >
                     <asp:Label ID="lblJobID" Font-Bold="True" ForeColor="#003162" runat="server" Text="JobID"></asp:Label>
                 </td>
                 <td class="auto-style3" align="left" style="padding: 1px 10px 1px 1px" >
                     <asp:Label ID="lblJobID0" runat="server" Text=""></asp:Label>
                 </td>
             </tr>                 
             <tr>                         
                 
                 <td class="auto-style3" align="right" style="padding: 1px 10px 1px 1px" > <asp:Label ID="lblTotal" Font-Bold="True" ForeColor="#003162" runat="server" Text="MRP Total"></asp:Label> </td>          
                 <td class="auto-style4" > <asp:Label ID="lblTotal0" runat="server" Text=""></asp:Label>  </td>          
             </tr>                        
             <tr>                         
                 <td class="auto-style3" align="right" style="padding: 1px 10px 1px 1px"  >  <asp:Label ID="lblOK" Font-Bold="True" ForeColor="#003162" runat="server" Text="OK"></asp:Label> </td>          
                 <td class="auto-style4"  >  <asp:Label ID="lblOK0" runat="server" Text=""></asp:Label> </td>          
             </tr>                        
             <tr>                         
                 <td class="auto-style3" align="right" style="padding: 1px 10px 1px 1px"  ><asp:Label ID="lblNG" Font-Bold="True" ForeColor="red" runat="server" Text="NG"></asp:Label></td>          
                 <td class="auto-style4" ><asp:Label ID="lblNG0" runat="server" Text=""></asp:Label></td>          
             </tr>     
             <tr>      
                 <td class="auto-style3" align="right" style="padding: 1px 10px 1px 1px" >  <asp:Label ID="lblPerformence" Font-Bold="True" ForeColor="#003162" runat="server" Text="REJ Rate" ToolTip="REJ Rate = NG / Total QTY"></asp:Label></td>
                 <td class="auto-style4" ><asp:Label ID="lb_RejRate" runat="server" Text="0%" ToolTip="REJ Rate = NG / Total QTY"></asp:Label> </td>
             </tr>   

              <tr>
                 <td class="auto-style3" align="right" style="padding: 1px 10px 1px 1px"  >
                    <asp:Label ID="lblQuality" Font-Bold="True" ForeColor="#003162" runat="server" Text="REJ PPM" ToolTip="REJ PPM = REJRate * 10000"></asp:Label> 
                 </td>
                 <td class="auto-style4"  >
                     <asp:Label ID="lb_RejPPM" runat="server" Text="0" ToolTip="REJ PPM = REJRate * 10000"></asp:Label>
                 </td>
             </tr>    
              <tr>    
                 <td class="auto-style3"  align="right" style="padding: 1px 10px 1px 1px" > 
                     <asp:Label ID="Label1" Font-Bold="True" ForeColor="#003162" runat="server" Text="Utilization%" ToolTip="REJ PPM = REJRate * 10000"></asp:Label> 
                 </td>
                 <td class="auto-style4"  >
                     <asp:Label ID="lbUtilization" runat="server" Text="0.00%" ></asp:Label> 
                 </td>
             </tr>
                 



              
</table>
     </asp:Panel>
         
     
 

  
  
 

 