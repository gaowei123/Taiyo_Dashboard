﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PQCPackingMaintenance.aspx.cs" Inherits="DashboardTTS.Webform.PQC.PQCPackingMaintenance" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >

    <style>
        @media (min-width: 1200px) {
            .container{
                max-width: 900px;
            }
        }
        .top10{
            margin-top:10px;
        }
        .bottom6{
            margin-bottom:6px;
        }
    </style>


    <div class="container">
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">PQC Packing Job Maintance</span>
        </div>

        <div class="row top10">
            <div class="col-sm-12 panel panel-default">
               <div class="row form-inline top10">
                    <div class="col-sm-2">
                        Job No:                         
                    </div>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" ID="txtJobNo" ></asp:TextBox>
                    </div>
                    <div class="col-sm-3"></div>
                    <div class="col-sm-3">
                         <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" style="float:right;width:70px;margin: -4px 0 4px 0px;"/>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12 panel panel-default">
                <div class="row form-inline top10">
                    <div class="col-sm-2"><b>Job Info :</b></div>
                    <div class="col-sm-10">
                        <asp:Label runat="server" ID="lbDay" Font-Bold="true"></asp:Label> &nbsp-&nbsp
                        <asp:Label runat="server" ID="lbShift" Font-Bold="true"></asp:Label>&nbsp-&nbsp
                        <asp:Label runat="server" ID="lbJob" Font-Bold="true"></asp:Label> &nbsp;-&nbsp;
                        <asp:Label runat="server" ID="lbTrackingID" Font-Bold="true"></asp:Label>

                        <button type="button" class="btn btn-success"  data-toggle="modal" data-target="#modalLoginEnd" style="float:right; width:70px;">End</button>
                    </div>
                </div>
                <div class="row form-inline top10"> 
                    <div class="col-sm-2">Part No :</div>
                    <div class="col-sm-9">
                        <asp:Label runat="server" ID="lbPartNo"></asp:Label>
                    </div>
                </div>
                <div class="row form-inline bottom6">
                    <div class="col-sm-2">MRP QTY :</div>
                    <div class="col-sm-9">
                        <asp:Label runat="server" ID="lbMrpQty" Width="50px"></asp:Label>                        
                    </div>
                </div>
            </div>
        </div>
        
        <div class="row">
            <div class="col-sm-12 panel panel-default">
                <div class="row form-inline top10">
                    <div class="col-sm-12">
                        <b>Material List</b>
                    </div>
                </div>
                <div class="row form-inline top10">
                    <div class="col-sm-12">
                        <asp:DataGrid runat="server" ID ="dgMaterial" AutoGenerateColumns="false" CssClass="table table-bordered" Width ="100%"  >
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Height="30px" />                      
                            <Columns>                              
                                <asp:BoundColumn DataField="MaterialName" HeaderText="Material Name"></asp:BoundColumn>
                                <asp:BoundColumn DataField="InventoryQty" HeaderText="Inventory Qty"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ScrapQty" HeaderText="Scrap Qty"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Packing Set Qty" >
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbCurPackQty"></asp:Label> 
                                        &nbsp;&nbsp;<asp:Label runat="server" Text="-->"></asp:Label>  &nbsp;&nbsp;
                                        <asp:TextBox runat="server" Width="100" ID="txtUpdatedQty"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </div>
                <div class="row form-inline top10 bottom6">
                    <div class="col-sm-12" style="display:flex; justify-content:center;">
                        <button type="button" class="btn btn-success"  data-toggle="modal" data-target="#modalLoginUpdate" style="width:150px;">Confirm</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

      
    <!-- 维护数量触发的拟态框 -->
    <div class="modal fade" id="modalLoginUpdate" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
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
				    <button type="button" class="btn btn-danger"  data-dismiss="modal">Cancel</button>
                    <asp:Button runat="server" ID="btn_generate" Text="Submit" Width="100px" Height="38px" CssClass="btn-success" OnClick="btn_confirm_Click" style="border-radius:4px;" UseSubmitBehavior="false" />
			    </div>
		    </div>
	    </div>
    </div>
    <!-- 维护数量触发的拟态框 -->

    <!-- End job 触发的拟态框 -->
    <div class="modal fade" id="modalLoginEnd" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
	    <div class="modal-dialog" style="width:400px;">
		    <div class="modal-content">
			    <div class="modal-header">
				    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
					    &times;
				    </button>
				    <h4 class="modal-title">
					    <b>User Login</b>
				    </h4>
			    </div>
			    <div class="modal-body">
                    <div class="row"> 
                        <div class="col-md-12 form-inline" >
                            <label style="width:35%">User Name :</label>
                            <asp:TextBox runat="server" ID="txtUsernameEnd" CssClass="form-control" Width="60%" placeholder ="Please input user name"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row"  style="margin-top:15px;"> 
                        <div class="col-md-12 form-inline" >
                            <label style="width:35%">Password :</label>
                            <asp:TextBox runat="server" ID="txtPasswordEnd" CssClass="form-control" Width="60%" placeholder ="Please input passworld" type="password" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                    </div>
			    </div>
			    <div class="modal-footer">
				    <button type="button" class="btn btn-danger"  data-dismiss="modal">Cancel</button>
                    <asp:Button runat="server" ID="Button1" Text="Submit" Width="100px" Height="38px" CssClass="btn-success" OnClick="btnEnd_Click" style="border-radius:4px;" UseSubmitBehavior="false" />
			    </div>
		    </div>
	    </div>
    </div>
    <!-- End job 触发的拟态框 -->

</asp:Content>