<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="LaserJobMaintance.aspx.cs" Inherits="DashboardTTS.Webform.Laser.LaserJobMaintance" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent"  ID="Test" >
   

    <style>
        @media (min-width: 1200px) {
            .container{
                max-width: 720px;
            }
        }
    </style>

    <div class="container">
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Laser Job Maintenance</span>
        </div>


        <div class="row" style="margin-top:10px;">
            <div class="col-md-12 panel panel-default" role="form">
                <div class="row form-inline" style="padding:10px 10px 0px 18px;">
                    <div class="col-md-2">
                        <b>Job Info :</b>
                    </div>
                    <div class="col-md-9">
                        <asp:Label runat="server" ID="lbDay" Font-Bold="true"></asp:Label> &nbsp-&nbsp
                        <asp:Label runat="server" ID="lbShift" Font-Bold="true"></asp:Label>&nbsp-&nbsp
                        <asp:Label runat="server" ID="lbMachineID" Font-Bold="true"></asp:Label>&nbsp-&nbsp
                        <asp:Label runat="server" ID="lbJob" Font-Bold="true"></asp:Label> &nbsp;&nbsp;
                    </div>
                </div>

                <hr />

                <div class="row form-inline" style="margin:4px;"> 
                    <div class="col-md-3">
                        Set Up Usage :
                    </div>
                    <div class="col-sm-8">
                        <asp:Label runat="server" ID="lbSetUp" width="50" ToolTip="Total setup for the job"></asp:Label>
                        <asp:Label runat="server" Text ="-->" width="50"></asp:Label>
                        <asp:TextBox runat="server"  ID="txtSetupQty" CssClass="form-control" Width="100px" AutoCompleteType="Disabled"  ToolTip="Total setup for the job"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-inline" style="margin:4px;">
                    <div class="col-md-3">
                        Buyoff Usage :
                    </div>
                    <div class="col-md-8">
                        <asp:Label runat="server" ID="lbBuyoff" Width="50px" ToolTip="Total buyoff for the job"></asp:Label>
                        <asp:Label runat="server" Text ="-->" width="50"></asp:Label>
                        <asp:TextBox runat="server"  ID="txtBuyoffQty" CssClass="form-control" Width="100px" AutoCompleteType="Disabled" ToolTip="Total buyoff for the job"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-inline" style="margin:4px;">
                    <div class="col-md-3">
                        Shortage Qty :
                    </div>
                    <div class="col-md-8">
                        <asp:Label runat="server" ID="lbShortage" Width="50px" ToolTip="Total shortage for the job"></asp:Label>
                        <asp:Label runat="server" Text ="-->" width="50"></asp:Label>
                        <asp:TextBox runat="server"  ID="txtShortage" CssClass="form-control" Width="100px" AutoCompleteType="Disabled" ToolTip="Total shortage for the job"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-md-12 panel panel-default">
                 <div class="row" style="padding:10px 10px 0px 20px;">
                     <div class="col-md-12">
                       <b>Material Detail Info</b>
                     </div>
                 </div>

                <hr />

                <div class="row">
                    <div class="col-md-12" style="padding:0px 30px 0 30px;">
                        <asp:Label runat="server" ID="lbResult"></asp:Label>
                        <asp:DataGrid runat="server" Width ="300" ID ="dgMaterialMaintain" CellPadding="10" ForeColor="#333333" GridLines="none" CellSpacing="2" AutoGenerateColumns="false"  CssClass="table">
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <Columns>
                                <asp:BoundColumn DataField="SN" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="materialNo" HeaderText="Material No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderStyle-Width="10%"><ItemTemplate></ItemTemplate></asp:TemplateColumn>

                                <asp:BoundColumn DataField="okQty" HeaderText="OK"></asp:BoundColumn>

                                <asp:BoundColumn DataField="ngQty" HeaderText="Current NG"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Actual NG">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtActualNG" Width="70" CssClass="form-control" style="height:24px; margin:0px; border-radius:4px; border:solid 1px #cccc;"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>

                    </div>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-md-12 panel panel-default" style="padding:10px 10px 0px 40px;">
                <asp:Label runat="server" Font-Bold="true">Lot Complete:</asp:Label>
                
                <hr />
                
                <asp:RadioButtonList runat="server" ID="radiobtnList" >
                    <asp:ListItem Text="Yes"></asp:ListItem>
                    <asp:ListItem Text="No"></asp:ListItem>
                </asp:RadioButtonList>

            </div>
        </div>


        <div class="row">
            <div class="col-md-12 panel panel-default"align="center" style="padding:4px 0px 4px 0px;">
                <asp:Button runat="server" ID="btnConfirm" Text="Login Confirm" Width="150px" Height="38px" CssClass="btn-success" style="border-radius:4px;" data-toggle="modal" data-target="#modalLogin" OnClientClick="return false;" />
            </div>
        </div>

    </div>







    <!-- Login 拟态框 -->
    <div class="modal fade" id="modalLogin" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
	    <div class="modal-dialog" style="width:400px;">
		    <div class="modal-content">
			    <div class="modal-header">
				    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
					    &times;
				    </button>
				    <h4 class="modal-title" id="myModalLabel">
					    <b>User Login</b>
				    </h4>
			    </div>
			    <div class="modal-body">
                    <div class="row"> 
                        <div class="col-md-12 form-inline" >
                            <label style="width:35%">User Name :</label>
                            <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" Width="60%" placeholder ="Please input user name"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row"  style="margin-top:15px;"> 
                        <div class="col-md-12 form-inline" >
                            <label style="width:35%">Password :</label>
                            <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" Width="60%" placeholder ="Please input passworld" type="password" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                    </div>
			    </div>
			    <div class="modal-footer">
				    <button type="button" class="btn btn-danger" data-dismiss="modal">Cancel</button>
                    <asp:Button runat="server" ID="btn_generate" Text="Submit" Width="100px" Height="38px" CssClass="btn-success" OnClick="btn_generate_Click" style="border-radius:4px;" UseSubmitBehavior="false" />
			    </div>
		    </div>
	    </div>
    </div>
    




</asp:Content>