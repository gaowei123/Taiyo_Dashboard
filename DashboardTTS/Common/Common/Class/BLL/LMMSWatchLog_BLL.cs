using System.Text.RegularExpressions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Common.Model;
namespace Common.BLL
{
	/// <summary>
	/// LMMSWatchLog_BLL
	/// </summary>
	public class LMMSWatchLog_BLL
	{
		private readonly Common.DAL.LMMSWatchLog_DAL dal=new Common.DAL.LMMSWatchLog_DAL();
		public LMMSWatchLog_BLL()
		{}
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string sJobNumber)
		{
            if (sJobNumber.Trim() == "")
            {
                return false;
            }

			DataSet ds = new DataSet();
			ds = dal.Exists(sJobNumber);
			if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}




        public DataTable GetMaterialList(string sDay, string sShift, DateTime dDateFrom, DateTime dDateTo, string sJobnumber, string sMachineID, string sPartNumber, string sModule)
        {
            Common.Class.BLL.LMMSInventoty_BLL InventoryBLL = new Common.Class.BLL.LMMSInventoty_BLL();

            //1. get all job list
            DataTable dt_JobList = GetJobList(sDay, sShift, dDateFrom, dDateTo, sJobnumber, sMachineID, sPartNumber, sModule);

            if (dt_JobList == null || dt_JobList.Rows.Count == 0)
                return null;


            #region material main datatable structure
            DataTable dt_MaterialList = new DataTable();
            dt_MaterialList.Columns.Add("machineID");
            dt_MaterialList.Columns.Add("shift");
            dt_MaterialList.Columns.Add("customer");
            dt_MaterialList.Columns.Add("module");
            dt_MaterialList.Columns.Add("jobNumber");
            dt_MaterialList.Columns.Add("partNumber");
            dt_MaterialList.Columns.Add("StartTime");
            dt_MaterialList.Columns.Add("EndTime");

            dt_MaterialList.Columns.Add("machineID_temp");
            dt_MaterialList.Columns.Add("shift_temp");
            dt_MaterialList.Columns.Add("customer_temp");
            dt_MaterialList.Columns.Add("module_temp");
            dt_MaterialList.Columns.Add("jobNumber_temp");
            dt_MaterialList.Columns.Add("partNumber_temp");
            dt_MaterialList.Columns.Add("StartTime_temp");
            dt_MaterialList.Columns.Add("EndTime_temp");

            dt_MaterialList.Columns.Add("materialPart");
            dt_MaterialList.Columns.Add("NG");
            dt_MaterialList.Columns.Add("OK");
            dt_MaterialList.Columns.Add("output");
            dt_MaterialList.Columns.Add("NGRate");
          
            dt_MaterialList.Columns.Add("UpdatedBy");
            #endregion


            //2. foreach 
            foreach (DataRow row in dt_JobList.Rows)
            {
                if (row["MachineID"].ToString() == "Total :")
                    continue;

                //2.1 get each row para.
                string Job_MachineID = row["MachineID"].ToString();
                string Job_Day = row["day"].ToString();
                string Job_Shift = row["shift"].ToString();
                string Job_Jobnumber = row["JobNumber"].ToString();
                string Job_PartNumber = row["partnumber"].ToString();


                //2.2 **Main Logic**  material segment  
                DataTable dt_MaterialListForSingleJob = JobMaterialDetail(Job_Day, Job_Shift, dDateFrom, dDateTo, Job_Jobnumber, Job_MachineID, Job_PartNumber);

                if (dt_MaterialListForSingleJob != null)
                {
                    dt_MaterialList.Merge(dt_MaterialListForSingleJob, false, MissingSchemaAction.AddWithKey);
                }
            }


            //sort
            DataRow[] drArry = dt_MaterialList.Select("", "MachineID asc, partnumber asc");
            DataTable dt_MaterialListAfterSort = Common.CommFunctions.DataRowToDataTable(drArry);
            //sort



            #region 3. add total summary row 
            double Count_OK = 0;
            double Count_NG = 0;


            foreach (DataRow dr in dt_MaterialListAfterSort.Rows)
            {
                try
                {
                    Count_OK += double.Parse(dr["OK"].ToString());
                }
                catch { Count_OK += 0; }

                try
                {
                    Count_NG += double.Parse(dr["NG"].ToString());
                }
                catch
                {
                    Count_NG += 0;
                }
            }

            DataRow dr_total = dt_MaterialListAfterSort.NewRow();
            dr_total["machineID_temp"] = "Total :";
            dr_total["OK"] = Count_OK;
            dr_total["NG"] = Count_NG;
            dr_total["output"] = Count_OK + Count_NG;
            dr_total["NGRate"] = Math.Round(Count_NG / (Count_OK + Count_NG) * 100, 2).ToString() + "%";

            dt_MaterialListAfterSort.Rows.Add(dr_total);
            #endregion


            return dt_MaterialListAfterSort;
        }

        


        public DataTable GetJobList(string sDay, string sShift, DateTime dDateFrom, DateTime dDateTo, string sJobnumber, string sMachineID, string sPartNumber, string sModule)
        {

            DataSet ds = dal.GetJobList(sDay, sShift, dDateFrom, dDateTo, sJobnumber, sMachineID, sPartNumber, sModule);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            return ds.Tables[0];
        }


        public DataTable GetJobNG(DateTime dDateFrom, DateTime dDateTo)
        {

            DataSet ds = dal.GetJobNG(dDateFrom, dDateTo);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            return ds.Tables[0];
        }




