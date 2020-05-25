using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Common.Class.BLL
{
    public class PaintingInventory_BLL
    {
        private readonly Common.Class.DAL.PaintingInventory_DAL dal = new DAL.PaintingInventory_DAL();
        public PaintingInventory_BLL()
        {
        }
        public List<SqlCommand> AddInventoryCMDList(List<Common.Class.Model.PaintingInventory_Model> list_Inventory)
        {
            List<SqlCommand> list_cmd = new List<SqlCommand>();
            foreach (Common.Class.Model.PaintingInventory_Model item in list_Inventory)
            {
                if (item != null)
                {
                    list_cmd.Add(dal.AddCommand(item));
                }
            }
            return list_cmd;
        }

        public bool Add(Common.Class.Model.PaintingInventory_Model model)
        {
            int result = dal.Add(model);

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public DataTable Report(string partNo, string customer)
        {
            DataTable dt = dal.SelectInventoryDailyReport(partNo, customer);

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            //dt.Columns.Add("ID");
            //int id = 1;

            //double AllSetCount = 0;
            //double PCSCount = 0;
            //double AllJobCount = 0;
            //double EstTimeCount = 0;

            //double BeforeSETCount = 0;
            //double AfterSETCount = 0;
            //double BeforePCSCount = 0;
            //double AfterPCSCount = 0;


            //foreach (DataRow row in dt.Rows)
            //{
            //    //set id
            //    row["ID"] = id.ToString();
            //    id++;

            //    #region  set status  not use
            //    //double undo = 0;
            //    //double total = 0;
            //    //string statusGen = "";

            //    //try
            //    //{
            //    //    undo = double.Parse(row["undoCount"].ToString());
            //    //}
            //    //catch
            //    //{
            //    //    statusGen = StaticRes.Global.clsConstValue.JobStatus.pending;
            //    //}

            //    //try
            //    //{
            //    //    total = double.Parse(row["totalQuantity"].ToString());

            //    //    //string aaa = row["totalQuantity"].ToString();
            //    //}
            //    //catch 
            //    //{
            //    //    statusGen = StaticRes.Global.clsConstValue.JobStatus.pending;
            //    //}

            //    //try
            //    //{
            //    //    if (row["estProcessTime"].ToString() == "")
            //    //    {
            //    //        row["estProcessTime"] = "0h";
            //    //    }
            //    //    else
            //    //    {
            //    //        row["estProcessTime"] = row["estProcessTime"] + "h";
            //    //    }

            //    //}
            //    //catch(Exception ee)
            //    //{}


            //    //if (statusGen == "" && undo >= total)
            //    //{
            //    //    row["status"] = StaticRes.Global.clsConstValue.JobStatus.pending;
            //    //}
            //    //else if (statusGen == "" && (undo > 0 && undo < total))
            //    //{
            //    //    row["status"] = StaticRes.Global.clsConstValue.JobStatus.inprocess;
            //    //}
            //    //else if (statusGen == "" && undo <= 0)
            //    //{
            //    //    row["status"] = StaticRes.Global.clsConstValue.JobStatus.complete;
            //    //}
            //    //else
            //    //{
            //    //    row["status"] = statusGen;
            //    //}
            //    #endregion

            //    #region  AllSetCount++ /  PCSCount++ / EstTimeCount++ / AllJobCount++
            //    try
            //    {
            //        AllSetCount += double.Parse(row["SetCount"].ToString());
            //    }
            //    catch
            //    { }
            //    try
            //    {
            //        PCSCount += double.Parse(row["Pcs"].ToString());
            //    }
            //    catch
            //    { }
            //    try
            //    {
            //        EstTimeCount += double.Parse(row["estProcessTime"].ToString().Trim('H'));
            //    }
            //    catch
            //    { }
            //    try
            //    {
            //        AllJobCount += double.Parse(row["JobCount"].ToString());
            //    }
            //    catch
            //    { }
            //    #endregion

            //    #region  Before  After
            //    string BeforeLaser = row["BeforeLaser"].ToString();
            //    string AfterLaser = row["AfterLaser"].ToString();
            //    try
            //    {
            //        BeforeSETCount += double.Parse(BeforeLaser.Trim(')').Split('(')[0]);
            //        BeforePCSCount += double.Parse(BeforeLaser.Trim(')').Split('(')[1]);
            //    }
            //    catch (Exception)
            //    {
            //        BeforeSETCount += 0;
            //        BeforePCSCount += 0;
            //    }

            //    if (double.Parse(BeforeLaser.Trim(')').Split('(')[0]) < 0 || double.Parse(BeforeLaser.Trim(')').Split('(')[1]) < 0)
            //    {
            //        row["BeforeLaser"] = "0(0)";
            //    }



            //    try
            //    {
            //        AfterSETCount += double.Parse(AfterLaser.Trim(')').Split('(')[0]);
            //        AfterPCSCount += double.Parse(AfterLaser.Trim(')').Split('(')[1]);
            //    }
            //    catch (Exception)
            //    {
            //        AfterSETCount += 0;
            //        AfterPCSCount += 0;
            //    }
            //    #endregion

            //    //set esttime format
            //    row["EstProcessTime"] = Common.CommFunctions.ConvertDateTimeShort(row["EstProcessTime"].ToString());


            //}

            //DataRow dr = dt.NewRow();
            //dr["Customer"] = "Total :";
            //dr["SetCount"] = AllSetCount < 0 ? 0 : AllSetCount;
            //dr["Pcs"] = PCSCount < 0 ? 0 : PCSCount;
            ////dr["estProcessTime"] = EstTimeCount < 0 ? 0 : EstTimeCount;
            //dr["JobCount"] = AllJobCount < 0 ? 0 : AllJobCount;
            //dr["estProcessTime"] = Common.CommFunctions.ConvertDateTimeShort(Math.Round(EstTimeCount < 0 ? 0 : EstTimeCount, 2).ToString() + 'H');
            //dr["MRPSet_PCS"] = AllSetCount.ToString() + "(" + PCSCount + ")";



            //dr["BeforeLaser"] = BeforeSETCount.ToString() + "(" + BeforePCSCount.ToString() + ")";
            //dr["AfterLaser"] = AfterSETCount.ToString() + "(" + AfterPCSCount.ToString() + ")";


            //dt.Rows.Add(dr);

            return dt;

        }
    }
}
