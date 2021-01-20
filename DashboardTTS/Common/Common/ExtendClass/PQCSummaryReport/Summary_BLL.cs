using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taiyo.SearchParam.PQCParam;
using Taiyo.Enum.Production;
using Taiyo.Tool.Extension;

namespace Common.ExtendClass.PQCSummaryReport
{
    public class Summary_BLL
    {
        private readonly Summary_DAL _dal;
        public Summary_BLL()
        {
            _dal = new Summary_DAL();
        }




        /// <summary>
        /// 2021-1-18, 逻辑重构. 原本一个列表拆分成3个列表显示
        /// checking/wip由同一段逻辑生成, 通过type字段区分.
        /// 
        /// 
        /// job查询不做限制 -- 旧逻辑保留下来的
        /// 
        /// checking分laser,wip2个表格显示.
        /// laser btn: 
        ///     1.有laser工序, 并且当前工序为check1, 并且不包括num小分类.
        ///     2. num小分类独立显示
        /// 
        /// wip btn:
        ///     1. 没有laser工序的job.
        ///     2. 有laser工序, 但当前工序是check2的
        ///     3. 不包括num小分类.
        ///     4. num小分类独立显示
        /// </summary>
        /// <returns></returns>
        public List<Summary_Model.Report> GetCheckingList(PQCSummaryParam param)
        {
            var  detailList = _dal.GetCheckingList(param);
            if (detailList == null)
                return null;
            
            List<Summary_Model.Report> reportList = new List<Summary_Model.Report>();
            decimal totalQty = 0;
            decimal ttsMouldRej = 0;
            decimal vendorMouldRej = 0;
            decimal paintRej = 0;
            decimal laesrRej = 0;
            decimal othersRej = 0;
            decimal totalRej = 0;
            decimal passQty = 0;

            #region 添加 laser model
            var laserResult = from a in detailList
                              where bool.Parse(a.IsContainLaser)         //必须有laser工序
                              && a.CurrentProcess.ToUpper() == "CHECK#1" //且当前工序是check#1
                              && a.Number == ""                          //且不包含num的小分类
                              select a;
            if (laserResult != null && laserResult.Count() != 0)
            {
                totalQty = laserResult.Sum(p => p.TotalQty);
                ttsMouldRej = laserResult.Sum(p => p.TTSMouldRej);
                vendorMouldRej = laserResult.Sum(p => p.VendorMouldRej);
                paintRej = laserResult.Sum(p => p.PaintRej);
                laesrRej = laserResult.Sum(p => p.LaserRej);
                othersRej = laserResult.Sum(p => p.OthersRej);
                totalRej = ttsMouldRej + vendorMouldRej + paintRej + laesrRej + othersRej;
                passQty = laserResult.Sum(p => p.PassQty);

                Summary_Model.Report laserBtnModel = new Summary_Model.Report();
                laserBtnModel.Type = PQCReportType.Laser.GetDescription();
                laserBtnModel.PQCDept = "Laser Button";
                laserBtnModel.TotalOutput = totalQty;
                laserBtnModel.TTSMouldRej = string.Format("{0}({1}%)", ttsMouldRej, Math.Round(ttsMouldRej / totalQty * 100, 2).ToString("0.00"));
                laserBtnModel.VendorsMouldRej = string.Format("{0}({1}%)", vendorMouldRej, Math.Round(vendorMouldRej / totalQty * 100, 2).ToString("0.00"));
                laserBtnModel.PaintRej = string.Format("{0}({1}%)", paintRej, Math.Round(paintRej / totalQty * 100, 2).ToString("0.00"));
                laserBtnModel.LaserRej = string.Format("{0}({1}%)", laesrRej, Math.Round(laesrRej / totalQty * 100, 2).ToString("0.00"));
                laserBtnModel.OthersRej = string.Format("{0}({1}%)", othersRej, Math.Round(othersRej / totalQty * 100, 2).ToString("0.00"));
                laserBtnModel.TotalRej = string.Format("{0}({1}%)", totalRej, Math.Round(totalRej / totalQty * 100, 2).ToString("0.00"));
                laserBtnModel.ActualOutput = passQty;

                reportList.Add(laserBtnModel);
            }
            #endregion
            
            #region 添加 wip model
            var wipResult = from a in detailList
                            where (!bool.Parse(a.IsContainLaser))                                             //不包含laser工序的job
                                  || (bool.Parse(a.IsContainLaser) && a.CurrentProcess.ToUpper() != "CHECK#1")//以及有laser工序但不是check1的job
                                  && a.Number == ""                                                           //并且不包括num小分类
                            select a;

            if (wipResult != null && wipResult.Count() != 0)
            {
                totalQty = wipResult.Sum(p => p.TotalQty);
                ttsMouldRej = wipResult.Sum(p => p.TTSMouldRej);
                vendorMouldRej = wipResult.Sum(p => p.VendorMouldRej);
                paintRej = wipResult.Sum(p => p.PaintRej);
                laesrRej = wipResult.Sum(p => p.LaserRej);
                othersRej = wipResult.Sum(p => p.OthersRej);
                totalRej = ttsMouldRej + vendorMouldRej + paintRej + laesrRej + othersRej;
                passQty = wipResult.Sum(p => p.PassQty);

                var wipModel = new Summary_Model.Report();
                wipModel.Type = PQCReportType.WIP.GetDescription();
                wipModel.PQCDept = "WIP Button";
                wipModel.TotalOutput = totalQty;
                wipModel.TTSMouldRej = string.Format("{0}({1}%)", ttsMouldRej, Math.Round(ttsMouldRej / totalQty * 100, 2).ToString("0.00"));
                wipModel.VendorsMouldRej = string.Format("{0}({1}%)", vendorMouldRej, Math.Round(vendorMouldRej / totalQty * 100, 2).ToString("0.00"));
                wipModel.PaintRej = string.Format("{0}({1}%)", paintRej, Math.Round(paintRej / totalQty * 100, 2).ToString("0.00"));
                wipModel.LaserRej = string.Format("{0}({1}%)", laesrRej, Math.Round(laesrRej / totalQty * 100, 2).ToString("0.00"));
                wipModel.OthersRej = string.Format("{0}({1}%)", othersRej, Math.Round(othersRej / totalQty * 100, 2).ToString("0.00"));
                wipModel.TotalRej = string.Format("{0}({1}%)", totalRej, Math.Round(totalRej / totalQty * 100, 2).ToString("0.00"));
                wipModel.ActualOutput = passQty;

                reportList.Add(wipModel);
            }
            #endregion
            
            #region  按num汇总
            //获取bom中所有的number
            Common.Class.BLL.PQCBom_BLL bomBLL = new Common.Class.BLL.PQCBom_BLL();
            List<string> numList = bomBLL.GetNumberList();
            var laserNumList = from a in detailList
                               where a.Number != ""
                               && a.CurrentProcess.ToUpper() == "CHECK#1"
                               && bool.Parse(a.IsContainLaser)
                               group a by a.Number into numGroup
                               select new
                               {
                                   Type = PQCReportType.Laser.GetDescription(),
                                   Number = numGroup.Key,
                                   PQCDept = $"{numGroup.Max(p => p.Description)} {numGroup.Key}",

                                   TotalQty = numGroup.Sum(p => p.TotalQty),
                                   PassQty = numGroup.Sum(p => p.PassQty),

                                   TTSMouldRej = numGroup.Sum(p => p.TTSMouldRej),
                                   VendorMouldRej = numGroup.Sum(p => p.VendorMouldRej),
                                   PaintRej = numGroup.Sum(p => p.PaintRej),
                                   LaserRej = numGroup.Sum(p => p.LaserRej),
                                   OthersRej = numGroup.Sum(p => p.OthersRej)
                               };

            var wipNumList = from a in detailList
                             where
                             (
                              !bool.Parse(a.IsContainLaser) ||                                              //没有laser工序
                              (bool.Parse(a.IsContainLaser) && a.CurrentProcess.ToUpper() != "CHECK#1")     //或者又laser工序但不是check#1的
                             )
                             &&
                             a.Number != ""
                             group a by a.Number into numGroup
                             select new
                             {
                                 Type = PQCReportType.Laser.GetDescription(),
                                 Number = numGroup.Key,
                                 PQCDept = $"{numGroup.Max(p => p.Description)} {numGroup.Key}",

                                 TotalQty = numGroup.Sum(p => p.TotalQty),
                                 PassQty = numGroup.Sum(p => p.PassQty),

                                 TTSMouldRej = numGroup.Sum(p => p.TTSMouldRej),
                                 VendorMouldRej = numGroup.Sum(p => p.VendorMouldRej),
                                 PaintRej = numGroup.Sum(p => p.PaintRej),
                                 LaserRej = numGroup.Sum(p => p.LaserRej),
                                 OthersRej = numGroup.Sum(p => p.OthersRej)
                             };





            //添加 num小分类的model
            foreach (string num in numList)
            {
                var laserNumModel = laserNumList.Where(p => p.Number == num).FirstOrDefault();
                if (laserNumModel!= null)
                {
                    totalQty = laserNumModel.TotalQty;
                    ttsMouldRej = laserNumModel.TTSMouldRej;
                    vendorMouldRej = laserNumModel.VendorMouldRej;
                    paintRej = laserNumModel.PaintRej;
                    laesrRej = laserNumModel.LaserRej;
                    othersRej = laserNumModel.OthersRej;
                    totalRej = ttsMouldRej + vendorMouldRej + paintRej + laesrRej + othersRej;
                    passQty = laserNumModel.PassQty;


                    var reportNumModel = new Summary_Model.Report();
                    reportNumModel.Type = PQCReportType.Laser.GetDescription();
                    reportNumModel.PQCDept = laserNumModel.PQCDept;
                    reportNumModel.TotalOutput = totalQty;
                    reportNumModel.TTSMouldRej = string.Format("{0}({1}%)", ttsMouldRej, Math.Round(ttsMouldRej / totalQty * 100, 2).ToString("0.00"));
                    reportNumModel.VendorsMouldRej = string.Format("{0}({1}%)", vendorMouldRej, Math.Round(vendorMouldRej / totalQty * 100, 2).ToString("0.00"));
                    reportNumModel.PaintRej = string.Format("{0}({1}%)", paintRej, Math.Round(paintRej / totalQty * 100, 2).ToString("0.00"));
                    reportNumModel.LaserRej = string.Format("{0}({1}%)", laesrRej, Math.Round(laesrRej / totalQty * 100, 2).ToString("0.00"));
                    reportNumModel.OthersRej = string.Format("{0}({1}%)", othersRej, Math.Round(othersRej / totalQty * 100, 2).ToString("0.00"));
                    reportNumModel.TotalRej = string.Format("{0}({1}%)", totalRej, Math.Round(totalRej / totalQty * 100, 2).ToString("0.00"));
                    reportNumModel.ActualOutput = passQty;

                    reportList.Add(reportNumModel);
                } 


                var wipNumModel = wipNumList.Where(p => p.Number == num).FirstOrDefault();
                if (wipNumModel != null)
                {
                    totalQty = wipNumModel.TotalQty;
                    ttsMouldRej = wipNumModel.TTSMouldRej;
                    vendorMouldRej = wipNumModel.VendorMouldRej;
                    paintRej = wipNumModel.PaintRej;
                    laesrRej = wipNumModel.LaserRej;
                    othersRej = wipNumModel.OthersRej;
                    totalRej = ttsMouldRej + vendorMouldRej + paintRej + laesrRej + othersRej;
                    passQty = wipNumModel.PassQty;


                    var reportNumModel = new Summary_Model.Report();
                    reportNumModel.Type = PQCReportType.WIP.GetDescription();
                    reportNumModel.PQCDept = wipNumModel.PQCDept;
                    reportNumModel.TotalOutput = totalQty;
                    reportNumModel.TTSMouldRej = string.Format("{0}({1}%)", ttsMouldRej, Math.Round(ttsMouldRej / totalQty * 100, 2).ToString("0.00"));
                    reportNumModel.VendorsMouldRej = string.Format("{0}({1}%)", vendorMouldRej, Math.Round(vendorMouldRej / totalQty * 100, 2).ToString("0.00"));
                    reportNumModel.PaintRej = string.Format("{0}({1}%)", paintRej, Math.Round(paintRej / totalQty * 100, 2).ToString("0.00"));
                    reportNumModel.LaserRej = string.Format("{0}({1}%)", laesrRej, Math.Round(laesrRej / totalQty * 100, 2).ToString("0.00"));
                    reportNumModel.OthersRej = string.Format("{0}({1}%)", othersRej, Math.Round(othersRej / totalQty * 100, 2).ToString("0.00"));
                    reportNumModel.TotalRej = string.Format("{0}({1}%)", totalRej, Math.Round(totalRej / totalQty * 100, 2).ToString("0.00"));
                    reportNumModel.ActualOutput = passQty;

                    reportList.Add(reportNumModel);
                }
            }
            #endregion
            
            #region 添加 laser total
            var allLaserList = reportList.Where(p => p.Type == PQCReportType.Laser.GetDescription());
            if (allLaserList != null && allLaserList.Count() != 0)
            {
                decimal laserTotalQty = allLaserList.Sum(p => p.TotalOutput);
                decimal laserTotalPass = allLaserList.Sum(p => p.ActualOutput);
                decimal laserTotalTTSRej = allLaserList.Sum(p => decimal.Parse(p.TTSMouldRej.Split('(')[0]));
                decimal laserTotalVendorRej = allLaserList.Sum(p => decimal.Parse(p.VendorsMouldRej.Split('(')[0]));
                decimal laserTotalPaintRej = allLaserList.Sum(p => decimal.Parse(p.PaintRej.Split('(')[0]));
                decimal laserTotalLaserRej = allLaserList.Sum(p => decimal.Parse(p.LaserRej.Split('(')[0]));
                decimal laserTotalOthersRej = allLaserList.Sum(p => decimal.Parse(p.OthersRej.Split('(')[0]));
                decimal laserTotalRej = laserTotalTTSRej + laserTotalVendorRej + laserTotalPaintRej + laserTotalLaserRej + laserTotalOthersRej;


                var laserSummaryReport = new Summary_Model.Report();
                laserSummaryReport.Type = PQCReportType.Laser.GetDescription();
                laserSummaryReport.PQCDept = "Total";
                laserSummaryReport.TotalOutput = laserTotalQty;
                laserSummaryReport.TTSMouldRej = string.Format("{0}({1}%)", laserTotalTTSRej, Math.Round(laserTotalTTSRej / laserTotalQty * 100, 2).ToString("0.00"));
                laserSummaryReport.VendorsMouldRej = string.Format("{0}({1}%)", laserTotalVendorRej, Math.Round(laserTotalVendorRej / laserTotalQty * 100, 2).ToString("0.00"));
                laserSummaryReport.PaintRej = string.Format("{0}({1}%)", laserTotalPaintRej, Math.Round(laserTotalPaintRej / laserTotalQty * 100, 2).ToString("0.00"));
                laserSummaryReport.LaserRej = string.Format("{0}({1}%)", laserTotalLaserRej, Math.Round(laserTotalLaserRej / laserTotalQty * 100, 2).ToString("0.00"));
                laserSummaryReport.OthersRej = string.Format("{0}({1}%)", laserTotalOthersRej, Math.Round(laserTotalOthersRej / laserTotalQty * 100, 2).ToString("0.00"));
                laserSummaryReport.TotalRej = string.Format("{0}({1}%)", laserTotalRej, Math.Round(laserTotalRej / laserTotalQty * 100, 2).ToString("0.00"));
                laserSummaryReport.ActualOutput = laserTotalPass;

                reportList.Add(laserSummaryReport);
            }
            #endregion

            #region 添加 wip total
            var allWIPList = reportList.Where(p => p.Type == PQCReportType.WIP.GetDescription());
            if (allWIPList != null && allWIPList.Count() != 0)
            {
                decimal wipTotalQty = allWIPList.Sum(p => p.TotalOutput);
                decimal wipTotalPass = allWIPList.Sum(p => p.ActualOutput);
                decimal wipTotalTTSRej = allWIPList.Sum(p => decimal.Parse(p.TTSMouldRej.Split('(')[0]));
                decimal wipTotalVendorRej = allWIPList.Sum(p => decimal.Parse(p.VendorsMouldRej.Split('(')[0]));
                decimal wipTotalPaintRej = allWIPList.Sum(p => decimal.Parse(p.PaintRej.Split('(')[0]));
                decimal wipTotalLaserRej = allWIPList.Sum(p => decimal.Parse(p.LaserRej.Split('(')[0]));
                decimal wipTotalOthersRej = allWIPList.Sum(p => decimal.Parse(p.OthersRej.Split('(')[0]));
                decimal wipTotalRej = wipTotalTTSRej + wipTotalVendorRej + wipTotalPaintRej + wipTotalLaserRej + wipTotalOthersRej;


                var wipSummaryReport = new Summary_Model.Report();
                wipSummaryReport.Type = PQCReportType.WIP.GetDescription();
                wipSummaryReport.PQCDept = "Total";
                wipSummaryReport.TotalOutput = wipTotalQty;
                wipSummaryReport.TTSMouldRej = string.Format("{0}({1}%)", wipTotalTTSRej, Math.Round(wipTotalTTSRej / wipTotalQty * 100, 2).ToString("0.00"));
                wipSummaryReport.VendorsMouldRej = string.Format("{0}({1}%)", wipTotalVendorRej, Math.Round(wipTotalVendorRej / wipTotalQty * 100, 2).ToString("0.00"));
                wipSummaryReport.PaintRej = string.Format("{0}({1}%)", wipTotalPaintRej, Math.Round(wipTotalPaintRej / wipTotalQty * 100, 2).ToString("0.00"));
                wipSummaryReport.LaserRej = string.Format("{0}({1}%)", wipTotalLaserRej, Math.Round(wipTotalLaserRej / wipTotalQty * 100, 2).ToString("0.00"));
                wipSummaryReport.OthersRej = string.Format("{0}({1}%)", wipTotalOthersRej, Math.Round(wipTotalOthersRej / wipTotalQty * 100, 2).ToString("0.00"));
                wipSummaryReport.TotalRej = string.Format("{0}({1}%)", wipTotalRej, Math.Round(wipTotalRej / wipTotalQty * 100, 2).ToString("0.00"));
                wipSummaryReport.ActualOutput = wipTotalPass;

                reportList.Add(wipSummaryReport);
            }            
            #endregion


            return reportList;
        }



