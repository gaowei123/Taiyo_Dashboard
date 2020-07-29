 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Common.Model;
namespace Common.BLL
{
	/// <summary>
	/// LMMSWatchDog_His_BLL
	/// </summary>
	public class LMMSWatchDog_His_BLL
	{
        private readonly Common.DAL.LMMSWatchDog_His_DAL dal = new Common.DAL.LMMSWatchDog_His_DAL();
        
        public LMMSWatchDog_His_BLL()
		{}



        public DataTable GetList(string sJobNo)
        {
            DataSet ds = dal.GetList(sJobNo);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            return ds.Tables[0];
        }
        

        public List<Common.Model.LMMSWatchDog_His_Model> GetModelList(string sJobNo)
        {
            DataSet ds = dal.GetList(sJobNo);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[0];

            List<Common.Model.LMMSWatchDog_His_Model> watchdogModelList = new List<LMMSWatchDog_His_Model>();
            foreach (DataRow dr in dt.Rows)
            {
                LMMSWatchDog_His_Model model = DataRowToList(dr);

                watchdogModelList.Add(model);

            }


            return watchdogModelList;
        }



        public Common.Model.LMMSWatchDog_His_Model GetModel(string sJobNo, DateTime? dDay, string sShift, string sMachineID  )
        {
            DataTable dt = dal.GetModel(sJobNo, dDay, sShift, sMachineID);
            if (dt == null || dt.Rows.Count == 0)
                return null;
            
            return DataRowToList(dt.Rows[0]);
        }




