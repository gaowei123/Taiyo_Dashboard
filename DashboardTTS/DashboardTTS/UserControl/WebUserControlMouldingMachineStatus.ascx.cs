using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace DashboardTTS.UserControl
{
    public partial class WebUserControlMouldingMachineStatus : System.Web.UI.UserControl
    {

        public enum MachineStatus
        {
            Running,
            Adjustment,
            No_Schedule,
            Mould_Testing,
            Material_Testing,
            Change_Model,
            No_Operator,
            No_Material,
            Break_Time,
            ShutDown,
            Login_Out, 
            Machine_Break,
            Damage_Mould,
            Under_Dev
            
        }



       

        #region property
        private string _machineNo = "";
        public string MachineNo
        {
            get
            {
                return _machineNo;
            }
            set
            {
                _machineNo = value;
                lblMachineNo.Text = _machineNo.ToString().Trim();
            }
        }


        private DateTime _refreshTime;
        public DateTime RefreshTime
        {
            get
            {
                return _refreshTime;
            }
            set
            {
                _refreshTime = value;
            }
        }


        private MachineStatus _status = MachineStatus.ShutDown;
        public MachineStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                lblStatus.Text = _status.ToString();
                switch (_status)
                {
                    case (MachineStatus.Running):
                        {
                            lblStatus.BackColor = StaticRes.MouldingStatusColor.Running;
                            lblStatus.Text = StaticRes.Global.clsConstValue.ConstMouldingStatus.Running;
                            break;
                        }
                    case (MachineStatus.Adjustment):
                        {
                            lblStatus.BackColor = StaticRes.MouldingStatusColor.Adjustment;
                            lblStatus.Text = StaticRes.Global.clsConstValue.ConstMouldingStatus.Adjustment;
                            break;
                        }
                    case (MachineStatus.No_Schedule):
                        {
                            lblStatus.BackColor = StaticRes.MouldingStatusColor.No_Schedule;
                            lblStatus.Text = "No Schedule";
                            break;
                        }
                    case (MachineStatus.Mould_Testing):
                        {
                            lblStatus.BackColor = StaticRes.MouldingStatusColor.Mould_Testing;
                            lblStatus.Text = StaticRes.Global.clsConstValue.ConstMouldingStatus.Mould_Testing;
                            break;
                        }
                    case (MachineStatus.Material_Testing):
                        {
                            lblStatus.BackColor = StaticRes.MouldingStatusColor.Material_Testing;
                            lblStatus.Text = StaticRes.Global.clsConstValue.ConstMouldingStatus.Material_Testing;
                            break;
                        }
                    case (MachineStatus.Change_Model):
                        {
                            lblStatus.BackColor = StaticRes.MouldingStatusColor.Change_Model;
                            lblStatus.Text = StaticRes.Global.clsConstValue.ConstMouldingStatus.Change_Model;
                            break;
                        }
                    case (MachineStatus.No_Operator):
                        {
                            lblStatus.BackColor = StaticRes.MouldingStatusColor.No_Operator;
                            lblStatus.Text = StaticRes.Global.clsConstValue.ConstMouldingStatus.No_Operator;
                            break;
                        }
                    case (MachineStatus.No_Material):
                        {
                            lblStatus.BackColor = StaticRes.MouldingStatusColor.No_Material;
                            lblStatus.Text = StaticRes.Global.clsConstValue.ConstMouldingStatus.No_Material;
                            break;
                        }
                    case (MachineStatus.Break_Time):
                        {
                            lblStatus.BackColor = StaticRes.MouldingStatusColor.Break_Time;
                            lblStatus.Text = "Meal";// StaticRes.Global.clsConstValue.ConstMouldingStatus.Break_Time;
                            break;
                        }
                    case (MachineStatus.ShutDown):
                        {
                            lblStatus.BackColor = StaticRes.MouldingStatusColor.ShutDown;
                            lblStatus.Text = StaticRes.Global.clsConstValue.ConstMouldingStatus.ShutDown;
                            break;
                        }
                    case (MachineStatus.Login_Out):
                        {
                            //lblStatus.Text = MachineStatus.No_Schedule.ToString();
                            lblStatus.BackColor = StaticRes.MouldingStatusColor.Login_Out;
                            lblStatus.Text = "Changing Shift";// StaticRes.Global.clsConstValue.ConstMouldingStatus.Login_Out;
                            break;
                        }
                    case (MachineStatus.Machine_Break):
                        {
                            //lblStatus.Text = MachineStatus.No_Schedule.ToString();
                            lblStatus.BackColor = StaticRes.MouldingStatusColor.MachineBreak;
                            lblStatus.Text = "Machine Break Down";// StaticRes.Global.clsConstValue.ConstMouldingStatus.MachineBreak;
                            break;
                        }
                    case (MachineStatus.Damage_Mould):
                        {
                            //lblStatus.Text = MachineStatus.No_Schedule.ToString();
                            lblStatus.BackColor = StaticRes.MouldingStatusColor.DamageMould;
                            lblStatus.Text = "Mould Damage";// StaticRes.Global.clsConstValue.ConstMouldingStatus.DamageMould;
                            break;
                        }
                    case (MachineStatus.Under_Dev):
                        lblStatus.BackColor = StaticRes.MouldingStatusColor.ShutDown;
                        lblStatus.Text = "Under Develop";
                        break;
                }
            }
        }


        private double _utilizaiton = 0.0;
        public double Utilization
        {
            get
            {
                return _utilizaiton;
            }
            set
            {
                _utilizaiton = value;
                lbUtilization.Text = _utilizaiton.ToString().Trim() + "%";
            }
        }


        private string _model = "";
        public string  Model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
                lb_Model.Text = _model;
            }
        }


        private string _Rejrate = "";
        public string Rejrate
        {
            get
            {
                return _Rejrate;
            }
            set
            {
                _Rejrate = value;
                lb_RejRate.Text = _Rejrate.ToString().Trim() + "%";
            }
        }


        private double _RejPPM = 0.0;
        public double RejPPM
        {
            get
            {
                return _RejPPM;
            }
            set
            {
                _RejPPM = value;
                lb_RejPPM.Text = _RejPPM.ToString().Trim(); //+ "%";
            }
        }


        private string _partNo = "";
        public string PartNo
        {
            get
            {
                return _partNo;
            }
            set
            {
                _partNo = value;
                lblPartNo0.Text = _partNo.ToString().Trim();
            }
        }


        private string _jigNo = "";
        public string JigNo
        {
            get
            {
                return _jigNo;
            }
            set
            {
                _jigNo = value;
                lb_Jig.Text = _jigNo.ToString().Trim();
            }
        }


        private int _totalQtyToday = 0;
        public int TotalQtyToday
        {
            get
            {
                return _totalQtyToday;
            }
            set
            {
                _totalQtyToday = value;
                //   lblTotal0.Text = _totalQtyCurrent.ToString().Trim() + " / " + _totalQtyToday.ToString().Trim();
            }
        }


        private int _totalQtyCurrent = 0;
        public int TotalQtyCurrent
        {
            get
            {
                return _totalQtyCurrent;
            }
            set
            {
                _totalQtyCurrent = value;
                // lblTotal0.Text = _totalQtyCurrent.ToString().Trim()+ " / " + _totalQtyToday.ToString().Trim();
                lblTotal0.Text = _totalQtyCurrent.ToString().Trim();
            }
        }


        private int _okQtyToday = 0;
        public int OkQtyToday
        {
            get
            {
                return _okQtyToday;
            }
            set
            {
                _okQtyToday = value;
                //lblOK0.Text = _okQtyCurrent.ToString().Trim() + " / " + _okQtyToday.ToString().Trim();
            }
        }


        private int _okQtyCurrent = 0;
        public int OkQtyCurrent
        {
            get
            {
                return _okQtyCurrent;
            }
            set
            {
                _okQtyCurrent = value;
                //lblOK0.Text = _okQtyCurrent.ToString().Trim() + " / " + _okQtyToday.ToString().Trim();
                lblOK0.Text = _okQtyCurrent.ToString().Trim();
            }
        }


        private int _ngQtyToday = 0;
        public int NgQtyToday
        {
            get
            {
                return _ngQtyToday;
            }
            set
            {
                _ngQtyToday = value;
                //lblNG0.Text = _ngQtyCurrent.ToString().Trim() + " / " + _ngQtyToday.ToString().Trim(); 
            }
        }


        private int _ngQtyCurrent = 0;
        public int NgQtyCurrent
        {
            get
            {
                return _ngQtyCurrent;
            }
            set
            {
                _ngQtyCurrent = value;
                // lblNG0.Text = _ngQtyCurrent.ToString().Trim() + " / " + _ngQtyToday.ToString().Trim();
                lblNG0.Text = _ngQtyCurrent.ToString().Trim();
            }
        }

        #endregion




        protected void Page_Load(object sender, EventArgs e)
        {
            imgLogo.ImageUrl = "../Resources/Images/MouldingMachine.png";
        }

        public delegate void OnClickEventHandle(object Sender, MachineStatusOnClickEventArgs e);
        private event OnClickEventHandle _onClickEvent;
        public event OnClickEventHandle OnClickEvent
        {
            add
            {
                _onClickEvent += value;
            }
            remove
            {
                _onClickEvent -= value;
            }

        }
        public class MachineStatusOnClickEventArgs
        {
            public MachineStatusOnClickEventArgs(string MachineNo, string Status, string JobID, string PartNo)
            {
                _machineNo = MachineNo;
                _jobID = JobID;
                _partNo = PartNo;
                _status = Status;
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

            private string _status;
            public string Status
            {
                get
                {
                    return _status;
                }
                set
                {
                    _status = value;
                }
            }
            private string _jobID;
            public string JobID
            {
                get
                {
                    return _jobID;
                }
                set
                {
                    _jobID = value;
                }
            }
            private string _partNo;
            public string PartNo
            {
                get
                {
                    return _partNo;
                }
                set
                {
                    _partNo = value;
                }
            }
            #endregion


        }
        //protected void UltraChart1_ChartDataClicked(object sender, Infragistics.UltraChart.Shared.Events.ChartDataEventArgs e)
        //{
        //    //string sLabel = e.RowLabel;
        //    //TimeBarEventArgs ev = new TimeBarEventArgs(this._machineNo, this._day, this._startTime, this._endTime, sLabel);
        //    //OnTimeBarClick(ev);
        //}

        public void OnClick(MachineStatusOnClickEventArgs e)
        {
            if (_onClickEvent != null)
            {
                _onClickEvent(this, e);
            }
        }
        

    }
}