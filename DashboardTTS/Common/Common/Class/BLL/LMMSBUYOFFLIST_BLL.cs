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
            
            return DBHelp.SqlDB.SetData_Rollback(list_cmd_laser);
        }
        
        public DataTable GetLaserInfoButtonReport_NEW(string strWhere)
        {
            DataTable dt = dal.GetLaserInfoForButtonReport_NEW(strWhere);
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
