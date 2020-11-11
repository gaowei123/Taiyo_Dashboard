using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Taiyo.Enum.Production;
using Taiyo.Tool;

namespace DashboardTTS.Webform.PQC
{
    public partial class PQCRealTime : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Common.Class.BLL.PQCQaViTracking_BLL pqcbll = new Common.Class.BLL.PQCQaViTracking_BLL();
            DataTable dtTracking = pqcbll.GetRealTime();

            Common.Class.BLL.PaintingDeliveryHis_BLL paintbll = new Common.Class.BLL.PaintingDeliveryHis_BLL();
            List<Common.Class.Model.PaintingDeliveryHis_Model> paintList = paintbll.GetModels("", DateTime.Now.AddYears(-1), DateTime.Now);

         


            for (int i = 1; i < 25; i++)
            {
                UserControl.WebUserControlPQCStatus.UIModel model = new UserControl.WebUserControlPQCStatus.UIModel();
                model.Station = "Station " + i.ToString();

                DataRow[] drArrTemp = dtTracking.Select(" machineID = '" + i + "'", " datetime desc");
                if (drArrTemp == null || drArrTemp.Length == 0)
                {
                    model.Status = PQCStatus.Shutdown;
                    model.LotNo = "";
                    model.JobNo = "";
                    model.PartNo = "";
                    model.LotQty = 0;
                    model.OK = 0;
                    model.NG = 0;
                    model.RejRate = 0;
                    model.Operator = "";
                }
                else
                {
                    DataRow dr = drArrTemp[0];

                    if (dr["stopTime"].ToString() == "")
                    {
                        model.Status = StatusConventor.ConventnPQC(dr["status"].ToString());
                        var paint = (from a in paintList
                                     where a.paintProcess == "Paint#1" && a.jobNumber == dr["jobId"].ToString()
                                     select a).FirstOrDefault();


                        model.LotNo = paint.lotNo;
                        model.JobNo = dr["jobId"].ToString();
                        model.PartNo = dr["partNumber"].ToString();
                        model.LotQty = (double)paint.inQuantity;
                        model.OK = double.Parse(dr["acceptQty"].ToString());
                        model.NG = double.Parse(dr["rejectQty"].ToString());
                        model.RejRate = Math.Round(model.NG / model.LotQty * 100, 2);
                        model.Operator = dr["userID"].ToString();
                    }
                    else
                    {
                        model.Status = PQCStatus.NoSchedule;
                        model.LotNo = "";
                        model.JobNo = "";
                        model.PartNo = "";
                        model.LotQty = 0;
                        model.OK = 0;
                        model.NG = 0;
                        model.RejRate = 0;
                        model.Operator = "";
                    }
                }

                GetControl(i).SetUI(model);
            }

        }

       

        UserControl.WebUserControlPQCStatus GetControl(int ID)
        {
            UserControl.WebUserControlPQCStatus control = new UserControl.WebUserControlPQCStatus();

            switch (ID)
            {
                case 1:
                    control = this.ucPQC1;
                    break;
                case 2:
                    control = this.ucPQC2;
                    break;
                case 3:
                    control = this.ucPQC3;
                    break;
                case 4:
                    control = this.ucPQC4;
                    break;
                case 5:
                    control = this.ucPQC5;
                    break;
                case 6:
                    control = this.ucPQC6;
                    break;
                case 7:
                    control = this.ucPQC7;
                    break;
                case 8:
                    control = this.ucPQC8;
                    break;
                case 9:
                    control = this.ucPQC9;
                    break;
                case 10:
                    control = this.ucPQC10;
                    break;
                case 11:
                    control = this.ucPQC11;
                    break;
                case 12:
                    control = this.ucPQC12;
                    break;
                case 13:
                    control = this.ucPQC13;
                    break;
                case 14:
                    control = this.ucPQC14;
                    break;
                case 15:
                    control = this.ucPQC15;
                    break;
                case 16:
                    control = this.ucPQC16;
                    break;
                case 17:
                    control = this.ucPQC17;
                    break;
                case 18:
                    control = this.ucPQC18;
                    break;
                case 19:
                    control = this.ucPQC19;
                    break;
                case 20:
                    control = this.ucPQC20;
                    break;
                case 21:
                    control = this.ucPQC21;
                    break;
                case 22:
                    control = this.ucPQC22;
                    break;
                case 23:
                    control = this.ucPQC23;
                    break;
                case 24:
                    control = this.ucPQC24;
                    break;
             

                default:
                    break;
            }

            return control;

        }
           
    }
}