        public DataTable JobMaterialDetail(string sDay, string sShift, DateTime dDateFrom, DateTime dDateTo, string sJobnumber, string sMachineID, string sPartNumber)
        {
            Common.Class.BLL.LMMSUserEventLog_BLL bll = new Class.BLL.LMMSUserEventLog_BLL();
            DataSet ds = dal.getNGByJobnumber(sDay,sShift,dDateFrom,dDateTo,sJobnumber,sMachineID,sPartNumber);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            DataRow dr_job = ds.Tables[0].Rows[0];


            #region material datatable structure
            DataTable dt_Material = new DataTable();
            dt_Material.Columns.Add("machineID");
            dt_Material.Columns.Add("shift");
            dt_Material.Columns.Add("customer");
            dt_Material.Columns.Add("module");
            dt_Material.Columns.Add("jobNumber");
            dt_Material.Columns.Add("partNumber");
            dt_Material.Columns.Add("StartTime");
            dt_Material.Columns.Add("EndTime");

            dt_Material.Columns.Add("machineID_temp");
            dt_Material.Columns.Add("shift_temp");
            dt_Material.Columns.Add("customer_temp");
            dt_Material.Columns.Add("module_temp");
            dt_Material.Columns.Add("jobNumber_temp");
            dt_Material.Columns.Add("partNumber_temp");
            dt_Material.Columns.Add("StartTime_temp");
            dt_Material.Columns.Add("EndTime_temp");

            dt_Material.Columns.Add("materialPart");
            dt_Material.Columns.Add("NG");
            dt_Material.Columns.Add("OK");
            dt_Material.Columns.Add("output");
            dt_Material.Columns.Add("NGRate");
            dt_Material.Columns.Add("UpdatedBy");
            #endregion


            string UpdatedBy = "";
            double NGForMaterial = 0;
            double OKForMaterial = 0;

            if (sMachineID == "1" || sMachineID == "2" || sMachineID == "3" || sMachineID == "4" || sMachineID == "5")
            {
                #region Machine 1- 5
                for (int i = 1; i < 17; i++)
                {
                    DataRow dr = dt_Material.NewRow();
                    

                    string ModelName = dr_job["model" + i.ToString() + "Name"].ToString();
                    if (!string.IsNullOrEmpty(ModelName))
                    {
                        dr["materialPart"] = ModelName;
                        UpdatedBy = bll.GetUpdatedByMaterial(sJobnumber, ModelName);
                    }
                    else
                    {
                        continue;
                    }

                    string GetNG = dr_job["ng" + i.ToString() + "Count"].ToString();
                    if (!string.IsNullOrEmpty(GetNG) && GetNG != "0")
                    {
                        NGForMaterial = double.Parse(GetNG);
                    }
                    else
                    {
                        NGForMaterial = 0;
                    }

                    string GetOK = dr_job["ok" + i.ToString() + "Count"].ToString();
                    if (!string.IsNullOrEmpty(GetOK) && GetOK != "0")
                    {
                        OKForMaterial = double.Parse(GetOK);
                    }
                    else
                    {
                        OKForMaterial = 0;
                    }

                    dr["OK"] = OKForMaterial;
                    dr["NG"] = NGForMaterial;
                    dr["output"] = OKForMaterial + NGForMaterial;
                    if (OKForMaterial + NGForMaterial == 0)
                    {
                        dr["NGRate"] = "0%";
                    }else
                    {
                        dr["NGRate"] = Math.Round(NGForMaterial * 100 / (OKForMaterial + NGForMaterial), 2).ToString() + "%";
                    }

                    dr["machineID"] = "Machine" + sMachineID;
                    dr["jobNumber"] = dr_job["jobnumber"].ToString();
                    dr["partNumber"] = dr_job["partNumber"].ToString();
                    dr["StartTime"] = dr_job["StartTime"].ToString();
                    dr["EndTime"] = dr_job["stopTime"].ToString();
                    dr["module"] = dr_job["module"].ToString();
                    dr["UpdatedBy"] = UpdatedBy;
                    dr["shift"] = DateTime.Parse( dr_job["day"].ToString()).ToShortDateString() + "-" + dr_job["shift"].ToString();
                    dr["customer"] = dr_job["customer"].ToString();

                    //相同part, 只显示一次
                    if (i == 1)
                    {
                        dr["machineID_temp"] = "Machine" + sMachineID;
                        dr["shift_temp"] = DateTime.Parse(dr_job["day"].ToString()).ToShortDateString() + "-" + dr_job["shift"].ToString();
                        dr["module_temp"] = dr_job["module"].ToString();
                        dr["jobNumber_temp"] = dr_job["jobnumber"].ToString();
                        dr["partNumber_temp"] = dr_job["partNumber"].ToString();
                        dr["customer_temp"] = dr_job["customer"].ToString();

                        dr["StartTime_temp"] = DateTime.Parse(dr_job["StartTime"].ToString()).ToString("dd/MM/yy H:mm:ss");
                        dr["EndTime_temp"] = DateTime.Parse(dr_job["stopTime"].ToString()).ToString("dd/MM/yy H:mm:ss");
                   
                    }
                    else
                    {
                        dr["machineID_temp"] = "";
                        dr["shift_temp"] = "";
                        dr["module_temp"] = "";
                        dr["jobNumber_temp"] = "";
                        dr["partNumber_temp"] = "";
                        dr["customer_temp"] = "";
                        dr["StartTime_temp"] = "";
                        dr["EndTime_temp"] = "";
                    }
                    //相同part, 只显示一次


                    dt_Material.Rows.Add(dr);
                  
                }
                #endregion
            }
            else
            {
                #region machine 6-8
                DataRow dr = dt_Material.NewRow();


                UpdatedBy = bll.GetUpdatedByJob(sJobnumber);
                try
                {
                    NGForMaterial = double.Parse(dr_job["totalFail"].ToString());
                }
                catch
                {
                    NGForMaterial = 0;
                }

                try
                {
                    OKForMaterial = double.Parse(dr_job["totalPass"].ToString());
                }
                catch
                {
                    OKForMaterial = 0;
                }
                


                dr["machineID"] = "Machine" + sMachineID;

                dr["shift"] = DateTime.Parse(dr_job["day"].ToString()).ToShortDateString() + "-" + dr_job["shift"].ToString();
                dr["jobNumber"] = dr_job["jobnumber"].ToString();
                dr["partNumber"] = dr_job["partnumber"].ToString();
                dr["UpdatedBy"] = UpdatedBy;

                dr["NG"] = NGForMaterial;
                dr["OK"] = OKForMaterial;
                dr["output"] = OKForMaterial + NGForMaterial;
                dr["NGRate"] = OKForMaterial+NGForMaterial ==0 ? "0%": Math.Round(NGForMaterial * 100 / (NGForMaterial + OKForMaterial), 2).ToString() + "%";
                dr["StartTime"] = dr_job["startTime"].ToString();
                dr["EndTime"] = dr_job["stopTime"].ToString();
                dr["module"] = dr_job["module"].ToString();
                dr["customer"] = dr_job["customer"].ToString();


                dr["machineID_temp"] = "Machine" + sMachineID;
                dr["shift_temp"] = DateTime.Parse(dr_job["day"].ToString()).ToShortDateString() + "-" + dr_job["shift"].ToString();
                dr["module_temp"] = dr_job["module"].ToString();
                dr["jobNumber_temp"] = dr_job["jobnumber"].ToString();
                dr["partNumber_temp"] = dr_job["partNumber"].ToString();
                dr["customer_temp"] = dr_job["customer"].ToString();
                //dr["StartTime_temp"] = dr_job["startTime"].ToString();
                //dr["EndTime_temp"] = dr_job["stopTime"].ToString();
                dr["StartTime_temp"] = DateTime.Parse(dr_job["StartTime"].ToString()).ToString("dd/MM/yy H:mm:ss");
                dr["EndTime_temp"] = DateTime.Parse(dr_job["stopTime"].ToString()).ToString("dd/MM/yy H:mm:ss");


                Common.Class.BLL.LMMSBomDetail_BLL bll_BomDetail = new Class.BLL.LMMSBomDetail_BLL();
                DataTable dt_bomDetail = bll_BomDetail.GetBomDetailListByPartNumber(dr_job["partnumber"].ToString());
                if (dt_bomDetail == null || dt_bomDetail.Rows.Count == 0)
                {
                    dr["materialPart"] = "No BOM Detail !";
                }
                else
                {
                    dr["materialPart"] = dt_bomDetail.Rows[0]["materialPartNo"].ToString();
                }
               

                dt_Material.Rows.Add(dr);

                #endregion
            }

            return dt_Material;
        }

		
        
		public Common.Model.LMMSWatchLog_Model GetModel(string sJobNo)
		{
			return dal.GetModel(sJobNo);
		}
        
