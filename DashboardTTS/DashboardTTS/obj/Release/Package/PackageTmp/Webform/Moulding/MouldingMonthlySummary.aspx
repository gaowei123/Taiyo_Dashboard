<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MouldingMonthlySummary.aspx.cs" Inherits="DashboardTTS.Webform.Moulding.MouldingMonthlySummary" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" >












    <div class="container-fluid">
        <div class="row titleRow">
            <img class="titleImg" src="../../Resources/Images/headericon.gif" />
            <span class="titleText">Moulding Monthly Report</span>
        </div>


        <div class="row">
            <div class="col-md-12 panel panel-default searchingPanel" >
                <div class="row form-inline searchingBar ">
                    <div class="col-md-3">
                        <label style="width:35%">Year :</label>
                        <asp:DropDownList runat="server" ID="ddlYear" CssClass="form-control" Width="60%"></asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label style="width:60%">Month For Traceability&Summary:</label>
                        <asp:DropDownList runat="server" ID="ddlMonth" CssClass="form-control" Width="35%">
                            <asp:ListItem Text="January" Value="1"></asp:ListItem>
                            <asp:ListItem Text="February" Value="2"></asp:ListItem>
                            <asp:ListItem Text="March" Value="3"></asp:ListItem>
                            <asp:ListItem Text="April" Value="4"></asp:ListItem>
                            <asp:ListItem Text="May" Value="5"></asp:ListItem>
                            <asp:ListItem Text="June" Value="6"></asp:ListItem>
                            <asp:ListItem Text="July" Value="7"></asp:ListItem>
                            <asp:ListItem Text="August" Value="8"></asp:ListItem>
                            <asp:ListItem Text="September" Value="9"></asp:ListItem>
                            <asp:ListItem Text="October" Value="10"></asp:ListItem>
                            <asp:ListItem Text="November" Value="11"></asp:ListItem>
                            <asp:ListItem Text="December" Value="12"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label style="width:35%">Type For Summary:</label>
                        <asp:DropDownList runat="server" ID="ddlType" CssClass="form-control" Width="60%">
                            <asp:ListItem Text="All" Value=""></asp:ListItem>
                            <asp:ListItem Text="Button" Value="Button"></asp:ListItem>
                            <asp:ListItem Text="Knob" Value="Knob"></asp:ListItem>
                            <asp:ListItem Text="Len" Value="Len"></asp:ListItem>
                            <asp:ListItem Text="Panel" Value="Panel"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3" align="right" style="padding-right:2%;">
                        <asp:Button ID="Button1" runat="server" Text="Generate" OnClick="btnGenerate_Click" CssClass="btn-success" style="width:100px; height:34px; border-radius:4px;" />
                    </div>
                </div>

            </div>
        </div>


        <div class="row">
            <div class="col-md-12 panel panel-default" style="padding-top:10px;padding-bottom:10px;">


                <ul id="myTab" class="nav nav-tabs">
                    <li class="active"><a href="#Main" data-toggle="tab">Main</a></li>
                    <li><a href="#Rejection" data-toggle="tab">Rejection</a></li>
                    <li><a href="#Summary" data-toggle="tab">Summary</a></li>
                    <li><a href="#Traceability" data-toggle="tab">Traceability</a></li>
                </ul>


                <!-- 每个tab页对应的内容 -->
                <div id="myTabContent" class="tab-content" style="margin-top:8px;">
                    <div class="tab-pane fade in active" id="Main">

                        <asp:DataGrid runat="server" CssClass="table table-hover" Width ="100%" ID ="dgMain" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="false" >
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <EditItemStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center"  VerticalAlign="Middle" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundColumn DataField="SN" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Month" HeaderText="Month" Visible="false"></asp:BoundColumn>
                                <asp:ButtonColumn DataTextField="Month" HeaderText="Month" CommandName="LinkDetail"></asp:ButtonColumn>
                                <asp:BoundColumn DataField="Button" HeaderText="Button"></asp:BoundColumn> 
                                <asp:BoundColumn DataField="ButtonRej%" HeaderText="ButtonRej%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Knob" HeaderText="Knob"></asp:BoundColumn>
                                <asp:BoundColumn DataField="KnobRej%" HeaderText="KnobRej%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Len" HeaderText="Len"></asp:BoundColumn>
                                <asp:BoundColumn DataField="LenRej%" HeaderText="LenRej%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Panel" HeaderText="Panel"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PanelRej%" HeaderText="PanelRej%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Total Parts Produce" HeaderText="Total Parts Produce"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Total Parts Cost" HeaderText="Total Parts Cost"></asp:BoundColumn>

                                <asp:BoundColumn DataField="Good Parts Qty" HeaderText="Good Parts Qty"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Good Parts Cost" HeaderText="Good Parts Cost"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Reject Parts Qty" HeaderText="Reject Parts Qty"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Reject Parts Cost" HeaderText="Reject Parts Cost"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Rej%" HeaderText="Rej%"></asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                                
                        <asp:Label ID="lbMainResult" runat="server"></asp:Label>                  
                    </div>


                    <div class="tab-pane fade" id="Rejection">
                        <asp:DataGrid runat="server"  CssClass="table table-hover" Width ="100%" ID ="dgRejection" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="false" style="line-height:14px">
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <EditItemStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center"  VerticalAlign="Middle" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundColumn DataField="Month" HeaderText="Month"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Output" HeaderText="Output"></asp:BoundColumn>
                                <asp:BoundColumn DataField="rejectQty" HeaderText="Rej</br>Qty"></asp:BoundColumn>
                                <asp:BoundColumn DataField="rejRate" HeaderText="Rej%"></asp:BoundColumn>

                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="White Dot" HeaderText="White</br>Dot"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Scratches" HeaderText="Scratches"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Dented Mark" HeaderText="Dented</br>Mark"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Shinning Dot" HeaderText="Shinning</br>Dot"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Black Mark" HeaderText="Black</br>Mark"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Sink Mark" HeaderText="Sink</br>Mark"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Flow Mark" HeaderText="Flow</br>Mark"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="High Gate" HeaderText="High</br>Gate"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Silver Steak" HeaderText="Silver</br>Steak"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Black Dot" HeaderText="Black</br>Dot"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Oil Stain" HeaderText="Oil</br>Stain"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Flow Line" HeaderText="Flow</br>Line"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Over-Cut" HeaderText="Over</br>Cut"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Crack" HeaderText="Crack"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Short Mold" HeaderText="Short</br>Mold"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Stain Mark" HeaderText="Stain</br>Mark"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Weld Line" HeaderText="Weld</br>Line"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Flashes" HeaderText="Flashes"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Foreign Materials" HeaderText="Foreign</br>Materials"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Drag" HeaderText="Drag"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Material Bleed" HeaderText="Material</br>Bleed"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Bent" HeaderText="Bent"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Defrom" HeaderText="Defrom"></asp:BoundColumn>
                                <asp:BoundColumn HeaderStyle-Width="10px" DataField="Gas Mark" HeaderText="Gas</br>Mark"></asp:BoundColumn>

                            </Columns>
                        </asp:DataGrid>

                        <asp:Label ID="lbRejResult" runat="server"></asp:Label>    
                    </div>


                    <div  class="tab-pane fade" id="Summary">
                        <div id="divScrollSummary" style="overflow: scroll;">
                            <asp:DataGrid runat="server" CssClass="table table-hover"  Width ="100%" ID ="dgSummary" CellPadding="10" ForeColor="#333333" GridLines="None" CellSpacing="2" AutoGenerateColumns="false" >
                                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                <EditItemStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" Height="30px" HorizontalAlign="Center"  VerticalAlign="Middle" />
                                <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="None" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="None" PageButtonCount="5" />
                                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <Columns>
                                    <asp:BoundColumn DataField="Parts_No" HeaderText="Parts No"></asp:BoundColumn> 
                                    <asp:BoundColumn DataField="Type" HeaderText="Type"></asp:BoundColumn> 

                                    <asp:BoundColumn DataField="CavityCount" HeaderText="Cavity Count"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Total_Parts_Produce" HeaderText="Total Parts Produce"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="OK_QTY" HeaderText="OK QTY"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Amount_SGD" HeaderText="Amount SGD"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Reject_Qty" HeaderText="Reject Qty"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Reject_Cost_SGD" HeaderText="Reject Cost SGD"></asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>

                        <asp:Label ID="lbSummaryResult" runat="server"></asp:Label>    
                       
                    </div>


                    <div class="tab-pane fade" id="Traceability">
                        <div id="divScrollTraceability" style="overflow: scroll;">
                            <asp:DataGrid runat="server" CssClass="table table-hover"  Width ="100px" ID ="dgTraceability" CellPadding="10" ForeColor="#333333" CellSpacing="2" AutoGenerateColumns="true" class="table" >
                                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                <EditItemStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" BorderStyle="Solid" BorderWidth="1px"  Wrap="False" Height="30px" HorizontalAlign="Center"  VerticalAlign="Middle" />
                                <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <PagerStyle BackColor="#284775" HorizontalAlign="Center" BorderStyle="Solid" BorderWidth="1px"   PageButtonCount="5" />
                                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <Columns>
                                       
                                </Columns>
                            </asp:DataGrid>
                            </div>

                        <asp:Label ID="lbTraceabilityResult" runat="server"></asp:Label>    
                       
                </div>

                </div>
            </div>
        </div>
    </div>

    <script>

      
        $(document).ready(function () {

            var height = $(window).height() - 290;

            $('#divScrollSummary').height(height);
            $('#divScrollTraceability').height(height);

        });



    </script>

 </asp:Content>



