<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlMachineStatus.ascx.cs" Inherits="DashboardTTS.UserControl.WebUserControlMachineStatus" %> 

<style type="text/css" >
    .auto-style1 {
        width: 150px;
        align-content:center;
        align-items:center;
        vertical-align:central;
        font-size:16px; 
        height:20px;
    }
    .auto-style3 {
        width:110px;
        height:21px;
        font-size:12px;
        padding: 1px 10px 1px 1px;
    }
    .auto-style4 {
        width:175px;
        height:21px;
        font-size:12px;
    }
    .lbDescript{
        font-weight: bold;
        color:#003162;
        text-align:right;
    }
    .ng{
        color:red;
    }
    .title{
        text-align:center;
    }
    .status{
        text-align:center;
        font-size:14px;
    }
    .tableBorder{
        width:370px; 
        height: 200px; 
        border-color:#000066; 
        border-style:solid;
        border-width:3px;
        font-family: 'Arial Unicode MS';
    }
</style>


<table  class="tableBorder">
    <tr>
        <td class="auto-style1 lbDescript title"> 
            <asp:Label ID="lbMachineID" runat="server" Text="Machine 8"></asp:Label>
        </td> 

        <td class="auto-style3 lbDescript">Part No</td>
        <td class="auto-style4"  >
            <asp:Label runat="server" ID="lbPartNo"></asp:Label>
        </td>
    </tr>

    <tr> 
        <td class="auto-style1 lbDescript status">
            <asp:Label ID="lbStatus" runat="server"  Width="120px" BorderStyle="Solid" BorderWidth="1px"></asp:Label> 
        </td>
        <td class="auto-style3 lbDescript">Lot No</td>          
        <td class="auto-style4">
            <asp:Label ID="lbLotNo" runat="server"></asp:Label>
        </td>
    </tr>
          
    <tr>    
        <td class="auto-style1" rowspan="6" > 
            <asp:Image ID="imgLogo"  runat="server"  Width="146" Height ="146" />
        </td>
        <td class="auto-style3 lbDescript">Job No</td>
        <td class="auto-style4">
            <asp:Label ID="lbJobNo" runat="server"></asp:Label>
        </td>
    </tr>

    <tr>   
        <td class="auto-style3 lbDescript">MRP Total</td>          
        <td class="auto-style4" > 
            <asp:Label ID="lbLotQty" runat="server"></asp:Label>  
        </td>          
    </tr>
                           
    <tr>                         
        <td class="auto-style3 lbDescript">OK</td>          
        <td class="auto-style4">  
            <asp:Label ID="lbOKQty" runat="server"></asp:Label>
        </td>
    </tr>
                           
    <tr>                         
        <td class="auto-style3 lbDescript ng">NG</td>          
        <td class="auto-style4 ng" >
            <asp:Label ID="lbNGQty" runat="server"></asp:Label>
        </td>
    </tr>
          
    <tr>
        <td class="auto-style3 lbDescript">REJ Rate</td>
        <td class="auto-style4" >
            <asp:Label ID="lbRejRate" runat="server" ToolTip="REJ Rate = NG/MRP Total"></asp:Label>
        </td>
    </tr>   

    <tr>
        <td class="auto-style3 lbDescript">Used Rate</td>
        <td class="auto-style4"  >
            <asp:Label ID="lbUsedRate" runat="server" ToolTip="Used Rate = Run Time/Total Time(Except Shutdown)"></asp:Label> 
        </td>
    </tr>
</table>