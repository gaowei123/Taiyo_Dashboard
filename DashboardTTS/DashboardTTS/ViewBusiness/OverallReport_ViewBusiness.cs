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
        private readonly Common.Class.BLL.PQCInventory_BLL pqcInventoryBLL = new Common.Class.BLL.PQCInventory_BLL();


        private readonly Common.Class.BLL.LMMSInventoty_BLL laserInventoryBLL = new Common.Class.BLL.LMMSInventoty_BLL();



        


        #region all section inventory report 
        public string GetAllSectionList(DateTime dStartTime)
        {

            //pqc bom info.
            List<ViewModel.AllSectionInventory.pqcBomInfo> bomList = GetPQCBomInfo();
            if (bomList == null) return "";



            //主表信息
            List<ViewModel.AllSectionInventory.mainMaterialList> mainList = GetMainMaterialList(dStartTime);
            if (mainList == null)
                return null;


            //before laser  
            List<ViewModel.AllSectionInventory.laserInventoryInfo> laserInventory = GetLaserInventory(dStartTime);


            //after laser 
            List<ViewModel.AllSectionInventory.laserOutputInfo> laserOutputList = GetLaserOutput(dStartTime);


            //before wip
            List<ViewModel.AllSectionInventory.wipInventoryInfo> wipInventoryList = GetWIPInventory(dStartTime);


            //after wip
            List<ViewModel.AllSectionInventory.wipOutputInfo> wipOutputList = GetWIPOutput(dStartTime);








            JavaScriptSerializer js = new JavaScriptSerializer();

            return js.Serialize("");
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
            if (dt == null || dt.Rows.Count == 0)
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


        //从painting delivery中获取list, 生成material list 作为主表.
        private List<ViewModel.AllSectionInventory.mainMaterialList> GetMainMaterialList(DateTime dStartTime)
        {

            DataTable dt = paintDeliveryBLL.GetList(dStartTime, DateTime.Now.AddDays(1), "");
            if (dt == null || dt.Rows.Count == 0)
                return null;




            //pqc bom list, 用于生成不同material name的主表信息. 
            List<ViewModel.AllSectionInventory.pqcBomInfo> bomList = GetPQCBomInfo();






            List<ViewModel.AllSectionInventory.mainMaterialList> modelList = new List<ViewModel.AllSectionInventory.mainMaterialList>();





            //排除laser delete掉的job
            DataTable dtPaintDelivery = dt.Select(" status is  null").CopyToDataTable();


            foreach (DataRow dr in dtPaintDelivery.Rows)
            {
                string tempPartNo = dr["partNumber"].ToString();
                string tempModel = dr["model"].ToString();

                var partMaterialList = from a in bomList
                                       where a.partNo == tempPartNo
                                       group a by a.materialName into c
                                       select new
                                       {
                                           materialName = c.Key
                                       };
                if (partMaterialList == null || partMaterialList.Count() == 0)
                {
                    DBHelp.Reports.LogFile.Log("AllSectionInventory", "OverallReport_ViewBusiness, GetAllMaterialList, can not find part no from pqc bom [" + tempPartNo + "]");
                    continue;
                }



                foreach (var material in partMaterialList)
                {
                    ViewModel.AllSectionInventory.mainMaterialList model = new ViewModel.AllSectionInventory.mainMaterialList();

                    model.model = tempModel;
                    model.partNo = tempPartNo;
                    model.materialName = material.materialName;
                    modelList.Add(model);

                    if (!modelList.Contains(model))
                        modelList.Add(model);
                }
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


        //after wip
        private List<ViewModel.AllSectionInventory.wipOutputInfo> GetWIPOutput(DateTime dStartTime)
        {

            DataTable dt = viTrackingBLL.GetWIPOutputForAllInventoryReport(dStartTime);
            if (dt == null || dt.Rows.Count == 0)
                return null;
            

            List<ViewModel.AllSectionInventory.wipOutputInfo> modelList = new List<ViewModel.AllSectionInventory.wipOutputInfo>();
            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.AllSectionInventory.wipOutputInfo model = new ViewModel.AllSectionInventory.wipOutputInfo();
            
                model.partNo = dr["partnumber"].ToString();
                model.materialName = dr["materialName"].ToString();
                model.passQty = double.Parse(dr["passQty"].ToString());
                model.rejectQty = double.Parse(dr["rejectQty"].ToString());
                
                modelList.Add(model);
            }


            return modelList;
        }
                

        //before wip
        private List<ViewModel.AllSectionInventory.wipInventoryInfo> GetWIPInventory(DateTime dStartTime)
        {
            DataTable dt = pqcInventoryBLL.GetInventoryForAllSectionReport(dStartTime);
            if (dt == null || dt.Rows.Count == 0)
                return null;



            List<ViewModel.AllSectionInventory.wipInventoryInfo> modelList = new List<ViewModel.AllSectionInventory.wipInventoryInfo>();
            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.AllSectionInventory.wipInventoryInfo model = new ViewModel.AllSectionInventory.wipInventoryInfo();
                
                model.partNo = dr["PartNumber"].ToString();
                model.materialName = dr["materialName"].ToString();
                model.inventoryQty = double.Parse(dr["inventoryQty"].ToString());
            
                modelList.Add(model);
            }

            return modelList;
        }


       



     


        #endregion




    }
}