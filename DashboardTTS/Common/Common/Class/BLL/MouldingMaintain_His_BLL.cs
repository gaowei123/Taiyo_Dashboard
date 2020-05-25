using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
    public class MouldingMaintain_His_BLL
    {

        Common.Class.DAL.MouldingMaintain_His_DAL dal = new DAL.MouldingMaintain_His_DAL();

        

        public List<string> GetCheckPeriod()
        {
            DataSet ds = dal.SelectCheckPeriod();
            if (ds == null|| ds.Tables.Count ==0 )
            {
                return null;
            }

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count ==0)
            {
                return null;
            }

            List<string> list_CheckPeriod = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string sCheckPeriod = dr["CheckPeriod"].ToString();

                list_CheckPeriod.Add(sCheckPeriod);
            }

            return list_CheckPeriod;
        }

        public List<string> GetCheckItem(string sCheckPeriod)
        {
            DataSet ds = dal.SelectCheckItem(sCheckPeriod);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            List<string> list_CheckItem = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string sCheckItem = dr["CheckItem"].ToString();

                list_CheckItem.Add(sCheckItem);
            }

            return list_CheckItem;
        }


        public bool Add(List<Common.Class.Model.MouldingMaintain_His_Model> List_Model)
        {
            List<SqlCommand> list_CMD = new List<SqlCommand>();

            foreach (Common.Class.Model.MouldingMaintain_His_Model model in List_Model)
            {
                string[] strArr = model.MachineID.Split(',');

                if (strArr.Length > 0)
                {
                    foreach (string machineID in strArr)
                    {
                        model.MachineID = machineID;
                        SqlCommand cmd = dal.AddCMD(model);
                        list_CMD.Add(cmd);
                    }

                }
                else
                {
                    continue;
                }

            }

            if (DBHelp.SqlDB.SetData_Rollback(list_CMD, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Verify(List<Common.Class.Model.MouldingMaintain_His_Model> List_Model)
        {
            List<SqlCommand> list_CMD = new List<SqlCommand>();


            Common.Class.DAL.MouldingMaintain_Tracking_DAL dal_Maintaintracking = new DAL.MouldingMaintain_Tracking_DAL();

            foreach (Common.Class.Model.MouldingMaintain_His_Model model in List_Model)
            {
                string TotalMachineID = model.MachineID;
                string[] strArr = model.MachineID.Split(',');

                if (strArr.Length > 0)
                {
                    //add to history
                    foreach (string machineID in strArr)
                    {
                        model.MachineID = machineID;
                        SqlCommand cmd = dal.AddCMD(model);
                        list_CMD.Add(cmd);
                    }

                    //delete from tracking
                    SqlCommand cmd_del = dal_Maintaintracking.DeleteCmd(model.CheckPeriod,model.CheckItem, TotalMachineID,(DateTime)model.CheckDate);
                    list_CMD.Add(cmd_del);
                }
                else
                {
                    continue;
                }

            }

            if (DBHelp.SqlDB.SetData_Rollback(list_CMD, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server))
            {
                return true;
            }
            else
            {
                return false;
            }
        }





        public bool UpdateSpareParts(Common.Class.Model.MouldingMaintain_His_Model model)
        {
            int temp = dal.UpdateSpareParts(model);

            if (temp > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public DataTable GetList(DateTime dDateFrom, DateTime dDateTo, string sMachineID, string sCheckPeriod)
        {
            DataSet ds = dal.Select( dDateFrom,  dDateTo,  sMachineID,  sCheckPeriod);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];


            return dt;
        }



        public bool IsAllowCheck(string sMachineID, string sCheckItem, string sCheckPeriod)
        {
            DateTime? dDateFrom = DateTime.Now.Date;
            if (sCheckPeriod == "Daily")
            {
                dDateFrom = null;
            }
            else if (sCheckPeriod == "Weekly")
            {
                dDateFrom = dDateFrom.Value.AddDays(-5);
            }
            else if (sCheckPeriod == "Monthly")
            {
                dDateFrom = dDateFrom.Value.AddDays(-20);
            }
            else if (sCheckPeriod == "Halfyear")
            {
                dDateFrom = dDateFrom.Value.AddDays(-150);
            }
            else if (sCheckPeriod == "Quarter")
            {
                dDateFrom = dDateFrom.Value.AddDays(-80);
            }
            else if (sCheckPeriod == "Yearly")
            {
                dDateFrom = dDateFrom.Value.AddDays(-300);
            }

            DataSet ds = dal.CheckExist(dDateFrom, sCheckItem, sMachineID, sCheckPeriod);

            if (ds == null || ds.Tables.Count == 0)
            {
                return true;
            }

            DataTable dt = ds.Tables[0];

            if (dt == null || dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
