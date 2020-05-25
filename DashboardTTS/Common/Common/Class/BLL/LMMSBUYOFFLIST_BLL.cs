using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
    public class LMMSBUYOFFLIST_BLL
    {
        private readonly Common.Class.DAL.LMMSBuyOffList dal = new DAL.LMMSBuyOffList();

        public LMMSBUYOFFLIST_BLL()
        {

        }
      

        public DataTable GetBuyofflist(string JobID,string PartNo,string Machine,string MC_Operator,string ApprovdeBy,string CheckBy, DateTime? From, DateTime? To)
        {
            DataSet ds = new DataSet(); 
            ds = dal.SelectBuyoffReport(JobID, PartNo, Machine, MC_Operator, ApprovdeBy, CheckBy, From, To);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }

        public DataTable GetLaserInfo(DateTime dDateFrom, DateTime dDateTo, string sJobNumber)
        {
            DataSet ds = new DataSet();


            ds = dal.GetLaserInfo(dDateFrom, dDateTo, sJobNumber);



            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }else
            {
                return ds.Tables[0];
            }
        }




        public Common.Class.Model.LMMSBuyOffList_Mode GetModel(string sJobNumber)
        {
           
            DataSet ds = dal.GetList(sJobNumber);


            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];




            Common.Class.Model.LMMSBuyOffList_Mode model = new Model.LMMSBuyOffList_Mode();
            model.BUYOFF_ID = dt.Rows[0]["BUYOFF_ID"].ToString();
            model.JOB_ID = dt.Rows[0]["JOB_ID"].ToString();
            model.PART_NO = dt.Rows[0]["PART_NO"].ToString();
            model.MACHINE_ID = dt.Rows[0]["MACHINE_ID"].ToString();
            model.DATE_TIME = DateTime.Parse(dt.Rows[0]["DATE_TIME"].ToString());
            model.MC_OPERATOR = dt.Rows[0]["MC_OPERATOR"].ToString();
            model.BUYOFF_BY = dt.Rows[0]["BUYOFF_BY"].ToString();
            model.APPROVED_BY = dt.Rows[0]["APPROVED_BY"].ToString();
            model.CHECK_BY = dt.Rows[0]["CHECK_BY"].ToString();
            model.BLACK_MARK_1ST = dt.Rows[0]["BLACK_MARK_1ST"].ToString();
            model.BLACK_DOT_1ST = dt.Rows[0]["BLACK_DOT_1ST"].ToString();
            model.PIN_HOLE_1ST = dt.Rows[0]["PIN_HOLE_1ST"].ToString();
            model.JAGGED_1ST = dt.Rows[0]["JAGGED_1ST"].ToString();
            model.CHECK_GULED_1ST = dt.Rows[0]["CHECK_GULED_1ST"].ToString();
            model.NAVITAS_1ST = dt.Rows[0]["NAVITAS_1ST"].ToString();
            model.SMART_SCOPE_1ST = dt.Rows[0]["SMART_SCOPE_1ST"].ToString();
            model.BLACK_MARK_2ND = dt.Rows[0]["BLACK_MARK_2ND"].ToString();
            model.BLACK_DOT_2ND = dt.Rows[0]["BLACK_DOT_2ND"].ToString();
            model.PIN_HOLE_2ND = dt.Rows[0]["PIN_HOLE_2ND"].ToString();
            model.JAGGED_2ND = dt.Rows[0]["JAGGED_2ND"].ToString();
            model.CHECK_GULED_2ND = dt.Rows[0]["CHECK_GULED_2ND"].ToString();
            model.NAVITAS_2ND = dt.Rows[0]["NAVITAS_2ND"].ToString();
            model.SMART_SCOPE_2ND = dt.Rows[0]["SMART_SCOPE_2ND"].ToString();
            model.BLACK_MARK_IN = dt.Rows[0]["BLACK_MARK_IN"].ToString();
            model.BLACK_DOT_IN = dt.Rows[0]["BLACK_DOT_IN"].ToString();
            model.PIN_HOLE_IN = dt.Rows[0]["PIN_HOLE_IN"].ToString();
            model.JAGGED_IN = dt.Rows[0]["JAGGED_IN"].ToString();
            model.CHECK_GULED_IN = dt.Rows[0]["CHECK_GULED_IN"].ToString();
            model.NAVITAS_IN = dt.Rows[0]["NAVITAS_IN"].ToString();
            model.SMART_SCOPE_IN = dt.Rows[0]["SMART_SCOPE_IN"].ToString();


            return model;
        }





        public bool Exist(string sJobnumber)
        {
            DataSet ds = dal.GetList(sJobnumber);

            if (ds == null || ds.Tables.Count == 0 )
            {
                return false;
            }

            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count == 0)
            {
                return false;
            }else
            {
                return true;
            }
        }


   

        public bool AddBomDetailRollback(Common.Class.Model.LMMSBuyOffList_Mode buyoffModel, Model.LMMSVisionMachineSettingHis_Model visionModel)
        {
            
            List<SqlCommand> list_cmd_laser = new List<SqlCommand>();
            list_cmd_laser.Add(dal.AddCommand(buyoffModel));




            Common.Class.DAL.LMMSVisionMachineSettingHis_DAL visionSettingDAL = new Common.Class.DAL.LMMSVisionMachineSettingHis_DAL();
            list_cmd_laser.Add(visionSettingDAL.AddCommand(visionModel));





            //Common.Class.Model.LMMSBom_Model bomModel = new Model.LMMSBom_Model();
            //bomModel.partNumber = visionModel.partNumber;
            //bomModel.machineID = visionModel.machineID;
            //bomModel.Lighting = visionModel.lighting;
            //bomModel.CurrentPower = visionModel.power;
            //bomModel.Camera = visionModel.camera;

            //Common.Class.DAL.LMMSBom_DAL bomDAL = new DAL.LMMSBom_DAL();
            //同步更新到 laser bom中, client会显示 lighting, power, camera 这3个参数
            //list_cmd_laser.Add(bomDAL.UpdateVisionParaByPartMachineCommand(bomModel));







            return DBHelp.SqlDB.SetData_Rollback(list_cmd_laser);
        }


        public DataTable GetLaserInfoButtonReport_NEW(DateTime dateFrom, DateTime dateTo, string jobNo)
        {
            DataTable dt = dal.GetLaserInfoForButtonReport_NEW(dateFrom, dateTo, jobNo);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return dt;
            }
        }



    }
}
