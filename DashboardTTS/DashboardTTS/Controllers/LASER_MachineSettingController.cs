using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DashboardTTS.Controllers
{
    public class LASER_MachineSettingController : Controller
    {
        // GET: LASER_MachineSetting
        public ActionResult Index()
        {
            return View();
        }




        public ActionResult GetData()
        {
            try
            {
                DateTime dateFrom = DateTime.Parse(Request.Form["DateFrom"].ToString());
                DateTime dateTo = DateTime.Parse(Request.Form["DateTo"].ToString());
                dateTo = dateTo.AddDays(1);


                //select控件, 接受参数自动添加了[].
                string partNo = Request.Form["PartNo[]"] == null ? "" : Request.Form["PartNo[]"].ToString();
                string machineID = Request.Form["MachineID[]"] == null ? "" : Request.Form["MachineID[]"].ToString();

                string[] arrPartNo = partNo.Split(',');
                string[] arrMachineID = machineID.Split(',');





                Common.Class.BLL.LMMSVisionMachineSettingHis_BLL bll = new Common.Class.BLL.LMMSVisionMachineSettingHis_BLL();
                List<Common.Class.Model.LMMSVisionMachineSettingHis_Model> models = bll.GetModelList(dateFrom, dateTo, arrMachineID, arrPartNo);

                List<ViewModel.LaserMachineSetting_ViewModel> viewModels = new List<ViewModel.LaserMachineSetting_ViewModel>();

                foreach (var model in models)
                {
                    ViewModel.LaserMachineSetting_ViewModel viewModel = new ViewModel.LaserMachineSetting_ViewModel();


                    viewModel.jobNo = model.jobNumber;
                    viewModel.partNo = model.partNumber;
                    viewModel.machineID = model.machineID;
                    viewModel.rate = model.rate == "" ? 0 : double.Parse(model.rate);
                    viewModel.frequency = model.frequency == "" ? 0 : double.Parse(model.frequency);
                    viewModel.power = model.power == "" ? 0 : double.Parse(model.power);
                    viewModel.sDateTime = model.dateTime.Value.ToString("yyyy/MM/dd HH:mm:ss");
                    viewModel.sDateTimeDisplay = model.dateTime.Value.ToString("MMddHHmmss");

                    viewModels.Add(viewModel);
                }

                viewModels = viewModels.OrderBy(item => item.sDateTime).ToList();


                JavaScriptSerializer js = new JavaScriptSerializer();
                string jsonResult = js.Serialize(viewModels);


                return Content(jsonResult);
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("LaserVisionSettingList", "btn_Generate_Click exception: " + ee.ToString());
                return null;
            }
        }


    }
}