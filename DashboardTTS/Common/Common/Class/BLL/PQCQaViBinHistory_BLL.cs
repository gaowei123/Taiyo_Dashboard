using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
    public class PQCQaViBinHistory_BLL
    {
        private readonly Common.Class.DAL.PQCQaViBinHistory_DAL dal = new DAL.PQCQaViBinHistory_DAL();

        public PQCQaViBinHistory_BLL() { }




        public SqlCommand AddCommand(Common.Class.Model.PQCQaViBinHistory_Model Model)
        {
            return dal.AddCommand(Model);
        }

        public SqlCommand UpdateScrapCommand(Common.Class.Model.PQCQaViBinHistory_Model Model)
        {
            return dal.UpdateScrapCommand(Model);
        }



        public Model.PQCQaViBinHistory_Model CopyModel(Model.PQCQaViBinning model)
        {
            if (model == null) return null;

            Model.PQCQaViBinHistory_Model hisModel = new Model.PQCQaViBinHistory_Model();

            hisModel.id = model.id;
            hisModel.trackingID = model.trackingID;
            hisModel.processes = model.processes;
            hisModel.jobId = model.jobId;
            hisModel.PartNumber = model.PartNumber;
            hisModel.materialPartNo = model.materialPartNo;
            hisModel.materialName = model.materialName;
            hisModel.shipTo = model.shipTo;
            hisModel.model = model.model;
            hisModel.jigNo = model.jigNo;
            hisModel.materialQty = model.materialQty;
            hisModel.status = model.status;
            hisModel.nextViFlag = model.nextViFlag;
            hisModel.dateTime = model.dateTime;
            hisModel.day = model.day;
            hisModel.shift = model.shift;
            hisModel.userName = model.userName;
            hisModel.userID = model.userID;
            hisModel.remark_1 = model.remark_1;
            hisModel.remark_2 = model.remark_2;
            hisModel.remark_3 = model.remark_3;
            hisModel.remark_4 = model.remark_4;
            hisModel.remarks = model.remarks;
            hisModel.materialFromQty = 0;
            hisModel.updatedTime = model.updatedTime;


            return hisModel;
        }


        public List<Model.PQCQaViBinHistory_Model> GetList(string sJobNo)
        {
            DataTable dt = dal.GetList(sJobNo);
            if (dt == null || dt.Rows.Count == 0)
                return null;


            List<Model.PQCQaViBinHistory_Model> list = new List<Model.PQCQaViBinHistory_Model>();

            foreach (DataRow dr in dt.Rows)
            {
                Model.PQCQaViBinHistory_Model model = new Model.PQCQaViBinHistory_Model();
                
                model.id = dr["id"].ToString();
                model.trackingID = dr["trackingID"].ToString();
                model.processes = dr["processes"].ToString();
                model.jobId = dr["jobId"].ToString();
                model.PartNumber = dr["PartNumber"].ToString();
                model.materialPartNo = dr["materialPartNo"].ToString();
                model.materialName = dr["materialName"].ToString();
                model.shipTo = dr["shipTo"].ToString();
                model.model = dr["model"].ToString();
                model.jigNo = dr["jigNo"].ToString();

                if (dr["materialQty"].ToString() == "")
                    model.materialQty = 0;
                else
                    model.materialQty = decimal.Parse(dr["materialQty"].ToString());
          
                model.status = dr["status"].ToString();
                model.nextViFlag = dr["nextViFlag"].ToString();

                if (dr["dateTime"].ToString() != "")
                    model.dateTime = DateTime.Parse(dr["dateTime"].ToString());
                if (dr["day"].ToString() != "")
                    model.day = DateTime.Parse(dr["day"].ToString());

                model.shift = dr["shift"].ToString();
                model.userName = dr["userName"].ToString();
                model.userID = dr["userID"].ToString();
                model.remark_1 = dr["remark_1"].ToString();
                model.remark_2 = dr["remark_2"].ToString();
                model.remark_3 = dr["remark_3"].ToString();
                model.remark_4 = dr["remark_4"].ToString();
                model.remarks = dr["remarks"].ToString();
                if (dr["updatedTime"].ToString() != "")
                    model.updatedTime = DateTime.Parse(dr["updatedTime"].ToString());

                if (dr["materialFromQty"].ToString() == "")
                    model.materialFromQty = 0;
                else
                    model.materialFromQty = decimal.Parse(dr["materialFromQty"].ToString());

               

                list.Add(model);
            }

            return list;
        }


    }

}
