using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.ExtendClass.LaserInventory
{
    public class LaserInventory_BLL
    {
        private readonly LaserInventory_DAL _dal = new LaserInventory_DAL();

        private DataTable Report(string partNo, string customer)
        {
            DataTable dt = _dal.SelectInventoryDailyReport(partNo, customer);

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            dt.Columns.Add("ID");
            int id = 1;

            double AllSetCount = 0;
            double PCSCount = 0;
            double AllJobCount = 0;
            double EstTimeCount = 0;

            double BeforeSETCount = 0;
            double AfterSETCount = 0;
            double BeforePCSCount = 0;
            double AfterPCSCount = 0;


            foreach (DataRow row in dt.Rows)
            {
                //set id
                row["ID"] = id.ToString();
                id++;

     

                #region  AllSetCount++ /  PCSCount++ / EstTimeCount++ / AllJobCount++
                try
                {
                    AllSetCount += double.Parse(row["SetCount"].ToString());
                }
                catch
                { }
                try
                {
                    PCSCount += double.Parse(row["Pcs"].ToString());
                }
                catch
                { }
                try
                {
                    EstTimeCount += double.Parse(row["estProcessTime"].ToString().Trim('H'));
                }
                catch
                { }
                try
                {
                    AllJobCount += double.Parse(row["JobCount"].ToString());
                }
                catch
                { }
                #endregion

                #region  Before  After
                string BeforeLaser = row["BeforeLaser"].ToString();
                string AfterLaser = row["AfterLaser"].ToString();
                try
                {
                    BeforeSETCount += double.Parse(BeforeLaser.Trim(')').Split('(')[0]);
                    BeforePCSCount += double.Parse(BeforeLaser.Trim(')').Split('(')[1]);
                }
                catch (Exception)
                {
                    BeforeSETCount += 0;
                    BeforePCSCount += 0;
                }

                if (double.Parse(BeforeLaser.Trim(')').Split('(')[0]) < 0 || double.Parse(BeforeLaser.Trim(')').Split('(')[1]) < 0)
                {
                    row["BeforeLaser"] = "0(0)";
                }



                try
                {
                    AfterSETCount += double.Parse(AfterLaser.Trim(')').Split('(')[0]);
                    AfterPCSCount += double.Parse(AfterLaser.Trim(')').Split('(')[1]);
                }
                catch (Exception)
                {
                    AfterSETCount += 0;
                    AfterPCSCount += 0;
                }
                #endregion

                //set esttime format
                row["EstProcessTime"] = Common.CommFunctions.ConvertDateTimeShort(row["EstProcessTime"].ToString());


            }

            DataRow dr = dt.NewRow();
            dr["Customer"] = "Total :";
            dr["SetCount"] = AllSetCount < 0 ? 0 : AllSetCount;
            dr["Pcs"] = PCSCount < 0 ? 0 : PCSCount;
            dr["JobCount"] = AllJobCount < 0 ? 0 : AllJobCount;
            dr["estProcessTime"] = Common.CommFunctions.ConvertDateTimeShort(Math.Round(EstTimeCount < 0 ? 0 : EstTimeCount, 2).ToString() + 'H');
            dr["MRPSet_PCS"] = AllSetCount.ToString() + "(" + PCSCount + ")";
            
            dr["BeforeLaser"] = BeforeSETCount.ToString() + "(" + BeforePCSCount.ToString() + ")";
            dr["AfterLaser"] = AfterSETCount.ToString() + "(" + AfterPCSCount.ToString() + ")";


            dt.Rows.Add(dr);

            return dt;

        }



        public List<LaserInventory_Model.Summary> GetInventoryList()
        {
            List<LaserInventory_Model.Summary> list = new List<LaserInventory_Model.Summary>();
            DataTable dt = Report("", "");

            foreach (DataRow dr in dt.Rows)
            {
                LaserInventory_Model.Summary model = new LaserInventory_Model.Summary();

                model.Customer = dr["Customer"].ToString();
                model.Model = dr["Module"].ToString();
                model.PartNo = dr["PartNumber"].ToString();
                model.JobNo = dr["JobNumber"].ToString();
                model.MRPQty = dr["MRPSet_PCS"].ToString();
                model.BeforeQty = dr["BeforeLaser"].ToString();
                model.AfterQty = dr["AfterLaser"].ToString();
                model.JobCount = int.Parse(dr["JobCount"].ToString());
                model.HourlyQty = dr["Hourly"].ToString();
                model.CycleTime = dr["SET_PCS_CycleTime"].ToString();
                model.MFGDate = dr["MFGDate"].ToString();
                model.BomFlag = dr["BomExistFlag"].ToString();

                list.Add(model);
            }


            return list;
        }


    }
}
