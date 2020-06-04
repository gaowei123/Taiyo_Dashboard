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
            hisModel.remark_1 = model.remark_1;
            hisModel.remark_2 = model.remark_2;
            hisModel.remark_3 = model.remark_3;
            hisModel.remark_4 = model.remark_4;
            hisModel.remarks = model.remarks;
            hisModel.materialFromQty = 0;
            hisModel.updatedTime = model.updatedTime;


            return hisModel;
        }

    }

}
