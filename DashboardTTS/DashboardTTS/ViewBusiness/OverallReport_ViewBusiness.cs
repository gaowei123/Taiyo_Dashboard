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
        private readonly Common.Class.BLL.PaintingTempInfo paintTempBLL = new Common.Class.BLL.PaintingTempInfo();






        #region all section inventory report 
        public string GetAllSectionResult(DateTime dStartTime, string sPartNo, string sShipTo)
        {
            try
            {


                //pqc bom info.
                List<ViewModel.AllSectionInventory.pqcBomInfo> bomList = GetPQCBomInfo();
                if (bomList == null) return "";



                //主表信息,   model, part no, material name
                List<ViewModel.AllSectionInventory.mainMaterialList> mainList = GetMainMaterialList(dStartTime);
                if (mainList == null) return null;



                //before laser  
                //详细到material no一条的记录. 需要join pqcbom在 group by material name
                List<ViewModel.AllSectionInventory.laserInventoryInfo> laserInventory = GetLaserInventory(dStartTime);


                //after laser 
                //详细到material no一条的记录, 需要join pqcbom在 group by material name, 并排除 jobStatus 是done的. 
                List<ViewModel.AllSectionInventory.laserOutputInfo> laserOutputList = GetLaserOutput(dStartTime);


                //before wip,  详细到material name, 直接join
                List<ViewModel.AllSectionInventory.wipInventoryInfo> wipInventoryList = GetWIPInventory(dStartTime);


                //after wip, 详细到material name, 直接join
                List<ViewModel.AllSectionInventory.wipOutputInfo> wipOutputList = GetWIPOutput(dStartTime);



                //pqc bin, 详细到material name, 通过process 区分出 check complete qty (before pack),   没pack完的(after pack)
                List<ViewModel.AllSectionInventory.pqcBinInfo> pqcBinInfo = GetPackInventory(dStartTime);
                var beforePacking = from a in pqcBinInfo where a.processes == "CHECK#1" || a.processes == "CHECK#2" select a;
                var afterPacking = from a in pqcBinInfo where a.processes == "PACKING" select a;


                //FG, Assembly list, 通过material qty/outerboxqty, 获取能被整除的数量作为fg,assembly的数量.
                var fgAssemblyList = GetFgAssembly();




                //join 
                List<ViewModel.AllSectionInventory.report> reportList = new List<ViewModel.AllSectionInventory.report>();
                foreach (var material in mainList)
                {


                    //获取该part的 bom detail list.   (material no一条)
                    var tempBomList = (from a in bomList where a.partNo == material.partNo && a.materialName == material.materialName select a).ToList();
                    if (tempBomList == null || tempBomList.Count == 0)
                    {
                        DBHelp.Reports.LogFile.Log("AllSectionInventory", "OverallReport_ViewBusiness, GetAllSectionList, can not find pqc bom, part no:[" + material.partNo + "], material name :[" + material.materialName + "]");
                        continue;
                    }





                    #region before laser
                    //laser join pqcbom,  在按照pqcbom的materialname group by.
                    var tempLaserDetailList = from a in laserInventory
                                              where a.partNo == material.partNo
                                              join b in tempBomList on new { a.partNo, a.materialNo } equals new { b.partNo, b.materialNo }
                                              select new
                                              {
                                                  a,
                                                  b
                                              };

                    //group by material name
                    var beforeLaserModel = (from a in tempLaserDetailList
                                            where a.b.materialName == material.materialName
                                            group a by a.b.materialName into summary
                                            select new
                                            {
                                                summary.Key,
                                                qty = summary.Max(p => p.a.qty)
                                            }).FirstOrDefault();
                    #endregion







                    #region after laser
                    //laser join pqcbom, 在按照pqcbom的materialname group by.
                    var tempLaserOutputDetialList = from a in laserOutputList
                                                    where a.partNo == material.partNo
                                                    join b in tempBomList on new { a.partNo, a.materialNo } equals new { b.partNo, b.materialNo }
                                                    select new
                                                    {
                                                        a,
                                                        b
                                                    };


                    //group by material name
                    var afterLaserModel = (from a in tempLaserOutputDetialList
                                           where a.b.materialName == material.materialName
                                           group a by a.b.materialName into summary
                                           select new
                                           {
                                               summary.Key,
                                               qty = summary.Max(p => p.a.okQty + p.a.ngQty)
                                           }).FirstOrDefault();
                    #endregion






                    //before wip
                    var beforeWIPModel = (from a in wipInventoryList
                                          where a.partNo == material.partNo
                                           && a.materialName == material.materialName
                                          select a).FirstOrDefault();




                    //after wip
                    var afterWIPModel = (from a in wipOutputList
                                         where a.partNo == material.partNo
                                         && a.materialName == material.materialName
                                         select a).FirstOrDefault();




                    //beforepacking
                    var beforePackingModel = (from a in beforePacking
                                              where a.partNo == material.partNo
                                              && a.materialName == material.materialName
                                              select a).FirstOrDefault();




                    //afterpacking
                    var afterPackingModel = (from a in afterPacking
                                             where a.partNo == material.partNo
                                             && a.materialName == material.materialName
                                             join b in tempBomList on new { a.partNo, a.materialName } equals new { b.partNo, b.materialName }
                                             select new {
                                                 a.materialName,
                                                 qty = a.qty % b.outerBoxQty
                                             }).FirstOrDefault();



                    
                    var fgAssemblyModel = (from a in fgAssemblyList
                                           where a.partNo == material.partNo
                                           && a.materialName == material.materialName
                                           select a).FirstOrDefault();





                    ViewModel.AllSectionInventory.report reportModel = new ViewModel.AllSectionInventory.report();
                    reportModel.model = material.model;
                    reportModel.partNo = material.partNo;
                    reportModel.materialName = material.materialName;
                    reportModel.shipTo = material.shipTo;

                    reportModel.rawPart = null;
                    reportModel.ucPaint = null;
                    reportModel.mcPaint = null;
                    reportModel.print = null;
                    reportModel.tcPaint = null;

                    if (beforeLaserModel != null) reportModel.beforeLaser = beforeLaserModel.qty;
                    if (afterLaserModel != null) reportModel.afterLaser = afterLaserModel.qty;
                    if (beforeWIPModel != null) reportModel.beforeWIP = beforeWIPModel.inventoryQty;
                    if (afterWIPModel != null) reportModel.afterWIP = afterWIPModel.passQty + afterWIPModel.rejectQty;
                    if (beforePackingModel != null) reportModel.beforePack = beforePackingModel.qty;
                    if (afterPackingModel != null) reportModel.afterPack = afterPackingModel.qty;


                     if (fgAssemblyModel != null)
                    {
                        reportModel.fg = fgAssemblyModel.fg;
                        reportModel.assembly = fgAssemblyModel.assembly;
                    }

  



                    reportList.Add(reportModel);
                }


                JavaScriptSerializer js = new JavaScriptSerializer();

                var orderbyList = from a in reportList
                                  where a.afterLaser != null
                                  || a.beforeLaser != null
                                  || a.afterWIP != null
                                  || a.beforeWIP != null
                                  || a.afterPack != null
                                  || a.beforePack != null
                                  orderby a.model ascending
                                  select a;


                if (sPartNo == "" && sShipTo == "")
                {
                    return js.Serialize(orderbyList);
                }
                else if (sPartNo != "" && sShipTo == "")
                {
                    var result = from a in orderbyList where a.partNo == sPartNo select a;
                    return js.Serialize(result);
                }
                else if (sPartNo == "" && sShipTo != "")
                {
                    var result = from a in orderbyList where a.shipTo == sShipTo select a;
                    return js.Serialize(result);
                }
                else
                {
                    var result = from a in orderbyList where a.shipTo == sShipTo && a.model == sPartNo select a;
                    return js.Serialize(result);
                }

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("AllSectionInventory", "OverallReport_ViewBusiness, Catch Exception: " + ee.ToString());
                return "";
            }
        }



        public List<ViewModel.AllSectionInventory.report> GetAllSectionList(DateTime dStartTime, string sPartNo, string sShipTo)
        {
            try
            {


                //pqc bom info.
                List<ViewModel.AllSectionInventory.pqcBomInfo> bomList = GetPQCBomInfo();
                if (bomList == null) return null;



                //主表信息,   model, part no, material name
                List<ViewModel.AllSectionInventory.mainMaterialList> mainList = GetMainMaterialList(dStartTime);
                if (mainList == null) return null;



                //before laser  
                //详细到material no一条的记录. 需要join pqcbom在 group by material name
                List<ViewModel.AllSectionInventory.laserInventoryInfo> laserInventory = GetLaserInventory(dStartTime);


                //after laser 
                //详细到material no一条的记录, 需要join pqcbom在 group by material name, 并排除 jobStatus 是done的. 
                List<ViewModel.AllSectionInventory.laserOutputInfo> laserOutputList = GetLaserOutput(dStartTime);


                //before wip,  详细到material name, 直接join
                List<ViewModel.AllSectionInventory.wipInventoryInfo> wipInventoryList = GetWIPInventory(dStartTime);


                //after wip, 详细到material name, 直接join
                List<ViewModel.AllSectionInventory.wipOutputInfo> wipOutputList = GetWIPOutput(dStartTime);



                //pqc bin, 详细到material name, 通过process 区分出 check complete qty (before pack),   没pack完的(after pack)
                List<ViewModel.AllSectionInventory.pqcBinInfo> pqcBinInfo = GetPackInventory(dStartTime);
                var beforePacking = from a in pqcBinInfo where a.processes == "CHECK#1" || a.processes == "CHECK#2" select a;
                var afterPacking = from a in pqcBinInfo where a.processes == "PACKING" select a;


                //FG, Assembly list, 通过material qty/outerboxqty, 获取能被整除的数量作为fg,assembly的数量.
                var fgAssemblyList = GetFgAssembly();




                //join 
                List<ViewModel.AllSectionInventory.report> reportList = new List<ViewModel.AllSectionInventory.report>();
                foreach (var material in mainList)
                {


                    //获取该part的 bom detail list.   (material no一条)
                    var tempBomList = (from a in bomList where a.partNo == material.partNo && a.materialName == material.materialName select a).ToList();
                    if (tempBomList == null || tempBomList.Count == 0)
                    {
                        DBHelp.Reports.LogFile.Log("AllSectionInventory", "OverallReport_ViewBusiness, GetAllSectionList, can not find pqc bom, part no:[" + material.partNo + "], material name :[" + material.materialName + "]");
                        continue;
                    }





                    #region before laser
                    //laser join pqcbom,  在按照pqcbom的materialname group by.
                    var tempLaserDetailList = from a in laserInventory
                                              where a.partNo == material.partNo
                                              join b in tempBomList on new { a.partNo, a.materialNo } equals new { b.partNo, b.materialNo }
                                              select new
                                              {
                                                  a,
                                                  b
                                              };

                    //group by material name
                    var beforeLaserModel = (from a in tempLaserDetailList
                                            where a.b.materialName == material.materialName
                                            group a by a.b.materialName into summary
                                            select new
                                            {
                                                summary.Key,
                                                qty = summary.Max(p => p.a.qty)
                                            }).FirstOrDefault();
                    #endregion







                    #region after laser
                    //laser join pqcbom, 在按照pqcbom的materialname group by.
                    var tempLaserOutputDetialList = from a in laserOutputList
                                                    where a.partNo == material.partNo
                                                    join b in tempBomList on new { a.partNo, a.materialNo } equals new { b.partNo, b.materialNo }
                                                    select new
                                                    {
                                                        a,
                                                        b
                                                    };


                    //group by material name
                    var afterLaserModel = (from a in tempLaserOutputDetialList
                                           where a.b.materialName == material.materialName
                                           group a by a.b.materialName into summary
                                           select new
                                           {
                                               summary.Key,
                                               qty = summary.Max(p => p.a.okQty + p.a.ngQty)
                                           }).FirstOrDefault();
                    #endregion






                    //before wip
                    var beforeWIPModel = (from a in wipInventoryList
                                          where a.partNo == material.partNo
                                           && a.materialName == material.materialName
                                          select a).FirstOrDefault();




                    //after wip
                    var afterWIPModel = (from a in wipOutputList
                                         where a.partNo == material.partNo
                                         && a.materialName == material.materialName
                                         select a).FirstOrDefault();




                    //beforepacking
                    var beforePackingModel = (from a in beforePacking
                                              where a.partNo == material.partNo
                                              && a.materialName == material.materialName
                                              select a).FirstOrDefault();




                    //afterpacking
                    var afterPackingModel = (from a in afterPacking
                                             where a.partNo == material.partNo
                                             && a.materialName == material.materialName
                                             join b in tempBomList on new { a.partNo, a.materialName } equals new { b.partNo, b.materialName }
                                             select new
                                             {
                                                 a.materialName,
                                                 qty = a.qty % b.outerBoxQty
                                             }).FirstOrDefault();




                    var fgAssemblyModel = (from a in fgAssemblyList
                                           where a.partNo == material.partNo
                                           && a.materialName == material.materialName
                                           select a).FirstOrDefault();





                    ViewModel.AllSectionInventory.report reportModel = new ViewModel.AllSectionInventory.report();
                    reportModel.model = material.model;
                    reportModel.partNo = material.partNo;
                    reportModel.materialName = material.materialName;
                    reportModel.shipTo = material.shipTo;

                    reportModel.rawPart = null;
                    reportModel.ucPaint = null;
                    reportModel.mcPaint = null;
                    reportModel.print = null;
                    reportModel.tcPaint = null;

                    if (beforeLaserModel != null) reportModel.beforeLaser = beforeLaserModel.qty;
                    if (afterLaserModel != null) reportModel.afterLaser = afterLaserModel.qty;
                    if (beforeWIPModel != null) reportModel.beforeWIP = beforeWIPModel.inventoryQty;
                    if (afterWIPModel != null) reportModel.afterWIP = afterWIPModel.passQty + afterWIPModel.rejectQty;
                    if (beforePackingModel != null) reportModel.beforePack = beforePackingModel.qty;
                    if (afterPackingModel != null) reportModel.afterPack = afterPackingModel.qty;


                    if (fgAssemblyModel != null)
                    {
                        reportModel.fg = fgAssemblyModel.fg;
                        reportModel.assembly = fgAssemblyModel.assembly;
                    }





                    reportList.Add(reportModel);
                }


                JavaScriptSerializer js = new JavaScriptSerializer();

                var orderbyList = from a in reportList
                                  where a.afterLaser != null
                                  || a.beforeLaser != null
                                  || a.afterWIP != null
                                  || a.beforeWIP != null
                                  || a.afterPack != null
                                  || a.beforePack != null
                                  orderby a.model ascending
                                  select a;


                if (sPartNo == "" && sShipTo == "")
                {
                    return orderbyList.ToList();
                }
                else if (sPartNo != "" && sShipTo == "")
                {
                    var result = from a in orderbyList where a.partNo == sPartNo select a;
                    return result.ToList();
                }
                else if (sPartNo == "" && sShipTo != "")
                {
                    var result = from a in orderbyList where a.shipTo == sShipTo select a;
                    return result.ToList();
                }
                else
                {
                    var result = from a in orderbyList where a.shipTo == sShipTo && a.model == sPartNo select a;
                    return result.ToList();
                }

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("AllSectionInventory", "OverallReport_ViewBusiness, Catch Exception: " + ee.ToString());
                return null;
            }
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
                model.outerBoxQty = int.Parse(dr["outerBoxQty"].ToString());

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
                var partMaterialList = from a in bomList
                                       where a.partNo == tempPartNo
                                       group a by new { a.materialName, a.model, a.shipTo } into c
                                       select new
                                       {
                                           model = c.Key.model,
                                           materialName = c.Key.materialName,
                                           shipTo = c.Key.shipTo
                                       };
                if (partMaterialList == null || partMaterialList.Count() == 0)
                {
                    DBHelp.Reports.LogFile.Log("AllSectionInventory", "OverallReport_ViewBusiness, GetAllMaterialList, can not find part no from pqc bom [" + tempPartNo + "]");
                    continue;
                }



                foreach (var material in partMaterialList)
                {
                    ViewModel.AllSectionInventory.mainMaterialList model = new ViewModel.AllSectionInventory.mainMaterialList();

                    model.model = material.model;
                    model.partNo = tempPartNo;
                    model.materialName = material.materialName;
                    model.shipTo = material.shipTo;




                    var existModel = from a in modelList
                                     where a.model == model.model &&
                                          a.partNo == model.partNo &&
                                          a.materialName == model.materialName &&
                                          a.shipTo == model.shipTo
                                     select a;

                    if (existModel == null || existModel.Count() == 0)
                    {
                        modelList.Add(model);
                    }

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
                model.partNo = dr["partNumber"].ToString();
                model.materialNo = dr["materialNo"].ToString();
                model.okQty = double.Parse(dr["okQty"].ToString());
                model.ngQty = double.Parse(dr["ngQty"].ToString());
         
                modelList.Add(model);
            }


            return modelList;
        }



        //before laser
        //按amterial no汇总的信息
        private List<ViewModel.AllSectionInventory.laserInventoryInfo> GetLaserInventory(DateTime dStartTime)
        {
            DataTable dt = laserInventoryBLL.GetInventoryInfoForAllInventoryReport(dStartTime);
            if (dt == null || dt.Rows.Count == 0)
                return null;
            
            List<ViewModel.AllSectionInventory.laserInventoryInfo> modelList = new List<ViewModel.AllSectionInventory.laserInventoryInfo>();
            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.AllSectionInventory.laserInventoryInfo model = new ViewModel.AllSectionInventory.laserInventoryInfo();
             
                model.partNo = dr["partNumber"].ToString();
                model.materialNo = dr["materialPartNo"].ToString();
                model.qty = double.Parse(dr["inventoryQty"].ToString());
                
                modelList.Add(model);
            }


            return modelList;
        }


        //after wip ,按amterial no汇总的信息
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


        //before wip ,按amterial no汇总的信息
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


        //按amterial no汇总的信息
        private List<ViewModel.AllSectionInventory.pqcBinInfo> GetPackInventory(DateTime dStartTime)
        {
            DataTable dt = pqcBinBLL.GetBinInfoForAllInventoryReport(dStartTime);
            if (dt == null)
                return null;



            List<ViewModel.AllSectionInventory.pqcBinInfo> modelList = new List<ViewModel.AllSectionInventory.pqcBinInfo>();
            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.AllSectionInventory.pqcBinInfo model = new ViewModel.AllSectionInventory.pqcBinInfo();
                model.partNo = dr["PartNumber"].ToString();
                model.processes = dr["processes"].ToString();
                model.materialName = dr["materialName"].ToString();
                model.qty = double.Parse(dr["inventoryQty"].ToString());

                modelList.Add(model);
            }

            return modelList;
        }



        private List<ViewModel.AllSectionInventory.fgAndAssembly> GetFgAssembly()
        {
            DataTable dt = pqcBinBLL.GetFgAndAssembly();
            if (dt == null)
                return null;



            List<ViewModel.AllSectionInventory.fgAndAssembly> fgAssyList = new List<ViewModel.AllSectionInventory.fgAndAssembly>();
            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.AllSectionInventory.fgAndAssembly model = new ViewModel.AllSectionInventory.fgAndAssembly();
                model.partNo = dr["PartNumber"].ToString();
                model.materialName = dr["materialName"].ToString();
              
                model.fg = dr["FG"].ToString() == ""? 0:  double.Parse(dr["FG"].ToString());
                model.assembly = dr["Assembly"].ToString() == "" ? 0 : double.Parse(dr["Assembly"].ToString());

                fgAssyList.Add(model);
            }

            return fgAssyList;
        }


         
        
        #endregion




    }
}