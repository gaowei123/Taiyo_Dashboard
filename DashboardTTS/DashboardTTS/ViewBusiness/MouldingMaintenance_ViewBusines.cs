using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DashboardTTS.ViewBusiness
{
    public class MouldingMaintenance_ViewBusines
    {
        Common.Class.BLL.MouldingViHistory_BLL trackingBLL = new Common.Class.BLL.MouldingViHistory_BLL();



        public List<ViewModel.MouldMaintenance.Tracking> GetTrackingList(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sJigNo, string sMachineID)
        {
            DataTable dt = trackingBLL.GetList(dDateFrom, dDateTo, sPartNo, sJigNo, sMachineID);


            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }


            List<ViewModel.MouldMaintenance.Tracking> trackingList = new List<ViewModel.MouldMaintenance.Tracking>();

            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.MouldMaintenance.Tracking model = new ViewModel.MouldMaintenance.Tracking();


                model.trackingID = dr["trackingID"].ToString();
                model.day = DateTime.Parse(dr["day"].ToString());
                model.shift = dr["shift"].ToString();

                model.machineID = dr["machineID"].ToString();
                model.partNo = dr["partNumber"].ToString();
                model.jigNo = dr["jigNo"].ToString();
                model.model = dr["model"].ToString();

                model.totalQty = dr["acountReading"].ToString() == "" ? 0 : double.Parse(dr["acountReading"].ToString());
                model.passQty = dr["acceptQty"].ToString() == "" ? 0 : double.Parse(dr["acceptQty"].ToString());
                model.rejQty = dr["rejectQty"].ToString() == "" ? 0 : double.Parse(dr["rejectQty"].ToString());
                model.setup = dr["Setup"].ToString() == "" ? 0 : double.Parse(dr["Setup"].ToString());
                model.wastedMaterial01 = dr["WastageMaterial01"].ToString() == "" ? 0 : double.Parse(dr["WastageMaterial01"].ToString());
                model.wastedMaterial02 = dr["WastageMaterial02"].ToString() == "" ? 0 : double.Parse(dr["WastageMaterial02"].ToString());

                model.datetime = DateTime.Parse(dr["dateTime"].ToString());



                trackingList.Add(model);                
            }


            return trackingList;
        }


    }
}