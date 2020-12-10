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
                    model.DayShiftUserCount = dptModel.DayShift;
                    model.NightShiftUserCount = dptModel.NightShift;
                    model.TotalPresent = dptModel.TotalPresent;

                    model.AnnualLeave = dptModel.AnnualLeavel;                   
                    model.MC = dptModel.MC;
                    model.Absent = dptModel.Absent;
                    model.UPL_UPMC = dptModel.UPL_UPMC;
                    model.BussinessTrip_WFH = dptModel.BusinessTrip + dptModel.WFH;
                    model.Target = "98.50%";
                    model.LeaveDetail = dptModel.LeaveReason;


                    //Others Leave are Hospitalization,  Maternity,  Marriage,  Paternity,  Compassiondate,  Reservist,  Child Care Leave 
                    model.OthersLeave =
                        dptModel.Hospitalization +
                        dptModel.Maternity +
                        dptModel.Marriage +
                        dptModel.Paternity +
                        dptModel.Compassionate +
                        dptModel.Reservist +
                        dptModel.ChildCareLeave;
                    
                    //Excluded AL % =( Nos of staff -mc -upL/upMC -absent)/ Nos of staff
                    model.ExcludedAL = Math.Round( 
                        (dicDeparmentUserCount[item]
                        - dptModel.MC 
                        - dptModel.UPL_UPMC 
                        - dptModel.Absent) / dicDeparmentUserCount[item] * 100, 2).ToString("0.00") + "%";

                    //Included AL % = ( Nos of staff -mc -upL/upMC -absent -AL -OAL)/Nos of staff
                    model.IncludedAL = Math.Round(
                        (dicDeparmentUserCount[item]
                        - dptModel.MC 
                        -dptModel.UPL_UPMC 
                        -dptModel.Absent 
                        -dptModel.AnnualLeavel 
                        -dptModel.Paternity
                        -dptModel.Marriage
                        -dptModel.Hospitalization
                        -dptModel.Compassionate
                        -dptModel.ChildCareLeave) / dicDeparmentUserCount[item] * 100, 2).ToString("0.00") + "%";
                }
                else
                {
                    model.DayShiftUserCount = 0;
                    model.NightShiftUserCount = 0;
                    model.TotalPresent = 0;
                    model.AnnualLeave = 0;
                    model.MC = 0;
                    model.Absent = 0;
                    model.UPL_UPMC = 0;
                    model.BussinessTrip_WFH = 0;
                    model.Target = "98.50%";
                    model.LeaveDetail = "";
                    model.OthersLeave = 0;
                    model.ExcludedAL = "0.00%";
                    model.IncludedAL = "0.00%";
                }



                resultList.Add(model);
                #endregion
            }

            return resultList;
        }


    }
}
