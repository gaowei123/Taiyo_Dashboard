using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
    public class PaintingDeliveryHis_BLL
    {
        private readonly Common.Class.DAL.PaintingDeliveryHis_DAL dal = new Common.Class.DAL.PaintingDeliveryHis_DAL();



        #region  Method
     
        public SqlCommand AddCMD(Common.Class.Model.PaintingDeliveryHis_Model model)
        {
            return dal.AddCMD(model);
        }


        public bool Add(Common.Class.Model.PaintingDeliveryHis_Model model)
        {
            int result = dal.Add(model);

            if (result > 0)
            {
                return true;
            }else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Common.Class.Model.PaintingDeliveryHis_Model model)
        {
            int result = dal.Update(model);

            if (result <= 0)
            {
                return false;
            }else
            {
                return true;
            }
         
        }

        public bool UpdatePaintRej(string jobNumber, int rejQty, string process)
        {
            int result = dal.UpdatePaintRej(jobNumber, rejQty, process);

            if (result <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool UpdateMFGDate(string jobNumber, DateTime MFGDate)
        {
            int result = dal.UpdateMFGDate(jobNumber, MFGDate);

            if (result <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete()
        {
            return dal.Delete();
        }
        
        #endregion

        public DataTable GetList(DateTime dDateFrom, DateTime dDateTo,string sJobNo)
        {
            DataSet ds = new DataSet();

            ds = dal.GetList(dDateFrom, dDateTo,sJobNo,"");

            if (ds == null || ds.Tables.Count == 0 )
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }

        


        public DataTable GetDayOutput( DateTime dDay)
        {
            DataSet ds = new DataSet();

            ds = dal.GetDayOutput(dDay);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }


        public DataTable GetList(DateTime dDateFrom, DateTime dDateTo, string sJobNo, string sPartNo,string sSendingTo,string sLotno)
        {
            DataSet ds = new DataSet();

            ds = dal.GetList( dDateFrom,  dDateTo,  sJobNo,  sPartNo, sSendingTo, sLotno);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return null;
            }
          

            #region dr total

            double TotalInSET = 0;
            double TotalInPCS = 0;
            foreach (DataRow dr  in dt.Rows)
            {
                try
                {
                    TotalInSET += double.Parse(dr["inQtySET"].ToString());
                }
                catch{ }

                try
                {
                    TotalInPCS += double.Parse(dr["inQtyPCS"].ToString());
                }
                catch { }
            }

            DataRow drTotal = dt.NewRow();
            drTotal["partNumber"] = "Total :";
            drTotal["inQuantity"] = TotalInSET+ "("+ TotalInPCS + ")";
            drTotal["paintRejQty"] = dt.Compute("sum(paintRejQty)", "");

            dt.Rows.Add(drTotal);
            #endregion

            return dt;

        }

        




        


        public bool ExistJobno(string sJobno)
        {
            bool Result = false;
            DataSet ds = dal.CheckJobno(sJobno);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                Result = false;
            }
            else
            {
                Result = true;
            }


            return Result;
        }


        public DataTable GetOuputForAllMachineChart(DateTime DateFrom, DateTime DateTo, string Shift, string DateNotIn, bool ExceptWeekends)
        {
            DataTable dt = dal.GetOuput(DateFrom, DateTo, Shift, DateNotIn, ExceptWeekends);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            else
            {
                return dt;
            }
        }



        public DataTable GetPaintDeliveryForButtonReport_NEW(DateTime dDateFrom, DateTime dDateTo, string sJobNo)
        {
            DataSet ds = dal.GetPaintDeliveryForButtonReport_NEW(dDateFrom, dDateTo, sJobNo);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }



        public List<Common.Class.Model.PaintingDeliveryHis_Model> GetModels(string sJobNo, DateTime dDateFrom, DateTime dDateTo)
        {
            DataSet ds = dal.GetList(dDateFrom, dDateTo, sJobNo,"");
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }


            DataTable dt = ds.Tables[0];


            List<Common.Class.Model.PaintingDeliveryHis_Model> models = new List<Model.PaintingDeliveryHis_Model>();

            foreach (DataRow dr in dt.Rows)
            {
                models.Add(ConvertModel(dr));
            }


            return models;
        }


        public Common.Class.Model.PaintingDeliveryHis_Model GetModel(string sJobNo, string sPaintProcess)
        {
            DataSet ds = dal.GetList(DateTime.Now.AddYears(-2), DateTime.Now.AddYears(1), sJobNo, sPaintProcess);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }


            DataTable dt = ds.Tables[0];


            return ConvertModel(dt.Rows[0]);
        }





        public Common.Class.Model.PaintingDeliveryHis_Model ConvertModel(DataRow dr)
        {

            Common.Class.Model.PaintingDeliveryHis_Model model = new Model.PaintingDeliveryHis_Model();
            model.jobNumber = dr["jobNumber"].ToString();
            model.partNumber = dr["partNumber"].ToString();
            model.sendingTo = dr["sendingTo"].ToString();
            if (dr["inQuantity"].ToString() != "")
            {
                model.inQuantity = decimal.Parse(dr["inQuantity"].ToString());
            }
         
            model.lotNo = dr["lotNo"].ToString();
            if (dr["boxQty"].ToString() != "")
            {
                model.boxQty = decimal.Parse(dr["boxQty"].ToString());
            }
           
            model.remark = dr["remark"].ToString();

            if (dr["dateTime"].ToString() != "")
            {
                model.dateTime = DateTime.Parse(dr["dateTime"].ToString());
            }


            if (dr["updatedTime"].ToString() != "")
            {
                model.updatedTime = DateTime.Parse(dr["updatedTime"].ToString());
            }

            model.SignID = dr["SignID"].ToString();
            model.status = dr["status"].ToString();
            model.paintProcess = dr["paintProcess"].ToString();

            if (dr["paintRejQty"].ToString() != "")
            {
                model.paintRejQty = int.Parse(dr["paintRejQty"].ToString());
            }



            return model;
        }




     



    }
}
