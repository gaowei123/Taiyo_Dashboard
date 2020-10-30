using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DashboardTTS.UserControl
{
    public partial class UcMoulding : System.Web.UI.UserControl
    {

        public class UIModel
        {
            public string MachineID { get; set; }
            public string Status { get; set; }
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
            this.lbStatus.Text = model.Status;
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


        private System.Drawing.Color GetStatusColor(string status)
        {
            System.Drawing.Color statusColor = new System.Drawing.Color();
            switch (status)
            {
                case StaticRes.Global.MouldingStatus.Run:
                case StaticRes.Global.MouldingStatus.Material_Testing:
                case StaticRes.Global.MouldingStatus.Mould_Testing:
                case StaticRes.Global.MouldingStatus.Adjustment:
                case StaticRes.Global.MouldingStatus.Change_Model:
                    statusColor = StaticRes.MainStatusColor.Run;
                    break;

                case StaticRes.Global.MouldingStatus.No_Operator:
                case StaticRes.Global.MouldingStatus.Login_Out:
                case StaticRes.Global.MouldingStatus.No_Material:
                case StaticRes.Global.MouldingStatus.Login_Late:
                case StaticRes.Global.MouldingStatus.No_Schedule:
                case StaticRes.Global.MouldingStatus.Break_Time:
                    statusColor = StaticRes.MainStatusColor.NoSchedule;
                    break;

                case StaticRes.Global.MouldingStatus.MachineBreak:
                case StaticRes.Global.MouldingStatus.DamageMould:
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



        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}