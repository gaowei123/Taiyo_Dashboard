using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taiyo.Tool.Extension;
using Taiyo.Enum.Production;

namespace DashboardTTS.UserControl
{
    public partial class UcMoulding : System.Web.UI.UserControl
    {

        public class UIModel
        {
            public string MachineID { get; set; }
            public MouldingStatus Status { get; set; }
            public System.Drawing.Color StatusColor { get; set; }
            public string ImgURL { get; set; }
            public string PartNo { get; set; }
            public string Model { get; set; }
            public string JigNo { get; set; }
            public decimal TotalQty { get; set; }
            public decimal OKQty { get; set; }
            public decimal NGQty { get; set; }
            public string RejRate { get; set; }
            public string UsedRate { get; set; }
        }

        public void SetUI(UIModel model)
        {
            this.lbMachineID.Text = model.MachineID;
            this.lbStatus.Text = model.Status.GetDescription();
            this.lbStatus.BackColor = GetStatusColor(model.Status);
            this.imgLogo.ImageUrl = model.ImgURL;


            this.lbPartNo.Text = model.PartNo;
            this.lbModel.Text = model.Model;
            this.lbJigNo.Text = model.JigNo;
            this.lbTotalQty.Text = model.TotalQty.ToString();
            this.lbOKQty.Text = model.OKQty.ToString();
            this.lbNGQty.Text = model.NGQty.ToString();
            this.lbRejRate.Text = model.RejRate;
            this.lbUsedRate.Text = model.UsedRate;
        }



        public void SetShutdown(string machineID, string imgURL)
        {
            this.lbMachineID.Text = machineID;
            this.lbStatus.Text = MouldingStatus.Shutdown.GetDescription();
            this.lbStatus.BackColor = GetStatusColor(MouldingStatus.Shutdown);
            this.imgLogo.ImageUrl = imgURL;            

            this.lbPartNo.Text = "";
            this.lbModel.Text = "";
            this.lbJigNo.Text = "";
            this.lbTotalQty.Text = "0";
            this.lbOKQty.Text = "0";
            this.lbNGQty.Text = "0";
            this.lbRejRate.Text = "0.00%";
            this.lbUsedRate.Text = "0.00%";
        }


        private System.Drawing.Color GetStatusColor(MouldingStatus status)
        {
            System.Drawing.Color statusColor = new System.Drawing.Color();



            switch (status)
            {
                case MouldingStatus.Running:
                    statusColor = StaticRes.MainStatusColor.Run;
                    break;
                case MouldingStatus.MaterialTesting:
                    statusColor = StaticRes.MainStatusColor.Run;
                    break;
                case MouldingStatus.MouldTesting:
                    statusColor = StaticRes.MainStatusColor.Run;
                    break;
                case MouldingStatus.Adjustment:
                    statusColor = StaticRes.MainStatusColor.Run;
                    break;
                case MouldingStatus.ChangeModel:
                    statusColor = StaticRes.MainStatusColor.Run;
                    break;
                case MouldingStatus.NoOperator:
                    statusColor = StaticRes.MainStatusColor.NoSchedule;
                    break;
                case MouldingStatus.LoginOut:
                    statusColor = StaticRes.MainStatusColor.NoSchedule;
                    break;
                case MouldingStatus.NoMaterial:
                    statusColor = StaticRes.MainStatusColor.NoSchedule;
                    break;
                case MouldingStatus.LoginLate:
                    statusColor = StaticRes.MainStatusColor.NoSchedule;
                    break;
                case MouldingStatus.NoSchedule:
                    statusColor = StaticRes.MainStatusColor.NoSchedule;
                    break;
                case MouldingStatus.BreakTime:
                    statusColor = StaticRes.MainStatusColor.NoSchedule;
                    break;
                case MouldingStatus.MachineBreak:
                    statusColor = StaticRes.MainStatusColor.Breakdown;
                    break;
                case MouldingStatus.DamageMould:
                    statusColor = StaticRes.MainStatusColor.Breakdown;
                    break;
                case MouldingStatus.Shutdown:
                    statusColor = StaticRes.MainStatusColor.Shutdown;
                    break;
                default:
                    throw new NullReferenceException("no such status" + status);
                    break;
            }

            return statusColor;
        }


     

    }
}