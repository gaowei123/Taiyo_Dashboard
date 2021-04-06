/**  版本信息模板在安装目录下，可自行修改。
* PQCQaViBinning.cs
*
* 功 能： N/A
* 类 名： PQCQaViBinning
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/4/10 10:15:12   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using Common.Class.Model;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
    /// <summary>
    /// PQCQaViBinning
    /// </summary>
    public partial class PQCQaViBinning
    {
        private readonly Common.Class.DAL.PQCQaViBinning dal = new Common.Class.DAL.PQCQaViBinning();
        public PQCQaViBinning()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Common.Class.Model.PQCQaViBinning model)
        {
            return dal.Add(model);
        }

        public SqlCommand AddCommand(Common.Class.Model.PQCQaViBinning model)
        {
            return dal.AddCommand(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Class.Model.PQCQaViBinning model)
        {
            return dal.Update(model);
        }




        public SqlCommand UpdateCommand(Common.Class.Model.PQCQaViBinning model)
        {
            return dal.UpdateCommand(model);
        }

        public SqlCommand DeleteCheckCommand(string sTrackingID, string sMaterialPartNo)
        {
            return dal.DeleteCheckCommand(sTrackingID, sMaterialPartNo);
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
        /// 得到一个对象实体
        /// </summary>
        public Common.Class.Model.PQCQaViBinning GetModel(string sTrackingID)
        {
            //该表无主键信息，请自定义主键/条件字段
            return dal.GetModel(sTrackingID);
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
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
         
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Common.Class.Model.PQCQaViBinning> GetModelList(DateTime? dDateFrom, DateTime? dDateTo, string sJobID, string sCheckProcess)
        {
            DataSet ds = dal.GetList(dDateFrom, dDateTo, sJobID, sCheckProcess);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count  == 0)
            {
                return null;
            }


            return DataTableToList(ds.Tables[0]);
        }


        public List<Common.Class.Model.PQCQaViBinning> GetModelList(string sTrackingID)
        {
            DataSet ds = dal.GetList(sTrackingID);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            return DataTableToList(ds.Tables[0]);
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Common.Class.Model.PQCQaViBinning> DataTableToList(DataTable dt)
        {
            List<Common.Class.Model.PQCQaViBinning> modelList = new List<Common.Class.Model.PQCQaViBinning>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Common.Class.Model.PQCQaViBinning model;
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




        #endregion  BasicMethod






        public DataTable GetBinInfoForAllInventoryReport(DateTime dStartTime)
        {
            return dal.GetBinInfoForAllInventoryReport(dStartTime);
        }


        /// <summary>
        /// 获取当天的fg/assembly的数量.  
        /// 直接通过packing qty / outerboxqty能被整除的数量,即为fg/assembly的数量.
        /// </summary>
        public DataTable GetFgAndAssembly()
        {
            return dal.GetFgAndAssembly();
        }

        
    }
}

