using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Common.Class.BLL
{
	/// <summary>
	/// LMMSCheckList
	/// </summary>
	public partial class LMMSCheckList_BLL
	{
        private readonly Common.Class.DAL.LMMSCheckList_DAL dal = new Common.Class.DAL.LMMSCheckList_DAL();
		public LMMSCheckList_BLL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Common.Class.Model.LMMSCheckList_Model model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Common.Class.Model.LMMSCheckList_Model model)
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

	
		public DataTable GetList(DateTime dDateFrom, DateTime dDateTo, string sMachineID)
		{
            DataSet ds = dal.GetList(dDateFrom, dDateTo, sMachineID);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }


            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            return dt;
        }




        public bool IsChecked(string sMachineID, DateTime dDay)
        {

            DataTable dt = dal.IsChecked(sMachineID, dDay);

            if (dt == null || dt.Rows.Count == 0)
            {
                return false;
            }else
            {
                return true;
            }
        }





        public bool Check_RollBack(Common.Class.Model.LMMSCheckList_Model model)
        {
            List<SqlCommand> cmdList = new List<SqlCommand>();


            cmdList.Add(dal.AddCMD(model));

            if (model.filterBagReplace== "YES")
            {
                Common.Class.BLL.LMMSSparePartsInventory_BLL sparePartsBLL = new BLL.LMMSSparePartsInventory_BLL();
                Common.Class.Model.LMMSSparePartsInventory_Model sparePartsModel = sparePartsBLL.getPartsModel("Filter Bag");


                if (sparePartsModel.quantity > 0)
                {
                    sparePartsModel.lastUpdatedBy = model.doneBy;
                    sparePartsModel.lastUpdatedTime = DateTime.Now;
                    sparePartsModel.quantity = sparePartsModel.quantity - 1;

                    sparePartsModel.machineID = model.machineID;
                    sparePartsModel.action = "OUT";
                    sparePartsModel.usage = 1;


                    Common.Class.DAL.LMMSSparePartsInventory_DAL sparePartsDAL = new DAL.LMMSSparePartsInventory_DAL();
                    cmdList.Add(sparePartsDAL.UpdatePartInventoryCMD(sparePartsModel));
                    cmdList.Add(sparePartsDAL.AddHisCMD(sparePartsModel));
                }
            }



            return DBHelp.SqlDB.SetData_Rollback(cmdList);
        }


        #endregion  BasicMethod

    }
}

