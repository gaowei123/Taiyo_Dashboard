<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DashboardTTS.Webform.Login" %>

<asp:Content runat="server" ContentPlaceHolderID ="MainContent" >
  
    <div class="container container-fluid" >
        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <div class="row titleRow">
                    <img class="titleImg" src="../../Resources/Images/headericon.gif" />
                    <span class="titleText">User Login</span>
                </div>

                <div class="row" style="margin-top:10px;">
                    <div class="col-md-12 panel panel-default" style="padding-top:10px; padding-bottom:10px;">
                        <div class="row" style="margin-top:15px;"> 
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
                        <hr />
                        <div class="row"  style="margin-top:15px;"> 
                            <div class="col-md-12 form-inline" align="center">
                                <asp:Button ID="Button1" runat="server" Text="Login" CssClass="btn-success" OnClick="btn_Login_Click" style="width:150px; height:34px; border-radius:4px;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4"></div>
        </div>
    </div>


</asp:Content>