using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
    public class LMMSBomDetail_BLL
    {
        public LMMSBomDetail_BLL()
        {
             
        }

        Common.Class.DAL.LMMSBomDetail_DAL dal = new DAL.LMMSBomDetail_DAL();
        public DataTable GetBomDetailListByPartNumber(string PartNumber)
        {
            DataSet ds = new DataSet();
            ds = dal.GetListByPartNumber(PartNumber);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }


        public bool Add(List<SqlCommand> list_cmd)
        {
            return dal.Add_Rollback(list_cmd);
        }

        public int MaterialCountByPart(string PartNumber)
        {
            DataSet ds = dal.SelectMaterialCount(PartNumber);

            int Count = 0;

            if (ds == null || ds.Tables.Count ==0)
            {
                Count = 0;
            }
            else
            {
                DataTable dt = ds.Tables[0];

                try
                {
                    Count = int.Parse(dt.Rows[0][0].ToString());
                }
                catch (Exception)
                {
                    Count = 0;
                }
               
            }

            return Count;
        }

        public bool IsExis(Common.Class.Model.LMMSBomDetail_Model model)
        {
           DataSet ds = dal.SelectMaterialByModel(model);
            if (ds == null || ds.Tables.Count == 0)
            {
                return false;
            }
            int count = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public SqlCommand GetCmd(Common.Class.Model.LMMSBomDetail_Model model)
        {

            return dal.AddCommand(model);
        }


        public bool DeleteByPart(string PartNumber)
        {
            int result = dal.DeleteByPart(PartNumber);

            if (result >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable  GetMaterialListByPart(string PartNumber)
        {
            DataSet ds = dal.SelectMaterialList(PartNumber);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }

        public bool DeleteMaterial(string MaterialPart, string PartNumber)
        {
            int result = dal.DeleteMaterial(MaterialPart,PartNumber);

            if (result > 0)
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
