using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;

namespace DashboardTTS.ViewBusiness
{
    public class MOULDProduct_ViewBusiness
    {
        private readonly Common.Class.BLL.MouldingViHistory_BLL viBLL = new Common.Class.BLL.MouldingViHistory_BLL();
        private readonly Common.Class.BLL.MouldingViDefectTracking_BLL defectBLL = new Common.Class.BLL.MouldingViDefectTracking_BLL();



        public MOULDProduct_ViewBusiness()
        {

        }








        #region daily verify report 
        public string GetDailyVerifyReportPCS(DateTime dDay, string sShift)
        {
            try
            {


                List<ViewModel.MouldDailyVerifyReport_ViewModel.TrackingInfo> trackingList = GetTrackingInfo(dDay, sShift);
                List<ViewModel.MouldDailyVerifyReport_ViewModel.DefectInfo> defectList = GetDefectList(dDay, sShift);
                if (trackingList == null || trackingList.Count == 0 || defectList == null || defectList.Count == 0)
                    return null;



                //tracking list 按 machine, partno汇总
                var trackingSummary = from a in trackingList
                                      group a by new { a.machineID, a.partNo }
                                      into b
                                      select new
                                      {
                                          machineID = b.Key.machineID,
                                          model = b.Max(p => p.model),
                                          partNo = b.Key.partNo,
                                          jigNo = b.Max(p => p.jigNo),
                                          mcRunHours = b.Sum(p => (p.stopTime - p.startTime).TotalSeconds / 3600),
                                          cavityCount = b.Max(p => p.cavityCount),
                                          passQty = b.Sum(p => p.passQty),
                                          rejQty = b.Sum(p => p.rejQty),
                                          totalQty = b.Sum(p => p.totalQty),
                                          ipqcRejQty = b.Sum(p => p.ipqcRejQty),
                                          wastageMaterial01 = b.Sum(p => p.wastageMaterial01),
                                          wastageMaterial02 = b.Sum(p => p.wastageMaterial02),
                                          setUp = b.Sum(p => p.setUp),
                                          userID = b.Max(p => p.userID),
                                          supervisor = b.Max(p => p.supervisor)
                                      };

                //defect list 按 machine, partno汇总
                var defectSummary = from a in defectList
                                    group a by new { a.machineID, a.partNo }
                                    into b
                                    select new
                                    {
                                        machineID = b.Key.machineID,
                                        partNo = b.Key.partNo,

                                        whiteDot = b.Sum(p => p.whiteDot),
                                        scratches = b.Sum(p => p.scratches),
                                        dentedMark = b.Sum(p => p.dentedMark),
                                        shinningDot = b.Sum(p => p.shinningDot),
                                        blackMark = b.Sum(p => p.blackMark),
                                        sinkMark = b.Sum(p => p.sinkMark),
                                        flowMark = b.Sum(p => p.flowMark),
                                        highGate = b.Sum(p => p.highGate),
                                        silverSteak = b.Sum(p => p.silverSteak),
                                        blackDot = b.Sum(p => p.blackDot),
                                        oilStain = b.Sum(p => p.oilStain),
                                        flowLine = b.Sum(p => p.flowLine),
                                        overCut = b.Sum(p => p.overCut),
                                        crack = b.Sum(p => p.crack),
                                        shortMold = b.Sum(p => p.shortMold),
                                        stainMark = b.Sum(p => p.stainMark),
                                        weldLine = b.Sum(p => p.weldLine),
                                        flashes = b.Sum(p => p.flashes),
                                        foreignMaterial = b.Sum(p => p.foreignMaterial),
                                        drag = b.Sum(p => p.drag),
                                        materialBleed = b.Sum(p => p.materialBleed),
                                        bent = b.Sum(p => p.bent),
                                        deform = b.Sum(p => p.deform),
                                        gasMark = b.Sum(p => p.gasMark)
                                    };

                //合并 tracking, defect汇总信息.
                var mergedList = from a in trackingSummary
                                 join b in defectSummary on new { a.machineID, a.partNo } equals new { b.machineID, b.partNo }
                                 select new
                                 {
                                     a,
                                     b
                                 };




                //导入到 report list中
                List<ViewModel.MouldDailyVerifyReport_ViewModel.Report> reportList = new List<ViewModel.MouldDailyVerifyReport_ViewModel.Report>();
                foreach (var item in mergedList)
                {
                    ViewModel.MouldDailyVerifyReport_ViewModel.Report model = new ViewModel.MouldDailyVerifyReport_ViewModel.Report();

                    model.machineID = item.a.machineID;
                    model.model = item.a.model;
                    model.partNo = item.a.partNo;
                    model.jigNo = item.a.jigNo;
                    model.mcRunHours = Common.CommFunctions.ConvertDateTimeShort(item.a.mcRunHours.ToString());
                    model.cavityCount = item.a.cavityCount;
                    model.totalShots = Math.Round(item.a.totalQty / item.a.cavityCount, 0);
                    model.passQty = item.a.passQty;
                    model.rejQty = item.a.rejQty;
                    model.rejRate = Math.Round(item.a.rejQty / item.a.totalQty * 100, 2).ToString("0.00") + "%";

                    model.whiteDot = item.b.whiteDot;
                    model.scratches = item.b.scratches;
                    model.dentedMark = item.b.dentedMark;
                    model.shinningDot = item.b.shinningDot;
                    model.blackMark = item.b.blackMark;
                    model.sinkMark = item.b.sinkMark;
                    model.flowMark = item.b.flowMark;
                    model.highGate = item.b.highGate;
                    model.silverSteak = item.b.silverSteak;
                    model.blackDot = item.b.blackDot;
                    model.oilStain = item.b.oilStain;
                    model.flowLine = item.b.flowLine;
                    model.overCut = item.b.overCut;
                    model.crack = item.b.crack;
                    model.shortMold = item.b.shortMold;
                    model.stainMark = item.b.stainMark;
                    model.weldLine = item.b.weldLine;
                    model.flashes = item.b.flashes;
                    model.foreignMaterial = item.b.foreignMaterial;
                    model.drag = item.b.drag;
                    model.materialBleed = item.b.materialBleed;
                    model.bent = item.b.bent;
                    model.deform = item.b.deform;
                    model.gasMark = item.b.gasMark;

                    model.ipqcRejQty = item.a.ipqcRejQty;
                    model.wastageMaterial = item.a.wastageMaterial01 + item.a.wastageMaterial02;
                    model.setUp = item.a.setUp;
                    model.setUpRate = Math.Round(item.a.setUp / item.a.totalQty * 100, 2).ToString("0.00") + "%";
                    model.userID = item.a.userID;
                    model.supervisor = item.a.supervisor;



                    reportList.Add(model);
                }


                var sortedList = (from a in reportList orderby a.machineID ascending select a).ToList();


                ViewModel.MouldDailyVerifyReport_ViewModel.Report total = new ViewModel.MouldDailyVerifyReport_ViewModel.Report();
                total.partNo = "Sub Total->";
                total.mcRunHours = Common.CommFunctions.ConvertDateTimeShort(mergedList.Sum(p => p.a.mcRunHours).ToString());
                total.totalShots = reportList.Sum(p => p.totalShots);
                total.passQty = reportList.Sum(p => p.passQty);
                total.rejQty = reportList.Sum(p => p.rejQty);
                total.rejRate = Math.Round(reportList.Sum(p => p.rejQty) / (reportList.Sum(p => p.passQty) + reportList.Sum(p => p.rejQty)) * 100, 2).ToString("0.00") + "%";


                total.whiteDot = reportList.Sum(p => p.whiteDot);
                total.scratches = reportList.Sum(p => p.scratches);
                total.dentedMark = reportList.Sum(p => p.dentedMark);
                total.shinningDot = reportList.Sum(p => p.shinningDot);
                total.blackMark = reportList.Sum(p => p.blackMark);
                total.sinkMark = reportList.Sum(p => p.sinkMark);
                total.flowMark = reportList.Sum(p => p.flowMark);
                total.highGate = reportList.Sum(p => p.highGate);
                total.silverSteak = reportList.Sum(p => p.silverSteak);
                total.blackDot = reportList.Sum(p => p.blackDot);
                total.oilStain = reportList.Sum(p => p.oilStain);
                total.flowLine = reportList.Sum(p => p.flowLine);
                total.overCut = reportList.Sum(p => p.overCut);
                total.crack = reportList.Sum(p => p.crack);
                total.shortMold = reportList.Sum(p => p.shortMold);
                total.stainMark = reportList.Sum(p => p.stainMark);
                total.weldLine = reportList.Sum(p => p.weldLine);
                total.flashes = reportList.Sum(p => p.flashes);
                total.foreignMaterial = reportList.Sum(p => p.foreignMaterial);
                total.drag = reportList.Sum(p => p.drag);
                total.materialBleed = reportList.Sum(p => p.materialBleed);
                total.bent = reportList.Sum(p => p.bent);
                total.deform = reportList.Sum(p => p.deform);
                total.gasMark = reportList.Sum(p => p.gasMark);


                total.ipqcRejQty = reportList.Sum(p => p.ipqcRejQty);
                total.wastageMaterial = reportList.Sum(p => p.wastageMaterial);
                total.setUp = reportList.Sum(p => p.setUp);
                total.setUpRate = Math.Round(reportList.Sum(p => p.setUp) / (reportList.Sum(p => p.passQty) + reportList.Sum(p => p.rejQty)) * 100, 2).ToString("0.00") + "%";

                total.supervisor = GetReportVerifyBy(dDay, sShift);


                sortedList.Add(total);



                JavaScriptSerializer js = new JavaScriptSerializer();

                return js.Serialize(sortedList);


            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("M_DailyReport_Debug", "GetDailyVerifyReportPCS Catch Exception: " + ee.ToString());
                return "";
            }
        }

