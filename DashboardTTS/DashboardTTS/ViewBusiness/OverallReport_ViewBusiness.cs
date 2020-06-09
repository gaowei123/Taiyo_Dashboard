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
        private readonly Common.Class.BLL.PQCQaViTracking_BLL viTrackingBLL = new Common.Class.BLL.PQCQaViTracking_BLL();


        private readonly Common.Class.BLL.LMMSInventoty_BLL laserInventoryBLL = new Common.Class.BLL.LMMSInventoty_BLL();



        


        #region all section inventory report 
        public string GetAllSectionList(DateTime dStartTime)
        {

            //pqc bom info.
            List<ViewModel.AllSectionInventory.pqcBomInfo> bomList = GetPQCBomInfo();
            if (bomList == null) return "";




            //laser before 
            //painting delivery  not complete.
            

            //laser after

            

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














        private List<ViewModel.AllSectionInventory.pqcBomInfo> GetPQCBomInfo()
        {
            DataTable dt = pqcBomBLL.GetListWithDetail("");
            if (dt == null || dt.Rows.Count ==0)
                return null;


            List<ViewModel.AllSectionInventory.pqcBomInfo> modelList = new List<ViewModel.AllSectionInventory.pqcBomInfo>();

            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.AllSectionInventory.pqcBomInfo model = new ViewModel.AllSectionInventory.pqcBomInfo();
                model.partNo = dr["partNumber"].ToString();
                model.materialName = dr["materialName"].ToString();
                model.materialNo = dr["materialPartNo"].ToString();
                model.model = dr["model"].ToString();
                model.processes = dr["processes"].ToString();
                model.shipTo = dr["shipTo"].ToString();

                modelList.Add(model);
            }


            return modelList;
        }
        

        //after laser
        private List<ViewModel.AllSectionInventory.laserOutputInfo> GetLaserOutput(DateTime dStartTime)
        {

            DataTable dt = watchLogBLL.GetMaterialListForAllSectionReport(dStartTime);
            if (dt == null || dt.Rows.Count == 0)
                return null;


            List<ViewModel.AllSectionInventory.laserOutputInfo> modelList = new List<ViewModel.AllSectionInventory.laserOutputInfo>();

            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.AllSectionInventory.laserOutputInfo model = new ViewModel.AllSectionInventory.laserOutputInfo();

                model.jobNo = dr["jobNumber"].ToString();
                model.partNo = dr["partNumber"].ToString();
                model.materialNo = dr["materialNo"].ToString();
                model.okQty = double.Parse(dr["okQty"].ToString());
                model.ngQty = double.Parse(dr["ngQty"].ToString());
                model.jobStatus = dr["jobStatus"].ToString();

                modelList.Add(model);
            }




            return modelList;
        }


        //before laser
        private List<ViewModel.AllSectionInventory.laserInventoryInfo> GetLaserInventory(DateTime dStartTime)
        {

            DataTable dt = laserInventoryBLL.GetInventoryInfoForAllInventoryReport(dStartTime);
            if (dt == null || dt.Rows.Count == 0)
                return null;
            
            List<ViewModel.AllSectionInventory.laserInventoryInfo> modelList = new List<ViewModel.AllSectionInventory.laserInventoryInfo>();
            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.AllSectionInventory.laserInventoryInfo model = new ViewModel.AllSectionInventory.laserInventoryInfo();
                model.jobNo = dr["jobNumber"].ToString();
                model.partNo = dr["partNumber"].ToString();
                model.materialNo = dr["materialPartNo"].ToString();
                model.qty = double.Parse(dr["quantity"].ToString());
                
                modelList.Add(model);                
            }


            return modelList;
        }


        private List<ViewModel.AllSectionInventory.pqcOutputInfo> GetPQCOutput(DateTime dStartTime)
        {

            DataTable dt = viTrackingBLL.GetOutputForAllInventoryReport(dStartTime);
            if (dt == null || dt.Rows.Count == 0)
                return null;
            

            List<ViewModel.AllSectionInventory.pqcOutputInfo> modelList = new List<ViewModel.AllSectionInventory.pqcOutputInfo>();
            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.AllSectionInventory.pqcOutputInfo model = new ViewModel.AllSectionInventory.pqcOutputInfo();
                model.jobNo = dr["jobid"].ToString();
                model.partNo = dr["partnumber"].ToString();
                model.checkProcess = dr["checkProcess"].ToString();
                model.nextViFlag = bool.Parse(dr["nextViFlag"].ToString());
                model.materialNo = dr["materialPartNo"].ToString();
                model.materialName = dr["materialName"].ToString();
                model.passQty = double.Parse(dr["passQty"].ToString());
                model.rejectQty = double.Parse(dr["rejectQty"].ToString());
                model.allProcess = dr["allProcess"].ToString();

             
                modelList.Add(model);
            }


            return modelList;
        }
        




        #endregion




    }
}