using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.ExtendClass.PQCProduction.PackMaintain
{
    public class PackMaintain_BLL
    {
        private readonly Core.Base_BLL _baseBLL;

        public PackMaintain_BLL()
        {
            _baseBLL = new Core.Base_BLL();
        }


        public PackMaintain_Model GetMaintainInfo(string sJobNo, string sTrackingID)
        {
            if (string.IsNullOrEmpty(sJobNo) || string.IsNullOrEmpty(sTrackingID))
                throw new Exception("Job No, Tracking ID can not be empty!");


            var maintainModel = new PackMaintain_Model();



            // ========== 获取job的基本信息, jobId, trackingID, day, shift, partNumber, mrp qty ==========//
            var baseModel = _baseBLL.GetPackingModel(sTrackingID);
            maintainModel.Job.JobNo = baseModel.JobNo;
            maintainModel.Job.TrackingID = baseModel.TrackingID;
            maintainModel.Job.Day = baseModel.Day;
            maintainModel.Job.Shift = baseModel.Shift;
            maintainModel.Job.PartNo = baseModel.PartNo;

            var paintBaseModel = _baseBLL.GetLotInfoModel(sJobNo);
            maintainModel.Job.MRPQty = paintBaseModel == null? 0: _baseBLL.GetLotInfoModel(sJobNo).LotQty;



            

            // ========== 获取material 信息 ==========//

            // 获取 bom, 用来遍历每一个 material. 以当前 bom 为准.
            var bom = _baseBLL.GetBomModel(maintainModel.Job.PartNo);
            


            // 从 pack vi detial 中获取 op pack 每个 material part 的数量
            var checkViDetailList = _baseBLL.GetPackDetailList(new Taiyo.SearchParam.PQCParam.PQCOutputParam()
            {
                TrackingID = sTrackingID
            });



            // 从 checking bin 中获取还没有 pack 的库存.
            var checkBinList = _baseBLL.GetBinList(new Taiyo.SearchParam.PQCParam.PQCOutputParam()
            {
                TrackingID = sTrackingID
            });
            checkBinList = checkBinList == null ? null: checkBinList.Where(p => p.Processes == bom.LastCheckProcess).ToList();



            // 从 bin history 中获取 scrap 的数量.
            var scrapList = _baseBLL.GetBinHisScrapList(new Taiyo.SearchParam.PQCParam.PQCOutputParam()
            {
                TrackingID = sTrackingID
            });




            // 遍历每个 material 进行赋值
            foreach (var item in bom.MaterialPartList)
            {
                var maintainMaterial = new PackMaintain_Model.MaterialInfo();
                maintainMaterial.MaterialName = item.MaterialName;
                maintainMaterial.MaterialPartNo = item.MaterialPartNo;


                // checking bin inventory 数量
                var checkMaterialBinModel = checkBinList == null? null: checkBinList.Where(p => p.MaterialPartNo == item.MaterialPartNo).FirstOrDefault();
                maintainMaterial.InventoryQty = checkMaterialBinModel == null? 0: checkMaterialBinModel.MaterialQty;

                // op pack 的数量
                var checkViDetailModel = checkViDetailList ==null? null : checkViDetailList.Where(p => p.MaterialPartNo == item.MaterialPartNo).FirstOrDefault();
                maintainMaterial.MaterialQty = checkViDetailModel == null ? 0 : checkViDetailModel.TotalQty;

                // 被 scrap 掉的数量
                var scrapModel = scrapList == null? null: scrapList.Where(p => p.MaterialPartNo == item.MaterialPartNo).FirstOrDefault();
                maintainMaterial.ScrapQty = scrapModel == null ? 0 : scrapModel.MaterialQty;



                maintainModel.MaterialNameList.Add(maintainMaterial);
            }


            

            return maintainModel;
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