        public string GetDailyVerifyReportSET(DateTime dDay, string sShift)
        {

            try
            {

                List<ViewModel.MouldDailyVerifyReport_ViewModel.TrackingInfo> trackingList = GetTrackingInfo(dDay, sShift);
            List<ViewModel.MouldDailyVerifyReport_ViewModel.DefectInfo> defectList = GetDefectList(dDay, sShift);
            if (trackingList == null || trackingList.Count == 0 || defectList == null || defectList.Count == 0)
                return null;



            //tracking list 按 machine, partnoall汇总
            var trackingSummary = from a in trackingList
                                  group a by new { a.machineID, a.partNoALL }
                                  into b
                                  select new
                                  {
                                      machineID = b.Key.machineID,
                                      model = b.Max(p => p.model),
                                      partNoALL = b.Key.partNoALL,
                                      mcRunHours = b.Max(p => (p.stopTime - p.startTime).TotalSeconds / 3600),

                                      passQty = b.Min(p => p.passQty),//取小part no中最小的.
                                      rejQty = b.Sum(p => p.rejQty),//rej 按照pcs汇总.
                                      totalQty = b.Max(p => p.totalQty),
                                      ipqcRejQty = b.Max(p => p.ipqcRejQty),
                                      wastageMaterial01 = b.Max(p => p.wastageMaterial01),
                                      wastageMaterial02 = b.Max(p => p.wastageMaterial02),
                                      setUp = b.Max(p => p.setUp),
                                      userID = b.Max(p => p.userID),
                                      supervisor = b.Max(p => p.supervisor)
                                  };

            //defect list 按 machine, partno汇总
            var defectSummary = from a in defectList
                                group a by new { a.machineID, a.partNoALL }
                                into b
                                select new
                                {
                                    machineID = b.Key.machineID,
                                    partNoALL = b.Key.partNoALL,

                                    whiteDot = b.Sum(p => p.whiteDot),
                                    scratches = b.Sum(p => p.scratches),
                                    dentedMark = b.Sum(p => p.dentedMark),
                                    shinningDot = b.Sum(p => p.shinningDot),
                                    blackMark = b.Sum(p => p.blackMark),
                                    sinkMark = b.Sum(p => p.sinkMark),
                                    flowMark = b.Sum(p => p.flowMark),
                                    highGate = b.Sum(p => p.highGate),
                                    silverSteak = b.Sum(p => p.silverSteak),
                                    blackDot = b.Sum(p => p.blackDot),
                                    oilStain = b.Sum(p => p.oilStain),
                                    flowLine = b.Sum(p => p.flowLine),
                                    overCut = b.Sum(p => p.overCut),
                                    crack = b.Sum(p => p.crack),
                                    shortMold = b.Sum(p => p.shortMold),
                                    stainMark = b.Sum(p => p.stainMark),
                                    weldLine = b.Sum(p => p.weldLine),
                                    flashes = b.Sum(p => p.flashes),
                                    foreignMaterial = b.Sum(p => p.foreignMaterial),
                                    drag = b.Sum(p => p.drag),
                                    materialBleed = b.Sum(p => p.materialBleed),
                                    bent = b.Sum(p => p.bent),
                                    deform = b.Sum(p => p.deform),
                                    gasMark = b.Sum(p => p.gasMark)
                                };

            //合并 tracking, defect汇总信息.
            var mergedList = from a in trackingSummary
                             join b in defectSummary on new { a.machineID, a.partNoALL } equals new { b.machineID, b.partNoALL }
                             select new
                             {
                                 a,
                                 b
                             };




            //导入到 report list中
            List<ViewModel.MouldDailyVerifyReport_ViewModel.Report> reportList = new List<ViewModel.MouldDailyVerifyReport_ViewModel.Report>();
            foreach (var item in mergedList)
            {
                ViewModel.MouldDailyVerifyReport_ViewModel.Report model = new ViewModel.MouldDailyVerifyReport_ViewModel.Report();

                model.machineID = item.a.machineID;
                model.model = item.a.model;
                model.partNoALL = item.a.partNoALL;
                model.mcRunHours = Common.CommFunctions.ConvertDateTimeShort(item.a.mcRunHours.ToString());
                model.totalShots = item.a.totalQty;
                model.passQty = item.a.passQty;
                model.rejQty = item.a.rejQty;
                model.rejRate = Math.Round(item.a.rejQty / item.a.totalQty * 100, 2).ToString("0.00") + "%";

                model.whiteDot = item.b.whiteDot;
                model.scratches = item.b.scratches;
                model.dentedMark = item.b.dentedMark;
                model.shinningDot = item.b.shinningDot;
                model.blackMark = item.b.blackMark;
                model.sinkMark = item.b.sinkMark;
                model.flowMark = item.b.flowMark;
                model.highGate = item.b.highGate;
                model.silverSteak = item.b.silverSteak;
                model.blackDot = item.b.blackDot;
                model.oilStain = item.b.oilStain;
                model.flowLine = item.b.flowLine;
                model.overCut = item.b.overCut;
                model.crack = item.b.crack;
                model.shortMold = item.b.shortMold;
                model.stainMark = item.b.stainMark;
                model.weldLine = item.b.weldLine;
                model.flashes = item.b.flashes;
                model.foreignMaterial = item.b.foreignMaterial;
                model.drag = item.b.drag;
                model.materialBleed = item.b.materialBleed;
                model.bent = item.b.bent;
                model.deform = item.b.deform;
                model.gasMark = item.b.gasMark;

                model.ipqcRejQty = item.a.ipqcRejQty;
                model.wastageMaterial = item.a.wastageMaterial01 + item.a.wastageMaterial02;
                model.setUp = item.a.setUp;
                model.setUpRate = Math.Round(item.a.setUp / item.a.totalQty * 100, 2).ToString("0.00") + "%";
                model.userID = item.a.userID;
                model.supervisor = item.a.supervisor;




                reportList.Add(model);
            }


            var sortedList = (from a in reportList orderby a.machineID ascending select a).ToList();


            ViewModel.MouldDailyVerifyReport_ViewModel.Report total = new ViewModel.MouldDailyVerifyReport_ViewModel.Report();
            total.partNoALL = "Sub Total->";
            total.mcRunHours = Common.CommFunctions.ConvertDateTimeShort(mergedList.Sum(p => p.a.mcRunHours).ToString());
            total.totalShots = reportList.Sum(p => p.totalShots);
            total.passQty = reportList.Sum(p => p.passQty);
            total.rejQty = reportList.Sum(p => p.rejQty);
            total.rejRate = Math.Round(reportList.Sum(p => p.rejQty) / (reportList.Sum(p => p.passQty) + reportList.Sum(p => p.rejQty)) * 100, 2).ToString("0.00") + "%";


            total.whiteDot = reportList.Sum(p => p.whiteDot);
            total.scratches = reportList.Sum(p => p.scratches);
            total.dentedMark = reportList.Sum(p => p.dentedMark);
            total.shinningDot = reportList.Sum(p => p.shinningDot);
            total.blackMark = reportList.Sum(p => p.blackMark);
            total.sinkMark = reportList.Sum(p => p.sinkMark);
            total.flowMark = reportList.Sum(p => p.flowMark);
            total.highGate = reportList.Sum(p => p.highGate);
            total.silverSteak = reportList.Sum(p => p.silverSteak);
            total.blackDot = reportList.Sum(p => p.blackDot);
            total.oilStain = reportList.Sum(p => p.oilStain);
            total.flowLine = reportList.Sum(p => p.flowLine);
            total.overCut = reportList.Sum(p => p.overCut);
            total.crack = reportList.Sum(p => p.crack);
            total.shortMold = reportList.Sum(p => p.shortMold);
            total.stainMark = reportList.Sum(p => p.stainMark);
            total.weldLine = reportList.Sum(p => p.weldLine);
            total.flashes = reportList.Sum(p => p.flashes);
            total.foreignMaterial = reportList.Sum(p => p.foreignMaterial);
            total.drag = reportList.Sum(p => p.drag);
            total.materialBleed = reportList.Sum(p => p.materialBleed);
            total.bent = reportList.Sum(p => p.bent);
            total.deform = reportList.Sum(p => p.deform);
            total.gasMark = reportList.Sum(p => p.gasMark);


            total.ipqcRejQty = reportList.Sum(p => p.ipqcRejQty);
            total.wastageMaterial = reportList.Sum(p => p.wastageMaterial);
            total.setUp = reportList.Sum(p => p.setUp);
            total.setUpRate = Math.Round(reportList.Sum(p => p.setUp) / (reportList.Sum(p => p.passQty) + reportList.Sum(p => p.rejQty)) * 100, 2).ToString("0.00") + "%";

            total.supervisor = GetReportVerifyBy(dDay, sShift);


            sortedList.Add(total);



            JavaScriptSerializer js = new JavaScriptSerializer();

            return js.Serialize(sortedList);
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("M_DailyReport_Debug", "GetDailyVerifyReportSET, Catch Exception: " + ee.ToString());
                return "";
            }
        }

