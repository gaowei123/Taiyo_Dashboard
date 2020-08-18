using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.PQC
{
    public partial class PQCButtonReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    this.lblUserHeader.Text = "PQC Button Report";
                    this.title.Text = "Taiyo - Button Report";


                    //周日, 周一显示 上周五的. 默认显示前一天的.
                    DateTime dLastDay = Common.CommFunctions.GetDefaultReportsSearchingDay();




                    this.txtDateFrom.Text = dLastDay.ToString("yyyy-MM-dd");
                    //this.txtDateTo.Text = dLastDay.ToString("yyyy-MM-dd");




                    //SetColorDDL();
                    //SetSupplierDDL();
                    //SetModelDDL();


                    BtnGenerate_Click(new object(), new EventArgs());

                }
                catch (Exception ex)
                {
                    DBHelp.Reports.LogFile.Log("PQCButtonReport_New", "Page_Load error : " + ex.ToString());
                }
            }
        }












        protected void BtnGenerate_Click(object sender, EventArgs e)
        {
            try
            {


                //搜索条件
                string JobNo = "";// this.txtJobNo.Text.Trim();
                string partNumber = "";//this.txtPartNo.Text.Trim();
                string model = "";// this.ddlModel.SelectedValue;
                string color = "";// this.ddlColor.SelectedValue;
                string supplier = "";// this.ddlSupplier.SelectedValue;
                string coating = "";// this.ddlCoating.SelectedValue;
                DateTime DateFrom = DateTime.Parse(this.txtDateFrom.Text).Date;
                DateTime DateTo = DateFrom.AddDays(1);// DateTime.Parse(this.txtDateTo.Text).Date.AddDays(1);
                string reportType = this.ddlType.SelectedItem.Value; // 可以选定现显示 laser, wip部分列表
                string sDescription = "BUTTON";//除了panel, bezel的part都显示.
























                //先拉取满足条件的所有job id.
                List<string> jobs = GetAllDisplayJobs(DateFrom, DateTo, sDescription, partNumber, JobNo, model, supplier, color, coating);
                if (jobs == null || jobs.Count() == 0)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "There is no data, Please change searching condition!");
                    this.dgButton.Visible = false;
                    return;
                }



                //组合成sql in格式.
                string sqlWhere = "(";
                foreach (string jobNo in jobs)
                {
                    sqlWhere += "'" + jobNo + "',";
                }
                sqlWhere = sqlWhere.Substring(0, sqlWhere.Length - 1);
                sqlWhere += ")";
























                //获取数据源
                List<ViewModel.PQCButtonReport_ViewModel.PQCDetail> pqcDetailList = GetPQCDetialList(sqlWhere);
                
                List<ViewModel.PQCButtonReport_ViewModel.PQCDefect> pqcDefectList = GetPQCDefectList(sqlWhere);
                
                List<ViewModel.PQCButtonReport_ViewModel.LaserInfo> laserInfoList = GetLaserInfoList(sqlWhere);

                List<ViewModel.PQCButtonReport_ViewModel.PaintTempInfo> paintTempInfoList = GetPaintTempInfoList(sqlWhere);
                
                List<ViewModel.PQCButtonReport_ViewModel.PaintDelivery> paintDeliveryList = GetPaintDeliveryList(sqlWhere);
                
                List<ViewModel.PQCButtonReport_ViewModel.Report> reportList = new List<ViewModel.PQCButtonReport_ViewModel.Report>();
                foreach (var pqcdetailModel in pqcDetailList)
                {
                    #region 合并数据源 赋值到 PQCButtonReport_ViewModel.Report
                    ViewModel.PQCButtonReport_ViewModel.LaserInfo laserInfoModel = new ViewModel.PQCButtonReport_ViewModel.LaserInfo();
                    laserInfoModel = (from a in laserInfoList
                                      where a.jobNo == pqcdetailModel.jobID
                                      select a).FirstOrDefault();

                    ViewModel.PQCButtonReport_ViewModel.PaintTempInfo paintTempInfoModel = new ViewModel.PQCButtonReport_ViewModel.PaintTempInfo();
                    paintTempInfoModel = (from a in paintTempInfoList
                                          where a.jobNo == pqcdetailModel.jobID
                                          select a).FirstOrDefault();

                    ViewModel.PQCButtonReport_ViewModel.PaintDelivery paintDeliveryModel = new ViewModel.PQCButtonReport_ViewModel.PaintDelivery();
                    paintDeliveryModel = (from a in paintDeliveryList
                                          where a.jobNo == pqcdetailModel.jobID & a.paintProcess.ToUpper()== "PAINT#1"
                                          select a).FirstOrDefault();

                    List<ViewModel.PQCButtonReport_ViewModel.PQCDefect> jobDefectList = new List<ViewModel.PQCButtonReport_ViewModel.PQCDefect>();
                    jobDefectList = (from a in pqcDefectList
                                     where a.jobID == pqcdetailModel.jobID && a.materialNo == pqcdetailModel.materialNo
                                     select a).ToList();


                    ViewModel.PQCButtonReport_ViewModel.Report reportModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                    reportModel.partsType = pqcdetailModel.partsType;//区分laser, wip part.   
                    reportModel.model = pqcdetailModel.model;
                    reportModel.jobID = string.Format("<a href=\"../../Buyoff/OverallBuyoff?JobNumber={0}\" target=\"_blank\">{1}</a>", pqcdetailModel.jobID, pqcdetailModel.jobID);

                    reportModel.lotNo = paintDeliveryModel.lotNo;
                    reportModel.partNo = pqcdetailModel.partNumber;
                    reportModel.materialNo = pqcdetailModel.materialNo;
                    reportModel.lotQty = paintDeliveryModel.mrpQty;
                    reportModel.pass = pqcdetailModel.passQty;
                    //defect list中 rejqty的总和, 包括了laser ng, shortage, buyoff, setup, painting setup, painting qa
                    reportModel.rejQty = jobDefectList.Sum(p => p.rejectQty) + paintTempInfoModel.paintQAQty + paintTempInfoModel.paintSetUpQty;
                    reportModel.rejRate = Math.Round((jobDefectList.Sum(p => p.rejectQty) + paintTempInfoModel.paintQAQty + paintTempInfoModel.paintSetUpQty) / paintDeliveryModel.mrpQty * 100, 2);

                    reportModel.rejRateDisplay = string.Format("{0}({1}%)", reportModel.rejQty, reportModel.rejRate);

                    reportModel.supplier = pqcdetailModel.supplier;


                    //tts = tts rej/tts total,    vendor = vendor rej/ vendor total
                    if (pqcdetailModel.mouldType == "TTS")
                        reportModel.ttsLotQty = paintDeliveryModel.mrpQty;
                    else
                        reportModel.vendorLotQty = paintDeliveryModel.mrpQty;



                    #region  TTS defect code
                    reportModel.TTS_Raw_Part_Scratch = GetDefectCodeRejQty(jobDefectList, "Raw Part Scratch", "TTS");
                    reportModel.TTS_Oil_Stain = GetDefectCodeRejQty(jobDefectList, "Oil Stain", "TTS");
                    reportModel.TTS_Dented = GetDefectCodeRejQty(jobDefectList, "Dented", "TTS");
                    reportModel.TTS_Dust = GetDefectCodeRejQty(jobDefectList, "Dust", "TTS");
                    reportModel.TTS_Flyout = GetDefectCodeRejQty(jobDefectList, "Flyout", "TTS");
                    reportModel.TTS_Over_Spray = GetDefectCodeRejQty(jobDefectList, "Over Spray", "TTS");
                    reportModel.TTS_Weld_line = GetDefectCodeRejQty(jobDefectList, "Weld line", "TTS");
                    reportModel.TTS_Crack = GetDefectCodeRejQty(jobDefectList, "Crack", "TTS");
                    reportModel.TTS_Gas_mark = GetDefectCodeRejQty(jobDefectList, "Gas mark", "TTS");
                    reportModel.TTS_Sink_mark = GetDefectCodeRejQty(jobDefectList, "Sink mark", "TTS");
                    reportModel.TTS_Bubble = GetDefectCodeRejQty(jobDefectList, "Bubble", "TTS");
                    reportModel.TTS_White_dot = GetDefectCodeRejQty(jobDefectList, "White dot", "TTS");
                    reportModel.TTS_Black_dot = GetDefectCodeRejQty(jobDefectList, "Black dot", "TTS");
                    reportModel.TTS_Red_Dot = GetDefectCodeRejQty(jobDefectList, "Red Dot", "TTS");
                    reportModel.TTS_Poor_Gate_Cut = GetDefectCodeRejQty(jobDefectList, "Poor Gate Cut", "TTS");
                    reportModel.TTS_High_Gate = GetDefectCodeRejQty(jobDefectList, "High Gate", "TTS");
                    reportModel.TTS_White_Mark = GetDefectCodeRejQty(jobDefectList, "White Mark", "TTS");
                    reportModel.TTS_Drag_mark = GetDefectCodeRejQty(jobDefectList, "Drag mark", "TTS");
                    reportModel.TTS_Foreigh_Material = GetDefectCodeRejQty(jobDefectList, "Foreigh Material", "TTS");
                    reportModel.TTS_Double_Claim = GetDefectCodeRejQty(jobDefectList, "Double Claim", "TTS");
                    reportModel.TTS_Short_mould = GetDefectCodeRejQty(jobDefectList, "Short mould", "TTS");
                    reportModel.TTS_Flashing = GetDefectCodeRejQty(jobDefectList, "Flashing", "TTS");
                    reportModel.TTS_Pink_Mark = GetDefectCodeRejQty(jobDefectList, "Pink Mark", "TTS");
                    reportModel.TTS_Deform = GetDefectCodeRejQty(jobDefectList, "Deform", "TTS");
                    reportModel.TTS_Damage = GetDefectCodeRejQty(jobDefectList, "Damage", "TTS");
                    reportModel.TTS_Mould_Dirt = GetDefectCodeRejQty(jobDefectList, "Mould Dirt", "TTS");
                    reportModel.TTS_Yellowish = GetDefectCodeRejQty(jobDefectList, "Yellowish", "TTS");
                    reportModel.TTS_Oil_Mark = GetDefectCodeRejQty(jobDefectList, "Oil Mark", "TTS");
                    reportModel.TTS_Printing_Mark = GetDefectCodeRejQty(jobDefectList, "Printing Mark", "TTS");
                    reportModel.TTS_Printing_Uneven = GetDefectCodeRejQty(jobDefectList, "Printing Uneven", "TTS");
                    reportModel.TTS_Printing_Color_Dark = GetDefectCodeRejQty(jobDefectList, "Printing Color Dark", "TTS");
                    reportModel.TTS_Wrong_Orietation = GetDefectCodeRejQty(jobDefectList, "Wrong Orietation", "TTS");
                    reportModel.TTS_Other = GetDefectCodeRejQty(jobDefectList, "Other", "TTS");
                    #endregion

                    #region Vendor defect code
                    reportModel.Vendor_Raw_Part_Scratch = GetDefectCodeRejQty(jobDefectList, "Raw Part Scratch", "Vendor");
                    reportModel.Vendor_Oil_Stain = GetDefectCodeRejQty(jobDefectList, "Oil Stain", "Vendor");
                    reportModel.Vendor_Dented = GetDefectCodeRejQty(jobDefectList, "Dented", "Vendor");
                    reportModel.Vendor_Dust = GetDefectCodeRejQty(jobDefectList, "Dust", "Vendor");
                    reportModel.Vendor_Flyout = GetDefectCodeRejQty(jobDefectList, "Flyout", "Vendor");
                    reportModel.Vendor_Over_Spray = GetDefectCodeRejQty(jobDefectList, "Over Spray", "Vendor");
                    reportModel.Vendor_Weld_line = GetDefectCodeRejQty(jobDefectList, "Weld line", "Vendor");
                    reportModel.Vendor_Crack = GetDefectCodeRejQty(jobDefectList, "Crack", "Vendor");
                    reportModel.Vendor_Gas_mark = GetDefectCodeRejQty(jobDefectList, "Gas mark", "Vendor");
                    reportModel.Vendor_Sink_mark = GetDefectCodeRejQty(jobDefectList, "Sink mark", "Vendor");
                    reportModel.Vendor_Bubble = GetDefectCodeRejQty(jobDefectList, "Bubble", "Vendor");
                    reportModel.Vendor_White_dot = GetDefectCodeRejQty(jobDefectList, "White dot", "Vendor");
                    reportModel.Vendor_Black_dot = GetDefectCodeRejQty(jobDefectList, "Black dot", "Vendor");
                    reportModel.Vendor_Red_Dot = GetDefectCodeRejQty(jobDefectList, "Red Dot", "Vendor");
                    reportModel.Vendor_Poor_Gate_Cut = GetDefectCodeRejQty(jobDefectList, "Poor Gate Cut", "Vendor");
                    reportModel.Vendor_High_Gate = GetDefectCodeRejQty(jobDefectList, "High Gate", "Vendor");
                    reportModel.Vendor_White_Mark = GetDefectCodeRejQty(jobDefectList, "White Mark", "Vendor");
                    reportModel.Vendor_Drag_mark = GetDefectCodeRejQty(jobDefectList, "Drag mark", "Vendor");
                    reportModel.Vendor_Foreigh_Material = GetDefectCodeRejQty(jobDefectList, "Foreigh Material", "Vendor");
                    reportModel.Vendor_Double_Claim = GetDefectCodeRejQty(jobDefectList, "Double Claim", "Vendor");
                    reportModel.Vendor_Short_mould = GetDefectCodeRejQty(jobDefectList, "Short mould", "Vendor");
                    reportModel.Vendor_Flashing = GetDefectCodeRejQty(jobDefectList, "Flashing", "Vendor");
                    reportModel.Vendor_Pink_Mark = GetDefectCodeRejQty(jobDefectList, "Pink Mark", "Vendor");
                    reportModel.Vendor_Deform = GetDefectCodeRejQty(jobDefectList, "Deform", "Vendor");
                    reportModel.Vendor_Damage = GetDefectCodeRejQty(jobDefectList, "Damage", "Vendor");
                    reportModel.Vendor_Mould_Dirt = GetDefectCodeRejQty(jobDefectList, "Mould Dirt", "Vendor");
                    reportModel.Vendor_Yellowish = GetDefectCodeRejQty(jobDefectList, "Yellowish", "Vendor");
                    reportModel.Vendor_Oil_Mark = GetDefectCodeRejQty(jobDefectList, "Oil Mark", "Vendor");
                    reportModel.Vendor_Printing_Mark = GetDefectCodeRejQty(jobDefectList, "Printing Mark", "Vendor");
                    reportModel.Vendor_Printing_Uneven = GetDefectCodeRejQty(jobDefectList, "Printing Uneven", "Vendor");
                    reportModel.Vendor_Printing_Color_Dark = GetDefectCodeRejQty(jobDefectList, "Printing Color Dark", "Vendor");
                    reportModel.Vendor_Wrong_Orietation = GetDefectCodeRejQty(jobDefectList, "Wrong Orietation", "Vendor");
                    reportModel.Vendor_Other = GetDefectCodeRejQty(jobDefectList, "Other", "Vendor");
                    #endregion

                    #region Paint defect code
                    reportModel.Paint_Particle = GetDefectCodeRejQty(jobDefectList, "Particle", "Paint");
                    reportModel.Paint_Fibre = GetDefectCodeRejQty(jobDefectList, "Fibre", "Paint");
                    reportModel.Paint_Many_particle = GetDefectCodeRejQty(jobDefectList, "Many particle", "Paint");
                    reportModel.Paint_Stain_mark = GetDefectCodeRejQty(jobDefectList, "Stain mark", "Paint");
                    reportModel.Paint_Uneven_paint = GetDefectCodeRejQty(jobDefectList, "Uneven paint", "Paint");
                    reportModel.Paint_Under_coat_uneven_paint = GetDefectCodeRejQty(jobDefectList, "Under coat uneven paint", "Paint");
                    reportModel.Paint_Under_spray = GetDefectCodeRejQty(jobDefectList, "Under spray", "Paint");
                    reportModel.Paint_White_dot = GetDefectCodeRejQty(jobDefectList, "White dot", "Paint");
                    reportModel.Paint_Silver_dot = GetDefectCodeRejQty(jobDefectList, "Silver dot", "Paint");
                    reportModel.Paint_Dust = GetDefectCodeRejQty(jobDefectList, "Dust", "Paint");
                    reportModel.Paint_Paint_crack = GetDefectCodeRejQty(jobDefectList, "Paint crack", "Paint");
                    reportModel.Paint_Bubble = GetDefectCodeRejQty(jobDefectList, "Bubble", "Paint");
                    reportModel.Paint_Scratch = GetDefectCodeRejQty(jobDefectList, "Scratch", "Paint");
                    reportModel.Paint_Abrasion_Mark = GetDefectCodeRejQty(jobDefectList, "Abrasion Mark", "Paint");
                    reportModel.Paint_Paint_Dripping = GetDefectCodeRejQty(jobDefectList, "Paint Dripping", "Paint");
                    reportModel.Paint_Rough_Surface = GetDefectCodeRejQty(jobDefectList, "Rough Surface", "Paint");
                    reportModel.Paint_Shinning = GetDefectCodeRejQty(jobDefectList, "Shinning", "Paint");
                    reportModel.Paint_Matt = GetDefectCodeRejQty(jobDefectList, "Matt", "Paint");
                    reportModel.Paint_Paint_Pin_Hole = GetDefectCodeRejQty(jobDefectList, "Paint Pin Hole", "Paint");
                    reportModel.Paint_Light_Leakage = GetDefectCodeRejQty(jobDefectList, "Light Leakage", "Paint");
                    reportModel.Paint_White_Mark = GetDefectCodeRejQty(jobDefectList, "White Mark", "Paint");
                    reportModel.Paint_Dented = GetDefectCodeRejQty(jobDefectList, "Dented", "Paint");
                    reportModel.Paint_Other = GetDefectCodeRejQty(jobDefectList, "Other", "Paint");
                    reportModel.Paint_Particle_for_laser_setup = GetDefectCodeRejQty(jobDefectList, "Particle for laser setup", "Paint");
                    reportModel.Paint_Buyoff = GetDefectCodeRejQty(jobDefectList, "Buyoff", "Paint");
                    reportModel.Paint_Shortage = GetDefectCodeRejQty(jobDefectList, "Shortage", "Paint");//laser维护的shortage数量
                    #endregion

                    #region Laser defect code
                    reportModel.Laser_Black_Mark = GetDefectCodeRejQty(jobDefectList, "Black Mark", "Laser");
                    reportModel.Laser_Black_Dot = GetDefectCodeRejQty(jobDefectList, "Black Dot", "Laser");
                    reportModel.Laser_Graphic_Shift_check_by_PQC = GetDefectCodeRejQty(jobDefectList, "Graphic Shift check by PQC", "Laser");
                    reportModel.Laser_Graphic_Shift_check_by_MC = GetDefectCodeRejQty(jobDefectList, "Graphic Shift check by M/C", "Laser");//laser vision ng
                    reportModel.Laser_Scratch = GetDefectCodeRejQty(jobDefectList, "Scratch", "Laser");
                    reportModel.Laser_Jagged = GetDefectCodeRejQty(jobDefectList, "Jagged", "Laser");
                    reportModel.Laser_Laser_Bubble = GetDefectCodeRejQty(jobDefectList, "Laser Bubble", "Laser");
                    reportModel.Laser_double_outer_line = GetDefectCodeRejQty(jobDefectList, "double outer line", "Laser");
                    reportModel.Laser_Pin_hold = GetDefectCodeRejQty(jobDefectList, "Pin hold", "Laser");
                    reportModel.Laser_Poor_Laser = GetDefectCodeRejQty(jobDefectList, "Poor Laser", "Laser");
                    reportModel.Laser_Burm_Mark = GetDefectCodeRejQty(jobDefectList, "Burm Mark", "Laser");
                    reportModel.Laser_Stain_Mark = GetDefectCodeRejQty(jobDefectList, "Stain Mark", "Laser");
                    reportModel.Laser_Graphic_Small = GetDefectCodeRejQty(jobDefectList, "Graphic Small", "Laser");
                    reportModel.Laser_Double_Laser = GetDefectCodeRejQty(jobDefectList, "Double Laser", "Laser");
                    reportModel.Laser_Color_Yellow = GetDefectCodeRejQty(jobDefectList, "Color Yellow", "Laser");
                    reportModel.Laser_Crack = GetDefectCodeRejQty(jobDefectList, "Crack", "Laser");
                    reportModel.Laser_Smoke = GetDefectCodeRejQty(jobDefectList, "Smoke", "Laser");
                    reportModel.Laser_Wrong_Orientation = GetDefectCodeRejQty(jobDefectList, "Wrong Orientation", "Laser");
                    reportModel.Laser_Dented = GetDefectCodeRejQty(jobDefectList, "Dented", "Laser");
                    reportModel.Laser_Other = GetDefectCodeRejQty(jobDefectList, "Other", "Laser");
                    reportModel.Laser_Buyoff = GetDefectCodeRejQty(jobDefectList, "Buyoff", "Laser");//laser维护的buyoff数量
                    reportModel.Laser_Setup = GetDefectCodeRejQty(jobDefectList, "Setup", "Laser");//laser维护的setup数量
                    #endregion

                    #region Others defect code
                    reportModel.PQC_Scratch = GetDefectCodeRejQty(jobDefectList, "PQC Scratch", "Others");
                    reportModel.Over_Spray = GetDefectCodeRejQty(jobDefectList, "Over Spray", "Others");
                    reportModel.Bubble = GetDefectCodeRejQty(jobDefectList, "Bubble", "Others");
                    reportModel.Oil_Stain = GetDefectCodeRejQty(jobDefectList, "Oil Stain", "Others");
                    reportModel.Drag_Mark = GetDefectCodeRejQty(jobDefectList, "Drag Mark", "Others");
                    reportModel.Light_Leakage = GetDefectCodeRejQty(jobDefectList, "Light Leakage", "Others");
                    reportModel.Light_Bubble = GetDefectCodeRejQty(jobDefectList, "Light Bubble", "Others");
                    reportModel.White_Dot_in_Material = GetDefectCodeRejQty(jobDefectList, "White Dot in Material", "Others");
                    reportModel.Other = GetDefectCodeRejQty(jobDefectList, "Other", "Others");
                    #endregion


                    //TTS total Rej Qty & Reject Rate
                    reportModel.TTS_Mould_TotalRej = (from a in jobDefectList where a.defectDescription == "TTS" select a).Sum(p => p.rejectQty);
                    reportModel.TTS_Mould_TotalRejRate = Math.Round((from a in jobDefectList where a.defectDescription == "TTS" select a).Sum(p => p.rejectQty) / paintDeliveryModel.mrpQty * 100, 2);

                    //Vendor total rej qty & Reject Rate
                    reportModel.Vendor_Mould_TotalRej = (from a in jobDefectList where a.defectDescription == "Vendor" select a).Sum(p => p.rejectQty);
                    reportModel.Vendor_Mould_TotalRejRate = Math.Round((from a in jobDefectList where a.defectDescription == "Vendor" select a).Sum(p => p.rejectQty) / paintDeliveryModel.mrpQty * 100, 2);



                    //Paint total Rej Qty & Reject Rate
                    reportModel.Paint_TotalRej = (from a in jobDefectList where a.defectDescription == "Paint" select a).Sum(p => p.rejectQty);
                    reportModel.Paint_TotalRejRate = Math.Round((from a in jobDefectList where a.defectDescription == "Paint" select a).Sum(p => p.rejectQty) / paintDeliveryModel.mrpQty * 100, 2);


                    #region paint buyoff 信息
                    //早期job没有buyoff记录, 会找不到信息. 
                    if (paintTempInfoModel != null)
                    {
                        //mfgdate
                        reportModel.MFGDate = paintTempInfoModel == null ? null : paintTempInfoModel.mfgDate;

                        //paint setup, qa test qty & Rate
                        reportModel.Paint_SetupRej = paintTempInfoModel.paintSetUpQty;
                        reportModel.Paint_SetupRejRate = Math.Round(paintTempInfoModel.paintSetUpQty / paintDeliveryModel.mrpQty * 100, 2);
                        reportModel.Paint_QATestRej = paintTempInfoModel.paintQAQty;
                        reportModel.Paint_QATestRejRate = Math.Round(paintTempInfoModel.paintQAQty / paintDeliveryModel.mrpQty * 100, 2);

                        //paint coat, machine, date 
                        reportModel.paintCoat1st = paintTempInfoModel.paintCoat1st;
                        reportModel.paintMachine1st = paintTempInfoModel.paintMachine1st;
                        reportModel.paintDate1st = paintTempInfoModel.paintDate1st;
                        reportModel.paintCoat2nd = paintTempInfoModel.paintCoat2nd;
                        reportModel.paintMachine2nd = paintTempInfoModel.paintMachine2nd;
                        reportModel.paintDate2nd = paintTempInfoModel.paintDate2nd;
                        reportModel.paintCoat3rd = paintTempInfoModel.paintCoat3rd;
                        reportModel.paintMachine3rd = paintTempInfoModel.paintMachine3rd;
                        reportModel.paintDate3rd = paintTempInfoModel.paintDate3rd;
                    }
                    #endregion


                    //laser total rej & Rate
                    reportModel.Laser_TotalRej = (from a in jobDefectList where a.defectDescription == "Laser" select a).Sum(p => p.rejectQty);
                    reportModel.Laser_TotalRejRate = Math.Round((from a in jobDefectList where a.defectDescription == "Laser" select a).Sum(p => p.rejectQty) / paintDeliveryModel.mrpQty * 100, 2);


                    //没有laser工序的是找不到laser信息
                    if (laserInfoModel != null)
                    {
                        //laser machine, op, date    
                        reportModel.laserMachine = laserInfoModel.laserMachine;
                        reportModel.laserOP = laserInfoModel.laserOP;
                        reportModel.laserDate = laserInfoModel.laserDate;
                    }


                    //others total rej qty & rej rate
                    reportModel.Others_TotalRej = (from a in jobDefectList where a.defectDescription == "Others" select a).Sum(p => p.rejectQty);
                    reportModel.Others_TotalRejRate = Math.Round((from a in jobDefectList where a.defectDescription == "Others" select a).Sum(p => p.rejectQty) / paintDeliveryModel.mrpQty * 100, 2);

                    reportModel.InspBy = pqcdetailModel.OP;

                    reportList.Add(reportModel);
                    #endregion                    
                }














              










                #region 获取laser, wip类型汇总信息 
                var partsTypeSummaryList = from a in reportList
                                           group a by new { a.partsType } into modelGroup
                                           select new
                                           {
                                               partsType = modelGroup.Key.partsType,
                                               partNo = "",
                                               lotQty = modelGroup.Sum(p => p.lotQty),
                                               pass = modelGroup.Sum(p => p.pass),
                                               rejQty = modelGroup.Sum(p => p.rejQty),
                                               rejRate = Math.Round(modelGroup.Sum(p => p.rejQty) / modelGroup.Sum(p => p.lotQty) * 100, 2),

                                               

                                               ttsLotQty = modelGroup.Sum(p => p.ttsLotQty),
                                               vendorLotQty = modelGroup.Sum(p => p.vendorLotQty),




                                               //TTS defect code
                                               TTS_Raw_Part_Scratch = modelGroup.Sum(p => p.TTS_Raw_Part_Scratch),
                                               TTS_Oil_Stain = modelGroup.Sum(p => p.TTS_Oil_Stain),
                                               TTS_Dented = modelGroup.Sum(p => p.TTS_Dented),
                                               TTS_Dust = modelGroup.Sum(p => p.TTS_Dust),
                                               TTS_Flyout = modelGroup.Sum(p => p.TTS_Flyout),
                                               TTS_Over_Spray = modelGroup.Sum(p => p.TTS_Over_Spray),
                                               TTS_Weld_line = modelGroup.Sum(p => p.TTS_Weld_line),
                                               TTS_Crack = modelGroup.Sum(p => p.TTS_Crack),
                                               TTS_Gas_mark = modelGroup.Sum(p => p.TTS_Gas_mark),
                                               TTS_Sink_mark = modelGroup.Sum(p => p.TTS_Sink_mark),
                                               TTS_Bubble = modelGroup.Sum(p => p.TTS_Bubble),
                                               TTS_White_dot = modelGroup.Sum(p => p.TTS_White_dot),
                                               TTS_Black_dot = modelGroup.Sum(p => p.TTS_Black_dot),
                                               TTS_Red_Dot = modelGroup.Sum(p => p.TTS_Red_Dot),
                                               TTS_Poor_Gate_Cut = modelGroup.Sum(p => p.TTS_Poor_Gate_Cut),
                                               TTS_High_Gate = modelGroup.Sum(p => p.TTS_High_Gate),
                                               TTS_White_Mark = modelGroup.Sum(p => p.TTS_White_Mark),
                                               TTS_Drag_mark = modelGroup.Sum(p => p.TTS_Drag_mark),
                                               TTS_Foreigh_Material = modelGroup.Sum(p => p.TTS_Foreigh_Material),
                                               TTS_Double_Claim = modelGroup.Sum(p => p.TTS_Double_Claim),
                                               TTS_Short_mould = modelGroup.Sum(p => p.TTS_Short_mould),
                                               TTS_Flashing = modelGroup.Sum(p => p.TTS_Flashing),
                                               TTS_Pink_Mark = modelGroup.Sum(p => p.TTS_Pink_Mark),
                                               TTS_Deform = modelGroup.Sum(p => p.TTS_Deform),
                                               TTS_Damage = modelGroup.Sum(p => p.TTS_Damage),
                                               TTS_Mould_Dirt = modelGroup.Sum(p => p.TTS_Mould_Dirt),
                                               TTS_Yellowish = modelGroup.Sum(p => p.TTS_Yellowish),
                                               TTS_Oil_Mark = modelGroup.Sum(p => p.TTS_Oil_Mark),
                                               TTS_Printing_Mark = modelGroup.Sum(p => p.TTS_Printing_Mark),
                                               TTS_Printing_Uneven = modelGroup.Sum(p => p.TTS_Printing_Uneven),
                                               TTS_Printing_Color_Dark = modelGroup.Sum(p => p.TTS_Printing_Color_Dark),
                                               TTS_Wrong_Orietation = modelGroup.Sum(p => p.TTS_Wrong_Orietation),
                                               TTS_Other = modelGroup.Sum(p => p.TTS_Other),

                                               //vendor defect code
                                               Vendor_Raw_Part_Scratch = modelGroup.Sum(p => p.Vendor_Raw_Part_Scratch),
                                               Vendor_Oil_Stain = modelGroup.Sum(p => p.Vendor_Oil_Stain),
                                               Vendor_Dented = modelGroup.Sum(p => p.Vendor_Dented),
                                               Vendor_Dust = modelGroup.Sum(p => p.Vendor_Dust),
                                               Vendor_Flyout = modelGroup.Sum(p => p.Vendor_Flyout),
                                               Vendor_Over_Spray = modelGroup.Sum(p => p.Vendor_Over_Spray),
                                               Vendor_Weld_line = modelGroup.Sum(p => p.Vendor_Weld_line),
                                               Vendor_Crack = modelGroup.Sum(p => p.Vendor_Crack),
                                               Vendor_Gas_mark = modelGroup.Sum(p => p.Vendor_Gas_mark),
                                               Vendor_Sink_mark = modelGroup.Sum(p => p.Vendor_Sink_mark),
                                               Vendor_Bubble = modelGroup.Sum(p => p.Vendor_Bubble),
                                               Vendor_White_dot = modelGroup.Sum(p => p.Vendor_White_dot),
                                               Vendor_Black_dot = modelGroup.Sum(p => p.Vendor_Black_dot),
                                               Vendor_Red_Dot = modelGroup.Sum(p => p.Vendor_Red_Dot),
                                               Vendor_Poor_Gate_Cut = modelGroup.Sum(p => p.Vendor_Poor_Gate_Cut),
                                               Vendor_High_Gate = modelGroup.Sum(p => p.Vendor_High_Gate),
                                               Vendor_White_Mark = modelGroup.Sum(p => p.Vendor_White_Mark),
                                               Vendor_Drag_mark = modelGroup.Sum(p => p.Vendor_Drag_mark),
                                               Vendor_Foreigh_Material = modelGroup.Sum(p => p.Vendor_Foreigh_Material),
                                               Vendor_Double_Claim = modelGroup.Sum(p => p.Vendor_Double_Claim),
                                               Vendor_Short_mould = modelGroup.Sum(p => p.Vendor_Short_mould),
                                               Vendor_Flashing = modelGroup.Sum(p => p.Vendor_Flashing),
                                               Vendor_Pink_Mark = modelGroup.Sum(p => p.Vendor_Pink_Mark),
                                               Vendor_Deform = modelGroup.Sum(p => p.Vendor_Deform),
                                               Vendor_Damage = modelGroup.Sum(p => p.Vendor_Damage),
                                               Vendor_Mould_Dirt = modelGroup.Sum(p => p.Vendor_Mould_Dirt),
                                               Vendor_Yellowish = modelGroup.Sum(p => p.Vendor_Yellowish),
                                               Vendor_Oil_Mark = modelGroup.Sum(p => p.Vendor_Oil_Mark),
                                               Vendor_Printing_Mark = modelGroup.Sum(p => p.Vendor_Printing_Mark),
                                               Vendor_Printing_Uneven = modelGroup.Sum(p => p.Vendor_Printing_Uneven),
                                               Vendor_Printing_Color_Dark = modelGroup.Sum(p => p.Vendor_Printing_Color_Dark),
                                               Vendor_Wrong_Orietation = modelGroup.Sum(p => p.Vendor_Wrong_Orietation),
                                               Vendor_Other = modelGroup.Sum(p => p.Vendor_Other),

                                               //paint defect code
                                               Paint_Particle = modelGroup.Sum(p => p.Paint_Particle),
                                               Paint_Fibre = modelGroup.Sum(p => p.Paint_Fibre),
                                               Paint_Many_particle = modelGroup.Sum(p => p.Paint_Many_particle),
                                               Paint_Stain_mark = modelGroup.Sum(p => p.Paint_Stain_mark),
                                               Paint_Uneven_paint = modelGroup.Sum(p => p.Paint_Uneven_paint),
                                               Paint_Under_coat_uneven_paint = modelGroup.Sum(p => p.Paint_Under_coat_uneven_paint),
                                               Paint_Under_spray = modelGroup.Sum(p => p.Paint_Under_spray),
                                               Paint_White_dot = modelGroup.Sum(p => p.Paint_White_dot),
                                               Paint_Silver_dot = modelGroup.Sum(p => p.Paint_Silver_dot),
                                               Paint_Dust = modelGroup.Sum(p => p.Paint_Dust),
                                               Paint_Paint_crack = modelGroup.Sum(p => p.Paint_Paint_crack),
                                               Paint_Bubble = modelGroup.Sum(p => p.Paint_Bubble),
                                               Paint_Scratch = modelGroup.Sum(p => p.Paint_Scratch),
                                               Paint_Abrasion_Mark = modelGroup.Sum(p => p.Paint_Abrasion_Mark),
                                               Paint_Paint_Dripping = modelGroup.Sum(p => p.Paint_Paint_Dripping),
                                               Paint_Rough_Surface = modelGroup.Sum(p => p.Paint_Rough_Surface),
                                               Paint_Shinning = modelGroup.Sum(p => p.Paint_Shinning),
                                               Paint_Matt = modelGroup.Sum(p => p.Paint_Matt),
                                               Paint_Paint_Pin_Hole = modelGroup.Sum(p => p.Paint_Paint_Pin_Hole),
                                               Paint_Light_Leakage = modelGroup.Sum(p => p.Paint_Light_Leakage),
                                               Paint_White_Mark = modelGroup.Sum(p => p.Paint_White_Mark),
                                               Paint_Dented = modelGroup.Sum(p => p.Paint_Dented),
                                               Paint_Other = modelGroup.Sum(p => p.Paint_Other),
                                               Paint_Particle_for_laser_setup = modelGroup.Sum(p => p.Paint_Particle_for_laser_setup),
                                               Paint_Buyoff = modelGroup.Sum(p => p.Paint_Buyoff),
                                               Paint_Shortage = modelGroup.Sum(p => p.Paint_Shortage),

                                               //laser defect code
                                               Laser_Black_Mark = modelGroup.Sum(p => p.Laser_Black_Mark),
                                               Laser_Black_Dot = modelGroup.Sum(p => p.Laser_Black_Dot),
                                               Laser_Graphic_Shift_check_by_PQC = modelGroup.Sum(p => p.Laser_Graphic_Shift_check_by_PQC),
                                               Laser_Graphic_Shift_check_by_MC = modelGroup.Sum(p => p.Laser_Graphic_Shift_check_by_MC),
                                               Laser_Scratch = modelGroup.Sum(p => p.Laser_Scratch),
                                               Laser_Jagged = modelGroup.Sum(p => p.Laser_Jagged),
                                               Laser_Laser_Bubble = modelGroup.Sum(p => p.Laser_Laser_Bubble),
                                               Laser_double_outer_line = modelGroup.Sum(p => p.Laser_double_outer_line),
                                               Laser_Pin_hold = modelGroup.Sum(p => p.Laser_Pin_hold),
                                               Laser_Poor_Laser = modelGroup.Sum(p => p.Laser_Poor_Laser),
                                               Laser_Burm_Mark = modelGroup.Sum(p => p.Laser_Burm_Mark),
                                               Laser_Stain_Mark = modelGroup.Sum(p => p.Laser_Stain_Mark),
                                               Laser_Graphic_Small = modelGroup.Sum(p => p.Laser_Graphic_Small),
                                               Laser_Double_Laser = modelGroup.Sum(p => p.Laser_Double_Laser),
                                               Laser_Color_Yellow = modelGroup.Sum(p => p.Laser_Color_Yellow),
                                               Laser_Crack = modelGroup.Sum(p => p.Laser_Crack),
                                               Laser_Smoke = modelGroup.Sum(p => p.Laser_Smoke),
                                               Laser_Wrong_Orientation = modelGroup.Sum(p => p.Laser_Wrong_Orientation),
                                               Laser_Dented = modelGroup.Sum(p => p.Laser_Dented),
                                               Laser_Other = modelGroup.Sum(p => p.Laser_Other),
                                               Laser_Buyoff = modelGroup.Sum(p => p.Laser_Buyoff),
                                               Laser_Setup = modelGroup.Sum(p => p.Laser_Setup),

                                               //others defect code 
                                               PQC_Scratch = modelGroup.Sum(p => p.PQC_Scratch),
                                               Over_Spray = modelGroup.Sum(p => p.Over_Spray),
                                               Bubble = modelGroup.Sum(p => p.Bubble),
                                               Oil_Stain = modelGroup.Sum(p => p.Oil_Stain),
                                               Drag_Mark = modelGroup.Sum(p => p.Drag_Mark),
                                               Light_Leakage = modelGroup.Sum(p => p.Light_Leakage),
                                               Light_Bubble = modelGroup.Sum(p => p.Light_Bubble),
                                               White_Dot_in_Material = modelGroup.Sum(p => p.White_Dot_in_Material),
                                               Other = modelGroup.Sum(p => p.Other),


                                              
                                               TTS_Mould_TotalRej = modelGroup.Sum(p => p.TTS_Mould_TotalRej),
                                               TTS_Mould_TotalRejRate = Math.Round(modelGroup.Sum(p => p.TTS_Mould_TotalRej) / modelGroup.Sum(p => p.ttsLotQty) * 100, 2),

                                               Vendor_Mould_TotalRej = modelGroup.Sum(p => p.Vendor_Mould_TotalRej),
                                               Vendor_Mould_TotalRejRate = Math.Round(modelGroup.Sum(p => p.Vendor_Mould_TotalRej) / modelGroup.Sum(p => p.vendorLotQty) * 100, 2),

                                               Paint_TotalRej = modelGroup.Sum(p => p.Paint_TotalRej),
                                               Paint_TotalRejRate = Math.Round(modelGroup.Sum(p => p.Paint_TotalRej) / modelGroup.Sum(p => p.lotQty) * 100, 2),

                                               Laser_TotalRej = modelGroup.Sum(p => p.Laser_TotalRej),
                                               Laser_TotalRejRate = Math.Round(modelGroup.Sum(p => p.Laser_TotalRej) / modelGroup.Sum(p => p.lotQty) * 100, 2),

                                               Others_TotalRej = modelGroup.Sum(p => p.Others_TotalRej),
                                               Others_TotalRejRate = Math.Round(modelGroup.Sum(p => p.Others_TotalRej) / modelGroup.Sum(p => p.lotQty) * 100, 2),

                                               Paint_SetupRej = modelGroup.Sum(p => p.Paint_SetupRej),
                                               Paint_SetupRejRate = Math.Round(modelGroup.Sum(p => p.Paint_SetupRej) / modelGroup.Sum(p => p.lotQty) * 100, 2),
                                               Paint_QATestRej = modelGroup.Sum(p => p.Paint_QATestRej),
                                               Paint_QATestRejRate = Math.Round(modelGroup.Sum(p => p.Paint_QATestRej) / modelGroup.Sum(p => p.lotQty) * 100, 2)
                                           };
                #endregion


                #region 生成laser part summary 信息
                List<ViewModel.PQCButtonReport_ViewModel.Report> laserPartSummaryInfo = new List<ViewModel.PQCButtonReport_ViewModel.Report>();
                var laserPartModel = (from a in partsTypeSummaryList where a.partsType == "Laser" select a).FirstOrDefault();
                if (laserPartModel != null)
                {
                    #region add  laser summary part   others >
                    ViewModel.PQCButtonReport_ViewModel.Report othersLaserSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                    othersLaserSummaryModel.partNo = "OTHERS >";
                    othersLaserSummaryModel.rejQty = laserPartModel.Others_TotalRej;
                    othersLaserSummaryModel.rejRate = laserPartModel.Others_TotalRejRate;
                    othersLaserSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", othersLaserSummaryModel.rejQty, othersLaserSummaryModel.rejRate);



                    othersLaserSummaryModel.PQC_Scratch = laserPartModel.PQC_Scratch;
                    othersLaserSummaryModel.Over_Spray = laserPartModel.Over_Spray;
                    othersLaserSummaryModel.Bubble = laserPartModel.Bubble;
                    othersLaserSummaryModel.Oil_Stain = laserPartModel.Oil_Stain;
                    othersLaserSummaryModel.Drag_Mark = laserPartModel.Drag_Mark;
                    othersLaserSummaryModel.Light_Leakage = laserPartModel.Light_Leakage;
                    othersLaserSummaryModel.Light_Bubble = laserPartModel.Light_Bubble;
                    othersLaserSummaryModel.White_Dot_in_Material = laserPartModel.White_Dot_in_Material;
                    othersLaserSummaryModel.Other = laserPartModel.Other;

                    othersLaserSummaryModel.Others_TotalRej = laserPartModel.Others_TotalRej;
                    othersLaserSummaryModel.Others_TotalRejRate = laserPartModel.Others_TotalRejRate;


                    laserPartSummaryInfo.Add(othersLaserSummaryModel);
                    #endregion

                    #region add  laser summary part   tts - moulding >
                    ViewModel.PQCButtonReport_ViewModel.Report ttsMouldingLaserSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                    ttsMouldingLaserSummaryModel.partNo = "TTS MOULD >";
                    ttsMouldingLaserSummaryModel.lotQty = laserPartModel.ttsLotQty;
                    ttsMouldingLaserSummaryModel.rejQty = laserPartModel.TTS_Mould_TotalRej;
                    ttsMouldingLaserSummaryModel.rejRate = laserPartModel.TTS_Mould_TotalRejRate;
                    ttsMouldingLaserSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", ttsMouldingLaserSummaryModel.rejQty, ttsMouldingLaserSummaryModel.rejRate);

                    ttsMouldingLaserSummaryModel.TTS_Raw_Part_Scratch = laserPartModel.TTS_Raw_Part_Scratch;
                    ttsMouldingLaserSummaryModel.TTS_Oil_Stain = laserPartModel.TTS_Oil_Stain;
                    ttsMouldingLaserSummaryModel.TTS_Dented = laserPartModel.TTS_Dented;
                    ttsMouldingLaserSummaryModel.TTS_Dust = laserPartModel.TTS_Dust;
                    ttsMouldingLaserSummaryModel.TTS_Flyout = laserPartModel.TTS_Flyout;
                    ttsMouldingLaserSummaryModel.TTS_Over_Spray = laserPartModel.TTS_Over_Spray;
                    ttsMouldingLaserSummaryModel.TTS_Weld_line = laserPartModel.TTS_Weld_line;
                    ttsMouldingLaserSummaryModel.TTS_Crack = laserPartModel.TTS_Crack;
                    ttsMouldingLaserSummaryModel.TTS_Gas_mark = laserPartModel.TTS_Gas_mark;
                    ttsMouldingLaserSummaryModel.TTS_Sink_mark = laserPartModel.TTS_Sink_mark;
                    ttsMouldingLaserSummaryModel.TTS_Bubble = laserPartModel.TTS_Bubble;
                    ttsMouldingLaserSummaryModel.TTS_White_dot = laserPartModel.TTS_White_dot;
                    ttsMouldingLaserSummaryModel.TTS_Black_dot = laserPartModel.TTS_Black_dot;
                    ttsMouldingLaserSummaryModel.TTS_Red_Dot = laserPartModel.TTS_Red_Dot;
                    ttsMouldingLaserSummaryModel.TTS_Poor_Gate_Cut = laserPartModel.TTS_Poor_Gate_Cut;
                    ttsMouldingLaserSummaryModel.TTS_High_Gate = laserPartModel.TTS_High_Gate;
                    ttsMouldingLaserSummaryModel.TTS_White_Mark = laserPartModel.TTS_White_Mark;
                    ttsMouldingLaserSummaryModel.TTS_Drag_mark = laserPartModel.TTS_Drag_mark;
                    ttsMouldingLaserSummaryModel.TTS_Foreigh_Material = laserPartModel.TTS_Foreigh_Material;
                    ttsMouldingLaserSummaryModel.TTS_Double_Claim = laserPartModel.TTS_Double_Claim;
                    ttsMouldingLaserSummaryModel.TTS_Short_mould = laserPartModel.TTS_Short_mould;
                    ttsMouldingLaserSummaryModel.TTS_Flashing = laserPartModel.TTS_Flashing;
                    ttsMouldingLaserSummaryModel.TTS_Pink_Mark = laserPartModel.TTS_Pink_Mark;
                    ttsMouldingLaserSummaryModel.TTS_Deform = laserPartModel.TTS_Deform;
                    ttsMouldingLaserSummaryModel.TTS_Damage = laserPartModel.TTS_Damage;
                    ttsMouldingLaserSummaryModel.TTS_Mould_Dirt = laserPartModel.TTS_Mould_Dirt;
                    ttsMouldingLaserSummaryModel.TTS_Yellowish = laserPartModel.TTS_Yellowish;
                    ttsMouldingLaserSummaryModel.TTS_Oil_Mark = laserPartModel.TTS_Oil_Mark;
                    ttsMouldingLaserSummaryModel.TTS_Printing_Mark = laserPartModel.TTS_Printing_Mark;
                    ttsMouldingLaserSummaryModel.TTS_Printing_Uneven = laserPartModel.TTS_Printing_Uneven;
                    ttsMouldingLaserSummaryModel.TTS_Printing_Color_Dark = laserPartModel.TTS_Printing_Color_Dark;
                    ttsMouldingLaserSummaryModel.TTS_Wrong_Orietation = laserPartModel.TTS_Wrong_Orietation;
                    ttsMouldingLaserSummaryModel.TTS_Other = laserPartModel.TTS_Other;

                    ttsMouldingLaserSummaryModel.TTS_Mould_TotalRej = laserPartModel.TTS_Mould_TotalRej;
                    ttsMouldingLaserSummaryModel.TTS_Mould_TotalRejRate = laserPartModel.TTS_Mould_TotalRejRate;

                    laserPartSummaryInfo.Add(ttsMouldingLaserSummaryModel);
                    #endregion

                    #region add  laser summary part   vendor - moulding >
                    ViewModel.PQCButtonReport_ViewModel.Report vendorMouldingLaserSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                    vendorMouldingLaserSummaryModel.partNo = "VENDOR MOULD >";
                    vendorMouldingLaserSummaryModel.lotQty = laserPartModel.vendorLotQty;
                    vendorMouldingLaserSummaryModel.rejQty = laserPartModel.Vendor_Mould_TotalRej;
                    vendorMouldingLaserSummaryModel.rejRate = laserPartModel.Vendor_Mould_TotalRejRate;
                    vendorMouldingLaserSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", vendorMouldingLaserSummaryModel.rejQty, vendorMouldingLaserSummaryModel.rejRate);

                    vendorMouldingLaserSummaryModel.Vendor_Raw_Part_Scratch = laserPartModel.Vendor_Raw_Part_Scratch;
                    vendorMouldingLaserSummaryModel.Vendor_Oil_Stain = laserPartModel.Vendor_Oil_Stain;
                    vendorMouldingLaserSummaryModel.Vendor_Dented = laserPartModel.Vendor_Dented;
                    vendorMouldingLaserSummaryModel.Vendor_Dust = laserPartModel.Vendor_Dust;
                    vendorMouldingLaserSummaryModel.Vendor_Flyout = laserPartModel.Vendor_Flyout;
                    vendorMouldingLaserSummaryModel.Vendor_Over_Spray = laserPartModel.Vendor_Over_Spray;
                    vendorMouldingLaserSummaryModel.Vendor_Weld_line = laserPartModel.Vendor_Weld_line;
                    vendorMouldingLaserSummaryModel.Vendor_Crack = laserPartModel.Vendor_Crack;
                    vendorMouldingLaserSummaryModel.Vendor_Gas_mark = laserPartModel.Vendor_Gas_mark;
                    vendorMouldingLaserSummaryModel.Vendor_Sink_mark = laserPartModel.Vendor_Sink_mark;
                    vendorMouldingLaserSummaryModel.Vendor_Bubble = laserPartModel.Vendor_Bubble;
                    vendorMouldingLaserSummaryModel.Vendor_White_dot = laserPartModel.Vendor_White_dot;
                    vendorMouldingLaserSummaryModel.Vendor_Black_dot = laserPartModel.Vendor_Black_dot;
                    vendorMouldingLaserSummaryModel.Vendor_Red_Dot = laserPartModel.Vendor_Red_Dot;
                    vendorMouldingLaserSummaryModel.Vendor_Poor_Gate_Cut = laserPartModel.Vendor_Poor_Gate_Cut;
                    vendorMouldingLaserSummaryModel.Vendor_High_Gate = laserPartModel.Vendor_High_Gate;
                    vendorMouldingLaserSummaryModel.Vendor_White_Mark = laserPartModel.Vendor_White_Mark;
                    vendorMouldingLaserSummaryModel.Vendor_Drag_mark = laserPartModel.Vendor_Drag_mark;
                    vendorMouldingLaserSummaryModel.Vendor_Foreigh_Material = laserPartModel.Vendor_Foreigh_Material;
                    vendorMouldingLaserSummaryModel.Vendor_Double_Claim = laserPartModel.Vendor_Double_Claim;
                    vendorMouldingLaserSummaryModel.Vendor_Short_mould = laserPartModel.Vendor_Short_mould;
                    vendorMouldingLaserSummaryModel.Vendor_Flashing = laserPartModel.Vendor_Flashing;
                    vendorMouldingLaserSummaryModel.Vendor_Pink_Mark = laserPartModel.Vendor_Pink_Mark;
                    vendorMouldingLaserSummaryModel.Vendor_Deform = laserPartModel.Vendor_Deform;
                    vendorMouldingLaserSummaryModel.Vendor_Damage = laserPartModel.Vendor_Damage;
                    vendorMouldingLaserSummaryModel.Vendor_Mould_Dirt = laserPartModel.Vendor_Mould_Dirt;
                    vendorMouldingLaserSummaryModel.Vendor_Yellowish = laserPartModel.Vendor_Yellowish;
                    vendorMouldingLaserSummaryModel.Vendor_Oil_Mark = laserPartModel.Vendor_Oil_Mark;
                    vendorMouldingLaserSummaryModel.Vendor_Printing_Mark = laserPartModel.Vendor_Printing_Mark;
                    vendorMouldingLaserSummaryModel.Vendor_Printing_Uneven = laserPartModel.Vendor_Printing_Uneven;
                    vendorMouldingLaserSummaryModel.Vendor_Printing_Color_Dark = laserPartModel.Vendor_Printing_Color_Dark;
                    vendorMouldingLaserSummaryModel.Vendor_Wrong_Orietation = laserPartModel.Vendor_Wrong_Orietation;
                    vendorMouldingLaserSummaryModel.Vendor_Other = laserPartModel.Vendor_Other;

                    vendorMouldingLaserSummaryModel.Vendor_Mould_TotalRej = laserPartModel.Vendor_Mould_TotalRej;
                    vendorMouldingLaserSummaryModel.Vendor_Mould_TotalRejRate = laserPartModel.Vendor_Mould_TotalRejRate;

                    laserPartSummaryInfo.Add(vendorMouldingLaserSummaryModel);
                    #endregion

                    #region add  laser summary part   paint >
                    ViewModel.PQCButtonReport_ViewModel.Report paintLaserSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                    paintLaserSummaryModel.partNo = "PAINTING >";
                    paintLaserSummaryModel.rejQty = laserPartModel.Paint_TotalRej;
                    paintLaserSummaryModel.rejRate = laserPartModel.Paint_TotalRejRate;
                    paintLaserSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", paintLaserSummaryModel.rejQty, paintLaserSummaryModel.rejRate);

                    paintLaserSummaryModel.Paint_Particle = laserPartModel.Paint_Particle;
                    paintLaserSummaryModel.Paint_Fibre = laserPartModel.Paint_Fibre;
                    paintLaserSummaryModel.Paint_Many_particle = laserPartModel.Paint_Many_particle;
                    paintLaserSummaryModel.Paint_Stain_mark = laserPartModel.Paint_Stain_mark;
                    paintLaserSummaryModel.Paint_Uneven_paint = laserPartModel.Paint_Uneven_paint;
                    paintLaserSummaryModel.Paint_Under_coat_uneven_paint = laserPartModel.Paint_Under_coat_uneven_paint;
                    paintLaserSummaryModel.Paint_Under_spray = laserPartModel.Paint_Under_spray;
                    paintLaserSummaryModel.Paint_White_dot = laserPartModel.Paint_White_dot;
                    paintLaserSummaryModel.Paint_Silver_dot = laserPartModel.Paint_Silver_dot;
                    paintLaserSummaryModel.Paint_Dust = laserPartModel.Paint_Dust;
                    paintLaserSummaryModel.Paint_Paint_crack = laserPartModel.Paint_Paint_crack;
                    paintLaserSummaryModel.Paint_Bubble = laserPartModel.Paint_Bubble;
                    paintLaserSummaryModel.Paint_Scratch = laserPartModel.Paint_Scratch;
                    paintLaserSummaryModel.Paint_Abrasion_Mark = laserPartModel.Paint_Abrasion_Mark;
                    paintLaserSummaryModel.Paint_Paint_Dripping = laserPartModel.Paint_Paint_Dripping;
                    paintLaserSummaryModel.Paint_Rough_Surface = laserPartModel.Paint_Rough_Surface;
                    paintLaserSummaryModel.Paint_Shinning = laserPartModel.Paint_Shinning;
                    paintLaserSummaryModel.Paint_Matt = laserPartModel.Paint_Matt;
                    paintLaserSummaryModel.Paint_Paint_Pin_Hole = laserPartModel.Paint_Paint_Pin_Hole;
                    paintLaserSummaryModel.Paint_Light_Leakage = laserPartModel.Paint_Light_Leakage;
                    paintLaserSummaryModel.Paint_White_Mark = laserPartModel.Paint_White_Mark;
                    paintLaserSummaryModel.Paint_Dented = laserPartModel.Paint_Dented;
                    paintLaserSummaryModel.Paint_Other = laserPartModel.Paint_Other;
                    paintLaserSummaryModel.Paint_Particle_for_laser_setup = laserPartModel.Paint_Particle_for_laser_setup;
                    paintLaserSummaryModel.Paint_Buyoff = laserPartModel.Paint_Buyoff;
                    paintLaserSummaryModel.Paint_Shortage = laserPartModel.Paint_Shortage;

                    paintLaserSummaryModel.Paint_TotalRej = laserPartModel.Paint_TotalRej;
                    paintLaserSummaryModel.Paint_TotalRejRate = laserPartModel.Paint_TotalRejRate;

                    laserPartSummaryInfo.Add(paintLaserSummaryModel);
                    #endregion

                    // add  laser summary part   paint setup >
                    ViewModel.PQCButtonReport_ViewModel.Report paintSetupLaserSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                    paintSetupLaserSummaryModel.partNo = "PAINTING SETUP >";
                    paintSetupLaserSummaryModel.rejQty = laserPartModel.Paint_SetupRej;
                    paintSetupLaserSummaryModel.rejRate = laserPartModel.Paint_SetupRejRate;
                    paintSetupLaserSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", paintSetupLaserSummaryModel.rejQty, paintSetupLaserSummaryModel.rejRate);
                    laserPartSummaryInfo.Add(paintSetupLaserSummaryModel);

                    // add laser summary part   paint qa test >
                    ViewModel.PQCButtonReport_ViewModel.Report paintQALaserSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                    paintQALaserSummaryModel.partNo = "QA PAINT TEST >";
                    paintQALaserSummaryModel.rejQty = laserPartModel.Paint_QATestRej;
                    paintQALaserSummaryModel.rejRate = laserPartModel.Paint_QATestRejRate;
                    paintQALaserSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", paintQALaserSummaryModel.rejQty, paintQALaserSummaryModel.rejRate);
                    laserPartSummaryInfo.Add(paintQALaserSummaryModel);

                    #region add  laser summary part   laser >
                    ViewModel.PQCButtonReport_ViewModel.Report laserLaserSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                    laserLaserSummaryModel.partNo = "LASER >";
                    laserLaserSummaryModel.rejQty = laserPartModel.Laser_TotalRej;
                    laserLaserSummaryModel.rejRate = laserPartModel.Laser_TotalRejRate;
                    laserLaserSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", laserLaserSummaryModel.rejQty, laserLaserSummaryModel.rejRate);

                    laserLaserSummaryModel.Laser_Black_Mark = laserPartModel.Laser_Black_Mark;
                    laserLaserSummaryModel.Laser_Black_Dot = laserPartModel.Laser_Black_Dot;
                    laserLaserSummaryModel.Laser_Graphic_Shift_check_by_PQC = laserPartModel.Laser_Graphic_Shift_check_by_PQC;
                    laserLaserSummaryModel.Laser_Graphic_Shift_check_by_MC = laserPartModel.Laser_Graphic_Shift_check_by_MC;
                    laserLaserSummaryModel.Laser_Scratch = laserPartModel.Laser_Scratch;
                    laserLaserSummaryModel.Laser_Jagged = laserPartModel.Laser_Jagged;
                    laserLaserSummaryModel.Laser_Laser_Bubble = laserPartModel.Laser_Laser_Bubble;
                    laserLaserSummaryModel.Laser_double_outer_line = laserPartModel.Laser_double_outer_line;
                    laserLaserSummaryModel.Laser_Pin_hold = laserPartModel.Laser_Pin_hold;
                    laserLaserSummaryModel.Laser_Poor_Laser = laserPartModel.Laser_Poor_Laser;
                    laserLaserSummaryModel.Laser_Burm_Mark = laserPartModel.Laser_Burm_Mark;
                    laserLaserSummaryModel.Laser_Stain_Mark = laserPartModel.Laser_Stain_Mark;
                    laserLaserSummaryModel.Laser_Graphic_Small = laserPartModel.Laser_Graphic_Small;
                    laserLaserSummaryModel.Laser_Double_Laser = laserPartModel.Laser_Double_Laser;
                    laserLaserSummaryModel.Laser_Color_Yellow = laserPartModel.Laser_Color_Yellow;
                    laserLaserSummaryModel.Laser_Crack = laserPartModel.Laser_Crack;
                    laserLaserSummaryModel.Laser_Smoke = laserPartModel.Laser_Smoke;
                    laserLaserSummaryModel.Laser_Wrong_Orientation = laserPartModel.Laser_Wrong_Orientation;
                    laserLaserSummaryModel.Laser_Dented = laserPartModel.Laser_Dented;
                    laserLaserSummaryModel.Laser_Other = laserPartModel.Laser_Other;
                    laserLaserSummaryModel.Laser_Buyoff = laserPartModel.Laser_Buyoff;
                    laserLaserSummaryModel.Laser_Setup = laserPartModel.Laser_Setup;

                    laserLaserSummaryModel.Laser_TotalRej = laserPartModel.Laser_TotalRej;
                    laserLaserSummaryModel.Laser_TotalRejRate = laserPartModel.Laser_TotalRejRate;

                    laserPartSummaryInfo.Add(laserLaserSummaryModel);
                    #endregion


                    // add laser summary part   overall >
                    ViewModel.PQCButtonReport_ViewModel.Report overallLaserSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                    overallLaserSummaryModel.partNo = "OVERALL >";
                    overallLaserSummaryModel.lotQty = laserPartModel.lotQty;
                    overallLaserSummaryModel.pass = laserPartModel.pass;
                    overallLaserSummaryModel.rejQty = laserPartModel.rejQty;
                    overallLaserSummaryModel.rejRate = laserPartModel.rejRate;
                    overallLaserSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", overallLaserSummaryModel.rejQty, overallLaserSummaryModel.rejRate);

                    laserPartSummaryInfo.Add(overallLaserSummaryModel);
                }


                #endregion

                #region 生成wip part summary 信息
                List<ViewModel.PQCButtonReport_ViewModel.Report> WIPPartSummaryInfo = new List<ViewModel.PQCButtonReport_ViewModel.Report>();
                var wipPartModel = (from a in partsTypeSummaryList where a.partsType == "WIP" select a).FirstOrDefault();
                if (wipPartModel != null)
                {
                    #region add  laser summary part   others >
                    ViewModel.PQCButtonReport_ViewModel.Report othersWIPSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                    othersWIPSummaryModel.partNo = "OTHERS >";
                    othersWIPSummaryModel.rejQty = wipPartModel.Others_TotalRej;
                    othersWIPSummaryModel.rejRate = wipPartModel.Others_TotalRejRate;
                    othersWIPSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", othersWIPSummaryModel.rejQty, othersWIPSummaryModel.rejRate);

                    othersWIPSummaryModel.PQC_Scratch = wipPartModel.PQC_Scratch;
                    othersWIPSummaryModel.Over_Spray = wipPartModel.Over_Spray;
                    othersWIPSummaryModel.Bubble = wipPartModel.Bubble;
                    othersWIPSummaryModel.Oil_Stain = wipPartModel.Oil_Stain;
                    othersWIPSummaryModel.Drag_Mark = wipPartModel.Drag_Mark;
                    othersWIPSummaryModel.Light_Leakage = wipPartModel.Light_Leakage;
                    othersWIPSummaryModel.Light_Bubble = wipPartModel.Light_Bubble;
                    othersWIPSummaryModel.White_Dot_in_Material = wipPartModel.White_Dot_in_Material;
                    othersWIPSummaryModel.Other = wipPartModel.Other;
                    othersWIPSummaryModel.Others_TotalRej = wipPartModel.Others_TotalRej;
                    othersWIPSummaryModel.Others_TotalRejRate = wipPartModel.Others_TotalRejRate;


                    WIPPartSummaryInfo.Add(othersWIPSummaryModel);
                    #endregion

                    #region add  laser summary part   tts - moulding >
                    ViewModel.PQCButtonReport_ViewModel.Report ttsMouldingWIPSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                    ttsMouldingWIPSummaryModel.partNo = "TTS MOULD >";
                    ttsMouldingWIPSummaryModel.lotQty = wipPartModel.ttsLotQty;
                    ttsMouldingWIPSummaryModel.rejQty = wipPartModel.TTS_Mould_TotalRej;
                    ttsMouldingWIPSummaryModel.rejRate = wipPartModel.TTS_Mould_TotalRejRate;
                    ttsMouldingWIPSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", ttsMouldingWIPSummaryModel.rejQty, ttsMouldingWIPSummaryModel.rejRate);

                    ttsMouldingWIPSummaryModel.TTS_Raw_Part_Scratch = wipPartModel.TTS_Raw_Part_Scratch;
                    ttsMouldingWIPSummaryModel.TTS_Oil_Stain = wipPartModel.TTS_Oil_Stain;
                    ttsMouldingWIPSummaryModel.TTS_Dented = wipPartModel.TTS_Dented;
                    ttsMouldingWIPSummaryModel.TTS_Dust = wipPartModel.TTS_Dust;
                    ttsMouldingWIPSummaryModel.TTS_Flyout = wipPartModel.TTS_Flyout;
                    ttsMouldingWIPSummaryModel.TTS_Over_Spray = wipPartModel.TTS_Over_Spray;
                    ttsMouldingWIPSummaryModel.TTS_Weld_line = wipPartModel.TTS_Weld_line;
                    ttsMouldingWIPSummaryModel.TTS_Crack = wipPartModel.TTS_Crack;
                    ttsMouldingWIPSummaryModel.TTS_Gas_mark = wipPartModel.TTS_Gas_mark;
                    ttsMouldingWIPSummaryModel.TTS_Sink_mark = wipPartModel.TTS_Sink_mark;
                    ttsMouldingWIPSummaryModel.TTS_Bubble = wipPartModel.TTS_Bubble;
                    ttsMouldingWIPSummaryModel.TTS_White_dot = wipPartModel.TTS_White_dot;
                    ttsMouldingWIPSummaryModel.TTS_Black_dot = wipPartModel.TTS_Black_dot;
                    ttsMouldingWIPSummaryModel.TTS_Red_Dot = wipPartModel.TTS_Red_Dot;
                    ttsMouldingWIPSummaryModel.TTS_Poor_Gate_Cut = wipPartModel.TTS_Poor_Gate_Cut;
                    ttsMouldingWIPSummaryModel.TTS_High_Gate = wipPartModel.TTS_High_Gate;
                    ttsMouldingWIPSummaryModel.TTS_White_Mark = wipPartModel.TTS_White_Mark;
                    ttsMouldingWIPSummaryModel.TTS_Drag_mark = wipPartModel.TTS_Drag_mark;
                    ttsMouldingWIPSummaryModel.TTS_Foreigh_Material = wipPartModel.TTS_Foreigh_Material;
                    ttsMouldingWIPSummaryModel.TTS_Double_Claim = wipPartModel.TTS_Double_Claim;
                    ttsMouldingWIPSummaryModel.TTS_Short_mould = wipPartModel.TTS_Short_mould;
                    ttsMouldingWIPSummaryModel.TTS_Flashing = wipPartModel.TTS_Flashing;
                    ttsMouldingWIPSummaryModel.TTS_Pink_Mark = wipPartModel.TTS_Pink_Mark;
                    ttsMouldingWIPSummaryModel.TTS_Deform = wipPartModel.TTS_Deform;
                    ttsMouldingWIPSummaryModel.TTS_Damage = wipPartModel.TTS_Damage;
                    ttsMouldingWIPSummaryModel.TTS_Mould_Dirt = wipPartModel.TTS_Mould_Dirt;
                    ttsMouldingWIPSummaryModel.TTS_Yellowish = wipPartModel.TTS_Yellowish;
                    ttsMouldingWIPSummaryModel.TTS_Oil_Mark = wipPartModel.TTS_Oil_Mark;
                    ttsMouldingWIPSummaryModel.TTS_Printing_Mark = wipPartModel.TTS_Printing_Mark;
                    ttsMouldingWIPSummaryModel.TTS_Printing_Uneven = wipPartModel.TTS_Printing_Uneven;
                    ttsMouldingWIPSummaryModel.TTS_Printing_Color_Dark = wipPartModel.TTS_Printing_Color_Dark;
                    ttsMouldingWIPSummaryModel.TTS_Wrong_Orietation = wipPartModel.TTS_Wrong_Orietation;
                    ttsMouldingWIPSummaryModel.TTS_Other = wipPartModel.TTS_Other;
                    ttsMouldingWIPSummaryModel.TTS_Mould_TotalRej = wipPartModel.TTS_Mould_TotalRej;
                    ttsMouldingWIPSummaryModel.TTS_Mould_TotalRejRate = wipPartModel.TTS_Mould_TotalRejRate;

                    ttsMouldingWIPSummaryModel.TTS_Mould_TotalRej = wipPartModel.TTS_Mould_TotalRej;
                    ttsMouldingWIPSummaryModel.TTS_Mould_TotalRejRate = wipPartModel.TTS_Mould_TotalRejRate;

                    WIPPartSummaryInfo.Add(ttsMouldingWIPSummaryModel);
                    #endregion

                    #region add  laser summary part   vendor - moulding >
                    ViewModel.PQCButtonReport_ViewModel.Report vendorMouldingWIPSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                    vendorMouldingWIPSummaryModel.partNo = "VENDOR MOULD >";
                    vendorMouldingWIPSummaryModel.lotQty = wipPartModel.vendorLotQty;
                    vendorMouldingWIPSummaryModel.rejQty = wipPartModel.Vendor_Mould_TotalRej;
                    vendorMouldingWIPSummaryModel.rejRate = wipPartModel.Vendor_Mould_TotalRejRate;
                    vendorMouldingWIPSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", vendorMouldingWIPSummaryModel.rejQty, vendorMouldingWIPSummaryModel.rejRate);


                    vendorMouldingWIPSummaryModel.Vendor_Raw_Part_Scratch = wipPartModel.Vendor_Raw_Part_Scratch;
                    vendorMouldingWIPSummaryModel.Vendor_Oil_Stain = wipPartModel.Vendor_Oil_Stain;
                    vendorMouldingWIPSummaryModel.Vendor_Dented = wipPartModel.Vendor_Dented;
                    vendorMouldingWIPSummaryModel.Vendor_Dust = wipPartModel.Vendor_Dust;
                    vendorMouldingWIPSummaryModel.Vendor_Flyout = wipPartModel.Vendor_Flyout;
                    vendorMouldingWIPSummaryModel.Vendor_Over_Spray = wipPartModel.Vendor_Over_Spray;
                    vendorMouldingWIPSummaryModel.Vendor_Weld_line = wipPartModel.Vendor_Weld_line;
                    vendorMouldingWIPSummaryModel.Vendor_Crack = wipPartModel.Vendor_Crack;
                    vendorMouldingWIPSummaryModel.Vendor_Gas_mark = wipPartModel.Vendor_Gas_mark;
                    vendorMouldingWIPSummaryModel.Vendor_Sink_mark = wipPartModel.Vendor_Sink_mark;
                    vendorMouldingWIPSummaryModel.Vendor_Bubble = wipPartModel.Vendor_Bubble;
                    vendorMouldingWIPSummaryModel.Vendor_White_dot = wipPartModel.Vendor_White_dot;
                    vendorMouldingWIPSummaryModel.Vendor_Black_dot = wipPartModel.Vendor_Black_dot;
                    vendorMouldingWIPSummaryModel.Vendor_Red_Dot = wipPartModel.Vendor_Red_Dot;
                    vendorMouldingWIPSummaryModel.Vendor_Poor_Gate_Cut = wipPartModel.Vendor_Poor_Gate_Cut;
                    vendorMouldingWIPSummaryModel.Vendor_High_Gate = wipPartModel.Vendor_High_Gate;
                    vendorMouldingWIPSummaryModel.Vendor_White_Mark = wipPartModel.Vendor_White_Mark;
                    vendorMouldingWIPSummaryModel.Vendor_Drag_mark = wipPartModel.Vendor_Drag_mark;
                    vendorMouldingWIPSummaryModel.Vendor_Foreigh_Material = wipPartModel.Vendor_Foreigh_Material;
                    vendorMouldingWIPSummaryModel.Vendor_Double_Claim = wipPartModel.Vendor_Double_Claim;
                    vendorMouldingWIPSummaryModel.Vendor_Short_mould = wipPartModel.Vendor_Short_mould;
                    vendorMouldingWIPSummaryModel.Vendor_Flashing = wipPartModel.Vendor_Flashing;
                    vendorMouldingWIPSummaryModel.Vendor_Pink_Mark = wipPartModel.Vendor_Pink_Mark;
                    vendorMouldingWIPSummaryModel.Vendor_Deform = wipPartModel.Vendor_Deform;
                    vendorMouldingWIPSummaryModel.Vendor_Damage = wipPartModel.Vendor_Damage;
                    vendorMouldingWIPSummaryModel.Vendor_Mould_Dirt = wipPartModel.Vendor_Mould_Dirt;
                    vendorMouldingWIPSummaryModel.Vendor_Yellowish = wipPartModel.Vendor_Yellowish;
                    vendorMouldingWIPSummaryModel.Vendor_Oil_Mark = wipPartModel.Vendor_Oil_Mark;
                    vendorMouldingWIPSummaryModel.Vendor_Printing_Mark = wipPartModel.Vendor_Printing_Mark;
                    vendorMouldingWIPSummaryModel.Vendor_Printing_Uneven = wipPartModel.Vendor_Printing_Uneven;
                    vendorMouldingWIPSummaryModel.Vendor_Printing_Color_Dark = wipPartModel.Vendor_Printing_Color_Dark;
                    vendorMouldingWIPSummaryModel.Vendor_Wrong_Orietation = wipPartModel.Vendor_Wrong_Orietation;
                    vendorMouldingWIPSummaryModel.Vendor_Other = wipPartModel.Vendor_Other;

                    vendorMouldingWIPSummaryModel.Vendor_Mould_TotalRej = wipPartModel.Vendor_Mould_TotalRej;
                    vendorMouldingWIPSummaryModel.Vendor_Mould_TotalRejRate = wipPartModel.Vendor_Mould_TotalRejRate;

                    WIPPartSummaryInfo.Add(vendorMouldingWIPSummaryModel);
                    #endregion

                    #region add  laser summary part   paint >
                    ViewModel.PQCButtonReport_ViewModel.Report paintWIPSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                    paintWIPSummaryModel.partNo = "PAINTING >";
                    paintWIPSummaryModel.rejQty = wipPartModel.Paint_TotalRej;
                    paintWIPSummaryModel.rejRate = wipPartModel.Paint_TotalRejRate;
                    paintWIPSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", paintWIPSummaryModel.rejQty, paintWIPSummaryModel.rejRate);

                    paintWIPSummaryModel.Paint_Particle = wipPartModel.Paint_Particle;
                    paintWIPSummaryModel.Paint_Fibre = wipPartModel.Paint_Fibre;
                    paintWIPSummaryModel.Paint_Many_particle = wipPartModel.Paint_Many_particle;
                    paintWIPSummaryModel.Paint_Stain_mark = wipPartModel.Paint_Stain_mark;
                    paintWIPSummaryModel.Paint_Uneven_paint = wipPartModel.Paint_Uneven_paint;
                    paintWIPSummaryModel.Paint_Under_coat_uneven_paint = wipPartModel.Paint_Under_coat_uneven_paint;
                    paintWIPSummaryModel.Paint_Under_spray = wipPartModel.Paint_Under_spray;
                    paintWIPSummaryModel.Paint_White_dot = wipPartModel.Paint_White_dot;
                    paintWIPSummaryModel.Paint_Silver_dot = wipPartModel.Paint_Silver_dot;
                    paintWIPSummaryModel.Paint_Dust = wipPartModel.Paint_Dust;
                    paintWIPSummaryModel.Paint_Paint_crack = wipPartModel.Paint_Paint_crack;
                    paintWIPSummaryModel.Paint_Bubble = wipPartModel.Paint_Bubble;
                    paintWIPSummaryModel.Paint_Scratch = wipPartModel.Paint_Scratch;
                    paintWIPSummaryModel.Paint_Abrasion_Mark = wipPartModel.Paint_Abrasion_Mark;
                    paintWIPSummaryModel.Paint_Paint_Dripping = wipPartModel.Paint_Paint_Dripping;
                    paintWIPSummaryModel.Paint_Rough_Surface = wipPartModel.Paint_Rough_Surface;
                    paintWIPSummaryModel.Paint_Shinning = wipPartModel.Paint_Shinning;
                    paintWIPSummaryModel.Paint_Matt = wipPartModel.Paint_Matt;
                    paintWIPSummaryModel.Paint_Paint_Pin_Hole = wipPartModel.Paint_Paint_Pin_Hole;
                    paintWIPSummaryModel.Paint_Light_Leakage = wipPartModel.Paint_Light_Leakage;
                    paintWIPSummaryModel.Paint_White_Mark = wipPartModel.Paint_White_Mark;
                    paintWIPSummaryModel.Paint_Dented = wipPartModel.Paint_Dented;
                    paintWIPSummaryModel.Paint_Other = wipPartModel.Paint_Other;
                    paintWIPSummaryModel.Paint_Particle_for_laser_setup = wipPartModel.Paint_Particle_for_laser_setup;
                    paintWIPSummaryModel.Paint_Buyoff = wipPartModel.Paint_Buyoff;
                    paintWIPSummaryModel.Paint_Shortage = wipPartModel.Paint_Shortage;

                    paintWIPSummaryModel.Paint_TotalRej = wipPartModel.Paint_TotalRej;
                    paintWIPSummaryModel.Paint_TotalRejRate = wipPartModel.Paint_TotalRejRate;


                    WIPPartSummaryInfo.Add(paintWIPSummaryModel);
                    #endregion

                    // add  laser summary part   paint setup >
                    ViewModel.PQCButtonReport_ViewModel.Report paintSetupWIPSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                    paintSetupWIPSummaryModel.partNo = "PAINTING SETUP >";
                    paintSetupWIPSummaryModel.rejQty = wipPartModel.Paint_SetupRej;
                    paintSetupWIPSummaryModel.rejRate = wipPartModel.Paint_SetupRejRate;
                    paintSetupWIPSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", paintSetupWIPSummaryModel.rejQty, paintSetupWIPSummaryModel.rejRate);

                    WIPPartSummaryInfo.Add(paintSetupWIPSummaryModel);

                    // add laser summary part   paint qa test >
                    ViewModel.PQCButtonReport_ViewModel.Report paintQAWIPSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                    paintQAWIPSummaryModel.partNo = "QA PAINT TEST >";
                    paintQAWIPSummaryModel.rejQty = wipPartModel.Paint_QATestRej;
                    paintQAWIPSummaryModel.rejRate = wipPartModel.Paint_QATestRejRate;
                    paintQAWIPSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", paintQAWIPSummaryModel.rejQty, paintQAWIPSummaryModel.rejRate);

                    WIPPartSummaryInfo.Add(paintQAWIPSummaryModel);

                    #region add  laser summary part   laser >
                    ViewModel.PQCButtonReport_ViewModel.Report laserWIPSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                    laserWIPSummaryModel.partNo = "LASER >";
                    laserWIPSummaryModel.rejQty = wipPartModel.Laser_TotalRej;
                    laserWIPSummaryModel.rejRate = wipPartModel.Laser_TotalRejRate;
                    laserWIPSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", laserWIPSummaryModel.rejQty, laserWIPSummaryModel.rejRate);

                    laserWIPSummaryModel.Laser_Black_Mark = wipPartModel.Laser_Black_Mark;
                    laserWIPSummaryModel.Laser_Black_Dot = wipPartModel.Laser_Black_Dot;
                    laserWIPSummaryModel.Laser_Graphic_Shift_check_by_PQC = wipPartModel.Laser_Graphic_Shift_check_by_PQC;
                    laserWIPSummaryModel.Laser_Graphic_Shift_check_by_MC = wipPartModel.Laser_Graphic_Shift_check_by_MC;
                    laserWIPSummaryModel.Laser_Scratch = wipPartModel.Laser_Scratch;
                    laserWIPSummaryModel.Laser_Jagged = wipPartModel.Laser_Jagged;
                    laserWIPSummaryModel.Laser_Laser_Bubble = wipPartModel.Laser_Laser_Bubble;
                    laserWIPSummaryModel.Laser_double_outer_line = wipPartModel.Laser_double_outer_line;
                    laserWIPSummaryModel.Laser_Pin_hold = wipPartModel.Laser_Pin_hold;
                    laserWIPSummaryModel.Laser_Poor_Laser = wipPartModel.Laser_Poor_Laser;
                    laserWIPSummaryModel.Laser_Burm_Mark = wipPartModel.Laser_Burm_Mark;
                    laserWIPSummaryModel.Laser_Stain_Mark = wipPartModel.Laser_Stain_Mark;
                    laserWIPSummaryModel.Laser_Graphic_Small = wipPartModel.Laser_Graphic_Small;
                    laserWIPSummaryModel.Laser_Double_Laser = wipPartModel.Laser_Double_Laser;
                    laserWIPSummaryModel.Laser_Color_Yellow = wipPartModel.Laser_Color_Yellow;
                    laserWIPSummaryModel.Laser_Crack = wipPartModel.Laser_Crack;
                    laserWIPSummaryModel.Laser_Smoke = wipPartModel.Laser_Smoke;
                    laserWIPSummaryModel.Laser_Wrong_Orientation = wipPartModel.Laser_Wrong_Orientation;
                    laserWIPSummaryModel.Laser_Dented = wipPartModel.Laser_Dented;
                    laserWIPSummaryModel.Laser_Other = wipPartModel.Laser_Other;
                    laserWIPSummaryModel.Laser_Buyoff = wipPartModel.Laser_Buyoff;
                    laserWIPSummaryModel.Laser_Setup = wipPartModel.Laser_Setup;

                    laserWIPSummaryModel.Laser_TotalRej = wipPartModel.Laser_TotalRej;
                    laserWIPSummaryModel.Laser_TotalRejRate  = wipPartModel.Laser_TotalRejRate;

                    WIPPartSummaryInfo.Add(laserWIPSummaryModel);
                    #endregion

                    // add laser summary part   overall >
                    ViewModel.PQCButtonReport_ViewModel.Report overallWIPSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                    overallWIPSummaryModel.partNo = "OVERALL >";
                    overallWIPSummaryModel.lotQty = wipPartModel.lotQty;
                    overallWIPSummaryModel.pass = wipPartModel.pass;
                    overallWIPSummaryModel.rejQty = wipPartModel.rejQty;
                    overallWIPSummaryModel.rejRate = wipPartModel.rejRate;
                    overallWIPSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", overallWIPSummaryModel.rejQty, overallWIPSummaryModel.rejRate);

                    WIPPartSummaryInfo.Add(overallWIPSummaryModel);
                }
                #endregion





















                #region 生成 overall part summary 信息

                List< ViewModel.PQCButtonReport_ViewModel.Report> OverallSummaryInfo = new List<ViewModel.PQCButtonReport_ViewModel.Report>();

                #region add  laser summary part   others >
                ViewModel.PQCButtonReport_ViewModel.Report othersOverallSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                othersOverallSummaryModel.partNo = "OTHERS >";
                othersOverallSummaryModel.rejQty = partsTypeSummaryList.Sum(p => p.Others_TotalRej);
                othersOverallSummaryModel.rejRate = Math.Round(partsTypeSummaryList.Sum(p => p.Others_TotalRej) / partsTypeSummaryList.Sum(p => p.lotQty) * 100, 2);
                othersOverallSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", othersOverallSummaryModel.rejQty, othersOverallSummaryModel.rejRate);

                othersOverallSummaryModel.PQC_Scratch = partsTypeSummaryList.Sum(p => p.PQC_Scratch);
                othersOverallSummaryModel.Over_Spray = partsTypeSummaryList.Sum(p => p.Over_Spray);
                othersOverallSummaryModel.Bubble = partsTypeSummaryList.Sum(p => p.Bubble);
                othersOverallSummaryModel.Oil_Stain = partsTypeSummaryList.Sum(p => p.Oil_Stain);
                othersOverallSummaryModel.Drag_Mark = partsTypeSummaryList.Sum(p => p.Drag_Mark);
                othersOverallSummaryModel.Light_Leakage = partsTypeSummaryList.Sum(p => p.Light_Leakage);
                othersOverallSummaryModel.Light_Bubble = partsTypeSummaryList.Sum(p => p.Light_Bubble);
                othersOverallSummaryModel.White_Dot_in_Material = partsTypeSummaryList.Sum(p => p.White_Dot_in_Material);
                othersOverallSummaryModel.Other = partsTypeSummaryList.Sum(p => p.Other);

                othersOverallSummaryModel.Others_TotalRej = partsTypeSummaryList.Sum(p => p.Others_TotalRej);
                othersOverallSummaryModel.Others_TotalRejRate = Math.Round(partsTypeSummaryList.Sum(p => p.Others_TotalRej) / partsTypeSummaryList.Sum(p => p.lotQty) * 100, 2);

                OverallSummaryInfo.Add(othersOverallSummaryModel);
                #endregion

                #region add  laser summary part   tts - moulding >
                ViewModel.PQCButtonReport_ViewModel.Report ttsMouldingOverallSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                ttsMouldingOverallSummaryModel.partNo = ("TTS MOULD >");
                ttsMouldingOverallSummaryModel.lotQty = partsTypeSummaryList.Sum(p => p.ttsLotQty);
                ttsMouldingOverallSummaryModel.rejQty = partsTypeSummaryList.Sum(p => p.TTS_Mould_TotalRej);
                ttsMouldingOverallSummaryModel.rejRate = Math.Round(partsTypeSummaryList.Sum(p => p.TTS_Mould_TotalRej) / partsTypeSummaryList.Sum(p => p.ttsLotQty) * 100 , 2);
                ttsMouldingOverallSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", ttsMouldingOverallSummaryModel.rejQty, ttsMouldingOverallSummaryModel.rejRate);


                ttsMouldingOverallSummaryModel.TTS_Raw_Part_Scratch = partsTypeSummaryList.Sum(p => p.TTS_Raw_Part_Scratch);
                ttsMouldingOverallSummaryModel.TTS_Oil_Stain = partsTypeSummaryList.Sum(p => p.TTS_Oil_Stain);
                ttsMouldingOverallSummaryModel.TTS_Dented = partsTypeSummaryList.Sum(p => p.TTS_Dented);
                ttsMouldingOverallSummaryModel.TTS_Dust = partsTypeSummaryList.Sum(p => p.TTS_Dust);
                ttsMouldingOverallSummaryModel.TTS_Flyout = partsTypeSummaryList.Sum(p => p.TTS_Flyout);
                ttsMouldingOverallSummaryModel.TTS_Over_Spray = partsTypeSummaryList.Sum(p => p.TTS_Over_Spray);
                ttsMouldingOverallSummaryModel.TTS_Weld_line = partsTypeSummaryList.Sum(p => p.TTS_Weld_line);
                ttsMouldingOverallSummaryModel.TTS_Crack = partsTypeSummaryList.Sum(p => p.TTS_Crack);
                ttsMouldingOverallSummaryModel.TTS_Gas_mark = partsTypeSummaryList.Sum(p => p.TTS_Gas_mark);
                ttsMouldingOverallSummaryModel.TTS_Sink_mark = partsTypeSummaryList.Sum(p => p.TTS_Sink_mark);
                ttsMouldingOverallSummaryModel.TTS_Bubble = partsTypeSummaryList.Sum(p => p.TTS_Bubble);
                ttsMouldingOverallSummaryModel.TTS_White_dot = partsTypeSummaryList.Sum(p => p.TTS_White_dot);
                ttsMouldingOverallSummaryModel.TTS_Black_dot = partsTypeSummaryList.Sum(p => p.TTS_Black_dot);
                ttsMouldingOverallSummaryModel.TTS_Red_Dot = partsTypeSummaryList.Sum(p => p.TTS_Red_Dot);
                ttsMouldingOverallSummaryModel.TTS_Poor_Gate_Cut = partsTypeSummaryList.Sum(p => p.TTS_Poor_Gate_Cut);
                ttsMouldingOverallSummaryModel.TTS_High_Gate = partsTypeSummaryList.Sum(p => p.TTS_High_Gate);
                ttsMouldingOverallSummaryModel.TTS_White_Mark = partsTypeSummaryList.Sum(p => p.TTS_White_Mark);
                ttsMouldingOverallSummaryModel.TTS_Drag_mark = partsTypeSummaryList.Sum(p => p.TTS_Drag_mark);
                ttsMouldingOverallSummaryModel.TTS_Foreigh_Material = partsTypeSummaryList.Sum(p => p.TTS_Foreigh_Material);
                ttsMouldingOverallSummaryModel.TTS_Double_Claim = partsTypeSummaryList.Sum(p => p.TTS_Double_Claim);
                ttsMouldingOverallSummaryModel.TTS_Short_mould = partsTypeSummaryList.Sum(p => p.TTS_Short_mould);
                ttsMouldingOverallSummaryModel.TTS_Flashing = partsTypeSummaryList.Sum(p => p.TTS_Flashing);
                ttsMouldingOverallSummaryModel.TTS_Pink_Mark = partsTypeSummaryList.Sum(p => p.TTS_Pink_Mark);
                ttsMouldingOverallSummaryModel.TTS_Deform = partsTypeSummaryList.Sum(p => p.TTS_Deform);
                ttsMouldingOverallSummaryModel.TTS_Damage = partsTypeSummaryList.Sum(p => p.TTS_Damage);
                ttsMouldingOverallSummaryModel.TTS_Mould_Dirt = partsTypeSummaryList.Sum(p => p.TTS_Mould_Dirt);
                ttsMouldingOverallSummaryModel.TTS_Yellowish = partsTypeSummaryList.Sum(p => p.TTS_Yellowish);
                ttsMouldingOverallSummaryModel.TTS_Oil_Mark = partsTypeSummaryList.Sum(p => p.TTS_Oil_Mark);
                ttsMouldingOverallSummaryModel.TTS_Printing_Mark = partsTypeSummaryList.Sum(p => p.TTS_Printing_Mark);
                ttsMouldingOverallSummaryModel.TTS_Printing_Uneven = partsTypeSummaryList.Sum(p => p.TTS_Printing_Uneven);
                ttsMouldingOverallSummaryModel.TTS_Printing_Color_Dark = partsTypeSummaryList.Sum(p => p.TTS_Printing_Color_Dark);
                ttsMouldingOverallSummaryModel.TTS_Wrong_Orietation = partsTypeSummaryList.Sum(p => p.TTS_Wrong_Orietation);
                ttsMouldingOverallSummaryModel.TTS_Other = partsTypeSummaryList.Sum(p => p.TTS_Other);

                ttsMouldingOverallSummaryModel.TTS_Mould_TotalRej = partsTypeSummaryList.Sum(p => p.TTS_Mould_TotalRej);
                ttsMouldingOverallSummaryModel.TTS_Mould_TotalRejRate = Math.Round(partsTypeSummaryList.Sum(p => p.TTS_Mould_TotalRej) / partsTypeSummaryList.Sum(p => p.ttsLotQty) * 100, 2);


                OverallSummaryInfo.Add(ttsMouldingOverallSummaryModel);
                #endregion

                #region add  laser summary part   vendor - moulding >
                ViewModel.PQCButtonReport_ViewModel.Report vendorMouldingOverallSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                vendorMouldingOverallSummaryModel.partNo = ("VENDOR MOULD >");
                vendorMouldingOverallSummaryModel.lotQty = partsTypeSummaryList.Sum(p => p.vendorLotQty);
                vendorMouldingOverallSummaryModel.rejQty = partsTypeSummaryList.Sum(p => p.Vendor_Mould_TotalRej);
                vendorMouldingOverallSummaryModel.rejRate = Math.Round(partsTypeSummaryList.Sum(p => p.Vendor_Mould_TotalRej) / partsTypeSummaryList.Sum(p => p.vendorLotQty) * 100, 2);
                vendorMouldingOverallSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", vendorMouldingOverallSummaryModel.rejQty, vendorMouldingOverallSummaryModel.rejRate);


                vendorMouldingOverallSummaryModel.Vendor_Raw_Part_Scratch = partsTypeSummaryList.Sum(p => p.Vendor_Raw_Part_Scratch);
                vendorMouldingOverallSummaryModel.Vendor_Oil_Stain = partsTypeSummaryList.Sum(p => p.Vendor_Oil_Stain);
                vendorMouldingOverallSummaryModel.Vendor_Dented = partsTypeSummaryList.Sum(p => p.Vendor_Dented);
                vendorMouldingOverallSummaryModel.Vendor_Dust = partsTypeSummaryList.Sum(p => p.Vendor_Dust);
                vendorMouldingOverallSummaryModel.Vendor_Flyout = partsTypeSummaryList.Sum(p => p.Vendor_Flyout);
                vendorMouldingOverallSummaryModel.Vendor_Over_Spray = partsTypeSummaryList.Sum(p => p.Vendor_Over_Spray);
                vendorMouldingOverallSummaryModel.Vendor_Weld_line = partsTypeSummaryList.Sum(p => p.Vendor_Weld_line);
                vendorMouldingOverallSummaryModel.Vendor_Crack = partsTypeSummaryList.Sum(p => p.Vendor_Crack);
                vendorMouldingOverallSummaryModel.Vendor_Gas_mark = partsTypeSummaryList.Sum(p => p.Vendor_Gas_mark);
                vendorMouldingOverallSummaryModel.Vendor_Sink_mark = partsTypeSummaryList.Sum(p => p.Vendor_Sink_mark);
                vendorMouldingOverallSummaryModel.Vendor_Bubble = partsTypeSummaryList.Sum(p => p.Vendor_Bubble);
                vendorMouldingOverallSummaryModel.Vendor_White_dot = partsTypeSummaryList.Sum(p => p.Vendor_White_dot);
                vendorMouldingOverallSummaryModel.Vendor_Black_dot = partsTypeSummaryList.Sum(p => p.Vendor_Black_dot);
                vendorMouldingOverallSummaryModel.Vendor_Red_Dot = partsTypeSummaryList.Sum(p => p.Vendor_Red_Dot);
                vendorMouldingOverallSummaryModel.Vendor_Poor_Gate_Cut = partsTypeSummaryList.Sum(p => p.Vendor_Poor_Gate_Cut);
                vendorMouldingOverallSummaryModel.Vendor_High_Gate = partsTypeSummaryList.Sum(p => p.Vendor_High_Gate);
                vendorMouldingOverallSummaryModel.Vendor_White_Mark = partsTypeSummaryList.Sum(p => p.Vendor_White_Mark);
                vendorMouldingOverallSummaryModel.Vendor_Drag_mark = partsTypeSummaryList.Sum(p => p.Vendor_Drag_mark);
                vendorMouldingOverallSummaryModel.Vendor_Foreigh_Material = partsTypeSummaryList.Sum(p => p.Vendor_Foreigh_Material);
                vendorMouldingOverallSummaryModel.Vendor_Double_Claim = partsTypeSummaryList.Sum(p => p.Vendor_Double_Claim);
                vendorMouldingOverallSummaryModel.Vendor_Short_mould = partsTypeSummaryList.Sum(p => p.Vendor_Short_mould);
                vendorMouldingOverallSummaryModel.Vendor_Flashing = partsTypeSummaryList.Sum(p => p.Vendor_Flashing);
                vendorMouldingOverallSummaryModel.Vendor_Pink_Mark = partsTypeSummaryList.Sum(p => p.Vendor_Pink_Mark);
                vendorMouldingOverallSummaryModel.Vendor_Deform = partsTypeSummaryList.Sum(p => p.Vendor_Deform);
                vendorMouldingOverallSummaryModel.Vendor_Damage = partsTypeSummaryList.Sum(p => p.Vendor_Damage);
                vendorMouldingOverallSummaryModel.Vendor_Mould_Dirt = partsTypeSummaryList.Sum(p => p.Vendor_Mould_Dirt);
                vendorMouldingOverallSummaryModel.Vendor_Yellowish = partsTypeSummaryList.Sum(p => p.Vendor_Yellowish);
                vendorMouldingOverallSummaryModel.Vendor_Oil_Mark = partsTypeSummaryList.Sum(p => p.Vendor_Oil_Mark);
                vendorMouldingOverallSummaryModel.Vendor_Printing_Mark = partsTypeSummaryList.Sum(p => p.Vendor_Printing_Mark);
                vendorMouldingOverallSummaryModel.Vendor_Printing_Uneven = partsTypeSummaryList.Sum(p => p.Vendor_Printing_Uneven);
                vendorMouldingOverallSummaryModel.Vendor_Printing_Color_Dark = partsTypeSummaryList.Sum(p => p.Vendor_Printing_Color_Dark);
                vendorMouldingOverallSummaryModel.Vendor_Wrong_Orietation = partsTypeSummaryList.Sum(p => p.Vendor_Wrong_Orietation);
                vendorMouldingOverallSummaryModel.Vendor_Other = partsTypeSummaryList.Sum(p => p.Vendor_Other);

                vendorMouldingOverallSummaryModel.Vendor_Mould_TotalRej = partsTypeSummaryList.Sum(p => p.Vendor_Mould_TotalRej);
                vendorMouldingOverallSummaryModel.Vendor_Mould_TotalRejRate = Math.Round(partsTypeSummaryList.Sum(p => p.Vendor_Mould_TotalRej) / partsTypeSummaryList.Sum(p => p.vendorLotQty) * 100, 2);

                OverallSummaryInfo.Add(vendorMouldingOverallSummaryModel);
                #endregion

                #region add  laser summary part   paint >
                ViewModel.PQCButtonReport_ViewModel.Report paintOverallSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                paintOverallSummaryModel.partNo = ("PAINTING >");
                paintOverallSummaryModel.rejQty = partsTypeSummaryList.Sum(p => p.Paint_TotalRej);
                paintOverallSummaryModel.rejRate = Math.Round(partsTypeSummaryList.Sum(p => p.Paint_TotalRej) / partsTypeSummaryList.Sum(p => p.lotQty) * 100, 2);
                paintOverallSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", paintOverallSummaryModel.rejQty, paintOverallSummaryModel.rejRate);


                paintOverallSummaryModel.Paint_Particle = partsTypeSummaryList.Sum(p => p.Paint_Particle);
                paintOverallSummaryModel.Paint_Fibre = partsTypeSummaryList.Sum(p => p.Paint_Fibre);
                paintOverallSummaryModel.Paint_Many_particle = partsTypeSummaryList.Sum(p => p.Paint_Many_particle);
                paintOverallSummaryModel.Paint_Stain_mark = partsTypeSummaryList.Sum(p => p.Paint_Stain_mark);
                paintOverallSummaryModel.Paint_Uneven_paint = partsTypeSummaryList.Sum(p => p.Paint_Uneven_paint);
                paintOverallSummaryModel.Paint_Under_coat_uneven_paint = partsTypeSummaryList.Sum(p => p.Paint_Under_coat_uneven_paint);
                paintOverallSummaryModel.Paint_Under_spray = partsTypeSummaryList.Sum(p => p.Paint_Under_spray);
                paintOverallSummaryModel.Paint_White_dot = partsTypeSummaryList.Sum(p => p.Paint_White_dot);
                paintOverallSummaryModel.Paint_Silver_dot = partsTypeSummaryList.Sum(p => p.Paint_Silver_dot);
                paintOverallSummaryModel.Paint_Dust = partsTypeSummaryList.Sum(p => p.Paint_Dust);
                paintOverallSummaryModel.Paint_Paint_crack = partsTypeSummaryList.Sum(p => p.Paint_Paint_crack);
                paintOverallSummaryModel.Paint_Bubble = partsTypeSummaryList.Sum(p => p.Paint_Bubble);
                paintOverallSummaryModel.Paint_Scratch = partsTypeSummaryList.Sum(p => p.Paint_Scratch);
                paintOverallSummaryModel.Paint_Abrasion_Mark = partsTypeSummaryList.Sum(p => p.Paint_Abrasion_Mark);
                paintOverallSummaryModel.Paint_Paint_Dripping = partsTypeSummaryList.Sum(p => p.Paint_Paint_Dripping);
                paintOverallSummaryModel.Paint_Rough_Surface = partsTypeSummaryList.Sum(p => p.Paint_Rough_Surface);
                paintOverallSummaryModel.Paint_Shinning = partsTypeSummaryList.Sum(p => p.Paint_Shinning);
                paintOverallSummaryModel.Paint_Matt = partsTypeSummaryList.Sum(p => p.Paint_Matt);
                paintOverallSummaryModel.Paint_Paint_Pin_Hole = partsTypeSummaryList.Sum(p => p.Paint_Paint_Pin_Hole);
                paintOverallSummaryModel.Paint_Light_Leakage = partsTypeSummaryList.Sum(p => p.Paint_Light_Leakage);
                paintOverallSummaryModel.Paint_White_Mark = partsTypeSummaryList.Sum(p => p.Paint_White_Mark);
                paintOverallSummaryModel.Paint_Dented = partsTypeSummaryList.Sum(p => p.Paint_Dented);
                paintOverallSummaryModel.Paint_Other = partsTypeSummaryList.Sum(p => p.Paint_Other);
                paintOverallSummaryModel.Paint_Particle_for_laser_setup = partsTypeSummaryList.Sum(p => p.Paint_Particle_for_laser_setup);
                paintOverallSummaryModel.Paint_Buyoff = partsTypeSummaryList.Sum(p => p.Paint_Buyoff);
                paintOverallSummaryModel.Paint_Shortage = partsTypeSummaryList.Sum(p => p.Paint_Shortage);

                paintOverallSummaryModel.Paint_TotalRej = partsTypeSummaryList.Sum(p => p.Paint_TotalRej);
                paintOverallSummaryModel.Paint_TotalRejRate = Math.Round(partsTypeSummaryList.Sum(p => p.Paint_TotalRej) / partsTypeSummaryList.Sum(p => p.lotQty) * 100, 2);

                OverallSummaryInfo.Add(paintOverallSummaryModel);
                #endregion

                // add  summary part   paint setup >
                ViewModel.PQCButtonReport_ViewModel.Report paintSetupOverallSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                paintSetupOverallSummaryModel.partNo = "PAINTING SETUP >";
                paintSetupOverallSummaryModel.rejQty = partsTypeSummaryList.Sum(p => p.Paint_SetupRej);
                paintSetupOverallSummaryModel.rejRate = Math.Round(partsTypeSummaryList.Sum(p => p.Paint_SetupRej) / partsTypeSummaryList.Sum(p => p.lotQty) * 100, 2);
                paintSetupOverallSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", paintSetupOverallSummaryModel.rejQty, paintSetupOverallSummaryModel.rejRate);

                OverallSummaryInfo.Add(paintSetupOverallSummaryModel);

                // add summary part   paint qa test >
                ViewModel.PQCButtonReport_ViewModel.Report paintQAOveralSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                paintQAOveralSummaryModel.partNo = "QA PAINT TEST >";
                paintQAOveralSummaryModel.rejQty = partsTypeSummaryList.Sum(p => p.Paint_QATestRej);
                paintQAOveralSummaryModel.rejRate = Math.Round(partsTypeSummaryList.Sum(p => p.Paint_QATestRej) / partsTypeSummaryList.Sum(p => p.lotQty) * 100, 2);
                paintQAOveralSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", paintQAOveralSummaryModel.rejQty, paintQAOveralSummaryModel.rejRate);

                OverallSummaryInfo.Add(paintQAOveralSummaryModel);

                #region add  summary part   laser >
                ViewModel.PQCButtonReport_ViewModel.Report laserOverallSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                laserOverallSummaryModel.partNo = ("LASER >");
                laserOverallSummaryModel.rejQty = partsTypeSummaryList.Sum(p => p.Laser_TotalRej);
                laserOverallSummaryModel.rejRate = Math.Round(partsTypeSummaryList.Sum(p => p.Laser_TotalRej) / partsTypeSummaryList.Sum(p => p.lotQty) * 100, 2);
                laserOverallSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", laserOverallSummaryModel.rejQty, laserOverallSummaryModel.rejRate);

                laserOverallSummaryModel.Laser_Black_Mark = partsTypeSummaryList.Sum(p => p.Laser_Black_Mark);
                laserOverallSummaryModel.Laser_Black_Dot = partsTypeSummaryList.Sum(p => p.Laser_Black_Dot);
                laserOverallSummaryModel.Laser_Graphic_Shift_check_by_PQC = partsTypeSummaryList.Sum(p => p.Laser_Graphic_Shift_check_by_PQC);
                laserOverallSummaryModel.Laser_Graphic_Shift_check_by_MC = partsTypeSummaryList.Sum(p => p.Laser_Graphic_Shift_check_by_MC);
                laserOverallSummaryModel.Laser_Scratch = partsTypeSummaryList.Sum(p => p.Laser_Scratch);
                laserOverallSummaryModel.Laser_Jagged = partsTypeSummaryList.Sum(p => p.Laser_Jagged);
                laserOverallSummaryModel.Laser_Laser_Bubble = partsTypeSummaryList.Sum(p => p.Laser_Laser_Bubble);
                laserOverallSummaryModel.Laser_double_outer_line = partsTypeSummaryList.Sum(p => p.Laser_double_outer_line);
                laserOverallSummaryModel.Laser_Pin_hold = partsTypeSummaryList.Sum(p => p.Laser_Pin_hold);
                laserOverallSummaryModel.Laser_Poor_Laser = partsTypeSummaryList.Sum(p => p.Laser_Poor_Laser);
                laserOverallSummaryModel.Laser_Burm_Mark = partsTypeSummaryList.Sum(p => p.Laser_Burm_Mark);
                laserOverallSummaryModel.Laser_Stain_Mark = partsTypeSummaryList.Sum(p => p.Laser_Stain_Mark);
                laserOverallSummaryModel.Laser_Graphic_Small = partsTypeSummaryList.Sum(p => p.Laser_Graphic_Small);
                laserOverallSummaryModel.Laser_Double_Laser = partsTypeSummaryList.Sum(p => p.Laser_Double_Laser);
                laserOverallSummaryModel.Laser_Color_Yellow = partsTypeSummaryList.Sum(p => p.Laser_Color_Yellow);
                laserOverallSummaryModel.Laser_Crack = partsTypeSummaryList.Sum(p => p.Laser_Crack);
                laserOverallSummaryModel.Laser_Smoke = partsTypeSummaryList.Sum(p => p.Laser_Smoke);
                laserOverallSummaryModel.Laser_Wrong_Orientation = partsTypeSummaryList.Sum(p => p.Laser_Wrong_Orientation);
                laserOverallSummaryModel.Laser_Dented = partsTypeSummaryList.Sum(p => p.Laser_Dented);
                laserOverallSummaryModel.Laser_Other = partsTypeSummaryList.Sum(p => p.Laser_Other);
                laserOverallSummaryModel.Laser_Buyoff = partsTypeSummaryList.Sum(p => p.Laser_Buyoff);
                laserOverallSummaryModel.Laser_Setup = partsTypeSummaryList.Sum(p => p.Laser_Setup);

                laserOverallSummaryModel.Laser_TotalRej = partsTypeSummaryList.Sum(p => p.Laser_TotalRej);
                laserOverallSummaryModel.Laser_TotalRejRate = Math.Round(partsTypeSummaryList.Sum(p => p.Laser_TotalRej) / partsTypeSummaryList.Sum(p => p.lotQty) * 100, 2);

                OverallSummaryInfo.Add(laserOverallSummaryModel);
                #endregion

                // add summary part   overall >
                ViewModel.PQCButtonReport_ViewModel.Report overallOverallSummaryModel = new ViewModel.PQCButtonReport_ViewModel.Report();
                overallOverallSummaryModel.partNo = "OVERALL >";
                overallOverallSummaryModel.lotQty = partsTypeSummaryList.Sum(p => p.lotQty);
                overallOverallSummaryModel.pass = partsTypeSummaryList.Sum(p => p.pass);
                overallOverallSummaryModel.rejQty = partsTypeSummaryList.Sum(p => p.rejQty);
				overallOverallSummaryModel.rejRate = Math.Round(partsTypeSummaryList.Sum(p => p.rejQty) / partsTypeSummaryList.Sum(p => p.lotQty) * 100 ,2);
                overallOverallSummaryModel.rejRateDisplay = string.Format("{0}({1}%)", overallOverallSummaryModel.rejQty, overallOverallSummaryModel.rejRate);

                OverallSummaryInfo.Add(overallOverallSummaryModel);

                #endregion
























                //在添加summary row之后, 再获取根据model汇总的信息.   
                //mark:若在添加model汇总信息后, 再添加summary信息会导致summary数量翻倍.  
                //model汇总信息也是添加在最初的reportlist中.  select出来的summary信息会随着reportlist中内容改变而改变.  
                //linq的特性 select出来的是寻址,而不是再分配内存.
                #region 获取model汇总信息
                var modelSummaryList = from a in reportList
                                       group a by new { a.partsType, a.model } into modelGroup
                                       select new
                                       {
                                           partsType = modelGroup.Key.partsType,
                                           model = modelGroup.Key.model,
                                           

                                           partNo = "SUB TOTAL>>",
                                           lotQty = modelGroup.Sum(p => p.lotQty),
                                           pass = modelGroup.Sum(p => p.pass),
                                           rejQty = modelGroup.Sum(p => p.rejQty),
                                           rejRate = Math.Round(modelGroup.Sum(p => p.rejQty) / modelGroup.Sum(p => p.lotQty) * 100, 2),

                                           rejRateDisplay =  string.Format("{0}({1}%)", modelGroup.Sum(p => p.rejQty), Math.Round(modelGroup.Sum(p => p.rejQty) / modelGroup.Sum(p => p.lotQty) * 100, 2)),

                                           //TTS defect code
                                           TTS_Raw_Part_Scratch = modelGroup.Sum(p => p.TTS_Raw_Part_Scratch),
                                           TTS_Oil_Stain = modelGroup.Sum(p => p.TTS_Oil_Stain),
                                           TTS_Dented = modelGroup.Sum(p => p.TTS_Dented),
                                           TTS_Dust = modelGroup.Sum(p => p.TTS_Dust),
                                           TTS_Flyout = modelGroup.Sum(p => p.TTS_Flyout),
                                           TTS_Over_Spray = modelGroup.Sum(p => p.TTS_Over_Spray),
                                           TTS_Weld_line = modelGroup.Sum(p => p.TTS_Weld_line),
                                           TTS_Crack = modelGroup.Sum(p => p.TTS_Crack),
                                           TTS_Gas_mark = modelGroup.Sum(p => p.TTS_Gas_mark),
                                           TTS_Sink_mark = modelGroup.Sum(p => p.TTS_Sink_mark),
                                           TTS_Bubble = modelGroup.Sum(p => p.TTS_Bubble),
                                           TTS_White_dot = modelGroup.Sum(p => p.TTS_White_dot),
                                           TTS_Black_dot = modelGroup.Sum(p => p.TTS_Black_dot),
                                           TTS_Red_Dot = modelGroup.Sum(p => p.TTS_Red_Dot),
                                           TTS_Poor_Gate_Cut = modelGroup.Sum(p => p.TTS_Poor_Gate_Cut),
                                           TTS_High_Gate = modelGroup.Sum(p => p.TTS_High_Gate),
                                           TTS_White_Mark = modelGroup.Sum(p => p.TTS_White_Mark),
                                           TTS_Drag_mark = modelGroup.Sum(p => p.TTS_Drag_mark),
                                           TTS_Foreigh_Material = modelGroup.Sum(p => p.TTS_Foreigh_Material),
                                           TTS_Double_Claim = modelGroup.Sum(p => p.TTS_Double_Claim),
                                           TTS_Short_mould = modelGroup.Sum(p => p.TTS_Short_mould),
                                           TTS_Flashing = modelGroup.Sum(p => p.TTS_Flashing),
                                           TTS_Pink_Mark = modelGroup.Sum(p => p.TTS_Pink_Mark),
                                           TTS_Deform = modelGroup.Sum(p => p.TTS_Deform),
                                           TTS_Damage = modelGroup.Sum(p => p.TTS_Damage),
                                           TTS_Mould_Dirt = modelGroup.Sum(p => p.TTS_Mould_Dirt),
                                           TTS_Yellowish = modelGroup.Sum(p => p.TTS_Yellowish),
                                           TTS_Oil_Mark = modelGroup.Sum(p => p.TTS_Oil_Mark),
                                           TTS_Printing_Mark = modelGroup.Sum(p => p.TTS_Printing_Mark),
                                           TTS_Printing_Uneven = modelGroup.Sum(p => p.TTS_Printing_Uneven),
                                           TTS_Printing_Color_Dark = modelGroup.Sum(p => p.TTS_Printing_Color_Dark),
                                           TTS_Wrong_Orietation = modelGroup.Sum(p => p.TTS_Wrong_Orietation),
                                           TTS_Other = modelGroup.Sum(p => p.TTS_Other),

                                           //vendor defect code
                                           Vendor_Raw_Part_Scratch = modelGroup.Sum(p => p.Vendor_Raw_Part_Scratch),
                                           Vendor_Oil_Stain = modelGroup.Sum(p => p.Vendor_Oil_Stain),
                                           Vendor_Dented = modelGroup.Sum(p => p.Vendor_Dented),
                                           Vendor_Dust = modelGroup.Sum(p => p.Vendor_Dust),
                                           Vendor_Flyout = modelGroup.Sum(p => p.Vendor_Flyout),
                                           Vendor_Over_Spray = modelGroup.Sum(p => p.Vendor_Over_Spray),
                                           Vendor_Weld_line = modelGroup.Sum(p => p.Vendor_Weld_line),
                                           Vendor_Crack = modelGroup.Sum(p => p.Vendor_Crack),
                                           Vendor_Gas_mark = modelGroup.Sum(p => p.Vendor_Gas_mark),
                                           Vendor_Sink_mark = modelGroup.Sum(p => p.Vendor_Sink_mark),
                                           Vendor_Bubble = modelGroup.Sum(p => p.Vendor_Bubble),
                                           Vendor_White_dot = modelGroup.Sum(p => p.Vendor_White_dot),
                                           Vendor_Black_dot = modelGroup.Sum(p => p.Vendor_Black_dot),
                                           Vendor_Red_Dot = modelGroup.Sum(p => p.Vendor_Red_Dot),
                                           Vendor_Poor_Gate_Cut = modelGroup.Sum(p => p.Vendor_Poor_Gate_Cut),
                                           Vendor_High_Gate = modelGroup.Sum(p => p.Vendor_High_Gate),
                                           Vendor_White_Mark = modelGroup.Sum(p => p.Vendor_White_Mark),
                                           Vendor_Drag_mark = modelGroup.Sum(p => p.Vendor_Drag_mark),
                                           Vendor_Foreigh_Material = modelGroup.Sum(p => p.Vendor_Foreigh_Material),
                                           Vendor_Double_Claim = modelGroup.Sum(p => p.Vendor_Double_Claim),
                                           Vendor_Short_mould = modelGroup.Sum(p => p.Vendor_Short_mould),
                                           Vendor_Flashing = modelGroup.Sum(p => p.Vendor_Flashing),
                                           Vendor_Pink_Mark = modelGroup.Sum(p => p.Vendor_Pink_Mark),
                                           Vendor_Deform = modelGroup.Sum(p => p.Vendor_Deform),
                                           Vendor_Damage = modelGroup.Sum(p => p.Vendor_Damage),
                                           Vendor_Mould_Dirt = modelGroup.Sum(p => p.Vendor_Mould_Dirt),
                                           Vendor_Yellowish = modelGroup.Sum(p => p.Vendor_Yellowish),
                                           Vendor_Oil_Mark = modelGroup.Sum(p => p.Vendor_Oil_Mark),
                                           Vendor_Printing_Mark = modelGroup.Sum(p => p.Vendor_Printing_Mark),
                                           Vendor_Printing_Uneven = modelGroup.Sum(p => p.Vendor_Printing_Uneven),
                                           Vendor_Printing_Color_Dark = modelGroup.Sum(p => p.Vendor_Printing_Color_Dark),
                                           Vendor_Wrong_Orietation = modelGroup.Sum(p => p.Vendor_Wrong_Orietation),
                                           Vendor_Other = modelGroup.Sum(p => p.Vendor_Other),

                                           //paint defect code
                                           Paint_Particle = modelGroup.Sum(p => p.Paint_Particle),
                                           Paint_Fibre = modelGroup.Sum(p => p.Paint_Fibre),
                                           Paint_Many_particle = modelGroup.Sum(p => p.Paint_Many_particle),
                                           Paint_Stain_mark = modelGroup.Sum(p => p.Paint_Stain_mark),
                                           Paint_Uneven_paint = modelGroup.Sum(p => p.Paint_Uneven_paint),
                                           Paint_Under_coat_uneven_paint = modelGroup.Sum(p => p.Paint_Under_coat_uneven_paint),
                                           Paint_Under_spray = modelGroup.Sum(p => p.Paint_Under_spray),
                                           Paint_White_dot = modelGroup.Sum(p => p.Paint_White_dot),
                                           Paint_Silver_dot = modelGroup.Sum(p => p.Paint_Silver_dot),
                                           Paint_Dust = modelGroup.Sum(p => p.Paint_Dust),
                                           Paint_Paint_crack = modelGroup.Sum(p => p.Paint_Paint_crack),
                                           Paint_Bubble = modelGroup.Sum(p => p.Paint_Bubble),
                                           Paint_Scratch = modelGroup.Sum(p => p.Paint_Scratch),
                                           Paint_Abrasion_Mark = modelGroup.Sum(p => p.Paint_Abrasion_Mark),
                                           Paint_Paint_Dripping = modelGroup.Sum(p => p.Paint_Paint_Dripping),
                                           Paint_Rough_Surface = modelGroup.Sum(p => p.Paint_Rough_Surface),
                                           Paint_Shinning = modelGroup.Sum(p => p.Paint_Shinning),
                                           Paint_Matt = modelGroup.Sum(p => p.Paint_Matt),
                                           Paint_Paint_Pin_Hole = modelGroup.Sum(p => p.Paint_Paint_Pin_Hole),
                                           Paint_Light_Leakage = modelGroup.Sum(p => p.Paint_Light_Leakage),
                                           Paint_White_Mark = modelGroup.Sum(p => p.Paint_White_Mark),
                                           Paint_Dented = modelGroup.Sum(p => p.Paint_Dented),
                                           Paint_Other = modelGroup.Sum(p => p.Paint_Other),
                                           Paint_Particle_for_laser_setup = modelGroup.Sum(p => p.Paint_Particle_for_laser_setup),
                                           Paint_Buyoff = modelGroup.Sum(p => p.Paint_Buyoff),
                                           Paint_Shortage = modelGroup.Sum(p => p.Paint_Shortage),

                                           //laser defect code
                                           Laser_Black_Mark = modelGroup.Sum(p => p.Laser_Black_Mark),
                                           Laser_Black_Dot = modelGroup.Sum(p => p.Laser_Black_Dot),
                                           Laser_Graphic_Shift_check_by_PQC = modelGroup.Sum(p => p.Laser_Graphic_Shift_check_by_PQC),
                                           Laser_Graphic_Shift_check_by_MC = modelGroup.Sum(p => p.Laser_Graphic_Shift_check_by_MC),
                                           Laser_Scratch = modelGroup.Sum(p => p.Laser_Scratch),
                                           Laser_Jagged = modelGroup.Sum(p => p.Laser_Jagged),
                                           Laser_Laser_Bubble = modelGroup.Sum(p => p.Laser_Laser_Bubble),
                                           Laser_double_outer_line = modelGroup.Sum(p => p.Laser_double_outer_line),
                                           Laser_Pin_hold = modelGroup.Sum(p => p.Laser_Pin_hold),
                                           Laser_Poor_Laser = modelGroup.Sum(p => p.Laser_Poor_Laser),
                                           Laser_Burm_Mark = modelGroup.Sum(p => p.Laser_Burm_Mark),
                                           Laser_Stain_Mark = modelGroup.Sum(p => p.Laser_Stain_Mark),
                                           Laser_Graphic_Small = modelGroup.Sum(p => p.Laser_Graphic_Small),
                                           Laser_Double_Laser = modelGroup.Sum(p => p.Laser_Double_Laser),
                                           Laser_Color_Yellow = modelGroup.Sum(p => p.Laser_Color_Yellow),
                                           Laser_Crack = modelGroup.Sum(p => p.Laser_Crack),
                                           Laser_Smoke = modelGroup.Sum(p => p.Laser_Smoke),
                                           Laser_Wrong_Orientation = modelGroup.Sum(p => p.Laser_Wrong_Orientation),
                                           Laser_Dented = modelGroup.Sum(p => p.Laser_Dented),
                                           Laser_Other = modelGroup.Sum(p => p.Laser_Other),
                                           Laser_Buyoff = modelGroup.Sum(p => p.Laser_Buyoff),
                                           Laser_Setup = modelGroup.Sum(p => p.Laser_Setup),

                                           //others defect code 
                                           PQC_Scratch = modelGroup.Sum(p => p.PQC_Scratch),
                                           Over_Spray = modelGroup.Sum(p => p.Over_Spray),
                                           Bubble = modelGroup.Sum(p => p.Bubble),
                                           Oil_Stain = modelGroup.Sum(p => p.Oil_Stain),
                                           Drag_Mark = modelGroup.Sum(p => p.Drag_Mark),
                                           Light_Leakage = modelGroup.Sum(p => p.Light_Leakage),
                                           Light_Bubble = modelGroup.Sum(p => p.Light_Bubble),
                                           White_Dot_in_Material = modelGroup.Sum(p => p.White_Dot_in_Material),
                                           Other = modelGroup.Sum(p => p.Other),



                                           TTS_Mould_TotalRej = modelGroup.Sum(p => p.TTS_Mould_TotalRej),
                                           TTS_Mould_TotalRejRate = Math.Round(modelGroup.Sum(p => p.TTS_Mould_TotalRej) / modelGroup.Sum(p => p.lotQty) * 100, 2),

                                           Vendor_Mould_TotalRej = modelGroup.Sum(p => p.Vendor_Mould_TotalRej),
                                           Vendor_Mould_TotalRejRate = Math.Round(modelGroup.Sum(p => p.Vendor_Mould_TotalRej) / modelGroup.Sum(p => p.lotQty) * 100, 2),

                                           Paint_TotalRej = modelGroup.Sum(p => p.Paint_TotalRej),
                                           Paint_TotalRejRate = Math.Round(modelGroup.Sum(p => p.Paint_TotalRej) / modelGroup.Sum(p => p.lotQty) * 100, 2),

                                           Laser_TotalRej = modelGroup.Sum(p => p.Laser_TotalRej),
                                           Laser_TotalRejRate = Math.Round(modelGroup.Sum(p => p.Laser_TotalRej) / modelGroup.Sum(p => p.lotQty) * 100, 2),

                                           Others_TotalRej = modelGroup.Sum(p => p.Others_TotalRej),
                                           Others_TotalRejRate = Math.Round(modelGroup.Sum(p => p.Others_TotalRej) / modelGroup.Sum(p => p.lotQty) * 100, 2),

                                           Paint_SetupRej = modelGroup.Sum(p => p.Paint_SetupRej),
                                           Paint_SetupRejRate = Math.Round(modelGroup.Sum(p => p.Paint_SetupRej) / modelGroup.Sum(p => p.lotQty) * 100, 2),
                                           Paint_QATestRej = modelGroup.Sum(p => p.Paint_QATestRej),
                                           Paint_QATestRejRate = Math.Round(modelGroup.Sum(p => p.Paint_QATestRej) / modelGroup.Sum(p => p.lotQty) * 100, 2)
                                       };

                #endregion

                #region 合并model汇总信息到 report list中
                foreach (var modelSummary in modelSummaryList)
                {
                    ViewModel.PQCButtonReport_ViewModel.Report reportModel = new ViewModel.PQCButtonReport_ViewModel.Report();

                    reportModel.partsType = modelSummary.partsType;
                    reportModel.model = modelSummary.model;
                    reportModel.jobID = "<b>"+ modelSummary.model + "</b>" ;
                    reportModel.partNo = modelSummary.partNo;
                    reportModel.lotQty = modelSummary.lotQty;
                    reportModel.pass = modelSummary.pass;
                    reportModel.rejQty = modelSummary.rejQty;
                    reportModel.rejRate = modelSummary.rejRate;
                    reportModel.rejRateDisplay = modelSummary.rejRateDisplay;

                    //tts
                    reportModel.TTS_Raw_Part_Scratch = modelSummary.TTS_Raw_Part_Scratch;
                    reportModel.TTS_Oil_Stain = modelSummary.TTS_Oil_Stain;
                    reportModel.TTS_Dented = modelSummary.TTS_Dented;
                    reportModel.TTS_Dust = modelSummary.TTS_Dust;
                    reportModel.TTS_Flyout = modelSummary.TTS_Flyout;
                    reportModel.TTS_Over_Spray = modelSummary.TTS_Over_Spray;
                    reportModel.TTS_Weld_line = modelSummary.TTS_Weld_line;
                    reportModel.TTS_Crack = modelSummary.TTS_Crack;
                    reportModel.TTS_Gas_mark = modelSummary.TTS_Gas_mark;
                    reportModel.TTS_Sink_mark = modelSummary.TTS_Sink_mark;
                    reportModel.TTS_Bubble = modelSummary.TTS_Bubble;
                    reportModel.TTS_White_dot = modelSummary.TTS_White_dot;
                    reportModel.TTS_Black_dot = modelSummary.TTS_Black_dot;
                    reportModel.TTS_Red_Dot = modelSummary.TTS_Red_Dot;
                    reportModel.TTS_Poor_Gate_Cut = modelSummary.TTS_Poor_Gate_Cut;
                    reportModel.TTS_High_Gate = modelSummary.TTS_High_Gate;
                    reportModel.TTS_White_Mark = modelSummary.TTS_White_Mark;
                    reportModel.TTS_Drag_mark = modelSummary.TTS_Drag_mark;
                    reportModel.TTS_Foreigh_Material = modelSummary.TTS_Foreigh_Material;
                    reportModel.TTS_Double_Claim = modelSummary.TTS_Double_Claim;
                    reportModel.TTS_Short_mould = modelSummary.TTS_Short_mould;
                    reportModel.TTS_Flashing = modelSummary.TTS_Flashing;
                    reportModel.TTS_Pink_Mark = modelSummary.TTS_Pink_Mark;
                    reportModel.TTS_Deform = modelSummary.TTS_Deform;
                    reportModel.TTS_Damage = modelSummary.TTS_Damage;
                    reportModel.TTS_Mould_Dirt = modelSummary.TTS_Mould_Dirt;
                    reportModel.TTS_Yellowish = modelSummary.TTS_Yellowish;
                    reportModel.TTS_Oil_Mark = modelSummary.TTS_Oil_Mark;
                    reportModel.TTS_Printing_Mark = modelSummary.TTS_Printing_Mark;
                    reportModel.TTS_Printing_Uneven = modelSummary.TTS_Printing_Uneven;
                    reportModel.TTS_Printing_Color_Dark = modelSummary.TTS_Printing_Color_Dark;
                    reportModel.TTS_Wrong_Orietation = modelSummary.TTS_Wrong_Orietation;
                    reportModel.TTS_Other = modelSummary.TTS_Other;

                    //vendor
                    reportModel.Vendor_Raw_Part_Scratch = modelSummary.Vendor_Raw_Part_Scratch;
                    reportModel.Vendor_Oil_Stain = modelSummary.Vendor_Oil_Stain;
                    reportModel.Vendor_Dented = modelSummary.Vendor_Dented;
                    reportModel.Vendor_Dust = modelSummary.Vendor_Dust;
                    reportModel.Vendor_Flyout = modelSummary.Vendor_Flyout;
                    reportModel.Vendor_Over_Spray = modelSummary.Vendor_Over_Spray;
                    reportModel.Vendor_Weld_line = modelSummary.Vendor_Weld_line;
                    reportModel.Vendor_Crack = modelSummary.Vendor_Crack;
                    reportModel.Vendor_Gas_mark = modelSummary.Vendor_Gas_mark;
                    reportModel.Vendor_Sink_mark = modelSummary.Vendor_Sink_mark;
                    reportModel.Vendor_Bubble = modelSummary.Vendor_Bubble;
                    reportModel.Vendor_White_dot = modelSummary.Vendor_White_dot;
                    reportModel.Vendor_Black_dot = modelSummary.Vendor_Black_dot;
                    reportModel.Vendor_Red_Dot = modelSummary.Vendor_Red_Dot;
                    reportModel.Vendor_Poor_Gate_Cut = modelSummary.Vendor_Poor_Gate_Cut;
                    reportModel.Vendor_High_Gate = modelSummary.Vendor_High_Gate;
                    reportModel.Vendor_White_Mark = modelSummary.Vendor_White_Mark;
                    reportModel.Vendor_Drag_mark = modelSummary.Vendor_Drag_mark;
                    reportModel.Vendor_Foreigh_Material = modelSummary.Vendor_Foreigh_Material;
                    reportModel.Vendor_Double_Claim = modelSummary.Vendor_Double_Claim;
                    reportModel.Vendor_Short_mould = modelSummary.Vendor_Short_mould;
                    reportModel.Vendor_Flashing = modelSummary.Vendor_Flashing;
                    reportModel.Vendor_Pink_Mark = modelSummary.Vendor_Pink_Mark;
                    reportModel.Vendor_Deform = modelSummary.Vendor_Deform;
                    reportModel.Vendor_Damage = modelSummary.Vendor_Damage;
                    reportModel.Vendor_Mould_Dirt = modelSummary.Vendor_Mould_Dirt;
                    reportModel.Vendor_Yellowish = modelSummary.Vendor_Yellowish;
                    reportModel.Vendor_Oil_Mark = modelSummary.Vendor_Oil_Mark;
                    reportModel.Vendor_Printing_Mark = modelSummary.Vendor_Printing_Mark;
                    reportModel.Vendor_Printing_Uneven = modelSummary.Vendor_Printing_Uneven;
                    reportModel.Vendor_Printing_Color_Dark = modelSummary.Vendor_Printing_Color_Dark;
                    reportModel.Vendor_Wrong_Orietation = modelSummary.Vendor_Wrong_Orietation;
                    reportModel.Vendor_Other = modelSummary.Vendor_Other;

                    //paint
                    reportModel.Paint_Particle = modelSummary.Paint_Particle;
                    reportModel.Paint_Fibre = modelSummary.Paint_Fibre;
                    reportModel.Paint_Many_particle = modelSummary.Paint_Many_particle;
                    reportModel.Paint_Stain_mark = modelSummary.Paint_Stain_mark;
                    reportModel.Paint_Uneven_paint = modelSummary.Paint_Uneven_paint;
                    reportModel.Paint_Under_coat_uneven_paint = modelSummary.Paint_Under_coat_uneven_paint;
                    reportModel.Paint_Under_spray = modelSummary.Paint_Under_spray;
                    reportModel.Paint_White_dot = modelSummary.Paint_White_dot;
                    reportModel.Paint_Silver_dot = modelSummary.Paint_Silver_dot;
                    reportModel.Paint_Dust = modelSummary.Paint_Dust;
                    reportModel.Paint_Paint_crack = modelSummary.Paint_Paint_crack;
                    reportModel.Paint_Bubble = modelSummary.Paint_Bubble;
                    reportModel.Paint_Scratch = modelSummary.Paint_Scratch;
                    reportModel.Paint_Abrasion_Mark = modelSummary.Paint_Abrasion_Mark;
                    reportModel.Paint_Paint_Dripping = modelSummary.Paint_Paint_Dripping;
                    reportModel.Paint_Rough_Surface = modelSummary.Paint_Rough_Surface;
                    reportModel.Paint_Shinning = modelSummary.Paint_Shinning;
                    reportModel.Paint_Matt = modelSummary.Paint_Matt;
                    reportModel.Paint_Paint_Pin_Hole = modelSummary.Paint_Paint_Pin_Hole;
                    reportModel.Paint_Light_Leakage = modelSummary.Paint_Light_Leakage;
                    reportModel.Paint_White_Mark = modelSummary.Paint_White_Mark;
                    reportModel.Paint_Dented = modelSummary.Paint_Dented;
                    reportModel.Paint_Other = modelSummary.Paint_Other;
                    reportModel.Paint_Particle_for_laser_setup = modelSummary.Paint_Particle_for_laser_setup;
                    reportModel.Paint_Buyoff = modelSummary.Paint_Buyoff;
                    reportModel.Paint_Shortage = modelSummary.Paint_Shortage;

                    //laser
                    reportModel.Laser_Black_Mark = modelSummary.Laser_Black_Mark;
                    reportModel.Laser_Black_Dot = modelSummary.Laser_Black_Dot;
                    reportModel.Laser_Graphic_Shift_check_by_PQC = modelSummary.Laser_Graphic_Shift_check_by_PQC;
                    reportModel.Laser_Graphic_Shift_check_by_MC = modelSummary.Laser_Graphic_Shift_check_by_MC;
                    reportModel.Laser_Scratch = modelSummary.Laser_Scratch;
                    reportModel.Laser_Jagged = modelSummary.Laser_Jagged;
                    reportModel.Laser_Laser_Bubble = modelSummary.Laser_Laser_Bubble;
                    reportModel.Laser_double_outer_line = modelSummary.Laser_double_outer_line;
                    reportModel.Laser_Pin_hold = modelSummary.Laser_Pin_hold;
                    reportModel.Laser_Poor_Laser = modelSummary.Laser_Poor_Laser;
                    reportModel.Laser_Burm_Mark = modelSummary.Laser_Burm_Mark;
                    reportModel.Laser_Stain_Mark = modelSummary.Laser_Stain_Mark;
                    reportModel.Laser_Graphic_Small = modelSummary.Laser_Graphic_Small;
                    reportModel.Laser_Double_Laser = modelSummary.Laser_Double_Laser;
                    reportModel.Laser_Color_Yellow = modelSummary.Laser_Color_Yellow;
                    reportModel.Laser_Crack = modelSummary.Laser_Crack;
                    reportModel.Laser_Smoke = modelSummary.Laser_Smoke;
                    reportModel.Laser_Wrong_Orientation = modelSummary.Laser_Wrong_Orientation;
                    reportModel.Laser_Dented = modelSummary.Laser_Dented;
                    reportModel.Laser_Other = modelSummary.Laser_Other;
                    reportModel.Laser_Buyoff = modelSummary.Laser_Buyoff;
                    reportModel.Laser_Setup = modelSummary.Laser_Setup;

                    //others
                    reportModel.PQC_Scratch = modelSummary.PQC_Scratch;
                    reportModel.Over_Spray = modelSummary.Over_Spray;
                    reportModel.Bubble = modelSummary.Bubble;
                    reportModel.Oil_Stain = modelSummary.Oil_Stain;
                    reportModel.Drag_Mark = modelSummary.Drag_Mark;
                    reportModel.Light_Leakage = modelSummary.Light_Leakage;
                    reportModel.Light_Bubble = modelSummary.Light_Bubble;
                    reportModel.White_Dot_in_Material = modelSummary.White_Dot_in_Material;
                    reportModel.Other = modelSummary.Other;

                    //totalrej rejRate
                    reportModel.TTS_Mould_TotalRej = modelSummary.TTS_Mould_TotalRej;
                    reportModel.TTS_Mould_TotalRejRate = modelSummary.TTS_Mould_TotalRejRate;
                    reportModel.Vendor_Mould_TotalRej = modelSummary.Vendor_Mould_TotalRej;
                    reportModel.Vendor_Mould_TotalRejRate = modelSummary.Vendor_Mould_TotalRejRate;
                    reportModel.Paint_TotalRej = modelSummary.Paint_TotalRej;
                    reportModel.Paint_TotalRejRate = modelSummary.Paint_TotalRejRate;
                    reportModel.Laser_TotalRej = modelSummary.Laser_TotalRej;
                    reportModel.Laser_TotalRejRate = modelSummary.Laser_TotalRejRate;
                    reportModel.Others_TotalRej = modelSummary.Others_TotalRej;
                    reportModel.Others_TotalRejRate = modelSummary.Others_TotalRejRate;

                    //paint qa, setup qty & rate
                    reportModel.Paint_QATestRej = modelSummary.Paint_QATestRej;
                    reportModel.Paint_QATestRejRate = modelSummary.Paint_QATestRejRate;
                    reportModel.Paint_SetupRej = modelSummary.Paint_SetupRej;
                    reportModel.Paint_SetupRejRate = modelSummary.Paint_SetupRejRate;

                    reportList.Add(reportModel);
                }
                #endregion
























                #region 拼datatable
                DataTable dtWIP = Common.CommFunctions.ListToDt<ViewModel.PQCButtonReport_ViewModel.Report>(from a in reportList where a.partsType == "WIP" orderby a.model ascending select a);
                DataTable dtLaser = Common.CommFunctions.ListToDt<ViewModel.PQCButtonReport_ViewModel.Report>(from a in reportList where a.partsType == "Laser" orderby a.model ascending select a);

                DataTable dtWIPSummary = Common.CommFunctions.ListToDt<ViewModel.PQCButtonReport_ViewModel.Report>(WIPPartSummaryInfo);
                DataTable dtLaserSummary = Common.CommFunctions.ListToDt<ViewModel.PQCButtonReport_ViewModel.Report>(laserPartSummaryInfo);
                DataTable dtOverallSummary = Common.CommFunctions.ListToDt<ViewModel.PQCButtonReport_ViewModel.Report>(OverallSummaryInfo);




                DataTable dtReport = dtWIP.Clone();//赋值结构


                if (reportType == "WIP")
                {
                    DataRow drWIPTitle = dtReport.NewRow();
                    drWIPTitle["model"] = "PART 1: PAINTING ONLY PARTS";
                    drWIPTitle["jobID"] = "PART 1: PAINTING ONLY PARTS";

                    dtReport.Rows.Add(drWIPTitle);//添加wip标题
                    dtReport.Merge(dtWIP.Copy());//合并 wip
                    dtReport.Rows.Add(dtReport.NewRow());//加个空行隔开
                    dtReport.Merge(dtWIPSummary.Copy());//合并 wip summary 信息
                    dtReport.Rows.Add(dtReport.NewRow());//加个空行隔开
                }
                else if (reportType == "Laser")
                {
                    DataRow drLaserTitle = dtReport.NewRow();
                    drLaserTitle["model"] = "PART 2: LASER PARTS";
                    drLaserTitle["jobID"] = "PART 2: LASER PARTS";
                    dtReport.Rows.Add(drLaserTitle);//添加laser标题
                    dtReport.Merge(dtLaser.Copy());//合并 laser
                    dtReport.Rows.Add(dtReport.NewRow());//加个空行隔开
                    dtReport.Merge(dtLaserSummary.Copy());//合并 laser summary 信息
                    dtReport.Rows.Add(dtReport.NewRow());//加个空行隔开
                }
                else
                {
                    DataRow drWIPTitle = dtReport.NewRow();
                    drWIPTitle["model"] = "PART 1: PAINTING ONLY PARTS";
                    drWIPTitle["jobID"] = "PART 1: PAINTING ONLY PARTS";
                    dtReport.Rows.Add(drWIPTitle);//添加wip标题
                    dtReport.Merge(dtWIP.Copy());//合并 wip
                    dtReport.Rows.Add(dtReport.NewRow());//加个空行隔开
                    dtReport.Merge(dtWIPSummary.Copy());//合并 wip summary 信息
                    dtReport.Rows.Add(dtReport.NewRow());//加个空行隔开

                    DataRow drLaserTitle = dtReport.NewRow();
                    drLaserTitle["model"] = "PART 2: LASER PARTS";
                    drLaserTitle["jobID"] = "PART 2: LASER PARTS";
                    dtReport.Rows.Add(drLaserTitle);//添加laser标题
                    dtReport.Merge(dtLaser.Copy());//合并 laser
                    dtReport.Rows.Add(dtReport.NewRow());//加个空行隔开
                    dtReport.Merge(dtLaserSummary.Copy());//合并 laser summary 信息
                    dtReport.Rows.Add(dtReport.NewRow());//加个空行隔开

                    DataRow drSummaryTitle = dtReport.NewRow();
                    drSummaryTitle["model"] = "SUMMARY:";
                    drSummaryTitle["jobID"] = "SUMMARY:";
                    dtReport.Rows.Add(drSummaryTitle);//添加summary标题
                    dtReport.Merge(dtOverallSummary.Copy());//合并 summary 信息
                }

                #endregion























                #region  get model for display,   =0 hide,  >0 display

                ViewModel.PQCButtonReport_ViewModel.Report modelForDisplay = new ViewModel.PQCButtonReport_ViewModel.Report();

                modelForDisplay.TTS_Raw_Part_Scratch = partsTypeSummaryList.Sum(p => p.TTS_Raw_Part_Scratch);
                modelForDisplay.TTS_Oil_Stain = partsTypeSummaryList.Sum(p => p.TTS_Oil_Stain);
                modelForDisplay.TTS_Dented = partsTypeSummaryList.Sum(p => p.TTS_Dented);
                modelForDisplay.TTS_Dust = partsTypeSummaryList.Sum(p => p.TTS_Dust);
                modelForDisplay.TTS_Flyout = partsTypeSummaryList.Sum(p => p.TTS_Flyout);
                modelForDisplay.TTS_Over_Spray = partsTypeSummaryList.Sum(p => p.TTS_Over_Spray);
                modelForDisplay.TTS_Weld_line = partsTypeSummaryList.Sum(p => p.TTS_Weld_line);
                modelForDisplay.TTS_Crack = partsTypeSummaryList.Sum(p => p.TTS_Crack);
                modelForDisplay.TTS_Gas_mark = partsTypeSummaryList.Sum(p => p.TTS_Gas_mark);
                modelForDisplay.TTS_Sink_mark = partsTypeSummaryList.Sum(p => p.TTS_Sink_mark);
                modelForDisplay.TTS_Bubble = partsTypeSummaryList.Sum(p => p.TTS_Bubble);
                modelForDisplay.TTS_White_dot = partsTypeSummaryList.Sum(p => p.TTS_White_dot);
                modelForDisplay.TTS_Black_dot = partsTypeSummaryList.Sum(p => p.TTS_Black_dot);
                modelForDisplay.TTS_Red_Dot = partsTypeSummaryList.Sum(p => p.TTS_Red_Dot);
                modelForDisplay.TTS_Poor_Gate_Cut = partsTypeSummaryList.Sum(p => p.TTS_Poor_Gate_Cut);
                modelForDisplay.TTS_High_Gate = partsTypeSummaryList.Sum(p => p.TTS_High_Gate);
                modelForDisplay.TTS_White_Mark = partsTypeSummaryList.Sum(p => p.TTS_White_Mark);
                modelForDisplay.TTS_Drag_mark = partsTypeSummaryList.Sum(p => p.TTS_Drag_mark);
                modelForDisplay.TTS_Foreigh_Material = partsTypeSummaryList.Sum(p => p.TTS_Foreigh_Material);
                modelForDisplay.TTS_Double_Claim = partsTypeSummaryList.Sum(p => p.TTS_Double_Claim);
                modelForDisplay.TTS_Short_mould = partsTypeSummaryList.Sum(p => p.TTS_Short_mould);
                modelForDisplay.TTS_Flashing = partsTypeSummaryList.Sum(p => p.TTS_Flashing);
                modelForDisplay.TTS_Pink_Mark = partsTypeSummaryList.Sum(p => p.TTS_Pink_Mark);
                modelForDisplay.TTS_Deform = partsTypeSummaryList.Sum(p => p.TTS_Deform);
                modelForDisplay.TTS_Damage = partsTypeSummaryList.Sum(p => p.TTS_Damage);
                modelForDisplay.TTS_Mould_Dirt = partsTypeSummaryList.Sum(p => p.TTS_Mould_Dirt);
                modelForDisplay.TTS_Yellowish = partsTypeSummaryList.Sum(p => p.TTS_Yellowish);
                modelForDisplay.TTS_Oil_Mark = partsTypeSummaryList.Sum(p => p.TTS_Oil_Mark);
                modelForDisplay.TTS_Printing_Mark = partsTypeSummaryList.Sum(p => p.TTS_Printing_Mark);
                modelForDisplay.TTS_Printing_Uneven = partsTypeSummaryList.Sum(p => p.TTS_Printing_Uneven);
                modelForDisplay.TTS_Printing_Color_Dark = partsTypeSummaryList.Sum(p => p.TTS_Printing_Color_Dark);
                modelForDisplay.TTS_Wrong_Orietation = partsTypeSummaryList.Sum(p => p.TTS_Wrong_Orietation);
                modelForDisplay.TTS_Other = partsTypeSummaryList.Sum(p => p.TTS_Other);


                modelForDisplay.Vendor_Raw_Part_Scratch = partsTypeSummaryList.Sum(p => p.Vendor_Raw_Part_Scratch);
                modelForDisplay.Vendor_Oil_Stain = partsTypeSummaryList.Sum(p => p.Vendor_Oil_Stain);
                modelForDisplay.Vendor_Dented = partsTypeSummaryList.Sum(p => p.Vendor_Dented);
                modelForDisplay.Vendor_Dust = partsTypeSummaryList.Sum(p => p.Vendor_Dust);
                modelForDisplay.Vendor_Flyout = partsTypeSummaryList.Sum(p => p.Vendor_Flyout);
                modelForDisplay.Vendor_Over_Spray = partsTypeSummaryList.Sum(p => p.Vendor_Over_Spray);
                modelForDisplay.Vendor_Weld_line = partsTypeSummaryList.Sum(p => p.Vendor_Weld_line);
                modelForDisplay.Vendor_Crack = partsTypeSummaryList.Sum(p => p.Vendor_Crack);
                modelForDisplay.Vendor_Gas_mark = partsTypeSummaryList.Sum(p => p.Vendor_Gas_mark);
                modelForDisplay.Vendor_Sink_mark = partsTypeSummaryList.Sum(p => p.Vendor_Sink_mark);
                modelForDisplay.Vendor_Bubble = partsTypeSummaryList.Sum(p => p.Vendor_Bubble);
                modelForDisplay.Vendor_White_dot = partsTypeSummaryList.Sum(p => p.Vendor_White_dot);
                modelForDisplay.Vendor_Black_dot = partsTypeSummaryList.Sum(p => p.Vendor_Black_dot);
                modelForDisplay.Vendor_Red_Dot = partsTypeSummaryList.Sum(p => p.Vendor_Red_Dot);
                modelForDisplay.Vendor_Poor_Gate_Cut = partsTypeSummaryList.Sum(p => p.Vendor_Poor_Gate_Cut);
                modelForDisplay.Vendor_High_Gate = partsTypeSummaryList.Sum(p => p.Vendor_High_Gate);
                modelForDisplay.Vendor_White_Mark = partsTypeSummaryList.Sum(p => p.Vendor_White_Mark);
                modelForDisplay.Vendor_Drag_mark = partsTypeSummaryList.Sum(p => p.Vendor_Drag_mark);
                modelForDisplay.Vendor_Foreigh_Material = partsTypeSummaryList.Sum(p => p.Vendor_Foreigh_Material);
                modelForDisplay.Vendor_Double_Claim = partsTypeSummaryList.Sum(p => p.Vendor_Double_Claim);
                modelForDisplay.Vendor_Short_mould = partsTypeSummaryList.Sum(p => p.Vendor_Short_mould);
                modelForDisplay.Vendor_Flashing = partsTypeSummaryList.Sum(p => p.Vendor_Flashing);
                modelForDisplay.Vendor_Pink_Mark = partsTypeSummaryList.Sum(p => p.Vendor_Pink_Mark);
                modelForDisplay.Vendor_Deform = partsTypeSummaryList.Sum(p => p.Vendor_Deform);
                modelForDisplay.Vendor_Damage = partsTypeSummaryList.Sum(p => p.Vendor_Damage);
                modelForDisplay.Vendor_Mould_Dirt = partsTypeSummaryList.Sum(p => p.Vendor_Mould_Dirt);
                modelForDisplay.Vendor_Yellowish = partsTypeSummaryList.Sum(p => p.Vendor_Yellowish);
                modelForDisplay.Vendor_Oil_Mark = partsTypeSummaryList.Sum(p => p.Vendor_Oil_Mark);
                modelForDisplay.Vendor_Printing_Mark = partsTypeSummaryList.Sum(p => p.Vendor_Printing_Mark);
                modelForDisplay.Vendor_Printing_Uneven = partsTypeSummaryList.Sum(p => p.Vendor_Printing_Uneven);
                modelForDisplay.Vendor_Printing_Color_Dark = partsTypeSummaryList.Sum(p => p.Vendor_Printing_Color_Dark);
                modelForDisplay.Vendor_Wrong_Orietation = partsTypeSummaryList.Sum(p => p.Vendor_Wrong_Orietation);
                modelForDisplay.Vendor_Other = partsTypeSummaryList.Sum(p => p.Vendor_Other);


                modelForDisplay.Paint_Particle = partsTypeSummaryList.Sum(p => p.Paint_Particle);
                modelForDisplay.Paint_Fibre = partsTypeSummaryList.Sum(p => p.Paint_Fibre);
                modelForDisplay.Paint_Many_particle = partsTypeSummaryList.Sum(p => p.Paint_Many_particle);
                modelForDisplay.Paint_Stain_mark = partsTypeSummaryList.Sum(p => p.Paint_Stain_mark);
                modelForDisplay.Paint_Uneven_paint = partsTypeSummaryList.Sum(p => p.Paint_Uneven_paint);
                modelForDisplay.Paint_Under_coat_uneven_paint = partsTypeSummaryList.Sum(p => p.Paint_Under_coat_uneven_paint);
                modelForDisplay.Paint_Under_spray = partsTypeSummaryList.Sum(p => p.Paint_Under_spray);
                modelForDisplay.Paint_White_dot = partsTypeSummaryList.Sum(p => p.Paint_White_dot);
                modelForDisplay.Paint_Silver_dot = partsTypeSummaryList.Sum(p => p.Paint_Silver_dot);
                modelForDisplay.Paint_Dust = partsTypeSummaryList.Sum(p => p.Paint_Dust);
                modelForDisplay.Paint_Paint_crack = partsTypeSummaryList.Sum(p => p.Paint_Paint_crack);
                modelForDisplay.Paint_Bubble = partsTypeSummaryList.Sum(p => p.Paint_Bubble);
                modelForDisplay.Paint_Scratch = partsTypeSummaryList.Sum(p => p.Paint_Scratch);
                modelForDisplay.Paint_Abrasion_Mark = partsTypeSummaryList.Sum(p => p.Paint_Abrasion_Mark);
                modelForDisplay.Paint_Paint_Dripping = partsTypeSummaryList.Sum(p => p.Paint_Paint_Dripping);
                modelForDisplay.Paint_Rough_Surface = partsTypeSummaryList.Sum(p => p.Paint_Rough_Surface);
                modelForDisplay.Paint_Shinning = partsTypeSummaryList.Sum(p => p.Paint_Shinning);
                modelForDisplay.Paint_Matt = partsTypeSummaryList.Sum(p => p.Paint_Matt);
                modelForDisplay.Paint_Paint_Pin_Hole = partsTypeSummaryList.Sum(p => p.Paint_Paint_Pin_Hole);
                modelForDisplay.Paint_Light_Leakage = partsTypeSummaryList.Sum(p => p.Paint_Light_Leakage);
                modelForDisplay.Paint_White_Mark = partsTypeSummaryList.Sum(p => p.Paint_White_Mark);
                modelForDisplay.Paint_Dented = partsTypeSummaryList.Sum(p => p.Paint_Dented);
                modelForDisplay.Paint_Other = partsTypeSummaryList.Sum(p => p.Paint_Other);
                modelForDisplay.Paint_Particle_for_laser_setup = partsTypeSummaryList.Sum(p => p.Paint_Particle_for_laser_setup);
                modelForDisplay.Paint_Buyoff = partsTypeSummaryList.Sum(p => p.Paint_Buyoff);
                modelForDisplay.Paint_Shortage = partsTypeSummaryList.Sum(p => p.Paint_Shortage);


                modelForDisplay.Laser_Black_Mark = partsTypeSummaryList.Sum(p => p.Laser_Black_Mark);
                modelForDisplay.Laser_Black_Dot = partsTypeSummaryList.Sum(p => p.Laser_Black_Dot);
                modelForDisplay.Laser_Graphic_Shift_check_by_PQC = partsTypeSummaryList.Sum(p => p.Laser_Graphic_Shift_check_by_PQC);
                modelForDisplay.Laser_Graphic_Shift_check_by_MC = partsTypeSummaryList.Sum(p => p.Laser_Graphic_Shift_check_by_MC);
                modelForDisplay.Laser_Scratch = partsTypeSummaryList.Sum(p => p.Laser_Scratch);
                modelForDisplay.Laser_Jagged = partsTypeSummaryList.Sum(p => p.Laser_Jagged);
                modelForDisplay.Laser_Laser_Bubble = partsTypeSummaryList.Sum(p => p.Laser_Laser_Bubble);
                modelForDisplay.Laser_double_outer_line = partsTypeSummaryList.Sum(p => p.Laser_double_outer_line);
                modelForDisplay.Laser_Pin_hold = partsTypeSummaryList.Sum(p => p.Laser_Pin_hold);
                modelForDisplay.Laser_Poor_Laser = partsTypeSummaryList.Sum(p => p.Laser_Poor_Laser);
                modelForDisplay.Laser_Burm_Mark = partsTypeSummaryList.Sum(p => p.Laser_Burm_Mark);
                modelForDisplay.Laser_Stain_Mark = partsTypeSummaryList.Sum(p => p.Laser_Stain_Mark);
                modelForDisplay.Laser_Graphic_Small = partsTypeSummaryList.Sum(p => p.Laser_Graphic_Small);
                modelForDisplay.Laser_Double_Laser = partsTypeSummaryList.Sum(p => p.Laser_Double_Laser);
                modelForDisplay.Laser_Color_Yellow = partsTypeSummaryList.Sum(p => p.Laser_Color_Yellow);
                modelForDisplay.Laser_Crack = partsTypeSummaryList.Sum(p => p.Laser_Crack);
                modelForDisplay.Laser_Smoke = partsTypeSummaryList.Sum(p => p.Laser_Smoke);
                modelForDisplay.Laser_Wrong_Orientation = partsTypeSummaryList.Sum(p => p.Laser_Wrong_Orientation);
                modelForDisplay.Laser_Dented = partsTypeSummaryList.Sum(p => p.Laser_Dented);
                modelForDisplay.Laser_Other = partsTypeSummaryList.Sum(p => p.Laser_Other);
                modelForDisplay.Laser_Buyoff = partsTypeSummaryList.Sum(p => p.Laser_Buyoff);
                modelForDisplay.Laser_Setup = partsTypeSummaryList.Sum(p => p.Laser_Setup);


                modelForDisplay.PQC_Scratch = partsTypeSummaryList.Sum(p => p.PQC_Scratch);
                modelForDisplay.Over_Spray = partsTypeSummaryList.Sum(p => p.Over_Spray);
                modelForDisplay.Bubble = partsTypeSummaryList.Sum(p => p.Bubble);
                modelForDisplay.Oil_Stain = partsTypeSummaryList.Sum(p => p.Oil_Stain);
                modelForDisplay.Drag_Mark = partsTypeSummaryList.Sum(p => p.Drag_Mark);
                modelForDisplay.Light_Leakage = partsTypeSummaryList.Sum(p => p.Light_Leakage);
                modelForDisplay.Light_Bubble = partsTypeSummaryList.Sum(p => p.Light_Bubble);
                modelForDisplay.White_Dot_in_Material = partsTypeSummaryList.Sum(p => p.White_Dot_in_Material);
                modelForDisplay.Other = partsTypeSummaryList.Sum(p => p.Other);
                
                #endregion

                Display(dtReport, modelForDisplay);

            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("PQCButtonReport_New", "BtnGenerate_Click   error : " + ex.ToString());
                Common.CommFunctions.ShowMessage(this.Page, "Warning!  catch exception: " + ex.ToString());
                return;
            }
        }







        //获取所有满足条件的job
        public List<string> GetAllDisplayJobs(DateTime dDateFrom, DateTime dDateTo, string sDescription, string sPartNumber, string sJobNo, string sModel, string sSupplier, string sColor, string sCoating)
        {
            Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();

            DataTable dt = bll.GetAllDisplayJobs(dDateFrom, dDateTo, sDescription, sPartNumber, sJobNo, sModel, sSupplier, sColor, sCoating);
            if (dt == null || dt.Rows.Count == 0)
                return null;

            List<string> jobList = new List<string>();
            foreach (DataRow      dr in dt.Rows)
            {
                jobList.Add(dr["jobid"].ToString());
            }


            return jobList;
        }
        

        //获取pqc detail tracking的信息
        public List<ViewModel.PQCButtonReport_ViewModel.PQCDetail> GetPQCDetialList(string strWhere)
        {
            Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();
            DataTable dt = bll.GetVIDetailForButtonReport_NEW(strWhere);


            if (dt == null || dt.Rows.Count == 0)
                return null;


            List<ViewModel.PQCButtonReport_ViewModel.PQCDetail> models = new List<ViewModel.PQCButtonReport_ViewModel.PQCDetail>();
            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.PQCButtonReport_ViewModel.PQCDetail model = new ViewModel.PQCButtonReport_ViewModel.PQCDetail();
                model.jobID = dr["jobID"].ToString().ToUpper();
                model.model = dr["model"].ToString();
                model.partNumber = dr["partNumber"].ToString();
                model.materialNo = dr["materialPartNo"].ToString();
                model.lotQty = 0;
                model.passQty = double.Parse(dr["passQty"].ToString());
                model.rejectQty = double.Parse(dr["rejectQty"].ToString());
                model.rejectRate = 0;
                model.supplier = dr["supplier"].ToString();
                model.process = dr["processes"].ToString();
                model.partsType = dr["PartsType"].ToString();
                model.mouldType = dr["mouldType"].ToString();
             
                model.OP = dr["OP"].ToString();

                models.Add(model);
            }



            var jobList = from a in models
                          group a by a.jobID into b
                          select new
                          {
                              b.Key,
                              lastProcess = b.Max(p => p.process)
                          };


            var result = (from a in models
                          join b in jobList on a.jobID equals b.Key
                          where a.process == b.lastProcess
                          orderby a.jobID ascending
                          select a).ToList();




            return result;
        }


        //获取pqc defect 信息
        public List<ViewModel.PQCButtonReport_ViewModel.PQCDefect> GetPQCDefectList(string sqlWhere)
        {
            Common.Class.BLL.PQCQaViDefectTracking_BLL bll = new Common.Class.BLL.PQCQaViDefectTracking_BLL();
            DataTable dt = bll.GetVIDefectForButtonReport_NEW(sqlWhere);


            if (dt == null || dt.Rows.Count == 0)
                return null;



            List<ViewModel.PQCButtonReport_ViewModel.PQCDefect> pqcDefectModelList = new List<ViewModel.PQCButtonReport_ViewModel.PQCDefect>();
            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.PQCButtonReport_ViewModel.PQCDefect model = new ViewModel.PQCButtonReport_ViewModel.PQCDefect();

                model.jobID = dr["jobID"].ToString().ToUpper();
                model.materialNo = dr["materialPartNo"].ToString();
                model.defectCodeID = dr["defectCodeID"].ToString();
                model.defectCode = dr["defectCode"].ToString();
                model.defectDescription = dr["defectDescription"].ToString();
                model.rejectQty = double.Parse(dr["rejectQty"].ToString());
                model.process = dr["processes"].ToString();

                pqcDefectModelList.Add(model);
            }


            #region 处理laser ng, shortage, buyoff, setup数量.
            //1000 - Shortage - Paint
            //1001 - Buyoff - Laser
            //1002 - Setup - Laser

            //获取2个月内的信息, 防止找不到.
            List<ViewModel.PQCButtonReport_ViewModel.LaserNG> laserNGList = new List<ViewModel.PQCButtonReport_ViewModel.LaserNG>();
            laserNGList = GetLaserNG(sqlWhere);


            //获取Graphic Shift check by M/C code的列表
            List<ViewModel.PQCButtonReport_ViewModel.PQCDefect> pqcDefectlaserMCRejList = new List<ViewModel.PQCButtonReport_ViewModel.PQCDefect>();
            pqcDefectlaserMCRejList = (from a in pqcDefectModelList
                                       where a.defectCode == "Graphic Shift check by M/C"
                                       //目前只有check#1的才会经过laser工序
                                       //没有过滤process, 会导致有check#2的多赋值一次laser ng的数量.
                                       && a.process == "CHECK#1"
                                       select a).ToList();

            foreach (var pqcDefectModel in pqcDefectlaserMCRejList)
            {

                //  ==============================  // 
                //同一个job会有check多次才完成. 就会有N条记录. 每次都赋值laser ng数量就会导致 总数 > mrp qty.
                //如果Graphic Shift check by M/C 这个code的记录总数超过 2* materialCount就跳过. 防止重复赋值.  

                int materialCount = (from a in pqcDefectlaserMCRejList where a.jobID == pqcDefectModel.jobID group a by a.materialNo into material select new { material.Key }).Count();
                int trackingCount = (from a in pqcDefectlaserMCRejList where a.jobID == pqcDefectModel.jobID select a).Count() / materialCount;


                int totalListCount = (from a in pqcDefectModelList where a.jobID == pqcDefectModel.jobID && a.defectCode == "Graphic Shift check by M/C" select a).Count();




                if (totalListCount >= (trackingCount + 1) * materialCount)
                {
                    continue;
                }
                //  ==============================  // 



                var laserNGModel = (from a in laserNGList
                                    where a.jobNo == pqcDefectModel.jobID
                                    && a.materialNo == pqcDefectModel.materialNo
                                    select a).FirstOrDefault();



                if (laserNGModel != null)
                {



                    //laser ng赋值到 pqcdefect 并添加到list
                    ViewModel.PQCButtonReport_ViewModel.PQCDefect pqcDefectModelNew = new ViewModel.PQCButtonReport_ViewModel.PQCDefect();
                    pqcDefectModelNew.jobID = pqcDefectModel.jobID;
                    pqcDefectModelNew.materialNo = pqcDefectModel.materialNo;
                    pqcDefectModelNew.defectCodeID = pqcDefectModel.defectCodeID;
                    pqcDefectModelNew.defectCode = pqcDefectModel.defectCode;
                    pqcDefectModelNew.defectDescription = pqcDefectModel.defectDescription;
                    pqcDefectModelNew.process = pqcDefectModel.process;
                    pqcDefectModelNew.rejectQty = laserNGModel.ng;//laser ng
                    pqcDefectModelList.Add(pqcDefectModelNew);

                    //添加shortage    1000 - Shortage - Paint
                    pqcDefectModelNew = new ViewModel.PQCButtonReport_ViewModel.PQCDefect();
                    pqcDefectModelNew.jobID = pqcDefectModel.jobID;
                    pqcDefectModelNew.materialNo = pqcDefectModel.materialNo;
                    pqcDefectModelNew.defectCodeID = "1000";
                    pqcDefectModelNew.defectCode = "Shortage";
                    pqcDefectModelNew.defectDescription = "Paint";
                    pqcDefectModelNew.process = pqcDefectModel.process;
                    pqcDefectModelNew.rejectQty = laserNGModel.shortage;
                    pqcDefectModelList.Add(pqcDefectModelNew);

                    //添加buyoff     1001 - Buyoff - Laser
                    pqcDefectModelNew = new ViewModel.PQCButtonReport_ViewModel.PQCDefect();
                    pqcDefectModelNew.jobID = pqcDefectModel.jobID;
                    pqcDefectModelNew.materialNo = pqcDefectModel.materialNo;
                    pqcDefectModelNew.defectCodeID = "1001";
                    pqcDefectModelNew.defectCode = "Buyoff";
                    pqcDefectModelNew.defectDescription = "Laser";
                    pqcDefectModelNew.process = pqcDefectModel.process;
                    pqcDefectModelNew.rejectQty = laserNGModel.buyoff;
                    pqcDefectModelList.Add(pqcDefectModelNew);

                    //添加setup       1002 - Setup - Laser
                    pqcDefectModelNew = new ViewModel.PQCButtonReport_ViewModel.PQCDefect();
                    pqcDefectModelNew.jobID = pqcDefectModel.jobID;
                    pqcDefectModelNew.materialNo = pqcDefectModel.materialNo;
                    pqcDefectModelNew.defectCodeID = "1002";
                    pqcDefectModelNew.defectCode = "Setup";
                    pqcDefectModelNew.defectDescription = "Laser";
                    pqcDefectModelNew.process = pqcDefectModel.process;
                    pqcDefectModelNew.rejectQty = laserNGModel.setup;
                    pqcDefectModelList.Add(pqcDefectModelNew);
                }
            }
            #endregion




            return pqcDefectModelList;
        }


        //获取laser vision rej, shortage, buyoff, setup 数量.
        public List<ViewModel.PQCButtonReport_ViewModel.LaserNG> GetLaserNG(string strWhere)
        {
            Common.BLL.LMMSWatchLog_BLL bll = new Common.BLL.LMMSWatchLog_BLL();
            DataTable dt = bll.GetLaserRejButtonReport_NEW(strWhere);

            if (dt == null)
                return null;


            List<ViewModel.PQCButtonReport_ViewModel.LaserNG> mdoels = new List<ViewModel.PQCButtonReport_ViewModel.LaserNG>();

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["materialNo"].ToString() == "")
                    continue;

                ViewModel.PQCButtonReport_ViewModel.LaserNG model = new ViewModel.PQCButtonReport_ViewModel.LaserNG();
                model.jobNo = dr["jobNumber"].ToString().ToUpper();
                model.materialNo = dr["materialNo"].ToString();
                model.ng = double.Parse(dr["ng"].ToString());
                model.shortage = double.Parse(dr["shortage"].ToString());
                model.setup = double.Parse(dr["setUpQTY"].ToString());
                model.buyoff = double.Parse(dr["buyOffQty"].ToString());

                mdoels.Add(model);
            }


            return mdoels;
        }


        //获取laser info
        public List<ViewModel.PQCButtonReport_ViewModel.LaserInfo> GetLaserInfoList(string strWhere)
        {
            Common.Class.BLL.LMMSBUYOFFLIST_BLL bll = new Common.Class.BLL.LMMSBUYOFFLIST_BLL();
            DataTable dt = bll.GetLaserInfoButtonReport_NEW(strWhere);

            if (dt == null)
                return null;


            List<ViewModel.PQCButtonReport_ViewModel.LaserInfo> mdoels = new List<ViewModel.PQCButtonReport_ViewModel.LaserInfo>();

            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.PQCButtonReport_ViewModel.LaserInfo model = new ViewModel.PQCButtonReport_ViewModel.LaserInfo();
                model.jobNo = dr["JobID"].ToString().ToUpper();
                if (dr["dateTime"].ToString() == "")
                {
                    model.laserDate = null;
                }
                else
                {
                    model.laserDate = DateTime.Parse(dr["dateTime"].ToString());
                }

                model.laserOP = dr["laserOP"].ToString();
                model.laserMachine = dr["laserMachine"].ToString();

                mdoels.Add(model);
            }


            return mdoels;
        }


        //获取paint info
        public List<ViewModel.PQCButtonReport_ViewModel.PaintTempInfo> GetPaintTempInfoList(string strWhere )
        {
            Common.Class.BLL.PaintingTempInfo bll = new Common.Class.BLL.PaintingTempInfo();
            DataTable dt = bll.GetPaintTempInfoForButtonReport_NEW(strWhere);
            if (dt == null)
                return null;


            List<ViewModel.PQCButtonReport_ViewModel.PaintTempInfo> paintTempInfoList = new List<ViewModel.PQCButtonReport_ViewModel.PaintTempInfo>();

            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.PQCButtonReport_ViewModel.PaintTempInfo model = new ViewModel.PQCButtonReport_ViewModel.PaintTempInfo();
                model.jobNo = dr["JobID"].ToString().ToUpper();
                model.lotNo = dr["lotNo"].ToString();

                if (dr["MFGDate"].ToString() != "")
                {
                    model.mfgDate = DateTime.Parse(dr["MFGDate"].ToString());
                }
                else
                {
                    model.mfgDate = null;
                }

                model.paintSetUpQty = double.Parse(dr["setupRejQty"].ToString());
                model.paintQAQty = double.Parse(dr["qaTestQty"].ToString());

                model.paintCoat1st = dr["coat_1st"].ToString();
                model.paintMachine1st = dr["pMachine_1st"].ToString();

                if (dr["paintingDate_1st"].ToString() != "")
                {
                    model.paintDate1st = DateTime.Parse(dr["paintingDate_1st"].ToString());
                }
                else
                {
                    model.paintDate1st = null;
                }


                model.paintCoat2nd = dr["coat_2nd"].ToString();
                model.paintMachine2nd = dr["pMachine_2nd"].ToString();
                if (dr["paintingDate_2nd"].ToString() != "")
                {
                    model.paintDate2nd = DateTime.Parse(dr["paintingDate_2nd"].ToString());
                }
                else
                {
                    model.paintDate2nd = null;
                }


                model.paintCoat3rd = dr["coat_3rd"].ToString();
                model.paintMachine3rd = dr["pMachine_3rd"].ToString();
                if (dr["paintingDate_3rd"].ToString() != "")
                {
                    model.paintDate3rd = DateTime.Parse(dr["paintingDate_3rd"].ToString());
                }
                else
                {
                    model.paintDate3rd = null;
                }



                paintTempInfoList.Add(model);
            }

            return paintTempInfoList;
        }


        //获取paint delivery info
        public List<ViewModel.PQCButtonReport_ViewModel.PaintDelivery> GetPaintDeliveryList(string strWhere)
        {
            Common.Class.BLL.PaintingDeliveryHis_BLL bll = new Common.Class.BLL.PaintingDeliveryHis_BLL();
            DataTable dt = bll.GetPaintDeliveryForButtonReport_NEW(strWhere);

            if (dt == null)
                return null;


            List<ViewModel.PQCButtonReport_ViewModel.PaintDelivery> paintDeliveryList = new List<ViewModel.PQCButtonReport_ViewModel.PaintDelivery>();
            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.PQCButtonReport_ViewModel.PaintDelivery model = new ViewModel.PQCButtonReport_ViewModel.PaintDelivery();
                model.jobNo = dr["jobNumber"].ToString().ToUpper();
                model.lotNo = dr["lotNo"].ToString();
                model.mrpQty = int.Parse(dr["MrpQty"].ToString());
                model.paintProcess = dr["paintProcess"].ToString();
                
                paintDeliveryList.Add(model);
            }

            return paintDeliveryList;
        }


        //获取某个code的rej qyt
        public double GetDefectCodeRejQty(List<ViewModel.PQCButtonReport_ViewModel.PQCDefect> jobDefectList, string defectCode, string defectType)
        {
            var model = from a in jobDefectList where a.defectCode == defectCode && a.defectDescription == defectType select a;

            double rejQty = 0;

            //defect code后期改过, 早期的defect detail中可能找不到, 默认为0
            if (model == null || model.Count() == 0)
                rejQty = 0;

            //正常情况一条tracking记录每个defect code只有一条记录.
            else if (model.Count() == 1)
                rejQty = model.FirstOrDefault().rejectQty;

            //laser ng, shortage, setup, buyoff由于自动添加过, 可能出现多条记录, 求sum.
            else
                rejQty = model.Sum(p => p.rejectQty);


            return rejQty;
        }




        void Display(DataTable dt, ViewModel.PQCButtonReport_ViewModel.Report modelForDisplay)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                this.dgButton.Visible = false;
                Common.CommFunctions.ShowMessage(this.Page, "There is no any data!");
                return;
            }
            else
            {
                //dt.Columns.RemoveAt(3);//remove column job no
                //dt.Columns.RemoveAt(2);//remove column parts type
                this.dgButton.Visible = true;
            }


            dgButton.DataSource = dt.DefaultView;
            dgButton.DataBind();


            int sn = 1;

            int iPart2TitleRowIndex = 0;

            foreach (DataGridItem item in dgButton.Items)
            {
                //处理sn
                item.Cells[0].Text = sn.ToString();
                sn++;

                string sPartRowText = item.Cells[4].Text;

                for (int i = 0; i < dgButton.Columns.Count; i++)
                {
                    //处理datetime列信息.  转换成 dd/MM/yyyy
                    string columnName = dgButton.Columns[i].HeaderText;
                    if (columnName.ToUpper().Contains("DATE"))
                    {
                        string sDate = item.Cells[i].Text;
                        if (sDate != "" && sDate != "&nbsp;")
                            item.Cells[i].Text = DateTime.Parse(sDate).ToString("dd/MM/yyyy");
                    }


                    //处理reject rate列信息, 加上 '%'
                    if (columnName.Contains("%"))
                    {
                        if (columnName == "Rej(%)")
                            continue;

                        string sRej = item.Cells[i].Text;
                        if (sRej != "" && sRej != "&nbsp;")
                            item.Cells[i].Text = sRej + "%";
                    }


                    #region 处理summary行, defect code列只有对应分类显示,  other,mould,paint,laser行 lotqty, pass2列值赋空
                    if (sPartRowText == "OTHERS >")
                    {
                        if (columnName.Contains("(TM)") || columnName.Contains("(VM)") || columnName.Contains("(P)") || columnName.Contains("(L)"))
                            item.Cells[i].Text = "";
                        else if (columnName == "Lot Qty" || columnName == "Pass")
                            item.Cells[i].Text = "";
                    }
                    else if (sPartRowText == "TTS MOULD >")
                    {
                        if (columnName.Contains("(VM)") || columnName.Contains("(P)") || columnName.Contains("(L)") || columnName.Contains("(O)"))
                            item.Cells[i].Text = "";
                        //else if (columnName == "Lot Qty" || columnName == "Pass")
                        else if (columnName == "Pass")
                            item.Cells[i].Text = "";
                    }
                    else if (sPartRowText == "VENDOR MOULD >")
                    {
                        if (columnName.Contains("(TM)") || columnName.Contains("(P)") || columnName.Contains("(L)") || columnName.Contains("(O)"))
                            item.Cells[i].Text = "";
                        //else if (columnName == "Lot Qty" || columnName == "Pass")
                        else if (columnName == "Pass")
                            item.Cells[i].Text = "";
                    }
                    else if (sPartRowText == "PAINTING >")
                    {
                        if (columnName.Contains("(TM)") || columnName.Contains("(VM)") || columnName.Contains("(L)") || columnName.Contains("(O)"))
                            item.Cells[i].Text = "";
                        else if (columnName == "Lot Qty" || columnName == "Pass")
                            item.Cells[i].Text = "";
                    }
                    else if (sPartRowText == "PAINTING SETUP >")
                    {
                        if (columnName.Contains("(TM)") || columnName.Contains("(VM)") || columnName.Contains("(P)") || columnName.Contains("(L)") || columnName.Contains("(O)"))
                            item.Cells[i].Text = "";
                        else if (columnName == "Lot Qty" || columnName == "Pass")
                            item.Cells[i].Text = "";
                    }
                    else if (sPartRowText == "QA PAINT TEST >")
                    {
                        if (columnName.Contains("(TM)") || columnName.Contains("(VM)") || columnName.Contains("(P)") || columnName.Contains("(L)") || columnName.Contains("(O)"))
                            item.Cells[i].Text = "";
                        else if (columnName == "Lot Qty" || columnName == "Pass")
                            item.Cells[i].Text = "";
                    }

                    else if (sPartRowText == "LASER >")
                    {
                        if (columnName.Contains("(TM)") || columnName.Contains("(VM)") || columnName.Contains("(P)") || columnName.Contains("(O)"))
                            item.Cells[i].Text = "";
                        else if (columnName == "Lot Qty" || columnName == "Pass")
                            item.Cells[i].Text = "";

                    }
                    else if (sPartRowText == "OVERALL >")
                    {
                        if (columnName.Contains("(TM)") || columnName.Contains("(VM)") || columnName.Contains("(P)") || columnName.Contains("(L)") || columnName.Contains("(O)"))
                            item.Cells[i].Text = "";
                    }
                    #endregion


                    #region 处理defect code qty, 为0的 填空,  非0的红色字体加粗.
                    if (columnName.Contains("(TM)") || columnName.Contains("(VM)") || columnName.Contains("(P)") || columnName.Contains("(L)") || columnName.Contains("(O)"))
                    {
                        //特殊行跳过
                        if (sPartRowText == "OTHERS >" || sPartRowText == "TTS MOULD >" ||
                            sPartRowText == "VENDOR MOULD >" || sPartRowText == "PAINTING >" || sPartRowText == "PAINTING SETUP >" ||
                            sPartRowText == "QA PAINT TEST >" || sPartRowText == "LASER >" || sPartRowText == "OVERALL >")
                            continue;


                        //特殊列跳过
                        if (columnName == "(L)Machine" || columnName == "(L)OP" || columnName == "(L)Date" ||
                            columnName == "(P)U/C 1st Coat" || columnName == "(P)1st Machine" || columnName == "(P)1st Date" ||
                            columnName == "(P)M/C 2nd Coat" || columnName == "(P)2nd Machine" || columnName == "(P)2nd Date" ||
                            columnName == "(P)T/C 3rd Coat" || columnName == "(P)3rd Machine" || columnName == "(P)3rd Date" ||
                            columnName == "(L)Machine" || columnName == "(L)OP" || columnName == "(L)Date" ||
                            columnName == "(TM)TotalRej" || columnName == "(VM)TotalRej" || columnName == "(P)TotalRej" ||
                            columnName == "(L)TotalRej" || columnName == "(O)TotalRej" || columnName.Contains("%"))
                            continue;


                        string sQty = item.Cells[i].Text;
                        if (sQty == "0")
                        {
                            item.Cells[i].Text = "";
                        }
                        else
                        {
                            item.Cells[i].Font.Bold = true;
                            item.Cells[i].ForeColor = sPartRowText == "SUB TOTAL>>" ? System.Drawing.Color.DarkRed : System.Drawing.Color.Red;
                        }
                    }
                    #endregion


                }


                #region 处理 special row  style
                string titleText = item.Cells[1].Text;
                string modelSummaryRowText = item.Cells[4].Text;
                string summaryRowText = item.Cells[4].Text;

                //part 1 title  style
                if (titleText == "PART 1: PAINTING ONLY PARTS")
                {
                    item.Cells[1].ForeColor = System.Drawing.Color.Brown;
                    item.Cells[1].Font.Bold = true;
                    item.Cells[1].Font.Underline = true;
                    item.Cells[1].ColumnSpan = 3;

                    item.Cells[2].ForeColor = System.Drawing.Color.Brown;
                    item.Cells[2].Font.Bold = true;
                    item.Cells[2].Font.Underline = true;
                    item.Cells[2].ColumnSpan = 3;


                    for (int i = 0; i < item.Cells.Count; i++)
                    {
                        item.Cells[i].BorderStyle = BorderStyle.None;
                        item.Cells[i].BackColor = System.Drawing.Color.White;
                    }


                    item.Cells[3].Visible = false;
                    item.Cells[4].Visible = false;
                    item.Cells[5].Visible = false;

                }
                //part 2 title  style
                else if (titleText == "PART 2: LASER PARTS")
                {
                    item.Cells[1].ForeColor = System.Drawing.Color.Blue;
                    item.Cells[1].Font.Bold = true;
                    item.Cells[1].Font.Underline = true;
                    item.Cells[1].ColumnSpan = 3;

                    item.Cells[2].ForeColor = System.Drawing.Color.Blue;
                    item.Cells[2].Font.Bold = true;
                    item.Cells[2].Font.Underline = true;
                    item.Cells[2].ColumnSpan = 3;


                    for (int i = 0; i < item.Cells.Count; i++)
                    {
                        item.Cells[i].BorderStyle = BorderStyle.None;
                        item.Cells[i].BackColor = System.Drawing.Color.White;
                    }

                    item.Cells[3].Visible = false;
                    item.Cells[4].Visible = false;
                    item.Cells[5].Visible = false;

                    iPart2TitleRowIndex = item.ItemIndex;
                }
                //summary row  title style
                else if (titleText == "SUMMARY:")
                {
                    item.Cells[1].ForeColor = System.Drawing.Color.DeepPink;
                    item.Cells[1].Font.Bold = true;
                    item.Cells[1].ColumnSpan = 3;

                    item.Cells[2].ForeColor = System.Drawing.Color.DeepPink;
                    item.Cells[2].Font.Bold = true;
                    item.Cells[2].ColumnSpan = 3;

                    for (int i = 0; i < item.Cells.Count; i++)
                    {
                        item.Cells[i].BorderStyle = BorderStyle.None;
                        item.Cells[i].BackColor = System.Drawing.Color.White;
                    }

                    item.Cells[3].Visible = false;
                    item.Cells[4].Visible = false;
                    item.Cells[5].Visible = false;
                }
                //sub total row style
                else if (modelSummaryRowText == "SUB TOTAL>>")
                {
                    item.BackColor = System.Drawing.Color.Beige;
                    item.Cells[1].Font.Bold = true;
                    item.Cells[4].Font.Bold = true;
                }
                else if (summaryRowText == "OTHERS >" || summaryRowText == "TTS MOULD >" || summaryRowText == "VENDOR MOULD >" || summaryRowText == "PAINTING >"
                    || summaryRowText == "PAINTING SETUP >" || summaryRowText == "QA PAINT TEST >" || summaryRowText == "LASER >" || summaryRowText == "OVERALL >")
                {
                    for (int i = 3; i < 11; i++)
                    {
                        item.Cells[i].Font.Bold = true;
                        item.Cells[i].ForeColor = System.Drawing.Color.Black;
                    }
                }
                #endregion


            }


            #region part 1 laser info 区域灰色覆盖

            //part 1 summary row 前一行
            int iPart1LastRowIndex = iPart2TitleRowIndex - 10;

            for (int i = 1; i < iPart1LastRowIndex; i++)
            {
                for (int x = 0; x < dgButton.Columns.Count; x++)
                {
                    string columnName = dgButton.Columns[x].HeaderText;
                    if (columnName.Contains("(L)"))
                    {
                        this.dgButton.Items[i].Cells[x].BackColor = System.Drawing.Color.Gainsboro;
                        this.dgButton.Items[i].Cells[x].Text = "";
                    }
                }
            }



            #endregion










            #region defect rej 0的列隐藏
            this.dgButton.Columns[12].Visible = modelForDisplay.TTS_Raw_Part_Scratch == 0 ? false : true;
            this.dgButton.Columns[13].Visible = modelForDisplay.TTS_Oil_Stain == 0 ? false : true;
            this.dgButton.Columns[14].Visible = modelForDisplay.TTS_Dented == 0 ? false : true;
            this.dgButton.Columns[15].Visible = modelForDisplay.TTS_Dust == 0 ? false : true;
            this.dgButton.Columns[16].Visible = modelForDisplay.TTS_Flyout == 0 ? false : true;
            this.dgButton.Columns[17].Visible = modelForDisplay.TTS_Over_Spray == 0 ? false : true;
            this.dgButton.Columns[18].Visible = modelForDisplay.TTS_Weld_line == 0 ? false : true;
            this.dgButton.Columns[19].Visible = modelForDisplay.TTS_Crack == 0 ? false : true;
            this.dgButton.Columns[20].Visible = modelForDisplay.TTS_Gas_mark == 0 ? false : true;
            this.dgButton.Columns[21].Visible = modelForDisplay.TTS_Sink_mark == 0 ? false : true;
            this.dgButton.Columns[22].Visible = modelForDisplay.TTS_Bubble == 0 ? false : true;
            this.dgButton.Columns[23].Visible = modelForDisplay.TTS_White_dot == 0 ? false : true;
            this.dgButton.Columns[24].Visible = modelForDisplay.TTS_Black_dot == 0 ? false : true;
            this.dgButton.Columns[25].Visible = modelForDisplay.TTS_Red_Dot == 0 ? false : true;
            this.dgButton.Columns[26].Visible = modelForDisplay.TTS_Poor_Gate_Cut == 0 ? false : true;
            this.dgButton.Columns[27].Visible = modelForDisplay.TTS_High_Gate == 0 ? false : true;
            this.dgButton.Columns[28].Visible = modelForDisplay.TTS_White_Mark == 0 ? false : true;
            this.dgButton.Columns[29].Visible = modelForDisplay.TTS_Drag_mark == 0 ? false : true;
            this.dgButton.Columns[30].Visible = modelForDisplay.TTS_Foreigh_Material == 0 ? false : true;
            this.dgButton.Columns[31].Visible = modelForDisplay.TTS_Double_Claim == 0 ? false : true;
            this.dgButton.Columns[32].Visible = modelForDisplay.TTS_Short_mould == 0 ? false : true;
            this.dgButton.Columns[33].Visible = modelForDisplay.TTS_Flashing == 0 ? false : true;
            this.dgButton.Columns[34].Visible = modelForDisplay.TTS_Pink_Mark == 0 ? false : true;
            this.dgButton.Columns[35].Visible = modelForDisplay.TTS_Deform == 0 ? false : true;
            this.dgButton.Columns[36].Visible = modelForDisplay.TTS_Damage == 0 ? false : true;
            this.dgButton.Columns[37].Visible = modelForDisplay.TTS_Mould_Dirt == 0 ? false : true;
            this.dgButton.Columns[38].Visible = modelForDisplay.TTS_Yellowish == 0 ? false : true;
            this.dgButton.Columns[39].Visible = modelForDisplay.TTS_Oil_Mark == 0 ? false : true;
            this.dgButton.Columns[40].Visible = modelForDisplay.TTS_Printing_Mark == 0 ? false : true;
            this.dgButton.Columns[41].Visible = modelForDisplay.TTS_Printing_Uneven == 0 ? false : true;
            this.dgButton.Columns[42].Visible = modelForDisplay.TTS_Printing_Color_Dark == 0 ? false : true;
            this.dgButton.Columns[43].Visible = modelForDisplay.TTS_Wrong_Orietation == 0 ? false : true;
            this.dgButton.Columns[44].Visible = modelForDisplay.TTS_Other == 0 ? false : true;

            this.dgButton.Columns[47].Visible =modelForDisplay.Vendor_Raw_Part_Scratch == 0 ? false : true;
            this.dgButton.Columns[48].Visible =modelForDisplay.Vendor_Oil_Stain == 0 ? false : true;
            this.dgButton.Columns[49].Visible =modelForDisplay.Vendor_Dented == 0 ? false : true;
            this.dgButton.Columns[50].Visible =modelForDisplay.Vendor_Dust == 0 ? false : true;
            this.dgButton.Columns[51].Visible =modelForDisplay.Vendor_Flyout == 0 ? false : true;
            this.dgButton.Columns[52].Visible =modelForDisplay.Vendor_Over_Spray == 0 ? false : true;
            this.dgButton.Columns[53].Visible =modelForDisplay.Vendor_Weld_line == 0 ? false : true;
            this.dgButton.Columns[54].Visible =modelForDisplay.Vendor_Crack == 0 ? false : true;
            this.dgButton.Columns[55].Visible =modelForDisplay.Vendor_Gas_mark == 0 ? false : true;
            this.dgButton.Columns[56].Visible =modelForDisplay.Vendor_Sink_mark == 0 ? false : true;
            this.dgButton.Columns[57].Visible =modelForDisplay.Vendor_Bubble == 0 ? false : true;
            this.dgButton.Columns[58].Visible =modelForDisplay.Vendor_White_dot == 0 ? false : true;
            this.dgButton.Columns[59].Visible =modelForDisplay.Vendor_Black_dot == 0 ? false : true;
            this.dgButton.Columns[60].Visible =modelForDisplay.Vendor_Red_Dot == 0 ? false : true;
            this.dgButton.Columns[61].Visible =modelForDisplay.Vendor_Poor_Gate_Cut == 0 ? false : true;
            this.dgButton.Columns[62].Visible =modelForDisplay.Vendor_High_Gate == 0 ? false : true;
            this.dgButton.Columns[63].Visible =modelForDisplay.Vendor_White_Mark == 0 ? false : true;
            this.dgButton.Columns[64].Visible =modelForDisplay.Vendor_Drag_mark == 0 ? false : true;
            this.dgButton.Columns[65].Visible =modelForDisplay.Vendor_Foreigh_Material == 0 ? false : true;
            this.dgButton.Columns[66].Visible =modelForDisplay.Vendor_Double_Claim == 0 ? false : true;
            this.dgButton.Columns[67].Visible =modelForDisplay.Vendor_Short_mould == 0 ? false : true;
            this.dgButton.Columns[68].Visible =modelForDisplay.Vendor_Flashing == 0 ? false : true;
            this.dgButton.Columns[69].Visible =modelForDisplay.Vendor_Pink_Mark == 0 ? false : true;
            this.dgButton.Columns[70].Visible =modelForDisplay.Vendor_Deform == 0 ? false : true;
            this.dgButton.Columns[71].Visible =modelForDisplay.Vendor_Damage == 0 ? false : true;
            this.dgButton.Columns[72].Visible =modelForDisplay.Vendor_Mould_Dirt == 0 ? false : true;
            this.dgButton.Columns[73].Visible =modelForDisplay.Vendor_Yellowish == 0 ? false : true;
            this.dgButton.Columns[74].Visible =modelForDisplay.Vendor_Oil_Mark == 0 ? false : true;
            this.dgButton.Columns[75].Visible =modelForDisplay.Vendor_Printing_Mark == 0 ? false : true;
            this.dgButton.Columns[76].Visible =modelForDisplay.Vendor_Printing_Uneven == 0 ? false : true;
            this.dgButton.Columns[77].Visible =modelForDisplay.Vendor_Printing_Color_Dark == 0 ? false : true;
            this.dgButton.Columns[78].Visible =modelForDisplay.Vendor_Wrong_Orietation == 0 ? false : true;
            this.dgButton.Columns[79].Visible =modelForDisplay.Vendor_Other == 0 ? false : true;

            this.dgButton.Columns[83].Visible =modelForDisplay.Paint_Particle == 0 ? false : true;
            this.dgButton.Columns[84].Visible =modelForDisplay.Paint_Fibre == 0 ? false : true;
            this.dgButton.Columns[85].Visible =modelForDisplay.Paint_Many_particle == 0 ? false : true;
            this.dgButton.Columns[86].Visible =modelForDisplay.Paint_Stain_mark == 0 ? false : true;
            this.dgButton.Columns[87].Visible =modelForDisplay.Paint_Uneven_paint == 0 ? false : true;
            this.dgButton.Columns[88].Visible =modelForDisplay.Paint_Under_coat_uneven_paint == 0 ? false : true;
            this.dgButton.Columns[89].Visible =modelForDisplay.Paint_Under_spray == 0 ? false : true;
            this.dgButton.Columns[90].Visible =modelForDisplay.Paint_White_dot == 0 ? false : true;
            this.dgButton.Columns[91].Visible =modelForDisplay.Paint_Silver_dot == 0 ? false : true;
            this.dgButton.Columns[92].Visible =modelForDisplay.Paint_Dust == 0 ? false : true;
            this.dgButton.Columns[93].Visible =modelForDisplay.Paint_Paint_crack == 0 ? false : true;
            this.dgButton.Columns[94].Visible =modelForDisplay.Paint_Bubble == 0 ? false : true;
            this.dgButton.Columns[95].Visible =modelForDisplay.Paint_Scratch == 0 ? false : true;
            this.dgButton.Columns[96].Visible =modelForDisplay.Paint_Abrasion_Mark == 0 ? false : true;
            this.dgButton.Columns[97].Visible =modelForDisplay.Paint_Paint_Dripping == 0 ? false : true;
            this.dgButton.Columns[98].Visible =modelForDisplay.Paint_Rough_Surface == 0 ? false : true;
            this.dgButton.Columns[99].Visible =modelForDisplay.Paint_Shinning == 0 ? false : true;
            this.dgButton.Columns[100].Visible =modelForDisplay.Paint_Matt == 0 ? false : true;
            this.dgButton.Columns[101].Visible =modelForDisplay.Paint_Paint_Pin_Hole == 0 ? false : true;
            this.dgButton.Columns[102].Visible =modelForDisplay.Paint_Light_Leakage == 0 ? false : true;
            this.dgButton.Columns[103].Visible =modelForDisplay.Paint_White_Mark == 0 ? false : true;
            this.dgButton.Columns[104].Visible =modelForDisplay.Paint_Dented == 0 ? false : true;
            this.dgButton.Columns[105].Visible =modelForDisplay.Paint_Other == 0 ? false : true;
            this.dgButton.Columns[106].Visible =modelForDisplay.Paint_Particle_for_laser_setup == 0 ? false : true;
            this.dgButton.Columns[107].Visible =modelForDisplay.Paint_Buyoff == 0 ? false : true;
            this.dgButton.Columns[108].Visible =modelForDisplay.Paint_Shortage == 0 ? false : true;

            this.dgButton.Columns[124].Visible =modelForDisplay.Laser_Black_Mark == 0 ? false : true;
            this.dgButton.Columns[125].Visible =modelForDisplay.Laser_Black_Dot == 0 ? false : true;
            this.dgButton.Columns[126].Visible =modelForDisplay.Laser_Graphic_Shift_check_by_PQC == 0 ? false : true;
            this.dgButton.Columns[127].Visible =modelForDisplay.Laser_Graphic_Shift_check_by_MC == 0 ? false : true;
            this.dgButton.Columns[128].Visible =modelForDisplay.Laser_Scratch == 0 ? false : true;
            this.dgButton.Columns[129].Visible =modelForDisplay.Laser_Jagged == 0 ? false : true;
            this.dgButton.Columns[130].Visible =modelForDisplay.Laser_Laser_Bubble == 0 ? false : true;
            this.dgButton.Columns[131].Visible =modelForDisplay.Laser_double_outer_line == 0 ? false : true;
            this.dgButton.Columns[132].Visible =modelForDisplay.Laser_Pin_hold == 0 ? false : true;
            this.dgButton.Columns[133].Visible =modelForDisplay.Laser_Poor_Laser == 0 ? false : true;
            this.dgButton.Columns[134].Visible =modelForDisplay.Laser_Burm_Mark == 0 ? false : true;
            this.dgButton.Columns[135].Visible =modelForDisplay.Laser_Stain_Mark == 0 ? false : true;
            this.dgButton.Columns[136].Visible =modelForDisplay.Laser_Graphic_Small == 0 ? false : true;
            this.dgButton.Columns[137].Visible =modelForDisplay.Laser_Double_Laser == 0 ? false : true;
            this.dgButton.Columns[138].Visible =modelForDisplay.Laser_Color_Yellow == 0 ? false : true;
            this.dgButton.Columns[139].Visible =modelForDisplay.Laser_Crack == 0 ? false : true;
            this.dgButton.Columns[140].Visible =modelForDisplay.Laser_Smoke == 0 ? false : true;
            this.dgButton.Columns[141].Visible =modelForDisplay.Laser_Wrong_Orientation == 0 ? false : true;
            this.dgButton.Columns[142].Visible =modelForDisplay.Laser_Dented == 0 ? false : true;
            this.dgButton.Columns[143].Visible =modelForDisplay.Laser_Other == 0 ? false : true;
            this.dgButton.Columns[144].Visible =modelForDisplay.Laser_Buyoff == 0 ? false : true;
            this.dgButton.Columns[145].Visible =modelForDisplay.Laser_Setup == 0 ? false : true;

            this.dgButton.Columns[151].Visible =modelForDisplay.PQC_Scratch == 0 ? false : true;
            this.dgButton.Columns[152].Visible =modelForDisplay.Over_Spray == 0 ? false : true;
            this.dgButton.Columns[153].Visible =modelForDisplay.Bubble == 0 ? false : true;
            this.dgButton.Columns[154].Visible =modelForDisplay.Oil_Stain == 0 ? false : true;
            this.dgButton.Columns[155].Visible =modelForDisplay.Drag_Mark == 0 ? false : true;
            this.dgButton.Columns[156].Visible =modelForDisplay.Light_Leakage == 0 ? false : true;
            this.dgButton.Columns[157].Visible =modelForDisplay.Light_Bubble == 0 ? false : true;
            this.dgButton.Columns[158].Visible =modelForDisplay.White_Dot_in_Material == 0 ? false : true;
            this.dgButton.Columns[159].Visible = modelForDisplay.Other == 0 ? false : true;
            #endregion
        }









        //private void SetColorDDL()
        //{
        //    this.ddlColor.Items.Clear();

        //    ListItem Li = new ListItem();
        //    Li.Text = "All";
        //    Li.Value = "";
        //    this.ddlColor.Items.Add(Li);

        //    Li = new ListItem();
        //    Li.Text = "Black";
        //    Li.Value = "Black";
        //    this.ddlColor.Items.Add(Li);

        //    Li = new ListItem();
        //    Li.Text = "Silver";
        //    Li.Value = "Silver";
        //    this.ddlColor.Items.Add(Li);

        //    Li = new ListItem();
        //    Li.Text = "High gloss";
        //    Li.Value = "High gloss";
        //    this.ddlColor.Items.Add(Li);

        //    Li = new ListItem();
        //    Li.Text = "Mat black";
        //    Li.Value = "Mat black";
        //    this.ddlColor.Items.Add(Li);

        //    Li = new ListItem();
        //    Li.Text = "Texture line";
        //    Li.Value = "Texture line";
        //    this.ddlColor.Items.Add(Li);
        //}

        //private void SetModelDDL()
        //{
        //    this.ddlModel.Items.Clear();


        //    Common.Class.BLL.PQCBom_BLL bll = new Common.Class.BLL.PQCBom_BLL();

        //    List<string> modelList = bll.GetModelNoList();
        //    if (modelList == null)
        //        return;

        //    ListItem Li = new ListItem();
        //    Li.Text = "All";
        //    Li.Value = "";

        //    this.ddlModel.Items.Add(Li);



        //    foreach (string model in modelList)
        //    {
        //        if (model != "")
        //        {
        //            Li = new ListItem();

        //            Li.Text = model;
        //            Li.Value = model;

        //            this.ddlModel.Items.Add(Li);
        //        }
        //    }
        //}

        //private void SetSupplierDDL()
        //{
        //    this.ddlSupplier.Items.Clear();

        //    Common.Class.BLL.PQCBom_BLL bll = new Common.Class.BLL.PQCBom_BLL();

        //    List<string> supplierList = bll.GetSupplierList();
        //    if (supplierList == null)
        //        return;

        //    ListItem Li = new ListItem();
        //    Li.Text = "All";
        //    Li.Value = "";

        //    this.ddlSupplier.Items.Add(Li);



        //    foreach (string supplier in supplierList)
        //    {
        //        if (supplier != "")
        //        {
        //            Li = new ListItem();

        //            Li.Text = supplier;
        //            Li.Value = supplier;

        //            this.ddlSupplier.Items.Add(Li);
        //        }
        //    }
        //}


    }
}