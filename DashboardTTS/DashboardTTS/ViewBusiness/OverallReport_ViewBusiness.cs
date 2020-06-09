using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;

namespace DashboardTTS.ViewBusiness
{

    

    public class OverallReport_ViewBusiness
    {

        private readonly Common.Class.BLL.PaintingDeliveryHis_BLL paintDeliveryBLL = new Common.Class.BLL.PaintingDeliveryHis_BLL();
        private readonly Common.BLL.LMMSWatchLog_BLL watchLogBLL = new Common.BLL.LMMSWatchLog_BLL();
        private readonly Common.Class.BLL.PQCQaViDetailTracking_BLL detialTrackingBLL = new Common.Class.BLL.PQCQaViDetailTracking_BLL();
        private readonly Common.Class.BLL.PQCQaViBinning pqcBinBLL = new Common.Class.BLL.PQCQaViBinning();
        private readonly Common.Class.BLL.PQCBom_BLL pqcBomBLL = new Common.Class.BLL.PQCBom_BLL();





        #region all section inventory report 
        public string GetAllSectionList(DateTime dateStart)
        {
            
          
            JavaScriptSerializer js = new JavaScriptSerializer();

            return js.Serialize("");
        }




        private List<ViewModel.AllSectionInventory.paintingInfo> GetPaintingOutput()
        {
            //laser inventory 从'2018-8-14'开始, 统一时间.
            DataTable dt = paintDeliveryBLL.GetList(DateTime.Parse("2018-9-1"), DateTime.Now.AddDays(1), "");
            if (dt == null || dt.Rows.Count == 0)
                return null;

            //排除laser delete掉的job
            DataTable dtPaintDelivery = dt.Select(" status is  null").CopyToDataTable();


            DataTable dtPQCBom = pqcBomBLL.GetListWithDetail("");


            List<ViewModel.AllSectionInventory.paintingInfo> modelList = new List<ViewModel.AllSectionInventory.paintingInfo>();

            foreach (DataRow dr in dtPaintDelivery.Rows)
            {
                //按material 细分.
                DataRow[] arrPart = dtPQCBom.Select(" partNumber = '" + dr["partNumber"].ToString() + "'");
                if (arrPart !=null && arrPart.Length!=0)
                {
                    foreach (DataRow drPart in arrPart)
                    {
                        ViewModel.AllSectionInventory.paintingInfo model = new ViewModel.AllSectionInventory.paintingInfo();

                        model.jobNo = dr["jobNumber"].ToString();
                        model.partNo = dr["partNumber"].ToString();
                        model.output = double.Parse(dr["inQuantity"].ToString());
                        model.sendingTo = dr["sendingTo"].ToString();


                        model.model = drPart["model"].ToString();
                        model.materialNo = drPart["materialPartNo"].ToString();

                        modelList.Add(model);
                    }
                }
            }

            

            return modelList;
        }
        
        private List<ViewModel.AllSectionInventory.laserInfo> GetLaserOutput()
        {
           
            DataTable dt = watchLogBLL.GetMaterialListForAllSectionReport( "");
            if (dt == null || dt.Rows.Count == 0 )
                return null;


            List<ViewModel.AllSectionInventory.laserInfo> modelList = new List<ViewModel.AllSectionInventory.laserInfo>();

            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.AllSectionInventory.laserInfo model = new ViewModel.AllSectionInventory.laserInfo();
                
                model.materialNo = dr["materialNo"].ToString();


                if (dr["outputQty"].ToString() == "")
                {
                    model.outputQty = 0;
                }else
                {
                    model.outputQty = double.Parse(dr["outputQty"].ToString());
                }
                
                           
                modelList.Add(model);
            }




            return modelList;
        }
        
        private List<ViewModel.AllSectionInventory.pqcCheckInfo> GetCheckInfo(DateTime dDateStart)
        {
            List<Common.Class.Model.PQCQaViDetailTracking_Model> detialList = detialTrackingBLL.GetModelList("", "", dDateStart, DateTime.Now.AddDays(1));
            if (detialList == null || detialList.Count == 0)
                return null;



            List<ViewModel.AllSectionInventory.pqcCheckInfo> pqcCheckList = new List<ViewModel.AllSectionInventory.pqcCheckInfo>();

            foreach (Common.Class.Model.PQCQaViDetailTracking_Model detailModel in detialList)
            {
                ViewModel.AllSectionInventory.pqcCheckInfo checkInfoModel = new ViewModel.AllSectionInventory.pqcCheckInfo();

                checkInfoModel.jobNo = detailModel.jobid;
                checkInfoModel.materialNo = detailModel.materialPartNo;
                checkInfoModel.outputQty = double.Parse((detailModel.passQty.Value + detailModel.rejectQty.Value).ToString());
                checkInfoModel.process = detailModel.processes;
                
                pqcCheckList.Add(checkInfoModel);
            }



            return pqcCheckList;
        }
        
        private List<ViewModel.AllSectionInventory.pqcBinInfo> GetPQCBinInfo(DateTime dDateStart)
        {
            DataTable dt = pqcBinBLL.GetList(dDateStart, DateTime.Now.AddDays(1));
            if (dt == null || dt.Rows.Count == 0)
                return null;


            DataTable dtPQCBom = pqcBomBLL.GetList("");



            List<ViewModel.AllSectionInventory.pqcBinInfo> binList = new List<ViewModel.AllSectionInventory.pqcBinInfo>();

            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.AllSectionInventory.pqcBinInfo binModel = new ViewModel.AllSectionInventory.pqcBinInfo();

                binModel.jobNo = dr["jobId"].ToString();
                binModel.materialNo = dr["materialPartNo"].ToString();
                binModel.process = dr["processes"].ToString();
                binModel.binQty = double.Parse(dr["materialQty"].ToString());
                binModel.shipTo = dr["shipTo"].ToString();


              
                DataRow[] drArr = dtPQCBom.Select(" partNumber = '" + dr["PartNumber"].ToString() + "'");
                if (drArr.Length !=0 )
                {
                    binModel.allProcess = drArr[0]["processes"].ToString();
                }
              

                binList.Add(binModel);
            }



            return binList;
        }


        #endregion




    }
}