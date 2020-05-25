using System;
using System.Data;
using System.Collections.Generic;
using Common.Model;
using Common.DAL;
namespace Common.BLL
{
    /// <summary>
    /// MouldingLabel_BLL
    /// </summary>
    public class MouldingLabel_BLL
    {
        private readonly MouldingLabel_DAL dal = new MouldingLabel_DAL();
        public MouldingLabel_BLL()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string trackingID, string machineID, decimal UsageQTY01, decimal UsageQTY02, decimal UsageQTY03, decimal UsageQTY04, decimal UsageQTY05, decimal UsageQTY06, decimal UsageQTY07, decimal UsageQTY08, decimal UsageQTY09, decimal UsageQTY10, decimal UsageQTY11, decimal UsageQTY12, decimal RejectQTY01, decimal RejectQTY02, decimal RejectQTY03, decimal RejectQTY04, decimal RejectQTY05, decimal RejectQTY06, decimal RejectQTY07, decimal RejectQTY08, decimal RejectQTY09, decimal RejectQTY10, decimal RejectQTY11, decimal RejectQTY12, decimal SerialNo01, decimal SerialNo02, decimal SerialNo03, decimal SerialNo04, decimal SerialNo05, decimal SerialNo06, decimal SerialNo07, decimal SerialNo08, decimal SerialNo09, decimal SerialNo10, decimal SerialNo11, decimal SerialNo12, decimal SerialNoEnd, string SerialNo)
        {
            DataSet ds = new DataSet();
            ds = dal.Exists(trackingID, machineID, UsageQTY01, UsageQTY02, UsageQTY03, UsageQTY04, UsageQTY05, UsageQTY06, UsageQTY07, UsageQTY08, UsageQTY09, UsageQTY10, UsageQTY11, UsageQTY12, RejectQTY01, RejectQTY02, RejectQTY03, RejectQTY04, RejectQTY05, RejectQTY06, RejectQTY07, RejectQTY08, RejectQTY09, RejectQTY10, RejectQTY11, RejectQTY12, SerialNo01, SerialNo02, SerialNo03, SerialNo04, SerialNo05, SerialNo06, SerialNo07, SerialNo08, SerialNo09, SerialNo10, SerialNo11, SerialNo12, SerialNoEnd, SerialNo);
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ExistsbytrackingID(string trackingID)
        {
            DataSet ds = new DataSet();
            ds = dal.ExistsbytrackingID(trackingID);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows[0][0].ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(Common.Model.MouldingLabel_Model model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Model.MouldingLabel_Model model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string trackingID, string machineID, decimal UsageQTY01, decimal UsageQTY02, decimal UsageQTY03, decimal UsageQTY04, decimal UsageQTY05, decimal UsageQTY06, decimal UsageQTY07, decimal UsageQTY08, decimal UsageQTY09, decimal UsageQTY10, decimal UsageQTY11, decimal UsageQTY12, decimal RejectQTY01, decimal RejectQTY02, decimal RejectQTY03, decimal RejectQTY04, decimal RejectQTY05, decimal RejectQTY06, decimal RejectQTY07, decimal RejectQTY08, decimal RejectQTY09, decimal RejectQTY10, decimal RejectQTY11, decimal RejectQTY12, decimal SerialNo01, decimal SerialNo02, decimal SerialNo03, decimal SerialNo04, decimal SerialNo05, decimal SerialNo06, decimal SerialNo07, decimal SerialNo08, decimal SerialNo09, decimal SerialNo10, decimal SerialNo11, decimal SerialNo12, decimal SerialNoEnd, string SerialNo)
        {

            return dal.Delete(trackingID, machineID, UsageQTY01, UsageQTY02, UsageQTY03, UsageQTY04, UsageQTY05, UsageQTY06, UsageQTY07, UsageQTY08, UsageQTY09, UsageQTY10, UsageQTY11, UsageQTY12, RejectQTY01, RejectQTY02, RejectQTY03, RejectQTY04, RejectQTY05, RejectQTY06, RejectQTY07, RejectQTY08, RejectQTY09, RejectQTY10, RejectQTY11, RejectQTY12, SerialNo01, SerialNo02, SerialNo03, SerialNo04, SerialNo05, SerialNo06, SerialNo07, SerialNo08, SerialNo09, SerialNo10, SerialNo11, SerialNo12, SerialNoEnd, SerialNo);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Common.Model.MouldingLabel_Model GetModel(string trackingID, string machineID, decimal UsageQTY01, decimal UsageQTY02, decimal UsageQTY03, decimal UsageQTY04, decimal UsageQTY05, decimal UsageQTY06, decimal UsageQTY07, decimal UsageQTY08, decimal UsageQTY09, decimal UsageQTY10, decimal UsageQTY11, decimal UsageQTY12, decimal RejectQTY01, decimal RejectQTY02, decimal RejectQTY03, decimal RejectQTY04, decimal RejectQTY05, decimal RejectQTY06, decimal RejectQTY07, decimal RejectQTY08, decimal RejectQTY09, decimal RejectQTY10, decimal RejectQTY11, decimal RejectQTY12, decimal SerialNo01, decimal SerialNo02, decimal SerialNo03, decimal SerialNo04, decimal SerialNo05, decimal SerialNo06, decimal SerialNo07, decimal SerialNo08, decimal SerialNo09, decimal SerialNo10, decimal SerialNo11, decimal SerialNo12, decimal SerialNoEnd, string SerialNo)
        {

            return dal.GetModel(trackingID, machineID, UsageQTY01, UsageQTY02, UsageQTY03, UsageQTY04, UsageQTY05, UsageQTY06, UsageQTY07, UsageQTY08, UsageQTY09, UsageQTY10, UsageQTY11, UsageQTY12, RejectQTY01, RejectQTY02, RejectQTY03, RejectQTY04, RejectQTY05, RejectQTY06, RejectQTY07, RejectQTY08, RejectQTY09, RejectQTY10, RejectQTY11, RejectQTY12, SerialNo01, SerialNo02, SerialNo03, SerialNo04, SerialNo05, SerialNo06, SerialNo07, SerialNo08, SerialNo09, SerialNo10, SerialNo11, SerialNo12, SerialNoEnd, SerialNo);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
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
        public List<Common.Model.MouldingLabel_Model> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Common.Model.MouldingLabel_Model> DataTableToList(DataTable dt)
        {
            List<Common.Model.MouldingLabel_Model> modelList = new List<Common.Model.MouldingLabel_Model>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Common.Model.MouldingLabel_Model model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Common.Model.MouldingLabel_Model();
                    model.trackingID = dt.Rows[n]["trackingID"].ToString();
                    model.machineID = dt.Rows[n]["machineID"].ToString();
                    if (dt.Rows[n]["UsageQTY01"].ToString() != "")
                    {
                        model.UsageQTY01 = decimal.Parse(dt.Rows[n]["UsageQTY01"].ToString());
                    }
                    if (dt.Rows[n]["UsageQTY02"].ToString() != "")
                    {
                        model.UsageQTY02 = decimal.Parse(dt.Rows[n]["UsageQTY02"].ToString());
                    }
                    if (dt.Rows[n]["UsageQTY03"].ToString() != "")
                    {
                        model.UsageQTY03 = decimal.Parse(dt.Rows[n]["UsageQTY03"].ToString());
                    }
                    if (dt.Rows[n]["UsageQTY04"].ToString() != "")
                    {
                        model.UsageQTY04 = decimal.Parse(dt.Rows[n]["UsageQTY04"].ToString());
                    }
                    if (dt.Rows[n]["UsageQTY05"].ToString() != "")
                    {
                        model.UsageQTY05 = decimal.Parse(dt.Rows[n]["UsageQTY05"].ToString());
                    }
                    if (dt.Rows[n]["UsageQTY06"].ToString() != "")
                    {
                        model.UsageQTY06 = decimal.Parse(dt.Rows[n]["UsageQTY06"].ToString());
                    }
                    if (dt.Rows[n]["UsageQTY07"].ToString() != "")
                    {
                        model.UsageQTY07 = decimal.Parse(dt.Rows[n]["UsageQTY07"].ToString());
                    }
                    if (dt.Rows[n]["UsageQTY08"].ToString() != "")
                    {
                        model.UsageQTY08 = decimal.Parse(dt.Rows[n]["UsageQTY08"].ToString());
                    }
                    if (dt.Rows[n]["UsageQTY09"].ToString() != "")
                    {
                        model.UsageQTY09 = decimal.Parse(dt.Rows[n]["UsageQTY09"].ToString());
                    }
                    if (dt.Rows[n]["UsageQTY10"].ToString() != "")
                    {
                        model.UsageQTY10 = decimal.Parse(dt.Rows[n]["UsageQTY10"].ToString());
                    }
                    if (dt.Rows[n]["UsageQTY11"].ToString() != "")
                    {
                        model.UsageQTY11 = decimal.Parse(dt.Rows[n]["UsageQTY11"].ToString());
                    }
                    if (dt.Rows[n]["UsageQTY12"].ToString() != "")
                    {
                        model.UsageQTY12 = decimal.Parse(dt.Rows[n]["UsageQTY12"].ToString());
                    }
                    if (dt.Rows[n]["RejectQTY01"].ToString() != "")
                    {
                        model.RejectQTY01 = decimal.Parse(dt.Rows[n]["RejectQTY01"].ToString());
                    }
                    if (dt.Rows[n]["RejectQTY02"].ToString() != "")
                    {
                        model.RejectQTY02 = decimal.Parse(dt.Rows[n]["RejectQTY02"].ToString());
                    }
                    if (dt.Rows[n]["RejectQTY03"].ToString() != "")
                    {
                        model.RejectQTY03 = decimal.Parse(dt.Rows[n]["RejectQTY03"].ToString());
                    }
                    if (dt.Rows[n]["RejectQTY04"].ToString() != "")
                    {
                        model.RejectQTY04 = decimal.Parse(dt.Rows[n]["RejectQTY04"].ToString());
                    }
                    if (dt.Rows[n]["RejectQTY05"].ToString() != "")
                    {
                        model.RejectQTY05 = decimal.Parse(dt.Rows[n]["RejectQTY05"].ToString());
                    }
                    if (dt.Rows[n]["RejectQTY06"].ToString() != "")
                    {
                        model.RejectQTY06 = decimal.Parse(dt.Rows[n]["RejectQTY06"].ToString());
                    }
                    if (dt.Rows[n]["RejectQTY07"].ToString() != "")
                    {
                        model.RejectQTY07 = decimal.Parse(dt.Rows[n]["RejectQTY07"].ToString());
                    }
                    if (dt.Rows[n]["RejectQTY08"].ToString() != "")
                    {
                        model.RejectQTY08 = decimal.Parse(dt.Rows[n]["RejectQTY08"].ToString());
                    }
                    if (dt.Rows[n]["RejectQTY09"].ToString() != "")
                    {
                        model.RejectQTY09 = decimal.Parse(dt.Rows[n]["RejectQTY09"].ToString());
                    }
                    if (dt.Rows[n]["RejectQTY10"].ToString() != "")
                    {
                        model.RejectQTY10 = decimal.Parse(dt.Rows[n]["RejectQTY10"].ToString());
                    }
                    if (dt.Rows[n]["RejectQTY11"].ToString() != "")
                    {
                        model.RejectQTY11 = decimal.Parse(dt.Rows[n]["RejectQTY11"].ToString());
                    }
                    if (dt.Rows[n]["RejectQTY12"].ToString() != "")
                    {
                        model.RejectQTY12 = decimal.Parse(dt.Rows[n]["RejectQTY12"].ToString());
                    }
                    if (dt.Rows[n]["SerialNo01"].ToString() != "")
                    {
                        model.SerialNo01 = decimal.Parse(dt.Rows[n]["SerialNo01"].ToString());
                    }
                    if (dt.Rows[n]["SerialNo02"].ToString() != "")
                    {
                        model.SerialNo02 = decimal.Parse(dt.Rows[n]["SerialNo02"].ToString());
                    }
                    if (dt.Rows[n]["SerialNo03"].ToString() != "")
                    {
                        model.SerialNo03 = decimal.Parse(dt.Rows[n]["SerialNo03"].ToString());
                    }
                    if (dt.Rows[n]["SerialNo04"].ToString() != "")
                    {
                        model.SerialNo04 = decimal.Parse(dt.Rows[n]["SerialNo04"].ToString());
                    }
                    if (dt.Rows[n]["SerialNo05"].ToString() != "")
                    {
                        model.SerialNo05 = decimal.Parse(dt.Rows[n]["SerialNo05"].ToString());
                    }
                    if (dt.Rows[n]["SerialNo06"].ToString() != "")
                    {
                        model.SerialNo06 = decimal.Parse(dt.Rows[n]["SerialNo06"].ToString());
                    }
                    if (dt.Rows[n]["SerialNo07"].ToString() != "")
                    {
                        model.SerialNo07 = decimal.Parse(dt.Rows[n]["SerialNo07"].ToString());
                    }
                    if (dt.Rows[n]["SerialNo08"].ToString() != "")
                    {
                        model.SerialNo08 = decimal.Parse(dt.Rows[n]["SerialNo08"].ToString());
                    }
                    if (dt.Rows[n]["SerialNo09"].ToString() != "")
                    {
                        model.SerialNo09 = decimal.Parse(dt.Rows[n]["SerialNo09"].ToString());
                    }
                    if (dt.Rows[n]["SerialNo10"].ToString() != "")
                    {
                        model.SerialNo10 = decimal.Parse(dt.Rows[n]["SerialNo10"].ToString());
                    }
                    if (dt.Rows[n]["SerialNo11"].ToString() != "")
                    {
                        model.SerialNo11 = decimal.Parse(dt.Rows[n]["SerialNo11"].ToString());
                    }
                    if (dt.Rows[n]["SerialNo12"].ToString() != "")
                    {
                        model.SerialNo12 = decimal.Parse(dt.Rows[n]["SerialNo12"].ToString());
                    }
                    if (dt.Rows[n]["SerialNoEnd"].ToString() != "")
                    {
                        model.SerialNoEnd = decimal.Parse(dt.Rows[n]["SerialNoEnd"].ToString());
                    }
                    model.SerialNo = dt.Rows[n]["SerialNo"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }


        public MouldingLabel_Model GetModel_ByTrackingID(string TrackingID)
        {
            MouldingLabel_Model objVi = new MouldingLabel_Model();
            DataSet ds = dal.GetModel_ByTrackingID(TrackingID);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                objVi = null;
            }
            else
            {
                objVi = DataTableToList(ds.Tables[0])[0];
            }
            return objVi;
        }




        public DataTable SelectList(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sPartNo, string sShift, string sModule)
        {
            DataSet ds = dal.SelectList(dDateFrom, dDateTo, sMachineID, sPartNo, sShift, sModule);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            //if (ds == null || ds.Tables.Count == 0)
            //{
            //    return null;
            //}

            //DataTable dt = ds.Tables[0];


            return dt;
        }


        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  Method
    }
}

