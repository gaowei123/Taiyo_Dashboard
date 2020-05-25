using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
namespace DashboardTTS.UserControl
{
    public partial class WebUserControlMachineStatus : System.Web.UI.UserControl
    {

        public enum MachineStatus
        {
            Run,        //运行
            Idle,       //机器开着,但没任务
            NoWIP,      //no  schedule
            Adjustment, //调整
            Down,       //机器坏了
            ShutDown,   //关机了
            Unavailability,
            Testing,
            Maintance,
            Setup,
            Buyoff
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
                lblMachineNo.Text = "Machine " + _machineNo.ToString().Trim();
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

                if (_status.ToString() == "NoWIP")
                {
                    lblStatus.Text = "No Schedule";
                }
                else
                {
                    lblStatus.Text = _status.ToString();
                }
                
                switch (_status)
                {
                    case (MachineStatus.Adjustment):
                        {
                            lblStatus.BackColor = StaticRes.McStatusColor.Adjustment;
                            break;
                        }
                    case (MachineStatus.NoWIP):
                        {
                            lblStatus.BackColor = StaticRes.McStatusColor.NoWIP;
                            break;
                        }
                    case (MachineStatus.Down):
                        {
                            lblStatus.BackColor = StaticRes.McStatusColor.BreakDown;
                            break;
                        }
                    case (MachineStatus.Run):
                        {
                            lblStatus.BackColor = StaticRes.McStatusColor.Operating;
                            break;
                        } 
                    case (MachineStatus.ShutDown):
                        {
                            lblStatus.BackColor = StaticRes.McStatusColor.ShutDown;
                            break;
                        }
                    case (MachineStatus.Testing):
                        {
                            lblStatus.BackColor = StaticRes.McStatusColor.Testing;
                            break;
                        }
                    case (MachineStatus.Setup):
                        {
                            lblStatus.BackColor = StaticRes.McStatusColor.Setup;
                            break;
                        }
                    case (MachineStatus.Maintance):
                        {
                            lblStatus.BackColor = StaticRes.McStatusColor.Maintainence;
                            break;
                        }
                    case (MachineStatus.Buyoff):
                        {
                            lblStatus.BackColor = StaticRes.McStatusColor.Buyoff;
                            break;
                        }
                }
            }
        }

        private double _utilization = 0.0;
        public double Utilization
        {
            get
            {
                return _utilization;
            }
            set
            {
                _utilization = value;
                lbUtilization.Text = _utilization.ToString().Trim() + "%";
            }
        }




        private string _Rejrate = "";
        public string  Rejrate
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



        private string _lotNo = "";
        public string LotNo
        {
            get
            {
                return _lotNo;
            }
            set
            {
                _lotNo = value;
                this.lbLotNo.Text = _lotNo;
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

        private string _jobID = "";
        public string JobID
        {
            get
            {
                return _jobID;
            }
            set
            {
                _jobID = value;
                lblJobID0.Text = _jobID.ToString().Trim();
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

        public WebUserControlMachineStatus()
        {
          
         
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.lblMachineNo.Text == "Machine 1" ||
                this.lblMachineNo.Text == "Machine 2" ||
                this.lblMachineNo.Text == "Machine 3" ||
                this.lblMachineNo.Text == "Machine 4" ||
                this.lblMachineNo.Text == "Machine 5")
            {
             

                imgLogo.ImageUrl = "~/Resources/Images/LaserMachine_RobortArm.png";
            }
            else
            {
                imgLogo.ImageUrl = "~/Resources/Images/LaserMachine_TurnTable.png";
            }

            #region  not use
            //lblAvailability0.Text = _availability.ToString().Trim() + "%";
            //lblPerformence0.Text = _performence.ToString().Trim() + "%";
            //lblQuality0.Text = _quality.ToString().Trim() + "%";
            //lblOEE0.Text = _oEE.ToString().Trim() + "%";
            //lblJobID0.Text = _jobID.ToString().Trim();
            //lblPartNo0.Text = _partNo.ToString().Trim();
            //lblMachineNo.Text = "Machine " + _machineNo.ToString().Trim();
            //lblNG0.Text = _ngQtyCurrent.ToString().Trim() + " / " + _ngQtyToday.ToString().Trim();
            //lblOK0.Text = _okQtyCurrent.ToString().Trim() + " / " + _okQtyToday.ToString().Trim();
            //lblTotal0.Text = _totalQtyCurrent.ToString().Trim() + " / " + _totalQtyToday.ToString().Trim();

            //lblStatus.Text = _status.ToString();
            //switch (_status)
            //{
            //    case (MachineStatus.Adjustment):
            //        {
            //            lblStatus.BackColor = Color.Red;
            //            break;
            //        }
            //    case (MachineStatus.Down):
            //        {
            //            lblStatus.BackColor = Color.Red;
            //            break;
            //        }
            //    case (MachineStatus.Run):
            //        {
            //            lblStatus.BackColor = Color.LawnGreen;
            //            break;
            //        }
            //    case (MachineStatus.ShutDown):
            //        {
            //            lblStatus.BackColor = Color.LawnGreen;
            //            break;
            //        }
            //}

            #endregion
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
            public MachineStatusOnClickEventArgs(string MachineNo,  string Status, string JobID, string PartNo)
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
        protected void UltraChart1_ChartDataClicked(object sender, Infragistics.UltraChart.Shared.Events.ChartDataEventArgs e)
        {
            //string sLabel = e.RowLabel;
            //TimeBarEventArgs ev = new TimeBarEventArgs(this._machineNo, this._day, this._startTime, this._endTime, sLabel);
            //OnTimeBarClick(ev);
        }
        
        public void OnClick(MachineStatusOnClickEventArgs e)
        {
            if (_onClickEvent != null)
            {
                _onClickEvent(this, e);
            }
            
        }
     
    }
}