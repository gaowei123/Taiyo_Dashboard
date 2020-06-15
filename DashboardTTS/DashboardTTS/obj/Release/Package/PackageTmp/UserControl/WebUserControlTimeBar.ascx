<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlTimeBar.ascx.cs" Inherits="DashboardTTS.UserControl.WebUserControlTimeBar" %>
 
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.UltraWebChart" tagprefix="igchart" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.UltraChart.Resources.Appearance" tagprefix="igchartprop" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.UltraChart.Data" tagprefix="igchartdata" %>
 

 <script type="text/javascript" id="igClientScript">





function UltraChart1_ClientOnShowTooltip(text, tooltip_ref){
    //Add code to handle your event here.
    //alert(text + "    " + tooltip_ref);
    Tip(text, CLICKSTICKY, true)
}

//function UltraChart1_ClientOnMouseClick(this_ref, row, column, value, row_label, column_label, evt_type, layer_id) {
//    //Add code to handle your event here.
   
//}
function UltraChart1_ClientOnMouseOver(this_ref, row, column, value, row_label, column_label, evt_type, layer_id){
    //Add code to handle your event here.
    //alert(this_ref + "   " + row + "   " + column + "   " + value.length + " \r\n  " +
    //value[0] + " \r\n  " +
    //value[1] + " \r\n  " + 
    //value[2] + " \r\n  " +
    //value[3] + " \r\n  " +
    //value[4] + " \r\n  " +
    //value[5] + " \r\n  " +
    //value[6] + " \r\n  " +
    //value[7] + " \r\n  " +
    //value[8] + " \r\n  " +
    //value[9] + " \r\n  " +
    //value[10] + " \r\n  " +
    //value[11] + " \r\n  " +
    //value[12] + " \r\n  " +
    //value[13] + " \r\n  " +
    //value[14] + " \r\n  " +
    //value[15] + " \r\n  " +
    //value[16] + " \r\n  " + 
    //row_label + "   " + column_label + "   " + evt_type + "   " + layer_id);
}

function UltraChart1_ClientOnShowCrosshair(x, y){
    //Add code to handle your event here.
   // alert( x + " \r\n  " + y);
}


function UltraChart1_ClientOnMouseOut(this_ref, row, column, value, row_label, column_label, evt_type, layer_id){
    //Add code to handle your event here.
    UnTip()
}

