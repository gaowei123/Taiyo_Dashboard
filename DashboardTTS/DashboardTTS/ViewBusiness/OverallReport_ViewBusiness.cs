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
            
            //painting output 
            List<ViewModel.AllSectionInventory.paintingInfo> paintOutList = GetPaintingOutput();

            //group by material for paint
            var paintSummary = from a in paintOutList
                               where a.sendingTo == "LASER"
                               group a by a.materialNo
                                       into groupedList
                               select new
                               {
                                   groupedList.Key,
                                   paintOutput = groupedList.Sum(p => p.output)
                               };




            //laser output  
            List<ViewModel.AllSectionInventory.laserInfo> laserOutList = GetLaserOutput();
            
            //group by material for laser
            var laserSummary = from a in laserOutList
                               group a by a.materialNo
                               into groupedList
                               select new
                               {
                                   groupedList.Key,
                                   laserOutput = groupedList.Sum(p => p.outputQty)
                               };





            //laser inventory :   paintoutput - laser output 
            var laserInventory = from a in paintSummary
                                 join b in laserSummary on a.Key equals b.Key
                                 select new
                                 {
                                     a.Key,
                                     inventory = a.paintOutput - b.laserOutput
                                 };










            //pqc output
            //List < ViewModel.AllSectionInventory.pqcCheckInfo > pqcOutList = GetCheckInfo(dateStart);



            //pqc check#1 output 
            //var checkOutput_1 = from a in pqcOutList
            //                   where a.process == "CHECK#1"
            //                   select a;

            //pqc check#2 output 
            //var checkOutput_2 = from a in pqcOutList
            //                    where a.process == "CHECK#2"
            //                    select a;

            

            //pqc bin
            List < ViewModel.AllSectionInventory.pqcBinInfo > pqcBinList = GetPQCBinInfo(dateStart);


            //pqc check#1 bin
            var checkBin_1 = from a in pqcBinList
                             group a by a.materialNo into c
                             where c.Max(p=>p.process) == "CHECK#1"
                             select new
                             {
                                 c.Key,
                                 inventory = c.Sum(p => p.binQty)
                             };

            //pqc check#2 bin 
            var checkBin_2 = from a in pqcBinList
                             group a by a.materialNo into c
                             where c.Max(p => p.process) == "CHECK#2"                             
                             select new
                             {
                                 c.Key,
                                 inventory = c.Sum(p => p.binQty)
                             };





           

            var paintSummaried = from a in paintOutList
                                 group a by new { a.model, a.partNo, a.materialNo }
                                 into b
                                 select new
                                 {
                                     b.Key.model,
                                     b.Key.partNo,
                                     b.Key.materialNo,
                                     output = b.Sum(p => p.output)
                                 };



            List<ViewModel.AllSectionInventory.report> reportList = new List<ViewModel.AllSectionInventory.report>();

            foreach (var item in paintSummaried)
            {
                

                ViewModel.AllSectionInventory.report model = new ViewModel.AllSectionInventory.report();

                model.model = item.model;
                model.partNo = item.partNo;
                model.materialNo = item.materialNo;



                //paint inventory
                model.paintInventory = null;


                //laser inventory
                if (laserInventory == null)
                {
                    model.laserInventory = 0;
                }
                else
                {
                    var laser = from a in laserInventory
                                where a.Key == item.materialNo
                                select a;
                    if (laser != null && laser.Count() != 0)
                        model.laserInventory = laser.FirstOrDefault().inventory < 0 ? 0 : laser.FirstOrDefault().inventory;
                    else
                        model.laserInventory = 0;
                }
                
                //check#1 inventory
                if (checkBin_1 == null)
                {
                    model.checkInventory_1 = 0;
                }
                else
                {
                    var check_1 = from a in checkBin_1
                                  where a.Key == item.materialNo
                                  select a;
                    if (check_1 != null && check_1.Count() != 0)
                        model.checkInventory_1 = check_1.FirstOrDefault().inventory < 0 ? 0 : check_1.FirstOrDefault().inventory;
                    else
                        model.checkInventory_1 = 0;
                }
                
                //check#2 inventory
                if (checkBin_2 == null)
                {
                    model.checkInventory_2 = 0;
                }
                else
                {
                    var check_2 = from a in checkBin_2
                                  where a.Key == item.materialNo
                                  select a;
                    if (check_2 != null && check_2.Count() != 0)
                        model.checkInventory_2 = check_2.FirstOrDefault().inventory < 0 ? 0 : check_2.FirstOrDefault().inventory;
                    else
                        model.checkInventory_2 = 0;
                }
                
                //packing inventory 
                model.packingInventory = null;



                reportList.Add(model);
                
            }



            var result = from a in reportList
                         where 
                         a.laserInventory != 0 ||
                         a.checkInventory_1 != 0 ||
                         a.checkInventory_2 != 0 
                         orderby a.model ascending , a.partNo ascending, a.materialNo ascending
                         select a;





            JavaScriptSerializer js = new JavaScriptSerializer();

            return js.Serialize(result);
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