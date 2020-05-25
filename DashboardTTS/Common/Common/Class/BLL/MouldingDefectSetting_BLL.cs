using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace Common.Class.BLL
{
    public class MouldingDefectSetting_BLL
    {
        Common.Class.DAL.MouldingDefectSetting_DAL dal = new DAL.MouldingDefectSetting_DAL();
        public MouldingDefectSetting_BLL()
        {

        }


        Hashtable Ht = new Hashtable();
      
        


        public List<string> RejTypeList()
        {
            DataSet ds = dal.SelectList();

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            List<string> list = new List<string>();

            list.Add("All");

            foreach (DataRow  dr in dt.Rows)
            {
                try
                {
                    string Type = dr["defectCode"].ToString();
                    if (Type != "")
                    {
                        list.Add(Type);
                    }
                }
                catch {  }
            }
            

            return list;
        }

        


    }
}
