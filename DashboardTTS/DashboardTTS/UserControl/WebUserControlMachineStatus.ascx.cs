﻿using System;
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
        
        private System.Drawing.Color GetStatusColor(string status)
        {
            System.Drawing.Color statusColor = new System.Drawing.Color();
            switch (status)
            {
                case StaticRes.Global.LaserStatus.Run:
                case StaticRes.Global.LaserStatus.Setup:
                case StaticRes.Global.LaserStatus.Testing:
                case StaticRes.Global.LaserStatus.Buyoff:
                    statusColor = StaticRes.LaserStautsColor.Run;
                    break;

                case StaticRes.Global.LaserStatus.NoSchedule:
                    statusColor = StaticRes.LaserStautsColor.NoSchedule;
                    break;

                case StaticRes.Global.LaserStatus.Maintenance:
                case StaticRes.Global.LaserStatus.Breakdown:
                    statusColor = StaticRes.LaserStautsColor.Breakdown;
                    break;

                case StaticRes.Global.LaserStatus.Shutdown:
                    statusColor = StaticRes.LaserStautsColor.Shutdown;
                    break;
                    
                default:
                    throw new NullReferenceException("no such status" + status);                   
            }

            return statusColor;
        }
    }
}