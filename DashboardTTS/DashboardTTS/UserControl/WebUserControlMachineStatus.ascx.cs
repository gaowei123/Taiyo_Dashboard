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
        public class UIModel
        {
            public string MachineID { get; set; }
            public string Status { get; set; }
            public System.Drawing.Color StatusColor { get; set; }
            public string ImgURL { get; set; }
            public string PartNo { get; set; }
            public string LotNo { get; set; }
            public string JobNo { get; set; }
            public decimal LotQty { get; set; }
            public decimal OKQty { get; set; } 
            public decimal NGQty { get; set; }
            public string RejRate { get; set; }
            public string UsedRate { get; set; }
        }
        
        public WebUserControlMachineStatus()
        {

        }

        public void SetUI(UIModel model)
        {
            this.lbMachineID.Text = model.MachineID;
            this.lbPartNo.Text = model.PartNo;
            this.lbStatus.Text = model.Status;
            this.lbStatus.BackColor = GetStatusColor(model.Status);
            this.lbLotNo.Text = model.LotNo;


            this.imgLogo.ImageUrl = model.ImgURL;
            this.lbJobNo.Text = model.JobNo;
            this.lbLotQty.Text = model.LotQty.ToString();
            this.lbOKQty.Text = model.OKQty.ToString();
            this.lbNGQty.Text = model.NGQty.ToString();
            this.lbRejRate.Text = model.RejRate;
            this.lbUsedRate.Text = model.UsedRate;
        }

        public void SetShutdown(string machineID, string imgRUL)
        {
            this.lbMachineID.Text = machineID;
            this.lbPartNo.Text = "";
            this.lbStatus.Text = StaticRes.Global.LaserStatus.Shutdown;
            this.lbStatus.BackColor = GetStatusColor(StaticRes.Global.LaserStatus.Shutdown);
            this.lbLotNo.Text = "";


            this.imgLogo.ImageUrl = imgRUL;
            this.lbJobNo.Text = "";
            this.lbLotQty.Text = "0";
            this.lbOKQty.Text = "0";
            this.lbNGQty.Text = "0";
            this.lbRejRate.Text = "0.00%";
            this.lbUsedRate.Text = "0.00%";
        }
        
        private System.Drawing.Color GetStatusColor(string status)
        {
            System.Drawing.Color statusColor = new System.Drawing.Color();
            switch (status)
            {
                case StaticRes.Global.LaserStatus.Run:
                case StaticRes.Global.LaserStatus.Setup:
                case StaticRes.Global.LaserStatus.Testing:
                case StaticRes.Global.LaserStatus.Buyoff:
                    statusColor = StaticRes.MainStatusColor.Run;
                    break;

                case StaticRes.Global.LaserStatus.NoSchedule:
                    statusColor = StaticRes.MainStatusColor.NoSchedule;
                    break;

                case StaticRes.Global.LaserStatus.Maintenance:
                case StaticRes.Global.LaserStatus.Breakdown:
                    statusColor = StaticRes.MainStatusColor.Breakdown;
                    break;

                case StaticRes.Global.LaserStatus.Shutdown:
                    statusColor = StaticRes.MainStatusColor.Shutdown;
                    break;
                    
                default:
                    throw new NullReferenceException("no such status" + status);                   
            }

            return statusColor;
        }
    }
}