        /// <summary>
        /// 2021-1-18, 逻辑重构. 原本一个列表拆分成3个列表显示
        /// 用于生成packing列表信息.
        /// 
        /// 
        /// job查询不做限制 -- 旧逻辑保留下来的
        /// 
        /// packing分online, offline
        /// online: 有laser工序, 并且只check一次的为online
        /// offline: 1. 没有laser工序
        ///          2. 有laser工序, 并且check不止一次的为offline
        /// </summary>
        /// <returns></returns>
        public List<Summary_Model.Report> GetPackingList(PQCSummaryParam param)
        {
            var detailList = _dal.GetPackingList(param);
            if (detailList == null)
                return null;

            //汇总packing online部分
            var onlineList = from a in detailList                                                           //同checking的逻辑区分online, offline
                             where bool.Parse(a.IsContainLaser) && a.LastCheckProcess.ToUpper() == "CHECK#1"  //有laser工序, 并且只check一次的为online
                             select a;

            decimal onlineTotalQty = 0;
            decimal onlinePassQty = 0;
            decimal onlineRej = 0;
            string onlineRejRate = "0.00%";
            if (onlineList !=null &&onlineList.Count()!= 0)
            {
                onlineTotalQty = onlineList.Sum(p => p.TotalQty);
                onlinePassQty = onlineList.Sum(p => p.PassQty);
                onlineRej = onlineList.Sum(p => p.RejQty);
                onlineRejRate = onlineTotalQty == 0 ? "0.00%" : Math.Round(onlineRej / onlineTotalQty * 100, 2).ToString("0.00") + "%";
            }
            


            //汇总packing offline部分
            var offlineList = from a in detailList
                              where (!bool.Parse(a.IsContainLaser))                                         //没有laser工序 或者
                              || (bool.Parse(a.IsContainLaser) && a.LastCheckProcess.ToUpper() != "CHECK#1")  //有laser工序, 并且check不止一次的为offline
                              select a;

            decimal offlineTotalQty = 0;
            decimal offlinePassQty = 0;
            decimal offlineRej = 0;
            string offlineRejRate = "0.00%";
            if (offlineList != null && offlineList.Count()!= 0)
            {
                offlineTotalQty = offlineList.Sum(p => p.TotalQty);
                offlinePassQty = offlineList.Sum(p => p.PassQty);
                offlineRej = offlineList.Sum(p => p.RejQty);
                offlineRejRate = offlineTotalQty == 0 ? "0.00%" : Math.Round(offlineRej / offlineTotalQty * 100, 2).ToString("0.00") + "%";
            }
            

            return new List<Summary_Model.Report>() {
                new Summary_Model.Report()
                {
                    Type = PQCReportType.Packing.GetDescription(),
                    PQCDept = "On-Line",
                    TotalOutput = onlineTotalQty,
                    ActualOutput = onlinePassQty,
                    TotalRej = onlineRejRate
                },
                new Summary_Model.Report()
                {
                    Type = PQCReportType.Packing.GetDescription(),
                    PQCDept = "Off-Line",
                    TotalOutput = offlineTotalQty,
                    ActualOutput = offlinePassQty,
                    TotalRej = offlineRejRate
                },
                new Summary_Model.Report()
                {
                    Type = PQCReportType.Packing.GetDescription(),
                    PQCDept = "Total",
                    TotalOutput = detailList.Sum(p => p.TotalQty),
                    ActualOutput = detailList.Sum(p => p.PassQty),
                    TotalRej = string.Format("{0}({1}%)",detailList.Sum(p => p.RejQty),Math.Round(detailList.Sum(p => p.RejQty)/detailList.Sum(p => p.TotalQty)*100,2).ToString("0.00"))
                }
            };
        }
    }
}