        public ViewModel.MouldDailyVerifyReport_ViewModel.DefectInfo GetDefectDisplayFlag(DateTime dDay)
        {
            List<ViewModel.MouldDailyVerifyReport_ViewModel.DefectInfo> defectlist = GetDefectList(dDay, "");

            var summary = from a in defectlist
                          group a by a.day into b
                          select new
                          {
                              b.Key,
                              whiteDot = b.Sum(p => p.whiteDot),
                              scratches = b.Sum(p => p.scratches),
                              dentedMark = b.Sum(p => p.dentedMark),
                              shinningDot = b.Sum(p => p.shinningDot),
                              blackMark = b.Sum(p => p.blackMark),
                              sinkMark = b.Sum(p => p.sinkMark),
                              flowMark = b.Sum(p => p.flowMark),
                              highGate = b.Sum(p => p.highGate),
                              silverSteak = b.Sum(p => p.silverSteak),
                              blackDot = b.Sum(p => p.blackDot),
                              oilStain = b.Sum(p => p.oilStain),
                              flowLine = b.Sum(p => p.flowLine),
                              overCut = b.Sum(p => p.overCut),
                              crack = b.Sum(p => p.crack),
                              shortMold = b.Sum(p => p.shortMold),
                              stainMark = b.Sum(p => p.stainMark),
                              weldLine = b.Sum(p => p.weldLine),
                              flashes = b.Sum(p => p.flashes),
                              foreignMaterial = b.Sum(p => p.foreignMaterial),
                              drag = b.Sum(p => p.drag),
                              materialBleed = b.Sum(p => p.materialBleed),
                              bent = b.Sum(p => p.bent),
                              deform = b.Sum(p => p.deform),
                              gasMark = b.Sum(p => p.gasMark)
                          };

            //0为不显示, 1为显示
            ViewModel.MouldDailyVerifyReport_ViewModel.DefectInfo displayFlag = new ViewModel.MouldDailyVerifyReport_ViewModel.DefectInfo();

            displayFlag.whiteDot = summary.FirstOrDefault().whiteDot == 0 ? 0 : 1;
            displayFlag.scratches = summary.FirstOrDefault().scratches == 0 ? 0 : 1;
            displayFlag.dentedMark = summary.FirstOrDefault().dentedMark == 0 ? 0 : 1;
            displayFlag.shinningDot = summary.FirstOrDefault().shinningDot == 0 ? 0 : 1;
            displayFlag.blackMark = summary.FirstOrDefault().blackMark == 0 ? 0 : 1;
            displayFlag.sinkMark = summary.FirstOrDefault().sinkMark == 0 ? 0 : 1;
            displayFlag.flowMark = summary.FirstOrDefault().flowMark == 0 ? 0 : 1;
            displayFlag.highGate = summary.FirstOrDefault().highGate == 0 ? 0 : 1;
            displayFlag.silverSteak = summary.FirstOrDefault().silverSteak == 0 ? 0 : 1;
            displayFlag.blackDot = summary.FirstOrDefault().blackDot == 0 ? 0 : 1;
            displayFlag.oilStain = summary.FirstOrDefault().oilStain == 0 ? 0 : 1;
            displayFlag.flowLine = summary.FirstOrDefault().flowLine == 0 ? 0 : 1;
            displayFlag.overCut = summary.FirstOrDefault().overCut == 0 ? 0 : 1;
            displayFlag.crack = summary.FirstOrDefault().crack == 0 ? 0 : 1;
            displayFlag.shortMold = summary.FirstOrDefault().shortMold == 0 ? 0 : 1;
            displayFlag.stainMark = summary.FirstOrDefault().stainMark == 0 ? 0 : 1;
            displayFlag.weldLine = summary.FirstOrDefault().weldLine == 0 ? 0 : 1;
            displayFlag.flashes = summary.FirstOrDefault().flashes == 0 ? 0 : 1;
            displayFlag.foreignMaterial = summary.FirstOrDefault().foreignMaterial == 0 ? 0 : 1;
            displayFlag.drag = summary.FirstOrDefault().drag == 0 ? 0 : 1;
            displayFlag.materialBleed = summary.FirstOrDefault().materialBleed == 0 ? 0 : 1;
            displayFlag.bent = summary.FirstOrDefault().bent == 0 ? 0 : 1;
            displayFlag.deform = summary.FirstOrDefault().deform == 0 ? 0 : 1;
            displayFlag.gasMark = summary.FirstOrDefault().gasMark == 0 ? 0 : 1;

            return displayFlag;
        }

