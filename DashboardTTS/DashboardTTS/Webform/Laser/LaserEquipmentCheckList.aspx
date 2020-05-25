<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LaserEquipmentCheckList.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.Laser.LaserEquipmentCheckList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >

    <style >
        .container-fluid{
            max-width: 1700px;
        }
    </style>


    <div class="container-fluid">
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Laser Equipment Preventive Maintenance & Check List</span>
        </div>


        <div class="row">
            <div class="col-md-12 panel panel-default" style="padding:0px 0px 10px 10px;">
                <div class="row form-inline searchingBar ">
                    
                    <div class="col-md-3">
                        <label style="width:35%">Date From:</label>
                        <asp:TextBox runat="server" ID="txtDateFrom" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd" Width="60%"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label style="width:35%">Date To:</label>
                        <asp:TextBox runat="server" ID="txtDateTo" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd" Width="60%"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label style="width:35%">Machine No:</label>
                        <asp:DropDownList runat="server" ID="ddlMachineNo" CssClass="form-control" Width="60%"></asp:DropDownList>
                    </div>
                    <div class="col-md-3" align="right" style="padding-right:2%;">
                        <asp:Button runat="server" ID="btn_Generate" CssClass="btn btn-success"  Text="Generate" OnClick="btn_Generate_Click" style="width:100px; height:34px; border-radius:4px;"/>
                        &nbsp;<asp:Button runat="server" ID="btn_Check"    CssClass ="btn btn-danger"  Text="Check"    OnClick ="btn_Check_Click"   style="width:100px; height:34px; border-radius:4px;"/>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
           <h3> <asp:Label ID="lblResult" runat="server" /> </h3>
        </div>


        <div class="row">
            <div class="col-md-12 panel panel-default"  style="padding-top:10px;padding-bottom:10px;">
                <asp:DataGrid runat="server" Width ="100%" ID ="dg_CheckList" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="false" CssClass="table table-hover" >
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="left" VerticalAlign="Bottom"  />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="left" />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"/>
                    <Columns>
                        <asp:BoundColumn DataField="date" HeaderText="Date"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MachineID" HeaderText="Machine"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DetectOKSample" HeaderText="Detect OK Sample"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DetectNGSample" HeaderText="Detect NG Sample"></asp:BoundColumn>
                        <asp:BoundColumn DataField="greenLight" HeaderText="Running Light"></asp:BoundColumn>
                        <asp:BoundColumn DataField="yellowLight" HeaderText="Loading Light"></asp:BoundColumn>
                        <asp:BoundColumn DataField="redLight" HeaderText="Fault Light"></asp:BoundColumn>
                        <asp:BoundColumn DataField="productBeforeBlowing" HeaderText="Product before blowing"></asp:BoundColumn>
                        <asp:BoundColumn DataField="productAfterBlowing" HeaderText="Product after blowing"></asp:BoundColumn>
                        <asp:BoundColumn DataField="filterBagReplace" HeaderText="Clean/Replace Weekly"></asp:BoundColumn>
                        <asp:BoundColumn DataField="doneBy" HeaderText="Done By"></asp:BoundColumn>
                        <asp:BoundColumn DataField="VerifyBy" HeaderText="Verify By"></asp:BoundColumn>

                    </Columns>
                </asp:DataGrid>
            </div>
        </div>
    </div>

    
    
    
    
    <script type="text/javascript">
       
        $('#MainContent_txtDateFrom').datetimepicker({
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            minView: 2,
            forceParse: 0,
            initialDate: new Date()
        });

        $('#MainContent_txtDateTo').datetimepicker({
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            minView: 2,
            forceParse: 0,
            initialDate: new Date()
        });
    </script>                   
</asp:Content>
