using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Taiyo.Tool.Extension;

namespace DashboardTTS.ViewBusiness
{
    public class PQCProduct
    {
        private readonly Common.Class.BLL.PQCQaViTracking_BLL viTrackingBLL = new Common.Class.BLL.PQCQaViTracking_BLL();
        private readonly Common.Class.BLL.PaintingDeliveryHis_BLL paintBLL = new Common.Class.BLL.PaintingDeliveryHis_BLL();
        private readonly Common.Class.BLL.PQCQaViDetailTracking_BLL viDetailBLL = new Common.Class.BLL.PQCQaViDetailTracking_BLL();
        private readonly Common.Class.BLL.PQCQaViDefectTracking_BLL viDefectBLL = new Common.Class.BLL.PQCQaViDefectTracking_BLL();
        private readonly Common.Class.BLL.PQCInventory_BLL inventoryBLL = new Common.Class.BLL.PQCInventory_BLL();
        private readonly Common.Class.BLL.PQCQaViBinning viBinBLL = new Common.Class.BLL.PQCQaViBinning();
        private readonly Common.Class.BLL.PQCQaViBinHistory_BLL binHisBLL = new Common.Class.BLL.PQCQaViBinHistory_BLL();
        private readonly Common.Class.BLL.PQCPackTracking packTrackBLL = new Common.Class.BLL.PQCPackTracking();
        private readonly Common.Class.BLL.PQCBom_BLL bomBLL = new Common.Class.BLL.PQCBom_BLL();



        JavaScriptSerializer _js = new JavaScriptSerializer();


        public PQCProduct()
        {

        }

        





        #region daily pqc  report 
        public List<ViewModel.PQCDailyReport_ViewModel> GetCheckingDailyList(DateTime dDateFrom, DateTime dDateTo, string sShift, string sPartNo, string sStation, string sPIC, string sType)
        {

            Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();
            DataTable dt = bll.GetCheckingDailyList( dDateFrom, dDateTo, sShift, sPartNo, sStation, sPIC, sType);


            if (dt == null || dt.Rows.Count == 0)
                return null;



            List<ViewModel.PQCDailyReport_ViewModel> dailyList = new List<ViewModel.PQCDailyReport_ViewModel>();


            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.PQCDailyReport_ViewModel model = new ViewModel.PQCDailyReport_ViewModel();



                model.sDate = dr["Day"].ToString();
                model.station = dr["Station"].ToString();
                model.partNo = dr["partNumber"].ToString();
                model.jobNo = dr["JobNumber"].ToString();
                model.lotNo = dr["lotNo"].ToString();


                if (dr["MrpQtyPcs"].ToString() == "")
                {
                    model.dMrpQtyPcs = 0;
                    model.dMrpQtySet = 0;
                    model.sMrpQty = "No Found";
                }
                else
                {
                    model.dMrpQtyPcs = double.Parse(dr["MrpQtyPcs"].ToString());
                    model.dMrpQtySet = double.Parse(dr["MrpQtySet"].ToString());
                    model.sMrpQty = string.Format("{0}({1})", model.dMrpQtySet, model.dMrpQtyPcs);
                }

               
                model.dTotalQtyPcs = double.Parse(dr["totalQtyPcs"].ToString());
                model.dTotalQtySet = double.Parse(dr["totalQtySet"].ToString());
                model.dTotalPassPcs = double.Parse(dr["acceptQtyPcs"].ToString());
                model.dTotalPassSet = double.Parse(dr["passQtySet"].ToString());


               
                model.sTotalQty = string.Format("{0}({1})", model.dTotalQtySet, model.dTotalQtyPcs);
                model.sTotalPass = string.Format("{0}({1})", model.dTotalPassSet, model.dTotalPassPcs);



                model.mouldingRej = double.Parse(dr["MouldingRej"].ToString());
                model.paintingRej = double.Parse(dr["PaintingRej"].ToString());
                model.laserRej = double.Parse(dr["LaserRej"].ToString());
                model.othersRej = double.Parse(dr["OthersRej"].ToString());
                model.totalRej = double.Parse(dr["rejectQty"].ToString());




                model.startTime = DateTime.Parse(dr["startTime"].ToString());
                if (dr["stoptime"].ToString() == "")
                {
                    model.stopTime = null;
                    model.sUsedTime = "-";
                }
                else
                {
                    model.stopTime = DateTime.Parse(dr["stoptime"].ToString());
                    model.dUsedTime = double.Parse(dr["totalSecond"].ToString());
                    model.sUsedTime = Common.CommFunctions.ConvertDateTimeShort((model.dUsedTime / 3600).ToString());
                }





                model.pic = dr["PIC"].ToString().ToUpper();




                dailyList.Add(model);
            }


            ViewModel.PQCDailyReport_ViewModel summaryModel = new ViewModel.PQCDailyReport_ViewModel();
            summaryModel.sDate = "Total";
            summaryModel.sMrpQty = string.Format("{0}({1})", dailyList.Sum(p => p.dMrpQtySet), dailyList.Sum(p => p.dMrpQtyPcs));
            summaryModel.sTotalQty = string.Format("{0}({1})", dailyList.Sum(p => p.dTotalQtySet), dailyList.Sum(p => p.dTotalQtyPcs));
            summaryModel.sTotalPass = string.Format("{0}({1})", dailyList.Sum(p => p.dTotalPassSet), dailyList.Sum(p => p.dTotalPassPcs));

            summaryModel.mouldingRej = dailyList.Sum(p => p.mouldingRej);
            summaryModel.paintingRej = dailyList.Sum(p => p.paintingRej);
            summaryModel.laserRej = dailyList.Sum(p => p.laserRej);
            summaryModel.othersRej = dailyList.Sum(p => p.othersRej);
            summaryModel.totalRej = dailyList.Sum(p => p.totalRej);

            summaryModel.sUsedTime =Common.CommFunctions.ConvertDateTimeShort((dailyList.Sum(p => p.dUsedTime)/3600).ToString()) ;


            dailyList.Add(summaryModel);


            return dailyList;
        }
        
