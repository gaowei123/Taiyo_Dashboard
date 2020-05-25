using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Common.Class.BLL
{
    public class LMMSUserEventLog_BLL
    {
        private  Common.Class.DAL.LMMSUserEventLog_DAL dal = new DAL.LMMSUserEventLog_DAL();
        public LMMSUserEventLog_BLL()
        {

        }

        public bool AddRollBack(List<Common.Class.Model.LMMSUserEventLog>  listModel)
        {
          

            List<SqlCommand> cmdList = new List<SqlCommand>();

            foreach (Common.Class.Model.LMMSUserEventLog model in listModel)
            {
                cmdList.Add(dal.AddCommand(model));
            }

            return DBHelp.SqlDB.SetData_Rollback(cmdList);
            
        }

        public string GetUpdatedByJob(string Jobnumber)
        {
            string UpdatedBy = "";

            DataTable dt = dal.SearchByJob(Jobnumber);

            foreach (DataRow dr in dt.Rows)
            {
                if (!Regex.IsMatch(UpdatedBy, dr["UserID"].ToString()))
                {
                    UpdatedBy += dr["UserID"].ToString() + ",";
                }
            }

            if (UpdatedBy.Trim() != "")
            {
                UpdatedBy = UpdatedBy.Substring(0, UpdatedBy.Length - 1);
            }
            
            return UpdatedBy;
        }

        public string GetUpdatedByMaterial(string Jobnumber ,string Material)
        {
            string UpdatedBy = "";

            DataTable dt = dal.SearchByMaterial(Jobnumber, Material);

            foreach (DataRow dr in dt.Rows)
            {
                if (!Regex.IsMatch(UpdatedBy, dr["UserID"].ToString()))
                {
                    UpdatedBy += dr["UserID"].ToString() + ",";
                }
            }

            if (UpdatedBy.Trim() != "")
            {
                UpdatedBy = UpdatedBy.Substring(0, UpdatedBy.Length - 1);
            }

            return UpdatedBy;
        }

    }
}
