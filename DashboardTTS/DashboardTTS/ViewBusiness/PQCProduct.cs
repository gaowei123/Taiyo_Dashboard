using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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





        #region summary report

        /// <summary>
        /// pqc summary report动态版本  --2020 08 14--
        /// job查询不做限制
        /// laser btn: 有laser process, 并且当前process为check1.  并且不包括num设定的小分类.
        /// wip btn: wip part 和有laser process并且当前process不是check1的job, 并且不包括num设定的小分类.
        /// num小分类: 取最后一道check process的数量,  并且按照bom中设置的num 动态汇总.
        /// packing: 分online, wip显示.
        ///     online: 同laser btn逻辑
        ///     wip: 同wip btn逻辑
        /// </summary>
        public List<ViewModel.PQCSummaryReport_ViewModel.Report> GetSummaryList(DateTime dDateFrom, DateTime dDateTo, string sShift, string sPartNo)
        {
            List<ViewModel.PQCSummaryReport_ViewModel.ViDetail> viDetailList = GetViDetailList(dDateFrom, dDateTo, sShift, sPartNo);
            if (viDetailList == null)
                return null;
            



            List<ViewModel.PQCSummaryReport_ViewModel.Report> reportList = new List<ViewModel.PQCSummaryReport_ViewModel.Report>();


            #region 添加 laser model
            var laserGroupList = from a in viDetailList
                                 where a.number == "" && a.isContainLaser == true && a.currentProcess.ToUpper() == "CHECK#1"
                                 select a;

            ViewModel.PQCSummaryReport_ViewModel.Report modelForLaser = new ViewModel.PQCSummaryReport_ViewModel.Report();
            modelForLaser.pqcDept = "Laser BTN";
            modelForLaser.totalOutput = laserGroupList.Sum(p => p.totalQty);
            modelForLaser.actualOutput = laserGroupList.Sum(p => p.acceptQty);
            modelForLaser.totalRej = laserGroupList.Sum(p => p.rejectQty);
            modelForLaser.ttsMouldRej = laserGroupList.Sum(p => p.ttsRej);
            modelForLaser.vendorsModelRej = laserGroupList.Sum(p => p.vendorRej);
            modelForLaser.paintRej = laserGroupList.Sum(p => p.paintRej);
            modelForLaser.laserRej = laserGroupList.Sum(p => p.laserRej);
            modelForLaser.othersRej = laserGroupList.Sum(p => p.othersRej);

            modelForLaser.totalRejRate = string.Format("{0}({1}%)", modelForLaser.totalRej, Math.Round(modelForLaser.totalRej / modelForLaser.totalOutput * 100, 2));
            modelForLaser.ttsMouldRejRate = string.Format("{0}({1}%)", modelForLaser.vendorsModelRej, Math.Round(modelForLaser.vendorsModelRej / modelForLaser.totalOutput * 100, 2));
            modelForLaser.vendorsModelRejRate = string.Format("{0}({1}%)", modelForLaser.ttsMouldRej, Math.Round(modelForLaser.ttsMouldRej / modelForLaser.totalOutput * 100, 2));
            modelForLaser.paintRejRate = string.Format("{0}({1}%)", modelForLaser.paintRej, Math.Round(modelForLaser.paintRej / modelForLaser.totalOutput * 100, 2));
            modelForLaser.laserRejRate = string.Format("{0}({1}%)", modelForLaser.laserRej, Math.Round(modelForLaser.laserRej / modelForLaser.totalOutput * 100, 2));
            modelForLaser.othersRejRate = string.Format("{0}({1}%)", modelForLaser.othersRej, Math.Round(modelForLaser.othersRej / modelForLaser.totalOutput * 100, 2));

            reportList.Add(modelForLaser);
            #endregion




            #region 添加 wip model
            var wipGroupList = from a in viDetailList
                               where a.number == "" && !(a.isContainLaser == true && a.currentProcess.ToUpper() == "CHECK#1")
                               select a;

            ViewModel.PQCSummaryReport_ViewModel.Report modelForWIP = new ViewModel.PQCSummaryReport_ViewModel.Report();
            modelForWIP.pqcDept = "WIP BTN";
            modelForWIP.totalOutput = wipGroupList.Sum(p => p.totalQty);
            modelForWIP.actualOutput = wipGroupList.Sum(p => p.acceptQty);
            modelForWIP.totalRej = wipGroupList.Sum(p => p.rejectQty);
            modelForWIP.ttsMouldRej = wipGroupList.Sum(p => p.ttsRej);
            modelForWIP.vendorsModelRej = wipGroupList.Sum(p => p.vendorRej);
            modelForWIP.paintRej = wipGroupList.Sum(p => p.paintRej);
            modelForWIP.laserRej = wipGroupList.Sum(p => p.laserRej);
            modelForWIP.othersRej = wipGroupList.Sum(p => p.othersRej);

            modelForWIP.totalRejRate = string.Format("{0}({1}%)", modelForWIP.totalRej, Math.Round(modelForWIP.totalRej / modelForWIP.totalOutput * 100, 2));
            modelForWIP.ttsMouldRejRate = string.Format("{0}({1}%)", modelForWIP.vendorsModelRej, Math.Round(modelForWIP.vendorsModelRej / modelForWIP.totalOutput * 100, 2));
            modelForWIP.vendorsModelRejRate = string.Format("{0}({1}%)", modelForWIP.ttsMouldRej, Math.Round(modelForWIP.ttsMouldRej / modelForWIP.totalOutput * 100, 2));
            modelForWIP.paintRejRate = string.Format("{0}({1}%)", modelForWIP.paintRej, Math.Round(modelForWIP.paintRej / modelForWIP.totalOutput * 100, 2));
            modelForWIP.laserRejRate = string.Format("{0}({1}%)", modelForWIP.laserRej, Math.Round(modelForWIP.laserRej / modelForWIP.totalOutput * 100, 2));
            modelForWIP.othersRejRate = string.Format("{0}({1}%)", modelForWIP.othersRej, Math.Round(modelForWIP.othersRej / modelForWIP.totalOutput * 100, 2));


            reportList.Add(modelForWIP);
            #endregion

            


            #region  按num汇总
            var numGroupList = from a in viDetailList
                               where a.number != "" && a.lastCheckProcess == a.currentProcess//取最后一道check process
                               group a by new { a.number, a.description, a.lastCheckProcess} into groupList
                               select new
                               {
                                   groupList.Key.number,
                                   groupList.Key.description,
                                   output = groupList.Sum(p => p.totalQty),
                                   passQty = groupList.Sum(p => p.acceptQty),
                                   rejQty = groupList.Sum(p => p.rejectQty),

                                   ttsRej = groupList.Sum(p=>p.ttsRej),
                                   vendorRej = groupList.Sum(p => p.vendorRej),
                                   paintRej = groupList.Sum(p => p.paintRej),
                                   laserRej = groupList.Sum(p => p.laserRej),
                                   othersRej = groupList.Sum(p => p.othersRej)
                               };


          
            //获取bom中所有的number
            List<string> numList = bomBLL.GetNumberList();

            //添加 num model
            foreach (string num in numList)
            {
                var numModel = (from a in numGroupList where a.number == num select a).FirstOrDefault();
                if (numModel == null)
                    continue;


                ViewModel.PQCSummaryReport_ViewModel.Report modelForNum = new ViewModel.PQCSummaryReport_ViewModel.Report();
                modelForNum.pqcDept = numModel.description + numModel.number;
                modelForNum.totalOutput = numModel.output;
                modelForNum.actualOutput = numModel.passQty;
                modelForNum.totalRej = numModel.rejQty;
                modelForNum.ttsMouldRej = numModel.ttsRej;
                modelForNum.vendorsModelRej = numModel.vendorRej;
                modelForNum.paintRej = numModel.paintRej;
                modelForNum.laserRej = numModel.laserRej;
                modelForNum.othersRej = numModel.laserRej;
                         
                modelForNum.totalRejRate = string.Format("{0}({1}%)", modelForNum.totalRej, Math.Round(modelForNum.totalRej / modelForNum.totalOutput * 100, 2));
                modelForNum.ttsMouldRejRate = string.Format("{0}({1}%)", modelForNum.vendorsModelRej, Math.Round(modelForNum.vendorsModelRej / modelForNum.totalOutput * 100, 2));
                modelForNum.vendorsModelRejRate = string.Format("{0}({1}%)", modelForNum.ttsMouldRej, Math.Round(modelForNum.ttsMouldRej / modelForNum.totalOutput * 100, 2));
                modelForNum.paintRejRate = string.Format("{0}({1}%)", modelForNum.paintRej, Math.Round(modelForNum.paintRej / modelForNum.totalOutput * 100, 2));
                modelForNum.laserRejRate = string.Format("{0}({1}%)", modelForNum.laserRej, Math.Round(modelForNum.laserRej / modelForNum.totalOutput * 100, 2));
                modelForNum.othersRejRate = string.Format("{0}({1}%)", modelForNum.othersRej, Math.Round(modelForNum.othersRej / modelForNum.totalOutput * 100, 2));

                
                reportList.Add(modelForNum);
            }
            #endregion




            #region checking total
            ViewModel.PQCSummaryReport_ViewModel.Report checkTotalModel = new ViewModel.PQCSummaryReport_ViewModel.Report();
            checkTotalModel.pqcDept = "Checking Total";
            checkTotalModel.totalOutput = reportList.Sum(p => p.totalOutput);
            checkTotalModel.actualOutput = reportList.Sum(p => p.actualOutput);
            checkTotalModel.totalRej = reportList.Sum(p => p.totalRej);
            checkTotalModel.ttsMouldRej = reportList.Sum(p => p.ttsMouldRej);
            checkTotalModel.vendorsModelRej = reportList.Sum(p => p.vendorsModelRej);
            checkTotalModel.paintRej = reportList.Sum(p => p.paintRej);
            checkTotalModel.laserRej = reportList.Sum(p => p.laserRej);
            checkTotalModel.othersRej = reportList.Sum(p => p.othersRej);

            checkTotalModel.totalRejRate = string.Format("{0}({1}%)", checkTotalModel.totalRej, Math.Round(checkTotalModel.totalRej / checkTotalModel.totalOutput * 100, 2));
            checkTotalModel.ttsMouldRejRate = string.Format("{0}({1}%)", checkTotalModel.vendorsModelRej, Math.Round(checkTotalModel.vendorsModelRej / checkTotalModel.totalOutput * 100, 2));
            checkTotalModel.vendorsModelRejRate = string.Format("{0}({1}%)", checkTotalModel.ttsMouldRej, Math.Round(checkTotalModel.ttsMouldRej / checkTotalModel.totalOutput * 100, 2));
            checkTotalModel.paintRejRate = string.Format("{0}({1}%)", checkTotalModel.paintRej, Math.Round(checkTotalModel.paintRej / checkTotalModel.totalOutput * 100, 2));
            checkTotalModel.laserRejRate = string.Format("{0}({1}%)", checkTotalModel.laserRej, Math.Round(checkTotalModel.laserRej / checkTotalModel.totalOutput * 100, 2));
            checkTotalModel.othersRejRate = string.Format("{0}({1}%)", checkTotalModel.othersRej, Math.Round(checkTotalModel.othersRej / checkTotalModel.totalOutput * 100, 2));
            
            reportList.Add(checkTotalModel);
            #endregion




            //packing online, offline, total 
            return AddPackList(dDateFrom, dDateTo, sShift, sPartNo, reportList);
        }


      
        private List<ViewModel.PQCSummaryReport_ViewModel.ViDetail> GetViDetailList(DateTime dDateFrom, DateTime dDateTo, string sShift, string sPartNo)
        {
            Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();
            DataTable dt = bll.GetSummaryReport(dDateFrom, dDateTo, sShift, sPartNo);
            if (dt == null || dt.Rows.Count == 0)
                return null;



            List<ViewModel.PQCSummaryReport_ViewModel.ViDetail> defectList = new List<ViewModel.PQCSummaryReport_ViewModel.ViDetail>();
            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.PQCSummaryReport_ViewModel.ViDetail model = new ViewModel.PQCSummaryReport_ViewModel.ViDetail();
                model.trackingID = dr["trackingID"].ToString();
                model.partNo = dr["partNumber"].ToString();
                model.totalQty = double.Parse(dr["TotalQty"].ToString());
                model.acceptQty = double.Parse(dr["acceptQty"].ToString());
                model.rejectQty = double.Parse(dr["rejectQty"].ToString());
                model.currentProcess = dr["currentProcess"].ToString();

                model.description = dr["description"].ToString();
                model.number = dr["number"].ToString();
                model.isContainLaser = bool.Parse(dr["isContainLaser"].ToString());
                model.lastCheckProcess = dr["lastCheckProcess"].ToString();

                model.ttsRej = double.Parse(dr["ttsRej"].ToString());
                model.vendorRej = double.Parse(dr["vendorRej"].ToString());
                model.paintRej = double.Parse(dr["paintRej"].ToString());
                model.laserRej = double.Parse(dr["laserRej"].ToString());
                model.othersRej = double.Parse(dr["othersRej"].ToString());            

                defectList.Add(model);
            }


            return defectList;
        }


        /// <summary>
        /// 生成pqc summary report对应packing的3条信息
        /// online: process只有laser,check#1
        /// offline: process没有laser, 或者有laser并且有check#2,3的.
        /// </summary>     
        private List<ViewModel.PQCSummaryReport_ViewModel.Report> AddPackList(DateTime dDateFrom, DateTime dDateTo, string sShift, string sPartNo, List<ViewModel.PQCSummaryReport_ViewModel.Report> reportList)
        {

            ViewModel.PQCSummaryReport_ViewModel.Report packOnlineModel = new ViewModel.PQCSummaryReport_ViewModel.Report();
            packOnlineModel.pqcDept = "Packing Online";       
            packOnlineModel.ttsMouldRejRate = "-";
            packOnlineModel.vendorsModelRejRate = "-";
            packOnlineModel.paintRejRate = "-";
            packOnlineModel.laserRejRate = "-";
            packOnlineModel.othersRejRate = "-";
            packOnlineModel.totalOutput = 0;
            packOnlineModel.totalRejRate = "0(0.00%)";
            packOnlineModel.actualOutput = 0;

            ViewModel.PQCSummaryReport_ViewModel.Report packOfflineModel = new ViewModel.PQCSummaryReport_ViewModel.Report();
            packOfflineModel.pqcDept = "Packing Offline";
            packOfflineModel.ttsMouldRejRate = "-";
            packOfflineModel.vendorsModelRejRate = "-";
            packOfflineModel.paintRejRate = "-";
            packOfflineModel.laserRejRate = "-";
            packOfflineModel.othersRejRate = "-";
            packOfflineModel.totalOutput = 0;
            packOfflineModel.totalRejRate = "0(0.00%)";
            packOfflineModel.actualOutput = 0;

            ViewModel.PQCSummaryReport_ViewModel.Report packTotalModel = new ViewModel.PQCSummaryReport_ViewModel.Report();
            packTotalModel.pqcDept = "Packing Total";
            packTotalModel.ttsMouldRejRate = "-";
            packTotalModel.vendorsModelRejRate = "-";
            packTotalModel.paintRejRate = "-";
            packTotalModel.laserRejRate = "-";
            packTotalModel.othersRejRate = "-";
            packTotalModel.totalOutput = 0;
            packTotalModel.totalRejRate = "0(0.00%)";
            packTotalModel.actualOutput = 0;


           


            DataTable dt = packTrackBLL.GetPackForSummaryReport(dDateFrom, dDateTo, sShift, sPartNo);
            if (dt != null && dt.Rows.Count != 0)
            {
                double totalQty = 0;
                double totalPass = 0;
                double totalRej = 0;
                DataRow drOnline;
                DataRow drOffline;

                DataRow[] temp = dt.Select(" packType = 'Online'", "");
                if (temp != null && temp.Count() != 0)
                {
                    drOnline = temp[0];
                    totalQty = double.Parse(drOnline["TotalQty"].ToString());
                    totalPass = double.Parse(drOnline["acceptQty"].ToString());
                    totalRej = double.Parse(drOnline["rejectQty"].ToString());

                    packOnlineModel.totalOutput = totalQty;
                    packOnlineModel.actualOutput = totalPass;
                    packOnlineModel.totalRejRate = string.Format("{0}({1}%)", totalRej, Math.Round(totalRej / totalQty * 100, 2));

                }


                temp = dt.Select(" packType = 'Offline'", "");
                if (temp != null && temp.Count() != 0)
                {
                    drOffline = temp[0];
                    totalQty = double.Parse(drOffline["TotalQty"].ToString());
                    totalPass = double.Parse(drOffline["acceptQty"].ToString());
                    totalRej = double.Parse(drOffline["rejectQty"].ToString());

                    packOfflineModel.totalOutput = totalQty;
                    packOfflineModel.actualOutput = totalPass;
                    packOfflineModel.totalRejRate = string.Format("{0}({1}%)", totalRej, Math.Round(totalRej / totalQty * 100, 2));
                }


                totalQty = double.Parse(dt.Compute("sum(TotalQty)", "").ToString());
                totalPass = double.Parse(dt.Compute("sum(acceptQty)", "").ToString());
                totalRej = double.Parse(dt.Compute("sum(rejectQty)", "").ToString());

                packTotalModel.totalOutput = totalQty;
                packTotalModel.actualOutput = totalPass;
                packTotalModel.totalRejRate = string.Format("{0}({1}%)", totalRej, Math.Round(totalRej / totalQty * 100, 2));
                
            }

            reportList.Add(packOnlineModel);
            reportList.Add(packOfflineModel);
            reportList.Add(packTotalModel);
            


            return reportList;
        }

        #endregion



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



                

                model.pic = dr["PIC"].ToString();




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


                model.pic = dr["PIC"].ToString();




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


        public List<ViewModel.PQCCheckingMaintenance_ViewModel.MaterialInfo> GetMaterialList(string sTrackingID)
        {
            DataTable dt = viDetailBLL.GetList(sTrackingID);
            if (dt == null || dt.Rows.Count == 0)
                return null;


            List<ViewModel.PQCCheckingMaintenance_ViewModel.MaterialInfo> modelList = new List<ViewModel.PQCCheckingMaintenance_ViewModel.MaterialInfo>();


            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.PQCCheckingMaintenance_ViewModel.MaterialInfo model = new ViewModel.PQCCheckingMaintenance_ViewModel.MaterialInfo();
                model.materialNo = dr["materialPartNo"].ToString();
                model.passQty = double.Parse(dr["passQty"].ToString());
                model.rejQty = double.Parse(dr["rejectQty"].ToString());


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


            List<ViewModel.PQCCheckingMaintenance_ViewModel.DefectInfo> modelList = new List<ViewModel.PQCCheckingMaintenance_ViewModel.DefectInfo>();


            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.PQCCheckingMaintenance_ViewModel.DefectInfo model = new ViewModel.PQCCheckingMaintenance_ViewModel.DefectInfo();

                model.materialNo = dr["materialPartNo"].ToString();
                model.defectCodeID = int.Parse(dr["defectCodeID"].ToString());
                model.defectCode = dr["defectCode"].ToString();
                model.defectDescription = dr["defectDescription"].ToString();
                model.rejQty = double.Parse(dr["rejectQty"].ToString());


                modelList.Add(model);
            }



            var materialList = (from a in modelList where a.materialNo == sMaterialNo orderby a.materialNo ascending, a.defectCodeID ascending select a).ToList();


            return materialList;
        }


        public bool UpdateQty( System.Collections.Specialized.NameValueCollection formParameters)
        {

            string trackingID = formParameters["TrackingID"];


            //关联获取 material sn
            List<ViewModel.PQCCheckingMaintenance_ViewModel.MaterialInfo> materialList = GetMaterialList(trackingID);


            



            //处理 defect list
            List<Common.Class.Model.PQCQaViDefectTracking_Model> defectTrackingList = viDefectBLL.GetModelList(trackingID);
            foreach (Common.Class.Model.PQCQaViDefectTracking_Model model in defectTrackingList)
            {
                //matrial sn,  defectCode id
                int materialSN = (from a in materialList where a.materialNo == model.materialPartNo select a).FirstOrDefault().sn;


                //前台id自动命名规则,   txtid_SN_CodeID   获取提交的数量
                decimal rejQty = decimal.Parse(formParameters["txtid_" + materialSN.ToString() + "_" + model.defectCodeID]);


                model.rejectQty = rejQty;
                model.updatedTime = DateTime.Now;
                model.lastUpdatedTime = DateTime.Now;
                model.remarks = "PQC Job Maintenance Update Qty";
            }








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
        public string GetWIPInventory(string sPartNo)
        {

            DateTime dateFrom = DateTime.Parse("2020-6-1");
            DateTime dateTo = DateTime.Now.AddDays(1);

            List<ViewModel.PQCWIPInventory_ViewModel> modelList = GetWIPList(dateFrom, dateTo, sPartNo, "","");



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

                                     mfgDate = (summary.Sum(p => p.jobCount) == 1 ? summary.Max(p => p.mfgDate) : null),
                                     

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




        #endregion





        #region pqc operator output report

        public List<ViewModel.PQCOperatorDailyReport> GetDailyOperatorList(DateTime dDate, string sShift, string sUserID)
        {
            try
            {
                List<ViewModel.PQCOperatorDailyReport> reportList = new List<ViewModel.PQCOperatorDailyReport>();



                //get checking info
                DataTable dt = viTrackingBLL.GetDailyOperatorList(dDate, sShift, sUserID);
                if (dt == null || dt.Rows.Count == 0)
                    return null;


                foreach (DataRow dr in dt.Rows)
                {
                    ViewModel.PQCOperatorDailyReport model = new ViewModel.PQCOperatorDailyReport();


                    model.jobID = dr["jobID"].ToString();

                    if (string.IsNullOrEmpty(dr["startTime"].ToString()))
                        model.startTime = null;
                    else
                        model.startTime = DateTime.Parse(dr["startTime"].ToString());

                    if (string.IsNullOrEmpty(dr["stopTime"].ToString()))
                        model.endTime = null;
                    else
                        model.endTime = DateTime.Parse(dr["stopTime"].ToString());


                    if (model.startTime != null && model.endTime != null)
                    {
                        double totalSeconds = (model.endTime.Value - model.startTime.Value).TotalSeconds;
                        model.operatedTime = Common.CommFunctions.ConvertDateTimeShort((totalSeconds / 3600).ToString());
                    }

                    model.partNo = dr["partNumber"].ToString();
                    model.process = dr["Process"].ToString();
                    model.mouldRej = double.Parse(dr["MouldRej"].ToString());
                    model.paintRej = double.Parse(dr["PaintRej"].ToString());
                    model.laserRej = double.Parse(dr["LaserRej"].ToString());
                    model.othersRej = double.Parse(dr["OthersRej"].ToString());
                    model.totalRej = double.Parse(dr["rejectQty"].ToString());
                    model.passQty = double.Parse(dr["acceptQty"].ToString());
                    model.rejPrice = double.Parse(dr["rejPrice"].ToString());
                    model.userID = dr["userID"].ToString();

                    model.materialCount = int.Parse(dr["materialCount"].ToString());

                    reportList.Add(model);
                }


                //merge packing info
                DataTable dtPack = packTrackBLL.GetDailyOperatorList(dDate, sShift, sUserID);
                foreach (DataRow dr in dtPack.Rows)
                {
                    ViewModel.PQCOperatorDailyReport model = new ViewModel.PQCOperatorDailyReport();

                    model.jobID = dr["jobID"].ToString();

                    if (string.IsNullOrEmpty(dr["startTime"].ToString()))
                        model.startTime = null;
                    else
                        model.startTime = DateTime.Parse(dr["startTime"].ToString());

                    if (string.IsNullOrEmpty(dr["stopTime"].ToString()))
                        model.endTime = null;
                    else
                        model.endTime = DateTime.Parse(dr["stopTime"].ToString());


                    if (model.startTime != null && model.endTime != null)
                    {
                        double totalSeconds = (model.endTime.Value - model.startTime.Value).TotalSeconds;
                        model.operatedTime = Common.CommFunctions.ConvertDateTimeShort((totalSeconds / 3600).ToString());
                    }

                    model.partNo = dr["partNumber"].ToString();
                    model.process = dr["Process"].ToString();
                    model.totalRej = double.Parse(dr["rejectQty"].ToString());
                    model.passQty = double.Parse(dr["acceptQty"].ToString());
                    model.rejPrice = double.Parse(dr["rejPrice"].ToString());
                    model.userID = dr["userID"].ToString();



                    model.materialCount = int.Parse(dr["materialCount"].ToString());

                    reportList.Add(model);
                }


                string strJobIn = "(";
                foreach (var model in reportList)
                {
                    strJobIn += string.Format("'{0}',", model.jobID);
                }
                strJobIn = strJobIn.Substring(0, strJobIn.Length - 1);
                strJobIn += ")";



                DataTable dtPaintDelivery = paintBLL.GetPaintDeliveryForButtonReport_NEW(strJobIn);


                foreach (var model in reportList)
                {
                    DataRow[] drArrTemp = dtPaintDelivery.Select(" jobNumber = '" + model.jobID + "'  and  paintProcess = 'Paint#1'");

                    if (drArrTemp != null && drArrTemp.Count() != 0)
                    {
                        model.lotNo = drArrTemp[0]["lotNo"].ToString();
                        model.lotQty = double.Parse(drArrTemp[0]["MrpQty"].ToString()) * model.materialCount;

                        model.totalRejDisplay = string.Format("{0}({1}%)", model.totalRej, Math.Round(model.totalRej / model.lotQty * 100, 2));
                        model.mouldRejDisplay = string.Format("{0}({1}%)", model.mouldRej, Math.Round(model.mouldRej / model.lotQty * 100, 2));
                        model.paintRejDisplay = string.Format("{0}({1}%)", model.paintRej, Math.Round(model.paintRej / model.lotQty * 100, 2));
                        model.laserRejDisplay = string.Format("{0}({1}%)", model.laserRej, Math.Round(model.laserRej / model.lotQty * 100, 2));
                        model.othersRejDisplay = string.Format("{0}({1}%)", model.othersRej, Math.Round(model.othersRej / model.lotQty * 100, 2));

                    }
                }

                reportList = reportList.OrderBy(p => p.startTime).ToList();


                ViewModel.PQCOperatorDailyReport summaryModel = new ViewModel.PQCOperatorDailyReport();

                //summaryModel.operatedTime = Common.CommFunctions.ConvertDateTimeShort((reportList.Sum(p => (p.endTime.Value - p.startTime.Value).TotalSeconds) / 3600).ToString());
                summaryModel.lotQty = (from a in reportList
                                       group a by new { a.jobID, a.process } into b
                                       select new
                                       {
                                           lotQty = b.Sum(p => p.lotQty)
                                       }).Sum(p => p.lotQty);
                summaryModel.mouldRejDisplay = string.Format("{0}({1}%)", reportList.Sum(p => p.mouldRej), Math.Round(reportList.Sum(p => p.mouldRej) / summaryModel.lotQty * 100, 2));
                summaryModel.paintRejDisplay = string.Format("{0}({1}%)", reportList.Sum(p => p.paintRej), Math.Round(reportList.Sum(p => p.paintRej) / summaryModel.lotQty * 100, 2));
                summaryModel.laserRejDisplay = string.Format("{0}({1}%)", reportList.Sum(p => p.laserRej), Math.Round(reportList.Sum(p => p.laserRej) / summaryModel.lotQty * 100, 2));
                summaryModel.othersRejDisplay = string.Format("{0}({1}%)", reportList.Sum(p => p.othersRej), Math.Round(reportList.Sum(p => p.othersRej) / summaryModel.lotQty * 100, 2));
                summaryModel.totalRejDisplay = string.Format("{0}({1}%)", reportList.Sum(p => p.totalRej), Math.Round(reportList.Sum(p => p.totalRej) / summaryModel.lotQty * 100, 2));
                summaryModel.passQty = reportList.Sum(p => p.passQty);
                summaryModel.rejPrice = reportList.Sum(p => p.rejPrice);
                summaryModel.operatedTime = Common.CommFunctions.ConvertDateTimeShort(reportList.Sum(p => Common.CommFunctions.ConvertDateTimeToDouble(p.operatedTime)).ToString());


                reportList.Add(summaryModel);


                return reportList;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("OperatorDailyOutput_Debug", "GetDailyOperatorList catch exception:" + ee.ToString());
                return null;
            }


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