		public DataTable GetList(DateTime dDateFrom, DateTime dDateTo)
		{
            DataSet ds = dal.GetList(dDateFrom, dDateTo);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;

            }
            else
            {
                return ds.Tables[0];
            }
            
		}
	


		public List<Common.Model.LMMSWatchLog_Model> DataTableToList(DataTable dt)
		{
			List<Common.Model.LMMSWatchLog_Model> modelList = new List<Common.Model.LMMSWatchLog_Model>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Common.Model.LMMSWatchLog_Model model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Common.Model.LMMSWatchLog_Model();
					if(dt.Rows[n]["id"].ToString()!="")
					{
						model.id=int.Parse(dt.Rows[n]["id"].ToString());
					}
					if(dt.Rows[n]["dateTime"].ToString()!="")
					{
						model.dateTime=DateTime.Parse(dt.Rows[n]["dateTime"].ToString());
					}
					model.machineID=dt.Rows[n]["machineID"].ToString();
					model.partNumber=dt.Rows[n]["partNumber"].ToString();
					model.jobNumber=dt.Rows[n]["jobNumber"].ToString();
					model.description=dt.Rows[n]["description"].ToString();
					if(dt.Rows[n]["currentQuantity"].ToString()!="")
					{
						model.currentQuantity=int.Parse(dt.Rows[n]["currentQuantity"].ToString());
					}
					if(dt.Rows[n]["totalQuantity"].ToString()!="")
					{
						model.totalQuantity=int.Parse(dt.Rows[n]["totalQuantity"].ToString());
					}
					model.currentOperation=dt.Rows[n]["currentOperation"].ToString();
					model.prepDateOut=dt.Rows[n]["prepDateOut"].ToString();
					model.prepDateIn=dt.Rows[n]["prepDateIn"].ToString();
					model.paintDateOut=dt.Rows[n]["paintDateOut"].ToString();
					model.paintDateIn=dt.Rows[n]["paintDateIn"].ToString();
					if(dt.Rows[n]["laserDateOut"].ToString()!="")
					{
						model.laserDateOut=DateTime.Parse(dt.Rows[n]["laserDateOut"].ToString());
					}
					if(dt.Rows[n]["laserDateIn"].ToString()!="")
					{
						model.laserDateIn=DateTime.Parse(dt.Rows[n]["laserDateIn"].ToString());
					}
					if(dt.Rows[n]["productDateOut"].ToString()!="")
					{
						model.productDateOut=DateTime.Parse(dt.Rows[n]["productDateOut"].ToString());
					}
					if(dt.Rows[n]["productDateIn"].ToString()!="")
					{
						model.productDateIn=DateTime.Parse(dt.Rows[n]["productDateIn"].ToString());
					}
					model.ownerID=dt.Rows[n]["ownerID"].ToString();
					model.laserFileA=dt.Rows[n]["laserFileA"].ToString();
					model.laserFileB=dt.Rows[n]["laserFileB"].ToString();
					if(dt.Rows[n]["totalPass"].ToString()!="")
					{
						model.totalPass=int.Parse(dt.Rows[n]["totalPass"].ToString());
					}
					if(dt.Rows[n]["totalFail"].ToString()!="")
					{
						model.totalFail=int.Parse(dt.Rows[n]["totalFail"].ToString());
					}
					if(dt.Rows[n]["lastUpdated"].ToString()!="")
					{
						model.lastUpdated=DateTime.Parse(dt.Rows[n]["lastUpdated"].ToString());
					}
					if(dt.Rows[n]["startTime"].ToString()!="")
					{
						model.startTime=DateTime.Parse(dt.Rows[n]["startTime"].ToString());
					}
					if(dt.Rows[n]["stopTime"].ToString()!="")
					{
						model.stopTime=DateTime.Parse(dt.Rows[n]["stopTime"].ToString());
					}
					model.rmsStatus=dt.Rows[n]["rmsStatus"].ToString();
					model.status=dt.Rows[n]["status"].ToString();
					if(dt.Rows[n]["paintQuantity"].ToString()!="")
					{
						model.paintQuantity=int.Parse(dt.Rows[n]["paintQuantity"].ToString());
					}
					if(dt.Rows[n]["pqcQuantity"].ToString()!="")
					{
						model.pqcQuantity=int.Parse(dt.Rows[n]["pqcQuantity"].ToString());
					}
					if(dt.Rows[n]["laserDefectQuantity"].ToString()!="")
					{
						model.laserDefectQuantity=int.Parse(dt.Rows[n]["laserDefectQuantity"].ToString());
					}
					if(dt.Rows[n]["paintDefectQuantity"].ToString()!="")
					{
						model.paintDefectQuantity=int.Parse(dt.Rows[n]["paintDefectQuantity"].ToString());
					}
					if(dt.Rows[n]["mouldDefectQuantity"].ToString()!="")
					{
						model.mouldDefectQuantity=int.Parse(dt.Rows[n]["mouldDefectQuantity"].ToString());
					}
					model.modelName=dt.Rows[n]["modelName"].ToString();
					if(dt.Rows[n]["currentTotalPass"].ToString()!="")
					{
						model.currentTotalPass=int.Parse(dt.Rows[n]["currentTotalPass"].ToString());
					}
					if(dt.Rows[n]["currentTotalFail"].ToString()!="")
					{
						model.currentTotalFail=int.Parse(dt.Rows[n]["currentTotalFail"].ToString());
					}
					model.model1Name=dt.Rows[n]["model1Name"].ToString();
					model.model2Name=dt.Rows[n]["model2Name"].ToString();
					model.model3Name=dt.Rows[n]["model3Name"].ToString();
					if(dt.Rows[n]["ok1Count"].ToString()!="")
					{
						model.ok1Count=int.Parse(dt.Rows[n]["ok1Count"].ToString());
					}
					if(dt.Rows[n]["ok2Count"].ToString()!="")
					{
						model.ok2Count=int.Parse(dt.Rows[n]["ok2Count"].ToString());
					}
					if(dt.Rows[n]["ok3Count"].ToString()!="")
					{
						model.ok3Count=int.Parse(dt.Rows[n]["ok3Count"].ToString());
					}
					if(dt.Rows[n]["ng1Count"].ToString()!="")
					{
						model.ng1Count=int.Parse(dt.Rows[n]["ng1Count"].ToString());
					}
					if(dt.Rows[n]["ng2Count"].ToString()!="")
					{
						model.ng2Count=int.Parse(dt.Rows[n]["ng2Count"].ToString());
					}
					if(dt.Rows[n]["ng3Count"].ToString()!="")
					{
						model.ng3Count=int.Parse(dt.Rows[n]["ng3Count"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}
        



	
		public Common.Model.LMMSWatchLog_Model CopyObj(Common.Model.LMMSWatchLog_Model objModel )
		{
			Common.Model.LMMSWatchLog_Model model;
			model = new Common.Model.LMMSWatchLog_Model();
			model.id = objModel.id ;
			model.dateTime = objModel.dateTime ;
			model.machineID = objModel.machineID ;
			model.partNumber = objModel.partNumber ;
			model.jobNumber = objModel.jobNumber ;
			model.description = objModel.description ;
			model.currentQuantity = objModel.currentQuantity ;
			model.totalQuantity = objModel.totalQuantity ;
			model.currentOperation = objModel.currentOperation ;
			model.prepDateOut = objModel.prepDateOut ;
			model.prepDateIn = objModel.prepDateIn ;
			model.paintDateOut = objModel.paintDateOut ;
			model.paintDateIn = objModel.paintDateIn ;
			model.laserDateOut = objModel.laserDateOut ;
			model.laserDateIn = objModel.laserDateIn ;
			model.productDateOut = objModel.productDateOut ;
			model.productDateIn = objModel.productDateIn ;
			model.ownerID = objModel.ownerID ;
			model.laserFileA = objModel.laserFileA ;
			model.laserFileB = objModel.laserFileB ;
			model.totalPass = objModel.totalPass ;
			model.totalFail = objModel.totalFail ;
			model.lastUpdated = objModel.lastUpdated ;
			model.startTime = objModel.startTime ;
			model.stopTime = objModel.stopTime ;
			model.rmsStatus = objModel.rmsStatus ;
			model.status = objModel.status ;
			model.paintQuantity = objModel.paintQuantity ;
			model.pqcQuantity = objModel.pqcQuantity ;
			model.laserDefectQuantity = objModel.laserDefectQuantity ;
			model.paintDefectQuantity = objModel.paintDefectQuantity ;
			model.mouldDefectQuantity = objModel.mouldDefectQuantity ;
			model.modelName = objModel.modelName ;
			model.currentTotalPass = objModel.currentTotalPass ;
			model.currentTotalFail = objModel.currentTotalFail ;
			model.model1Name = objModel.model1Name ;
			model.model2Name = objModel.model2Name ;
			model.model3Name = objModel.model3Name ;
			model.ok1Count = objModel.ok1Count ;
			model.ok2Count = objModel.ok2Count ;
			model.ok3Count = objModel.ok3Count ;
			model.ng1Count = objModel.ng1Count ;
			model.ng2Count = objModel.ng2Count ;
			model.ng3Count = objModel.ng3Count ;
			return model;
		}


        public List<Common.Model.LMMSWatchLog_Model> GetQty_CurrentnToday()
        {
            List<Common.Model.LMMSWatchLog_Model> lJobList = new List<LMMSWatchLog_Model>();
            DataSet ds = dal.getRealJobInfo();

            if (ds == null && ds.Tables.Count == 0)
                return null;


            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int iTotalPass = dr["totalPass"].ToString() == "" ? 0 : int.Parse(dr["totalPass"].ToString());
                int iTotalFail = dr["totalFail"].ToString() == "" ? 0 : int.Parse(dr["totalFail"].ToString());
                int iTotalQty = dr["totalQuantity"].ToString() == "" ? 0 : int.Parse(dr["totalQuantity"].ToString());
                string sJobnumber = dr["jobNumber"].ToString();
                string sPartNumber = dr["partNumber"].ToString();
                string sMachineID = "Machine " + dr["machineID"].ToString();
                string sLotNo = dr["LotNo"].ToString();

                
                Common.Model.LMMSWatchLog_Model wlCurrent = new LMMSWatchLog_Model();

                wlCurrent.totalPass = iTotalPass;
                wlCurrent.totalFail = iTotalFail;
                wlCurrent.totalQuantity = iTotalQty;
                wlCurrent.jobNumber = sJobnumber;
                wlCurrent.partNumber = sPartNumber;
                wlCurrent.machineID = sMachineID;
                wlCurrent.lotNo = sLotNo;
                lJobList.Add(wlCurrent);
            }


            return lJobList;
        }


      




      

        public DataTable getOEEData(string dateFrom, string dateTo,string MachineID,string shift,string sDateNotIn_Confirmed, bool bExceptWeekend)
        {
            DataTable dt = dal.GetOEEData(dateFrom, dateTo, MachineID, shift, sDateNotIn_Confirmed,   bExceptWeekend);

            double OK_Total = 0;
            double Output_Total = 0;
            double PrecessTime_Total = 0;

            DataRow dr = dt.NewRow();

            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    OK_Total +=double.Parse( row["OK"].ToString());
                }
                catch {
                    OK_Total = 0;
                }

                try
                {
                    Output_Total += double.Parse(row["OutputQTY"].ToString());
                }
                catch {

                    Output_Total = 0;
                }

                try
                {
                    PrecessTime_Total += double.Parse(row["ProcessTime"].ToString());
                }
                catch {
                    PrecessTime_Total = 0;
                }


            }

            dr["OK"] = OK_Total;
            dr["OutputQTY"] = Output_Total;
            dr["ProcessTime"] = PrecessTime_Total;
            dr[0] = "Total";

            dt.Rows.Add(dr);


            return dt;
        }

        

        public DataTable GetSummaryReport(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sShift)
        {
           
            DataTable dt = dal.GetSummaryReport(dDateFrom, dDateTo, sPartNo, sShift);

           
            if (dt == null || dt.Rows.Count == 0)
            { 
                //没数据拼出8台 默认为0;
                for (int i = 1; i < 9; i++)
                {
                    DataRow drNew = dt.NewRow();

                    drNew["MachineID"] = "Machine" + i.ToString();
                    drNew["LaserBTN"] = 0;
                    drNew["PrintBTN"] = 0;
                    drNew["Lens784"] = 0;
                    drNew["Lens824"] = 0;
                    drNew["Lens833"] = 0;
                    drNew["Bezel257"] = 0;
                    drNew["Bezel830"] = 0;
                    drNew["Bezel831"] = 0;
                    drNew["Panel452"] = 0;
                    drNew["Panel656"] = 0;
                    drNew["Tks869"] = 0;
                    drNew["OK"] = 0;
                    drNew["NG"] = 0;
                    drNew["Output"] = 0;
                    drNew["RejRate"] = 0;

                    dt.Rows.Add(drNew);
                }
            }
            else
            {
                //如果某一台机器没数据, 凑满8条
                for (int i = 1; i < 9; i++)
                {
                    DataRow[] drArr = dt.Select(" machineID = 'Machine" + i.ToString() + "'", "");
                    if (drArr.Length > 0)
                        continue;


                    DataRow drNew = dt.NewRow();

                    drNew["MachineID"] = "Machine" + i.ToString();
                    drNew["LaserBTN"] = 0;
                    drNew["PrintBTN"] = 0;
                    drNew["Lens784"] = 0;
                    drNew["Lens824"] = 0;
                    drNew["Lens833"] = 0;
                    drNew["Bezel257"] = 0;
                    drNew["Bezel830"] = 0;
                    drNew["Bezel831"] = 0;
                    drNew["Panel452"] = 0;
                    drNew["Panel656"] = 0;
                    drNew["Tks869"] = 0;
                    drNew["OK"] = 0;
                    drNew["NG"] = 0;
                    drNew["Output"] = 0;
                    drNew["RejRate"] = 0;

                    dt.Rows.InsertAt(drNew, i - 1);
                }
            }


            //添加total 行
            DataRow drTotal = dt.NewRow();
            drTotal["MachineID"] = "Total:";

            drTotal["LaserBTN"] = dt.Compute(" SUM(LaserBTN) ", "");
            drTotal["PrintBTN"] = dt.Compute(" SUM(PrintBTN) ", "");
            drTotal["Lens784"] = dt.Compute(" SUM(Lens784) ", "");
            drTotal["Lens824"] = dt.Compute(" SUM(Lens824) ", "");
            drTotal["Lens833"] = dt.Compute(" SUM(Lens833) ", "");
            drTotal["Bezel257"] = dt.Compute(" SUM(Bezel257) ", "");
            drTotal["Bezel830"] = dt.Compute(" SUM(Bezel830) ", "");
            drTotal["Bezel831"] = dt.Compute(" SUM(Bezel831) ", "");
            drTotal["Panel452"] = dt.Compute(" SUM(Panel452) ", "");
            drTotal["Panel656"] = dt.Compute(" SUM(Panel656) ", "");
            drTotal["Tks869"] = dt.Compute(" SUM(Tks869) ", "");

            double totalOK = double.Parse(dt.Compute(" SUM(OK) ", "").ToString());
            double totalNG = double.Parse(dt.Compute(" SUM(NG) ", "").ToString());
            double totalOutput = double.Parse(dt.Compute(" SUM(Output) ", "").ToString());


            drTotal["OK"] = totalOK;
            drTotal["NG"] = totalNG;
            drTotal["Output"] = totalOutput;
            drTotal["RejRate"] = totalOutput == 0? 0:  Math.Round(totalNG / totalOutput * 100, 2);

            dt.Rows.Add(drTotal);
            
            return dt;

        }
      

       
        public DataTable GetProductionDetailReport(DateTime dDateFrom, DateTime dDateTo, string sShift, string sModel, string sPartNo, string sMachineID, string sJobNo)
        {
            DataTable dt = dal.GetProductionDetailReport(dDateFrom, dDateTo, sShift, sModel, sPartNo, sMachineID, sJobNo);
            if (dt == null || dt.Rows.Count == 0)
                return null;


            TimeSpan totalTime = new TimeSpan(0);
            Dictionary<string, double> dicMrpQty = new Dictionary<string, double>();
            Dictionary<string, double> dicSetMrpQty = new Dictionary<string, double>();

            foreach (DataRow dr in dt.Rows)
            {
                string jobNo = dr["jobNo"].ToString();

                double ok = double.Parse(dr["ok"].ToString());
                double ng = double.Parse(dr["ng"].ToString());
                double output = double.Parse(dr["output"].ToString());
                double mrpTotal = double.Parse(dr["mrpTotal"].ToString());
                double setupQty = double.Parse(dr["setupQty"].ToString());

                double setOK = double.Parse(dr["setOK"].ToString());
                double setNG = double.Parse(dr["setNG"].ToString());                
                double setOutput = double.Parse(dr["setOutput"].ToString());
                double setMrpTotal = double.Parse(dr["setMrpTotal"].ToString());
                double setSetupQty = double.Parse(dr["setSetupQty"].ToString());

                
                

                dr["displayOK"] = string.Format("{0}({1})", setOK,ok);
                dr["displayNG"] = string.Format("{0}({1})", setNG, ng);
                dr["displayOutput"] = string.Format("{0}({1})", setOutput, output);
                dr["displayMRP"] = string.Format("{0}({1})", setMrpTotal, mrpTotal);
                dr["displaySetup"] = string.Format("{0}({1})", setSetupQty, setupQty);


                double rejRate = Math.Round((ng + setupQty) / output * 100, 2);
                double setRejRate = Math.Round((setNG + setSetupQty) / setOutput * 100, 2);
                dr["displayRejRate"] = string.Format("{0}%({1}%)", setRejRate.ToString("0.00"), rejRate.ToString("0.00"));



                totalTime = totalTime + TimeSpan.Parse(dr["Time"].ToString());



                if (!dicMrpQty.ContainsKey(jobNo))
                    dicMrpQty.Add(jobNo, mrpTotal);
                
                if (!dicSetMrpQty.ContainsKey(jobNo))
                    dicSetMrpQty.Add(jobNo, setMrpTotal);
            }



            double totalOk = double.Parse(dt.Compute("sum(ok)", "").ToString());
            double totalNg = double.Parse(dt.Compute("sum(ng)", "").ToString());
            double totalOutput = double.Parse(dt.Compute("sum(output)", "").ToString());
            double totalSetupQty = double.Parse(dt.Compute("sum(setupQty)", "").ToString());

            double totalSetOK = double.Parse(dt.Compute("sum(setOK)", "").ToString());
            double totalSetNG = double.Parse(dt.Compute("sum(setNG)", "").ToString());
            double totalSetOutput = double.Parse(dt.Compute("sum(setOutput)", "").ToString());
            double totalSetSetupQty = double.Parse(dt.Compute("sum(setSetupQty)", "").ToString());

            double totalMrpTotal = 0;
            double totalSetMrpTotal = 0;
            foreach (KeyValuePair<string, double> item in dicMrpQty)
            {
                totalMrpTotal += item.Value;
            }
            foreach (KeyValuePair<string, double> item in dicSetMrpQty)
            {
                totalSetMrpTotal += item.Value;
            }




            DataRow drSummary = dt.NewRow();
            drSummary["shift"] = "Total";
            drSummary["Time"] = Math.Round(totalTime.TotalHours, 2) + "H";
            drSummary["displayOK"] = string.Format("{0}({1})", totalSetOK, totalOk);
            drSummary["displayNG"] = string.Format("{0}({1})", totalSetNG, totalNg);
            drSummary["displayOutput"] = string.Format("{0}({1})", totalSetOutput, totalOutput);
            drSummary["displayMRP"] = string.Format("{0}({1})", totalSetMrpTotal, totalMrpTotal);
            drSummary["displaySetup"] = string.Format("{0}({1})", totalSetSetupQty, totalSetupQty);

            double totalRejRate = Math.Round((totalNg + totalSetupQty) / totalOutput * 100, 2);
            double totalSetRejRate = Math.Round((totalSetNG + totalSetSetupQty) / totalSetOutput * 100, 2);
            drSummary["displayRejRate"] = string.Format("{0}%({1}%)", totalSetRejRate.ToString("0.00"), totalRejRate.ToString("0.00"));

            dt.Rows.Add(drSummary);

            return dt;
        }




        public DataTable GetDayOutput(DateTime dDay)
        {
            DataSet ds = new DataSet();
            ds = dal.GetDayOutput(dDay);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }
        


        public bool IsJobFinished(string sJobNumber)
        {
            bool Result = false;

            DataSet ds = dal.IsJobFinished(sJobNumber);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                Result = false;
            }
            else
            {
                DataTable dt = ds.Tables[0];
                int Pass = int.Parse(dt.Rows[0]["totalPass"].ToString());
                int Fail = int.Parse(dt.Rows[0]["totalFail"].ToString());
                int Total = int.Parse(dt.Rows[0]["totalQuantity"].ToString());


                if (Pass + Fail >= Total)
                {
                    Result = true;
                }else
                {
                    Result = false;
                }

            }

            return Result;
        }

        #endregion  Method





        public DataTable GetProductivityChartData(string sReportType, string sYear, string sMachineID, string sPartNo, string sShift,string sModel, DateTime dDateFrom, DateTime dDateTo)
        {
            DataTable dtOutput = new DataTable();

            if (sReportType == "Yearly")
            {
                dtOutput = dal.getYearlyProduct(sPartNo, sShift, sMachineID,sModel);
            }
            else if (sReportType =="Monthly")
            {
                dtOutput = dal.getMonthlyProduct(sPartNo, sShift, int.Parse(sYear), sMachineID,sModel);
            }
            else if (sReportType == "Daily")
            {
                dtOutput = dal.getDailyProduct(sPartNo, sShift, dDateFrom, dDateTo, sMachineID, sModel);
            }
            else if (sReportType == "Model")
            {
                dtOutput = dal.getProductGroupByModel(sPartNo, sShift, dDateFrom, dDateTo, sMachineID);
            }
            else if (sReportType == "Machine")
            {
                dtOutput = dal.getProductGroupByMachine(sPartNo, sShift, dDateFrom, dDateTo, sModel);
            }



            return dtOutput;
        }





        #region for Output Report

        private string sAttdEmptyChar = "0"; 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dDay"></param>
        /// <param name="sShift"></param>
        /// <returns></returns>
        public DataTable getAttendance(DateTime dDay, string sShift, string sDepartment)
        {
            DataSet ds = new DataSet();
            //Columns: Module -- MachineCount -- OK -- NG -- Output -- Target -- ProdHrs
            BLL.LMMSUserAttendanceTracking_BLL bllAttTrk = new LMMSUserAttendanceTracking_BLL();
             
            ds = bllAttTrk.getAttendance(dDay, sShift, sDepartment);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                DataTable dtAttend = new DataTable();
                dtAttend.Columns.Add("Man Power");
                dtAttend.Columns.Add("Sup Te Leader");
                dtAttend.Columns.Add("Operator");
                dtAttend.Columns.Add("Attendance");
                dtAttend.Columns.Add("Annual Leave");
                dtAttend.Columns.Add("MC");
                dtAttend.Columns.Add("Absent");

                dtAttend.Columns.Add("UnPaid");
                dtAttend.Columns.Add("Maternity");
                dtAttend.Columns.Add("Marriage");
                dtAttend.Columns.Add("Compassionate");
                dtAttend.Columns.Add("ChildCare");
                dtAttend.Columns.Add("Business Trip");




                try
                {
                    int iManPower = 0;
                    int iSupTeLeader = 0;
                    int iOperator = 0;
                    int iAttendance = 0;
                    int iAnnualLeave = 0;
                    int iMCLeave = 0;
                    int iAbsent = 0;

                    int iUnpaid = 0;
                    int iMATERNITY = 0;
                    int iMARRIAGE = 0;
                    int iCOMPASSIONATE = 0;
                    int iCHILDCARE = 0;
                    int iBUSINESSTRIP = 0;

                    foreach (DataRow tmpDr in ds.Tables[0].Rows)
                    {
                        iManPower += int.Parse(tmpDr["UserCount"].ToString());
                        switch (tmpDr["Attendance"].ToString().ToUpper())
                        {
                            case "ATTENDANCE":
                                #region attendance
                                {
                                    switch (tmpDr["UserGroup"].ToString().ToUpper())
                                    {
                                        case "ADMIN":
                                            {
                                                break;
                                            }
                                        case "ENGINEER":
                                            {
                                                iSupTeLeader += int.Parse(tmpDr["UserCount"].ToString()); 
                                                iAttendance += int.Parse(tmpDr["UserCount"].ToString());
                                                break;
                                            }
                                        case "IPQC":
                                            {
                                                break;
                                            }
                                        case "LEADER":
                                            {
                                                iSupTeLeader += int.Parse(tmpDr["UserCount"].ToString()); 
                                                iAttendance += int.Parse(tmpDr["UserCount"].ToString());
                                                break;
                                            }
                                        case "MH":
                                            {
                                                iSupTeLeader += int.Parse(tmpDr["UserCount"].ToString()); 
                                                iAttendance += int.Parse(tmpDr["UserCount"].ToString());
                                                break;
                                            }
                                        case "OPERATOR":
                                            {
                                                iOperator += int.Parse(tmpDr["UserCount"].ToString()); 
                                                iAttendance += int.Parse(tmpDr["UserCount"].ToString());
                                                break;
                                            }
                                        case "SUPERVISOR":
                                            {
                                                iSupTeLeader += int.Parse(tmpDr["UserCount"].ToString()); 
                                                iAttendance += int.Parse(tmpDr["UserCount"].ToString());
                                                break;
                                            }
                                        case "TECHNICIAN":
                                            {
                                                iSupTeLeader += int.Parse(tmpDr["UserCount"].ToString());
                                                iAttendance += int.Parse(tmpDr["UserCount"].ToString());
                                                break;
                                            }

                                        case "CHECKER":
                                            {
                                                iSupTeLeader += int.Parse(tmpDr["UserCount"].ToString());
                                                iAttendance += int.Parse(tmpDr["UserCount"].ToString());
                                                break;
                                            }

                                        case "TEMPORARYSTAFF":
                                            {
                                                iSupTeLeader += int.Parse(tmpDr["UserCount"].ToString());
                                                iAttendance += int.Parse(tmpDr["UserCount"].ToString());
                                                break;
                                            }
                                    }
                                    break;
                                }
                            #endregion
                            case ("ANNUAL LEAVE"):
                                {
                                    iAnnualLeave += int.Parse(tmpDr["UserCount"].ToString());
                                    break;
                                }
                            case ("MC"):
                                {
                                    iMCLeave += int.Parse(tmpDr["UserCount"].ToString());
                                    break;
                                }
                            case ("ABSENT"):
                                {
                                    iAbsent += int.Parse(tmpDr["UserCount"].ToString());
                                    break;
                                }
                            case ("UNPAID"):
                                {
                                    iUnpaid += int.Parse(tmpDr["UserCount"].ToString());
                                    break;
                                }
                            case ("MATERNITY"):
                                {
                                    iMATERNITY += int.Parse(tmpDr["UserCount"].ToString());
                                    break;
                                }
                            case ("MARRIAGE"):
                                {
                                    iMARRIAGE += int.Parse(tmpDr["UserCount"].ToString());
                                    break;
                                }
                            case ("COMPASSIONATE"):
                                {
                                    iCOMPASSIONATE += int.Parse(tmpDr["UserCount"].ToString());
                                    break;
                                }
                            case ("CHILDCARE"):
                                {
                                    iCHILDCARE += int.Parse(tmpDr["UserCount"].ToString());
                                    break;
                                }
                            case ("BUSINESS TRIP"):
                                {
                                    iBUSINESSTRIP += int.Parse(tmpDr["UserCount"].ToString());
                                    break;
                                }
                        }
                             


                    }

                    DataRow dr = dtAttend.NewRow();

                    dr["Man Power"] = iManPower == 0 ? sAttdEmptyChar : iManPower.ToString();
                    dr["Sup Te Leader"] = iSupTeLeader == 0 ? sAttdEmptyChar : iSupTeLeader.ToString();
                    dr["Operator"] = iOperator == 0 ? sAttdEmptyChar : iOperator.ToString();
                    dr["Attendance"] = iAttendance == 0 ? sAttdEmptyChar : iAttendance.ToString();
                    dr["Annual Leave"] = iAnnualLeave == 0 ? sAttdEmptyChar : iAnnualLeave.ToString();
                    dr["MC"] = iMCLeave == 0 ? sAttdEmptyChar : iMCLeave.ToString();
                    dr["Absent"] = iAbsent == 0 ? sAttdEmptyChar : iAbsent.ToString();



                    dr["UnPaid"] = iUnpaid == 0 ? sAttdEmptyChar : iUnpaid.ToString();
                    dr["Maternity"] = iMATERNITY == 0 ? sAttdEmptyChar : iMATERNITY.ToString();
                    dr["Marriage"] = iMARRIAGE == 0 ? sAttdEmptyChar : iMARRIAGE.ToString();
                    dr["Compassionate"] = iCOMPASSIONATE == 0 ? sAttdEmptyChar : iCOMPASSIONATE.ToString();
                    dr["ChildCare"] = iCHILDCARE == 0 ? sAttdEmptyChar : iCHILDCARE.ToString();
                    dr["Business Trip"] = iBUSINESSTRIP == 0 ? sAttdEmptyChar : iBUSINESSTRIP.ToString();
                    



                    dtAttend.Rows.Add(dr);

                    return dtAttend;

                }
                catch (Exception)
                {
                    DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.BLL", "Class:LMMSWatchLog_BLL", "Function:  internal DataSet getAttendance(DateTime dDay, string sShift)" + "TableName:LMMSWatchLog", " sShift =" + sShift + "; dDay = " + dDay.ToString() + ")");
                    return dtAttend;
                }
            }
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="dDay"></param>
        /// <param name="sShift"></param>
        /// <returns></returns>
        public DataTable getOutput(DateTime dDay, string sShift,string sDepartment)
        { 
            DataSet ds = new DataSet();
            //Columns: Module -- MachineCount -- OK -- NG -- Output -- Target -- ProdHrs
            ds = dal.getOutput(dDay, sShift);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                //dwyane  get running & testing Time
                Dictionary<string, double> dicTestingTimeCount = new Dictionary<string, double>();
                for (int i = 1; i < 9; i++)
                {
                    Common.BLL.LMMSEventLog_BLL bll = new LMMSEventLog_BLL();
                    double TestingTimeCount = bll.GetStatusTime(dDay, sShift, i.ToString(), StaticRes.Global.clsConstValue.ConstStatus.Testing,StaticRes.Global.clsConstValue.ConstStatus.PowerOn);

                    dicTestingTimeCount.Add(i.ToString(), TestingTimeCount);
                }
                //dwyane  get testing Time


                //< asp:BoundColumn DataField = "Seq" HeaderText = "Seq" ></ asp:BoundColumn >
                //< asp:ButtonColumn DataTextField = "PartNo"  HeaderText = "Day_Shift_Total_MC_Used" CommandName = "Update" FooterText = "Text" >
                //< asp:BoundColumn DataField = "Customer" HeaderText = "Customer" >
                //< asp:BoundColumn DataField = "ProductType" HeaderText = "Pruduct Type" >
                //< asp:BoundColumn DataField = "MaterialName" HeaderText = "Material Name" >
                //< asp:BoundColumn DataField = "Model" HeaderText = "Model" >   
                //< asp:BoundColumn DataField = "TargetMCRunning" HeaderText = "TargetMC Running HR" >
                //< asp:BoundColumn DataField = "ActualMCRunning" HeaderText = "ActualMC Running" >
                //< asp:BoundColumn DataField = "EachUtilizationRate" HeaderText = "Each Utilization Rate" >
                //< asp:BoundColumn DataField = "TargetQty" HeaderText = "TARGET OUTPUT" >
                //< asp:BoundColumn DataField = "TotalQty" HeaderText = "TOTAL OUTPUT" >
                //< asp:BoundColumn DataField = "SetupUsage" HeaderText = "Setup Usage QTY (%)" >
                //< asp:BoundColumn DataField = "BuyoffUsage" HeaderText = "Buyoff Usage QTY (%)" >
                //< asp:BoundColumn DataField = "DefectedQTY" HeaderText = "Defected QTY (%)" >      
                //< asp:BoundColumn DataField = "RejectQty" HeaderText = "REJ QTY (%)" >                               
                //< asp:BoundColumn DataField = "Productivity" HeaderText = "Productivity%" >

                DataTable dtOutput = new DataTable();
                dtOutput.Columns.Add("MCID");
                dtOutput.Columns.Add("PartNo");
                dtOutput.Columns.Add("Customer");
                dtOutput.Columns.Add("ProductType");
                dtOutput.Columns.Add("MaterialName");
                dtOutput.Columns.Add("Model");
                dtOutput.Columns.Add("TargetMCRunning");
                dtOutput.Columns.Add("ActualMCRunning");
                dtOutput.Columns.Add("EachUtilizationRate"); 
                dtOutput.Columns.Add("TargetQty");
                dtOutput.Columns.Add("TotalQty");
                dtOutput.Columns.Add("SetupUsage");
                dtOutput.Columns.Add("BuyoffUsage");
                dtOutput.Columns.Add("DefectedQTY");
                dtOutput.Columns.Add("RejectQty");
                dtOutput.Columns.Add("Productivity");

                try
                {
                    string sModel = "Total:";
                    int dTargetMCRunning = 0;
                    decimal dActualMCRunning = 0;
                    decimal dProdHours = 0;
                    int iTargetQty = 0;
                    int iTotalQty = 0;
                    int iSetupQty = 0;
                    int iBuyoffQty = 0;
                    int iDefectedQty = 0;
                    int iRejectQty = 0;
                    int iPassQty = 0;
                    string sRejRate = "";


                    Common.Class.BLL.LMMSBomDetail_BLL bll_BomDetail = new Class.BLL.LMMSBomDetail_BLL();
                   

                    foreach (DataRow tmpDr in ds.Tables[0].Rows)
                    {
                        DataRow dr = dtOutput.NewRow();
                        dr["MCID"] = tmpDr["MCID"].ToString();
                        dr["PartNo"] = tmpDr["PartNo"].ToString();
                        dr["Model"] = tmpDr["Module"].ToString();
                        dr["Customer"] = tmpDr["Customer"].ToString();
                        dr["ProductType"] = tmpDr["Type"].ToString();

                        DataTable dt_bomDetail = bll_BomDetail.GetBomDetailListByPartNumber(tmpDr["PartNo"].ToString());
                        if (dt_bomDetail == null || dt_bomDetail.Rows.Count == 0)
                        {
                            dr["MaterialName"] = "";
                        }
                        else
                        {
                            string sMaterialPart = "";
                            foreach (DataRow tmpdr in dt_bomDetail.Rows)
                            {
                                sMaterialPart = sMaterialPart + "," + tmpdr["materialPartNo"].ToString();
                            }
                            sMaterialPart = sMaterialPart.Trim(char.Parse(","));
                            dr["MaterialName"] = sMaterialPart;
                        }
                         
                        dr["TargetMCRunning"] = "12"; // let user input  
                        dTargetMCRunning += 0;                                               //dwyane add testing time
                        dr["ActualMCRunning"] = double.Parse(tmpDr["ProdHrs"].ToString()) + dicTestingTimeCount[tmpDr["MCID"].ToString()];                                    //dwyane add testing time
                        dr["EachUtilizationRate"] = Math.Round(((    (double.Parse(tmpDr["ProdHrs"].ToString()) + dicTestingTimeCount[tmpDr["MCID"].ToString()])   * 100.0) / double.Parse(dr["TargetMCRunning"].ToString())), 2).ToString() + "%";
                        //dr["ActualMCRunning"] = tmpDr["MachineCount"].ToString();
                        //dActualMCRunning += decimal.Parse(tmpDr["MachineCount"].ToString());
                        //dr["ProdHours"] = tmpDr["ProdHrs"].ToString();
                        dProdHours += decimal.Parse(tmpDr["ProdHrs"].ToString());
                        dProdHours += decimal.Parse(dicTestingTimeCount[tmpDr["MCID"].ToString()].ToString());//dwyane add testing time
                        dr["TargetQty"] = tmpDr["Target"].ToString();
                        iTargetQty += int.Parse(tmpDr["Target"].ToString());
                        dr["TotalQty"] = tmpDr["Output"].ToString();
                        iTotalQty += int.Parse(tmpDr["Output"].ToString());


                        if (decimal.Parse(tmpDr["Output"].ToString()) > 0)
                        {

                            dr["SetupUsage"] = tmpDr["SetupQTY"].ToString() + "(" + Math.Round(((double.Parse(tmpDr["SetupQTY"].ToString()) * 100.0) / double.Parse(tmpDr["Output"].ToString())), 2).ToString() + "%)";
                            iSetupQty += int.Parse(tmpDr["SetupQTY"].ToString());

                            dr["BuyoffUsage"] = tmpDr["BuyoffQTY"].ToString() + "(" + Math.Round(((double.Parse(tmpDr["BuyoffQTY"].ToString()) * 100.0) / double.Parse(tmpDr["Output"].ToString())), 2).ToString() + "%)";
                            iBuyoffQty += int.Parse(tmpDr["BuyoffQty"].ToString());

                            dr["DefectedQTY"] = tmpDr["DefectedQTY"].ToString() + "(" + Math.Round(((double.Parse(tmpDr["DefectedQTY"].ToString()) * 100.0) / double.Parse(tmpDr["Output"].ToString())), 2).ToString() + "%)";
                            iDefectedQty += int.Parse(tmpDr["DefectedQTY"].ToString());


                            dr["RejectQty"] = tmpDr["NG"].ToString() + "(" + Math.Round(((double.Parse(tmpDr["NG"].ToString()) * 100.0) / double.Parse(tmpDr["Output"].ToString())), 2).ToString() + "%)";
                            iRejectQty += int.Parse(tmpDr["NG"].ToString());
                        }
                        else
                        {
                            dr["SetupUsage"] = "0(0%)";
                            iSetupQty += 0;

                            dr["BuyoffUsage"] = "0(0%)";
                            iBuyoffQty += 0;

                            dr["DefectedQTY"] = "0(0%)";
                            iDefectedQty += 0;

                            dr["RejectQty"] = "0(0%)";
                            iRejectQty += 0;
                        }
                        //if (decimal.Parse(tmpDr["Output"].ToString()) > 0)
                        //{
                        //    dr["RejRate"] = (Math.Round(decimal.Parse(tmpDr["NG"].ToString()) * 100 / decimal.Parse(tmpDr["Output"].ToString()), 2)).ToString() + "%"; ;
                        //}
                        //else
                        //{
                        //    dr["RejRate"] = "0%";
                        //}
                        dr["Productivity"] = Math.Round(((double.Parse(tmpDr["Output"].ToString()) * 100.0) / double.Parse(tmpDr["Target"].ToString())), 2).ToString() + "%";// let user input

                        dtOutput.Rows.Add(dr);
                    }

                    //total data.
                    DataRow drTotal = dtOutput.NewRow();

                    drTotal["MCID"] = "Total:";

                    #region "Attendance Data"
                    DataTable dtLaserShiftAttendance = this.getAttendance(dDay, sShift, sDepartment);
                    //drTotal["PartNo"] = "";
                    //drTotal["Customer"] = "";
                    //drTotal["ProductType"] = "";
                    if (dtLaserShiftAttendance != null && dtLaserShiftAttendance.Rows.Count > 0)
                    {
                        drTotal["PartNo"] = "Target Attendance: " +  dtLaserShiftAttendance.Rows[0]["Man Power"].ToString();
                        drTotal["Customer"] = "Actual Attendance: " + dtLaserShiftAttendance.Rows[0]["Attendance"].ToString();
                        drTotal["ProductType"] = "Persentage: " + Math.Round(((double.Parse(dtLaserShiftAttendance.Rows[0]["Attendance"].ToString()) * 100.0) / double.Parse(dtLaserShiftAttendance.Rows[0]["Man Power"].ToString())), 2).ToString() + "%"; 
                    }
                    else
                    {
                        drTotal["PartNo"] = "Target Attendance: 0";
                        drTotal["Customer"] = "Actual Attendance: 0";
                        drTotal["ProductType"] = "Persentage: 0%"; 
                    }  
                    #endregion 
                   
                    drTotal["MaterialName"] = "";
                    drTotal["Model"] = "";
                    drTotal["TargetMCRunning"] = "";
                    drTotal["ActualMCRunning"] = Math.Ceiling(dProdHours).ToString();
                    drTotal["EachUtilizationRate"] = Math.Round(((double.Parse(dProdHours.ToString()) * 100.0) / double.Parse((12*8).ToString())), 2).ToString() + "%";
                    drTotal["TargetQty"] = iTargetQty.ToString();
                    drTotal["TotalQty"] = iTotalQty.ToString();
                    if (iTotalQty == 0)
                    {
                        drTotal["SetupUsage"] = "0(0%)";
                        drTotal["BuyoffUsage"] = "0(0%)";
                        drTotal["DefectedQTY"] = "0(0%)";
                        drTotal["RejectQty"] = "0(0%)";
                        drTotal["Productivity"] = "0(0%)";
                    }
                    else
                    {
                        drTotal["SetupUsage"] = iSetupQty.ToString() + "(" + Math.Round(((double.Parse(iSetupQty.ToString()) * 100.0) / double.Parse(iTotalQty.ToString())), 2).ToString() + "%)";
                        drTotal["BuyoffUsage"] = iBuyoffQty.ToString() + "(" + Math.Round(((double.Parse(iBuyoffQty.ToString()) * 100.0) / double.Parse(iTotalQty.ToString())), 2).ToString() + "%)";
                        drTotal["DefectedQTY"] = iDefectedQty.ToString() + "(" + Math.Round(((double.Parse(iDefectedQty.ToString()) * 100.0) / double.Parse(iTotalQty.ToString())), 2).ToString() + "%)";
                        drTotal["RejectQty"] = iRejectQty.ToString() + "(" + Math.Round(((double.Parse(iRejectQty.ToString()) * 100.0) / double.Parse(iTotalQty.ToString())), 2).ToString() + "%)";
                        drTotal["Productivity"] = Math.Round(((double.Parse(iTotalQty.ToString()) * 100.0) / double.Parse(iTargetQty.ToString())), 2).ToString() + "%";
                    }
                    

                    dtOutput.Rows.Add(drTotal);



                    #region dwyane:  同一mc 不同part, target time, actual time, utilization只显示一行.
                    for (int i = 1; i < 9; i++)
                    {
                        double Total_ActualTime = 0;

                        foreach (DataRow dr in dtOutput.Select("MCID = '" + i + "'"))
                        {
                            Total_ActualTime += double.Parse(dr["ActualMCRunning"].ToString());
                            dr["TargetMCRunning"] = "";
                            dr["ActualMCRunning"] = "";
                            dr["EachUtilizationRate"] = "";
                        }

                        dtOutput.Select("MCID = '" + i + "'")[0]["TargetMCRunning"] = 12;
                        dtOutput.Select("MCID = '" + i + "'")[0]["ActualMCRunning"] = Total_ActualTime;
                        dtOutput.Select("MCID = '" + i + "'")[0]["EachUtilizationRate"] = Math.Round((Total_ActualTime) / 12 * 100, 2).ToString() + "%";
                    }
                    #endregion





                    return dtOutput;

                }
                catch (Exception ex)
                {
                    DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace:Common.BLL", "Class:LMMSWatchLog_BLL", "Function:  internal DataSet getOutput(DateTime dDay, string sShift)" + "TableName:LMMSWatchLog", " sShift =" + sShift + "; dDay = " + dDay.ToString() + ") [" + ex.ToString() + "]");
                    return dtOutput;
                }
            }
        }




        public DataTable getProductivityReportForLaser(DateTime dDay, string sShift, string sDepartment)
        {

            DataTable dtProduct = dal.getProductivityReportForLaser(dDay, sShift);
            

            //total testing time
            double Total_TestingTime = 0;
            for (int i = 1; i < 9; i++)
            {
                Common.BLL.LMMSEventLog_BLL bll = new LMMSEventLog_BLL();
                Total_TestingTime += bll.GetStatusTime(dDay, sShift, i.ToString(), StaticRes.Global.clsConstValue.ConstStatus.Testing);
            }


            #region attendance 
            
            DataTable dt = getAttendance(dDay, sShift, sDepartment);

            double ManPower = 0;// double.Parse( dt.Rows[0]["Man Power"].ToString());
            double Attendance = 0;
            string AttendRate = "";

            if (dt != null && dt.Rows.Count != 0)
            {
                ManPower = double.Parse(dt.Rows[0]["Man Power"].ToString());
                Attendance = double.Parse(dt.Rows[0]["Attendance"].ToString());
                AttendRate = ManPower == 0 ? "0.00%" : Math.Round(Attendance / ManPower * 100, 2).ToString() + "%";
            }
            #endregion


            #region foreach for total Row
            double Total_ActualTime = 0;
            double Total_ActualQty = 0;//实际良品
            double Total_TargetQty = 0;//理论总量
            double Total_RejQty = 0;

            foreach (DataRow dr in dtProduct.Rows)
            {
                if (dr["ActualHR"].ToString() != "")
                    Total_ActualTime += double.Parse(dr["ActualHR"].ToString()) * 3600;

                if (dr["ActualQty"].ToString() != "")
                    Total_ActualQty += double.Parse(dr["ActualQty"].ToString());

                if (dr["TargetQty"].ToString() != "")
                    Total_TargetQty += double.Parse(dr["TargetQty"].ToString());

                if (dr["RejectQty"].ToString() != "")
                    Total_RejQty += double.Parse(dr["RejectQty"].ToString());
            }

            //Rejection Rate
            double TotalRejRate = Total_ActualQty == 0? 0.00: Math.Round(Total_RejQty / (Total_ActualQty + Total_RejQty) * 100, 2);

            //Utilization
            double dUtilization = Math.Round((Total_ActualTime + Total_TestingTime) / 3600 / 96 * 100, 2);
            dUtilization = dUtilization > 100 ? 100 : dUtilization;
            string Utilization = Total_ActualTime+ Total_TestingTime == 0? "0.00%" : dUtilization.ToString() + "%";

            //Productivity 
            double dProductivity = Total_TargetQty == 0 ? 0.00 : Math.Round(Total_ActualQty / Total_TargetQty * 100, 2);
            string Productivity = dProductivity > 100? "100%": dProductivity.ToString() + "%";

            //dr total 
            DataRow dr_total = dtProduct.NewRow();
            dr_total["SN"] = GetSN("Day Shift Total");
            dr_total["ProductType"] = sShift == StaticRes.Global.Shift.Day ? "Day Shift Total" : "Night Shift Total";
            dr_total["ActualQty"] = Total_ActualQty;
            dr_total["RejectQty"] = Total_RejQty;
            dr_total["TotalQty"] = Total_ActualQty + Total_RejQty;
            dr_total["ActualHR"] = Common.CommFunctions.ConvertDateTimeShort(Math.Round(Total_ActualTime / 3600, 2).ToString());
            dr_total["TargetQty"] = Total_TargetQty;
            dr_total["TargetHR"] = "96:00:00";

            dr_total["ManPower"] = ManPower;
            dr_total["Attendance"] = Attendance;
            dr_total["AttendRate"] = AttendRate;
            dr_total["Utilization"] = Utilization;
            dr_total["Productivity"] = Productivity;
            dr_total["RejRate"] = TotalRejRate==0 ? "0.00%" : TotalRejRate.ToString() + "%";

            dtProduct.Rows.Add(dr_total);

            #endregion
            

            #region add all type
            List<string> listType = new List<string>();
            listType.Add(StaticRes.Global.ProductType.BUTTON);
            listType.Add(StaticRes.Global.ProductType.PRINT);
            listType.Add(StaticRes.Global.ProductType.PANEL);
            listType.Add(StaticRes.Global.ProductType.LENS);
            listType.Add(StaticRes.Global.ProductType.BZ_257B);
            listType.Add(StaticRes.Global.ProductType.BZ_320B);
            

            foreach (string type in listType)
            {
                DataRow[] drArr = dtProduct.Select(" ProductType = '" + type + "'");
                if (drArr.Length == 0)
                {
                    DataRow dr = dtProduct.NewRow();
                    dr["SN"] = GetSN(type);
                    dr["ProductType"] = type;
                    dr["ManPower"] = ManPower;
                    dr["Attendance"] = Attendance;
                    dr["AttendRate"] = AttendRate;
                    dr["TargetQty"] = 0;
                    dr["ActualQty"] = 0;
                    dr["RejectQty"] = 0;
                    dr["TotalQty"] = 0;
                    dr["ActualHR"] = "00:00:00"; // 0;;
                    dr["TargetHR"] = "96:00:00"; // 12*8;
                    dr["RejRate"] = "0.00%";
                    dr["Utilization"] = Utilization;
                    dr["Productivity"] = Productivity;

                    dtProduct.Rows.Add(dr.ItemArray);
                }
                else
                {

                    drArr[0]["ActualHR"] = Common.CommFunctions.ConvertDateTimeShort(drArr[0]["ActualHR"].ToString());
                    drArr[0]["TargetHR"] = Common.CommFunctions.ConvertDateTimeShort(drArr[0]["TargetHR"].ToString());

                    drArr[0]["ManPower"] = ManPower;
                    drArr[0]["Attendance"] = Attendance;
                    drArr[0]["AttendRate"] = AttendRate;
                    drArr[0]["Utilization"] = Utilization;
                    drArr[0]["Productivity"] = Productivity;
                    
                }
            }

            DataRow drTesting = dtProduct.NewRow();
            drTesting["SN"] = GetSN("Project Testing");
            drTesting["ProductType"] = "Project Testing";
            drTesting["ActualHR"] = Common.CommFunctions.ConvertDateTimeShort(Math.Round(Total_TestingTime / 3600, 2).ToString());
            dtProduct.Rows.Add(drTesting.ItemArray);

            #endregion


            //order by sn
            dtProduct.Columns["SN"].DataType = typeof(int);
            dtProduct = dtProduct.Select("", "SN asc").CopyToDataTable();


            return dtProduct;
        }
        

      



        private int GetSN(string type)
        {
            int SN = 0;
            switch (type)
            {
                case StaticRes.Global.ProductType.BUTTON:
                    SN = 1;
                    break;
                case StaticRes.Global.ProductType.PRINT:
                    SN = 2;
                    break;
                case StaticRes.Global.ProductType.LENS:
                    SN = 3;
                    break;
                case StaticRes.Global.ProductType.BZ_257B:
                    SN = 4;
                    break;
                case StaticRes.Global.ProductType.BZ_320B:
                    SN = 5;
                    break;
                case StaticRes.Global.ProductType.PANEL:
                    SN = 6;
                    break;
                case "Project Testing":
                    SN = 7;
                    break;
                case "Day Shift Total":
                    SN = 99;
                    break;
                case "Night Shift Total":
                    SN = 99;
                    break;
                default:
                    SN = 10;
                    break;

            }
            return SN;
        }



        public DataTable GetOuputForAllMachineChart(DateTime DateFrom, DateTime DateTo, string Shift, string DateNotIn, bool ExceptWeekends)
        {
            DataTable dt = dal.GetOuput(DateFrom, DateTo, Shift, DateNotIn, ExceptWeekends);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            else
            {
                return dt;
            }
        }



        public DataTable GetLaserRejButtonReport_NEW(DateTime dateFrom, DateTime dateTo, string jobNo)
        {
            DataTable dt = dal.GetLaserRejForButtonReport_NEW(dateFrom, dateTo, jobNo);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return dt;
            }
        }








        #endregion




        public DataTable GetJobMaterialList(string sJobNo)
        {
            DataSet ds = dal.GetJobMaterialList(sJobNo);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }


        public DataTable GetMaterialListForAllSectionReport(DateTime  dStartTime)
        {
            DataSet ds = dal.GetMaterialListForAllSectionReport(dStartTime);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }


    }
}