        public List<ViewModel.PQCDailyReport_ViewModel> GetPackingDailyList(DateTime dDateFrom, DateTime dDateTo, string sShift, string sPartNo, string sStation, string sPIC)
        {

            Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();
            DataTable dt = bll.GetPackingDailyList(dDateFrom, dDateTo, sShift, sPartNo, sStation, sPIC);


            if (dt == null || dt.Rows.Count == 0)
                return null;



            List<ViewModel.PQCDailyReport_ViewModel> dailyList = new List<ViewModel.PQCDailyReport_ViewModel>();


            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.PQCDailyReport_ViewModel model = new ViewModel.PQCDailyReport_ViewModel();


                model.sDate = dr["Day"].ToString();
                model.station = dr["Station"].ToString();
                model.partNo = dr["partNumber"].ToString();
                model.jobNo = dr["JobNumber"].ToString();
                model.lotNo = dr["lotNo"].ToString();


                model.dMrpQtyPcs = double.Parse(dr["MrpQtyPcs"].ToString());
                model.dMrpQtySet = double.Parse(dr["MrpQtySet"].ToString());
                model.dTotalQtyPcs = double.Parse(dr["totalQtyPcs"].ToString());
                model.dTotalQtySet = double.Parse(dr["totalQtySet"].ToString());
                model.dTotalPassPcs = double.Parse(dr["acceptQtyPcs"].ToString());
                model.dTotalPassSet = double.Parse(dr["passQtySet"].ToString());


                model.sMrpQty = string.Format("{0}({1})", model.dMrpQtySet, model.dMrpQtyPcs);
                model.sTotalQty = string.Format("{0}({1})", model.dTotalQtySet, model.dTotalQtyPcs);
                model.sTotalPass = string.Format("{0}({1})", model.dTotalPassSet, model.dTotalPassPcs);



          
                model.totalRej = double.Parse(dr["rejectQty"].ToString());




                model.startTime = DateTime.Parse(dr["startTime"].ToString());
                if (dr["stoptime"].ToString() == "")
                {
                    model.stopTime = null;
                    model.sUsedTime = "-";
                }
                else
                {
                    model.stopTime = DateTime.Parse(dr["stoptime"].ToString());
                    model.dUsedTime = double.Parse(dr["totalSecond"].ToString());
                    model.sUsedTime = Common.CommFunctions.ConvertDateTimeShort((model.dUsedTime / 3600).ToString());
                }


                model.pic = dr["PIC"].ToString().ToUpper();




                dailyList.Add(model);
            }


            ViewModel.PQCDailyReport_ViewModel summaryModel = new ViewModel.PQCDailyReport_ViewModel();
            summaryModel.sDate = "Total";
            summaryModel.sMrpQty = string.Format("{0}({1})", dailyList.Sum(p => p.dMrpQtySet), dailyList.Sum(p => p.dMrpQtyPcs));
            summaryModel.sTotalQty = string.Format("{0}({1})", dailyList.Sum(p => p.dTotalQtySet), dailyList.Sum(p => p.dTotalQtyPcs));
            summaryModel.sTotalPass = string.Format("{0}({1})", dailyList.Sum(p => p.dTotalPassSet), dailyList.Sum(p => p.dTotalPassPcs));

          
            summaryModel.totalRej = dailyList.Sum(p => p.totalRej);

            summaryModel.sUsedTime = Common.CommFunctions.ConvertDateTimeShort((dailyList.Sum(p => p.dUsedTime) / 3600).ToString());


            dailyList.Add(summaryModel);



