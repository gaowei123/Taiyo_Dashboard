 
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
		public DataTable GetList(DateTime dDateFrom, DateTime dDateTo, string sTouchPC, string sRejType, string sRejCode, string sLotNo, string sPartNo)
		{
            DataSet ds = dal.GetList(dDateFrom, dDateTo, sTouchPC, sRejType, sRejCode, sLotNo, sPartNo);
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




            #region 2021/04/20 获取laser的 vision ng, setup, buyoff, shortage数量
            string strJobIn = "";
            DataTable dtTemp = dtDefectDetailList.DefaultView.ToTable(true, "Jobnumber");
            foreach (DataRow dr in dtTemp.Rows)
            {
                strJobIn += $"'{dr["Jobnumber"].ToString()}', ";
            }
            strJobIn = strJobIn.Substring(0, strJobIn.Length - 1);

            Common.BLL.LMMSWatchLog_BLL watchLogBLL = new Common.BLL.LMMSWatchLog_BLL();
            DataTable dtLaser = watchLogBLL.GetLaserQty(strJobIn);
            #endregion 2021/04/20 获取laser的 vision ng, setup, buyoff, shortage数量

            


            #region 遍历一个code一行的detail list, 转换成一个job一条的记录.
            foreach (DataRow dr in dtDefectDetailList.Rows)
            {
                // defect中的数据
                string jobNo = dr["Jobnumber"].ToString();
                decimal lotQty = decimal.Parse(dr["LotQty"].ToString());
                string defectCodeID = dr["defectCodeID"].ToString();
                string defectDescription = dr["defectDescription"].ToString();
                decimal rejQty = decimal.Parse(dr["rejectQty"].ToString());


                // 取出laser job的各种数量
                DataRow[] temp = dtLaser.Select($"jobnumber = '{jobNo}'");
                DataRow drLaser = temp == null || temp.Count() == 0 ? null : temp[0];
                decimal laserNG = decimal.Parse(drLaser["ngQty"].ToString());
                decimal laserSetup = decimal.Parse(drLaser["setupQty"].ToString());
                decimal laserBuyoff = decimal.Parse(drLaser["buyoffQty"].ToString());
                decimal laserShortage = decimal.Parse(drLaser["shortage"].ToString());



                // 先把laser的特殊数量赋值到查询出来的defect detail中, 后续操作直接取,汇总.
                if (dr["defectCode"].ToString() == "Graphic Shift check by M/C") dr["rejectQty"] = laserNG;
                if (dr["defectCode"].ToString() == "Paint Shortage") dr["rejectQty"] = laserShortage;
                if (dr["defectCode"].ToString() == "Laser Buyoff") dr["rejectQty"] = laserBuyoff;
                if (dr["defectCode"].ToString() == "Laser Setup") dr["rejectQty"] = laserSetup;



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
                catch (Exception)
                {
                    //由于修改defect setting的code之后
                    //defect tracking中历史的defect code无法对应到 defect setting中. 
                    //在根据defect setting动态生成的dtDefect中无法找到这些修改后的 defect code.导致报错.
                    //这里做跳过, 放弃这些数量. 
                    continue;
                }
            }
            #endregion




            //遍历转换后的dtPQCDefectOutPut
            foreach (DataRow dr in dtPQCDefectOutPut.Rows)
            {
                string jobNo = dr["JobNumber"].ToString();
                DataRow[] jobDefectList = dtDefectDetailList.Select($"JobNumber = '{jobNo}'");


               根据job汇总paint, mould, laser, other的总rej数量




            }



            foreach (KeyValuePair<string,int> kv in dicTotalRejForJob)
            {
                DataRow drJob = dtPQCDefectOutPut.Select(" JobNumber = '" + kv.Key + "'")[0];

                double lotQty = double.Parse(drJob["LotQty"].ToString());
                float unitCost = float.Parse(drJob["unitCost"].ToString());

                //defectSetting中对应codeID的 defect rej qty
                int particleRej = int.Parse(drJob["34"].ToString());  
                int fibreRej = int.Parse(drJob["35"].ToString());
                int manyParticleRej = int.Parse(drJob["36"].ToString());
                int dustRej = int.Parse(drJob["43"].ToString());
                int scratchRej = int.Parse(drJob["46"].ToString());




                drJob["Total REJ QTY"] = kv.Value;
                drJob["Total REJ AMT"] = "$" + Math.Round(kv.Value * unitCost, 2).ToString() ;
                drJob["Total Mould REJ%"] = Math.Round(dicMouldingRejForJob[kv.Key] / lotQty * 100.0, 2).ToString("0.00") + "%";
                drJob["Painting Particle REJ%"] = Math.Round(particleRej / lotQty * 100.0, 2).ToString("0.00") + "%";
                drJob["Painting Many Particle REJ%"] = Math.Round(manyParticleRej / lotQty * 100.0, 2).ToString("0.00") + "%";
                drJob["Painting Fiber REJ%"] = Math.Round(fibreRej / lotQty * 100.0, 2).ToString("0.00") + "%";

                drJob["Painting Dust REJ%"] = Math.Round(dustRej / lotQty * 100.0, 2).ToString("0.00") + "%";
                drJob["Painting Scratch REJ%"] = Math.Round(scratchRej / lotQty * 100.0, 2).ToString("0.00") + "%";



                drJob["Painting REJ%"] = Math.Round(dicPaintingRejForJob[kv.Key] / lotQty * 100.0, 2).ToString("0.00") + "%";
                drJob["Total Laser REJ%"] = Math.Round(dicLaserRejForJob[kv.Key] / lotQty * 100.0, 2).ToString("0.00") + "%";
                try
                {
                    drJob["Total Others REJ%"] = Math.Round(dicOthersRejForJob[kv.Key] / lotQty * 100.0, 2).ToString("0.00") + "%";
                }
                catch 
                {
                    drJob["Total Others REJ%"] = "0.00%";
                }
                
                drJob["Total REJ%"] = Math.Round(kv.Value / lotQty * 100.0, 2).ToString("0.00") + "%";
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
                DBHelp.Reports.LogFile.Log("TouchPCBuyoff", "GetDefectDetail,  in paint branch");

                dt = dal.GetPaintDefect(sJobID, sTrackingID, CheckProcess, IsExcludeTracking);
                //check#2 不经过laser, 不去累加laser的维护的 shortage
                //CheckProcess == "" 说明是overall buyoff调用的. 需要显示job的qa, setup, shortage   (2021-03-24)
                if (CheckProcess == "CHECK#1" || CheckProcess == "")
                {
                    #region 处理paint record中的setup,qa 和 laser inventory中的shortage.
                    Common.Class.BLL.LMMSInventoty_BLL inventoryBLL = new LMMSInventoty_BLL();
                    DataTable dtInventory = inventoryBLL.GetList(sJobID);

                    Common.Class.BLL.PaintingTempInfo paintBLL = new PaintingTempInfo();
                    DataTable dtPaintTemp = paintBLL.GetList(null, null, "", sJobID);

                    foreach (DataRow dr in dt.Rows)
                    {
                        double shortage = 0;
                        double setup = 0;
                        double qa = 0;

                        if (dtInventory != null && dtInventory.Rows.Count != 0)
                            shortage = double.Parse(dtInventory.Rows[0]["pqcQuantity"].ToString());


                        
                        if (dtPaintTemp != null && dtPaintTemp.Rows.Count != 0)
                        {
                            DataRow[] drs = dtPaintTemp.Select($"materialPartNo = '{dr["materialpartNo"].ToString()}'");
                            if (drs != null || drs.Count() != 0)
                            {
                                setup = double.Parse(drs[0]["setupRejQty"].ToString());
                                qa = double.Parse(drs[0]["qaTestQty"].ToString());
                            }                        }

                        dr["Shortage"] = shortage;
                        dr["Setup"] = setup;
                        dr["QA"] = qa;                    
                        dr["rejectQty"] = (double.Parse(dr["rejectQty"].ToString()) + shortage + setup + qa); 
                    }
                    #endregion
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

                #region 处理 qa
                //Common.Class.BLL.PaintingTempInfo paintBLL = new PaintingTempInfo();
                //DataTable dtPaintTemp = paintBLL.GetList(null, null, "", sJobID);

                //foreach (DataRow dr in dt.Rows)
                //{
                  
                //    double qa = 0;
                    

                //    if (dtPaintTemp != null && dtPaintTemp.Rows.Count != 0)
                //        qa = double.Parse(dtPaintTemp.Rows[0]["qaTestQty"].ToString());


                //    //dr["Shortage"] = shortage;
                //    dr["QA"] = qa;
                //    dr["rejectQty"] = (double.Parse(dr["rejectQty"].ToString()) + qa);
                //}
                #endregion
            }


            return dt;
        }



    }
}

