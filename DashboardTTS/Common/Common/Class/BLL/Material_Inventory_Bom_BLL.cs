using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Common.Class.BLL
{
    public class Material_Inventory_Bom_BLL
    {
        Common.Class.DAL.Material_Inventory_Bom_DAL dal = new DAL.Material_Inventory_Bom_DAL();
        public DataTable GetList(string sPartNumber)
        {
            DataSet ds = dal.SelectAll(sPartNumber);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            return dt;
        }


        public List< Common.Model.Material_Inventory_Bom> GetModelList()
        {
            DataSet  ds = dal.SelectAll("");
            if (ds == null || ds.Tables.Count  == 0 || ds.Tables[0].Rows.Count == 0)
                return null;

            List<Common.Model.Material_Inventory_Bom> modelList = new List<Common.Model.Material_Inventory_Bom>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                modelList.Add(convertModel(dr));
            }



            return modelList;
        }


        public Common.Model.Material_Inventory_Bom GetModel(string sMaterialNo)
        {
            DataTable  dt = dal.GetModel(sMaterialNo);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }



            return convertModel(dt.Rows[0]);
        }





        public Common.Model.Material_Inventory_Bom convertModel(DataRow dr)
        {
            if (dr == null )
                return null;

            

            Common.Model.Material_Inventory_Bom model = new Common.Model.Material_Inventory_Bom();
            model.Material_Part = dr["Material_Part"].ToString();
            model.Unit_Price = decimal.Parse(dr["Unit_Price"].ToString());


            model.Unit_Price_USD = dr["Unit_Price_USD"].ToString() == "" ? 0 : decimal.Parse(dr["Unit_Price_USD"].ToString());


            model.Updated_User = dr["Updated_User"].ToString();
            model.Updated_Time = DateTime.Parse(dr["Updated_Time"].ToString());
            model.REF_FIELD01 = dr["ResinType"].ToString();//树脂型号
            model.REF_FIELD02 = dr["REF_FIELD02"].ToString();
            model.REF_FIELD03 = dr["REF_FIELD03"].ToString();
            model.REF_FIELD04 = dr["REF_FIELD04"].ToString();
            model.REF_FIELD05 = dr["REF_FIELD05"].ToString();

            model.ExchangeRate = dr["ExchangeRate"].ToString() == ""? 0: decimal.Parse(dr["ExchangeRate"].ToString());


           
            model.MakeUp = dr["MakeUp"].ToString().Trim('%') ==""? 0: decimal.Parse(dr["MakeUp"].ToString().Trim('%'));



            model.Remarks = dr["Remarks"].ToString();


            return model;
        }





        public bool Add(Common.Model.Material_Inventory_Bom model)
        {
            if (model == null)
            {
                return false;
            }

            int result = dal.Add(model);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool Update(Common.Model.Material_Inventory_Bom model)
        {
            if (model == null)
            {
                return false;
            }

            int result = dal.Update(model);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(string MaterilPart)
        {
            int result = dal.Delete(MaterilPart);

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
