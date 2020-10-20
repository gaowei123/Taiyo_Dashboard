using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
    public class PQCQaViTracking_BLL
    {
        private  readonly Common.Class.DAL.PQCQaViTracking_DAL dal = new DAL.PQCQaViTracking_DAL();


        private Common.Class.Model.PQCQaViTracking dataRowToModel(DataRow dr)
        {

            Common.Class.Model.PQCQaViTracking model = new Model.PQCQaViTracking();

            model.id = int.Parse(dr["id"].ToString());
            model.machineID = dr["machineID"].ToString();

            model.partNumber = dr["partNumber"].ToString();
            model.jobId = dr["jobId"].ToString();
            model.processes = dr["processes"].ToString();
            model.jigNo = dr["jigNo"].ToString();
            model.model = dr["model"].ToString();
            model.cavityCount = dr["cavityCount"].ToString();
            model.cycleTime = dr["cycleTime"].ToString();
            model.targetQty = dr["targetQty"].ToString();
            model.userName = dr["userName"].ToString();
            model.userID = dr["userID"].ToString();
            model.TotalQty = dr["TotalQty"].ToString();
            model.rejectQty = dr["rejectQty"].ToString();
            model.acceptQty = dr["acceptQty"].ToString();
            model.startTime = DateTime.Parse(dr["startTime"].ToString());

            if (dr["stopTime"].ToString() != "")
            {
                model.stopTime = DateTime.Parse(dr["stopTime"].ToString());
            }
            else
            {
                model.stopTime = null;
            }


            model.nextViFlag = dr["nextViFlag"].ToString();
            model.day = DateTime.Parse(dr["day"].ToString());
            model.shift = dr["shift"].ToString();
            model.status = dr["status"].ToString();
            model.remark_1 = dr["remark_1"].ToString();
            model.remark_2 = dr["remark_2"].ToString();
            model.refField01 = dr["refField01"].ToString();
            model.refField02 = dr["refField02"].ToString();
            model.refField03 = dr["refField03"].ToString();
            model.refField04 = dr["refField04"].ToString();
            model.refField05 = dr["refField05"].ToString();
            model.refField06 = dr["refField06"].ToString();
            model.refField07 = dr["refField07"].ToString();
            model.refField08 = dr["refField08"].ToString();
            model.refField09 = dr["refField09"].ToString();
            model.refField10 = dr["refField10"].ToString();
            model.refField11 = dr["refField11"].ToString();
            model.refField12 = dr["refField12"].ToString();

            model.customer = dr["customer"].ToString();

            if (dr["lastUpdatedTime"].ToString() != "")
            {
                model.lastUpdatedTime = DateTime.Parse(dr["lastUpdatedTime"].ToString());
            }

            model.trackingID = dr["trackingID"].ToString();
            model.lastTrackingID = dr["lastTrackingID"].ToString();
            model.remarks = dr["remarks"].ToString();
            model.department = dr["department"].ToString();
            model.totalRejectQty = dr["totalRejectQty"].ToString();
            model.updatedTime = DateTime.Parse(dr["updatedTime"].ToString());


            return model;
        }

        public DataTable GetList(string sPartNumber, string sJobNumber, DateTime dDateFrom, DateTime dDateTo, string sShift, string sMachineType, string sLotNo, string sDateNotIn, string sTrackingID)
        {
            DataTable dt = dal.GetList(sPartNumber, sJobNumber, dDateFrom, dDateTo, sShift, sMachineType, sLotNo, sDateNotIn, sTrackingID);

            if (dt == null || dt.Rows.Count ==0)
            {
                return null;
            }

            double Total_NG = 0;
            double Total_OK = 0;
            double Total_Qty = 0;
            TimeSpan Total_Time = new TimeSpan();


            #region total dr
            DataRow dr = dt.NewRow();

            foreach (DataRow row in dt.Rows)
            {
                Total_NG += double.Parse(row["NG"].ToString());
                Total_OK += double.Parse(row["OK"].ToString());
                Total_Qty += double.Parse(row["Total"].ToString());



                if (row["Time"].ToString() != "")
                {
                    Total_Time = Total_Time + TimeSpan.Parse(row["Time"].ToString());
                }
               
            }


            dr["Shift"] = "Total :";
            dr["NG"] = Total_NG;
            dr["OK"] = Total_OK;
            dr["Output"] = Total_OK + Total_NG;
            dr["RejRate"] = Math.Round(Total_NG/(Total_OK + Total_NG)*100,2).ToString() + "%";
            dr["Total"] = Total_Qty;
            dr["Time"] = Total_Time;

            dt.Rows.Add(dr);
            #endregion


            return dt;
        }

        public int GetJobCheck1Output(string jobNo)
        {
            if (string.IsNullOrEmpty(jobNo))
            {
                return 0;
            }else
            {
                DataTable dt = dal.GetJobCheck1Output(jobNo);
                if (dt ==  null || dt.Rows.Count == 0)
                {
                    return 0;
                }

                return int.Parse(dt.Rows[0]["OK"].ToString());
            }
        }

        public int GetJobCheck2Output(string jobNo)
        {
            if (string.IsNullOrEmpty(jobNo))
            {
                return 0;
            }
            else
            {
                DataTable dt = dal.GetJobCheck2Output(jobNo);
                if (dt == null || dt.Rows.Count == 0)
                {
                    return 0;
                }

                return int.Parse(dt.Rows[0]["OK"].ToString());
            }
        }
        
   

        public DataTable GetSummaryReport(DateTime dDateFrom, DateTime dDateTo, string sShift, string sPartNo)
        {
           return  dal.GetSummaryReport(dDateFrom, dDateTo, sShift, sPartNo);
        }
        
        public Common.Class.Model.PQCQaViTracking GetModelByTrackingID(string sTrackingID)
        {
            if (sTrackingID == "")
                return null;

            DataTable dt = dal.GetModel(sTrackingID);




            Common.Class.Model.PQCQaViTracking model = dataRowToModel(dt.Rows[0]);


           
           

            return model;
        }
        
        public bool JobMaintenance(Common.Class.Model.PQCQaViTracking model, List<Common.Class.Model.PQCQaViDetailTracking_Model> listDetailModel, string User)
        {
            

            DBHelp.Reports.LogFile.Log("PQCJobMaintenance_Debug", "JobMaintenance ");

            bool result = false;
           
            List<SqlCommand> CMDList = new List<SqlCommand>();


            //PQCQaViTracking
            model.remark_1 = "Updated By " + User;
            CMDList.Add(dal.UpdateJob(model));

            //PQCQaViTracking his
            Common.Class.BLL.PQCQaViHistory_BLL trackingHisBLL = new PQCQaViHistory_BLL();
            Common.Class.Model.PQCQaViHistory_Model trackingHisModel = new Model.PQCQaViHistory_Model();
            trackingHisModel = CopyModel(GetModelByTrackingID(model.trackingID));
            CMDList.Add(trackingHisBLL.AddCommand(trackingHisModel));

           
            
            foreach (Common.Class.Model.PQCQaViDetailTracking_Model detailModel in listDetailModel)
            {
                //PQCQaViDetailTracking
                detailModel.remark_1 = "Updated By " + User;
                Common.Class.BLL.PQCQaViDetailTracking_BLL detialBLL = new PQCQaViDetailTracking_BLL();
                CMDList.Add(detialBLL.UpdateJob(detailModel));

                //PQCQaViDetailTracking is
                Common.Class.BLL.PQCQaViDetailHistory_BLL detailHisBLL = new PQCQaViDetailHistory_BLL();
                CMDList.Add(detailHisBLL.AddCommand(detailHisBLL.CopyObj(detailModel)));
            }

           

            //PQCQaViDefectTracking
            Common.Class.BLL.PQCQaViDefectTracking_BLL defectTrackingBLL = new PQCQaViDefectTracking_BLL();

            Common.Class.Model.PQCQaViDefectTracking_Model defectTrackingModel = new Model.PQCQaViDefectTracking_Model();
            defectTrackingModel.trackingID = model.trackingID;
            defectTrackingModel.remark_1 = "Updated By " + User;

            CMDList.Add(defectTrackingBLL.UpdateJob(defectTrackingModel));




            Common.Class.BLL.PQCQaViDefectHistory_BLL defectHisBLL = new PQCQaViDefectHistory_BLL();
            foreach (Common.Class.Model.PQCQaViDefectHistory_Model defectHisModel in defectHisBLL.GetModelListByTrackingID(model.trackingID))
            {
                defectHisModel.remark_1 = "Updated By " + User;
                defectHisModel.status = "End";
                defectHisModel.updatedTime = DateTime.Now;

                CMDList.Add(defectHisBLL.AddCommand(defectHisModel));
            }
            //PQCQaViDefectTracking


            DBHelp.Reports.LogFile.Log("PQCJobMaintenance_Debug", "JobMaintenance   cmd list count: "+CMDList.Count);

            try
            {
                result = DBHelp.SqlDB.SetData_Rollback(CMDList, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            }
            catch (Exception ee) {

            }
            

            return result;
        }
        
        public DataTable getProductivityReportForPQC(DateTime dDay, string sShift, string sDepartment)
        {
            //get manual key in data
            //Common.Class.DAL.TempProductivityData_DAL dal = new Common.Class.DAL.TempProductivityData_DAL();
            //DataTable dtProduct = dal.GetgetProductivtiyReportForTemp(sDepartment, dDay, sShift);


            DataTable dtProduct = dal.getProductivityReportForPQC(dDay, sShift);  //auto generate

           
            #region attendance 
            Common.BLL.LMMSWatchLog_BLL lmmswatchLogBLL = new Common.BLL.LMMSWatchLog_BLL();

            DataTable dt = lmmswatchLogBLL.getAttendance(dDay, sShift, sDepartment);

            double ManPower = 0;
            double Attendance = 0;
            string AttendRate = "";

            if (dt != null && dt.Rows.Count != 0)
            {
                ManPower = double.Parse(dt.Rows[0]["Man Power"].ToString());
                Attendance = double.Parse(dt.Rows[0]["Attendance"].ToString());
                AttendRate = ManPower == 0 ? "0%" : Math.Round(Attendance / ManPower * 100, 2).ToString() + "%";
            }
            #endregion


            #region foreach for total Row
            double Total_ActualTime = 0;

            double Total_ActualQty = 0;
            double Total_Qty = 0;
            //double Total_TargetQty = 0;

            double Total_LaserRej = 0;
            double Total_PaintRej = 0;
            double Total_MouldRej = 0;
            double Total_OthersRej = 0;



            foreach (DataRow dr in dtProduct.Rows)
            {
                Total_Qty += double.Parse(dr["TotalQty"].ToString());
                Total_ActualTime += double.Parse(dr["ActualHR"].ToString());
                Total_ActualQty += double.Parse(dr["ActualQty"].ToString());

                Total_LaserRej += double.Parse(dr["LaserRejCount"].ToString());
                Total_PaintRej += double.Parse(dr["PaintRejCount"].ToString());
                Total_MouldRej += double.Parse(dr["MouldRejCount"].ToString());
                Total_OthersRej += double.Parse(dr["OthersRejCount"].ToString());
                
                //Total_TargetQty += double.Parse(dr["TargetQty"].ToString());


                //累加之后, 转换格式
                dr["ActualHR"] = Common.CommFunctions.ConvertDateTimeShort(dr["ActualHR"].ToString());
            }

            
          



            //dr total 
            DataRow dr_total = dtProduct.NewRow();
            dr_total["SN"] = GetSN("Day Shift Total");
            dr_total["ProductType"] = sShift == StaticRes.Global.Shift.Day ? "Day Shift Total" : "Night Shift Total";

            dr_total["ManPower"] = ManPower;
            dr_total["Attendance"] = Attendance;
            dr_total["AttendRate"] = AttendRate;

            dr_total["TargetHR"] = "";
            dr_total["ActualHR"] = Common.CommFunctions.ConvertDateTimeShort(Total_ActualTime.ToString());

            dr_total["ActualQty"] = Total_ActualQty;
            dr_total["TotalQty"] = Total_Qty;
            dr_total["TargetQty"] = "";
            

            dr_total["LaserRejCount"] = Total_LaserRej;
            dr_total["PaintRejCount"] = Total_PaintRej;
            dr_total["MouldRejCount"] = Total_MouldRej;
            dr_total["OthersRejCount"] = Total_OthersRej;


            double Total_RejQty = Total_LaserRej + Total_PaintRej + Total_MouldRej + Total_OthersRej;
            double TotalRejRate = Total_ActualQty == 0 ? 0.00 : Math.Round(Total_RejQty / (Total_ActualQty + Total_RejQty) * 100, 2);
            dr_total["RejRate"] = TotalRejRate.ToString() + "%";


            dtProduct.Rows.Add(dr_total);

            #endregion


            #region add all type
            List<string> listType = new List<string>();

            listType.Add("LASER");
            listType.Add("WIP");
            listType.Add("784");
            listType.Add("824");
            listType.Add("833");
            listType.Add("452");
            listType.Add("595");
            listType.Add("830");
            listType.Add("831");



            foreach (string type in listType)
            {
                DataRow[] drArr = dtProduct.Select(" ProductType = '" + type + "'");
                if (drArr.Length == 0)
                {
                    DataRow dr = dtProduct.NewRow();
                    #region auto
                    dr["SN"] = GetSN(type);
                    dr["ProductType"] = type;
                    dr["ActualQty"] = 0;

                    dr["TotalQty"] = 0;
                    dr["ActualHR"] = Common.CommFunctions.ConvertDateTimeShort("0");
                    dr["TargetQty"] = 0;
                    dr["TargetHR"] = 0;

                    dr["ManPower"] = ManPower;
                    dr["Attendance"] = Attendance;
                    dr["AttendRate"] = AttendRate;

                    dr["LaserRejCount"] = 0;
                    dr["PaintRejCount"] = 0;
                    dr["MouldRejCount"] = 0;
                    dr["OthersRejCount"] = 0;

                    dr["RejRate"] = "0.00%";
                    #endregion

                    #region manual
                    //dr["SN"] = GetSN(type);
                    //dr["ProductType"] = type;
                    //dr["ActualQty"] = "";

                    //dr["TotalQty"] = "";
                    //dr["ActualHR"] = "";
                    //dr["TargetQty"] = "";
                    //dr["TargetHR"] = "";

                    //dr["ManPower"] = ManPower;
                    //dr["Attendance"] = Attendance;
                    //dr["AttendRate"] = AttendRate;

                    //dr["LaserRejCount"] = "";
                    //dr["PaintRejCount"] = "";
                    //dr["MouldRejCount"] = "";
                    //dr["VendorRejCount"] = "";
                    //dr["PrintRejCount"] = "";

                    //dr["RejRate"] = "";
                    #endregion

                    dtProduct.Rows.Add(dr.ItemArray);
                }
                else
                {
                    drArr[0]["ManPower"] = ManPower;
                    drArr[0]["Attendance"] = Attendance;
                    drArr[0]["AttendRate"] = AttendRate;

                    //drArr[0]["TargetHR"] = Common.CommFunctions.ConvertDateTimeShort(drArr[0]["TargetHR"].ToString());
                    //drArr[0]["ActualHR"] = Common.CommFunctions.ConvertDateTimeShort(drArr[0]["ActualHR"].ToString());

                }
            }
            #endregion


            //order by sn
            dtProduct = dtProduct.Select("", "SN asc").CopyToDataTable();


            return dtProduct;
        }
 
        
        public DataTable GetVIDetailForButtonReport_NEW(string strWhere)
        {

            DataTable dtViDetailTracking = new DataTable();
            dtViDetailTracking = dal.GetVIDetailForButtonReport_NEW(strWhere);
            
            if (dtViDetailTracking == null || dtViDetailTracking.Rows.Count == 0)
                return null;
            else
                return dtViDetailTracking;
        }

        public DataTable GetAllDisplayJobs(DateTime dDateFrom, DateTime dDateTo, string sDescription, string sPartNumber, string sJobNo, string sModel, string sSupplier, string sColor, string sCoating)
        {
            return dal.GetAllDisplayJobs(dDateFrom, dDateTo, sDescription,sPartNumber, sJobNo, sModel, sSupplier, sColor, sCoating);
        }
        
        private int GetSN(string type)
        {
            int SN = 0;
            switch (type)
            {
                case "LASER":
                    SN = 1;
                    break;
                case "WIP":
                    SN = 2;
                    break;
                case "784":
                    SN = 3;
                    break;
                case "824":
                    SN = 4;
                    break;
                case "833":
                    SN = 5;
                    break;
                case "452":
                    SN = 6;
                    break;
                case "595":
                    SN = 7;
                    break;
                case "830":
                    SN = 8;
                    break;
                case "831":
                    SN = 9;
                    break;
              
                case "Day Shift Total":
                    SN = 99;
                    break;
                case "Night Shift Total":
                    SN = 99;
                    break;


            }
            return SN;
        }
        
        public DataTable getBezelPanelReport(DateTime dDateFrom, DateTime dDateTo, string sType, string sDescription, string sNumber, DateTime? dPaintingDate, string sPIC, DateTime? dMFGDate)
        {
            DataTable dt = dal.getBezelPanelReport(dDateFrom, dDateTo, sType, sDescription,sNumber,  dPaintingDate, sPIC, dMFGDate);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return dt;
            }
        }
        
        public DataTable GetCheckingDailyList(DateTime dDateFrom, DateTime dDateTo, string sShift, string sPartNumber, string sStation, string sPIC, string sType)
        {
            DataTable dt = dal.GetCheckingDailyList( dDateFrom, dDateTo, sShift, sPartNumber, sStation, sPIC, sType);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }


            //dt paint delivery  避免找不到, 放宽4个月
            Common.Class.BLL.PaintingDeliveryHis_BLL paintBLL = new PaintingDeliveryHis_BLL();
            DataTable dtPaint = paintBLL.GetList(dDateFrom.AddMonths(-4), dDateTo,"");
            

       
            foreach (DataRow dr in dt.Rows)
            {
                double materialCount = dr["MaterialCount"].ToString() == "" ? 1 : double.Parse(dr["MaterialCount"].ToString());

                //合并 Lot no, mrp qty
                string jobnumber = dr["jobnumber"].ToString();
                DataRow[] drArr = dtPaint.Select("Jobnumber = '" + jobnumber + "'");
                if (drArr.Length > 0)
                {
                    dr["lotNo"] = drArr[0]["lotNo"].ToString();

                    double MrpQty = double.Parse(drArr[0]["InQuantity"].ToString());
                   

                    dr["MrpQtyPcs"] = MrpQty * materialCount ;
                    dr["MrpQtySet"] = MrpQty;
                }
            }
            

            return dt;
        }


        public DataTable GetPackingDailyList(DateTime dDateFrom, DateTime dDateTo, string sShift, string sPartNumber, string sStation, string sPIC)
        {
            DataTable dt = dal.GetPackingDailyList(dDateFrom, dDateTo, sShift, sPartNumber, sStation, sPIC);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }


            //dt paint delivery  避免找不到, 放宽60天查找.
            Common.Class.BLL.PaintingDeliveryHis_BLL paintBLL = new PaintingDeliveryHis_BLL();
            DataTable dtPaint = paintBLL.GetList(dDateFrom.AddDays(-60), dDateTo, "");

            

            foreach (DataRow dr in dt.Rows)
            {
                double materialCount = dr["MaterialCount"].ToString() == "" ? 1 : double.Parse(dr["MaterialCount"].ToString());

                //合并 Lot no, mrp qty
                string jobnumber = dr["jobnumber"].ToString();
                DataRow[] drArr = dtPaint.Select("Jobnumber = '" + jobnumber + "'");
                if (drArr.Length > 0)
                {
                    dr["lotNo"] = drArr[0]["lotNo"].ToString();

                    double MrpQty = double.Parse(drArr[0]["InQuantity"].ToString());



                    dr["MrpQtyPcs"] = MrpQty * materialCount;
                    dr["MrpQtySet"] = MrpQty;
                }
            }

            return dt;
        }


        public DataTable GetPICReport(DateTime dDateFrom, DateTime dDateTo, string sShift, string sJobNo, string sPartNo, string sPIC)
        {
            DataSet ds = dal.GetPICReport(dDateFrom, dDateTo, sShift, sJobNo, sPartNo, sPIC);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }



            DataTable dt = ds.Tables[0];




            double totalHours = 0;
            double totalMRPQtySET = 0;
            double totalMRPQtyPCS = 0;
            double totalOutputSET = 0;
            double totalOutputPCS = 0;

            foreach (DataRow dr in dt.Rows)
            {
                double totalSeconds = double.Parse(dr["TotalSeconds"].ToString());
                dr["TotalSeconds"] = Common.CommFunctions.ConvertDateTimeShort((totalSeconds / 3600).ToString());


                string sMRPQty = dr["MRPQty"].ToString();
                string sOutput = dr["Output"].ToString();
                totalMRPQtySET += double.Parse(sMRPQty.Split('(')[0]);
                totalMRPQtyPCS += double.Parse(sMRPQty.Split('(')[1].Trim(')'));
                totalOutputSET += double.Parse(sOutput.Split('(')[0]);
                totalOutputPCS += double.Parse(sOutput.Split('(')[1].Trim(')'));
                totalHours += totalSeconds / 3600;
            }


            DataRow drTotal = dt.NewRow();

            drTotal[1] = "Total";
            drTotal["MRPQty"] = string.Format("{0}({1})", totalMRPQtySET, totalMRPQtyPCS);
            drTotal["LaserBtn"] = dt.Compute("SUM(LaserBtn)", "").ToString();
            drTotal["WIPBtn"] = dt.Compute("SUM(WIPBtn)", "").ToString();
            drTotal["Packing"] = dt.Compute("SUM(Packing)", "").ToString();
            drTotal["MouldingRej"] = dt.Compute("SUM(MouldingRej)", "").ToString();
            drTotal["PaintingRej"] = dt.Compute("SUM(PaintingRej)", "").ToString();
            drTotal["LaserRej"] = dt.Compute("SUM(LaserRej)", "").ToString();
            drTotal["OthersRej"] = dt.Compute("SUM(OthersRej)", "").ToString();
            drTotal["totalRej"] = dt.Compute("SUM(totalRej)", "").ToString();
            drTotal["Output"] = string.Format("{0}({1})", totalOutputSET, totalOutputPCS);
            drTotal["TotalSeconds"] = Common.CommFunctions.ConvertDateTimeShort(totalHours.ToString());
            

            dt.Rows.Add(drTotal);




            return dt;
        }
        
        public DataTable GetProductionChart(string sReportType, DateTime dDateFrom, DateTime dDateTo, int iYear, string sPartNo, string sModel, string sCustomer, string sPIC)
        {
            DataTable dtOutput = new DataTable();
            if (sReportType == "Yearly")
            {
                dtOutput = dal.GetYearlyProduct(sPartNo, sModel, sCustomer, sPIC);
            }
            else if (sReportType == "Monthly")
            {
                dtOutput = dal.GetMonthlyProduct(iYear, sPartNo, sModel, sCustomer, sPIC);
            }
            else if (sReportType == "Daily")
            {
                dtOutput = dal.GetDailyProduct(sPartNo, sModel, sCustomer, sPIC, dDateFrom, dDateTo);
            }
            else if (sReportType == "PartNumber" || sReportType == "Model" || sReportType == "Customer" || sReportType == "PIC" || sReportType == "Type")
            {
                dtOutput = dal.GetProductGroupByReportType(sReportType, sPartNo, sModel, sCustomer, sPIC, dDateFrom, dDateTo);
            }
            else if (sReportType == "Rejection")
            {
                dtOutput = dal.GetProductGroupByReject(sPartNo, sModel, sCustomer, sPIC, dDateFrom, dDateTo);
            }


            if (dtOutput == null || dtOutput.Rows.Count == 0)
            {
                return null;
            }
            

            return dtOutput;
        }
        
        public DataTable GetCheckerOutput(string sType, DateTime dDateFrom, DateTime dDateTo)
        {

            DataTable dt = dal.GetCheckerOutput(sType, dDateFrom, dDateTo);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }


            return dt;
        }
        
        public Common.Class.Model.PQCQaViHistory_Model CopyModel(Common.Class.Model.PQCQaViTracking model)
        {

            Common.Class.Model.PQCQaViHistory_Model hisModel = new Model.PQCQaViHistory_Model();


            hisModel.id = model.id;
            hisModel.machineID = model.machineID;
            hisModel.partNumber = model.partNumber;
            hisModel.jobid = model.jobId;
            hisModel.processes = model.processes;
            hisModel.jigNo = model.jigNo;
            hisModel.model = model.model;
            hisModel.cavityCount = decimal.Parse(model.cavityCount);
            hisModel.cycleTime = decimal.Parse(model.cycleTime);
            hisModel.targetQty = decimal.Parse(model.targetQty);
            hisModel.userName = model.userName;
            hisModel.userID = model.userID;
            hisModel.TotalQty = decimal.Parse(model.TotalQty);
            hisModel.rejectQty = decimal.Parse(model.rejectQty);
            hisModel.acceptQty = decimal.Parse(model.acceptQty);
            hisModel.startTime = model.startTime;
            hisModel.stopTime = model.stopTime;
            hisModel.nextViFlag = model.nextViFlag;
            hisModel.day = model.day;
            hisModel.shift = model.shift;
            hisModel.status = model.status;


            hisModel.remark_1 = model.remark_1;
            hisModel.remark_2 = model.remark_2;
            hisModel.refField01 = model.refField01;
            hisModel.refField02 = model.refField02;
            hisModel.refField03 = model.refField03;
            hisModel.refField04 = model.refField04;
            hisModel.refField05 = model.refField05;
            hisModel.refField06 = model.refField06;
            hisModel.refField07 = model.refField07;
            hisModel.refField08 = model.refField08;
            hisModel.refField09 = model.refField09;
            hisModel.refField10 = model.refField10;
            hisModel.refField11 = model.refField11;
            hisModel.refField12 = model.refField12;

            hisModel.customer = model.customer;
            hisModel.lastUpdatedTime = model.lastUpdatedTime;
            hisModel.trackingID = model.trackingID;
            hisModel.lastTrackingID = model.lastTrackingID;
            hisModel.remarks = model.remarks;
            hisModel.department = model.department;
            hisModel.TotalRejectQty = model.totalRejectQty==""? 0:  decimal.Parse(model.totalRejectQty);
            hisModel.updatedTime = model.updatedTime;

            return hisModel;
        }
        
        
        
        public DataTable GetRealTimeData(string sType)
        {

            DateTime day = DateTime.Now.AddHours(-8).Date;

            //day = DateTime.Parse("2019-12-10");
         

            DataTable dt = new DataTable();

            if (sType == "Online")
            {
                dt = dal.GetRealTimeForOnline(day);
            }
            else if (sType == "WIP")
            {
                dt = dal.GetRealTimeForWIP(day);
            }
            else if (sType =="Packing")
            {
                dt = dal.GetRealTimeForPack(day);
            }
            else
            {
                dt = null;
            }

            return dt;
        }
        
        public DataTable GetOnlineDayOutput(DateTime dDay)
        {
            DataTable dt = dal.GetOnlineDayOutput(dDay);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            return dt;
        }

        public DataTable GetWIPDayOutput(DateTime dDay)
        {
            DataTable dt = dal.GetWIPDayOutput(dDay);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            return dt;
        }





        public DataTable GetRealTime()
        {
            return dal.GetRealTime();
        }
        

        public bool MaintenanceUpdateEndFlag(string sTrackingID, bool bEndFlag)
        {

            //获取vitracking model
            Common.Class.Model.PQCQaViTracking viModel = new Model.PQCQaViTracking();
            viModel = GetModelByTrackingID(sTrackingID);
            
            //更新 job状态
            viModel.status = "End";
            viModel.nextViFlag = bEndFlag.ToString();
            viModel.lastUpdatedTime = DateTime.Now;
            viModel.remarks = "PQC Maintenance  End Job";

            


            //获取vi detail tracking model
            Common.Class.BLL.PQCQaViDetailTracking_BLL detailBLL = new PQCQaViDetailTracking_BLL();
            List<Common.Class.Model.PQCQaViDetailTracking_Model> detailModelList = new List<Model.PQCQaViDetailTracking_Model>();
            detailModelList = detailBLL.GetModelList(sTrackingID, "", null, null);

            foreach (Model.PQCQaViDetailTracking_Model detailModel in detailModelList)
            {
                //更新 job状态
                detailModel.status = "End";
                detailModel.lastUpdatedTime = DateTime.Now;
                detailModel.remarks = "PQC Maintenance  End Job";
            }

                  



            //复制之前的逻辑, 只不过数量不变.
            List<SqlCommand> cmdList = new List<SqlCommand>();


            //update vi tracking model
            cmdList.Add(dal.UpdatePQCMaintenance(viModel));



            //add vi tracking his
            Common.DAL.PQCQaViHistory_DAL viHisDAL = new Common.DAL.PQCQaViHistory_DAL();
            cmdList.Add(viHisDAL.AddCommand(CopyModel(viModel)));




            //update detail tracking model
            Common.DAL.PQCQaViDetailTracking_DAL detailTrackDAL = new Common.DAL.PQCQaViDetailTracking_DAL();
            Common.Class.BLL.PQCQaViDetailHistory_BLL detailHisBLL = new PQCQaViDetailHistory_BLL();
            foreach (var model in detailModelList)
            {
                cmdList.Add(detailTrackDAL.UpdatePQCMaintenance(model));

                cmdList.Add(detailHisBLL.AddCommand(detailHisBLL.CopyObj(model)));
            }





            return DBHelp.SqlDB.SetData_Rollback(cmdList, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }

        public bool MaintenanceUpdateQty(Common.Class.Model.PQCQaViTracking trackingModel, 
            List<Common.Class.Model.PQCQaViDetailTracking_Model> detailModelList, 
            List<Model.PQCQaViDefectTracking_Model> defectModelList,
            List<Common.Class.Model.PQCQaViBinning> binList, 
            List<Common.Class.Model.PQCQaViBinHistory_Model> binHisList,
            bool isUpdateBin)
        {
            List<SqlCommand> cmdList = new List<SqlCommand>();


            //update vi tracking model
            cmdList.Add(dal.UpdatePQCMaintenance(trackingModel));



            //add vi tracking his
            Common.DAL.PQCQaViHistory_DAL viHisDAL = new Common.DAL.PQCQaViHistory_DAL();
            cmdList.Add(viHisDAL.AddCommand(CopyModel(trackingModel)));




            //update detail tracking model
            Common.DAL.PQCQaViDetailTracking_DAL detailTrackDAL = new Common.DAL.PQCQaViDetailTracking_DAL();
            Common.Class.BLL.PQCQaViDetailHistory_BLL detailHisBLL = new PQCQaViDetailHistory_BLL();
            foreach (var model in detailModelList)
            {
                cmdList.Add(detailTrackDAL.UpdatePQCMaintenance(model));

                cmdList.Add(detailHisBLL.AddCommand(detailHisBLL.CopyObj(model)));
            }



            //update defect tracking model
            Common.Class.DAL.PQCQaViDefectTracking_DAL defectDAL = new DAL.PQCQaViDefectTracking_DAL();
            Common.Class.BLL.PQCQaViDefectHistory_BLL defectHisBLL = new Common.Class.BLL.PQCQaViDefectHistory_BLL();
            foreach (var model in defectModelList)
            {
                cmdList.Add(defectDAL.UpdateCommand(model));
                cmdList.Add(defectHisBLL.AddCommand(defectHisBLL.CopyObj(model)));
            }



            //update vi bin 
            Common.Class.BLL.PQCQaViBinning binBLL = new PQCQaViBinning();
            foreach (var binModel in binList)
            {
                SqlCommand binCMD = isUpdateBin ? binBLL.UpdateCommand(binModel) : binBLL.AddCommand(binModel);
                cmdList.Add(binCMD);
            }


            Common.Class.BLL.PQCQaViBinHistory_BLL binHisBLL = new PQCQaViBinHistory_BLL();
            foreach (var binHisModel in binHisList)
            {
                cmdList.Add(binHisBLL.AddCommand(binHisModel));
            }
            



            return DBHelp.SqlDB.SetData_Rollback(cmdList, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }





        public List<string> GetBuyoffJobList(DateTime? dDate)
        {
            DataTable dt = dal.GetBuyoffJobList(dDate);

            if (dt == null || dt.Rows.Count  == 0)
            {
                return null;
            }




            List<string> jobList = new List<string>();

            foreach (DataRow dr in dt.Rows)
            {
                string jobId = dr["JobId"].ToString();

                jobList.Add(jobId);

            }



            return jobList;
        }

        public DataTable GetWIPOutputForAllInventoryReport(DateTime dStartTime)
        {
            return dal.GetWIPOutputForAllInventoryReport(dStartTime);
        }


        public DataTable GetCheckingDetailList(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sStation, string sPIC, string sJobNo)
        {
            return dal.GetCheckingDetailList(dDateFrom, dDateTo, sPartNo, sStation, sPIC, sJobNo);
        }


        public DateTime GetCheckingDateByJob(string sJobNo) {

            DataTable dt = dal.GetCheckingDateByJob(sJobNo);
            string sDay = dt.Rows[0]["Day"].ToString();
            return DateTime.Parse(sDay);
        }
            



        public DataTable GetDailyOperatorList(DateTime dDate, string sShift, string sUserID)
        {

            return dal.GetDailyOperatorList(dDate, sShift, sUserID);


        }


        public List<Common.Class.Model.PQCQaViTracking> GetModelList(DateTime? dDateFrom, DateTime? dDateTo, string sJobNo, string sProcess)
        {
            DataTable dt = dal.GetList(dDateFrom, dDateTo, sJobNo, sProcess);
            if (dt == null || dt.Rows.Count == 0)
                return null;


            List<Common.Class.Model.PQCQaViTracking> modelList = new List<Model.PQCQaViTracking>();
            foreach (DataRow dr in dt.Rows)
            {
                modelList.Add(dataRowToModel(dr));
            }

            return modelList;
        }

        public bool UpdateQASetupForWipPart(string sTrackingID, int iQa, int iSetup)
        {

            Common.DAL.PQCQaViDetailTracking_DAL viDetailTracking = new Common.DAL.PQCQaViDetailTracking_DAL();
            Common.Class.BLL.PQCQaViDetailTracking_BLL detailBLL = new PQCQaViDetailTracking_BLL();



            //处理 vi detail tracking 
            List<Common.Class.Model.PQCQaViDetailTracking_Model> detailList = detailBLL.GetModelList(sTrackingID, "", null, null);
            foreach (var detailModel in detailList)
            {
                detailModel.totalQty = detailModel.totalQty - iQa - iSetup;
                detailModel.passQty = detailModel.passQty - iQa - iSetup;

                detailModel.remarks = "Updated by buyoff record";
                detailModel.lastUpdatedTime = DateTime.Now;
                detailModel.updatedTime = DateTime.Now;
            }


            //处理 vi tracking
            Common.Class.Model.PQCQaViTracking viModel = GetModelByTrackingID(sTrackingID);
            viModel.TotalQty = (int.Parse(viModel.TotalQty) - (iQa + iSetup) * detailList.Count()).ToString();
            viModel.acceptQty = (int.Parse(viModel.acceptQty) - (iQa + iSetup) * detailList.Count()).ToString();

            viModel.remarks = "Updated by buyoff record";
            viModel.lastUpdatedTime = DateTime.Now;
            viModel.updatedTime = DateTime.Now;


            

            List<SqlCommand> cmdList = new List<SqlCommand>();

            cmdList.Add(dal.UpdateForQASetup(viModel));

            foreach (var detailModel in detailList)
            {
                cmdList.Add(viDetailTracking.UpdateForQASetup(detailModel));
            }


            //add vi tracking his
            Common.DAL.PQCQaViHistory_DAL viHisDAL = new Common.DAL.PQCQaViHistory_DAL();
            cmdList.Add(viHisDAL.AddCommand(CopyModel(viModel)));

            //add detail tracking his
            Common.Class.BLL.PQCQaViDetailHistory_BLL detailHisBLL = new PQCQaViDetailHistory_BLL();
            foreach (var model in detailList)
            {
                cmdList.Add(detailHisBLL.AddCommand(detailHisBLL.CopyObj(model)));
            }





            //update vi bin 
            Common.Class.BLL.PQCQaViBinning binBLL = new PQCQaViBinning();
            Common.Class.BLL.PQCQaViBinHistory_BLL binHisBLL = new PQCQaViBinHistory_BLL();

            List<Common.Class.Model.PQCQaViBinning> viBinList = binBLL.GetModelList(null, null, viModel.jobId, viModel.processes);
            foreach (var binModel in viBinList)
            {
                binModel.materialQty = binModel.materialQty - iQa - iSetup;
                binModel.updatedTime = DateTime.Now;
                binModel.remarks = "Updated by buyoff record";
                cmdList.Add(binBLL.UpdateCommand(binModel));



                Common.Class.Model.PQCQaViBinHistory_Model binHisModel = new Common.Class.Model.PQCQaViBinHistory_Model();
                binHisModel = binHisBLL.CopyModel(binModel);
                cmdList.Add(binHisBLL.AddCommand(binHisModel));
            }
         
         


            return DBHelp.SqlDB.SetData_Rollback(cmdList, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);

        }




        public bool DeleteJobRollBack(string sJobNo)
        {

            if (string.IsNullOrEmpty(sJobNo))
                return false;


            List<SqlCommand> cmdList = new List<SqlCommand>();


            cmdList.Add(dal.DeleteJobCommand(sJobNo));


            Common.DAL.PQCQaViDetailTracking_DAL detailDAL = new Common.DAL.PQCQaViDetailTracking_DAL();
            cmdList.Add(detailDAL.DeleteJobCommand(sJobNo));


            Common.Class.DAL.PQCQaViDefectTracking_DAL defectDAL = new DAL.PQCQaViDefectTracking_DAL();
            cmdList.Add(defectDAL.DeleteJobCommand(sJobNo));


            Common.Class.DAL.PQCQaViBinning binDAL = new DAL.PQCQaViBinning();
            cmdList.Add(binDAL.DeleteJobCommand(sJobNo));



            return DBHelp.SqlDB.SetData_Rollback(cmdList, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }
    }
}
