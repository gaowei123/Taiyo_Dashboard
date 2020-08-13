using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;

namespace DashboardTTS.ViewBusiness
{
    
    public class Buyoff_ViewBusiness
    {
        private readonly Common.Class.BLL.PQCQaViTracking_BLL trackingBLL = new Common.Class.BLL.PQCQaViTracking_BLL();
        private readonly Common.Class.BLL.PQCQaViDefectTracking_BLL defectTracking = new Common.Class.BLL.PQCQaViDefectTracking_BLL();

        private readonly Common.Class.BLL.LMMSBUYOFFLIST_BLL buyoffBLL = new Common.Class.BLL.LMMSBUYOFFLIST_BLL();
        private readonly Common.Class.BLL.PaintingDeliveryHis_BLL paintBLL = new Common.Class.BLL.PaintingDeliveryHis_BLL();
        private readonly Common.Class.BLL.PQCBomDetail_BLL pqcBomDetailBLL = new Common.Class.BLL.PQCBomDetail_BLL();

        private readonly Common.Class.BLL.LMMSVisionMachineSettingHis_BLL laserParameterBLL = new Common.Class.BLL.LMMSVisionMachineSettingHis_BLL();

        private readonly Common.Class.BLL.PaintingTempInfo paintTempBLL = new Common.Class.BLL.PaintingTempInfo();




