using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardTTS.ViewBusiness
{
    public class MOULDMachineStatus
    {

        private readonly Common.Class.BLL.MouldingMachineStatus_BLL statusBLL = new Common.Class.BLL.MouldingMachineStatus_BLL();

        public MOULDMachineStatus()
        {

        }

        
        public List<ViewModel.MouldMachineStatus.Summary> GetMachineSummaryList(DateTime dDateFrom , DateTime dDateTo, string sShift, string sDateNotIn, bool bExceptWeekend)
        {
            List<ViewModel.MouldMachineStatus.Summary> summaryList = GetMachineSummaryData(dDateFrom, dDateTo, sShift, sDateNotIn, bExceptWeekend);


            foreach (ViewModel.MouldMachineStatus.Summary model in summaryList)
            {
                model.running = Common.CommFunctions.ConvertDateTimeShort((double.Parse( model.running) / 3600).ToString());
                model.adjustment = Common.CommFunctions.ConvertDateTimeShort((double.Parse(model.adjustment) / 3600).ToString());
                model.noSchedule = Common.CommFunctions.ConvertDateTimeShort((double.Parse(model.noSchedule) / 3600).ToString());
                model.mouldTesting = Common.CommFunctions.ConvertDateTimeShort((double.Parse(model.mouldTesting) / 3600).ToString());
                model.materialTesting = Common.CommFunctions.ConvertDateTimeShort((double.Parse(model.materialTesting) / 3600).ToString());
                model.changeModel = Common.CommFunctions.ConvertDateTimeShort((double.Parse(model.changeModel) / 3600).ToString());
                model.noOperator = Common.CommFunctions.ConvertDateTimeShort((double.Parse(model.noOperator) / 3600).ToString());
                model.mcStop = Common.CommFunctions.ConvertDateTimeShort((double.Parse(model.mcStop) / 3600).ToString());
                model.mouldDamage = Common.CommFunctions.ConvertDateTimeShort((double.Parse(model.mouldDamage) / 3600).ToString());
                model.breakdown = Common.CommFunctions.ConvertDateTimeShort((double.Parse(model.breakdown) / 3600).ToString());
                model.meal = Common.CommFunctions.ConvertDateTimeShort((double.Parse(model.meal) / 3600).ToString());
                model.shutdown = Common.CommFunctions.ConvertDateTimeShort((double.Parse(model.shutdown) / 3600).ToString());
            }

            return summaryList;
        }
        
        public List<ViewModel.MouldMachineStatus.SummaryChart> GetMachineSummaryChart(DateTime dDateFrom, DateTime dDateTo, string sShift, string sDateNotIn, bool bExceptWeekend)
        {
            List<ViewModel.MouldMachineStatus.SummaryChart> chartList = new List<ViewModel.MouldMachineStatus.SummaryChart>();
            double totalSeconds = Common.CommFunctions.GetTotalSeconds(dDateFrom, dDateTo, sShift, sDateNotIn, bExceptWeekend);




            //获取每台机器单独的 utilization
            List<ViewModel.MouldMachineStatus.Summary> summaryList = GetMachineSummaryData(dDateFrom, dDateTo, sShift, sDateNotIn, bExceptWeekend);
            foreach (ViewModel.MouldMachineStatus.Summary model in summaryList)
            {
                if (model.machineID.ToUpper().Contains("TOTAL"))
                    continue;

                
                ViewModel.MouldMachineStatus.SummaryChart chartModel = new ViewModel.MouldMachineStatus.SummaryChart();
                chartModel.asixNo = model.machineID;
                chartModel.utilization = totalSeconds == 0 ? 0 : Math.Round( double.Parse( model.running) / totalSeconds * 100, 2);
                chartList.Add(chartModel);
            }




            //获取双色注塑机的 utilization 
            var mcDoubleList = from a in summaryList where (new string[] { "Machine1", "Machine2" , "Machine3", "Machine4" , "Machine5", "Machine6" }).Contains(a.machineID) select a;
            double doubleColorRunSeconds = mcDoubleList.Sum(p => double.Parse(p.running));

            ViewModel.MouldMachineStatus.SummaryChart doubleColorModel = new ViewModel.MouldMachineStatus.SummaryChart();
            doubleColorModel.asixNo = "Double Color(1~6)";
            doubleColorModel.utilization = totalSeconds == 0 ? 0 : Math.Round(doubleColorRunSeconds / totalSeconds / 6 * 100, 2);
            chartList.Add(doubleColorModel);


            //获取单色注塑机的 utilization 
            var mcSingleList = from a in summaryList where (new string[] { "Machine7", "Machine8", "Machine9" }).Contains(a.machineID) select a;
            double singleColorRunSeconds = mcSingleList.Sum(p => double.Parse(p.running));

            ViewModel.MouldMachineStatus.SummaryChart singleColorModel = new ViewModel.MouldMachineStatus.SummaryChart();
            singleColorModel.asixNo = "Single Color(7~9)";
            singleColorModel.utilization = totalSeconds == 0 ? 0 : Math.Round(singleColorRunSeconds / totalSeconds / 3 * 100, 2);
            chartList.Add(singleColorModel);


            return chartList;
        }
        
        public List<ViewModel.MouldMachineStatus.StatusChart> GetStatusChart(DateTime dDateFrom, DateTime dDateTo, string sShift)
        {
            List<ViewModel.MouldMachineStatus.StatusChart> statusList = new List<ViewModel.MouldMachineStatus.StatusChart>();



            double totalSeconds = Common.CommFunctions.GetTotalSeconds(dDateFrom, dDateTo, sShift, "", false);

            List<ViewModel.MouldMachineStatus.Summary> summaryList = GetMachineSummaryData(dDateFrom, dDateTo, sShift, "", false);
            foreach (var model in summaryList)
            {
                if (model.machineID.ToUpper().Contains("TOTAL"))
                    continue;

                
                ViewModel.MouldMachineStatus.StatusChart statusModel = new ViewModel.MouldMachineStatus.StatusChart();
                statusModel.machineID = model.machineID;
                statusModel.running = totalSeconds ==0? 0: Math.Round(double.Parse(model.running) / totalSeconds * 100, 2);
                statusModel.adjustment = totalSeconds == 0 ? 0 : Math.Round(double.Parse(model.adjustment) / totalSeconds * 100, 2);
                statusModel.noSchedule = totalSeconds == 0 ? 0 : Math.Round(double.Parse(model.noSchedule) / totalSeconds * 100, 2);
                statusModel.mouldTesting = totalSeconds == 0 ? 0 : Math.Round(double.Parse(model.mouldTesting) / totalSeconds * 100, 2);
                statusModel.materialTesting = totalSeconds == 0 ? 0 : Math.Round(double.Parse(model.materialTesting) / totalSeconds * 100, 2);
                statusModel.changeModel = totalSeconds == 0 ? 0 : Math.Round(double.Parse(model.changeModel) / totalSeconds * 100, 2);
                statusModel.noOperator = totalSeconds == 0 ? 0 : Math.Round(double.Parse(model.noOperator) / totalSeconds * 100, 2);
                statusModel.mcStop = totalSeconds == 0 ? 0 : Math.Round(double.Parse(model.mcStop) / totalSeconds * 100, 2);
                statusModel.mouldDamage = totalSeconds == 0 ? 0 : Math.Round(double.Parse(model.mouldDamage) / totalSeconds * 100, 2);
                statusModel.breakdown = totalSeconds == 0 ? 0 : Math.Round(double.Parse(model.breakdown) / totalSeconds * 100, 2);
                statusModel.meal = totalSeconds == 0 ? 0 : Math.Round(double.Parse(model.meal) / totalSeconds * 100, 2);
                statusModel.shutdown = totalSeconds == 0 ? 0 : Math.Round(double.Parse(model.shutdown) / totalSeconds * 100, 2);

                statusList.Add(statusModel);
            }

            return statusList;
        }

        



        //时间都是按seconds 计算
        private List<ViewModel.MouldMachineStatus.Summary> GetMachineSummaryData(DateTime dDateFrom, DateTime dDateTo, string sShift, string sDateNotIn, bool bExceptWeekend)
        {
            List<ViewModel.MouldMachineStatus.Summary> summaryList = new List<ViewModel.MouldMachineStatus.Summary>();


            List<Common.Class.Model.MouldingMachineStatus_Model> machineStatusList = statusBLL.GetModelList(dDateFrom, dDateTo, sShift, "", "");
            if (machineStatusList == null || machineStatusList.Count == 0)
            {
                #region no record set default
                for (int i = 1; i < 10; i++)
                {
                    ViewModel.MouldMachineStatus.Summary model = new ViewModel.MouldMachineStatus.Summary();
                    model.machineID = "Machine" + i.ToString();
                    model.running = "0";
                    model.adjustment = "0";
                    model.noSchedule = "0";
                    model.mouldTesting = "0";
                    model.materialTesting = "0";
                    model.changeModel = "0";
                    model.noOperator = "0";
                    model.mcStop = "0";
                    model.mouldDamage = "0";
                    model.breakdown = "0";
                    model.meal = "0";
                    model.shutdown = "0";
                    model.utilization = 0;
                    summaryList.Add(model);
                }

                ViewModel.MouldMachineStatus.Summary summaryModel1 = new ViewModel.MouldMachineStatus.Summary();
                summaryModel1.machineID = "Total:";
                summaryModel1.running = "0";
                summaryModel1.adjustment = "0";
                summaryModel1.noSchedule = "0";
                summaryModel1.mouldTesting = "0";
                summaryModel1.materialTesting = "0";
                summaryModel1.changeModel = "0";
                summaryModel1.noOperator = "0";
                summaryModel1.mcStop = "0";
                summaryModel1.mouldDamage = "0";
                summaryModel1.breakdown = "0";
                summaryModel1.meal = "0";
                summaryModel1.shutdown = "0";
                summaryModel1.utilization = 0;
                summaryList.Add(summaryModel1);


                return summaryList;
                #endregion
            }

            //排除 date not in, except weekend 
            var tempList = from a in machineStatusList
                           where a.Day.DayOfWeek != DayOfWeek.Sunday && a.Day.DayOfWeek != DayOfWeek.Saturday
                           && (!sDateNotIn.Split('/').Contains(a.Day.Day.ToString()))
                           select a;

            double totalSeconds = Common.CommFunctions.GetTotalSeconds(dDateFrom, dDateTo, sShift, sDateNotIn, bExceptWeekend);

            for (int i = 1; i < 10; i++)
            {
                ViewModel.MouldMachineStatus.Summary model = new ViewModel.MouldMachineStatus.Summary();
                model.machineID = "Machine" + i.ToString();




                var machineList = from a in tempList where a.MachineID == i.ToString() select a;

                if (machineList == null)
                {
                    #region no machine record , set default 0
                    model.running = "0";
                    model.adjustment = "0";
                    model.noSchedule = "0";
                    model.mouldTesting = "0";
                    model.materialTesting = "0";
                    model.changeModel = "0";
                    model.noOperator = "0";
                    model.mcStop = "0";
                    model.mouldDamage = "0";
                    model.breakdown = "0";
                    model.meal = "0";
                    model.shutdown = "0";
                    model.utilization = 0;
                    #endregion
                }
                else
                {
                    var runList = from a in machineList where a.MachineStatus == StaticRes.Global.MouldingMachineStatus.Running select a;
                    model.running = runList == null ? "0" : runList.Sum(p => (p.EndTime - p.StartTime).TotalSeconds).ToString();

                    var adjustmentList = from a in machineList where a.MachineStatus == StaticRes.Global.MouldingMachineStatus.Adjustment select a;
                    model.adjustment = adjustmentList == null ? "0" : adjustmentList.Sum(p => (p.EndTime - p.StartTime).TotalSeconds).ToString();

                    var noScheduleList = from a in machineList where a.MachineStatus == StaticRes.Global.MouldingMachineStatus.NoSchedule select a;
                    model.noSchedule = noScheduleList == null ? "0" : noScheduleList.Sum(p => (p.EndTime - p.StartTime).TotalSeconds).ToString();

                    var mouldTestingList = from a in machineList where a.MachineStatus == StaticRes.Global.MouldingMachineStatus.MouldTesting select a;
                    model.mouldTesting = mouldTestingList == null ? "0" : mouldTestingList.Sum(p => (p.EndTime - p.StartTime).TotalSeconds).ToString();

                    var materialTestingList = from a in machineList where a.MachineStatus == StaticRes.Global.MouldingMachineStatus.MaterialTesting select a;
                    model.materialTesting = materialTestingList == null ? "0" : materialTestingList.Sum(p => (p.EndTime - p.StartTime).TotalSeconds).ToString();

                    var changeModelList = from a in machineList where a.MachineStatus == StaticRes.Global.MouldingMachineStatus.ChangeModel select a;
                    model.changeModel = changeModelList == null ? "0" : changeModelList.Sum(p => (p.EndTime - p.StartTime).TotalSeconds).ToString();

                    var noOperatorList = from a in machineList where a.MachineStatus == StaticRes.Global.MouldingMachineStatus.NoOperator select a;
                    model.noOperator = noOperatorList == null ? "0" : noOperatorList.Sum(p => (p.EndTime - p.StartTime).TotalSeconds).ToString();

                    var mcStopList = from a in machineList where a.MachineStatus == StaticRes.Global.MouldingMachineStatus.McStop select a;
                    model.mcStop = mcStopList == null ? "0" : mcStopList.Sum(p => (p.EndTime - p.StartTime).TotalSeconds).ToString();

                    var mouldDamageList = from a in machineList where a.MachineStatus == StaticRes.Global.MouldingMachineStatus.DamageMould select a;
                    model.mouldDamage = mouldDamageList == null ? "0" : mouldDamageList.Sum(p => (p.EndTime - p.StartTime).TotalSeconds).ToString();

                    var breakdownList = from a in machineList where a.MachineStatus == StaticRes.Global.MouldingMachineStatus.MachineBreak select a;
                    model.breakdown = breakdownList == null ? "0" : breakdownList.Sum(p => (p.EndTime - p.StartTime).TotalSeconds).ToString();

                    var mealList = from a in machineList where a.MachineStatus == StaticRes.Global.MouldingMachineStatus.Meal select a;
                    model.meal = mealList == null ? "0" : mealList.Sum(p => (p.EndTime - p.StartTime).TotalSeconds).ToString();

                    var shutdownList = from a in machineList where a.MachineStatus == StaticRes.Global.MouldingMachineStatus.ShutDown select a;
                    model.shutdown = shutdownList == null ? "0" : shutdownList.Sum(p => (p.EndTime - p.StartTime).TotalSeconds).ToString();



                    model.utilization = totalSeconds == 0 ? 0 : Math.Round(double.Parse(model.running) / totalSeconds * 100, 2);
                }


                summaryList.Add(model);
            }


            ViewModel.MouldMachineStatus.Summary summaryModel = new ViewModel.MouldMachineStatus.Summary();
            summaryModel.machineID = "Total :";
            summaryModel.running = summaryList.Sum(p => double.Parse(p.running)).ToString();
            summaryModel.adjustment = summaryList.Sum(p => double.Parse(p.adjustment)).ToString();
            summaryModel.noSchedule = summaryList.Sum(p => double.Parse(p.noSchedule)).ToString();
            summaryModel.mouldTesting = summaryList.Sum(p => double.Parse(p.mouldTesting)).ToString();
            summaryModel.materialTesting = summaryList.Sum(p => double.Parse(p.materialTesting)).ToString();
            summaryModel.changeModel = summaryList.Sum(p => double.Parse(p.changeModel)).ToString();
            summaryModel.noOperator = summaryList.Sum(p => double.Parse(p.noOperator)).ToString();
            summaryModel.mcStop = summaryList.Sum(p => double.Parse(p.mcStop)).ToString();
            summaryModel.mouldDamage = summaryList.Sum(p => double.Parse(p.mouldDamage)).ToString();
            summaryModel.breakdown = summaryList.Sum(p => double.Parse(p.breakdown)).ToString();
            summaryModel.meal = summaryList.Sum(p => double.Parse(p.meal)).ToString();
            summaryModel.shutdown = summaryList.Sum(p => double.Parse(p.shutdown)).ToString();
            summaryModel.utilization = totalSeconds == 0 ? 0 : Math.Round(summaryList.Sum(p => double.Parse(p.running)) / totalSeconds / summaryList.Count() * 100, 2);
            summaryList.Add(summaryModel);


            return summaryList;
        }



    }
}