using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Taiyo.App.JobExecution
{
    class Program
    {

        private static DateTime _startTime = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["StartTime"].ToString());

        static void Main(string[] args)
        {
            DBHelp.Reports.LogFile.Log("Taiyo.App.JobExecution", "In Func");

            
            //防止重复启动
            bool f;
            System.Threading.Mutex m = new System.Threading.Mutex(true, "Taiyo.App.JobExecution", out f);
            if (!f)
            {
                DBHelp.Reports.LogFile.Log("Taiyo.App.JobExecution", "repeat start, close program");
                return;
            }


            try
            {
                Common.Class.BLL.ProductionInventoryHistory inventoryBLL = new Common.Class.BLL.ProductionInventoryHistory();


                //检查今天的数据是否保存过了.
                var totalList = inventoryBLL.GetDayList(DateTime.Now.Date);
                if (totalList != null && totalList.Count > 0)
                {
                    DBHelp.Reports.LogFile.Log("Taiyo.App.JobExecution", "Today list already exist!");
                    return;
                }



                //按照dashbaord原本的逻辑生成当前的list.
                DashboardTTS.ViewBusiness.OverallReport_ViewBusiness overallBLL = new DashboardTTS.ViewBusiness.OverallReport_ViewBusiness();
                List<DashboardTTS.ViewModel.AllSectionInventory.report> list = overallBLL.GetAllSectionList(_startTime, "", "");
                if (list != null)
                {
                    DBHelp.Reports.LogFile.Log("Taiyo.App.JobExecution", "Gerenate today list success, list.count = " + list.Count);


                    List<SqlCommand> cmdList = new List<SqlCommand>();
                    foreach (var item in list)
                    {
                        //遍历list中每一个保存到表中.
                        cmdList.Add(inventoryBLL.AddCommand(ConvertModel(item)));
                    }

                    bool result = DBHelp.SqlDB.SetData_Rollback(cmdList);

                    DBHelp.Reports.LogFile.Log("Taiyo.App.JobExecution", "Execute command list, result: " + result);
                }
                else
                {
                    DBHelp.Reports.LogFile.Log("Taiyo.App.JobExecution", "Gerenate today list, result is null ! ");
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("Taiyo.App.JobExecution", "Catch exception: " + ee.ToString());
            }
        }




        /// <summary>
        /// 将原本report的model, 和数据库的model, 做一个适配转换.
        /// </summary>
        private static Common.Class.Model.ProductionInventoryHistory ConvertModel(DashboardTTS.ViewModel.AllSectionInventory.report model)
        {
            Common.Class.Model.ProductionInventoryHistory dbModel = new Common.Class.Model.ProductionInventoryHistory();
            dbModel.Day = DateTime.Now.Date;


            dbModel.PartNumber = model.partNo;
            dbModel.Model = model.model;
            dbModel.MaterialName = model.materialName;
            dbModel.Assembly = model.assembly ==null? 0: (int)model.assembly.Value;
            dbModel.FG = model.fg ==null ? 0: (int)model.fg.Value;
            dbModel.AfterPacking = model.afterPack == null ? 0 : (int)model.afterPack.Value;
            dbModel.BeforePacking = model.beforePack == null ? 0 : (int)model.beforePack.Value;
            dbModel.AfterWIP = model.afterWIP == null ? 0 : (int)model.afterWIP.Value;
            dbModel.BeforeWIP = model.beforeWIP == null ? 0 : (int)model.beforeWIP.Value;
            dbModel.AfterLaser = model.afterLaser == null ? 0 : (int)model.afterLaser.Value;
            dbModel.BeforeLaser = model.beforeLaser == null ? 0 : (int)model.beforeLaser.Value;
            dbModel.TCPaint = model.tcPaint == null ? 0 : (int)model.tcPaint.Value;
            dbModel.MCPaint = model.mcPaint == null ? 0 : (int)model.mcPaint.Value;
            dbModel.PrintSupplier = model.print == null ? 0 : (int)model.print.Value;
            dbModel.UCPaint = model.ucPaint == null ? 0 : (int)model.ucPaint.Value;
            dbModel.PaintRawPart = model.rawPart == null ? 0 : (int)model.rawPart.Value;


            return dbModel;
        }

    }
}
