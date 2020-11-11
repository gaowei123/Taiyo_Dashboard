using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using Taiyo.Enum.Production;
using Taiyo.Tool;
using Taiyo.Tool.Extension;


namespace Common.Class.BLL
{
    public class MouldingMachineStatus_BLL
    {
        Common.Class.DAL.MouldingMachineStatus_DAL dal = new DAL.MouldingMachineStatus_DAL();
        
        public List<Common.Class.Model.MouldingMachineStatus_Model> GetModelList(DateTime? dDateTime, DateTime? dDateTo, string sShift, string sMachineID, string sStatus)
        {
            DataTable dt = dal.GetList(dDateTime, dDateTo, sShift, sMachineID, sStatus);
            if (dt == null || dt.Rows.Count == 0)
                return null;



            List<Common.Class.Model.MouldingMachineStatus_Model> modelList = new List<Model.MouldingMachineStatus_Model>();
            foreach (DataRow dr in dt.Rows)
            {
                Common.Class.Model.MouldingMachineStatus_Model model = new Model.MouldingMachineStatus_Model();
                
                model.MachineID = dr["MachineID"].ToString();
                model.Day = DateTime.Parse(dr["Day"].ToString());
                model.Shift = dr["Shif"].ToString();
                model.MachineStatus = dr["MachineStatus"].ToString();
                model.OEEStatus = dr["OEEStatus"].ToString(); 
                model.StartTime = DateTime.Parse(dr["StartTime"].ToString());
               

                //当天 默认赋值 datetime now, 不是和默认赋值 当时班次的最后时间点.
                if (dr["EndTime"] == null || dr["EndTime"].ToString() == "")
                {
                    if (model.Day == DateTime.Now.Date)
                        model.EndTime = DateTime.Now;
                    else
                        model.EndTime = model.Shift == "Day" ? model.Day.AddHours(20) : model.Day.AddDays(1).AddHours(8);
                }
                else
                {
                    model.EndTime = DateTime.Parse(dr["EndTime"].ToString());
                }


             
                model.PartNo = dr["PartNo"].ToString();
                model.UserID = dr["UserID"].ToString();
                model.Remark = dr["Remark"].ToString();
                
                modelList.Add(model);                
            }


            return modelList;
        }



        public Dictionary<int, MouldingStatus> GetCurrentStatus()
        {
            Dictionary<int, MouldingStatus> dicStatus = new Dictionary<int, MouldingStatus>();

            DataTable dt = dal.GetTodayList();
            if (dt == null || dt.Rows.Count == 0)
            {
                for (int i = 1; i < 10; i++)
                {
                    dicStatus.Add(i, MouldingStatus.Shutdown);
                }
            }
            else
            {
                for (int i = 1; i < 10; i++)
                {
                    DataRow[] arrDrTemp = dt.Select(" MachineID = '" + i.ToString() + "'", "starttime desc");
                    if (arrDrTemp == null || arrDrTemp.Count() == 0)
                    {
                        dicStatus.Add(i, MouldingStatus.Shutdown);
                    }
                    else
                    {
                        var status = StatusConventor.ConventMoulding(arrDrTemp[0]["MachineStatus"].ToString());
                        dicStatus.Add(i, status);
                    }
                }
            }

            return dicStatus;
        }
        
        public Dictionary<int, double> GetCurrentUsedRate()
        {
            Dictionary<int, double> dic = new Dictionary<int, double>();
            List<Model.MouldingMachineStatus_Model> modelList = GetModelList(DateTime.Now.Date, DateTime.Now.Date.AddDays(1), "", "", "");
            if (modelList == null)
            {
                for (int i = 1; i < 10; i++)
                {
                    dic.Add(i, 0);
                }
                return dic;
            }



            for (int i = 1; i < 10; i++)
            {
                var tempList = from a in modelList
                               where a.MachineID == i.ToString()
                               && (
                                    StatusConventor.ConventMoulding(a.MachineStatus) == MouldingStatus.Running ||
                                    StatusConventor.ConventMoulding(a.MachineStatus) == MouldingStatus.MaterialTesting ||
                                    StatusConventor.ConventMoulding(a.MachineStatus) == MouldingStatus.MouldTesting ||
                                    StatusConventor.ConventMoulding(a.MachineStatus) == MouldingStatus.Adjustment ||
                                    StatusConventor.ConventMoulding(a.MachineStatus) == MouldingStatus.ChangeModel
                               )
                               select a;

                double totalUsedSeconds = tempList == null || tempList.Count() == 0 ? 0 : tempList.Sum(p => (p.EndTime - p.StartTime).TotalSeconds);
                double totalSeconds = Common.CommFunctions.GetTotalSeconds(DateTime.Now.Date, DateTime.Now.Date.AddDays(1), "", "", false);

                double usedRate = Math.Round(totalUsedSeconds / totalSeconds * 100, 2);
                dic.Add(i, usedRate);

            }


            return dic;
        }

    }
}
