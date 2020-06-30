using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DashboardTTS.ViewBusiness
{
    public class LaserProduction
    {


        private readonly Common.BLL.LMMSWatchLog_BLL watchlogBll = new Common.BLL.LMMSWatchLog_BLL();
        private readonly Common.BLL.LMMSWatchDog_His_BLL watchdogBLL = new Common.BLL.LMMSWatchDog_His_BLL();
        private readonly Common.Class.BLL.LMMSInventoty_BLL inventoryBLL = new Common.Class.BLL.LMMSInventoty_BLL();
       

        public LaserProduction()
        {

        }




        public  List<ViewModel.LaserSummaryReport_ViewModel> GetSummaryList(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sShift)
        {
        
            DataTable dtSummary = watchlogBll.GetSummaryReport(dDateFrom, dDateTo, sPartNo, sShift);

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





        public ViewModel.LaserMaintenance_ViewModel GetMaintainJobInfo(DateTime dDay, string sShift, string sMachineID, string sJobID)
        {
            ViewModel.LaserMaintenance_ViewModel model = new ViewModel.LaserMaintenance_ViewModel();
            model.day = dDay;
            model.shift = sShift;
            model.machineID = sMachineID;
            model.jobID = sJobID;




            //从inventory中, 获取setup, shortage, buyoff数量.
            DataTable dtInventory = inventoryBLL.GetJobDetailForMaintenance(sJobID);
            if (dtInventory == null || dtInventory.Rows.Count != 0)
            {
                DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[GetMaintainJobInfo] set inventory info --  get datatable null from inventory! set de"));
                return null;
            }

            model.shortage = double.Parse(dtInventory.Rows[0]["shortage"].ToString());
            model.setup = double.Parse(dtInventory.Rows[0]["setupQty"].ToString());
            model.buyoff = double.Parse(dtInventory.Rows[0]["buyoffQty"].ToString());

            DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[GetMaintainJobInfo] set inventory info --  setup:{0}, buyoff:{1}, shortage:{2}", model.setup, model.buyoff, model.shortage));






            //获取material ng信息.
            DataTable dtMaterial = watchdogBLL.GetJobMaterialList(sJobID, dDay, sShift, sMachineID);
            if (dtMaterial == null || dtMaterial.Rows.Count == 0)
            {
                DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[GetMaintainJobInfo] set material detail list --  get material datatable null !"));
                return null;
            }
            
            model.materialList = new List<ViewModel.LaserMaintenance_ViewModel.Material>();
            foreach (DataRow dr in dtMaterial.Rows)
            {
                ViewModel.LaserMaintenance_ViewModel.Material materialModel = new ViewModel.LaserMaintenance_ViewModel.Material();
                materialModel.sn = int.Parse(dr["sn"].ToString());
                materialModel.materialNo = dr["materialNo"].ToString();
                materialModel.ng = double.Parse(dr["ngQty"].ToString());

                model.materialList.Add(materialModel);

                DBHelp.Reports.LogFile.Log("LaserJobMaintance", string.Format("[GetMaintainJobInfo] set material info --  sn:{0}, material no:{1}, ng:{2}", materialModel.sn, materialModel.materialNo, materialModel.ng));
            }




            return model;
        }




    }
}