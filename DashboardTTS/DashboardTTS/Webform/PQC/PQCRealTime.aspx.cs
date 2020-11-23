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


            #region online
            for (int i = 1; i < 9; i++)
            {
                string station = $"Online{i.ToString()} (Sta.{i.ToString()})";


                DataRow[] drArrTemp = dtTracking.Select(" machineID = '" + i + "'", " starttime desc");
                if (drArrTemp == null || drArrTemp.Length == 0)
                {
                    GetControl(i).SetEmpty(station, PQCStatus.Shutdown);
                }
                else
                {
                    DataRow dr = drArrTemp[0];

                    if (dr["stopTime"].ToString() == "")
                    {
                        var model = new UserControl.WebUserControlPQCStatus.UIModel();
                        model.Station = station;
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

                        GetControl(i).SetUI(model);
                    }
                    else
                    {
                        GetControl(i).SetEmpty(station, PQCStatus.NoSchedule);
                    }
                }
            }
            #endregion


           
            #region wip

            int count = 1;
            int[] wipIDList = new int[] { 16, 17, 14, 15, 11, 13 };
            foreach (var wipID in wipIDList)
            {
                string station = $"WIP{count.ToString()} (Sta.{wipID.ToString()})";
                count++;


                DataRow[] drArrTemp = dtTracking.Select(" machineID = '" + wipID + "'", " starttime desc");
                if (drArrTemp == null || drArrTemp.Length == 0)
                {
                    GetControl(wipID).SetEmpty(station, PQCStatus.Shutdown);
                }
                else
                {
                    DataRow dr = drArrTemp[0];

                    if (dr["stopTime"].ToString() == "")
                    {
                        var model = new UserControl.WebUserControlPQCStatus.UIModel();
                        model.Station = station;
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

                        GetControl(wipID).SetUI(model);
                    }
                    else
                    {
                        GetControl(wipID).SetEmpty(station, PQCStatus.NoSchedule);
                    }
                }

                
            }




            #endregion


            #region packing

            count = 1;
            int[] packingIDList = new int[] { 25, 23, 22, 21, 24, 12 };
            foreach (var packingID in packingIDList)
            {
                string station = $"Packing{count.ToString()} (Sta.{packingID.ToString()})";
                count++;



                DataRow[] drArrTemp = dtTracking.Select(" machineID = '" + packingID + "'", " starttime desc");
                if (drArrTemp == null || drArrTemp.Length == 0)
                {
                    GetControl(packingID).SetEmpty(station, PQCStatus.Shutdown);
                }
                else
                {
                    DataRow dr = drArrTemp[0];

                    if (dr["stopTime"].ToString() == "")
                    {
                        var model = new UserControl.WebUserControlPQCStatus.UIModel();
                        model.Station = station;
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

                        GetControl(packingID).SetUI(model);
                    }
                    else
                    {
                        GetControl(packingID).SetEmpty(station, PQCStatus.NoSchedule);
                    }
                }

                
            }


            #endregion


        }


        UserControl.WebUserControlPQCStatus GetControl(int ID)
        {
            UserControl.WebUserControlPQCStatus control = new UserControl.WebUserControlPQCStatus();


            switch (ID)
            {
                //online
                case 1:
                    control = this.ucOnline1;
                    break;
                case 2:
                    control = this.ucOnline2;
                    break;
                case 3:
                    control = this.ucOnline3;
                    break;
                case 4:
                    control = this.ucOnline4;
                    break;
                case 5:
                    control = this.ucOnline5;
                    break;
                case 6:
                    control = this.ucOnline6;
                    break;
                case 7:
                    control = this.ucOnline7;
                    break;
                case 8:
                    control = this.ucOnline8;
                    break;
                    
                    //wip
                case 11:
                    control = this.ucWIP5;
                    break;
                case 13:
                    control = this.ucWIP6;
                    break;
                case 14:
                    control = this.ucWIP3;
                    break;
                case 15:
                    control = this.ucWIP4;
                    break;
                case 16:
                    control = this.ucWIP1;
                    break;
                case 17:
                    control = this.ucWIP2;
                    break;
                          
                    //packing              
                case 12:
                    control = this.ucPacking6;
                    break;
                case 21:
                    control = this.ucPacking4;
                    break;
                case 22:
                    control = this.ucPacking3;
                    break;
                case 23:
                    control = this.ucPacking2;
                    break;
                case 24:
                    control = this.ucPacking5;
                    break;
                case 25:
                    control = this.ucPacking1;
                    break;

                default:
                    break;
            }

            return control;
        }


      
           
    }
}