using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Infragistics.UltraChart.Resources;
using System.Collections;

namespace DashboardTTS.UserControl
{
    public partial class WebUserControlTimeBar : System.Web.UI.UserControl
    {

        public class clsBarPoint
        {
            private DateTime _pointTime;
            public DateTime PointTime
            {
                get
                {
                    return _pointTime;
                }
                set
                {
                    _pointTime = value;
                }
            }

            private DateTime _startTime;
            public DateTime StartTime
            {
                get
                {
                    return _startTime;
                }
                set
                {
                    _startTime = value;
                }
            }

            private DateTime _endTime;
            public DateTime EndTime
            {
                get
                {
                    return _endTime;
                }
                set
                {
                    _endTime = value;
                }
            }

            private string _duration;
            public String Duration
            {
                get
                {
                    return _duration;
                }
                set
                {
                    _duration = value;
                }
            }
            private StaticRes.Global.StatusType _oeestatus = StaticRes.Global.StatusType.ShutDown;
            public StaticRes.Global.StatusType OeeStatus
            {
                get
                {
                    return _oeestatus;
                }
                set
                {
                    _oeestatus = value;
                }
            }

        }

        #region property
        private string _machineNo;
        public string MachineNo
        {
            get
            {
                return _machineNo;
            }
            set
            {
                _machineNo = value;
            }
        }
        private DateTime? _day;
        public DateTime? Day
        {
            get
            {
                return _day;
            }
            set
            {
                _day = value;
            }
        }
        private DateTime? _startTime;
        public DateTime? StartTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                _startTime = value;
            }
        }
        private DateTime? _endTime;
        public DateTime? EndTime
        {
            get
            {
                return _endTime;
            }
            set
            {
                _endTime = value;
            }
        }

        private List<clsBarPoint> _points = new List<clsBarPoint>();
        public List<clsBarPoint> Points
        {
            get
            {
                return _points;
            }
            set
            {
                _points = value;
            }
        }


        #region laser color setting
        private Color _colorPD = Color.Green; //StaticRes.McStatusColor.Operating;
        public Color ColorPD
        {
            get
            {
                return _colorPD;
            }
            set
            {
                _colorPD = value;
            }
        }
        private Color _colorIdle =  Color.Blue;
        public Color ColorIdle
        {
            get
            {
                return _colorIdle;
            }
            set
            {
                _colorIdle = value;
            }
        }
        private Color _colorDown = StaticRes.McStatusColor.BreakDown;// Color.Red;
        public Color ColorDown
        {
            get
            {
                return _colorDown;
            }
            set
            {
                _colorDown = value;
            }
        }
        private Color _colorNoWIP = StaticRes.McStatusColor.NoWIP;// Color.Yellow;
        public Color ColorNoWIP
        {
            get
            {
                return _colorNoWIP;
            }
            set
            {
                _colorNoWIP = value;
            }
        }
        private Color _colorAdjustment = StaticRes.McStatusColor.Adjustment;// Color.Orange;
        public Color ColorAdjustment
        {
            get
            {
                return _colorAdjustment;
            }
            set
            {
                _colorAdjustment = value;
            }
        }
        private Color _colorTesting = StaticRes.McStatusColor.Testing;// Color.Blue;
        public Color ColorTesting
        {
            get
            {
                return _colorTesting;
            }
            set
            {
                _colorTesting = value;
            }
        }
        private Color _colorSetup = StaticRes.McStatusColor.Setup;// Color.Brown;
        public Color ColorSetup
        {
            get
            {
                return _colorSetup;
            }
            set
            {
                _colorSetup = value;
            }
        }

        private Color _colorMaintance = StaticRes.McStatusColor.Maintainence;// Color.Purple;
        public Color ColorMaintance
        {
            get
            {
                return _colorMaintance;
            }
            set
            {
                _colorMaintance = value;
            }
        }

        private Color _colorBuyoff = StaticRes.McStatusColor.Buyoff;// Color.Orange;
        public Color ColorBuyoff
        {
            get
            {
                return _colorBuyoff;
            }
            set
            {
                _colorBuyoff = value;
            }
        }

        private Color _colorShutDown = StaticRes.McStatusColor.ShutDown;// Color.Gray;
        public Color ColorShutDown
        {
            get
            {
                return _colorShutDown;
            }
            set
            {
                _colorShutDown = value;
            }
        }
        #endregion

        #region Moulding color setting

        private Color _Moulding_Running = Color.Green;
        private Color _Moulding_Adjustment = StaticRes.MouldingStatusColor.Adjustment;
        private Color _Moulding_No_Schedule = StaticRes.MouldingStatusColor.No_Schedule;
        private Color _Moulding_Mould_Testing = StaticRes.MouldingStatusColor.Mould_Testing;
        private Color _Moulding_Material_Testing = StaticRes.MouldingStatusColor.Material_Testing;
        private Color _Moulding_Change_Model = StaticRes.MouldingStatusColor.Change_Model;
        private Color _Moulding_No_Operator = StaticRes.MouldingStatusColor.No_Operator;
        private Color _Moulding_No_Material = StaticRes.MouldingStatusColor.No_Material;
        private Color _Moulding_Break_Time = StaticRes.MouldingStatusColor.Break_Time;
        private Color _Moulding_ShutDown = StaticRes.MouldingStatusColor.ShutDown;
        private Color _Moulding_MachineBreak = StaticRes.MouldingStatusColor.MachineBreak; 
        private Color _Moulding_DamageMould = StaticRes.MouldingStatusColor.DamageMould;
        private Color _Moulding_Login_Out = StaticRes.MouldingStatusColor.Login_Out;

        #endregion 


        #endregion

        public WebUserControlTimeBar()
        {
           
        }
        public delegate void OnTimeBarClickEventHandle(object Sender, TimeBarEventArgs e); 
        private event OnTimeBarClickEventHandle _timeBarClickEvent;
        public event OnTimeBarClickEventHandle TimeBarClickEvent
        {
            add
            {
                _timeBarClickEvent += value;
            }
            remove
            {
                _timeBarClickEvent -= value;
            }

        }

        //2018 07 06
        public class MyLabelRenderer : IRenderLabel
        {
            public string ToString(Hashtable Context)
            {

                string ReturnString = " Start:" + Context["START_TIME"].ToString()
                           + "\r\n" + "   End:" + Context["END_TIME"].ToString();
                return ReturnString;
            }
        }





        public void reflash()
        {
            this.UltraChart1.Visible = true;
            if (_startTime == null || _endTime == null)
            {
                this.UltraChart1.Visible = false;
                return;
            }
            this.UltraChart1.ID = "Chart" + this.MachineNo.Replace("Machine #", "").Trim();
            this.UltraChart1.Series.Clear();
            this.UltraChart1.Axis.X.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.UltraChart1.Axis.X.Labels.ItemFormat = Infragistics.UltraChart.Shared.Styles.AxisItemLabelFormat.ItemLabel;
            this.UltraChart1.Tooltips.Display = Infragistics.UltraChart.Shared.Styles.TooltipDisplay.MouseMove;
            this.UltraChart1.Tooltips.Format = Infragistics.UltraChart.Shared.Styles.TooltipStyle.Custom;
            // this.UltraChart1.Tooltips.FormatString = "<DataValue:yyyy:MM:dd HH:mm>";
            

            UltraChart1.Tooltips.Format = Infragistics.UltraChart.Shared.Styles.TooltipStyle.Custom;
            UltraChart1.Tooltips.FormatString = "<MY_VALUE>";
            Hashtable MyLabelHashTable = new Hashtable();
            MyLabelHashTable.Add("MY_VALUE", new MyLabelRenderer());
            UltraChart1.LabelHash = MyLabelHashTable;


            Infragistics.UltraChart.Data.GanttSeries s1 = new Infragistics.UltraChart.Data.GanttSeries();
            Infragistics.UltraChart.Data.GanttItem gt1 = new Infragistics.UltraChart.Data.GanttItem();
            gt1.Label = this._machineNo;// DateTime.Now.Date.ToString();

            gt1.Empty = true;
            gt1.PE.Fill = System.Drawing.Color.Red;

            
            int iMaxMinutes = 0;
            iMaxMinutes = int.Parse(Math.Floor((_endTime.Value - _startTime.Value).TotalMinutes).ToString());
            Infragistics.UltraChart.Data.GanttTimeEntry gte = new Infragistics.UltraChart.Data.GanttTimeEntry();



            #region for testing

            //gte = new Infragistics.UltraChart.Data.GanttTimeEntry();
            //gte.Start = DateTime.Parse("2018/6/19 8:00");
            //gte.Label = "8:00 - 10:00";
            //gte.End = DateTime.Parse("2018/6/19 10:00");
            //gte.PE.Fill = Color.Red;
            //gte.PE.Stroke = Color.Green;
            //gte.PE.StrokeWidth = 1;

            //gte.TimeEntryID =  + 1;
            //gte.PE.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.SolidFill;

            //gt1.Times.Add(gte);

            //Infragistics.UltraChart.Data.GanttTimeEntry gte2 = new Infragistics.UltraChart.Data.GanttTimeEntry();
            //gte2.Start = DateTime.Parse("2018/6/19 10:00");
            //gte2.Label = "8:00 - 10:00";
            //gte2.End = DateTime.Parse("2018/6/19 14:00");
            //gte2.PE.Fill = Color.Yellow;
            //gte2.PE.Stroke = Color.Blue;
            //gte2.PE.StrokeWidth = 1;

            //gte.TimeEntryID = +1;
            //gte.PE.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.SolidFill;

            //gt1.Times.Add(gte2);

            #endregion

            #region not use
            //for (int i = 0; i < iMaxMinutes; i++)
            //{
            //    gte = new Infragistics.UltraChart.Data.GanttTimeEntry();
            //    gte.Start = _startTime.Value.AddMinutes(i);
            //    gte.Label = _startTime.Value.AddMinutes(i).ToString("hh:mm");
            //    gte.End = _startTime.Value.AddMinutes(i + 1);



            //    gte.TimeEntryID = i + 1;
            //    gte.PE.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.SolidFill;
            //    gte.PE.StrokeWidth = 1;
            //    gte.PE.Fill = System.Drawing.Color.Transparent;
            //    gte.PE.Stroke = System.Drawing.Color.Transparent;


            //    gt1.Times.Add(gte);
            //}

            //s1.Items.Add(gt1);

            //this.UltraChart1.Series.Add(s1);

            //ChartUpdate(_points, _startTime.Value, _endTime.Value);
            #endregion


            //DBHelp.Reports.LogFile.Log("Timebar_debug", "MachineID:"+ MachineNo + "_points.count:" + _points.Count);
            List<clsBarPoint> lPoints = new List<clsBarPoint>();
            lPoints = funFormatPoints(_points);

            for (int i = 0; i < lPoints.Count; i++)
            {
                gte = new Infragistics.UltraChart.Data.GanttTimeEntry();
                gte.Start = lPoints[i].StartTime;
                gte.Label = "111";// lPoints[i].StartTime.ToString("HH:mm") + "--" + lPoints[i].EndTime.ToString("HH:mm"); 
                gte.End = lPoints[i].EndTime;

                 
                gte.TimeEntryID = i + 1;
                gte.PE.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.SolidFill;
                gte.PE.StrokeWidth = 1;
                //gte.PE.Fill = System.Drawing.Color.Transparent;
                //gte.PE.Stroke = System.Drawing.Color.Transparent;
                
                gte.PE.Fill = getColor(lPoints[i].OeeStatus);
                gte.PE.Stroke = getColor(lPoints[i].OeeStatus);

                gt1.Times.Add(gte);
            }

            //****add Transparent status************** 
            gte = new Infragistics.UltraChart.Data.GanttTimeEntry();
            gte.Start = lPoints[lPoints.Count-1].EndTime;
            //gte.Label = lPoints[i].StartTime.ToString() + "\r\n" + lPoints[i].EndTime.ToString();
            gte.End = _endTime.Value; 

            gte.TimeEntryID = lPoints.Count ;
            gte.PE.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.SolidFill;
            gte.PE.StrokeWidth = 1;
            gte.PE.Fill = System.Drawing.Color.Transparent;
            gte.PE.Stroke = System.Drawing.Color.Transparent; 
            gt1.Times.Add(gte);
            //************************************************


            s1.Items.Add(gt1);

            this.UltraChart1.Series.Add(s1);
            
        }

        private List<clsBarPoint> funFormatPoints(List<clsBarPoint> Points  )
        {
            List<clsBarPoint> OrderPoints = new List<clsBarPoint>();
 
            OrderPoints = Points.OrderBy(n=> n.PointTime ).ToList();

            List<clsBarPoint> lPoints = new List<clsBarPoint>();
            clsBarPoint tmpPoint = null;
            string tmpStatus = "";
            foreach (clsBarPoint pot in OrderPoints)
            {
                try
                {
                    if(tmpStatus != pot.OeeStatus.ToString())
                    {
                        if (tmpPoint != null)
                        {
                            lPoints.Add(tmpPoint);
                        }
                        tmpPoint = new clsBarPoint();
                        tmpPoint.OeeStatus = pot.OeeStatus;
                        tmpPoint.StartTime = pot.PointTime;
                        tmpPoint.EndTime = pot.PointTime;
                        
                        tmpStatus = pot.OeeStatus.ToString();
                       
                    }
                    else
                    {
                        tmpPoint.EndTime = pot.PointTime;
                    }

                    if (tmpPoint.StartTime == tmpPoint.EndTime)
                    {
                        tmpPoint.EndTime = pot.PointTime.AddMinutes(1);
                    }
                }
                catch (Exception ex)
                {
                    string sss = ex.ToString();
                }
            }
            //add last status
            if (tmpPoint != null)
            {
                lPoints.Add(tmpPoint);
            }
            int i = 0;
            //DBHelp.Reports.LogFile.Log("Timebar_debug", "In funFormatPoints");
            //foreach (clsBarPoint point in lPoints)
            //{
            //    i++;
            //    DBHelp.Reports.LogFile.Log("Timebar_debug", "point" + i.ToString() + " in lPoints, startTime:" + point.StartTime + ", endTime:" + point.EndTime);
            //}
            //DBHelp.Reports.LogFile.Log("Timebar_debug", "End funFormatPoints");

            return lPoints;
        }

        public void ChartUpdate(List<clsBarPoint> Points, DateTime StartTime, DateTime EndTime)
        {
            int iMaxSeq = int.Parse(Math.Floor((EndTime - StartTime).TotalMinutes).ToString());
            int iSeq = 0;
            foreach (clsBarPoint pot in Points)
            {
                try
                {
                    iSeq = int.Parse(Math.Floor(( pot.PointTime - StartTime).TotalMinutes).ToString());

                    if (iSeq < iMaxSeq)
                    {
                        ((Infragistics.UltraChart.Data.GanttSeries)this.UltraChart1.Series[0]).Items[0].Times[iSeq].PE.Fill = getColor(pot.OeeStatus);
                        ((Infragistics.UltraChart.Data.GanttSeries)this.UltraChart1.Series[0]).Items[0].Times[iSeq].PE.Stroke = getColor(pot.OeeStatus);
                    }
                }
                catch (Exception ex)
                {
                    string sss = ex.ToString();
                }
            }
        }

        private Color getColor(StaticRes.Global.StatusType OeeStatus)
        {
            switch (OeeStatus)
            {
                #region Laser color
                case (StaticRes.Global.StatusType.Adjustment):
                    {
                        return _colorAdjustment;
                        break;
                    }
                case (StaticRes.Global.StatusType.PD):
                    {
                        return _colorPD;
                        break;
                    }
                case (StaticRes.Global.StatusType.Idle):
                    {
                        return _colorIdle;
                        break;
                    }
                case (StaticRes.Global.StatusType.NoWIP):
                    {
                        return _colorNoWIP;
                        break;
                    }
                case (StaticRes.Global.StatusType.BreakDown):
                    {
                        return _colorDown;
                        break;
                    }
                case (StaticRes.Global.StatusType.ShutDown):
                    {
                        return _colorShutDown;
                        break;
                    }
                case (StaticRes.Global.StatusType.Testing):
                    {
                        return _colorTesting;
                        break;
                    }
                case (StaticRes.Global.StatusType.Setup):
                    {
                        return _colorSetup;
                        break;
                    }
                case (StaticRes.Global.StatusType.Maintance):
                    {
                        return _colorMaintance;
                        break;
                    }
                case (StaticRes.Global.StatusType.Buyoff):
                    {
                        return _colorBuyoff;
                        break;
                    }
                #endregion

                #region Moulding color
                case (StaticRes.Global.StatusType.Running):
                    {
                        return _Moulding_Running;
                        break;
                    }

                //case (StaticRes.Global.StatusType.Moulding_Adjustment):
                //    {
                //        return _Moulding_Adjustment;
                //        break;
                //    }
                case (StaticRes.Global.StatusType.No_Schedule):
                    {
                        return _Moulding_No_Schedule;
                        break;
                    }
                case (StaticRes.Global.StatusType.Mould_Testing):
                    {
                        return _Moulding_Mould_Testing;
                        break;
                    }
                case (StaticRes.Global.StatusType.Material_Testing):
                    {
                        return _Moulding_Material_Testing;
                        break;
                    }
                case (StaticRes.Global.StatusType.Change_Model):
                    {
                        return _Moulding_Change_Model;
                        break;
                    }
                case (StaticRes.Global.StatusType.No_Operator):
                    {
                        return _Moulding_No_Operator;
                        break;
                    }
                case (StaticRes.Global.StatusType.No_Material):
                    {
                        return _Moulding_No_Material;
                        break;
                    }
                case (StaticRes.Global.StatusType.Break_Time):
                    {
                        return _Moulding_Break_Time;
                        break;
                    }
                case (StaticRes.Global.StatusType.MachineBreak):
                    {
                        return _Moulding_MachineBreak;
                        break;
                    }
                case (StaticRes.Global.StatusType.DamageMould):
                    {
                        return _Moulding_DamageMould;
                        break;
                    }
                //case (StaticRes.Global.StatusType.ShutDown):
                //    {
                //        return _Moulding_ShutDown;
                //        break;
                //    }
                case (StaticRes.Global.StatusType.Login_Out):
                    {
                        return _Moulding_Login_Out;
                        break;
                    }
                    
                #endregion


                default:
                    {
                        return _colorShutDown;
                        break;
                    }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public class TimeBarEventArgs
        {
            public TimeBarEventArgs(string MachineNo, DateTime? Day, DateTime? StartTime, DateTime? EndTime, string LabelInfo)
            {
                _machineNo = MachineNo;
                _day = Day;
                _startTime = StartTime;
                _endTime = EndTime;
                _labelInfo = LabelInfo;
            }
            #region property
            private string _machineNo;
            public string MachineNo
            {
                get
                {
                    return _machineNo;
                }
                set
                {
                    _machineNo = value;
                }
            }
            private DateTime? _day;
            public DateTime? Day
            {
                get
                {
                    return _day;
                }
                set
                {
                    _day = value;
                }
            }
            private DateTime? _startTime;
            public DateTime? StartTime
            {
                get
                {
                    return _startTime;
                }
                set
                {
                    _startTime = value;
                }
            }
            private DateTime? _endTime;
            public DateTime? EndTime
            {
                get
                {
                    return _endTime;
                }
                set
                {
                    _endTime = value;
                }
            }
            private string _labelInfo;
            public string LabelInfo
            {
                get
                {
                    return _labelInfo;
                }
                set
                {
                    _labelInfo = value;
                }
            }

            #endregion


        }
        //protected void UltraChart1_ChartDataClicked(object sender, Infragistics.UltraChart.Shared.Events.ChartDataEventArgs e)
        //{
        //    string sLabel = e.RowLabel;
        //    TimeBarEventArgs ev = new TimeBarEventArgs(this._machineNo, this._day, this._startTime, this._endTime, sLabel);
        //    OnTimeBarClick(ev);
        //}
        
        public void OnTimeBarClick(TimeBarEventArgs e)
        {
            if (_timeBarClickEvent != null)
            { 
                _timeBarClickEvent(this, e);
            }
            
        }
     
    }
}