        public List<ViewModel.MouldDailyVerifyReport_ViewModel.SummaryReport> GetSummaryReport(DateTime dDay)
        {
            List<ViewModel.MouldDailyVerifyReport_ViewModel.SummaryReport> summaryReportList = new List<ViewModel.MouldDailyVerifyReport_ViewModel.SummaryReport>();

            List<ViewModel.MouldDailyVerifyReport_ViewModel.TrackingInfo> trackingList = GetTrackingInfo(dDay, "");
            if (trackingList == null || trackingList.Count == 0)
                return null;




            //get bom list
            Common.Class.BLL.MouldingBom_BLL bomBLL = new Common.Class.BLL.MouldingBom_BLL();
            List<Common.Class.Model.MouldingBom_Model> bomList = bomBLL.GetModelList();




            //get material bom list
            Common.Class.BLL.Material_Inventory_Bom_BLL materialBLL = new Common.Class.BLL.Material_Inventory_Bom_BLL();
            List<Common.Model.Material_Inventory_Bom> materialList = materialBLL.GetModelList();




            double totalQty = 0;
            double totalPassQty = 0;
            double totalRej = 0;
            decimal totalRejCost = 0;

            double totalSetup = 0;
            decimal totalSetupCost = 0;

            double totalWastageMaterial01 = 0;
            double totalWastageMaterial02 = 0;
            decimal totalWastageMaterialCost = 0;



            foreach (ViewModel.MouldDailyVerifyReport_ViewModel.TrackingInfo model in trackingList)
            {
                var bomModel = (from a in bomList where a.partNumber == model.partNo select a).First();
                var materialModel01 = (from a in materialList where a.Material_Part == bomModel.matPart01 select a).FirstOrDefault();
                var materialModel02 = (from a in materialList where a.Material_Part == bomModel.matPart02 select a).FirstOrDefault();


                totalQty += model.totalQty;
                totalPassQty += model.passQty;
                totalRej += model.rejQty;
                totalRejCost += decimal.Parse(model.rejQty.ToString()) * bomModel.unitCount.Value;


                totalSetup += model.setUp;
                totalSetupCost += decimal.Parse(model.setUp.ToString()) * bomModel.unitCount.Value;


                totalWastageMaterial01 += model.wastageMaterial01;
                totalWastageMaterial02 += model.wastageMaterial02;
                if (materialModel01 != null && materialModel01.Unit_Price != null)
                    totalWastageMaterialCost += decimal.Parse(model.wastageMaterial01.ToString()) * materialModel01.Unit_Price.Value;
                if (materialModel02 != null && materialModel02.Unit_Price != null)
                    totalWastageMaterialCost += decimal.Parse(model.wastageMaterial02.ToString()) * materialModel02.Unit_Price.Value;
            }





            ViewModel.MouldDailyVerifyReport_ViewModel.SummaryReport overallResult = new ViewModel.MouldDailyVerifyReport_ViewModel.SummaryReport();
            overallResult.description = "Overall Result >>>>";
            overallResult.totalQty = totalQty.ToString() + "Pcs";
            overallResult.totalPass = totalPassQty.ToString() + "Pcs";
            overallResult.totalRejQty = totalRej.ToString() + "Pcs";
            overallResult.totalRejRate = Math.Round(totalRej / totalQty * 100, 2).ToString("0.00") + "%";
            overallResult.rejCost = "$" + totalRejCost.ToString("0.000");
            summaryReportList.Add(overallResult);

            ViewModel.MouldDailyVerifyReport_ViewModel.SummaryReport overallMaterialAdjust = new ViewModel.MouldDailyVerifyReport_ViewModel.SummaryReport();
            overallMaterialAdjust.description = "Overall Material Mc Adjust >>>>";
            overallMaterialAdjust.totalRejQty = totalSetup.ToString() + "Pcs";
            overallMaterialAdjust.rejCost = "$" + totalSetupCost.ToString("0.000");
            summaryReportList.Add(overallMaterialAdjust);

            ViewModel.MouldDailyVerifyReport_ViewModel.SummaryReport overallMaterialPurging = new ViewModel.MouldDailyVerifyReport_ViewModel.SummaryReport();
            overallMaterialPurging.description = "Overall Material Purging >>>>";
            overallMaterialPurging.totalRejQty = (totalWastageMaterial01 + totalWastageMaterial02).ToString() + "Kgs";
            overallMaterialPurging.rejCost = "$" + totalWastageMaterialCost.ToString("0.000");
            summaryReportList.Add(overallMaterialPurging);




            return summaryReportList;
        }