        public ViewModel.BuyoffReport_ViewModel.LaserRecord GetLaserModel(string sJobNo)
        {
            DataTable dt = buyoffBLL.GetBuyofflist(sJobNo, "", "", "", "", "", null, null);


            ViewModel.BuyoffReport_ViewModel.LaserRecord model = new ViewModel.BuyoffReport_ViewModel.LaserRecord();

            if (dt == null || dt.Rows.Count == 0)
            {
                DataTable dtPaint = paintBLL.GetList(DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1), sJobNo);

                
                int materialCount = pqcBomDetailBLL.GetMaterialCount(dtPaint.Rows[0]["partNumber"].ToString());

                double lotQty = double.Parse(dtPaint.Rows[0]["inQuantity"].ToString());


                model.lotNo = dtPaint.Rows[0]["lotNo"].ToString();
                model.lotQty = string.Format("{0}({1})", lotQty, lotQty * materialCount);
                model.partNo = dtPaint.Rows[0]["partNumber"].ToString();


                model.machineID = "";
                model.current = "";

                model.blackMark1st = "";
                model.blackDot1st = "";
                model.pinHole1st = "";
                model.jagged1st = "";
                model.checkGuide1st = "";
                model.navitas1st = "";
                model.smartScope1st = "";

                model.blackMark2nd = "";
                model.blackDot2nd = "";
                model.pinHole2nd = "";
                model.jagged2nd = "";
                model.checkGuide2nd = "";
                model.navitas2nd = "";
                model.smartScope2nd = "";

                model.blackMarkIN = "";
                model.blackDotIN = "";
                model.pinHoleIN = "";
                model.jaggedIN = "";
                model.checkGuideIN = "";
                model.navitasIN = "";
                model.smartScopeIN = "";


                model.mcOperator = "";
                model.buyoffBy = "";
                model.approvedBy = "";
                model.checkBy = "";

                model.date = null;



            }
            else
            {

                model.machineID = dt.Rows[0]["MACHINE_ID"].ToString();


                model.lotNo = dt.Rows[0]["LotNo"].ToString();
                model.lotQty = dt.Rows[0]["lotQty"].ToString();
                model.partNo = dt.Rows[0]["PART_NO"].ToString();
                model.current = dt.Rows[0]["CurrentPower"].ToString();

                #region 1st
                if (dt.Rows[0]["BLACK_MARK_1ST"].ToString() != "")
                {
                    model.blackMark1st = dt.Rows[0]["BLACK_MARK_1ST"].ToString();
                    model.blackMark1stColor = dt.Rows[0]["BLACK_MARK_1ST"].ToString() == "OK" ? "Green" : "Red";
                }
                else
                {
                    model.blackMark1st = "";
                }
                if (dt.Rows[0]["BLACK_DOT_1ST"].ToString() != "")
                {
                    model.blackDot1st = dt.Rows[0]["BLACK_DOT_1ST"].ToString();
                    model.blackDot1stColor = dt.Rows[0]["BLACK_DOT_1ST"].ToString() == "OK" ? "Green" : "Red";
                }
                else
                {
                    model.blackDot1st = "";
                }
                if (dt.Rows[0]["PIN_HOLE_1ST"].ToString() != "")
                {
                    model.pinHole1st = dt.Rows[0]["PIN_HOLE_1ST"].ToString();
                    model.pinHole1stColor = dt.Rows[0]["PIN_HOLE_1ST"].ToString() == "OK" ? "Green" : "Red";
                }
                else
                {
                    model.pinHole1st = "";
                }
                if (dt.Rows[0]["JAGGED_1ST"].ToString() != "")
                {
                    model.jagged1st = dt.Rows[0]["JAGGED_1ST"].ToString();
                    model.jagged1stColor = dt.Rows[0]["JAGGED_1ST"].ToString() == "OK" ? "Green" : "Red";
                }
                else
                {
                    model.jagged1st = "";
                }
                if (dt.Rows[0]["CHECK_GULED_1ST"].ToString() != "")
                {
                    model.checkGuide1st = dt.Rows[0]["CHECK_GULED_1ST"].ToString();
                    model.checkGuide1stColor = dt.Rows[0]["CHECK_GULED_1ST"].ToString() == "OK" ? "Green" : "Red";
                }
                else
                {
                    model.checkGuide1st = "";
                }
                if (dt.Rows[0]["NAVITAS_1ST"].ToString() != "")
                {
                    model.navitas1st = dt.Rows[0]["NAVITAS_1ST"].ToString();
                    model.navitas1stColor = dt.Rows[0]["NAVITAS_1ST"].ToString() == "OK" ? "Green" : "Red";
                }
                else
                {
                    model.navitas1st = "";
                }
                if (dt.Rows[0]["SMART_SCOPE_1ST"].ToString() != "")
                {
                    model.smartScope1st = dt.Rows[0]["SMART_SCOPE_1ST"].ToString();
                    model.smartScope1stColor = dt.Rows[0]["SMART_SCOPE_1ST"].ToString() == "OK" ? "Green" : "Red";
                }
                else
                {
                    model.smartScope1st = "";
                }
                #endregion


                #region 2nd
                if (dt.Rows[0]["BLACK_MARK_2ND"].ToString() != "")
                {
                    model.blackMark2nd = dt.Rows[0]["BLACK_MARK_2ND"].ToString();
                    model.blackMark2ndColor = dt.Rows[0]["BLACK_MARK_2ND"].ToString() == "OK" ? "Green" : "Red";
                }
                else
                {
                    model.blackMark2nd = "";
                }
                if (dt.Rows[0]["BLACK_DOT_2ND"].ToString() != "")
                {
                    model.blackDot2nd = dt.Rows[0]["BLACK_DOT_2ND"].ToString();
                    model.blackDot2ndColor = dt.Rows[0]["BLACK_DOT_2ND"].ToString() == "OK" ? "Green" : "Red";
                }
                else
                {
                    model.blackDot2nd = "";
                }
                if (dt.Rows[0]["PIN_HOLE_2ND"].ToString() != "")
                {
                    model.pinHole2nd = dt.Rows[0]["PIN_HOLE_2ND"].ToString();
                    model.pinHole2ndColor = dt.Rows[0]["PIN_HOLE_2ND"].ToString() == "OK" ? "Green" : "Red";
                }
                else
                {
                    model.pinHole2nd = "";
                }
                if (dt.Rows[0]["JAGGED_2ND"].ToString() != "")
                {
                    model.jagged2nd = dt.Rows[0]["JAGGED_2ND"].ToString();
                    model.jagged2ndColor = dt.Rows[0]["JAGGED_2ND"].ToString() == "OK" ? "Green" : "Red";
                }
                else
                {
                    model.jagged2nd = "";
                }
                if (dt.Rows[0]["CHECK_GULED_2ND"].ToString() != "")
                {
                    model.checkGuide2nd = dt.Rows[0]["CHECK_GULED_2ND"].ToString();
                    model.checkGuide2ndColor = dt.Rows[0]["CHECK_GULED_2ND"].ToString() == "OK" ? "Green" : "Red";
                }
                else
                {
                    model.checkGuide2nd = "";
                }
                if (dt.Rows[0]["NAVITAS_2ND"].ToString() != "")
                {
                    model.navitas2nd = dt.Rows[0]["NAVITAS_2ND"].ToString();
                    model.navitas2ndColor = dt.Rows[0]["NAVITAS_2ND"].ToString() == "OK" ? "Green" : "Red";
                }
                else
                {
                    model.navitas2nd = "";
                }
                if (dt.Rows[0]["SMART_SCOPE_1ST"].ToString() != "")
                {
                    model.smartScope2nd = dt.Rows[0]["SMART_SCOPE_2ND"].ToString();
                    model.smartScope2ndColor = dt.Rows[0]["SMART_SCOPE_2ND"].ToString() == "OK" ? "Green" : "Red";
                }
                else
                {
                    model.smartScope2nd = "";
                }
                #endregion


                #region inprocess
                if (dt.Rows[0]["BLACK_MARK_IN"].ToString() != "")
                {
                    model.blackMarkIN = dt.Rows[0]["BLACK_MARK_IN"].ToString();
                    model.blackMarkINColor = dt.Rows[0]["BLACK_MARK_IN"].ToString() == "OK" ? "Green" : "Red";
                }
                else
                {
                    model.blackMarkIN = "";
                }
                if (dt.Rows[0]["BLACK_DOT_IN"].ToString() != "")
                {
                    model.blackDotIN = dt.Rows[0]["BLACK_DOT_IN"].ToString();
                    model.blackDotINColor = dt.Rows[0]["BLACK_DOT_IN"].ToString() == "OK" ? "Green" : "Red";
                }
                else
                {
                    model.blackDotIN = "";
                }
                if (dt.Rows[0]["PIN_HOLE_IN"].ToString() != "")
                {
                    model.pinHoleIN = dt.Rows[0]["PIN_HOLE_IN"].ToString();
                    model.pinHoleINColor = dt.Rows[0]["PIN_HOLE_IN"].ToString() == "OK" ? "Green" : "Red";
                }
                else
                {
                    model.pinHoleIN = "";
                }
                if (dt.Rows[0]["JAGGED_IN"].ToString() != "")
                {
                    model.jaggedIN = dt.Rows[0]["JAGGED_IN"].ToString();
                    model.jaggedINColor = dt.Rows[0]["JAGGED_IN"].ToString() == "OK" ? "Green" : "Red";
                }
                else
                {
                    model.jaggedIN = "";
                }
                if (dt.Rows[0]["CHECK_GULED_IN"].ToString() != "")
                {
                    model.checkGuideIN = dt.Rows[0]["CHECK_GULED_IN"].ToString();
                    model.checkGuideINColor = dt.Rows[0]["CHECK_GULED_IN"].ToString() == "OK" ? "Green" : "Red";
                }
                else
                {
                    model.checkGuideIN = "";
                }
                if (dt.Rows[0]["NAVITAS_IN"].ToString() != "")
                {
                    model.navitasIN = dt.Rows[0]["NAVITAS_IN"].ToString();
                    model.navitasINColor = dt.Rows[0]["NAVITAS_IN"].ToString() == "OK" ? "Green" : "Red";
                }
                else
                {
                    model.navitasIN = "";
                }
                if (dt.Rows[0]["SMART_SCOPE_1ST"].ToString() != "")
                {
                    model.smartScopeIN = dt.Rows[0]["SMART_SCOPE_IN"].ToString();
                    model.smartScopeINColor = dt.Rows[0]["SMART_SCOPE_IN"].ToString() == "OK" ? "Green" : "Red";
                }
                else
                {
                    model.smartScopeIN = "";
                }
                #endregion

                model.mcOperator = dt.Rows[0]["MC_OPERATOR"].ToString();
                model.buyoffBy = dt.Rows[0]["BUYOFF_BY"].ToString();
                model.approvedBy = dt.Rows[0]["APPROVED_BY"].ToString();
                model.checkBy = dt.Rows[0]["CHECK_BY"].ToString();

                model.date = DateTime.Parse(dt.Rows[0]["DATE_TIME"].ToString());

            }
            

