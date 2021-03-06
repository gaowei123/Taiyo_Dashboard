﻿ 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Common.Model;
using System.Linq;

namespace Common.Class.BLL
{
	/// <summary>
	/// PQCQaViDefectTracking_BLL
	/// </summary>
	public class PQCQaViDefectTracking_BLL
	{
        private readonly Common.Class.DAL.PQCQaViDefectTracking_DAL dal = new Common.Class.DAL.PQCQaViDefectTracking_DAL();

        public PQCQaViDefectTracking_BLL()
		{}
		#region  Method

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(Common.Class.Model.PQCQaViDefectTracking_Model model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public SqlCommand AddCommand(Common.Class.Model.PQCQaViDefectTracking_Model model)
		{
			return dal.AddCommand(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Common.Class.Model.PQCQaViDefectTracking_Model model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public SqlCommand UpdateCommand(Common.Class.Model.PQCQaViDefectTracking_Model model)
		{
			return dal.UpdateCommand(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.Delete();
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public SqlCommand DeleteCommand()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.DeleteCommand();
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public SqlCommand DeleteAllCommand()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.DeleteAllCommand();
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Common.Class.Model.PQCQaViDefectTracking_Model GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel();
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataTable GetList(DateTime dDateFrom, DateTime dDateTo, string sTouchPC, string sRejType, string sRejCode, string sPartNo, string sJobNo)
		{
            DataSet ds = dal.GetList(dDateFrom, dDateTo, sTouchPC, sRejType, sRejCode, sPartNo, sJobNo);
            if (ds == null || ds.Tables.Count==0)
            {
                return null;
            }


            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count == 0)
            {
                return null;
            }
            
            double totalRejQty = 0;

            foreach (DataRow dr in dt.Rows)
            {
                totalRejQty += double.Parse(dr["RejQty"].ToString());
            }


            DataRow drTotal = dt.NewRow();
            drTotal[0] = "Total";
            drTotal["RejQty"] = totalRejQty;


            dt.Rows.Add(drTotal);




            return dt;
        }


        
        public DataTable GetListByTrackingID(string sTrackingID)
        {
            DataSet ds = dal.GetListByTrackingID(sTrackingID);
            if (ds == null || ds.Tables.Count == 0)
                return null;


            return ds.Tables[0];

        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
	
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Common.Class.Model.PQCQaViDefectTracking_Model> DataTableToList(DataTable dt)
        {
            List<Common.Class.Model.PQCQaViDefectTracking_Model> modelList = new List<Common.Class.Model.PQCQaViDefectTracking_Model>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Common.Class.Model.PQCQaViDefectTracking_Model model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Common.Class.Model.PQCQaViDefectTracking_Model();
                    if (dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    model.jobid = dt.Rows[n]["jobid"].ToString();
                    model.trackingID = dt.Rows[n]["trackingID"].ToString();
                    model.machineID = dt.Rows[n]["machineID"].ToString();
                    if (dt.Rows[n]["dateTime"].ToString() != "")
                    {
                        model.dateTime = DateTime.Parse(dt.Rows[n]["dateTime"].ToString());
                    }
                    model.materialPartNo = dt.Rows[n]["materialPartNo"].ToString();
                    model.jigNo = dt.Rows[n]["jigNo"].ToString();
                    model.model = dt.Rows[n]["model"].ToString();
                    if (dt.Rows[n]["cavityCount"].ToString() != "")
                    {
                        model.cavityCount = decimal.Parse(dt.Rows[n]["cavityCount"].ToString());
                    }
                    model.userName = dt.Rows[n]["userName"].ToString();
                    model.userID = dt.Rows[n]["userID"].ToString();
                    if (dt.Rows[n]["startTime"].ToString() != "")
                    {
                        model.startTime = DateTime.Parse(dt.Rows[n]["startTime"].ToString());
                    }
                    if (dt.Rows[n]["stopTime"].ToString() != "")
                    {
                        model.stopTime = DateTime.Parse(dt.Rows[n]["stopTime"].ToString());
                    }
                    if (dt.Rows[n]["day"].ToString() != "")
                    {
                        model.day = DateTime.Parse(dt.Rows[n]["day"].ToString());
                    }
                    model.shift = dt.Rows[n]["shift"].ToString();
                    model.status = dt.Rows[n]["status"].ToString();
                    model.remark_1 = dt.Rows[n]["remark_1"].ToString();
                    model.remark_2 = dt.Rows[n]["remark_2"].ToString();
                    model.defectCodeID = dt.Rows[n]["defectCodeID"].ToString();
                    model.defectCode = dt.Rows[n]["defectCode"].ToString();
                    model.defectDescription = dt.Rows[n]["defectDescription"].ToString();
                    if (dt.Rows[n]["rejectQty"].ToString() != "")
                    {
                        model.rejectQty = decimal.Parse(dt.Rows[n]["rejectQty"].ToString());
                    }
                    model.rejectQtyHour01 = dt.Rows[n]["rejectQtyHour01"].ToString();
                    model.rejectQtyHour02 = dt.Rows[n]["rejectQtyHour02"].ToString();
                    model.rejectQtyHour03 = dt.Rows[n]["rejectQtyHour03"].ToString();
                    model.rejectQtyHour04 = dt.Rows[n]["rejectQtyHour04"].ToString();
                    model.rejectQtyHour05 = dt.Rows[n]["rejectQtyHour05"].ToString();
                    model.rejectQtyHour06 = dt.Rows[n]["rejectQtyHour06"].ToString();
                    model.rejectQtyHour07 = dt.Rows[n]["rejectQtyHour07"].ToString();
                    model.rejectQtyHour08 = dt.Rows[n]["rejectQtyHour08"].ToString();
                    model.rejectQtyHour09 = dt.Rows[n]["rejectQtyHour09"].ToString();
                    model.rejectQtyHour10 = dt.Rows[n]["rejectQtyHour10"].ToString();
                    model.rejectQtyHour11 = dt.Rows[n]["rejectQtyHour11"].ToString();
                    model.rejectQtyHour12 = dt.Rows[n]["rejectQtyHour12"].ToString();
                    if (dt.Rows[n]["lastUpdatedTime"].ToString() != "")
                    {
                        model.lastUpdatedTime = DateTime.Parse(dt.Rows[n]["lastUpdatedTime"].ToString());
                    }
                    model.remarks = dt.Rows[n]["remarks"].ToString();
                    model.processes = dt.Rows[n]["processes"].ToString();
                    if (dt.Rows[n]["updatedTime"].ToString() != "")
                    {
                        model.updatedTime = DateTime.Parse(dt.Rows[n]["updatedTime"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }



        public Common.Class.Model.PQCQaViDefectTracking_Model CopyObj(Common.Class.Model.PQCQaViDefectTracking_Model objModel)
        {
            Common.Class.Model.PQCQaViDefectTracking_Model model;
            model = new Common.Class.Model.PQCQaViDefectTracking_Model();
            model.id = objModel.id;
            model.jobid = objModel.jobid;
            model.trackingID = objModel.trackingID;
            model.machineID = objModel.machineID;
            model.dateTime = objModel.dateTime;
            model.materialPartNo = objModel.materialPartNo;
            model.jigNo = objModel.jigNo;
            model.model = objModel.model;
            model.cavityCount = objModel.cavityCount;
            model.userName = objModel.userName;
            model.userID = objModel.userID;
            model.startTime = objModel.startTime;
            model.stopTime = objModel.stopTime;
            model.day = objModel.day;
            model.shift = objModel.shift;
            model.status = objModel.status;
            model.remark_1 = objModel.remark_1;
            model.remark_2 = objModel.remark_2;
            model.defectCodeID = objModel.defectCodeID;
            model.defectCode = objModel.defectCode;
            model.defectDescription = objModel.defectDescription;
            model.rejectQty = objModel.rejectQty;
            model.rejectQtyHour01 = objModel.rejectQtyHour01;
            model.rejectQtyHour02 = objModel.rejectQtyHour02;
            model.rejectQtyHour03 = objModel.rejectQtyHour03;
            model.rejectQtyHour04 = objModel.rejectQtyHour04;
            model.rejectQtyHour05 = objModel.rejectQtyHour05;
            model.rejectQtyHour06 = objModel.rejectQtyHour06;
            model.rejectQtyHour07 = objModel.rejectQtyHour07;
            model.rejectQtyHour08 = objModel.rejectQtyHour08;
            model.rejectQtyHour09 = objModel.rejectQtyHour09;
            model.rejectQtyHour10 = objModel.rejectQtyHour10;
            model.rejectQtyHour11 = objModel.rejectQtyHour11;
            model.rejectQtyHour12 = objModel.rejectQtyHour12;
            model.lastUpdatedTime = objModel.lastUpdatedTime;
            model.remarks = objModel.remarks;
            model.processes = objModel.processes;
            model.updatedTime = objModel.updatedTime;
            return model;
        }
        #endregion  Method

        

        public SqlCommand UpdateJob(Common.Class.Model.PQCQaViDefectTracking_Model model,SqlCommand cmd=null)
        {
            return dal.UpdateJob(model,cmd);
        }

        public DataTable getPQCDefectForBezelPanelReport(DateTime dDateFrom, DateTime dDateTo, string sType, string sDescription,string sNumber)
        {
            // 从defect tracking表中 defect rej等信息
            DataTable dtDefectDetailList = dal.GetDefectDetailForBezelPanelReport(dDateFrom, dDateTo, sType, sDescription, sNumber);
            if(dtDefectDetailList == null || dtDefectDetailList.Rows.Count == 0 )
                return null;


            
            // 从defect setting表中获取所有的defect code
            Common.Class.BLL.PQCDefectSetting_BLL defectSettingBLL = new BLL.PQCDefectSetting_BLL();
            DataTable dtDefectSetting = defectSettingBLL.GetDefectSetting();
            if (dtDefectSetting == null || dtDefectSetting.Rows.Count == 0)
                return null;



            #region  设置defect信息表的结构
            DataTable dtPQCDefectOutPut = new DataTable();
            dtPQCDefectOutPut.Columns.Add("JobNumber");
            dtPQCDefectOutPut.Columns.Add("lotQty");
            dtPQCDefectOutPut.Columns.Add("unitCost");

            foreach (DataRow dr in dtDefectSetting.Rows)
            {
                DataColumn dc = new DataColumn();
                dc.ColumnName = dr["defectCodeID"].ToString();
                dc.Caption = dr["defectCode"].ToString();
                dtPQCDefectOutPut.Columns.Add(dc);
            }

            dtPQCDefectOutPut.Columns.Add("Total REJ QTY");
            dtPQCDefectOutPut.Columns.Add("Total REJ AMT");
            dtPQCDefectOutPut.Columns.Add("Total Mould REJ%");
            dtPQCDefectOutPut.Columns.Add("Painting Particle REJ%");
            dtPQCDefectOutPut.Columns.Add("Painting Many Particle REJ%");
            dtPQCDefectOutPut.Columns.Add("Painting Fiber REJ%");
            dtPQCDefectOutPut.Columns.Add("Painting Dust REJ%");
            dtPQCDefectOutPut.Columns.Add("Painting Scratch REJ%");
            dtPQCDefectOutPut.Columns.Add("Painting REJ%");
            dtPQCDefectOutPut.Columns.Add("Total Laser REJ%");
            dtPQCDefectOutPut.Columns.Add("Total Others REJ%");
            dtPQCDefectOutPut.Columns.Add("Total REJ%");
            #endregion




            // 2021/04/20 获取laser的 vision ng, setup, buyoff, shortage数量
            string strJobIn = "";
            DataTable dtTemp = dtDefectDetailList.DefaultView.ToTable(true, "Jobnumber");
            foreach (DataRow dr in dtTemp.Rows)
            {
                strJobIn += $"'{dr["Jobnumber"].ToString()}',";
            }
            strJobIn = strJobIn.Substring(0, strJobIn.Length - 1);

            Common.BLL.LMMSWatchLog_BLL watchLogBLL = new Common.BLL.LMMSWatchLog_BLL();
            DataTable dtLaser = watchLogBLL.GetLaserQty(strJobIn);





            #region 提前处理 laser 的数据到 dtDefectDetailList 中
            foreach (DataRow dr in dtLaser.Rows)
            {
                string jobNo = dr["jobNumber"].ToString();
                decimal laserNG = decimal.Parse(dr["ngQty"].ToString());
                decimal laserSetup = decimal.Parse(dr["setupQty"].ToString());
                decimal laserBuyoff = decimal.Parse(dr["buyoffQty"].ToString());
                decimal laserShortage = decimal.Parse(dr["shortage"].ToString());


                // 随便取一条用来赋值一些信息.
                DataRow[] jobDefects = dtDefectDetailList.Select($" Jobnumber = '{jobNo}' ");



                // 处理 laser ng
                DataRow visionDefect = dtDefectDetailList.Select($" Jobnumber = '{jobNo}' and defectCode = 'Graphic Shift check by M/C' ")[0];
                visionDefect["rejectQty"] = laserNG;



                // 处理 shortage 合并到 painting 的 setup 中.               
                DataRow[] paintSetupDefect = dtDefectDetailList.Select($" Jobnumber = '{jobNo}' and defectCode = 'Setup' and defectDescription = 'Paint' ");
                if (paintSetupDefect != null && paintSetupDefect.Count() != 0)
                {
                    decimal setup = paintSetupDefect[0]["rejectQty"].ToString() == "" ? 0 : decimal.Parse(paintSetupDefect[0]["rejectQty"].ToString());
                    paintSetupDefect[0]["rejectQty"] = (laserBuyoff + setup);
                }
                else
                {
                    // 在修改 defect setting 之前是没有 paint setup 的, 没有则新增一条

                    string setupCodeID;

                    DataRow[] defectsettingPaintSetup = dtDefectSetting.Select(" defectDescription = 'Paint' and  defectCodeSource = 'Setup' ");
                    if (defectsettingPaintSetup == null || defectsettingPaintSetup.Count() == 0)
                    {
                        setupCodeID = defectsettingPaintSetup[0]["defectCodeID"].ToString();
                    }
                    else
                    {
                        setupCodeID = "100";
                    }

                    DataRow drSpecialCodeShortage = dtDefectDetailList.NewRow();
                    drSpecialCodeShortage["Jobnumber"] = jobDefects[0]["Jobnumber"].ToString();
                    drSpecialCodeShortage["LotQty"] = jobDefects[0]["LotQty"].ToString();
                    drSpecialCodeShortage["unitCost"] = jobDefects[0]["unitCost"].ToString();
                    drSpecialCodeShortage["defectCodeID"] = setupCodeID;
                    drSpecialCodeShortage["defectDescription"] = "Paint";
                    drSpecialCodeShortage["defectCode"] = "Setup";
                    drSpecialCodeShortage["rejectQty"] = laserShortage;
                    dtDefectDetailList.Rows.Add(drSpecialCodeShortage);
                }


                // 新增 laser buyoff
                DataRow drSpecialCodeBuyoff = dtDefectDetailList.NewRow();
                drSpecialCodeBuyoff["Jobnumber"] = jobDefects[0]["Jobnumber"].ToString();
                drSpecialCodeBuyoff["LotQty"] = jobDefects[0]["LotQty"].ToString();
                drSpecialCodeBuyoff["unitCost"] = jobDefects[0]["unitCost"].ToString();
                drSpecialCodeBuyoff["defectCodeID"] = 101;
                drSpecialCodeBuyoff["defectDescription"] = "Laser";
                drSpecialCodeBuyoff["defectCode"] = "Laser Buyoff";
                drSpecialCodeBuyoff["rejectQty"] = laserBuyoff;
                dtDefectDetailList.Rows.Add(drSpecialCodeBuyoff);




                // 新增 laser setup
                DataRow drSpecialCodeSetup = dtDefectDetailList.NewRow();
                drSpecialCodeSetup["Jobnumber"] = jobDefects[0]["Jobnumber"].ToString();
                drSpecialCodeSetup["LotQty"] = jobDefects[0]["LotQty"].ToString();                
                drSpecialCodeSetup["unitCost"] = jobDefects[0]["unitCost"].ToString();
                drSpecialCodeSetup["defectCodeID"] = 102;
                drSpecialCodeSetup["defectDescription"] = "Laser";
                drSpecialCodeSetup["defectCode"] = "Laser Setup";
                drSpecialCodeSetup["rejectQty"] = laserSetup;
                dtDefectDetailList.Rows.Add(drSpecialCodeSetup);
                

            }
            #endregion





            #region 遍历一个code一行的detail list, 转换成一个job一条的记录.
            foreach (DataRow dr in dtDefectDetailList.Rows)
            {
                // defect中的数据
                string jobNo = dr["Jobnumber"].ToString();
                decimal lotQty = decimal.Parse(dr["LotQty"].ToString());
                string defectCodeID = dr["defectCodeID"].ToString();
                string defectDescription = dr["defectDescription"].ToString();
                decimal rejQty = decimal.Parse(dr["rejectQty"].ToString());

                
                try
                {
                    //如果加过这个job信息, 就更新遍历到defectcode的rej数量.
                    if (dtPQCDefectOutPut.Select($"jobnumber = '{jobNo}'").Length > 0)
                    {
                        string sRejTemp = dtPQCDefectOutPut.Select($"jobnumber = '{jobNo}'")[0][defectCodeID].ToString();
                        decimal RejQtyTemp = sRejTemp == "" ? 0 : int.Parse(sRejTemp);
                        dtPQCDefectOutPut.Select($"jobnumber = '{jobNo}'")[0][defectCodeID] = RejQtyTemp + rejQty;
                    }
                    else
                    {
                        DataRow drPQCDefect = dtPQCDefectOutPut.NewRow();
                        drPQCDefect["jobnumber"] = jobNo;
                        drPQCDefect["lotQty"] = lotQty;
                        drPQCDefect["unitCost"] = dr["unitCost"].ToString();
                        drPQCDefect[defectCodeID] = rejQty;
                        dtPQCDefectOutPut.Rows.Add(drPQCDefect);
                    }
                }
                catch (Exception ee)
                {
                    // 由于 defect setting 中的 defect code 以及对应 id 修改过后，
                    // tracking 记录保留这之前的 defect code & id 无法在 defect setting 中找到而导致报错。 
                    // 所以在 catch 到错误后跳过这个 code rej 数量， 不做处理（反正后面也不算这些code了）
                    continue;
                }
            }
            #endregion




            //遍历转换后的 dtPQCDefectOutPut 中的每一个 job
            //再通过 dtDefectDetailList 中将 mould, paint, laser, other rej 汇总起来
            foreach (DataRow dr in dtPQCDefectOutPut.Rows)
            {
                string jobNo = dr["JobNumber"].ToString();
                DataTable dtDefectListByJob = dtDefectDetailList.Select($"JobNumber = '{jobNo}'").CopyToDataTable();

                string temp = dtDefectListByJob.Compute("sum(rejectQty) ", $"defectDescription = 'Mould'").ToString();
                decimal allMouldDefectRej = decimal.Parse(temp);

                temp = dtDefectListByJob.Compute("sum(rejectQty) ", $"defectDescription = 'Paint'").ToString();
                decimal allPaintDefectRej = decimal.Parse(temp);

                temp = dtDefectListByJob.Compute("sum(rejectQty) ", $"defectDescription = 'Laser'").ToString();
                decimal allLaserDefectRej = decimal.Parse(temp);

                temp = dtDefectListByJob.Compute("sum(rejectQty) ", $"defectDescription = 'Others'").ToString();
                decimal allOthersDefectRej = decimal.Parse(temp);



                decimal lotQty = decimal.Parse(dr["LotQty"].ToString());
                decimal unitCost = decimal.Parse(dr["unitCost"].ToString());

                // 特殊 code 需要单独计算 rejrate.
                // Painting Particle  -- 34
                // Painting Many Particle -- 36
                // Painting Fiber -- 35
                // Painting Dust -- 43
                // Painting Scratch -- 46
                decimal particleRej = decimal.Parse(dr["34"].ToString());
                decimal manyParticleRej = decimal.Parse(dr["36"].ToString());
                decimal fibreRej = decimal.Parse(dr["35"].ToString());
                decimal dustRej = decimal.Parse(dr["43"].ToString());
                decimal scratchRej = decimal.Parse(dr["46"].ToString());





                dr["Total REJ QTY"] = allMouldDefectRej + allPaintDefectRej + allLaserDefectRej + allOthersDefectRej;
                dr["Total REJ AMT"] = "$" + Math.Round((allMouldDefectRej + allPaintDefectRej + allLaserDefectRej + allOthersDefectRej) * unitCost, 2).ToString();
                dr["Total Mould REJ%"] = Math.Round(allMouldDefectRej / lotQty * 100, 2).ToString("0.00") + "%";

                dr["Painting Particle REJ%"] = Math.Round(particleRej / lotQty * 100, 2).ToString("0.00") + "%";
                dr["Painting Many Particle REJ%"] = Math.Round(manyParticleRej / lotQty * 100, 2).ToString("0.00") + "%";
                dr["Painting Fiber REJ%"] = Math.Round(fibreRej / lotQty * 100, 2).ToString("0.00") + "%";
                dr["Painting Dust REJ%"] = Math.Round(dustRej / lotQty * 100, 2).ToString("0.00") + "%";
                dr["Painting Scratch REJ%"] = Math.Round(scratchRej / lotQty * 100, 2).ToString("0.00") + "%";
                dr["Painting REJ%"] = Math.Round(allPaintDefectRej / lotQty * 100, 2).ToString("0.00") + "%";

                dr["Total Laser REJ%"] = Math.Round(allLaserDefectRej / lotQty * 100, 2).ToString("0.00") + "%";

                dr["Total Others REJ%"] = Math.Round(allOthersDefectRej / lotQty * 100, 2).ToString("0.00") + "%";
                
                dr["Total REJ%"] = Math.Round((allMouldDefectRej + allPaintDefectRej + allLaserDefectRej + allOthersDefectRej) / lotQty * 100, 2).ToString("0.00") + "%";


            }


            return dtPQCDefectOutPut;
        }

        

        public DataTable GetVIDefectForButtonReport_NEW(string sqlWhere)
        {

            DataTable dtViDetailTracking = new DataTable();
            dtViDetailTracking = dal.GetVIDefectForButtonReport_NEW(sqlWhere);

            if (dtViDetailTracking == null || dtViDetailTracking.Rows.Count == 0)
                return null;
            else
                return dtViDetailTracking;
        }
        

        public List<Common.Class.Model.PQCQaViDefectTracking_Model> GetModelList(string sTrackingID)
        {
            DataSet ds = dal.GetListByTrackingID(sTrackingID);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }



            DataTable dt = ds.Tables[0];

            return DataTableToList(dt);
        }


        public DataTable GetDefectDetail(string sJobID, string sTrackingID, string sDefectDescription, string CheckProcess, bool IsExcludeTracking)
        {
            DataTable dt = new DataTable();
            
            if (sDefectDescription == "Mould")
            {
                dt = dal.GetMouldDefect(sJobID, sTrackingID, CheckProcess, IsExcludeTracking);
            }
            else if (sDefectDescription == "Paint")
            {
                dt = dal.GetPaintDefect(sJobID, sTrackingID, CheckProcess, IsExcludeTracking);


                // 2021-03-24
                // check#2 不经过laser, 不去累加laser的维护的 shortage
                // CheckProcess == "" 说明是overall buyoff调用的. 需要显示job的qa, setup, shortage   (
                

                // 2021-04-22
                // qa, setup 放到 defect setting 中了.不用单独去查询
                // 只需要查询 laser 维护的 shortage 数量.
                if (CheckProcess == "CHECK#1" || CheckProcess == "")
                {
                    Common.Class.BLL.LMMSInventoty_BLL inventoryBLL = new LMMSInventoty_BLL();
                    DataTable dtInventory = inventoryBLL.GetList(sJobID);
                    
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dtInventory != null && dtInventory.Rows.Count != 0)
                        {
                            double shortage = double.Parse(dtInventory.Rows[0]["pqcQuantity"].ToString());
                            dr["Setup"] = double.Parse(dr["Setup"].ToString()) + shortage;
                            dr["rejectQty"] = double.Parse(dr["rejectQty"].ToString()) + shortage;
                        }
                    }
                }
            }
            else if (sDefectDescription == "Laser")
            {
                dt = dal.GetLaserDefect(sJobID, sTrackingID, CheckProcess, IsExcludeTracking);


                //check#2 不经过laser, 不去累加laser的vision ng
                //CheckProcess == "" 说明是overall buyoff调用的. 需要显示job的qa, setup, shortage   (2021-03-24)
                if (CheckProcess == "CHECK#1" || CheckProcess == "")
                {
                    #region  处理 laser ng, setup, buyoff
                    Common.Class.BLL.LMMSInventoty_BLL inventoryBLL = new LMMSInventoty_BLL();
                    DataTable dtInventory = inventoryBLL.GetList(sJobID);

                    Common.BLL.LMMSWatchLog_BLL watchLogBLL = new Common.BLL.LMMSWatchLog_BLL();
                    DataTable dtWatchLog = watchLogBLL.GetJobMaterialList(sJobID);


                    foreach (DataRow dr in dt.Rows)
                    {
                        string materialNo = dr["materialpartNo"].ToString();

                        double laserMaterialNG = 0;
                        double buyoff = 0;
                        double setup = 0;

                        //watchlog 中获取laser material ng的数量
                        if (dtWatchLog != null && dtWatchLog.Rows.Count != 0)
                        {
                            DataRow[] drArrTemp = dtWatchLog.Select("materialNo = '" + materialNo + "'");

                            if (drArrTemp.Length > 0)
                            {
                                laserMaterialNG = double.Parse(drArrTemp[0]["ngQty"].ToString());
                                dr["Graphic Shift check by M/C"] = laserMaterialNG;
                            }
                        }


                        //laser inventory 中获取 setup, buyoff数量.
                        if (dtInventory != null && dtInventory.Rows.Count != 0)
                        {
                            buyoff = double.Parse(dtInventory.Rows[0]["buyOffQty"].ToString());
                            setup = double.Parse(dtInventory.Rows[0]["setUpQTY"].ToString());
                            dr["Buyoff"] = buyoff;
                            dr["Setup"] = setup;
                        }

                        dr["rejectQty"] = (double.Parse(dr["rejectQty"].ToString()) + buyoff + setup + laserMaterialNG);
                    }
                    #endregion
                }
            }
            else if (sDefectDescription == "Others")
            {
                dt = dal.GetOthersDefect(sJobID, sTrackingID, CheckProcess, IsExcludeTracking);
            }


            return dt;
        }



    }
}

