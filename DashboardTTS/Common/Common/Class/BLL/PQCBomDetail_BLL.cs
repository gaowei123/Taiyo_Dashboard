
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Common.Model;

namespace Common.Class.BLL
{
    public class PQCBomDetail_BLL
    {
        private readonly Common.Class.DAL.PQCBomDetail_DAL dal = new Common.Class.DAL.PQCBomDetail_DAL();
        public PQCBomDetail_BLL()
        { }
        #region  Method

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(Common.Class.Model.PQCBomDetail_Model model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public SqlCommand AddCommand(Common.Class.Model.PQCBomDetail_Model model)
        {
            return dal.AddCommand(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Class.Model.PQCBomDetail_Model model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public SqlCommand UpdateCommand(Common.Class.Model.PQCBomDetail_Model model)
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
        public SqlCommand DeleteCommand(string sPartNumber)
        {
            //该表无主键信息，请自定义主键/条件字段
            return dal.DeleteCommand(sPartNumber);
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
        public Common.Class.Model.PQCBomDetail_Model GetModel()
        {
            //该表无主键信息，请自定义主键/条件字段
            return dal.GetModel();
        }

        
       
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Common.Class.Model.PQCBomDetail_Model> DataTableToList(DataTable dt)
        {
            List<Common.Class.Model.PQCBomDetail_Model> modelList = new List<Common.Class.Model.PQCBomDetail_Model>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Common.Class.Model.PQCBomDetail_Model model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Common.Class.Model.PQCBomDetail_Model();
                    model.sn = int.Parse(dt.Rows[n]["sn"].ToString() ?? "0");
                    model.partNumber = dt.Rows[n]["partNumber"].ToString() ?? "";
                    model.materialPartNo = dt.Rows[n]["materialPartNo"].ToString() ?? "";
                    model.partCount = decimal.Parse(dt.Rows[n]["partCount"].ToString() ?? "0");
                    model.userName = dt.Rows[n]["userName"].ToString() ?? "";
                    model.dateTime = DateTime.Parse(dt.Rows[n]["dateTime"].ToString() ?? System.DateTime.Now.ToString());
                    model.partImage = dt.Rows[n]["partImage"] as byte[];
                    model.color = dt.Rows[n]["color"].ToString() ?? "";
                    modelList.Add(model);
                }
            }
            return modelList;
        }

       
       

       
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public Common.Class.Model.PQCBomDetail_Model CopyObj(Common.Class.Model.PQCBomDetail_Model objModel)
        {
            Common.Class.Model.PQCBomDetail_Model model;
            model = new Common.Class.Model.PQCBomDetail_Model();
            model.sn = objModel.sn;
            model.partNumber = objModel.partNumber;
            model.materialPartNo = objModel.materialPartNo;
            model.partCount = objModel.partCount;
            model.userName = objModel.userName;
            model.dateTime = objModel.dateTime;
            model.partImage = objModel.partImage;
            model.color = objModel.color;
            return model;
        }


        #endregion  Method

        #region =============Dwyane============

        public List<Common.Class.Model.PQCBomDetail_Model> GetModelList(string sPartNo)
        {
            DataSet ds = dal.GetList(sPartNo);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                DataTable dt = ds.Tables[0];

                List<Common.Class.Model.PQCBomDetail_Model> modelList = new List<Model.PQCBomDetail_Model>();   

                Common.Class.Model.PQCBomDetail_Model model;
                foreach (DataRow dr in dt.Rows)
                {
                    model = new Common.Class.Model.PQCBomDetail_Model();

                    model.sn = int.Parse(dr["sn"].ToString() ?? "0");
                    model.partNumber = dr["partNumber"].ToString() ?? "";
                    model.materialPartNo = dr["materialPartNo"].ToString() ?? "";
                    model.partCount = decimal.Parse(dr["partCount"].ToString() ?? "0");
                    model.userName = dr["userName"].ToString() ?? "";
                    model.dateTime = DateTime.Parse(dr["dateTime"].ToString() ?? System.DateTime.Now.ToString());
                    model.color = dr["color"].ToString() ?? "";
                    model.imagePath = dr["imagePath"].ToString() ?? "";



                    model.imageAbsolutePath = dr["imageAbsolutePath"].ToString() ?? "";
                    model.materialName = dr["materialName"].ToString() ?? "";
                    model.customer = dr["customer"].ToString() ?? "";
                    model.outerBoxQty =  dr["outerBoxQty"].ToString() ==""? 0: int.Parse(dr["outerBoxQty"].ToString());
                    model.packingTrays = dr["packingTrays"].ToString() ?? "";
                    model.module = dr["module"].ToString() ?? "";



                    modelList.Add(model);
                }

                return modelList;
            }
        }

        public DataTable GetList(string sPartNo)
        {
            DataSet ds = dal.GetList(sPartNo);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                DataTable dt = ds.Tables[0];
                return dt;
            }
        }


        public int GetMaterialCount(string PartNumber)
        {


            DataSet ds = dal.GetList(PartNumber);



            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return ds.Tables[0].Rows.Count;

            }

        }


        #endregion
    }
}
