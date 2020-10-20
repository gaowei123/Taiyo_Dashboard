<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlPQCStatus.ascx.cs" Inherits="DashboardTTS.UserControl.WebUserControlPQCStatus" %>

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
        width:200px; 
        height: 200px; 
        border-color:#000066; 
        border-style:solid;
        border-width:3px;
        font-family: 'Arial Unicode MS';
    }
</style>




<table  class="tableBorder">
    <tr>
        <td class="auto-style1 lbDescript title" colspan="2"> 
            <asp:Label ID="lbStation" runat="server" Text="Machine 8"></asp:Label>
        </td> 
    </tr>
    <tr> 
        <td class="auto-style1 lbDescript status" colspan="2">
            <asp:Label ID="lbStatus" runat="server"  Width="110px" BorderStyle="Solid" BorderWidth="1px"></asp:Label> 
        </td>      
    </tr>
    <tr>    
        <td class="auto-style3 lbDescript">Lot No</td>
        <td class="auto-style4">
            <asp:Label ID="lbLotNo" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>    
        <td class="auto-style3 lbDescript">Job No</td>
        <td class="auto-style4">
            <asp:Label ID="lbJobNo" runat="server"></asp:Label>
        </td>
    </tr>

    <tr>    
        <td class="auto-style3 lbDescript">Part No</td>
        <td class="auto-style4">
            <asp:Label ID="lbPartNo" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>    
        <td class="auto-style3 lbDescript">MRP Total</td>
        <td class="auto-style4">
            <asp:Label ID="lbLotQty" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>    
        <td class="auto-style3 lbDescript">OK</td>
        <td class="auto-style4">
            <asp:Label ID="lbOK" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>    
        <td class="auto-style3 lbDescript ng">NG</td>
        <td class="auto-style4 ng">
            <asp:Label ID="lbNG" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>    
        <td class="auto-style3 lbDescript">Rej Rate</td>
        <td class="auto-style4">
            <asp:Label ID="lbRejRate" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>    
        <td class="auto-style3 lbDescript">Operator</td>
        <td class="auto-style4">
            <asp:Label ID="lbOP" runat="server"></asp:Label>
        </td>
    </tr>    
</table>