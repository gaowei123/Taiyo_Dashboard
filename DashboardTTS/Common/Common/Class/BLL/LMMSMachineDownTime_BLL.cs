using System;
using System.Data;
using System.Collections.Generic;



namespace Common.Class.BLL
{
	/// <summary>
	/// LMMSMachineDownTime
	/// </summary>
	public partial class LMMSMachineDownTime_BLL
	{
        private readonly Common.Class.DAL.LMMSMachineDownTime_DAL dal = new DAL.LMMSMachineDownTime_DAL();
		public LMMSMachineDownTime_BLL()
		{}

		#region  BasicMethod

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Common.Class.Model.LMMSMachineDownTime_Model model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Common.Class.Model.LMMSMachineDownTime_Model model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.Delete();
		}

	
		public DataTable GetList(DateTime dDateFrom, DateTime dDateTo, string sMachineID, DateTime? dDay , string sCause)
		{
            DataSet ds = dal.GetList(dDateFrom, dDateTo, sMachineID, dDay, sCause);
            
            if (ds == null || ds.Tables.Count == 0 )
            {
                return null;
            }
            else
            {
                DataTable dt = ds.Tables[0];

                foreach (DataRow dr in dt.Rows)
                {

                    double dTime = double.Parse(dr["Time"].ToString());
                    dr["Time"] = Common.CommFunctions.ConvertDateTimeShort((dTime / 3600).ToString());


                    string attachmentPath = dr["AttachmentPath"].ToString();
                    string[] pathArr = attachmentPath.Split('\\');
                    string fileName = pathArr[pathArr.Length - 1];

                    dr["fileName"] = fileName;

                }

                return dt;
            }
		}


        public Common.Class.Model.LMMSMachineDownTime_Model GetModel(string sMachineID, DateTime? dDay, string sCause)
        {
            DataTable dt = GetList(DateTime.MinValue, DateTime.MaxValue, sMachineID, dDay, sCause);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            Common.Class.Model.LMMSMachineDownTime_Model model = new Model.LMMSMachineDownTime_Model();
            model.machineID = dt.Rows[0]["machineID"].ToString();
            model.partRunning = dt.Rows[0]["partRunning"].ToString();

            model.cause = dt.Rows[0]["completeCause"].ToString();
            model.action = dt.Rows[0]["completeAction"].ToString();
            
            model.startTime = DateTime.Parse(dt.Rows[0]["startTime"].ToString());
            model.stopTime = DateTime.Parse(dt.Rows[0]["stopTime"].ToString());
            model.date = DateTime.Parse(dt.Rows[0]["dateComplete"].ToString());
            model.checker = dt.Rows[0]["checker"].ToString();
            model.attachmentPath = dt.Rows[0]["AttachmentPath"].ToString();


            return model;
        }



		#endregion  BasicMethod

	}
}

