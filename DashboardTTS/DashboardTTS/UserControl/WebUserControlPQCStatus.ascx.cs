using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DashboardTTS.UserControl
{
    public partial class WebUserControlPQCStatus : System.Web.UI.UserControl
    {

        public class MachineStatus
        {
            public const string Checking = "Checking";
            public const string Packing = "Packing";
            public const string NoWIP = "No Schedule";
            public const string ShutDown = "ShutDown";
        }


        #region property

        private string _station = "";
        public string Station
        {
            get
            {
                return _station;
            }
            set
            {
                _station = value;
                this.lbStation.Text = value;
            }
        }
        

        private string _status = MachineStatus.ShutDown;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                this.lbStatus.Text = _status.ToString();
                switch (_status)
                {
                    case (MachineStatus.NoWIP):
                        this.lbStatus.BackColor = StaticRes.McStatusColor.NoWIP;
                        break;
                    case (MachineStatus.Checking):
                        this.lbStatus.BackColor = StaticRes.McStatusColor.Operating;
                        break;
                    case (MachineStatus.Packing):
                        this.lbStatus.BackColor = StaticRes.McStatusColor.Operating;
                        break;
                    case (MachineStatus.ShutDown):
                        this.lbStatus.BackColor = StaticRes.McStatusColor.ShutDown;
                        break;
                }
            }
        }

        private string _lotno = "";
        public string LotNo
        {
            get
            {
                return _lotno;
            }
            set
            {
                _lotno = value;
                this.lbLotno.Text = _lotno.Trim();
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
                this.lbJobNumber.Text = _jobID.Trim();
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
                this.lbPartNo.Text = _partNo.Trim();
            }
        }

        private double _totalQtyCurrent = 0;
        public double TotalQtyCurrent
        {
            get
            {
                return _totalQtyCurrent;
            }
            set
            {
                _totalQtyCurrent = value;
                this.lbLotQty.Text = _totalQtyCurrent.ToString().Trim();
            }
        }




        private double _okQtyCurrent = 0;
        public double OkQtyCurrent
        {
            get
            {
                return _okQtyCurrent;
            }
            set
            {
                _okQtyCurrent = value;
                this.lbOK.Text = _okQtyCurrent.ToString().Trim();
            }
        }


        private double _ngQtyCurrent = 0;
        public double NgQtyCurrent
        {
            get
            {
                return _ngQtyCurrent;
            }
            set
            {
                _ngQtyCurrent = value;
                this.lbNG.Text = _ngQtyCurrent.ToString().Trim();
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
                this.lbRejRate.Text = _Rejrate.Trim();
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
                this.lbRejPPM.Text = _RejPPM.ToString().Trim(); //+ "%";
            }
        }

        private string _imageUrl = "";
        public string ImageUrl
        {
            get
            {
                return _imageUrl;
            }
            set
            {
                _imageUrl = value;
                imgLogo.ImageUrl = _imageUrl;
            }
        }

        private string _operator = "";
        public string Operator
        {
            get
            {
                return _operator;
            }
            set
            {
                _operator = value;
                this.lbOperator.Text = _operator;
            }
        }






        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
    }
}