            return model;
        }


        public ViewModel.BuyoffReport_ViewModel.LaserParameter GetLaserParameter(string sJobNo)
        {

            DataTable dt = laserParameterBLL.GetList(sJobNo);

            if (dt ==null || dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                ViewModel.BuyoffReport_ViewModel.LaserParameter model = new ViewModel.BuyoffReport_ViewModel.LaserParameter();

                model.rate = dt.Rows[0]["rate"].ToString();
                model.frequency = dt.Rows[0]["frequency"].ToString();
                model.power = dt.Rows[0]["power"].ToString() + "%";
                model.repeat = dt.Rows[0]["repeat"].ToString();

                return model;
            }

        }




        public List<ViewModel.BuyoffReport_ViewModel.MouldDefect> GetMaterialMouldDefectList(string sJobNo, string sTrackingID)
        {
            List<ViewModel.BuyoffReport_ViewModel.MouldDefect> mouldDefectList = new List<ViewModel.BuyoffReport_ViewModel.MouldDefect>();



            DataTable dt = defectTracking.GetDefectDetail(sJobNo, sTrackingID, "Mould");
            if (dt == null ||dt.Rows.Count == 0)
            {
                return null;
            }



            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.BuyoffReport_ViewModel.MouldDefect model = new ViewModel.BuyoffReport_ViewModel.MouldDefect();
                model.materialNo = dr["materialpartNo"].ToString();

                model.rawPartScratch = double.Parse(dr["Raw Part Scratch"].ToString());
                model.oilStain = double.Parse(dr["Oil Stain"].ToString());
                model.dented = double.Parse(dr["Dented"].ToString());
                model.dust = double.Parse(dr["Dust"].ToString());
                model.flyout = double.Parse(dr["Flyout"].ToString());
                model.overSpray = double.Parse(dr["Over Spray"].ToString());
                model.weldline = double.Parse(dr["Weld line"].ToString());
                model.crack = double.Parse(dr["Crack"].ToString());
                model.gasMark = double.Parse(dr["Gas mark"].ToString());
                model.sinkMark = double.Parse(dr["Sink mark"].ToString());
                model.bubble = double.Parse(dr["Bubble"].ToString());
                model.whiteDot = double.Parse(dr["White dot"].ToString());
                model.blackDot = double.Parse(dr["Black dot"].ToString());
                model.redDot = double.Parse(dr["Red Dot"].ToString());
                model.poorGateCut = double.Parse(dr["Poor Gate Cut"].ToString());
                model.highGate = double.Parse(dr["High Gate"].ToString());
                model.whiteMark = double.Parse(dr["White Mark"].ToString());
                model.dragMark = double.Parse(dr["Drag mark"].ToString());
                model.foreighMaterial = double.Parse(dr["Foreigh Material"].ToString());
                model.doubleClaim = double.Parse(dr["Double Claim"].ToString());
                model.shortMould = double.Parse(dr["Short mould"].ToString());
                model.flashing = double.Parse(dr["Flashing"].ToString());
                model.pinkMark = double.Parse(dr["Pink Mark"].ToString());
                model.deform = double.Parse(dr["Deform"].ToString());
                model.damage = double.Parse(dr["Damage"].ToString());
                model.mouldDirt = double.Parse(dr["Mould Dirt"].ToString());
                model.yelloWish = double.Parse(dr["Yellowish"].ToString());
                model.oilMark = double.Parse(dr["Oil Mark"].ToString());
                model.printingMark = double.Parse(dr["Printing Mark"].ToString());
                model.printingUneven = double.Parse(dr["Printing Uneven"].ToString());
                model.printingColorDark = double.Parse(dr["Printing Color Dark"].ToString());
                model.wrongOrietation = double.Parse(dr["Wrong Orietation"].ToString());
                model.other = double.Parse(dr["Other"].ToString());

                model.totalRej = double.Parse(dr["rejectQty"].ToString());

                mouldDefectList.Add(model);
            }


