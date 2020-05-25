<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MouldingMaintainCheckItem.aspx.cs" Inherits="DashboardTTS.Webform.Moulding.MouldingMaintainCheckItem" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >

    <script>
        function Cancel()
        {
            try
            {
                if (confirm('Your action will not save, are you sure?') == true)
                {
                    //alert("./BOMList.aspx" + "?CommandName=" + CommandName + "&PartNumberAll=" + PartNumberAll);
                    window.location.href = "./MouldingMaintainDetail.aspx";
                }
            }
            catch (e)
            {
                alert('exception' + e.message);
            }
        }
    </script>
     
    <div style="width: 600px;align-items:center;margin:auto">
        <table style =" padding: 0px; width: 100%; border-collapse: separate; border-spacing: 10px; table-layout: auto; line-height: 10px; vertical-align: 10%;">

            <tr style ="width: 100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; font-family: 'Arial Unicode MS'; height: 50px; background-color: #003366; font-weight: bold;" colspan="2"> 
                    <img src="../../Resources/Images/headericon.gif" alt="" style="width: 15px; height: 15px" /> 
                    <Asp:label ID="lblUserHeader"   runat="server" Font-Names="Arial Unicode MS"  Text="Moulding Check Item Detail" Font-Size="12" ForeColor="White"/>
                    <%--<font size="3" color="white" face="Arial Unicode MS">Moulding Check Item Detail</font>--%>
                </td>
            </tr>

             <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 40%;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                        Check Period :
                    </td>
                    <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 60%; height: 50px;">
                        <asp:DropDownList ID="ddl_CheckPeriod" runat="server" Width="100%" Height="23px"   > </asp:DropDownList>
                    </td>
              </tr>

              <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 40%;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                        Check Item :
                    </td>
                    <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 60%; height: 50px;">
                        <asp:TextBox ID="txt_CheckItem"  runat="server" Width="100%" Height="23px"   > </asp:TextBox>
                    </td>
              </tr>

              <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 40%;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                        Inspection Description :
                    </td>
                    <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 60%; height: 50px;">
                        <asp:TextBox ID="txt_Inspection"  runat="server" Width="100%" Height="60px"  TextMode="MultiLine"  > </asp:TextBox>
                    </td>
              </tr>
            <tr style ="width: 100%">
                    <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 40%;   font-family: 'Arial Unicode MS'; height: 50px;"> 
                        Maintenance Description:
                    </td>
                    <td style ="border: 1px solid #CCCCCC; padding: 10px 35px 10px 10px; width: 60%; height: 50px;">
                       <asp:TextBox ID="txt_maintain"  runat="server" Width="100%" Height="60px"   TextMode="MultiLine" > </asp:TextBox>
                    </td>
              </tr>

         

            <tr style="width:100%">
                <td style = "padding: 10px 10px 10px 25px; border: 1px solid #CCCCCC; width: 50%; height:50px; vertical-align:middle"; font-family: 'Arial Unicode MS'; align="center"  colspan="2">
                    <asp:Button  runat="server" Width="35%" Height="30px" Text="Submit" ID="btn_submit" OnClick="btn_submit_Click" CssClass="btn-success"/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button  runat="server" Width="35%" Text="Cancel" Height="30px" ID="btn_cancel"  OnClick="btn_cancel_Click" CssClass="btn-danger"/>
                </td>
            </tr>

        </table>
    </div>
</asp:Content>