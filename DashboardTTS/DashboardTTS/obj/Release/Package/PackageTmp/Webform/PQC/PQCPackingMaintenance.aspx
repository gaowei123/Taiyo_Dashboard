<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PQCPackingMaintenance.aspx.cs" Inherits="DashboardTTS.Webform.PQC.PQCPackingMaintenance" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >



    <style>
        @media (min-width: 1200px) {
            .container{
                max-width: 900px;
            }
        }
    </style>


    <div class="container">
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">PQC Packing Job Maintance</span>
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
                        <asp:Label runat="server" ID="lbJob" Font-Bold="true"></asp:Label> &nbsp;-&nbsp;
                        <asp:Label runat="server" ID="lbTrackingID" Font-Bold="true"></asp:Label>
                    </div>
                </div>
                <div class="row form-inline" style="margin:14px 4px 4px 4px;"> 
                    <div class="col-md-2">
                        Part No :
                    </div>
                    <div class="col-sm-9">
                        <asp:Label runat="server" ID="lbPartNo"></asp:Label>
                    </div>
                </div>
                <div class="row form-inline" style="margin:4px;">
                    <div class="col-md-2">
                        MRP QTY :
                    </div>
                    <div class="col-md-9">
                        <asp:Label runat="server" ID="lbMrpQty" Width="50px"></asp:Label>
                    </div>
                </div>

                 <hr />

                <div class="row form-inline" style="margin:4px 4px 20px 4px;">
                    <div class="col-md-2">
                        Packed Qty :
                    </div>
                    <div class="col-md-4">
                        <asp:Label runat="server" ID="lbCheckQty" Width="50px"></asp:Label>
                        <asp:Label runat="server" Text ="-->" width="50"></asp:Label>
                        <asp:TextBox runat="server"  ID="txtCheckQty" CssClass="form-control" BackColor="#90ee90" Width="140px" AutoCompleteType="Disabled" ></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:RadioButton runat="server" ID="rbtnComplete" GroupName="complete" Text="Lot Complete" /> &nbsp;
                        <asp:RadioButton runat="server" ID="rbtnNotComplete" GroupName="complete" Text="Lot Not Complete" />
                    </div>
                    <div class="col-md-2">
                         <asp:Button runat="server" ID="btnConfirm" Text="End" Height="38px" Width="100%" CssClass="btn-primary" style="border-radius:4px;" data-toggle="modal" data-target="#modalLogin" OnClientClick="return false;" />
                    </div>
                </div>
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
                    <asp:Button runat="server" ID="btn_generate" Text="Submit" Width="100px" Height="38px" CssClass="btn-success" OnClick="btn_confirm_Click" style="border-radius:4px;" UseSubmitBehavior="false" />
			    </div>
		    </div>
	    </div>
    </div>

   
</asp:Content>