            return mouldDefectList;
        }
        
        public List<ViewModel.BuyoffReport_ViewModel.PaintDefect> GetMaterialPaintDefectList(string sJobNo, string sTrackingID)
        {
            List<ViewModel.BuyoffReport_ViewModel.PaintDefect> paintDefectList = new List<ViewModel.BuyoffReport_ViewModel.PaintDefect>();

         
            try
            {
                
                DataTable dt = defectTracking.GetDefectDetail(sJobNo, sTrackingID, "Paint");
                if (dt == null || dt.Rows.Count == 0)
                {
                    return null;
                }



                foreach (DataRow dr in dt.Rows)
                {
                    ViewModel.BuyoffReport_ViewModel.PaintDefect model = new ViewModel.BuyoffReport_ViewModel.PaintDefect();
                    model.materialNo = dr["materialpartNo"].ToString();

                    model.particle = double.Parse(dr["Particle"].ToString());
                    model.fibre = double.Parse(dr["Fibre"].ToString());
                    model.manyParticle = double.Parse(dr["Many particle"].ToString());
                    model.stainMark = double.Parse(dr["Stain mark"].ToString());
                    model.unevenPaint = double.Parse(dr["Uneven paint"].ToString());
                    model.underCoatUnevenPaint = double.Parse(dr["Under coat uneven paint"].ToString());
                    model.underSpray = double.Parse(dr["Under spray"].ToString());
                    model.whiteDot = double.Parse(dr["White dot"].ToString());
                    model.silverDot = double.Parse(dr["Silver dot"].ToString());
                    model.dust = double.Parse(dr["Dust"].ToString());
                    model.paintCrack = double.Parse(dr["Paint crack"].ToString());
                    model.bubble = double.Parse(dr["Bubble"].ToString());
                    model.scratch = double.Parse(dr["Scratch"].ToString());
                    model.abrasionMark = double.Parse(dr["Abrasion Mark"].ToString());
                    model.paintDripping = double.Parse(dr["Paint Dripping"].ToString());
                    model.roughSurface = double.Parse(dr["Rough Surface"].ToString());
                    model.shinning = double.Parse(dr["Shinning"].ToString());
                    model.matt = double.Parse(dr["Matt"].ToString());
                    model.paintPinHole = double.Parse(dr["Paint Pin Hole"].ToString());
                    model.lightLeakage = double.Parse(dr["Light Leakage"].ToString());
                    model.whiteMark = double.Parse(dr["White Mark"].ToString());
                    model.dented = double.Parse(dr["Dented"].ToString());
                    model.particleForLaserSetup = double.Parse(dr["Particle for laser setup"].ToString());
                    model.buyoff = double.Parse(dr["Buyoff"].ToString());
                    //model.qa = double.Parse(dr["QA"].ToString());

                    model.shortage = double.Parse(dr["Shortage"].ToString());

                    model.other = double.Parse(dr["Other"].ToString());


                    model.totalRej = double.Parse(dr["rejectQty"].ToString());

                    paintDefectList.Add(model);
                }


              
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("BuyoffController", "GetMaterialPaintDefectList" + ee.ToString());
            }
            return paintDefectList;
        }



        public List<ViewModel.BuyoffReport_ViewModel.LaserDefect> GetMaterialLaserDefectList(string sJobNo, string sTrackingID)
        {

            List<ViewModel.BuyoffReport_ViewModel.LaserDefect> laserDefectList = new List<ViewModel.BuyoffReport_ViewModel.LaserDefect>();
            try
            {

                DataTable dt = defectTracking.GetDefectDetail(sJobNo, sTrackingID, "Laser");
                if (dt == null || dt.Rows.Count == 0)
                {
                    return null;
                }



                foreach (DataRow dr in dt.Rows)
                {
                    ViewModel.BuyoffReport_ViewModel.LaserDefect model = new ViewModel.BuyoffReport_ViewModel.LaserDefect();
                    model.materialNo = dr["materialpartNo"].ToString();

                    model.blackMark = double.Parse(dr["Black Mark"].ToString());
                    model.blackDot = double.Parse(dr["Black Dot"].ToString());
                    model.graphicShiftCheckByPQC = double.Parse(dr["Graphic Shift check by PQC"].ToString());
                    model.graphicShiftCheckByMC = double.Parse(dr["Graphic Shift check by M/C"].ToString());
                    model.scratch = double.Parse(dr["Scratch"].ToString());
                    model.jagged = double.Parse(dr["Jagged"].ToString());
                    model.laserBubble = double.Parse(dr["Laser Bubble"].ToString());
                    model.doublOuterLine = double.Parse(dr["double outer line"].ToString());
                    model.pinHold = double.Parse(dr["Pin hold"].ToString());
                    model.poorLaser = double.Parse(dr["Poor Laser"].ToString());
                    model.burmMark = double.Parse(dr["Burm Mark"].ToString());
                    model.stainMark = double.Parse(dr["Stain Mark"].ToString());
                    model.graphicSmall = double.Parse(dr["Graphic Small"].ToString());
                    model.doubleLaser = double.Parse(dr["Double Laser"].ToString());
                    model.colorYellow = double.Parse(dr["Color Yellow"].ToString());
                    model.crack = double.Parse(dr["Crack"].ToString());
                    model.smoke = double.Parse(dr["Smoke"].ToString());
                    model.wrongOrientation = double.Parse(dr["Wrong Orientation"].ToString());
                    model.dented = double.Parse(dr["Dented"].ToString());
                    model.setup = double.Parse(dr["Setup"].ToString());
                    model.buyoff = double.Parse(dr["Buyoff"].ToString());
                    model.other = double.Parse(dr["Other"].ToString());

                    model.totalRej = double.Parse(dr["rejectQty"].ToString());

                    laserDefectList.Add(model);
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("BuyoffController", "GetMaterialPaintDefectList" + ee.ToString());
            }






            return laserDefectList;
        }



        public List<ViewModel.BuyoffReport_ViewModel.OthersDefect> GetMaterialOthersDefectList(string sJobNo, string sTrackingID)
        {
            List<ViewModel.BuyoffReport_ViewModel.OthersDefect> othersDefectList = new List<ViewModel.BuyoffReport_ViewModel.OthersDefect>();


            try
            {

           
                DataTable dt = defectTracking.GetDefectDetail(sJobNo, sTrackingID, "Others");
                if (dt == null || dt.Rows.Count == 0)
                {
                    return null;
                }



                foreach (DataRow dr in dt.Rows)
                {
                    ViewModel.BuyoffReport_ViewModel.OthersDefect model = new ViewModel.BuyoffReport_ViewModel.OthersDefect();
                    model.materialNo = dr["materialpartNo"].ToString();

                    model.pqcScratch = double.Parse(dr["PQC Scratch"].ToString());
            
                    model.overSpray = double.Parse(dr["Over Spray"].ToString());
                    model.bubble = double.Parse(dr["Bubble"].ToString());
                    model.oilStain = double.Parse(dr["Oil Stain"].ToString());
                    model.dragMark = double.Parse(dr["Drag Mark"].ToString());
                    model.lightLeakage = double.Parse(dr["Light Leakage"].ToString());
                    model.lightBubble = double.Parse(dr["Light Bubble"].ToString());
                    model.whiteDotInMaterial = double.Parse(dr["White Dot in Material"].ToString());
                    model.other = double.Parse(dr["Other"].ToString());
                    model.qa = double.Parse(dr["QA"].ToString());
                    model.totalRej = double.Parse(dr["rejectQty"].ToString());

                    othersDefectList.Add(model);
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("BuyoffController", "GetMaterialOthersDefectList" + ee.ToString());
            }

            return othersDefectList;
        }



        public List<ViewModel.BuyoffReport_ViewModel.PQCBuyoffList> GetPQCBuyoffList(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sJobNo)
        {

            DataTable dt = paintTempBLL.GetBuyoffList(dDateFrom, dDateTo, sPartNo, sJobNo);


            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }


            List<ViewModel.BuyoffReport_ViewModel.PQCBuyoffList> buyoffList = new List<ViewModel.BuyoffReport_ViewModel.PQCBuyoffList>();


            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.BuyoffReport_ViewModel.PQCBuyoffList model = new ViewModel.BuyoffReport_ViewModel.PQCBuyoffList();


                model.jobNo = dr["jobnumber"].ToString();
                model.materialNo = dr["materialName"].ToString();



            
                if (dr["paintingDate_3rd"].ToString() =="")
                    model.topDate = null;
                else
                    model.topDate = DateTime.Parse(dr["paintingDate_3rd"].ToString());
                model.topMachine = dr["pMachine_3rd"].ToString();
                model.topRunTime = dr["paintingRunningTime_3rd"].ToString();
                model.topOvenTime = dr["paintingOvenTime_3rd"].ToString();
                model.topPaintLot = dr["paintLot_3rd"].ToString();
                model.topThinnersLot = dr["thinnersLot_3rd"].ToString();
                model.topThichness = dr["thickness_3rd"].ToString();
                model.topPaintPIC = dr["paintingPIC_3rd"].ToString();




                if (dr["paintingDate_2nd"].ToString() == "")
                    model.middleDate = null;
                else
                    model.middleDate = DateTime.Parse(dr["paintingDate_2nd"].ToString());
                model.middleMachine = dr["pMachine_2nd"].ToString();
                model.middleRunTime = dr["paintingRunningTime_2nd"].ToString();
                model.middleOvenTime = dr["paintingOvenTime_2nd"].ToString();
                model.middlePaintLot = dr["paintLot_2nd"].ToString();
                model.middleThinnersLot = dr["thinnersLot_2nd"].ToString();
                model.middleThichness = dr["thickness_2nd"].ToString();
                model.middlePaintPIC = dr["paintingPIC_3rd"].ToString();




                if (dr["paintingDate_1st"].ToString() == "")
                    model.underDate = null;
                else
                    model.underDate = DateTime.Parse(dr["paintingDate_1st"].ToString());
                model.underMachine = dr["pMachine_1st"].ToString();
                model.underRunTime = dr["paintingRunningTime_1st"].ToString();
                model.underOvenTime = dr["paintingOvenTime_1st"].ToString();
                model.underPaintLot = dr["paintLot_1st"].ToString();
                model.underThinnersLot = dr["thinnersLot_1st"].ToString();
                model.underThichness = dr["thickness_1st"].ToString();
                model.underPaintPIC = dr["paintingPIC_1st"].ToString();




                model.temperatureFront = dr["temperatureFront"].ToString();
                model.temperatureRear = dr["temperatureRear"].ToString();
                model.humidityFront = dr["humidityFront"].ToString();
                model.humidityRear = dr["humidityRear"].ToString();



                model.humidityRear = dr["humidityRear"].ToString();


                model.dateTime = DateTime.Parse(dr["createdTime"].ToString());


                buyoffList.Add(model);
            }



            return buyoffList;
        }


    }
}