        private List<ViewModel.MouldDailyVerifyReport_ViewModel.TrackingInfo> GetTrackingInfo(DateTime dDay, string sShift)
        {

            DataTable dt = viBLL.GetList(dDay, dDay.AddDays(1), "", "", "");
            if (dt == null || dt.Rows.Count == 0)
                return null;


            List<ViewModel.MouldDailyVerifyReport_ViewModel.TrackingInfo> viList = new List<ViewModel.MouldDailyVerifyReport_ViewModel.TrackingInfo>();

            foreach (DataRow dr in dt.Rows)
            {
                //排除 Mould_Testing 和 Material_Testing的记录.  output report不能包含testing的数量.
                string status = dr["status"].ToString();
                if (status == "Mould_Testing" || status == "Material_Testing")
                    continue;


                ViewModel.MouldDailyVerifyReport_ViewModel.TrackingInfo model = new ViewModel.MouldDailyVerifyReport_ViewModel.TrackingInfo();
                model.trackingID = dr["trackingID"].ToString();
                model.day = DateTime.Parse(dr["day"].ToString());
                model.shift = dr["shift"].ToString();
                model.machineID = dr["machineID"].ToString();
                model.model = dr["model"].ToString();
                model.partNo = dr["partNumber"].ToString();
                model.partNoALL = dr["partNumberAll"].ToString();
                model.jigNo = dr["jigNo"].ToString();

                model.cavityCount = double.Parse(dr["cavityCount"].ToString());


                model.passQty = dr["acceptQty"].ToString() == "" ? 0 : double.Parse(dr["acceptQty"].ToString());
                model.rejQty = dr["rejectQty"].ToString() == "" ? 0 : double.Parse(dr["rejectQty"].ToString());
                model.totalQty = dr["acountReading"].ToString() == "" ? 0 : double.Parse(dr["acountReading"].ToString());
                model.ipqcRejQty = dr["QCNGQTY"].ToString() == "" ? 0 : double.Parse(dr["QCNGQTY"].ToString());

                model.setUp = dr["Setup"].ToString() == "" ? 0 : double.Parse(dr["Setup"].ToString());
                model.wastageMaterial01 = dr["WastageMaterial01"].ToString() == "" ? 0 : double.Parse(dr["WastageMaterial01"].ToString());
                model.wastageMaterial02 = dr["WastageMaterial02"].ToString() == "" ? 0 : double.Parse(dr["WastageMaterial02"].ToString());
                model.userID = dr["userID"].ToString();
                model.supervisor = dr["SupervisorCheck"].ToString();
                model.startTime = DateTime.Parse(dr["startTime"].ToString());

                //如果stoptime为空, 则默认为当班次最后时刻.
                if (dr["stopTime"].ToString() == "")
                    model.stopTime = model.shift == "Day" ? model.day.AddHours(20) : model.day.AddDays(1).AddHours(8);
                else
                    model.stopTime = DateTime.Parse(dr["stopTime"].ToString());


                viList.Add(model);
            }


            if (sShift == "")
                return viList;
            else
                return (from a in viList where a.shift == sShift select a).ToList();

        }
        private List<ViewModel.MouldDailyVerifyReport_ViewModel.DefectInfo> GetDefectList(DateTime dDay, string sShift)
        {
            DataTable dtDefect = defectBLL.GetDefectForDailyReport(dDay, dDay.AddDays(1), "", "", "");
            if (dtDefect == null || dtDefect.Rows.Count == 0)
                return null;



            List<ViewModel.MouldDailyVerifyReport_ViewModel.DefectInfo> defectList = new List<ViewModel.MouldDailyVerifyReport_ViewModel.DefectInfo>();
            foreach (DataRow drDefect in dtDefect.Rows)
            {
                ViewModel.MouldDailyVerifyReport_ViewModel.DefectInfo model = new ViewModel.MouldDailyVerifyReport_ViewModel.DefectInfo();

                model.trackingID = drDefect["trackingID"].ToString();
                model.day = DateTime.Parse(drDefect["day"].ToString());
                model.shift = drDefect["shift"].ToString();
                model.machineID = drDefect["machineID"].ToString();
                model.partNo = drDefect["partNumber"].ToString();
                model.partNoALL = drDefect["partNumberAll"].ToString();



                model.whiteDot = int.Parse(drDefect["White Dot"].ToString());
                model.scratches = int.Parse(drDefect["Scratches"].ToString());
                model.dentedMark = int.Parse(drDefect["Dented Mark"].ToString());
                model.shinningDot = int.Parse(drDefect["Shinning Dot"].ToString());
                model.blackMark = int.Parse(drDefect["Black Mark"].ToString());
                model.sinkMark = int.Parse(drDefect["Sink Mark"].ToString());
                model.flowMark = int.Parse(drDefect["Flow Mark"].ToString());
                model.highGate = int.Parse(drDefect["High Gate"].ToString());
                model.silverSteak = int.Parse(drDefect["Silver Steak"].ToString());
                model.blackDot = int.Parse(drDefect["Black Dot"].ToString());
                model.oilStain = int.Parse(drDefect["Oil Stain"].ToString());
                model.flowLine = int.Parse(drDefect["Flow Line"].ToString());
                model.overCut = int.Parse(drDefect["Over-Cut"].ToString());
                model.crack = int.Parse(drDefect["Crack"].ToString());
                model.shortMold = int.Parse(drDefect["Short Mold"].ToString());
                model.stainMark = int.Parse(drDefect["Stain Mark"].ToString());
                model.weldLine = int.Parse(drDefect["Weld Line"].ToString());
                model.flashes = int.Parse(drDefect["Flashes"].ToString());
                model.foreignMaterial = int.Parse(drDefect["Foreign Materials"].ToString());
                model.drag = int.Parse(drDefect["Drag"].ToString());
                model.materialBleed = int.Parse(drDefect["Material Bleed"].ToString());
                model.bent = int.Parse(drDefect["Bent"].ToString());
                model.deform = int.Parse(drDefect["Deform"].ToString());
                model.gasMark = int.Parse(drDefect["Gas Mark"].ToString());



                defectList.Add(model);
            }



            if (sShift == "")
            {
                return defectList;
            }
            else
            {
                return (from a in defectList where a.shift == sShift select a).ToList();
            }
        }
        private string GetReportVerifyBy(DateTime dDate, string sShift)
        {
            Common.Class.BLL.MouldingCheckReport_BLL bll = new Common.Class.BLL.MouldingCheckReport_BLL();


            DataTable dt = bll.GetList(dDate, sShift);

            if (dt == null || dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["verityBy"].ToString();
            }
        }

