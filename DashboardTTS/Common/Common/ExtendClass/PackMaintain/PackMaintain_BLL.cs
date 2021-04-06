using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.ExtendClass.PackMaintain
{
    public class PackMaintain_BLL
    {
        private readonly PackMaintain_DAL _dal;
        public PackMaintain_BLL()
        {
            _dal = new PackMaintain_DAL();
        }


        public PackMaintain_Model GetMaintainInfo(string sJobNo, string sTrackingID)
        {
            if (string.IsNullOrEmpty(sJobNo) || string.IsNullOrEmpty(sTrackingID))
                throw new Exception("Job No, Tracking ID can not be empty!");


            var model = new PackMaintain_Model();




            // ========== 获取job的基本信息, jobId, trackingID, day, shift, partNumber, mrp qty ==========//
            model.Job = _dal.GetJobInfo(sTrackingID);

            DataTable dtPaint = _dal.GetPaintDelivery(sJobNo);
            if (dtPaint != null && dtPaint.Rows.Count != 0)
            {
                model.Job.MRPQty = decimal.Parse(dtPaint.Rows[0]["MrpQty"].ToString());
            }
            // ========== 获取job的基本信息, jobId, trackingID, day, shift, partNumber, mrp qty ==========//





            // ========== 获取material 信息 ==========//
            // 获取material剩下的库存数量.
            DataTable dtMaterialBinQty = _dal.GetMaterialBinQty(sTrackingID);
            if (dtMaterialBinQty != null || dtMaterialBinQty.Rows.Count != 0)
            {
                foreach (DataRow dr in dtMaterialBinQty.Rows)
                {
                    string materialName = dr["materialName"].ToString();
                    string materialPartNo = dr["materialPartNo"].ToString();
                    decimal binQty = decimal.Parse(dr["Qty"].ToString());
                    model.MaterialNameList.Add(new PackMaintain_Model.MaterialInfo()
                    {
                        MaterialName = materialName,
                        MaterialPartNo = materialPartNo,
                        InventoryQty = binQty
                    });
                }
            }


            // 获取detail tracking实际pack的material 数量
            DataTable dtMaterialPackQty = _dal.GetMaterialPackQty(sTrackingID);
            if (dtMaterialPackQty != null || dtMaterialPackQty.Rows.Count != 0)
            {
                foreach (DataRow dr in dtMaterialPackQty.Rows)
                {
                    string materialName = dr["materialName"].ToString();
                    string materialPartNo = dr["materialPartNo"].ToString();
                    decimal materialQty = decimal.Parse(dr["Qty"].ToString());                    
                    var newModel = model.MaterialNameList.Where(p => p.MaterialPartNo == materialPartNo).FirstOrDefault();
                    if (newModel == null)
                    {
                        model.MaterialNameList.Add(new PackMaintain_Model.MaterialInfo()
                        {
                            MaterialName = materialName,
                            MaterialPartNo = materialPartNo,
                            MaterialQty = materialQty
                        });
                    }else
                    {
                        newModel.MaterialQty = materialQty;
                    }
                }
            }
            // ========== 获取material 信息 ==========//

            return model;
        }


        public bool Update(PackMaintain_Model maintainModel, string sUserID)
        {
            List<SqlCommand> cmdList = new List<SqlCommand>();

            string remark = $"Updated by packing maintenance, userID:{sUserID}";
            string lastedCheckProcess = GetLastedCheckProcess(maintainModel.Job.PartNo);

            //================ 更新pack tracking ================//
            Common.Class.BLL.PQCPackTracking trackBLL = new Class.BLL.PQCPackTracking();
            var trackModel = trackBLL.GetModel(maintainModel.Job.TrackingID);

            decimal totalIncreaseQty = maintainModel.MaterialNameList.Sum(p => p.MaterialQty) - trackModel.acceptQty.Value;
            trackModel.TotalQty += totalIncreaseQty;
            trackModel.acceptQty += totalIncreaseQty;
            trackModel.status = "End";
            trackModel.nextViFlag = "True";
            trackModel.updatedTime = DateTime.Now;
            trackModel.remarks = remark;
            trackModel.lastUpdatedTime = DateTime.Now;
            cmdList.Add(trackBLL.UpdateCommand(trackModel));

            
            //insert his
            Common.Class.BLL.PQCPackHistory trackHisBLL = new Class.BLL.PQCPackHistory();
            cmdList.Add(trackHisBLL.AddCommand(trackModel));
            //================ 更新pack tracking ================//




            
            //================ 更新pack detial tracking ================//
            Common.Class.BLL.PQCPackDetailTracking detailBLL = new Class.BLL.PQCPackDetailTracking();
            Common.Class.BLL.PQCPackDetailHistory_BLL detailHisBLL = new Class.BLL.PQCPackDetailHistory_BLL();

            var detialList = detailBLL.GetModelList(maintainModel.Job.TrackingID);
            foreach (var detail in detialList)
            {
                var material = maintainModel.MaterialNameList.Where(p => p.MaterialPartNo == detail.materialPartNo).FirstOrDefault();
                
                detail.totalQty += (material.MaterialQty - detail.passQty);
                detail.passQty = material.MaterialQty;
                detail.status = "End";
                detail.lastUpdatedTime = DateTime.Now;
                detail.updatedTime = DateTime.Now;
                detail.remarks = remark;

                cmdList.Add(detailBLL.UpdateCommand(detail));

                //insert detail tracking his
                cmdList.Add(detailHisBLL.AddCommand(detail));
            }
            //================ 更新pack detial tracking ================//


            

            #region 更新 pqc qa vi bin        
            Common.Class.BLL.PQCQaViBinning binBLL = new Class.BLL.PQCQaViBinning();
            Common.Class.BLL.PQCQaViBinHistory_BLL binHisBLL = new Class.BLL.PQCQaViBinHistory_BLL();

            //获取这个job的所有记录, 包括checking和packing
            var binList = binBLL.GetModelList(null, null, maintainModel.Job.JobNo, "");

            //获取binList中packing的记录
            var binPackList = binList.Where(p => p.processes == "PACKING" && p.trackingID == maintainModel.Job.TrackingID);
            
            //获取binList中最后一道checking的的数据,   有check#2的数据的话要无视check#1的.
            var binCheckList = binList.Where(p => p.processes == lastedCheckProcess.ToUpper() && p.trackingID == maintainModel.Job.TrackingID);
            
            
            // 遍历每个material part no
            foreach (var material in maintainModel.MaterialNameList)
            {
                //计算每个material维护后的差值.
                var temp = detailBLL.GetModelList(maintainModel.Job.TrackingID);
                var detailModel = temp.Where(p => p.materialPartNo == material.MaterialPartNo).FirstOrDefault();
                decimal increaseQty = material.MaterialQty - detailModel.passQty.Value;


                //在重新赋值前,先用来记录下更新前的数量, 追踪变化
                decimal hisFromQty = 0;



                //获取并更新pack material model
                var binPackMaterial = binPackList.Where(p => p.materialPartNo == material.MaterialPartNo).FirstOrDefault();
                hisFromQty = binPackMaterial.materialQty.Value;//保存下更新前的数量
                binPackMaterial.materialQty += increaseQty;
                binPackMaterial.remarks = remark;
                binPackMaterial.updatedTime = DateTime.Now;
                cmdList.Add(binBLL.UpdateCommand(binPackMaterial));


                //拷贝保存到his list中.
                var binHisModel = binHisBLL.CopyModel(binPackMaterial);
                binHisModel.materialFromQty = hisFromQty;         
                cmdList.Add(binHisBLL.AddCommand(binHisModel));



                //获取并更新 bin 中pack的库存 (即check的数量)
                var binCheckMaterial = binCheckList.Where(p => p.materialPartNo == material.MaterialPartNo).FirstOrDefault();
                if (binCheckMaterial == null)
                {
                    #region new 一条check的记录,还给库存.
                    Common.Class.Model.PQCQaViBinning binModel = new Common.Class.Model.PQCQaViBinning();
                    binModel.PartNumber = maintainModel.Job.PartNo;
                    binModel.jobId = maintainModel.Job.JobNo;
                    binModel.trackingID = maintainModel.Job.TrackingID;
                    binModel.materialPartNo = material.MaterialPartNo;
                    binModel.materialName = material.MaterialName;
                    binModel.shipTo = binPackMaterial.shipTo;
                    binModel.model = binPackMaterial.model;
                    binModel.jigNo = binPackMaterial.jigNo;
                    binModel.updatedTime = DateTime.Now;
                    binModel.status = "LOAD";
                    binModel.nextViFlag = "true";
                    binModel.remark_1 = binPackMaterial.remark_1;
                    binModel.remark_2 = binPackMaterial.remark_2;
                    binModel.remark_3 = "";
                    binModel.remark_4 = "";
                    binModel.remarks = remark;
                    binModel.processes = lastedCheckProcess.ToUpper();

                    binModel.day = binPackMaterial.day;
                    binModel.shift = binPackMaterial.shift;
                    binModel.userName = binPackMaterial.userName;//偷懒了直接用pack的user了
                    binModel.userID = binPackMaterial.userID;//偷懒了直接用pack的user了

                    binModel.id = Guid.NewGuid().ToString();
                    binModel.materialQty = Math.Abs(increaseQty);
                    binModel.dateTime = DateTime.Now;
                    
                    //insert new bin model
                    cmdList.Add(binBLL.AddCommand(binModel));



                    //copy to his model and insert
                    Common.Class.Model.PQCQaViBinHistory_Model hisModel = new Common.Class.Model.PQCQaViBinHistory_Model();
                    hisModel = binHisBLL.CopyModel(binModel);
                    hisModel.materialFromQty = 0;
                    cmdList.Add(binHisBLL.AddCommand(hisModel));

                    #endregion
                }else
                {
                    #region 正常更新当前的check bin material的数据

                    //处理拷贝到bin his model
                    binHisModel = new Class.Model.PQCQaViBinHistory_Model();
                    binHisModel = binHisBLL.CopyModel(binCheckMaterial);
                    binHisModel.materialFromQty = binCheckMaterial.materialQty.Value;//在更新check bin material前先保存下更新前的数量


                    //处理bin check material
                    binCheckMaterial.materialQty -= increaseQty;
                    binCheckMaterial.remarks = remark;
                    binCheckMaterial.updatedTime = DateTime.Now;


                    //如果更新后库存数量为0, 则直接删除, 不是则直接更新
                    if (binCheckMaterial.materialQty == 0)
                    {
                        SqlCommand cmdDelete = binBLL.DeleteCheckCommand(maintainModel.Job.TrackingID, material.MaterialPartNo);
                        cmdList.Add(cmdDelete);


                        
                        binHisModel.status = "Delete"; //标记下再bin中被删除了
                        binHisModel.materialQty = binCheckMaterial.materialQty;
                    }
                    else
                    {
                        //直接更新bin check material.
                        cmdList.Add(binBLL.UpdateCommand(binCheckMaterial));

                        
                        binHisModel.materialQty = binCheckMaterial.materialQty;
                    }

                    //添加到bin his list中
                    cmdList.Add(binHisBLL.AddCommand(binHisModel));
                    #endregion
                }
            }
            #endregion

            

            return DBHelp.SqlDB.SetData_Rollback(cmdList,DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
        }



        private string GetLastedCheckProcess(string sPartNo)
        {
            Common.Class.BLL.PQCBom_BLL bomBLL = new Class.BLL.PQCBom_BLL();
            var pqcBom = bomBLL.GetModel(sPartNo);
            string process = pqcBom.processes.Split('-').Where(p => p.Contains("Check")).Max();

            return process;
        }

    }
}