        public Common.Model.LMMSWatchDog_His_Model DataRowToList(DataRow dr)
        {

            Common.Model.LMMSWatchDog_His_Model model = new LMMSWatchDog_His_Model();




            model.shift = dr["shift"].ToString();
            if (dr["day"].ToString() != "")
            {
                model.day = DateTime.Parse(dr["day"].ToString());
            }

            if (dr["id"].ToString() != "")
            {
                model.id = int.Parse(dr["id"].ToString());
            }
            if (dr["dateTime"].ToString() != "")
            {
                model.dateTime = DateTime.Parse(dr["dateTime"].ToString());
            }
            model.machineID = dr["machineID"].ToString();
            model.partNumber = dr["partNumber"].ToString();
            model.jobNumber = dr["jobNumber"].ToString();
            model.description = dr["description"].ToString();
            if (dr["currentQuantity"].ToString() != "")
            {
                model.currentQuantity = int.Parse(dr["currentQuantity"].ToString());
            }
            if (dr["totalQuantity"].ToString() != "")
            {
                model.totalQuantity = int.Parse(dr["totalQuantity"].ToString());
            }
            model.currentOperation = dr["currentOperation"].ToString();
            if (dr["prepDateOut"].ToString() != "")
            {
                model.prepDateOut = DateTime.Parse(dr["prepDateOut"].ToString());
            }

            if (dr["prepDateIn"].ToString() != "")
            {
                model.prepDateIn = DateTime.Parse(dr["prepDateIn"].ToString());
            }
            if (dr["laserDateOut"].ToString() != "")
            {
                model.laserDateOut = DateTime.Parse(dr["laserDateOut"].ToString());
            }
            if (dr["laserDateIn"].ToString() != "")
            {
                model.laserDateIn = DateTime.Parse(dr["laserDateIn"].ToString());
            }
            if (dr["productDateOut"].ToString() != "")
            {
                model.productDateOut = DateTime.Parse(dr["productDateOut"].ToString());
            }
            if (dr["productDateIn"].ToString() != "")
            {
                model.productDateIn = DateTime.Parse(dr["productDateIn"].ToString());
            }
            model.ownerID = dr["ownerID"].ToString();
            model.laserFileA = dr["laserFileA"].ToString();
            model.laserFileB = dr["laserFileB"].ToString();
            if (dr["totalPass"].ToString() != "")
            {
                model.totalPass = int.Parse(dr["totalPass"].ToString());
            }
            if (dr["totalFail"].ToString() != "")
            {
                model.totalFail = int.Parse(dr["totalFail"].ToString());
            }
            if (dr["lastUpdated"].ToString() != "")
            {
                model.lastUpdated = DateTime.Parse(dr["lastUpdated"].ToString());
            }
            if (dr["startTime"].ToString() != "")
            {
                model.startTime = DateTime.Parse(dr["startTime"].ToString());
            }
            if (dr["stopTime"].ToString() != "")
            {
                model.stopTime = DateTime.Parse(dr["stopTime"].ToString());
            }
            model.rmsStatus = dr["rmsStatus"].ToString();
            if (dr["paintQuantity"].ToString() != "")
            {
                model.paintQuantity = int.Parse(dr["paintQuantity"].ToString());
            }
            if (dr["pqcQuantity"].ToString() != "")
            {
                model.pqcQuantity = int.Parse(dr["pqcQuantity"].ToString());
            }
            if (dr["laserDefectQuantity"].ToString() != "")
            {
                model.laserDefectQuantity = int.Parse(dr["laserDefectQuantity"].ToString());
            }
            if (dr["paintDefectQuantity"].ToString() != "")
            {
                model.paintDefectQuantity = int.Parse(dr["paintDefectQuantity"].ToString());
            }
            if (dr["mouldDefectQuantity"].ToString() != "")
            {
                model.mouldDefectQuantity = int.Parse(dr["mouldDefectQuantity"].ToString());
            }
            model.modelName = dr["modelName"].ToString();
            if (dr["currentTotalPass"].ToString() != "")
            {
                model.currentTotalPass = int.Parse(dr["currentTotalPass"].ToString());
            }
            if (dr["currentTotalFail"].ToString() != "")
            {
                model.currentTotalFail = int.Parse(dr["currentTotalFail"].ToString());
            }
            model.model1Name = dr["model1Name"].ToString();
            model.model2Name = dr["model2Name"].ToString();
            model.model3Name = dr["model3Name"].ToString();
            model.model4Name = dr["model4Name"].ToString();
            model.model5Name = dr["model5Name"].ToString();
            model.model6Name = dr["model6Name"].ToString();
            model.model7Name = dr["model7Name"].ToString();
            model.model8Name = dr["model8Name"].ToString();
            model.model9Name = dr["model9Name"].ToString();
            model.model10Name = dr["model10Name"].ToString();
            model.model11Name = dr["model11Name"].ToString();
            model.model12Name = dr["model12Name"].ToString();
            model.model13Name = dr["model13Name"].ToString();
            model.model14Name = dr["model14Name"].ToString();
            model.model15Name = dr["model15Name"].ToString();
            model.model16Name = dr["model16Name"].ToString();
            if (dr["ok1Count"].ToString() != "")
            {
                model.ok1Count = int.Parse(dr["ok1Count"].ToString());
            }
            if (dr["ok2Count"].ToString() != "")
            {
                model.ok2Count = int.Parse(dr["ok2Count"].ToString());
            }
            if (dr["ok3Count"].ToString() != "")
            {
                model.ok3Count = int.Parse(dr["ok3Count"].ToString());
            }
            if (dr["ok4Count"].ToString() != "")
            {
                model.ok4Count = int.Parse(dr["ok4Count"].ToString());
            }
            if (dr["ok5Count"].ToString() != "")
            {
                model.ok5Count = int.Parse(dr["ok5Count"].ToString());
            }
            if (dr["ok6Count"].ToString() != "")
            {
                model.ok6Count = int.Parse(dr["ok6Count"].ToString());
            }
            if (dr["ok7Count"].ToString() != "")
            {
                model.ok7Count = int.Parse(dr["ok7Count"].ToString());
            }
            if (dr["ok8Count"].ToString() != "")
            {
                model.ok8Count = int.Parse(dr["ok8Count"].ToString());
            }
            if (dr["ok9Count"].ToString() != "")
            {
                model.ok9Count = int.Parse(dr["ok9Count"].ToString());
            }
            if (dr["ok10Count"].ToString() != "")
            {
                model.ok10Count = int.Parse(dr["ok10Count"].ToString());
            }
            if (dr["ok11Count"].ToString() != "")
            {
                model.ok11Count = int.Parse(dr["ok11Count"].ToString());
            }
            if (dr["ok12Count"].ToString() != "")
            {
                model.ok12Count = int.Parse(dr["ok12Count"].ToString());
            }
            if (dr["ok13Count"].ToString() != "")
            {
                model.ok13Count = int.Parse(dr["ok13Count"].ToString());
            }
            if (dr["ok14Count"].ToString() != "")
            {
                model.ok14Count = int.Parse(dr["ok14Count"].ToString());
            }
            if (dr["ok15Count"].ToString() != "")
            {
                model.ok15Count = int.Parse(dr["ok15Count"].ToString());
            }
            if (dr["ok16Count"].ToString() != "")
            {
                model.ok16Count = int.Parse(dr["ok16Count"].ToString());
            }


            if (dr["ng1Count"].ToString() != "")
            {
                model.ng1Count = int.Parse(dr["ng1Count"].ToString());
            }
            if (dr["ng2Count"].ToString() != "")
            {
                model.ng2Count = int.Parse(dr["ng2Count"].ToString());
            }
            if (dr["ng3Count"].ToString() != "")
            {
                model.ng3Count = int.Parse(dr["ng3Count"].ToString());
            }
            if (dr["ng4Count"].ToString() != "")
            {
                model.ng4Count = int.Parse(dr["ng4Count"].ToString());
            }
            if (dr["ng5Count"].ToString() != "")
            {
                model.ng5Count = int.Parse(dr["ng5Count"].ToString());
            }
            if (dr["ng6Count"].ToString() != "")
            {
                model.ng6Count = int.Parse(dr["ng6Count"].ToString());
            }
            if (dr["ng7Count"].ToString() != "")
            {
                model.ng7Count = int.Parse(dr["ng7Count"].ToString());
            }
            if (dr["ng8Count"].ToString() != "")
            {
                model.ng8Count = int.Parse(dr["ng8Count"].ToString());
            }
            if (dr["ng9Count"].ToString() != "")
            {
                model.ng9Count = int.Parse(dr["ng9Count"].ToString());
            }
            if (dr["ng10Count"].ToString() != "")
            {
                model.ng10Count = int.Parse(dr["ng10Count"].ToString());
            }
            if (dr["ng11Count"].ToString() != "")
            {
                model.ng11Count = int.Parse(dr["ng11Count"].ToString());
            }
            if (dr["ng12Count"].ToString() != "")
            {
                model.ng12Count = int.Parse(dr["ng12Count"].ToString());
            }
            if (dr["ng13Count"].ToString() != "")
            {
                model.ng13Count = int.Parse(dr["ng13Count"].ToString());
            }
            if (dr["ng14Count"].ToString() != "")
            {
                model.ng14Count = int.Parse(dr["ng14Count"].ToString());
            }
            if (dr["ng15Count"].ToString() != "")
            {
                model.ng15Count = int.Parse(dr["ng15Count"].ToString());
            }
            if (dr["ng16Count"].ToString() != "")
            {
                model.ng16Count = int.Parse(dr["ng16Count"].ToString());
            }


        

            if (dr["setUpQTY"].ToString() != "")
            {
                model.setupQty = int.Parse(dr["setUpQTY"].ToString());
            }else { model.setupQty = 0; }
            if (dr["buyOffQty"].ToString() != "")
            {
                model.buyoffQty = int.Parse(dr["buyOffQty"].ToString());
            }else { model.buyoffQty = 0; }
            if (dr["shortage"].ToString() != "")
            {
                model.shortage = int.Parse(dr["shortage"].ToString());
            }else { model.shortage = 0; }



            return model;
        }

        

        
     
     
        public DataTable GetMachineInfo(string sMachineID)
        {
            DataSet ds = dal.SelectMachineInfo(sMachineID);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }

