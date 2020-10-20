<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LaserMachineDownTimeList.aspx.cs" MasterPageFile="~/Site.Master" Inherits="DashboardTTS.Webform.Laser.LaserMachineDownTimeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >


    <style >
        .container-fluid{
            max-width: 1500px;
        }
    </style>


    <div class="container-fluid" >
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Machine Maintenance Record</span>
        </div>


        <div class="row">
            <div class="col-sm-12 panel panel-default searchingPanel" >
                <div class="row form-inline searchingBar">
                    <div class="col-sm-3">
                        <label style="width:35%">Date From:</label>
                        <asp:TextBox runat="server" ID="txtDateFrom" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd" Width="60%"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <label style="width:35%">Date To:</label>
                        <asp:TextBox runat="server" ID="txtDateTo" CssClass="form-control formDateTimePicker" data-date-format="yyyy-mm-dd" Width="60%"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <label style="width:35%">Machine No:</label>
                        <asp:DropDownList runat="server" ID="ddlMachineNo" CssClass="form-control" Width="60%"></asp:DropDownList>
                    </div>

                     <div class="col-sm-3" align="right" style="padding-right:2%;">
                        <asp:Button ID="Button1" runat="server" Text="Generate" OnClick="btn_Generate_Click" CssClass="btn-success" style="width:100px; height:34px; border-radius:4px;" />&nbsp;
                        <asp:Button ID="Button2" runat="server" Text="Maintain" OnClick="btn_Maintenance_Click" CssClass="btn-danger" style="width:100px; height:34px; border-radius:4px;" />
                     </div>
                </div>
            </div>
        </div>



        <div class="row">
            TF106/00<h3> <asp:Label ID="lblResult" runat="server" /> </h3>
        </div>


        <div class="row">
            <div class="col-sm-12 panel panel-default" style="padding-top:10px;padding-bottom:10px;">

                 <asp:DataGrid runat="server" ID ="dg_DownTime" AutoGenerateColumns="false" CssClass="table" Width ="100%" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" style="margin-top:10px;">
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="left" VerticalAlign="Middle" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="left" />
                    <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="No."></asp:BoundColumn>
                        <asp:BoundColumn DataField="date" HeaderText="Date"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MachineID" HeaderText="Machine"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PartRunning" HeaderText="Part Running"></asp:BoundColumn>
                        <asp:ButtonColumn DataTextField="Cause" HeaderText="BreakDown Cause" CommandName="LinkDetail"></asp:ButtonColumn>
                        <asp:BoundColumn DataField="Action" HeaderText="Corrective Action Taken"></asp:BoundColumn>
                        <asp:BoundColumn DataField="StartTime" HeaderText="Start Time"></asp:BoundColumn>
                        <asp:BoundColumn DataField="stopTime" HeaderText="End Time"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Time" HeaderText="Taken Hour"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Checker" HeaderText="Sign"></asp:BoundColumn>
                        <asp:ButtonColumn DataTextField="fileName" HeaderText="Attachment" CommandName="OpenPDF"></asp:ButtonColumn>
                        <asp:BoundColumn DataField="fileName" HeaderText="Attachment" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="AttachmentPath" HeaderText="Attachment" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="dateComplete" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="CauseComplete" Visible="false"></asp:BoundColumn>
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
