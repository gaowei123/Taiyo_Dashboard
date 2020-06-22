using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
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




        public PQCProduct()
        {

        }





        #region summary report

        public List<ViewModel.PQCSummaryReport_ViewModel.Report> GetSummaryList(DateTime dDateFrom, DateTime dDateTo, string sShift, string sPartNo)
        {
            List<ViewModel.PQCSummaryReport_ViewModel.ViDetail> viDetailList = GetViDetailList(dDateFrom, dDateTo, sShift, sPartNo);


            if (viDetailList == null)
                return null;


            double totalOutput = 0;
            double totalActualOutput = 0;
            double totalRej = 0;
            double totalTTSRej = 0;
            double totalVendorRej = 0;
            double totalPaintRej = 0;
            double totalLaserRej = 0;
            double totalOthersRej = 0;






            //Summary Report     job 不做限制

            //laser btn
                //1.process有laser工序, 并且当前工序是Check#1
                //2.其中452, 830, 831(只有check#1的小分类) 不算在laser btn, 独立显示
                //3. 833, 784, 824(有check#2的小分类),  check#1的数量计算在laser btn中.
		

            //wip btn
                //1.process没有laser工序.
                //2.process有laser工序, 并且当前的工序是Check#2,3.
                //3.其中833, 784, 824 有check#2的小分类,  check#2的数量 独立显示. 
		

            //452, 830, 831, 656, 595(只有Check#1的小分类)
                //1.独立显示, 均不算在laser, wip中.


            //833, 784, 824(有check#2的小分类)
                //1.Check#1算在laser btn中, 不独立显示.      
                //2.Check#2独立显示, 均不算在laser,wip中.
                

            List <ViewModel.PQCSummaryReport_ViewModel.Report> reportList = new List<ViewModel.PQCSummaryReport_ViewModel.Report>();

            #region laser btn
            var laserBtnList = from a in viDetailList
                               where a.allProcess.Split('-').Contains("Laser")  //必须是包含laser工序
                               && a.checkProcess == "CHECK#1"//并且当前工序必须是check#1
                               //&& (!(new string[] { "784", "824", "833", "452", "595", "656", "830", "831" }).Contains(a.number))//并且不包括784,824....等小分类


                               && (!(new string[] {  "452", "656","830", "831", "869"}).Contains(a.number))//452, 656, 830, 831,869等只有check#1的小分类, 独立显示不计入 laser btn
                                                                                                     //784, 833, 824等有check#2的小分类, check#1记入laser btn不独立显示.
                               select a;




       


            ViewModel.PQCSummaryReport_ViewModel.Report laserBtnModel = new ViewModel.PQCSummaryReport_ViewModel.Report();
            laserBtnModel.pqcDept = "Laser BTN";
            laserBtnModel.totalOutput = laserBtnList.Sum(p => p.totalQty);
            laserBtnModel.actualOutput = laserBtnList.Sum(p => p.acceptQty);
            laserBtnModel.totalRej = laserBtnList.Sum(p => p.totalRej);
            laserBtnModel.ttsMouldRej = laserBtnList.Sum(p => p.ttsRej);
            laserBtnModel.vendorsModelRej = laserBtnList.Sum(p => p.vendorRej);
            laserBtnModel.paintRej = laserBtnList.Sum(p => p.paintRej);
            laserBtnModel.laserRej = laserBtnList.Sum(p => p.laserRej);
            laserBtnModel.othersRej = laserBtnList.Sum(p => p.othersRej);

            laserBtnModel.totalRejRate = string.Format("{0}({1}%)", laserBtnModel.totalRej, Math.Round(laserBtnModel.totalRej / laserBtnModel.totalOutput * 100, 2).ToString("0.00"));
            laserBtnModel.ttsMouldRejRate = string.Format("{0}({1}%)", laserBtnModel.ttsMouldRej, Math.Round(laserBtnModel.ttsMouldRej / laserBtnModel.totalOutput * 100, 2).ToString("0.00"));
            laserBtnModel.vendorsModelRejRate = string.Format("{0}({1}%)", laserBtnModel.vendorsModelRej, Math.Round(laserBtnModel.vendorsModelRej / laserBtnModel.totalOutput * 100, 2).ToString("0.00"));
            laserBtnModel.paintRejRate = string.Format("{0}({1}%)", laserBtnModel.paintRej, Math.Round(laserBtnModel.paintRej / laserBtnModel.totalOutput * 100, 2).ToString("0.00"));
            laserBtnModel.laserRejRate = string.Format("{0}({1}%)", laserBtnModel.laserRej, Math.Round(laserBtnModel.laserRej / laserBtnModel.totalOutput * 100, 2).ToString("0.00"));
            laserBtnModel.othersRejRate = string.Format("{0}({1}%)", laserBtnModel.othersRej, Math.Round(laserBtnModel.othersRej / laserBtnModel.totalOutput * 100, 2).ToString("0.00"));

            reportList.Add(laserBtnModel);

            totalOutput += laserBtnModel.totalOutput;
            totalActualOutput += laserBtnModel.actualOutput;
            totalRej += laserBtnModel.totalRej;
            totalTTSRej += laserBtnModel.ttsMouldRej;
            totalVendorRej += laserBtnModel.vendorsModelRej;
            totalPaintRej += laserBtnModel.paintRej;
            totalLaserRej += laserBtnModel.laserRej;
            totalOthersRej += laserBtnModel.othersRej;

            #endregion

            #region wip btn
            var temp = from a in viDetailList
                       where (!a.allProcess.Split('-').Contains("Laser"))//没有laser工序
                       || (a.allProcess.Split('-').Contains("Laser") && a.allProcess.Split('-').Contains("Check#2") && a.checkProcess == "CHECK#2")//或者有laser工序并且有check2工序, 并且当前工序是check#2
                       || (a.allProcess.Split('-').Contains("Laser") && a.allProcess.Split('-').Contains("Check#3") && a.checkProcess == "CHECK#3")//或者有laser工序并且有check3工序, 并且当前工序是check#3
                       select a;

            var wipBtnList = from a in temp
                             //where (!(new string[] { "784", "833", "824", "595", "656", "830", "831" }).Contains(a.number))//并且不包括784,824....等小分类
                             where (!(new string[] { "784", "833", "824" }).Contains(a.number))//784, 833,824等有check#2的, check#2独立显示, 不计入wip中.
                             select a;



            ViewModel.PQCSummaryReport_ViewModel.Report wipBtnModel = new ViewModel.PQCSummaryReport_ViewModel.Report();
            wipBtnModel.pqcDept = "WIP BTN";
            wipBtnModel.totalOutput = wipBtnList.Sum(p => p.totalQty);
            wipBtnModel.actualOutput = wipBtnList.Sum(p => p.acceptQty);
            wipBtnModel.totalRej = wipBtnList.Sum(p => p.totalRej);
            wipBtnModel.ttsMouldRej = wipBtnList.Sum(p => p.ttsRej);
            wipBtnModel.vendorsModelRej = wipBtnList.Sum(p => p.vendorRej);
            wipBtnModel.paintRej = wipBtnList.Sum(p => p.paintRej);
            wipBtnModel.laserRej = wipBtnList.Sum(p => p.laserRej);
            wipBtnModel.othersRej = wipBtnList.Sum(p => p.othersRej);

            wipBtnModel.totalRejRate = string.Format("{0}({1})", wipBtnModel.totalRej, Math.Round(wipBtnModel.totalRej / wipBtnModel.totalOutput * 100, 2).ToString("0.00") + "%");
            wipBtnModel.ttsMouldRejRate = string.Format("{0}({1})", wipBtnModel.ttsMouldRej, Math.Round(wipBtnModel.ttsMouldRej / wipBtnModel.totalOutput * 100, 2).ToString("0.00") + "%");
            wipBtnModel.vendorsModelRejRate = string.Format("{0}({1})", wipBtnModel.vendorsModelRej, Math.Round(wipBtnModel.vendorsModelRej / wipBtnModel.totalOutput * 100, 2).ToString("0.00") + "%");
            wipBtnModel.paintRejRate = string.Format("{0}({1})", wipBtnModel.paintRej, Math.Round(wipBtnModel.paintRej / wipBtnModel.totalOutput * 100, 2).ToString("0.00") + "%");
            wipBtnModel.laserRejRate = string.Format("{0}({1})", wipBtnModel.laserRej, Math.Round(wipBtnModel.laserRej / wipBtnModel.totalOutput * 100, 2).ToString("0.00") + "%");
            wipBtnModel.othersRejRate = string.Format("{0}({1})", wipBtnModel.othersRej, Math.Round(wipBtnModel.othersRej / wipBtnModel.totalOutput * 100, 2).ToString("0.00") + "%");

            reportList.Add(wipBtnModel);

            totalOutput += wipBtnModel.totalOutput;
            totalActualOutput += wipBtnModel.actualOutput;
            totalRej += wipBtnModel.totalRej;
            totalTTSRej += wipBtnModel.ttsMouldRej;
            totalVendorRej += wipBtnModel.vendorsModelRej;
            totalPaintRej += wipBtnModel.paintRej;
            totalLaserRej += wipBtnModel.laserRej;
            totalOthersRej += wipBtnModel.othersRej;
            #endregion


            #region tks 784
            var tks784List = from a in viDetailList
                             where a.number == "784" && a.checkProcess == "CHECK#2"//有check#2的小分类, 只独立显示check#2的数量, check#1算入laserbtn中.
                             select a;


            ViewModel.PQCSummaryReport_ViewModel.Report tks784Model = new ViewModel.PQCSummaryReport_ViewModel.Report();
            tks784Model.pqcDept = "SBW TKS784";
            tks784Model.totalOutput = tks784List.Sum(p => p.totalQty);
            tks784Model.actualOutput = tks784List.Sum(p => p.acceptQty);
            tks784Model.totalRej = tks784List.Sum(p => p.totalRej);
            tks784Model.ttsMouldRej = tks784List.Sum(p => p.ttsRej);
            tks784Model.vendorsModelRej = tks784List.Sum(p => p.vendorRej);
            tks784Model.paintRej = tks784List.Sum(p => p.paintRej);
            tks784Model.laserRej = tks784List.Sum(p => p.laserRej);
            tks784Model.othersRej = tks784List.Sum(p => p.othersRej);

            tks784Model.totalRejRate = string.Format("{0}({1}%)", tks784Model.totalRej, Math.Round(tks784Model.totalRej / tks784Model.totalOutput * 100, 2).ToString("0.00"));
            tks784Model.ttsMouldRejRate = string.Format("{0}({1}%)", tks784Model.ttsMouldRej, Math.Round(tks784Model.ttsMouldRej / tks784Model.totalOutput * 100, 2).ToString("0.00"));
            tks784Model.vendorsModelRejRate = string.Format("{0}({1}%)", tks784Model.vendorsModelRej, Math.Round(tks784Model.vendorsModelRej / tks784Model.totalOutput * 100, 2).ToString("0.00"));
            tks784Model.paintRejRate = string.Format("{0}({1}%)", tks784Model.paintRej, Math.Round(tks784Model.paintRej / tks784Model.totalOutput * 100, 2).ToString("0.00"));
            tks784Model.laserRejRate = string.Format("{0}({1}%)", tks784Model.laserRej, Math.Round(tks784Model.laserRej / tks784Model.totalOutput * 100, 2).ToString("0.00"));
            tks784Model.othersRejRate = string.Format("{0}({1}%)", tks784Model.othersRej, Math.Round(tks784Model.othersRej / tks784Model.totalOutput * 100, 2).ToString("0.00"));

            reportList.Add(tks784Model);

            totalOutput += tks784Model.totalOutput;
            totalActualOutput += tks784Model.actualOutput;
            totalRej += tks784Model.totalRej;
            totalTTSRej += tks784Model.ttsMouldRej;
            totalVendorRej += tks784Model.vendorsModelRej;
            totalPaintRej += tks784Model.paintRej;
            totalLaserRej += tks784Model.laserRej;
            totalOthersRej += tks784Model.othersRej;
            #endregion

            #region tks 824
            var tks824List = from a in viDetailList
                             where a.number == "824" && a.checkProcess == "CHECK#2"//有check#2的小分类, 只独立显示check#2的数量, check#1算入laserbtn中.
                             select a;


            ViewModel.PQCSummaryReport_ViewModel.Report tks824Model = new ViewModel.PQCSummaryReport_ViewModel.Report();
            tks824Model.pqcDept = "TMS TKS824";
            tks824Model.totalOutput = tks824List.Sum(p => p.totalQty);
            tks824Model.actualOutput = tks824List.Sum(p => p.acceptQty);
            tks824Model.totalRej = tks824List.Sum(p => p.totalRej);
            tks824Model.ttsMouldRej = tks824List.Sum(p => p.ttsRej);
            tks824Model.vendorsModelRej = tks824List.Sum(p => p.vendorRej);
            tks824Model.paintRej = tks824List.Sum(p => p.paintRej);
            tks824Model.laserRej = tks824List.Sum(p => p.laserRej);
            tks824Model.othersRej = tks824List.Sum(p => p.othersRej);

            tks824Model.totalRejRate = string.Format("{0}({1}%)", tks824Model.totalRej, Math.Round(tks824Model.totalRej / tks824Model.totalOutput * 100, 2).ToString("0.00"));
            tks824Model.ttsMouldRejRate = string.Format("{0}({1}%)", tks824Model.ttsMouldRej, Math.Round(tks824Model.ttsMouldRej / tks824Model.totalOutput * 100, 2).ToString("0.00"));
            tks824Model.vendorsModelRejRate = string.Format("{0}({1}%)", tks824Model.vendorsModelRej, Math.Round(tks824Model.vendorsModelRej / tks824Model.totalOutput * 100, 2).ToString("0.00"));
            tks824Model.paintRejRate = string.Format("{0}({1}%)", tks824Model.paintRej, Math.Round(tks824Model.paintRej / tks824Model.totalOutput * 100, 2).ToString("0.00"));
            tks824Model.laserRejRate = string.Format("{0}({1}%)", tks824Model.laserRej, Math.Round(tks824Model.laserRej / tks824Model.totalOutput * 100, 2).ToString("0.00"));
            tks824Model.othersRejRate = string.Format("{0}({1}%)", tks824Model.othersRej, Math.Round(tks824Model.othersRej / tks824Model.totalOutput * 100, 2).ToString("0.00"));

            reportList.Add(tks824Model);

            totalOutput += tks824Model.totalOutput;
            totalActualOutput += tks824Model.actualOutput;
            totalRej += tks824Model.totalRej;
            totalTTSRej += tks824Model.ttsMouldRej;
            totalVendorRej += tks824Model.vendorsModelRej;
            totalPaintRej += tks824Model.paintRej;
            totalLaserRej += tks824Model.laserRej;
            totalOthersRej += tks824Model.othersRej;
            #endregion

            #region tks 833
            var tks833List = from a in viDetailList
                             where a.number == "833" && a.checkProcess == "CHECK#2"//有check#2的小分类, 只独立显示check#2的数量, check#1算入laserbtn中.
                             select a;


            ViewModel.PQCSummaryReport_ViewModel.Report tks833Model = new ViewModel.PQCSummaryReport_ViewModel.Report();
            tks833Model.pqcDept = "TAC TKS833";
            tks833Model.totalOutput = tks833List.Sum(p => p.totalQty);
            tks833Model.actualOutput = tks833List.Sum(p => p.acceptQty);
            tks833Model.totalRej = tks833List.Sum(p => p.totalRej);
            tks833Model.ttsMouldRej = tks833List.Sum(p => p.ttsRej);
            tks833Model.vendorsModelRej = tks833List.Sum(p => p.vendorRej);
            tks833Model.paintRej = tks833List.Sum(p => p.paintRej);
            tks833Model.laserRej = tks833List.Sum(p => p.laserRej);
            tks833Model.othersRej = tks833List.Sum(p => p.othersRej);

            tks833Model.totalRejRate = string.Format("{0}({1}%)", tks833Model.totalRej, Math.Round(tks833Model.totalRej / tks833Model.totalOutput * 100, 2).ToString("0.00"));
            tks833Model.ttsMouldRejRate = string.Format("{0}({1}%)", tks833Model.ttsMouldRej, Math.Round(tks833Model.ttsMouldRej / tks833Model.totalOutput * 100, 2).ToString("0.00"));
            tks833Model.vendorsModelRejRate = string.Format("{0}({1}%)", tks833Model.vendorsModelRej, Math.Round(tks833Model.vendorsModelRej / tks833Model.totalOutput * 100, 2).ToString("0.00"));
            tks833Model.paintRejRate = string.Format("{0}({1}%)", tks833Model.paintRej, Math.Round(tks833Model.paintRej / tks833Model.totalOutput * 100, 2).ToString("0.00"));
            tks833Model.laserRejRate = string.Format("{0}({1}%)", tks833Model.laserRej, Math.Round(tks833Model.laserRej / tks833Model.totalOutput * 100, 2).ToString("0.00"));
            tks833Model.othersRejRate = string.Format("{0}({1}%)", tks833Model.othersRej, Math.Round(tks833Model.othersRej / tks833Model.totalOutput * 100, 2).ToString("0.00"));

            reportList.Add(tks833Model);

            totalOutput += tks833Model.totalOutput;
            totalActualOutput += tks833Model.actualOutput;
            totalRej += tks833Model.totalRej;
            totalTTSRej += tks833Model.ttsMouldRej;
            totalVendorRej += tks833Model.vendorsModelRej;
            totalPaintRej += tks833Model.paintRej;
            totalLaserRej += tks833Model.laserRej;
            totalOthersRej += tks833Model.othersRej;
            #endregion

            #region 452
            var b452List = from a in viDetailList
                           where a.number == "452"
                           select a;


            ViewModel.PQCSummaryReport_ViewModel.Report b452Model = new ViewModel.PQCSummaryReport_ViewModel.Report();
            b452Model.pqcDept = "601B 452";
            b452Model.totalOutput = b452List.Sum(p => p.totalQty);
            b452Model.actualOutput = b452List.Sum(p => p.acceptQty);
            b452Model.totalRej = b452List.Sum(p => p.totalRej);
            b452Model.ttsMouldRej = b452List.Sum(p => p.ttsRej);
            b452Model.vendorsModelRej = b452List.Sum(p => p.vendorRej);
            b452Model.paintRej = b452List.Sum(p => p.paintRej);
            b452Model.laserRej = b452List.Sum(p => p.laserRej);
            b452Model.othersRej = b452List.Sum(p => p.othersRej);

            b452Model.totalRejRate = string.Format("{0}({1}%)", b452Model.totalRej, Math.Round(b452Model.totalRej / b452Model.totalOutput * 100, 2).ToString("0.00"));
            b452Model.ttsMouldRejRate = string.Format("{0}({1}%)", b452Model.ttsMouldRej, Math.Round(b452Model.ttsMouldRej / b452Model.totalOutput * 100, 2).ToString("0.00"));
            b452Model.vendorsModelRejRate = string.Format("{0}({1}%)", b452Model.vendorsModelRej, Math.Round(b452Model.vendorsModelRej / b452Model.totalOutput * 100, 2).ToString("0.00"));
            b452Model.paintRejRate = string.Format("{0}({1}%)", b452Model.paintRej, Math.Round(b452Model.paintRej / b452Model.totalOutput * 100, 2).ToString("0.00"));
            b452Model.laserRejRate = string.Format("{0}({1}%)", b452Model.laserRej, Math.Round(b452Model.laserRej / b452Model.totalOutput * 100, 2).ToString("0.00"));
            b452Model.othersRejRate = string.Format("{0}({1}%)", b452Model.othersRej, Math.Round(b452Model.othersRej / b452Model.totalOutput * 100, 2).ToString("0.00"));

            reportList.Add(b452Model);

            totalOutput += b452Model.totalOutput;
            totalActualOutput += b452Model.actualOutput;
            totalRej += b452Model.totalRej;
            totalTTSRej += b452Model.ttsMouldRej;
            totalVendorRej += b452Model.vendorsModelRej;
            totalPaintRej += b452Model.paintRej;
            totalLaserRej += b452Model.laserRej;
            totalOthersRej += b452Model.othersRej;
            #endregion

            #region 595
            var b595List = from a in viDetailList
                           where a.number == "595"
                           select a;


            ViewModel.PQCSummaryReport_ViewModel.Report b595Model = new ViewModel.PQCSummaryReport_ViewModel.Report();
            b595Model.pqcDept = "601B 595";
            b595Model.totalOutput = b595List.Sum(p => p.totalQty);
            b595Model.actualOutput = b595List.Sum(p => p.acceptQty);
            b595Model.totalRej = b595List.Sum(p => p.totalRej);
            b595Model.ttsMouldRej = b595List.Sum(p => p.ttsRej);
            b595Model.vendorsModelRej = b595List.Sum(p => p.vendorRej);
            b595Model.paintRej = b595List.Sum(p => p.paintRej);
            b595Model.laserRej = b595List.Sum(p => p.laserRej);
            b595Model.othersRej = b595List.Sum(p => p.othersRej);

            b595Model.totalRejRate = string.Format("{0}({1}%)", b595Model.totalRej, Math.Round(b595Model.totalRej / b595Model.totalOutput * 100, 2).ToString("0.00"));
            b595Model.ttsMouldRejRate = string.Format("{0}({1}%)", b595Model.ttsMouldRej, Math.Round(b595Model.ttsMouldRej / b595Model.totalOutput * 100, 2).ToString("0.00"));
            b595Model.vendorsModelRejRate = string.Format("{0}({1}%)", b595Model.vendorsModelRej, Math.Round(b595Model.vendorsModelRej / b595Model.totalOutput * 100, 2).ToString("0.00"));
            b595Model.paintRejRate = string.Format("{0}({1}%)", b595Model.paintRej, Math.Round(b595Model.paintRej / b595Model.totalOutput * 100, 2).ToString("0.00"));
            b595Model.laserRejRate = string.Format("{0}({1}%)", b595Model.laserRej, Math.Round(b595Model.laserRej / b595Model.totalOutput * 100, 2).ToString("0.00"));
            b595Model.othersRejRate = string.Format("{0}({1}%)", b595Model.othersRej, Math.Round(b595Model.othersRej / b595Model.totalOutput * 100, 2).ToString("0.00"));

            reportList.Add(b595Model);

            totalOutput += b595Model.totalOutput;
            totalActualOutput += b595Model.actualOutput;
            totalRej += b595Model.totalRej;
            totalTTSRej += b595Model.ttsMouldRej;
            totalVendorRej += b595Model.vendorsModelRej;
            totalPaintRej += b595Model.paintRej;
            totalLaserRej += b595Model.laserRej;
            totalOthersRej += b595Model.othersRej;
            #endregion

            #region 656
            var b656List = from a in viDetailList
                           where a.number == "656"
                           select a;


            ViewModel.PQCSummaryReport_ViewModel.Report b656Model = new ViewModel.PQCSummaryReport_ViewModel.Report();
            b656Model.pqcDept = "601B 656";
            b656Model.totalOutput = b656List.Sum(p => p.totalQty);
            b656Model.actualOutput = b656List.Sum(p => p.acceptQty);
            b656Model.totalRej = b656List.Sum(p => p.totalRej);
            b656Model.ttsMouldRej = b656List.Sum(p => p.ttsRej);
            b656Model.vendorsModelRej = b656List.Sum(p => p.vendorRej);
            b656Model.paintRej = b656List.Sum(p => p.paintRej);
            b656Model.laserRej = b656List.Sum(p => p.laserRej);
            b656Model.othersRej = b656List.Sum(p => p.othersRej);

            b656Model.totalRejRate = string.Format("{0}({1}%)", b656Model.totalRej, Math.Round(b656Model.totalRej / b656Model.totalOutput * 100, 2).ToString("0.00"));
            b656Model.ttsMouldRejRate = string.Format("{0}({1}%)", b656Model.ttsMouldRej, Math.Round(b656Model.ttsMouldRej / b656Model.totalOutput * 100, 2).ToString("0.00"));
            b656Model.vendorsModelRejRate = string.Format("{0}({1}%)", b656Model.vendorsModelRej, Math.Round(b656Model.vendorsModelRej / b656Model.totalOutput * 100, 2).ToString("0.00"));
            b656Model.paintRejRate = string.Format("{0}({1}%)", b656Model.paintRej, Math.Round(b656Model.paintRej / b656Model.totalOutput * 100, 2).ToString("0.00"));
            b656Model.laserRejRate = string.Format("{0}({1}%)", b656Model.laserRej, Math.Round(b656Model.laserRej / b656Model.totalOutput * 100, 2).ToString("0.00"));
            b656Model.othersRejRate = string.Format("{0}({1}%)", b656Model.othersRej, Math.Round(b656Model.othersRej / b656Model.totalOutput * 100, 2).ToString("0.00"));

            reportList.Add(b656Model);

            totalOutput += b656Model.totalOutput;
            totalActualOutput += b656Model.actualOutput;
            totalRej += b656Model.totalRej;
            totalTTSRej += b656Model.ttsMouldRej;
            totalVendorRej += b656Model.vendorsModelRej;
            totalPaintRej += b656Model.paintRej;
            totalLaserRej += b656Model.laserRej;
            totalOthersRej += b656Model.othersRej;
            #endregion

            #region tks 830
            var tks830List = from a in viDetailList
                             where a.number == "830"
                             select a;


            ViewModel.PQCSummaryReport_ViewModel.Report tks830Model = new ViewModel.PQCSummaryReport_ViewModel.Report();
            tks830Model.pqcDept = "320B TKS830";
            tks830Model.totalOutput = tks830List.Sum(p => p.totalQty);
            tks830Model.actualOutput = tks830List.Sum(p => p.acceptQty);
            tks830Model.totalRej = tks830List.Sum(p => p.totalRej);
            tks830Model.ttsMouldRej = tks830List.Sum(p => p.ttsRej);
            tks830Model.vendorsModelRej = tks830List.Sum(p => p.vendorRej);
            tks830Model.paintRej = tks830List.Sum(p => p.paintRej);
            tks830Model.laserRej = tks830List.Sum(p => p.laserRej);
            tks830Model.othersRej = tks830List.Sum(p => p.othersRej);

            tks830Model.totalRejRate = string.Format("{0}({1}%)", tks830Model.totalRej, Math.Round(tks830Model.totalRej / tks830Model.totalOutput * 100, 2).ToString("0.00"));
            tks830Model.ttsMouldRejRate = string.Format("{0}({1}%)", tks830Model.ttsMouldRej, Math.Round(tks830Model.ttsMouldRej / tks830Model.totalOutput * 100, 2).ToString("0.00"));
            tks830Model.vendorsModelRejRate = string.Format("{0}({1}%)", tks830Model.vendorsModelRej, Math.Round(tks830Model.vendorsModelRej / tks830Model.totalOutput * 100, 2).ToString("0.00"));
            tks830Model.paintRejRate = string.Format("{0}({1}%)", tks830Model.paintRej, Math.Round(tks830Model.paintRej / tks830Model.totalOutput * 100, 2).ToString("0.00"));
            tks830Model.laserRejRate = string.Format("{0}({1}%)", tks830Model.laserRej, Math.Round(tks830Model.laserRej / tks830Model.totalOutput * 100, 2).ToString("0.00"));
            tks830Model.othersRejRate = string.Format("{0}({1}%)", tks830Model.othersRej, Math.Round(tks830Model.othersRej / tks830Model.totalOutput * 100, 2).ToString("0.00"));

            reportList.Add(tks830Model);

            totalOutput += tks830Model.totalOutput;
            totalActualOutput += tks830Model.actualOutput;
            totalRej += tks830Model.totalRej;
            totalTTSRej += tks830Model.ttsMouldRej;
            totalVendorRej += tks830Model.vendorsModelRej;
            totalPaintRej += tks830Model.paintRej;
            totalLaserRej += tks830Model.laserRej;
            totalOthersRej += tks830Model.othersRej;
            #endregion

            #region tks 831
            var tks831List = from a in viDetailList
                             where a.number == "831"
                             select a;


            ViewModel.PQCSummaryReport_ViewModel.Report tks831Model = new ViewModel.PQCSummaryReport_ViewModel.Report();
            tks831Model.pqcDept = "320B TKS831";
            tks831Model.totalOutput = tks831List.Sum(p => p.totalQty);
            tks831Model.actualOutput = tks831List.Sum(p => p.acceptQty);
            tks831Model.totalRej = tks831List.Sum(p => p.totalRej);
            tks831Model.ttsMouldRej = tks831List.Sum(p => p.ttsRej);
            tks831Model.vendorsModelRej = tks831List.Sum(p => p.vendorRej);
            tks831Model.paintRej = tks831List.Sum(p => p.paintRej);
            tks831Model.laserRej = tks831List.Sum(p => p.laserRej);
            tks831Model.othersRej = tks831List.Sum(p => p.othersRej);

            tks831Model.totalRejRate = string.Format("{0}({1}%)", tks831Model.totalRej, Math.Round(tks831Model.totalRej / tks831Model.totalOutput * 100, 2).ToString("0.00"));
            tks831Model.ttsMouldRejRate = string.Format("{0}({1}%)", tks831Model.ttsMouldRej, Math.Round(tks831Model.ttsMouldRej / tks831Model.totalOutput * 100, 2).ToString("0.00"));
            tks831Model.vendorsModelRejRate = string.Format("{0}({1}%)", tks831Model.vendorsModelRej, Math.Round(tks831Model.vendorsModelRej / tks831Model.totalOutput * 100, 2).ToString("0.00"));
            tks831Model.paintRejRate = string.Format("{0}({1}%)", tks831Model.paintRej, Math.Round(tks831Model.paintRej / tks831Model.totalOutput * 100, 2).ToString("0.00"));
            tks831Model.laserRejRate = string.Format("{0}({1}%)", tks831Model.laserRej, Math.Round(tks831Model.laserRej / tks831Model.totalOutput * 100, 2).ToString("0.00"));
            tks831Model.othersRejRate = string.Format("{0}({1}%)", tks831Model.othersRej, Math.Round(tks831Model.othersRej / tks831Model.totalOutput * 100, 2).ToString("0.00"));

            reportList.Add(tks831Model);


            totalOutput += tks831Model.totalOutput;
            totalActualOutput += tks831Model.actualOutput;
            totalRej += tks831Model.totalRej;
            totalTTSRej += tks831Model.ttsMouldRej;
            totalVendorRej += tks831Model.vendorsModelRej;
            totalPaintRej += tks831Model.paintRej;
            totalLaserRej += tks831Model.laserRej;
            totalOthersRej += tks831Model.othersRej;
            #endregion
            
            #region tks 869
            var tks869List = from a in viDetailList
                             where a.number == "869"
                             select a;


            ViewModel.PQCSummaryReport_ViewModel.Report tks869Model = new ViewModel.PQCSummaryReport_ViewModel.Report();
            tks869Model.pqcDept = "TP1 TKS869";
            tks869Model.totalOutput = tks869List.Sum(p => p.totalQty);
            tks869Model.actualOutput = tks869List.Sum(p => p.acceptQty);
            tks869Model.totalRej = tks869List.Sum(p => p.totalRej);
            tks869Model.ttsMouldRej = tks869List.Sum(p => p.ttsRej);
            tks869Model.vendorsModelRej = tks869List.Sum(p => p.vendorRej);
            tks869Model.paintRej = tks869List.Sum(p => p.paintRej);
            tks869Model.laserRej = tks869List.Sum(p => p.laserRej);
            tks869Model.othersRej = tks869List.Sum(p => p.othersRej);

            tks869Model.totalRejRate = string.Format("{0}({1}%)", tks869Model.totalRej, Math.Round(tks869Model.totalRej / tks869Model.totalOutput * 100, 2).ToString("0.00"));
            tks869Model.ttsMouldRejRate = string.Format("{0}({1}%)", tks869Model.ttsMouldRej, Math.Round(tks869Model.ttsMouldRej / tks869Model.totalOutput * 100, 2).ToString("0.00"));
            tks869Model.vendorsModelRejRate = string.Format("{0}({1}%)", tks869Model.vendorsModelRej, Math.Round(tks869Model.vendorsModelRej / tks869Model.totalOutput * 100, 2).ToString("0.00"));
            tks869Model.paintRejRate = string.Format("{0}({1}%)", tks869Model.paintRej, Math.Round(tks869Model.paintRej / tks869Model.totalOutput * 100, 2).ToString("0.00"));
            tks869Model.laserRejRate = string.Format("{0}({1}%)", tks869Model.laserRej, Math.Round(tks869Model.laserRej / tks869Model.totalOutput * 100, 2).ToString("0.00"));
            tks869Model.othersRejRate = string.Format("{0}({1}%)", tks869Model.othersRej, Math.Round(tks869Model.othersRej / tks869Model.totalOutput * 100, 2).ToString("0.00"));

            reportList.Add(tks869Model);


            totalOutput += tks869Model.totalOutput;
            totalActualOutput += tks869Model.actualOutput;
            totalRej += tks869Model.totalRej;
            totalTTSRej += tks869Model.ttsMouldRej;
            totalVendorRej += tks869Model.vendorsModelRej;
            totalPaintRej += tks869Model.paintRej;
            totalLaserRej += tks869Model.laserRej;
            totalOthersRej += tks869Model.othersRej;
            #endregion



            

            ViewModel.PQCSummaryReport_ViewModel.Report totalModel = new ViewModel.PQCSummaryReport_ViewModel.Report();
            totalModel.pqcDept = "Total";
            totalModel.totalOutput = totalOutput;
            totalModel.actualOutput = totalActualOutput;
            totalModel.ttsMouldRejRate = string.Format("{0}({1}%)", totalTTSRej, Math.Round(totalTTSRej / totalOutput * 100, 2).ToString("0.00"));
            totalModel.vendorsModelRejRate = string.Format("{0}({1}%)", totalVendorRej, Math.Round(totalVendorRej / totalOutput * 100, 2).ToString("0.00"));
            totalModel.paintRejRate = string.Format("{0}({1}%)", totalPaintRej, Math.Round(totalPaintRej / totalOutput * 100, 2).ToString("0.00"));
            totalModel.laserRejRate = string.Format("{0}({1}%)", totalLaserRej, Math.Round(totalLaserRej / totalOutput * 100, 2).ToString("0.00"));
            totalModel.othersRejRate = string.Format("{0}({1}%)", totalOthersRej, Math.Round(totalOthersRej / totalOutput * 100, 2).ToString("0.00"));
            totalModel.totalRejRate = string.Format("{0}({1}%)", totalRej, Math.Round(totalRej / totalOutput * 100, 2).ToString("0.00"));
            reportList.Add(totalModel);




            ViewModel.PQCSummaryReport_ViewModel.Report packModel = GetPack(dDateFrom, dDateTo, sShift);
            reportList.Add(packModel);




            return reportList;
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
                model.totalRej = double.Parse(dr["rejectQty"].ToString());

                model.ttsRej = double.Parse(dr["ttsRejQty"].ToString());
                model.vendorRej = double.Parse(dr["vendorRejQty"].ToString());
                model.paintRej = double.Parse(dr["paintRej"].ToString());
                model.laserRej = double.Parse(dr["laserRej"].ToString());
                model.othersRej = double.Parse(dr["othersRej"].ToString());

                model.checkProcess = dr["checkProcess"].ToString();
                model.allProcess = dr["allProcess"].ToString();
                model.number = dr["number"].ToString();

                defectList.Add(model);
            }


            return defectList;
        }


        private ViewModel.PQCSummaryReport_ViewModel.Report GetPack(DateTime dDateFrom, DateTime dDateTo, string sShift)
        {

            ViewModel.PQCSummaryReport_ViewModel.Report packModel = new ViewModel.PQCSummaryReport_ViewModel.Report();
            packModel.pqcDept = "Packing";



            Common.Class.BLL.PQCPackTracking bll = new Common.Class.BLL.PQCPackTracking();

            DataTable dt = bll.GetList(dDateFrom, dDateTo, sShift,"","","","");
            if (dt == null || dt.Rows.Count == 0)
            {
                packModel.totalOutput = 0;
                packModel.actualOutput = 0;
                packModel.totalRej = 0;
            }
            else
            {
                packModel.totalOutput = double.Parse(dt.Compute("sum(TotalQty)", "").ToString());
                packModel.actualOutput = double.Parse(dt.Compute("sum(acceptQty)", "").ToString());
                packModel.totalRej = double.Parse(dt.Compute("sum(rejectQty)", "").ToString());

                packModel.totalRejRate = string.Format("{0}({1}%)", packModel.totalRej, Math.Round(packModel.totalRej / packModel.totalOutput * 100, 2).ToString("0.00"));
            }


            return packModel;
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

            //根据 jobid， process 获取下改 bin 信息。
            viBinList = viBinBLL.GetModelList(null,null, viTrackingModel.jobId, viTrackingModel.processes);



            foreach (Common.Class.Model.PQCQaViBinning binModel in viBinList)
            {

                decimal addedPassQty = dicAddedPassQty[binModel.materialPartNo];
                decimal curPassQty = binModel.materialQty.Value;


                // material qty =  bin的原本数量  +  修改后增加的数量。
                binModel.materialQty = binModel.materialQty.Value + addedPassQty;

                

                //拷贝到bin history model， 并将原本数量赋值给material from qty， 并添加到list中。
                Common.Class.Model.PQCQaViBinHistory_Model binHisModel = new Common.Class.Model.PQCQaViBinHistory_Model();
                binHisModel = binHisBLL.CopyModel(binModel);
                binHisModel.materialFromQty = curPassQty;
                binHisModel.updatedTime = DateTime.Now;
                binHistoryList.Add(binHisModel);

            }




            return viTrackingBLL.MaintenanceUpdateQty(viTrackingModel, detailTrackingList, defectTrackingList, viBinList, binHistoryList);
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
        public List<ViewModel.PackingDetail_ViewModel> GetPackingDetailList(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sStation, string sPIC, string sJobNo, string sLotNo)
        {
            DataTable dt = packTrackBLL.GetList(dDateFrom, dDateTo,"", sPartNo, sStation, sPIC, sJobNo);
            if (dt == null || dt.Rows.Count == 0) return null;



            DataTable dtPaint = paintBLL.GetList(dDateFrom.AddMonths(-6), dDateTo, "");




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
                //获取 lotno
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



                modelList.Add(model);
            }


            List<ViewModel.PackingDetail_ViewModel> temp = new List<ViewModel.PackingDetail_ViewModel>();
            if (sLotNo != "")
            {
                temp = (from a in modelList where a.lotNo == sLotNo orderby a.dateTime ascending select a).ToList();
            }
            else
            {
                temp = (from a in modelList orderby a.dateTime ascending select a).ToList();
            }
            

            ViewModel.PackingDetail_ViewModel summaryModel = new ViewModel.PackingDetail_ViewModel();
            summaryModel.shift = "Total";
            summaryModel.okQty = temp.Sum(p => p.okQty);
            summaryModel.ngQty = temp.Sum(p => p.ngQty);
            summaryModel.totalQty = temp.Sum(p => p.totalQty);
            temp.Add(summaryModel);


            return temp;
        }



        #region  checking detial list
        public List<ViewModel.CheckingDetail_ViewModel> GetCheckingDetailList(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sStation, string sPIC, string sJobNo, string sLotNo)
        {
            DataTable dt = viTrackingBLL.GetCheckingDetailList(dDateFrom, dDateTo, sPartNo, sStation, sPIC, sJobNo);
            if (dt == null || dt.Rows.Count == 0) return null;

        

            DataTable dtPaint = paintBLL.GetList(dDateFrom.AddMonths(-6), dDateTo, "");




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



                modelList.Add(model);
            }




            List<ViewModel.CheckingDetail_ViewModel> temp = new List<ViewModel.CheckingDetail_ViewModel>();
            if (sLotNo != "")
            {
                temp = (from a in modelList where a.lotNo == sLotNo orderby a.dateTime ascending select a).ToList();
            }else
            {
                temp = (from a in modelList orderby a.dateTime ascending select a).ToList();
            }



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



        




        #endregion



    }
}