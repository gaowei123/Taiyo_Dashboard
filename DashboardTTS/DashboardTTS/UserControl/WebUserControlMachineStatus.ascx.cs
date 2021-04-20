using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Taiyo.Enum.Production;
using Taiyo.Tool.Extension;



namespace DashboardTTS.UserControl
{
    public partial class WebUserControlMachineStatus : System.Web.UI.UserControl
    {
        public class UIModel
        {
            public string MachineID { get; set; }
            public LaserStatus Status { get; set; }
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

            public string UsedRateDescription { get; set; }
        }
        
        public WebUserControlMachineStatus()
        {

        }




        public void SetUI(UIModel model)
        {
            this.lbMachineID.Text = model.MachineID;
            this.lbPartNo.Text = model.PartNo;
            this.lbStatus.Text = model.Status.GetDescription();
            this.lbStatus.BackColor = GetStatusColor(model.Status);
            this.lbLotNo.Text = model.LotNo;


            this.imgLogo.ImageUrl = model.ImgURL;
            this.lbJobNo.Text = model.JobNo;
            this.lbLotQty.Text = model.LotQty.ToString();
            this.lbOKQty.Text = model.OKQty.ToString();
            this.lbNGQty.Text = model.NGQty.ToString();
            this.lbRejRate.Text = model.RejRate;
            this.lbUsedRate.Text = model.UsedRate;
            this.lbUsedRate.ToolTip = model.UsedRateDescription;
        }


        public void SetShutdown(string machineID, string imgURL)
        {
            this.lbMachineID.Text = machineID;
          
            this.lbStatus.Text = LaserStatus.Shutdown.GetDescription();
            this.lbStatus.BackColor = GetStatusColor(LaserStatus.Shutdown);
            this.imgLogo.ImageUrl = imgURL;
            

            this.lbLotNo.Text = "";
            this.lbPartNo.Text = "";
            this.lbJobNo.Text = "";
            this.lbLotQty.Text = "0";
            this.lbOKQty.Text = "0";
            this.lbNGQty.Text = "0";
            this.lbRejRate.Text = "0.00%";
            this.lbUsedRate.Text = "0.00%";
            this.lbUsedRate.ToolTip = "";
        }

        
        private System.Drawing.Color GetStatusColor(LaserStatus status)
        {
            System.Drawing.Color statusColor = new System.Drawing.Color();
            switch (status)
            {
                case LaserStatus.Running:
                    statusColor = StaticRes.MainStatusColor.Run;
                    break;
                case LaserStatus.Buyoff:
                    statusColor = StaticRes.MainStatusColor.Run;
                    break;
                case LaserStatus.Setup:
                    statusColor = StaticRes.MainStatusColor.Run;
                    break;
                case LaserStatus.Testing:
                    statusColor = StaticRes.MainStatusColor.Run;
                    break;
                case LaserStatus.NoSchedule:
                    statusColor = StaticRes.MainStatusColor.NoSchedule;
                    break;
                case LaserStatus.Maintenance:
                    statusColor = StaticRes.MainStatusColor.NoSchedule;
                    break;
                case LaserStatus.Breakdown:
                    statusColor = StaticRes.MainStatusColor.Breakdown;
                    break;
                case LaserStatus.Shutdown:
                    statusColor = StaticRes.MainStatusColor.Shutdown;
                    break;
                default:
                    break;
            }
          
            return statusColor;
        }
    }
}