        #endregion





        //rejTime Detail
        public List<ViewModel.MouldRejTimeDetail> GetRejTimeDetail(DateTime dDateFrom, DateTime dDateTo, string sShift, string sMachineID, string sPartNo, string sDefectCode, string sJigNo)
        {
            DataTable dt = defectBLL.GetRejTimeDetail(dDateFrom, dDateTo, sShift, sMachineID, sPartNo, sDefectCode, sJigNo);
            if (dt == null || dt.Rows.Count == 0)
                return null;


            List<ViewModel.MouldRejTimeDetail> modelList = new List<ViewModel.MouldRejTimeDetail>();
            foreach (DataRow dr in dt.Rows)
            {
                #region add hour 08
                if (dr["rejectQtyHour08"].ToString() != "")
                {
                    ViewModel.MouldRejTimeDetail model = new ViewModel.MouldRejTimeDetail();
                    model.date = DateTime.Parse(dr["day"].ToString());
                    model.shift = dr["shift"].ToString();
                    model.machineID = "Machine0" + dr["machineID"].ToString();
                    model.partNo = dr["partNumber"].ToString();
                    model.jigNo = dr["jigNo"].ToString();
                    model.defectCode = dr["defectCode"].ToString();
                    model.rejQty = double.Parse(dr["rejectQtyHour08"].ToString());

                    if (model.shift == "Night")
                        model.rejTime = "08:00 - 09:00";
                    else
                        model.rejTime = "20:00 - 21:00";

                    modelList.Add(model);
                }
                #endregion

                #region add hour 09
                if (dr["rejectQtyHour09"].ToString() != "")
                {
                    ViewModel.MouldRejTimeDetail model = new ViewModel.MouldRejTimeDetail();
                    model.date = DateTime.Parse(dr["day"].ToString());
                    model.shift = dr["shift"].ToString();
                    model.machineID = "Machine0" + dr["machineID"].ToString();
                    model.partNo = dr["partNumber"].ToString();
                    model.jigNo = dr["jigNo"].ToString();
                    model.defectCode = dr["defectCode"].ToString();
                    model.rejQty = double.Parse(dr["rejectQtyHour09"].ToString());

                    if (model.shift == "Day")
                        model.rejTime = "09:00 - 10:00";
                    else
                        model.rejTime = "21:00 - 22:00";

                    modelList.Add(model);
                }
                #endregion

                #region add hour 10
                if (dr["rejectQtyHour10"].ToString() != "")
                {
                    ViewModel.MouldRejTimeDetail model = new ViewModel.MouldRejTimeDetail();
                    model.date = DateTime.Parse(dr["day"].ToString());
                    model.shift = dr["shift"].ToString();
                    model.machineID = "Machine0" + dr["machineID"].ToString();
                    model.partNo = dr["partNumber"].ToString();
                    model.jigNo = dr["jigNo"].ToString();
                    model.defectCode = dr["defectCode"].ToString();
                    model.rejQty = double.Parse(dr["rejectQtyHour10"].ToString());

                    if (model.shift == "Day")
                        model.rejTime = "10:00 - 11:00";
                    else
                        model.rejTime = "22:00 - 23:00";

                    modelList.Add(model);
                }
                #endregion

                #region add hour 11
                if (dr["rejectQtyHour11"].ToString() != "")
                {
                    ViewModel.MouldRejTimeDetail model = new ViewModel.MouldRejTimeDetail();
                    model.date = DateTime.Parse(dr["day"].ToString());
                    model.shift = dr["shift"].ToString();
                    model.machineID = "Machine0" + dr["machineID"].ToString();
                    model.partNo = dr["partNumber"].ToString();
                    model.jigNo = dr["jigNo"].ToString();
                    model.defectCode = dr["defectCode"].ToString();
                    model.rejQty = double.Parse(dr["rejectQtyHour11"].ToString());

                    if (model.shift == "Day")
                        model.rejTime = "11:00 - 12:00";
                    else
                        model.rejTime = "23:00 - 24:00";

                    modelList.Add(model);
                }
                #endregion

                #region add hour 12
                if (dr["rejectQtyHour12"].ToString() != "")
                {
                    ViewModel.MouldRejTimeDetail model = new ViewModel.MouldRejTimeDetail();
                    model.date = DateTime.Parse(dr["day"].ToString());
                    model.shift = dr["shift"].ToString();
                    model.machineID = "Machine0" + dr["machineID"].ToString();
                    model.partNo = dr["partNumber"].ToString();
                    model.jigNo = dr["jigNo"].ToString();
                    model.defectCode = dr["defectCode"].ToString();
                    model.rejQty = double.Parse(dr["rejectQtyHour12"].ToString());

                    if (model.shift == "Day")
                        model.rejTime = "12:00 - 13:00";
                    else
                        model.rejTime = "00:00 - 01:00";

                    modelList.Add(model);
                }
                #endregion

                #region add hour 01
                if (dr["rejectQtyHour01"].ToString() != "")
                {
                    ViewModel.MouldRejTimeDetail model = new ViewModel.MouldRejTimeDetail();
                    model.date = DateTime.Parse(dr["day"].ToString());
                    model.shift = dr["shift"].ToString();
                    model.machineID = "Machine0" + dr["machineID"].ToString();
                    model.partNo = dr["partNumber"].ToString();
                    model.jigNo = dr["jigNo"].ToString();
                    model.defectCode = dr["defectCode"].ToString();
                    model.rejQty = double.Parse(dr["rejectQtyHour01"].ToString());

                    if (model.shift == "Day")
                        model.rejTime = "13:00 - 14:00";
                    else
                        model.rejTime = "01:00 - 02:00";

                    modelList.Add(model);
                }
                #endregion

                #region add hour 02
                if (dr["rejectQtyHour02"].ToString() != "")
                {
                    ViewModel.MouldRejTimeDetail model = new ViewModel.MouldRejTimeDetail();
                    model.date = DateTime.Parse(dr["day"].ToString());
                    model.shift = dr["shift"].ToString();
                    model.machineID = "Machine0" + dr["machineID"].ToString();
                    model.partNo = dr["partNumber"].ToString();
                    model.jigNo = dr["jigNo"].ToString();
                    model.defectCode = dr["defectCode"].ToString();
                    model.rejQty = double.Parse(dr["rejectQtyHour02"].ToString());

                    if (model.shift == "Day")
                        model.rejTime = "14:00 - 15:00";
                    else
                        model.rejTime = "02:00 - 03:00";

                    modelList.Add(model);
                }
                #endregion

                #region add hour 03
                if (dr["rejectQtyHour03"].ToString() != "")
                {
                    ViewModel.MouldRejTimeDetail model = new ViewModel.MouldRejTimeDetail();
                    model.date = DateTime.Parse(dr["day"].ToString());
                    model.shift = dr["shift"].ToString();
                    model.machineID = "Machine0" + dr["machineID"].ToString();
                    model.partNo = dr["partNumber"].ToString();
                    model.jigNo = dr["jigNo"].ToString();
                    model.defectCode = dr["defectCode"].ToString();
                    model.rejQty = double.Parse(dr["rejectQtyHour03"].ToString());

                    if (model.shift == "Day")
                        model.rejTime = "15:00 - 16:00";
                    else
                        model.rejTime = "03:00 - 04:00";

                    modelList.Add(model);
                }
                #endregion

                #region add hour 04
                if (dr["rejectQtyHour04"].ToString() != "")
                {
                    ViewModel.MouldRejTimeDetail model = new ViewModel.MouldRejTimeDetail();
                    model.date = DateTime.Parse(dr["day"].ToString());
                    model.shift = dr["shift"].ToString();
                    model.machineID = "Machine0" + dr["machineID"].ToString();
                    model.partNo = dr["partNumber"].ToString();
                    model.jigNo = dr["jigNo"].ToString();
                    model.defectCode = dr["defectCode"].ToString();
                    model.rejQty = double.Parse(dr["rejectQtyHour04"].ToString());

                    if (model.shift == "Day")
                        model.rejTime = "16:00 - 17:00";
                    else
                        model.rejTime = "04:00 - 05:00";

                    modelList.Add(model);
                }
                #endregion

                #region add hour 05
                if (dr["rejectQtyHour05"].ToString() != "")
                {
                    ViewModel.MouldRejTimeDetail model = new ViewModel.MouldRejTimeDetail();
                    model.date = DateTime.Parse(dr["day"].ToString());
                    model.shift = dr["shift"].ToString();
                    model.machineID = "Machine0" + dr["machineID"].ToString();
                    model.partNo = dr["partNumber"].ToString();
                    model.jigNo = dr["jigNo"].ToString();
                    model.defectCode = dr["defectCode"].ToString();
                    model.rejQty = double.Parse(dr["rejectQtyHour05"].ToString());

                    if (model.shift == "Day")
                        model.rejTime = "17:00 - 18:00";
                    else
                        model.rejTime = "05:00 - 06:00";

                    modelList.Add(model);
                }
                #endregion

                #region add hour 06
                if (dr["rejectQtyHour06"].ToString() != "")
                {
                    ViewModel.MouldRejTimeDetail model = new ViewModel.MouldRejTimeDetail();
                    model.date = DateTime.Parse(dr["day"].ToString());
                    model.shift = dr["shift"].ToString();
                    model.machineID = "Machine0" + dr["machineID"].ToString();
                    model.partNo = dr["partNumber"].ToString();
                    model.jigNo = dr["jigNo"].ToString();
                    model.defectCode = dr["defectCode"].ToString();
                    model.rejQty = double.Parse(dr["rejectQtyHour06"].ToString());

                    if (model.shift == "Day")
                        model.rejTime = "18:00 - 19:00";
                    else
                        model.rejTime = "06:00 - 07:00";

                    modelList.Add(model);
                }
                #endregion

                #region add hour 07
                if (dr["rejectQtyHour07"].ToString() != "")
                {
                    ViewModel.MouldRejTimeDetail model = new ViewModel.MouldRejTimeDetail();
                    model.date = DateTime.Parse(dr["day"].ToString());
                    model.shift = dr["shift"].ToString();
                    model.machineID = "Machine0" + dr["machineID"].ToString();
                    model.partNo = dr["partNumber"].ToString();
                    model.jigNo = dr["jigNo"].ToString();
                    model.defectCode = dr["defectCode"].ToString();
                    model.rejQty = double.Parse(dr["rejectQtyHour07"].ToString());

                    if (model.shift == "Day")
                        model.rejTime = "19:00 - 20:00";
                    else
                        model.rejTime = "07:00 - 08:00";

                    modelList.Add(model);
                }
                #endregion
            }

            return modelList;
        }
    }
}