</script>
 

 <script src="..\Scripts\wz_tooltip.js" language="javascript" type="text/javascript"></script> 
   
  <igchart:ultrachart ID="UltraChart1" runat="server" BackgroundImageFileName="" ChartImagesPath="./ChartImages" BorderWidth="0px" ChartType="GanttChart" EmptyChartText="Data Not Available. Please call UltraChart.Data.DataBind() after setting valid Data.DataSource" Version="7.3" Width="1140px" Height="108px" BackColor="#F7FBFF"  EnableCrossHair="True" TextRenderingHint="SingleBitPerPixelGridFit" ClientIDMode="Static">
      <ClientSideEvents ClientOnShowTooltip="UltraChart1_ClientOnShowTooltip" ClientOnMouseOver="UltraChart1_ClientOnMouseOver" ClientOnShowCrosshair="UltraChart1_ClientOnShowCrosshair"  ClientOnMouseOut="UltraChart1_ClientOnMouseOut" />
      <Border Thickness="0" />

        <Effects><Effects>
        <igchartprop:gradienteffect></igchartprop:GradientEffect>
        </Effects>
        </Effects>

        <ColorModel ModelStyle="Wireframe" ColorBegin="Pink" ColorEnd="DarkRed" AlphaLevel="150"></ColorModel>

        <GanttChart>
            <Columns SeriesLabelsColumnIndex="2" StartTimeColumnIndex="0" />
            <CompletePercentagesPE Fill="Yellow" />
            <EmptyPercentagesPE Fill="White" />
            <LinkLineStyle EndStyle="ArrowAnchor" />
            <OwnersLabelStyle Font="Microsoft Sans Serif, 7.8pt" />
      </GanttChart>

        <Axis>
        <PE ElementType="None" Fill="Cornsilk"></PE>

        <X Visible="True" LineThickness="1" TickmarkStyle="DataInterval" TickmarkInterval="120" TickmarkIntervalType="Minutes" Extent="42">
        <MajorGridLines Visible="True" DrawStyle="Dot" Color="black" Thickness="1" AlphaLevel="255"></MajorGridLines>

        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

        <Labels ItemFormatString="&lt;ITEM_LABEL:HH:mm&gt;" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="VerticalLeftFacing">
        <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="VerticalLeftFacing">
        <Layout Behavior="Auto"></Layout>
        </SeriesLabels>

        <Layout Behavior="Auto"></Layout>
        </Labels>
        </X>

        <Y Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="2" Extent="76">
        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

        <Labels ItemFormatString="&lt;ITEM_LABEL&gt;" Font="Verdana, 10pt" FontColor="Black" HorizontalAlign="Center" VerticalAlign="Center" Orientation="Horizontal">
        <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Center" VerticalAlign="Center" Orientation="VerticalLeftFacing">
        <Layout Behavior="Auto"></Layout>
        </SeriesLabels>

        <Layout Behavior="Auto"></Layout>
        </Labels>
        </Y>

        <Y2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="1">
        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

        <Labels ItemFormatString="&lt;ITEM_LABEL&gt;" Visible="False" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="Horizontal">
        <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="VerticalLeftFacing">
        <Layout Behavior="Auto"></Layout>
        </SeriesLabels>

        <Layout Behavior="Auto"></Layout>
        </Labels>
        </Y2>

        <X2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="25" TickmarkIntervalType="Seconds">
        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

        <Labels ItemFormatString="&lt;ITEM_LABEL:MM-dd-yy&gt;" Visible="False" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="VerticalLeftFacing">
        <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
        <Layout Behavior="Auto"></Layout>
        </SeriesLabels>

        <Layout Behavior="Auto"></Layout>
        </Labels>
        </X2>

        <Z Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

        <Labels ItemFormatString="" Visible="False" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
        <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
        <Layout Behavior="Auto"></Layout>
        </SeriesLabels>

        <Layout Behavior="Auto"></Layout>
        </Labels>
        </Z>

        <Z2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

        <Labels ItemFormatString="" Visible="False" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
        <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
        <Layout Behavior="Auto"></Layout>
        </SeriesLabels>

        <Layout Behavior="Auto"></Layout>
        </Labels>
        </Z2>
        </Axis>

      <Tooltips HotTrackingFillColor="Pink"   Format="Custom" FormatString="&lt;DATA_ROW&gt;,&lt;DATA_COLUMN&gt;: &lt;ITEM_VALUE:00.##&gt;" Padding="10" Overflow="ChartArea" HotTrackingOutlineColor="DarkMagenta" HotTrackingEnabled="False" />

      <CompositeChart>
          <Series>
              <igchartdata:ganttseries Key="ser1" >
                  <Items>
                      <igchartdata:ganttitem Empty="True" Key="2018-01-29">
                          <times>
                              <igchartdata:gantttimeentry End="00:02:00" Label="00:02" PercentComplete="50" Start="00:01" TimeEntryID="1">
                                  <pe elementtype="None" />
                              </igchartdata:GanttTimeEntry>
                              <igchartdata:gantttimeentry End="00:03:00" Label="00:03" Start="00:02" TimeEntryID="2">
                                  <pe elementtype="None" StrokeWidth="1" />
                              </igchartdata:GanttTimeEntry>
                              <igchartdata:gantttimeentry End="00:04:00" Label="00:04" Start="00:03" TimeEntryID="3">
                                  <pe elementtype="None" />
                              </igchartdata:GanttTimeEntry>
                          </times>
                          <pe elementtype="None" />
                      </igchartdata:ganttitem>
                  </Items>
              </igchartdata:GanttSeries>
          </Series>
      </CompositeChart>
    </igchart:ultrachart>
 

 