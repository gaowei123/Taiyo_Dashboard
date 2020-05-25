using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DashboardTTS.Controllers
{
    public class MOULD_MonthlyHightestRejectController : Controller
    {
        // GET: MOULD_MonthlyHightestReject
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetPartsData()
        {
            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"].ToString());
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"].ToString()).AddDays(1);
            string machineID = Request.Form["MachineID"] == null ? string.Empty : Request.Form["MachineID"].ToString();
            string partNo = Request.Form["PartNo"] == null ? string.Empty : Request.Form["PartNo"].ToString();

            


            ViewModel.MouldMonthlyHighestReject model = new ViewModel.MouldMonthlyHighestReject();
            model.partsList = new List<ViewModel.MouldMonthlyHighestReject.Parts>();

            

            Common.Class.BLL.MouldingViHistory_BLL bll = new Common.Class.BLL.MouldingViHistory_BLL();
            DataTable dtParts = bll.GetTopRejPartsList(dateFrom, dateTo, machineID);
            if (dtParts != null && dtParts.Rows.Count != 0)
            {
                foreach (DataRow dr in dtParts.Rows)
                {
                    ViewModel.MouldMonthlyHighestReject.Parts partsModel = new ViewModel.MouldMonthlyHighestReject.Parts();

                    partsModel.partNo = dr["partNo"].ToString();
                    partsModel.output = dr["output"].ToString() == "" ? 0 : double.Parse(dr["output"].ToString());
                    partsModel.rejQty = dr["rejQty"].ToString() == "" ? 0 : double.Parse(dr["rejQty"].ToString());
                    partsModel.rejRate = dr["rejRate"].ToString() == "" ? "0.00%" : dr["rejRate"].ToString() + "%";
                    partsModel.highestDefect_1st = dr["highestDefect_1st"].ToString();
                    partsModel.highestDefect_2nd = dr["highestDefect_2nd"].ToString();
                    partsModel.highestDefect_3rd = dr["highestDefect_3rd"].ToString();



                    model.partsList.Add(partsModel);
                }
            }
            

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = js.Serialize(model.partsList);

            return Content(jsonResult);
        }



        public ActionResult GetDefectsData()
        {
            DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"].ToString());
            DateTime dateTo = DateTime.Parse(Request.Form["DateTo"].ToString()).AddDays(1);
            string machineID = Request.Form["MachineID"] == null ? string.Empty : Request.Form["MachineID"].ToString();
            string partNo = Request.Form["PartNo"] == null ? string.Empty : Request.Form["PartNo"].ToString();

            


            ViewModel.MouldMonthlyHighestReject model = new ViewModel.MouldMonthlyHighestReject();
            model.defectList = new List<ViewModel.MouldMonthlyHighestReject.Defect>();

            
            Common.Class.BLL.MouldingViHistory_BLL bll = new Common.Class.BLL.MouldingViHistory_BLL();
            DataTable dtDefect = bll.GetTopRejDefectsList(dateFrom, dateTo, machineID);
            if (dtDefect != null && dtDefect.Rows.Count != 0)
            {
                foreach (DataRow dr in dtDefect.Rows)
                {
                    ViewModel.MouldMonthlyHighestReject.Defect defectModel = new ViewModel.MouldMonthlyHighestReject.Defect();

                    defectModel.defectCode = dr["defectCode"].ToString();
                    defectModel.rejQty = double.Parse(dr["totalRejQty"].ToString());
                    defectModel.rejRate = double.Parse(dr["totalRejRate"].ToString()).ToString("0.00") + "%";

                    defectModel.affectedPart_1st = dr["affectedPart_1st"].ToString();
                    defectModel.affectedPart_2nd = dr["affectedPart_2nd"].ToString();
                    defectModel.affectedPart_3rd = dr["affectedPart_3rd"].ToString();
                    

                    model.defectList.Add(defectModel);
                }
            }



            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = js.Serialize(model.defectList);

            return Content(jsonResult);
        }









    }
}