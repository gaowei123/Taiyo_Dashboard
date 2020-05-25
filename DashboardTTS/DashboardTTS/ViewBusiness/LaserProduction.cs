using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DashboardTTS.ViewBusiness
{
    public class LaserProduction
    {
        public LaserProduction()
        {

        }




        public  List<ViewModel.LaserSummaryReport_ViewModel> GetSummaryList(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sShift)
        {
            Common.BLL.LMMSWatchLog_BLL WatchDogBll = new Common.BLL.LMMSWatchLog_BLL();
            DataTable dtSummary = WatchDogBll.GetSummaryReport(dDateFrom, dDateTo, sPartNo, sShift);

            if (dtSummary == null || dtSummary.Rows.Count == 0)
            {
                return null;
            }


            List<ViewModel.LaserSummaryReport_ViewModel> models = new List<ViewModel.LaserSummaryReport_ViewModel>();

            foreach (DataRow dr in dtSummary.Rows)
            {
                ViewModel.LaserSummaryReport_ViewModel model = new ViewModel.LaserSummaryReport_ViewModel();
                model.machineID = dr["machineID"].ToString();
                model.laserBtn = double.Parse(dr["laserBtn"].ToString());

                model.laserBtn = double.Parse(dr["laserBtn"].ToString());
                model.printBtn = double.Parse(dr["printBtn"].ToString());
                model.lens784 = double.Parse(dr["lens784"].ToString());
                model.lens824 = double.Parse(dr["lens824"].ToString());
                model.lens833 = double.Parse(dr["lens833"].ToString());
                model.bezel257 = double.Parse(dr["bezel257"].ToString());
                model.bezel830 = double.Parse(dr["bezel830"].ToString());
                model.bezel831 = double.Parse(dr["bezel831"].ToString());
                model.panel452 = double.Parse(dr["panel452"].ToString());
                model.panel656 = double.Parse(dr["panel656"].ToString());
                model.tks869 = double.Parse(dr["tks869"].ToString());
                model.ok = double.Parse(dr["ok"].ToString());
                model.ng = double.Parse(dr["ng"].ToString());
                model.output = double.Parse(dr["Output"].ToString());  
                model.rejRate = double.Parse(dr["rejRate"].ToString()).ToString("0.00") + "%";

                models.Add(model);
            }

          

            return models;
        }

    }
}