            return dailyList;
        }

        #endregion



        #region pqc checking maintenance
        public ViewModel.PQCCheckingMaintenance_ViewModel.JobInfo GetJobInfo(string sTrackingID, string sJobNo)
        {

            Common.Class.Model.PQCQaViTracking viModel = viTrackingBLL.GetModelByTrackingID(sTrackingID);
            if (viModel == null)
                return null;


            Common.Class.Model.PaintingDeliveryHis_Model paintModel = paintBLL.GetModel(sJobNo, "Paint#1");




            ViewModel.PQCCheckingMaintenance_ViewModel.JobInfo jobInfoModel = new ViewModel.PQCCheckingMaintenance_ViewModel.JobInfo();
            jobInfoModel.day = viModel.day;
            jobInfoModel.shift = viModel.shift;
            jobInfoModel.jobNo = sJobNo;
            jobInfoModel.trackingID = sTrackingID;
            jobInfoModel.partNo = viModel.partNumber;

            if (paintModel != null)
            {
                jobInfoModel.mrpQty = double.Parse(paintModel.inQuantity.ToString());
            }


            return jobInfoModel;
        }


        public List<ViewModel.PQCCheckingMaintenance_ViewModel.MaterialInfo> GetMaterialList(string sTrackingID, string sJobNo)
        {
            DataTable dt = viDetailBLL.GetList(sTrackingID);
            if (dt == null || dt.Rows.Count == 0)
                return null;


            

            //2021-03-25
            //获取painting qa, setup的数量计算在material list的rej中. 保持和defect rej数量的一致
            Common.Class.BLL.PaintingTempInfo paintTempBLL = new Common.Class.BLL.PaintingTempInfo();
            DataTable dtPaintTemp = paintTempBLL.GetList(null, null, string.Empty, sJobNo);

            List<ViewModel.PQCCheckingMaintenance_ViewModel.MaterialInfo> modelList = new List<ViewModel.PQCCheckingMaintenance_ViewModel.MaterialInfo>();
            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.PQCCheckingMaintenance_ViewModel.MaterialInfo model = new ViewModel.PQCCheckingMaintenance_ViewModel.MaterialInfo();
                model.materialNo = dr["materialPartNo"].ToString();
                model.passQty = double.Parse(dr["passQty"].ToString());
                model.rejQty = double.Parse(dr["rejectQty"].ToString());

                double qaQty = 0;
                double setupQty = 0;
                if (dtPaintTemp != null && dtPaintTemp.Rows.Count != 0)
                {
                    DataRow[] arrDrs = dtPaintTemp.Select($" materialPartNo = '{model.materialNo}'");
                    if (arrDrs != null && arrDrs.Length != 0)
                    {
                        qaQty = int.Parse(arrDrs[0]["qaTestQty"].ToString());
                        setupQty = int.Parse(arrDrs[0]["setupRejQty"].ToString());
                    }
                }


                model.rejQty = model.rejQty + qaQty + setupQty;


                modelList.Add(model);
            }


            var sorted = (from a in modelList orderby a.materialNo ascending select a).ToList();

            for (int i = 0; i < sorted.Count; i++)
            {
                sorted[i].sn = i;
            }

            return sorted;
        }

        
        public List<ViewModel.PQCCheckingMaintenance_ViewModel.DefectInfo> GetDefectInfo(string sTrackingID, string sMaterialNo)
        {
            DataTable dt = viDefectBLL.GetListByTrackingID(sTrackingID);
            if (dt == null || dt.Rows.Count == 0)
                return null;

            string jobNo = "";

            var modelList = new List<ViewModel.PQCCheckingMaintenance_ViewModel.DefectInfo>();
            foreach (DataRow dr in dt.Rows)
            {
                var model = new ViewModel.PQCCheckingMaintenance_ViewModel.DefectInfo();
                model.materialNo = dr["materialPartNo"].ToString();
                model.defectCodeID = int.Parse(dr["defectCodeID"].ToString());
                model.defectCode = dr["defectCode"].ToString();
                model.defectDescription = dr["defectDescription"].ToString();
                model.rejQty = double.Parse(dr["rejectQty"].ToString());
                jobNo = dr["jobId"].ToString();


                modelList.Add(model);
            }



            //为了同步overall buyoff report & button report中的数据的一致性. 
            //在painting defectcode中添加QA, Setup显示, 来提供维护.
            #region 2021-03-25 qa, setup
            int qaQty = 0;
            int setupQty = 0;

            Common.Class.BLL.PaintingTempInfo paintTempBLL = new Common.Class.BLL.PaintingTempInfo();
            DataTable dtPaintTemp =  paintTempBLL.GetList(null, null, string.Empty, jobNo);
            if (dtPaintTemp != null && dtPaintTemp.Rows.Count !=0 )
            {
                DataRow[] arrDrs = dtPaintTemp.Select($" materialPartNo = '{sMaterialNo}'");
                if (arrDrs!= null && arrDrs.Length != 0)
                {
                     qaQty = int.Parse(arrDrs[0]["qaTestQty"].ToString());
                     setupQty= int.Parse(arrDrs[0]["setupRejQty"].ToString());
                }
            }
           
            // 89 - QA
            // 90 - Setup
            modelList.Add(CreateExtraDefect(sMaterialNo, 89, "QA", qaQty));
            modelList.Add(CreateExtraDefect(sMaterialNo, 90, "Setup", setupQty));
            #endregion


            var materialList = (from a in modelList where a.materialNo == sMaterialNo orderby a.materialNo ascending, a.defectCodeID ascending select a).ToList();


            return materialList;
        }


        private ViewModel.PQCCheckingMaintenance_ViewModel.DefectInfo CreateExtraDefect(string MaterialNo, int ID, string Code, int RejQty )
        {
            return new ViewModel.PQCCheckingMaintenance_ViewModel.DefectInfo()
            {
                materialNo = MaterialNo,
                defectCodeID = ID,
                defectCode = Code,
                defectDescription = "Paint",
                rejQty = RejQty
            };
        }



        public bool UpdateQty( System.Collections.Specialized.NameValueCollection formParameters)
        {
            string trackingID = formParameters["TrackingID"];
            string jobNo = formParameters["JobNo"];


            //关联获取 material sn
            List<ViewModel.PQCCheckingMaintenance_ViewModel.MaterialInfo> materialList = GetMaterialList(trackingID, jobNo);
            
            
            //处理 defect list
            List<Common.Class.Model.PQCQaViDefectTracking_Model> defectTrackingList = viDefectBLL.GetModelList(trackingID);
            foreach (Common.Class.Model.PQCQaViDefectTracking_Model model in defectTrackingList)
            {
                //matrial sn,  defectCode id
                int materialSN = (from a in materialList where a.materialNo == model.materialPartNo select a).FirstOrDefault().sn;


                //根据前台id自动命名规则,   txtid_{material sn}_{defect code id}   获取提交的数量
                decimal rejQty = decimal.Parse(formParameters[$"txtid_{materialSN.ToString()}_{model.defectCodeID}"]);

                
                model.rejectQty = rejQty;
                model.updatedTime = DateTime.Now;
                model.lastUpdatedTime = DateTime.Now;
                model.remarks = "PQC Job Maintenance Update Qty";
            }



            #region ================ 2021-03-25  更新paintingTempInfo中的qa,setup数量. ================
            List<Common.Class.Model.PaintingTempInfo_Model> paintTempModelList = new List<Common.Class.Model.PaintingTempInfo_Model>();
            foreach (var model in materialList)
            {
                paintTempModelList.Add(new Common.Class.Model.PaintingTempInfo_Model()
                {
                    jobNumber = jobNo,
                    materialName = model.materialNo,
                    updatedTime = DateTime.Now,

                    // 89 - QA, 90 - Setup
                    qaTestQty = decimal.Parse(formParameters[$"txtid_{model.sn.ToString()}_89"]),
                    setupRejQty = decimal.Parse(formParameters[$"txtid_{model.sn.ToString()}_90"])
                });
            }
            Common.Class.BLL.PaintingTempInfo paintTempInfoBLL = new Common.Class.BLL.PaintingTempInfo();
            bool result = paintTempInfoBLL.UpdateQASetup(paintTempModelList);
            if (!result)
            {
                DBHelp.Reports.LogFile.Log("CheckingMaintenance", "Func UpdateQty, update PaintTempInfo qa,setup fail!");
            }
            #endregion ================ 2021-03-25  更新paintingTempInfo中的qa,setup数量. ================
          






            //处理 detail list
            //收集对应material pass qty修改后增加了多少。 用于更新 vi bin history。
            Dictionary<string, decimal> dicAddedPassQty = new Dictionary<string, decimal>();

            List<Common.Class.Model.PQCQaViDetailTracking_Model> detailTrackingList = viDetailBLL.GetModelList(trackingID, "", null, null);
            foreach (Common.Class.Model.PQCQaViDetailTracking_Model model in detailTrackingList)
            {

                //matrial sn,  defectCode id
                int materialSN = (from a in materialList where a.materialNo == model.materialPartNo select a).FirstOrDefault().sn;


                //从defect中 汇总出rej qty
                decimal rejQty = (from a in defectTrackingList where a.materialPartNo == model.materialPartNo select a).Sum(p => p.rejectQty).Value;


                //前台id规则,   txtid_sn  获取提交的数量.
                decimal passQty = decimal.Parse(formParameters["txtid_" + materialSN.ToString()]);



                //记录passqty增加了多少。
                dicAddedPassQty.Add(model.materialPartNo, passQty - model.passQty.Value);




                model.passQty = passQty;
                model.rejectQty = rejQty;
                model.totalQty = passQty + rejQty;
                model.updatedTime = DateTime.Now;
                model.lastUpdatedTime = DateTime.Now;
                model.remarks = "PQC Job Maintenance Update Qty";
            }





            


            //处理 vi tracing model
            Common.Class.Model.PQCQaViTracking viTrackingModel = viTrackingBLL.GetModelByTrackingID(trackingID);
            
            viTrackingModel.TotalQty = detailTrackingList.Sum(p => p.totalQty).ToString();
            viTrackingModel.acceptQty = detailTrackingList.Sum(p => p.passQty).ToString();
            viTrackingModel.rejectQty = detailTrackingList.Sum(p => p.rejectQty).ToString();
            viTrackingModel.updatedTime = DateTime.Now;
            viTrackingModel.lastUpdatedTime = DateTime.Now;
            viTrackingModel.remarks = "PQC Job Maintenance Update Qty";

            //2021-03-26, 避免没有endtime operator daily report不显示, 直接赋值成starttime.
            viTrackingModel.stopTime = viTrackingModel.stopTime == null ? viTrackingModel.startTime : viTrackingModel.stopTime;









            //处理 PQCQaViBinning
            List<Common.Class.Model.PQCQaViBinning> viBinList = new List<Common.Class.Model.PQCQaViBinning>();
            List<Common.Class.Model.PQCQaViBinHistory_Model> binHistoryList = new List<Common.Class.Model.PQCQaViBinHistory_Model>();

            bool isUpdateBin;

            //根据 jobid， process 获取下改 bin 信息。
            viBinList = viBinBLL.GetModelList(null,null, viTrackingModel.jobId, viTrackingModel.processes);

            if (viBinList == null || viBinList.Count == 0)
            {
                #region insert new 

                isUpdateBin = false;
                viBinList = new List<Common.Class.Model.PQCQaViBinning>();
                foreach (Common.Class.Model.PQCQaViDetailTracking_Model detailModel in detailTrackingList)
                {
                    Common.Class.Model.PQCQaViBinning binModel = new Common.Class.Model.PQCQaViBinning();
                    binModel.PartNumber = viTrackingModel.partNumber;
                    binModel.jobId = detailModel.jobid;
                    binModel.trackingID = detailModel.trackingID;
                    binModel.materialPartNo = detailModel.materialPartNo;
                    binModel.materialName = detailModel.materialName;
                    binModel.shipTo = detailModel.ShipTo;
                    binModel.model = detailModel.model;
                    binModel.jigNo = detailModel.jigNo;

                    binModel.updatedTime = DateTime.Now;
                    binModel.status = "LOAD";
                    binModel.nextViFlag = "true";
                    binModel.remark_1 = detailModel.remark_1;
                    binModel.remark_2 = detailModel.remark_2;
                    binModel.remark_3 = "";
                    binModel.remark_4 = "";
                    binModel.remarks = detailModel.remarks;
                    binModel.processes = detailModel.processes;
                    binModel.shipTo = detailModel.ShipTo;

                    binModel.day = detailModel.day;
                    binModel.shift = detailModel.shift;
                    binModel.userName = detailModel.userName;
                    binModel.userID = detailModel.userID;
                    
                    binModel.id = Guid.NewGuid().ToString();
                    binModel.materialQty = detailModel.passQty;
                    binModel.dateTime = DateTime.Now;


                    viBinList.Add(binModel);
                }

                foreach (var binModel in viBinList)
                {
                    Common.Class.Model.PQCQaViBinHistory_Model binHisModel = new Common.Class.Model.PQCQaViBinHistory_Model();
                    binHisModel = binHisBLL.CopyModel(binModel);
                    binHisModel.materialFromQty = 0;

                    binHistoryList.Add(binHisModel);
                }
                #endregion
            }
            else
            {
                #region update bin

                isUpdateBin = true;

                foreach (Common.Class.Model.PQCQaViBinning binModel in viBinList)
                {

                    decimal addedPassQty = dicAddedPassQty[binModel.materialPartNo];
                    decimal curPassQty = binModel.materialQty.Value;


                    //material qty =  bin的原本数量  +  修改后增加的数量。
                    binModel.materialQty = binModel.materialQty.Value + addedPassQty;
                    binModel.updatedTime = DateTime.Now;
                    //binModel.remark_1 = "Updated By " + formParameters["txtUsername"];



                    //拷贝到bin history model， 并将原本数量赋值给material from qty， 并添加到list中。
                    Common.Class.Model.PQCQaViBinHistory_Model binHisModel = new Common.Class.Model.PQCQaViBinHistory_Model();
                    binHisModel = binHisBLL.CopyModel(binModel);
                    binHisModel.materialFromQty = curPassQty;
                    binHisModel.updatedTime = DateTime.Now;
                    binHistoryList.Add(binHisModel);


                }
                #endregion
            }
            

            return viTrackingBLL.MaintenanceUpdateQty(viTrackingModel, detailTrackingList, defectTrackingList, viBinList, binHistoryList, isUpdateBin);
        }




        #endregion



        #region wip inventory report & job order detail
        public string GetWIPInventory()
        {

            DateTime dateFrom = DateTime.Parse("2020-6-1");
            DateTime dateTo = DateTime.Now.AddDays(1);

            List<ViewModel.PQCWIPInventory_ViewModel> modelList = GetWIPList(dateFrom, dateTo, "", "","");



            //group by partnumber
            var summaryList = from a in modelList
                              where a.jobStatus == "Pending" || a.jobStatus == "Inprocess"
                              group a by a.partNo into summary
                              select new
                              {
                                  customer = summary.Max(p => p.customer),
                                  model = summary.Max(p => p.model),
                                  partNo = summary.Key,

                                  jobNo = (summary.Sum(p => p.jobCount) == 1 ? summary.Max(p => p.jobNo) : "JOT###"),



                                  mrpQtyPCS = summary.Sum(p => p.mrpQtyPCS),
                                  mrpQtySET = summary.Sum(p => p.mrpQtySET),
                                  sMrpQty = summary.Sum(p => p.mrpQtySET) + "(" + summary.Sum(p => p.mrpQtyPCS) + ")",


                                  beforeQtyPCS = summary.Sum(p => p.beforeQtyPCS),
                                  beforeQtySET = summary.Sum(p => p.beforeQtySET),
                                  sBeforeQty = summary.Sum(p => p.beforeQtySET) + "(" + summary.Sum(p => p.beforeQtyPCS) + ")",


                                  afterQtyPCS = summary.Sum(p => p.afterQtyPCS),
                                  afterQtySET = summary.Sum(p => p.afterQtySET),
                                  sAfterQty = summary.Sum(p => p.afterQtySET) + "(" + summary.Sum(p => p.afterQtyPCS) + ")",



                                  jobCount = summary.Sum(p => p.jobCount),

                                  mfgDate = (summary.Sum(p => p.jobCount) == 1 ? summary.Max(p => p.mfgDate.Value.ToString("dd/MM/yyyy")) : ""),
                                     

                                     bomFlag = summary.Max(p => p.bomFlag)
                                 };

            var sorted = from a in summaryList
                         orderby a.customer ascending, a.model ascending, a.partNo ascending
                         select a;



            JavaScriptSerializer js = new JavaScriptSerializer();
            
            return js.Serialize(sorted);
        }

        public List<ViewModel.PQCWIPInventory_ViewModel> GetJobOrderDetailList(DateTime? dDateFrom, DateTime? dDateTo, string sPartNo, string sModel, string sJobNo, string sJobStatus)
        {
            List<ViewModel.PQCWIPInventory_ViewModel>  modelList = GetWIPList(dDateFrom, dDateTo, sPartNo, sModel, sJobNo);
            if (modelList == null)
                return null;

            if (sJobStatus == "")
            {
                var jobList = from a in modelList
                              orderby a.mfgDate descending
                              select a;

                return jobList.ToList();
            }
            else if (sJobStatus == "Uncomplete")
            {
                var jobList = from a in modelList
                              where a.jobStatus == "Pending" || a.jobStatus == "Inprocess"
                              orderby a.mfgDate descending
                              select a;

                return jobList.ToList();
            }
            else
            {
                var jobList = from a in modelList
                              where a.jobStatus == sJobStatus
                              orderby a.mfgDate
                              select a;

                return jobList.ToList();
            }
        }



        private List<ViewModel.PQCWIPInventory_ViewModel> GetWIPList(DateTime? dDateFrom, DateTime? dDateTo, string sPartNo, string sModel, string sJobNo)
        {
            DataTable dt = inventoryBLL.GetWIPInventoryReport(dDateFrom, dDateTo, sPartNo, sModel, sJobNo);
            if (dt == null || dt.Rows.Count == 0)
                return null;


            List<ViewModel.PQCWIPInventory_ViewModel> modelList = new List<ViewModel.PQCWIPInventory_ViewModel>();

            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.PQCWIPInventory_ViewModel model = new ViewModel.PQCWIPInventory_ViewModel();


                model.customer = dr["customer"].ToString();

                model.model = dr["model"].ToString();
                model.partNo = dr["partNumber"].ToString();
                model.jobNo = dr["jobNumber"].ToString();

                model.mrpQtyPCS = double.Parse(dr["MrpQtyPCS"].ToString());
                model.mrpQtySET = double.Parse(dr["MrpQtySET"].ToString());
                model.sMrpQty = model.mrpQtySET + "(" + model.mrpQtyPCS + ")";

                model.beforeQtyPCS = double.Parse(dr["BeforePCS"].ToString());
                model.beforeQtySET = double.Parse(dr["BeforeSET"].ToString());
                model.sBeforeQty = model.beforeQtySET + "(" + model.beforeQtyPCS + ")";

                model.afterQtyPCS = double.Parse(dr["AfterPCS"].ToString());
                model.afterQtySET = double.Parse(dr["AfterSET"].ToString());
                model.sAfterQty = model.afterQtySET + "(" + model.afterQtyPCS + ")";


                model.jobCount = 1;
                model.mfgDate = DateTime.Parse(dr["MFGDate"].ToString());
                model.bomFlag = dr["BomFlag"].ToString();
                model.jobStatus = dr["jobStatus"].ToString();

                
                modelList.Add(model);
            }



            return modelList;
        }



        #endregion



        //packing detail list
        public List<ViewModel.PackingDetail_ViewModel> GetPackingDetailList(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sStation, string sPIC, string sJobNo, string sLotNo, string sType)
        {
            DataTable dt = packTrackBLL.GetList(dDateFrom, dDateTo,"", sPartNo, sStation, sPIC, sJobNo);
            if (dt == null || dt.Rows.Count == 0) return null;



            DataTable dtPaint = paintBLL.GetList(dDateFrom.AddMonths(-6), dDateTo, "");

            List<Common.Class.Model.PQCBom_Model> bomList = bomBLL.GetModelList();


            List<ViewModel.PackingDetail_ViewModel> modelList = new List<ViewModel.PackingDetail_ViewModel>();
            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.PackingDetail_ViewModel model = new ViewModel.PackingDetail_ViewModel();
                model.trackingID = dr["trackingID"].ToString();
                model.day = DateTime.Parse(dr["day"].ToString());
                model.shift = dr["shift"].ToString();
                model.station = dr["machineID"].ToString();
                model.partNo = dr["partnumber"].ToString();
                model.jobID = dr["jobId"].ToString();

                //从painting delivery中获取 lotno
                DataRow[] tempDrArr = dtPaint.Select(" jobNumber = '" + dr["jobId"].ToString() + "'");
                if (tempDrArr != null && tempDrArr.Count() != 0)
                    model.lotNo = tempDrArr[0]["lotNo"].ToString();
                
                model.okQty = double.Parse(dr["acceptQty"].ToString());
                model.ngQty = double.Parse(dr["rejectQty"].ToString());
                model.totalQty = double.Parse(dr["totalQty"].ToString());


                if (dr["startTime"].ToString() == "")
                {
                    model.startTime = null;
                }else
                {
                    model.startTime = DateTime.Parse(dr["startTime"].ToString());
                }

                if (dr["stopTime"].ToString() == "")
                {
                    model.stopTime = null;
                }
                else
                {
                    model.stopTime = DateTime.Parse(dr["stopTime"].ToString());
                }

            
                model.PIC = dr["userID"].ToString();
                model.dateTime = DateTime.Parse(dr["dateTime"].ToString());


                //根据bom中process设定type
                var bomModel = (from a in bomList where a.partNumber == model.partNo select a).FirstOrDefault();
                //只有 有laser process 并且只有check#1的是 Online, 其余都是offline
                if (bomModel.processes.ToUpper().Contains("LASER") && (!bomModel.processes.ToUpper().Contains("CHECK#2")) && (!bomModel.processes.ToUpper().Contains("CHECK#3")))
                {
                    model.type = "Online";
                }
                else
                {
                    model.type = "Offline";
                }



                modelList.Add(model);
            }


            


            string[] typeArr = sType == "" ? new string[] { "Online", "Offline" } : new string[] { sType };

            var result = (from a in modelList
                          where (sLotNo == "" ? true : sLotNo == a.lotNo)
                          && typeArr.Contains(a.type)
                          && a.totalQty > 0
                          orderby a.dateTime ascending
                          select a).ToList();

            ViewModel.PackingDetail_ViewModel summaryModel = new ViewModel.PackingDetail_ViewModel();
            summaryModel.shift = "Total";
            summaryModel.okQty = result.Sum(p => p.okQty);
            summaryModel.ngQty = result.Sum(p => p.ngQty);
            summaryModel.totalQty = result.Sum(p => p.totalQty);
            result.Add(summaryModel);


            return result;
        }



        //checking detial list
        public List<ViewModel.CheckingDetail_ViewModel> GetCheckingDetailList(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sStation, string sPIC, string sJobNo, string sLotNo, string sType)
        {
            try
            {
                DataTable dt = viTrackingBLL.GetCheckingDetailList(dDateFrom, dDateTo, sPartNo, sStation, sPIC, sJobNo);
                if (dt == null || dt.Rows.Count == 0) return null;



                DataTable dtPaint = paintBLL.GetList(dDateFrom.AddMonths(-6), dDateTo, "");

                List<Common.Class.Model.PQCBom_Model> bomList = bomBLL.GetModelList();


                List<ViewModel.CheckingDetail_ViewModel> modelList = new List<ViewModel.CheckingDetail_ViewModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    ViewModel.CheckingDetail_ViewModel model = new ViewModel.CheckingDetail_ViewModel();
                    model.trackingID = dr["trackingID"].ToString();
                    model.day = DateTime.Parse(dr["day"].ToString());
                    model.shift = dr["shift"].ToString();
                    model.station = dr["machineID"].ToString();
                    model.partNo = dr["partnumber"].ToString();
                    model.processes = dr["processes"].ToString();
                    model.jobID = dr["jobId"].ToString();
                    //获取 lotno
                    DataRow[] tempDrArr = dtPaint.Select(" jobNumber = '" + dr["jobId"].ToString() + "'");
                    if (tempDrArr != null && tempDrArr.Count() != 0)
                        model.lotNo = tempDrArr[0]["lotNo"].ToString();

                    model.okQty = double.Parse(dr["acceptQty"].ToString());
                    model.ngQty = double.Parse(dr["rejectQty"].ToString());
                    model.totalQty = double.Parse(dr["totalQty"].ToString());


                    model.PIC = dr["userID"].ToString();
                    model.dateTime = DateTime.Parse(dr["dateTime"].ToString());


                    model.mouldRej = double.Parse(dr["mouldrej"].ToString());
                    model.paintRej = double.Parse(dr["paintRej"].ToString());
                    model.laserRej = double.Parse(dr["laserRej"].ToString());
                    model.othersRej = double.Parse(dr["othersRej"].ToString());


                    if (dr["startTime"].ToString() == "")
                    {
                        model.startTime = null;
                    }
                    else
                    {
                        model.startTime = DateTime.Parse(dr["startTime"].ToString());
                    }

                    if (dr["stopTime"].ToString() == "")
                    {
                        model.stopTime = null;
                    }
                    else
                    {
                        model.stopTime = DateTime.Parse(dr["stopTime"].ToString());
                    }



                    //根据bom中process设定type
                    var bomModel = (from a in bomList where a.partNumber == model.partNo select a).FirstOrDefault();
                    //只有 有laser process 并且只有check#1的是 Online, 其余都是offline
                    if (bomModel.processes.ToUpper().Contains("LASER") && (!bomModel.processes.ToUpper().Contains("CHECK#2")) && (!bomModel.processes.ToUpper().Contains("CHECK#3")))
                    {
                        model.type = "Online";
                    }
                    else
                    {
                        model.type = "Offline";
                    }


                    modelList.Add(model);
                }


                string[] typeArr = sType == "" ? new string[] { "Online", "Offline" } : new string[] { sType };
                List<ViewModel.CheckingDetail_ViewModel> temp = temp = (from a in modelList
                                                                        where typeArr.Contains(a.type) && a.totalQty > 0
                                                                        orderby a.dateTime ascending
                                                                        select a).ToList();




                ViewModel.CheckingDetail_ViewModel summaryModel = new ViewModel.CheckingDetail_ViewModel();
                summaryModel.shift = "Total";
                summaryModel.okQty = temp.Sum(p => p.okQty);
                summaryModel.ngQty = temp.Sum(p => p.ngQty);
                summaryModel.totalQty = temp.Sum(p => p.totalQty);

                summaryModel.mouldRej = temp.Sum(p => p.mouldRej);
                summaryModel.paintRej = temp.Sum(p => p.paintRej);
                summaryModel.laserRej = temp.Sum(p => p.laserRej);
                summaryModel.othersRej = temp.Sum(p => p.othersRej);



                temp.Add(summaryModel);
                return temp;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("CheckingDetailReport", "GetCheckingDetailList, catch exception " + ee.ToString());
                return null;
            }
        }
        
 



        #region packing chart 

        private List<ViewModel.PackingProductChart_ViewModel> GetPackingProductionData(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sStation, string sPIC, string sJobNo, string sLotNo, string sType)
        {
            DataTable dt = packTrackBLL.GetList(dDateFrom, dDateTo, "", sPartNo, sStation, sPIC, sJobNo);
            if (dt == null || dt.Rows.Count == 0) return null;


            List<Common.Class.Model.PQCBom_Model> bomList = bomBLL.GetModelList();



            List<ViewModel.PackingProductChart_ViewModel> modelList = new List<ViewModel.PackingProductChart_ViewModel>();
            foreach (DataRow dr in dt.Rows)
            {

                //根据bom中process设定type
                string jobType = "";
                var bomModel = (from a in bomList where a.partNumber == dr["partnumber"].ToString() select a).FirstOrDefault();
                if (bomModel == null)
                    continue;


                //只有 有laser process 并且只有check#1的是 Online, 其余都是offline
                if (bomModel.processes.ToUpper().Contains("LASER") && (!bomModel.processes.ToUpper().Contains("CHECK#2")))
                {
                    jobType = "Online";
                }
                else
                {
                    jobType = "Offline";
                }

                //如果选定  on/off line.  则判断不是当前记录不是 选定的type跳过.
                if (sType != "" && jobType != sType)
                    continue;





                ViewModel.PackingProductChart_ViewModel model = new ViewModel.PackingProductChart_ViewModel();


                DateTime day = DateTime.Parse(dr["day"].ToString());

                model.iYear = day.Year;
                model.iMonth = day.Month;
                model.iDay = day.Day;
                model.dDay = day;
                model.output = double.Parse(dr["totalQty"].ToString());
                model.op = dr["userID"].ToString().ToUpper();


                modelList.Add(model);
            }


            DateTime dTemp = dDateFrom.Date;
            while (dTemp < dDateTo)
            {
                var result = (from a in modelList where a.dDay == dTemp select a).ToList();
                if (result == null || result.Count == 0)
                {
                    ViewModel.PackingProductChart_ViewModel model = new ViewModel.PackingProductChart_ViewModel();
                    model.iYear = dTemp.Year;
                    model.iMonth = dTemp.Month;
                    model.iDay = dTemp.Day;
                    model.dDay = dTemp;
                    model.output = 0;
                    model.op = "";

                    modelList.Add(model);
                }



                dTemp = dTemp.AddDays(1);
            }


            return modelList;
        }



        public string GetPicList(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sStation, string sPIC, string sJobNo, string sType)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();


            List<ViewModel.PackingProductChart_ViewModel> packingChartDataList = GetPackingProductionData(dDateFrom, dDateTo, sPartNo, sStation, sPIC, sJobNo,"", sType);
            if (packingChartDataList == null)
            {
                return js.Serialize("");
            }
            

            var result = from a in packingChartDataList
                         where a.output != 0
                         group a by a.op into b
                         where b.Key != ""
                         orderby b.Key ascending
                         select new
                         {
                             op = b.Key,
                             output = b.Sum(p => p.output)
                         };

          
            if (result == null)
            {
                return js.Serialize("");
            }
            else
            {
                return js.Serialize(result);
            }
        }
        
        public string GetProductTrendList(string sGroupBy,DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sStation, string sPIC, string sType)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            

            List <ViewModel.PackingProductChart_ViewModel> packingChartDataList = GetPackingProductionData(dDateFrom, dDateTo, sPartNo, sStation, sPIC, "","", sType);
            if (packingChartDataList == null)
            {
                return js.Serialize("");
            }

       

            string result = "";

            switch (sGroupBy)
            {
                case "Year":
                    var result1 = from a in packingChartDataList
                                 group a by a.iYear into b
                                  orderby b.Key
                                  select new
                                 {
                                     year = b.Key,
                                     output = b.Sum(p => p.output)
                                 };

                    result = js.Serialize(result1);
                    break;

                case "Month":
                    var result2 = from a in packingChartDataList
                                 group a by a.iMonth into b
                                  orderby b.Key
                                  select new
                                 {
                                      month = Common.CommFunctions.GetMonthName(b.Key, false),
                                      output = b.Sum(p => p.output)
                                 };
                    result = js.Serialize(result2);
                    break;

                case "Day":
                    var result3 = from a in packingChartDataList
                                 group a by new { a.iMonth, a.iDay } into b
                                 orderby b.Key.iMonth ascending , b.Key.iDay ascending
                                 select new
                                 {
                                     day = b.Key.iDay,
                                     month = Common.CommFunctions.GetMonthName(b.Key.iMonth, false) ,
                                     output = b.Sum(p => p.output)
                                 };
                    result = js.Serialize(result3);
                    break;

                default:
                    break;
            };



            return result;
        }
        #endregion



        





        #region packing inventory
        
        public string GetPackingInventory(DateTime dDateFrom, string sPartNo)
        {
            try
            {
                List<ViewModel.PackingInventory_ViewModel.Detail> detailList = GetPackBinDetailList(dDateFrom, DateTime.Now, sPartNo, string.Empty);
                if (detailList == null)
                    return _js.Serialize("");


                List<Common.Class.Model.PaintingDeliveryHis_Model> paintList = paintBLL.GetModels("", dDateFrom.AddMonths(-6), DateTime.Now);

                //join paint delivery
                var detailJoinList = from a in detailList
                                     join b in paintList on a.jobID equals b.jobNumber
                                     select new
                                     {
                                         a.customer,
                                         a.model,
                                         a.partNo,
                                         a.jobID,
                                         lotQty = b.inQuantity,
                                         a.beforeQty,
                                         a.afterQty,
                                         mfgDate = b.dateTime,
                                         a.bomFlag,
                                         a.materialCount
                                     };

                //将material 按job group
                var groupByJobList = from a in detailJoinList
                                     group a by new { a.customer, a.model, a.partNo, a.jobID, a.mfgDate, a.lotQty, a.materialCount, a.bomFlag }
                                     into grouped
                                     select new
                                     {
                                         grouped.Key.customer,
                                         grouped.Key.model,
                                         grouped.Key.partNo,
                                         grouped.Key.jobID,

                                         lotQtyPCS = (double)grouped.Key.lotQty * grouped.Key.materialCount,
                                         lotQtySET = grouped.Key.lotQty,

                                         beforeQtyPCS = grouped.Sum(p => p.beforeQty),
                                         beforeQtySET = grouped.Min(p => p.beforeQty),

                                         afterQtyPCS = grouped.Sum(p => p.afterQty),
                                         afterQtySET = grouped.Min(p => p.afterQty),


                                         grouped.Key.mfgDate,
                                         grouped.Key.bomFlag
                                     };

                //将job 按照part group by.
                var groupByPartList = from a in groupByJobList
                                      orderby a.customer ascending, a.model ascending, a.partNo ascending
                                      group a by new { a.customer, a.model, a.partNo } into grouped
                                      where grouped.Sum(p => (double)p.lotQtyPCS - p.afterQtyPCS) > 0
                                      select new
                                      {
                                          grouped.Key.customer,
                                          grouped.Key.model,
                                          grouped.Key.partNo,

                                          jobID = grouped.Count() == 1 ? grouped.Max(p => p.jobID) : "JOT###",

                                          lotQty = string.Format("{0}({1})", grouped.Sum(p => p.lotQtySET), grouped.Sum(p => p.lotQtyPCS)),
                                          beforeQty = string.Format("{0}({1})", grouped.Sum(p => p.beforeQtySET), grouped.Sum(p => p.beforeQtyPCS)),
                                          afterQty = string.Format("{0}({1})", grouped.Sum(p => p.afterQtySET), grouped.Sum(p => p.afterQtyPCS)),

                                          jobCount = grouped.Count(),

                                          mfgDate = grouped.Count() == 1 ? grouped.Max(p => p.mfgDate.Value.ToString("dd/MM/yyyy")) : "",

                                          bomFlag = grouped.Max(p => p.bomFlag)

                                      };
                 
                return _js.Serialize(groupByPartList);
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("PackingInventory_Debug", "GetPackingInventory, catch exception :" + ee.ToString());
                return _js.Serialize("");
            }
        }
        
        public string GetPackingInventoryDetail(DateTime dDateFrom, DateTime dDateTo,  string sPartNo, string sJobNo)
        {
            try
            {
                List<ViewModel.PackingInventory_ViewModel.Detail> detailList = GetPackBinDetailList(dDateFrom, DateTime.Now, sPartNo, sJobNo);
                if (detailList == null)
                    return _js.Serialize("");


                List<Common.Class.Model.PaintingDeliveryHis_Model> paintList = paintBLL.GetModels("", dDateFrom.AddMonths(-6), DateTime.Now);

                //join paint delivery
                var detailJoinList = from a in detailList
                                     join b in paintList on a.jobID equals b.jobNumber
                                     select new
                                     {
                                         a.customer,
                                         a.model,
                                         a.partNo,
                                         a.jobID,
                                         lotQty = b.inQuantity,
                                         a.beforeQty,
                                         a.afterQty,
                                         mfgDate = b.dateTime,
                                         a.bomFlag,
                                         a.materialCount
                                     };

                //将material 按job group
                var groupByJobList = from a in detailJoinList
                                     group a by new { a.customer, a.model, a.partNo, a.jobID, a.mfgDate, a.lotQty, a.materialCount, a.bomFlag }
                                     into grouped
                                     select new
                                     {
                                         grouped.Key.customer,
                                         grouped.Key.model,
                                         grouped.Key.partNo,
                                         grouped.Key.jobID,

                                         lotQtyPCS = (double)grouped.Key.lotQty * grouped.Key.materialCount,
                                         lotQtySET = grouped.Key.lotQty,

                                         beforeQtyPCS = grouped.Sum(p => p.beforeQty),
                                         beforeQtySET = grouped.Min(p => p.beforeQty),

                                         afterQtyPCS = grouped.Sum(p => p.afterQty),
                                         afterQtySET = grouped.Min(p => p.afterQty),


                                         grouped.Key.mfgDate,
                                         grouped.Key.bomFlag
                                     };

                return _js.Serialize(groupByJobList);
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("PackingInventory_Debug", "GetPackingInventoryDetail, catch exception :" + ee.ToString());
                return _js.Serialize("");
            }
        }

        
        private List<ViewModel.PackingInventory_ViewModel.Detail> GetPackBinDetailList(DateTime dDateFrom, DateTime dDateTo,  string sPartNo, string sJobNo)
        {
            try
            {
                DataTable dt = packTrackBLL.GetPackInventoryDetailList(dDateFrom, dDateTo, sPartNo, sJobNo);
                if (dt == null || dt.Rows.Count == 0)
                    return null;


                List<ViewModel.PackingInventory_ViewModel.Detail> detailList = new List<ViewModel.PackingInventory_ViewModel.Detail>();
                foreach (DataRow dr in dt.Rows)
                {


                   

                    ViewModel.PackingInventory_ViewModel.Detail detialModel = new ViewModel.PackingInventory_ViewModel.Detail();
                  

                    detialModel.customer = dr["customer"].ToString();
                    detialModel.model = dr["model"].ToString();
                    detialModel.partNo = dr["PartNumber"].ToString();
                    detialModel.jobID = dr["jobid"].ToString();
                    detialModel.materialPartNo = dr["materialPartNo"].ToString();

                    DBHelp.Reports.LogFile.Log("PackingInventory_Debug", "job no:" + detialModel.jobID);


                    detialModel.beforeQty = double.Parse(dr["beforeQty"].ToString());
                    detialModel.afterQty = double.Parse(dr["afterQty"].ToString());

                    detialModel.materialCount = double.Parse(dr["materialCount"].ToString());
                    detialModel.bomFlag = dr["bomFlag"].ToString();


                    detailList.Add(detialModel);
                }


                return detailList;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("PackingInventory_Debug", "GetPackBinDetailList, catch exception :" + ee.ToString());
                return null;
            }           
        }
        

        #endregion




    }
}