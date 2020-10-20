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
        public class UIModel {
            public string Station { get; set; }
            public string Status { get; set; }
            public string LotNo { get; set; }
            public string JobNo { get; set; }
            public string PartNo { get; set; }
            public double LotQty { get; set; }
            public double OK { get; set; }
            public double NG { get; set; }
            public double RejRate { get; set; }
            public string Operator { get; set; }
          
        }
        
        public void SetUI(UIModel model)
        {
            this.lbStation.Text = model.Station;
            this.lbStatus.Text = model.Status;
            this.lbLotNo.Text = model.LotNo;
            this.lbJobNo.Text = model.JobNo;
            this.lbPartNo.Text = model.PartNo;
            this.lbLotQty.Text = model.LotQty.ToString();
            this.lbOK.Text = model.OK.ToString();
            this.lbNG.Text = model.NG.ToString();
            this.lbRejRate.Text = model.RejRate.ToString("0.00")+ "%";
            this.lbOP.Text = model.Operator;
            
            this.lbStatus.BackColor = GetStatusColor(model.Status);
        }
        
        private System.Drawing.Color GetStatusColor(string status)
        {
            System.Drawing.Color statusColor = new System.Drawing.Color();
            switch (status)
            {
                case StaticRes.Global.PQCStatus.Checking:
                case StaticRes.Global.PQCStatus.Packing:              
                    statusColor = StaticRes.PQCStautsColor.Run;
                    break;
                case StaticRes.Global.PQCStatus.NoSchedule:
                    statusColor = StaticRes.PQCStautsColor.NoSchedule;
                    break;
                case StaticRes.Global.PQCStatus.Shutdown:
                    statusColor = StaticRes.PQCStautsColor.Shutdown;
                    break;

                default:
                    throw new NullReferenceException("no such status " + status);
            }

            return statusColor;
        }
    }
}