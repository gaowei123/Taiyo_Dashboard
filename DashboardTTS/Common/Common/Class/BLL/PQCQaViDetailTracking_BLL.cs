 
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
	public class PQCQaViDetailTracking_BLL
	{
        private readonly Common.DAL.PQCQaViDetailTracking_DAL dal = new Common.DAL.PQCQaViDetailTracking_DAL();
		public PQCQaViDetailTracking_BLL()
		{}
		#region  Method
        
		public Common.Class.Model.PQCQaViDetailTracking_Model GetModel(string sTrackingID, string sMaterialNo)
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel(sTrackingID, sMaterialNo);
		}
        

		
		public DataTable GetList(string sTrackingID)
		{
            DataSet ds = dal.GetList(sTrackingID);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }else
            {
                return ds.Tables[0];
            }
		}


        public DataTable GetList(string sTrackingID, string sJobID, DateTime dDatefrom, DateTime dDateto)
        {
            DataSet ds = dal.GetList(sTrackingID, sJobID, dDatefrom, dDateto);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }

        public SqlCommand UpdateJob(Common.Class.Model.PQCQaViDetailTracking_Model model,SqlCommand cmd=null)
        {
            SqlCommand updateJobCmd = dal.UpdateJob(model,cmd);
            return updateJobCmd;
        }

        public SqlCommand UpdatePassQty(Common.Class.Model.PQCQaViDetailTracking_Model model)
        {
            SqlCommand updateJobCmd = dal.UpdateJob(model);
            return updateJobCmd;
        }


        public List<Common.Class.Model.PQCQaViDetailTracking_Model> GetModelList(string sTrackingID, string sJobID, DateTime? dDatefrom, DateTime? dDateto)
        {

            DataSet ds = dal.GetList(sTrackingID, sJobID, dDatefrom, dDateto);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            

            DataTable dt = ds.Tables[0];


            List<Common.Class.Model.PQCQaViDetailTracking_Model> modelList = new List<Model.PQCQaViDetailTracking_Model>();

            foreach (DataRow dr in dt.Rows)
            {
                Common.Class.Model.PQCQaViDetailTracking_Model model = new Model.PQCQaViDetailTracking_Model();


                model = DataRowToModel(dr);
                modelList.Add(model);
            }



            return modelList;
        }


        public Common.Class.Model.PQCQaViDetailTracking_Model DataRowToModel(DataRow dr)
        {

            if (dr == null)
            {
                return null;
            }

            Common.Class.Model.PQCQaViDetailTracking_Model model = new Model.PQCQaViDetailTracking_Model();
            if (dr["id"].ToString() != "")
            {
                model.id = int.Parse(dr["id"].ToString());
            }
            model.jobid = dr["jobid"].ToString();
            model.trackingID = dr["trackingID"].ToString();
            model.machineID = dr["machineID"].ToString();
            if (dr["dateTime"].ToString() != "")
            {
                model.dateTime = DateTime.Parse(dr["dateTime"].ToString());
            }
            model.materialPartNo = dr["materialPartNo"].ToString();
            model.jigNo = dr["jigNo"].ToString();
            model.model = dr["model"].ToString();
            model.color = dr["color"].ToString();
            if (dr["cavityCount"].ToString() != "")
            {
                model.cavityCount = decimal.Parse(dr["cavityCount"].ToString());
            }
            model.userName = dr["userName"].ToString();
            model.userID = dr["userID"].ToString();
            if (dr["startTime"].ToString() != "")
            {
                model.startTime = DateTime.Parse(dr["startTime"].ToString());
            }
            if (dr["stopTime"].ToString() != "")
            {
                model.stopTime = DateTime.Parse(dr["stopTime"].ToString());
            }
            if (dr["day"].ToString() != "")
            {
                model.day = DateTime.Parse(dr["day"].ToString());
            }
            model.shift = dr["shift"].ToString();
            model.status = dr["status"].ToString();
            model.remark_1 = dr["remark_1"].ToString();
            model.remark_2 = dr["remark_2"].ToString();
            if (dr["totalQty"].ToString() != "")
            {
                model.totalQty = decimal.Parse(dr["totalQty"].ToString());
            }
            if (dr["totalPassQty"].ToString() != "")
            {
                model.totalPassQty = decimal.Parse(dr["totalPassQty"].ToString());
            }
            if (dr["totalRejectQty"].ToString() != "")
            {
                model.totalRejectQty = decimal.Parse(dr["totalRejectQty"].ToString());
            }
            if (dr["passQty"].ToString() != "")
            {
                model.passQty = decimal.Parse(dr["passQty"].ToString());
            }
            if (dr["rejectQty"].ToString() != "")
            {
                model.rejectQty = decimal.Parse(dr["rejectQty"].ToString());
            }
            model.rejectQtyHour01 = dr["rejectQtyHour01"].ToString();
            model.rejectQtyHour02 = dr["rejectQtyHour02"].ToString();
            model.rejectQtyHour03 = dr["rejectQtyHour03"].ToString();
            model.rejectQtyHour04 = dr["rejectQtyHour04"].ToString();
            model.rejectQtyHour05 = dr["rejectQtyHour05"].ToString();
            model.rejectQtyHour06 = dr["rejectQtyHour06"].ToString();
            model.rejectQtyHour07 = dr["rejectQtyHour07"].ToString();
            model.rejectQtyHour08 = dr["rejectQtyHour08"].ToString();
            model.rejectQtyHour09 = dr["rejectQtyHour09"].ToString();
            model.rejectQtyHour10 = dr["rejectQtyHour10"].ToString();
            model.rejectQtyHour11 = dr["rejectQtyHour11"].ToString();
            model.rejectQtyHour12 = dr["rejectQtyHour12"].ToString();
            if (dr["lastUpdatedTime"].ToString() != "")
            {
                model.lastUpdatedTime = DateTime.Parse(dr["lastUpdatedTime"].ToString());
            }
            model.remarks = dr["remarks"].ToString();
            model.processes = dr["processes"].ToString();
            if (dr["updatedTime"].ToString() != "")
            {
                model.updatedTime = DateTime.Parse(dr["updatedTime"].ToString());
            }


            model.materialName = dr["materialName"].ToString();
            model.PackingTrays = dr["packingTrays"].ToString();
            model.Customer = dr["customer"].ToString();
            model.ShipTo = dr["shipTo"].ToString();
            model.Module = dr["module"].ToString();


            if (dr["module"].ToString() != "")
            {
                model.outerBoxQty = decimal.Parse(dr["outerBoxQty"].ToString());
            }

            if (dr["sn"].ToString() != "")
            {
                model.Sn = int.Parse(dr["sn"].ToString());
            }

            if (dr["indexId"].ToString() != "")
            {
                model.IndexId = int.Parse(dr["indexId"].ToString());
            }


            return model;


        }


        #endregion  Method

    }
}

