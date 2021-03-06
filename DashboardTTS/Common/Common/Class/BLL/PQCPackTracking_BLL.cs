﻿/**  版本信息模板在安装目录下，可自行修改。
* PQCPackTracking.cs
*
* 功 能： N/A
* 类 名： PQCPackTracking
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/1/30 21:14:51   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Common.Class.BLL
{
	/// <summary>
	/// PQCPackTracking
	/// </summary>
	public partial class PQCPackTracking
	{
		private readonly Common.Class.DAL.PQCPackTracking_DAL dal=new Common.Class.DAL.PQCPackTracking_DAL();
		public PQCPackTracking()
		{}
		

		

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Common.Class.Model.PQCPackTracking_Model GetModel(string trackingID)
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel(trackingID);
		}


		public List<Common.Class.Model.PQCPackTracking_Model> DataTableToList(DataTable dt)
		{
			List<Common.Class.Model.PQCPackTracking_Model> modelList = new List<Common.Class.Model.PQCPackTracking_Model>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Common.Class.Model.PQCPackTracking_Model model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

	

        public DataTable GetList(DateTime dDateFrom, DateTime dDateTo, string sShift, string sPartNo, string sStation, string sPIC, string sJobNo)
        {
            return dal.GetList(dDateFrom, dDateTo, sShift, sPartNo, sStation, sPIC, sJobNo);
        }

        public DataTable GetProductDetailList(DateTime dDateFrom, DateTime dDateTo, string sShift, string sPartNumber, string sMachineID, string sJobNumber, string sLotNo)
        {
            DataTable dt = dal.GetProductDetailList(dDateFrom, dDateTo, sShift, sPartNumber, sMachineID, sJobNumber);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }


            dt.Columns.Add("RejRate");
            dt.Columns.Add("Time");



            Common.Class.BLL.PaintingDeliveryHis_BLL paintBLL = new PaintingDeliveryHis_BLL();
            DataTable dtPaint = paintBLL.GetList(dDateFrom.AddMonths(-4), dDateTo.AddDays(10), "");
            


            double totalSecond = 0;
            foreach (DataRow row in dt.Rows)
            {
                //合并 lotno
                string jobNo = row["jobID"].ToString();
                DataRow[] arrRowPaint = dtPaint.Select(" jobnumber = '" + jobNo + "'");
                if (arrRowPaint.Length>0)
                {
                    row["lotNo"] = arrRowPaint[0]["lotNo"].ToString();
                }



                double totalQty = double.Parse(row["TotalQty"].ToString());
                double ng = double.Parse(row["rejectQty"].ToString());
                double rejRate = totalQty == 0 ? 0 : Math.Round(ng / totalQty * 100, 2);

                row["RejRate"] = rejRate.ToString("0.00") + "%";




            

                if (row["startTime"].ToString() == "" || row["stopTime"].ToString() == "")
                    continue;
                
                double seconds = (DateTime.Parse(row["stopTime"].ToString()) - DateTime.Parse(row["startTime"].ToString())).TotalSeconds;




                row["Time"] = Common.CommFunctions.ConvertDateTimeShort((seconds / 3600).ToString());

                totalSecond += seconds;
            }

            if (sLotNo != "")
            {
                dt = dt.Select(" lotNo = '"+ sLotNo + "' ", "datetime desc").CopyToDataTable();
            }
            else
            {
                dt = dt.Select("", "datetime desc").CopyToDataTable();
            }
            


            //添加total row
            DataRow dr = dt.NewRow();

            double totalOK = double.Parse(dt.Compute(" sum(acceptQty) ","").ToString());
            double totalNG = double.Parse(dt.Compute(" sum(rejectQty)", "").ToString());
            double totalOutput = double.Parse(dt.Compute(" sum(TotalQty)", "").ToString());



            dr["Shift"] = "Total :";
            dr["acceptQty"] = totalOK;
            dr["rejectQty"] = totalNG;
            dr["TotalQty"] = totalOutput;
            dr["RejRate"] = Math.Round(totalNG / totalOutput * 100, 2).ToString() + "%";
            dr["Time"] = Common.CommFunctions.ConvertDateTimeShort((totalSecond / 3600).ToString());

            dt.Rows.Add(dr);


            return dt;
        }

        public SqlCommand UpdateCommand(Model.PQCPackTracking_Model model)
        {
            return dal.UpdatePQCMaintenance(model);
        }

        public SqlCommand AddCommand(Model.PQCPackTracking_Model model)
        {
            return dal.AddCommand(model);
        }


        public DataTable GetPackInventoryDetailList(DateTime dDateFrom, DateTime dDateTo, string sPartNo, string sJobNo)
        {
            return dal.GetPackInventoryDetailList(dDateFrom, dDateTo, sPartNo, sJobNo);
        }

    }
}

