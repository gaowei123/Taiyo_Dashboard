using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taiyo.SearchParam;
using Taiyo.Enum.Organization;
using Taiyo.Tool.Extension;

namespace Common.ExtendClass.Attendance
{
    public class DailySummary_BLL
    {
        private readonly Base_BLL _bll;

        public DailySummary_BLL()
        {
            _bll = new Base_BLL();
        }



        public List<DailySummary_Model> GetDailySummaryList(BaseParam param)
        {
            List<Attendance_Model> departmentAttendanceList = _bll.GetDepartmentAttendanceList(param);
            if (departmentAttendanceList == null)
                return null;

            Dictionary<Department, decimal> dicDeparmentUserCount = _bll.GetDepartmentUserCount();


            //用于计算summary row中的 excludedAL,includedAL.
            decimal allExcludedAL = 0;
            decimal allIncludedAL = 0;


            List<DailySummary_Model> resultList = new List<DailySummary_Model>();
            foreach (Department item in Enum.GetValues(typeof(Department)))
            {
                #region foreach
                //排除一些不需要的部门.
                if (item == Department.Online || item == Department.Packing || item == Department.WIP)
                    continue;


                DailySummary_Model model = new DailySummary_Model();
                model.Department = item.GetDescription();
                model.TotalUser = dicDeparmentUserCount[item];


                var dptModel = departmentAttendanceList.Where(p => p.Department == item).FirstOrDefault();
                if (dptModel != null)
                {
                    model.BackgroundColor = System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.White);
                    model.DayShiftUserCount = dptModel.DayShift;
                    model.NightShiftUserCount = dptModel.NightShift;
                
                    model.AnnualLeave = dptModel.AnnualLeavel;
                    
                    //Others Leave are Hospitalization,  Maternity,  Marriage,  Paternity,  Compassiondate,  Reservist,  Child Care Leave 
                    model.OthersLeave =
                        dptModel.Hospitalization +
                        dptModel.Maternity +
                        dptModel.Marriage +
                        dptModel.Paternity +
                        dptModel.Compassionate +
                        dptModel.Reservist +
                        dptModel.ChildCareLeave;
                    
                    model.UnpaidLeave = dptModel.Unpaid;
                    model.MC_UPMC = dptModel.MC_UPMC;
                    model.Absent = dptModel.Absent;
                    model.BussinessTrip_WFH = dptModel.BusinessTrip + dptModel.WFH;
                    model.Pending = dptModel.Pending;
                    model.TotalPresent = dptModel.TotalPresent;

                    //Excluded AL % =( Nos of staff -mc -upL/upMC -absent)/ Nos of staff
                    model.ExcludedAL = model.TotalUser == 0 ? "0.00%" :
                        Math.Round((model.TotalUser - dptModel.MC_UPMC - dptModel.Unpaid - dptModel.Absent) / model.TotalUser * 100, 2).ToString("0.00") + "%";

                    //Included AL % = ( Nos of staff -mc -upL/upMC -absent -AL -OAL)/Nos of staff
                    model.IncludedAL = model.TotalUser == 0 ? "0.00%" : Math.Round(
                        (model.TotalUser - dptModel.MC_UPMC - dptModel.Unpaid - dptModel.Absent - dptModel.Maternity
                        - dptModel.AnnualLeavel - dptModel.Paternity - dptModel.Marriage - dptModel.Hospitalization
                        - dptModel.Compassionate - dptModel.ChildCareLeave) / model.TotalUser * 100, 2).ToString("0.00") + "%";
                    
                    model.Target = "98.50%";
                    model.Remarks = dptModel.LeaveReason;

                    allExcludedAL +=  dptModel.MC_UPMC + dptModel.Unpaid + dptModel.Absent;
                    allIncludedAL +=  dptModel.MC_UPMC + dptModel.Unpaid + dptModel.Absent + dptModel.AnnualLeavel + dptModel.Paternity + dptModel.Marriage + dptModel.Maternity + dptModel.Hospitalization + dptModel.Compassionate + dptModel.ChildCareLeave;
                }
                else
                {
                    //超过当天8:15还没有提交, 背景显示为黄色.
                    //超过当天8:30还没有提交, 背景显示为红色.                   
                    if (DateTime.Now > DateTime.Now.AddHours(-8).Date.AddHours(8).AddMinutes(30))
                    {
                        model.BackgroundColor = System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.Pink);
                    }
                    else if (DateTime.Now > DateTime.Now.AddHours(-8).Date.AddHours(8).AddMinutes(15))
                    {
                        model.BackgroundColor = System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.Yellow);
                    }
                    else
                    {
                        model.BackgroundColor = System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.White);
                    }
                   
                    model.DayShiftUserCount = 0;
                    model.NightShiftUserCount = 0;
                    model.TotalPresent = 0;
                    model.AnnualLeave = 0;
                    model.MC_UPMC = 0;
                    model.Absent = 0;
                    model.UnpaidLeave = 0;
                    model.BussinessTrip_WFH = 0;
                    model.Target = "98.50%";
                    model.Remarks = "";
                    model.OthersLeave = 0;
                    model.ExcludedAL = "0.00%";
                    model.IncludedAL = "0.00%";
                }


                resultList.Add(model);
                #endregion
            }


            #region summary row
            DailySummary_Model modelSummary = new DailySummary_Model();
            modelSummary.Department = "Total";
            modelSummary.BackgroundColor = System.Drawing.ColorTranslator.ToHtml( System.Drawing.Color.White);
            modelSummary.TotalUser = resultList.Sum(p => p.TotalUser);
            modelSummary.DayShiftUserCount = resultList.Sum(p => p.DayShiftUserCount);
            modelSummary.NightShiftUserCount = resultList.Sum(p => p.NightShiftUserCount);
            
            modelSummary.AnnualLeave = resultList.Sum(p => p.AnnualLeave);
            modelSummary.OthersLeave = resultList.Sum(p => p.OthersLeave);
            modelSummary.UnpaidLeave = resultList.Sum(p => p.UnpaidLeave);
            modelSummary.MC_UPMC = resultList.Sum(p => p.MC_UPMC);
            modelSummary.Absent = resultList.Sum(p => p.Absent);
            modelSummary.BussinessTrip_WFH = resultList.Sum(p => p.BussinessTrip_WFH);
            modelSummary.Pending = resultList.Sum(p => p.Pending);
            modelSummary.TotalPresent = resultList.Sum(p => p.TotalPresent);
            
            modelSummary.ExcludedAL = Math.Round((dicDeparmentUserCount.Sum(p => p.Value) -allExcludedAL) / dicDeparmentUserCount.Sum(p => p.Value) * 100, 2).ToString() + "%";
            modelSummary.IncludedAL = Math.Round((dicDeparmentUserCount.Sum(p => p.Value) - allIncludedAL) / dicDeparmentUserCount.Sum(p=>p.Value) * 100, 2).ToString() + "%";

            modelSummary.Target = "";
            modelSummary.Remarks = "";
            resultList.Add(modelSummary);
            #endregion


            return resultList;
        }


    }
}
