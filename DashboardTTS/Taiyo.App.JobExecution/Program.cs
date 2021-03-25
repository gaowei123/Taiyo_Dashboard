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
        //库存信息的起始计算时间.
        private static DateTime _startTime = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["StartTime"].ToString());
        private static Common.Class.BLL.ProductionInventoryHistory _inventoryBLL = new Common.Class.BLL.ProductionInventoryHistory();

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
            //防止重复启动







            try
            {
                //检查数据是否保存过了.
                var totalList = _inventoryBLL.GetDayList(DateTime.Now.Date.AddDays(-1));
                if (totalList != null && totalList.Count > 0)
                {
                    DBHelp.Reports.LogFile.Log("Taiyo.App.JobExecution", "Today list already exist!");
                    return;
                }





                //按照dashbaord原本的逻辑生成当前的list.
                DashboardTTS.ViewBusiness.OverallReport_ViewBusiness overallBLL = new DashboardTTS.ViewBusiness.OverallReport_ViewBusiness();
                //每天8点执行, 当作前一天的库存信息   
                var list = overallBLL.GetAllSectionResult(_startTime, "", "","", DateTime.Now.Date.AddDays(-1));
                if (list != null)
                {
                    DBHelp.Reports.LogFile.Log("Taiyo.App.JobExecution", "Gerenate today list success, list.count = " + list.Count);


                    List<SqlCommand> cmdList = new List<SqlCommand>();
                    //遍历list中每一个保存到表中.
                    foreach (var item in list)
                    {
                        //当天8点执行, 算作前一天的库存信息.
                        item.Day = DateTime.Now.Date.AddDays(-1);
                        cmdList.Add(_inventoryBLL.AddCommand(item));
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
        
    }
}