        public DataTable GetJobInfo(string sJobnumber)
        {
            DataSet ds = dal.SelectJobInfo(sJobnumber);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }


    
        public bool JobMaintainRollBack(LMMSWatchDog_His_Model watchdogModel,LMMSWatchLog_Model watchlogModel, Class.Model.LMMSInventory_Model inventoryModel)
        {
            List<SqlCommand> cmdList = new List<SqlCommand>();


            //update inventory cmd
            Common.Class.DAL.LMMSInventory_DAL inventoryDAL = new Class.DAL.LMMSInventory_DAL();
            cmdList.Add(inventoryDAL.UpdateJobMaintenanceCMD(inventoryModel));



            //update watch dog shift
            cmdList.Add(dal.UpdateJobMaintenanceCMD(watchdogModel));



            //update watch log 
            Common.DAL.LMMSWatchLog_DAL watchlogDAL = new DAL.LMMSWatchLog_DAL();
            cmdList.Add(watchlogDAL.UpdateJobMaintenanceCMD(watchlogModel));



           return DBHelp.SqlDB.SetData_Rollback(cmdList);
        }

        

        public DataTable GetJobMaterialList(string sJobNo, DateTime dDay, string sShift, string sMachineID)
        {
            DataSet ds = dal.GetJobMaterialList(sJobNo, dDay, sShift, sMachineID);

            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }


        
    }
}

