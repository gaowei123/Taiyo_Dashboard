<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BOMList.aspx.cs" Inherits="DashboardTTS.Webform.BOMList" %>



<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
  

    <div class="container" >
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Laser Bom Item List</span>
        </div>


        <div class="row">
            <div class="col-md-12 panel panel-default searchingPanel" >
                <div class="row form-inline searchingBar ">
                    <div class="col-md-3">
                        <label style="width:35%">Part No:</label>
                        <asp:TextBox runat="server" ID="txtPartNo" CssClass="form-control" Width="60%" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label style="width:35%">Machine No:</label>
                        <asp:DropDownList runat="server" ID="ddlMachineNo" CssClass="form-control" Width="60%"></asp:DropDownList>
                    </div>
                    <div class="col-md-3"></div>
                    <div class="col-md-3" align="right" style="padding-right:2%;">
                        <asp:Button ID="Button1" runat="server" Text="Generate" OnClick="btn_search_Click" CssClass="btn btn-success" style="width:100px; height:34px; border-radius:4px;" />
                        &nbsp;&nbsp;<asp:Button ID="Button2" runat="server" Text="Add" OnClick="btn_Add_Click" CssClass="btn btn-primary" style="width:100px; height:34px; border-radius:4px;" />
                    </div>
                </div>
            </div>
        </div>
    

        <div class="row">
            <h3><asp:Label runat="server" ID="lblResult"></asp:Label></h3>
        </div>

        <div class="row">
            <div class="col-lg-12 panel panel-default" style="padding-top:10px; padding-bottom:10px;">
                <asp:DataGrid runat="server" ID ="dg_BOMList" AutoGenerateColumns="false" CssClass="table" Width ="100%" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2">
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775"  />
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundColumn DataField="customer" HeaderText="Customer" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Supplier" HeaderText="Supplier" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        <asp:BoundColumn DataField="module" HeaderText="Model" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        <asp:ButtonColumn DataTextField="partNumber" CommandName="UpdatePart" HeaderText="Part No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:ButtonColumn>
                        <asp:BoundColumn DataField="partNumber" HeaderText="Part No" Visible="False" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        <asp:BoundColumn DataField="type" HeaderText="Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Number" HeaderText="Num"></asp:BoundColumn>
                        <asp:BoundColumn DataField="machineID" HeaderText="Mc No"></asp:BoundColumn>
                        <asp:BoundColumn DataField="lighting" HeaderText="Lighting"></asp:BoundColumn>
                        <asp:BoundColumn DataField="camera" HeaderText="Camera"></asp:BoundColumn>
                        <asp:BoundColumn DataField="currentPower" HeaderText="Power"></asp:BoundColumn>
                        <asp:BoundColumn DataField="dateTime" HeaderText="Date"></asp:BoundColumn>

                        <asp:TemplateColumn HeaderText="">
                            <ItemTemplate>
                                <asp:Button runat="server" CommandName="Delete" Text="×" Height="23px" CssClass="btn-danger"/>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                    </Columns>
                </asp:DataGrid>
            </div>
        </div>
    </div>


    <link href="../../plugins/bigautocomplete/jquery.bigautocomplete.css" rel="stylesheet" />
    <script src="../../plugins/bigautocomplete/jquery.bigautocomplete.js"></script>
    <script type="text/javascript">
        jQuery(function($){  $(function(){
            setAutoComplete($('#MainContent_txtPartNo'), 'Laser');
        }); });

    </script>


</asp:Content>