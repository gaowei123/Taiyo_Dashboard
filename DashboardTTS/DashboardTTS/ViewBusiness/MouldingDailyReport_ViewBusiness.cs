using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DashboardTTS.ViewBusiness
{
    public class MouldingDailyReport_ViewBusiness
    {


        public MouldingDailyReport_ViewBusiness() { }


        public List<ViewModel.MouldDailyReport_ViewModel.VIData> GetViList(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sJigNo, string sShift)
        {
            Common.Class.BLL.MouldingViHistory_BLL viBLL = new Common.Class.BLL.MouldingViHistory_BLL();


            DataTable dtVi = viBLL.GetViForDailyReport_NEW(dDateFrom, dDateTo, sPartNo, sJigNo, sShift);
            if (dtVi == null ||dtVi.Rows.Count== 0)
            {
                return null;
            }

            //vi tracking datatable  convert to list
            List<ViewModel.MouldDailyReport_ViewModel.VIData> viList = new List<ViewModel.MouldDailyReport_ViewModel.VIData>();
            foreach (DataRow drVI in dtVi.Rows)
            {
                ViewModel.MouldDailyReport_ViewModel.VIData model = new ViewModel.MouldDailyReport_ViewModel.VIData();

                model.date = DateTime.Parse(drVI["day"].ToString());
                model.shift = drVI["shift"].ToString();
                model.machineID = drVI["machineID"].ToString();
                model.jigNo = drVI["jigNo"].ToString();
                model.partNo = drVI["partNumber"].ToString();
                model.output = double.Parse(drVI["output"].ToString());
                model.passQty = double.Parse(drVI["passQty"].ToString());
                model.rejQty = double.Parse(drVI["rejQty"].ToString());
                model.rejRate = drVI["rejRate"].ToString() + "%";
                model.opID = drVI["userID"].ToString();
                
                viList.Add(model);
            }

            return viList;
        }




        public List<ViewModel.MouldDailyReport_ViewModel.DefectData> GetDefectList(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sJigNo, string sShift)
        {
            Common.Class.BLL.MouldingViDefectTracking_BLL defectBLL = new Common.Class.BLL.MouldingViDefectTracking_BLL();


        
            DataTable dtDefect = defectBLL.GetDefectForDailyReport(dDateFrom, dDateTo, sPartNo, sJigNo, sShift);
            if (dtDefect == null || dtDefect.Rows.Count == 0)
            {
                return null;
            }

            

            //defect tracking datatable convert to list
            List<ViewModel.MouldDailyReport_ViewModel.DefectData> defectList = new List<ViewModel.MouldDailyReport_ViewModel.DefectData>();
            foreach (DataRow drDefect in dtDefect.Rows)
            {
                ViewModel.MouldDailyReport_ViewModel.DefectData model = new ViewModel.MouldDailyReport_ViewModel.DefectData();

                model.date = DateTime.Parse(drDefect["day"].ToString());
                model.shift = drDefect["shift"].ToString();
                model.machineID = drDefect["machineID"].ToString();
                model.partNo = drDefect["partNumber"].ToString();



                model.whiteDot = int.Parse(drDefect["White Dot"].ToString());
                model.scratches = int.Parse(drDefect["Scratches"].ToString());
                model.dentedMark = int.Parse(drDefect["Dented Mark"].ToString());
                model.shinningDot = int.Parse(drDefect["Shinning Dot"].ToString());
                model.blackMark = int.Parse(drDefect["Black Mark"].ToString());
                model.sinkMark = int.Parse(drDefect["Sink Mark"].ToString());
                model.flowMark = int.Parse(drDefect["Flow Mark"].ToString());
                model.highGate = int.Parse(drDefect["High Gate"].ToString());
                model.silverSteak = int.Parse(drDefect["Silver Steak"].ToString());
                model.blackDot = int.Parse(drDefect["Black Dot"].ToString());
                model.oilStain = int.Parse(drDefect["Oil Stain"].ToString());
                model.flowLine = int.Parse(drDefect["Flow Line"].ToString());
                model.overCut = int.Parse(drDefect["Over-Cut"].ToString());
                model.crack = int.Parse(drDefect["Crack"].ToString());
                model.shortMold = int.Parse(drDefect["Short Mold"].ToString());
                model.stainMark = int.Parse(drDefect["Stain Mark"].ToString());
                model.weldLine = int.Parse(drDefect["Weld Line"].ToString());
                model.flashes = int.Parse(drDefect["Flashes"].ToString());
                model.foreignMaterial = int.Parse(drDefect["Foreign Materials"].ToString());
                model.drag = int.Parse(drDefect["Drag"].ToString());
                model.materialBleed = int.Parse(drDefect["Material Bleed"].ToString());
                model.bent = int.Parse(drDefect["Bent"].ToString());
                model.deform = int.Parse(drDefect["Deform"].ToString());
                model.gasMark = int.Parse(drDefect["Gas Mark"].ToString());



                defectList.Add(model);
            }

            return defectList;

        }




    }
}