using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Taiyo.Tool.Extension;

namespace Common.ExtendClass.PQCProduction.PackMaintain
{
    public class PackMaintain_BLL
    {
        private readonly Core.Base_BLL _baseBLL = new Core.Base_BLL();

        private readonly Common.Class.BLL.PQCPackTracking _packTrackingBLL = new Class.BLL.PQCPackTracking();
        private readonly Common.Class.BLL.PQCPackHistory _packTrackingHisBLL = new Class.BLL.PQCPackHistory();

        private readonly Common.Class.BLL.PQCPackDetailTracking _packTrackingDetailBLL = new Class.BLL.PQCPackDetailTracking();
        private readonly Common.Class.BLL.PQCPackDetailHistory_BLL _packTrackingDetailHisBLL = new Class.BLL.PQCPackDetailHistory_BLL();

        private readonly Common.Class.BLL.PQCQaViBinning _binBLL = new Class.BLL.PQCQaViBinning();
        private readonly Common.Class.BLL.PQCQaViBinHistory_BLL _binHisBLL = new Class.BLL.PQCQaViBinHistory_BLL();



        public PackMaintain_Model GetMaintainInfo(string sJobNo, string sTrackingID)
        {
            if (string.IsNullOrEmpty(sJobNo) && string.IsNullOrEmpty(sTrackingID))
                throw new Exception("Job No,Tracking ID can not be both empty!");



            // 1. 获取job的基本信息
            // 1.1 有 tracking id, 说明有 pack tracking 记录,直接从 pack tracking 表获取
            // 1.2 没 tracking id, 说明在做 pack 之前就被 scrap 掉了. 
            //     导致没有任何 packing tracking, bin check/pack 的数据, 
            //     所以根据 jobno 从 checking tracking 表中获取记录
            // 1.3 从 painting 中获取 mrp qty

            // 2. 获取 material 信息
            // 2.1 获取 bom, 以 bom 中的 material part 为准
            // 2.2 从 pack vi detial 中获取 op 实际 pack qty
            // 2.3 从 bin 中获取 check qty
            // 2.4 从 bin history 中获取 scrap qty
            // 2.5 遍历每个 material, 生成 job 的 material part list



            #region 1. 获取job的基本信息

            var maintainModel = new PackMaintain_Model();

            // 1.1 有 tracking id, 说明有 pack tracking 记录,直接从 pack tracking 表获取
            if (!string.IsNullOrEmpty(sTrackingID))
            {
                var baseModel = _baseBLL.GetPackingModel(sTrackingID);
                maintainModel.Job.JobNo = baseModel.JobNo;
                maintainModel.Job.TrackingID = baseModel.TrackingID;
                maintainModel.Job.Day = baseModel.Day;
                maintainModel.Job.Shift = baseModel.Shift;
                maintainModel.Job.PartNo = baseModel.PartNo;
                maintainModel.Job.PackQty = baseModel.TotalQty;
            }
            // 1.2 没 tracking id, 说明在做 pack 之前就被 scrap 掉了. 
            // 导致没有任何 packing tracking, bin check/pack 的数据, 
            // 所以根据 jobno 从 checking tracking 表中获取记录
            else
            {
                // 随便从 list 中取出一个就行, 只需要显示 job 的基础信息.
                var checkModel = _baseBLL.GetCheckingList(new Taiyo.SearchParam.PQCParam.PQCOutputParam(){JobNo = sJobNo}).FirstOrDefault(); 

                maintainModel.Job.JobNo = checkModel.JobNo;
                maintainModel.Job.TrackingID = "";
                maintainModel.Job.Day = null;
                maintainModel.Job.Shift = "";
                maintainModel.Job.PartNo = checkModel.PartNo;
                maintainModel.Job.PackQty = 0;
            }

            // 1.3 从 painting 中获取 mrp qty
            var paintBaseModel = _baseBLL.GetLotInfoModel(sJobNo);
            maintainModel.Job.MRPQty = paintBaseModel == null ? 0 : _baseBLL.GetLotInfoModel(sJobNo).LotQty;
            
            #endregion


            
            #region 2. 获取 material 信息

            // 2.1 获取 bom, 以 bom 中的 material part 为准
            var bom = _baseBLL.GetBomModel(maintainModel.Job.PartNo);
            


            // 2.2 从 pack vi detial 中获取 op 实际 pack qty
            var packDetailList = string.IsNullOrEmpty(sTrackingID) ? null : _baseBLL.GetPackDetailList(new Taiyo.SearchParam.PQCParam.PQCOutputParam()
            {
                TrackingID = sTrackingID
            });



            // 2.3 从 bin 中获取 check qty
            var checkBinList = string.IsNullOrEmpty(sTrackingID) ? null :  _baseBLL.GetBinList(new Taiyo.SearchParam.PQCParam.PQCOutputParam()
            {
                JobNo = maintainModel.Job.JobNo
            });
            checkBinList = checkBinList == null ? null: checkBinList.Where(p => p.Processes == bom.LastCheckProcess.ToUpper()).ToList();



            // 2.4 从 bin history 中获取 scrap qty
            var scrapList = _baseBLL.GetBinHisScrapList(new Taiyo.SearchParam.PQCParam.PQCOutputParam()
            {
                JobNo = maintainModel.Job.JobNo
            });




            // 2.5 遍历每个 material, 生成 job 的 material part list
            foreach (var item in bom.MaterialPartList)
            {
                var maintainMaterial = new PackMaintain_Model.MaterialInfo();
                maintainMaterial.MaterialName = item.MaterialName;
                maintainMaterial.MaterialPartNo = item.MaterialPartNo;


                // 2.5.1 取出这个 material's checking bin qty
                var checkMaterialBinModel = checkBinList == null? null: checkBinList.Where(p => p.MaterialPartNo == item.MaterialPartNo).FirstOrDefault();
                maintainMaterial.InventoryQty = checkMaterialBinModel == null? 0: checkMaterialBinModel.MaterialQty;

                // 2.5.2 取出这个 material's pack qty
                var packDetailModel = packDetailList == null? null : packDetailList.Where(p => p.MaterialPartNo == item.MaterialPartNo).FirstOrDefault();
                maintainMaterial.MaterialQty = packDetailModel == null ? 0 : packDetailModel.TotalQty;

                // 2.5.3 取出这个 material's scrap qty
                var scrapModel = scrapList == null? null: scrapList.Where(p => p.MaterialPartNo == item.MaterialPartNo).FirstOrDefault();
                maintainMaterial.ScrapQty = scrapModel == null ? 0 : scrapModel.MaterialQty;



                maintainModel.MaterialPartList.Add(maintainMaterial);
            }
            #endregion



            return maintainModel;
        }







        /// <summary>
        /// 2021 - 04 - 27
        /// 1. 更新 pack tracking 记录
        /// 1.1 有 pack tracking,  直接更新
        /// 1.2 没有 pack tracking 记录, 创建一条新的记录
        /// 1.3 插入 pack tracking history 记录
        /// 
        /// 
        /// 2. 更新 pack detail tracking 记录
        /// 2.1 有 pack detial tracking, 直接更新
        /// 2.2 没有 pack detail tracking, 创建一条新的记录
        /// 2.3 插入 pack detail tracking history 记录
        /// 
        /// 
        /// 3. 更新 bin 表
        /// 
        /// 3.1 更新 bin pack 记录
        /// 3.1.1 有 bin pack 记录, 直接更新 
        /// 3.1.2 没有 bin pack 记录, 创建一条新的记录
        /// 3.1.3 insert bin his
        /// 
        /// 3.2 更新 bin check 记录
        /// 3.2.1 有 bin check 库存 
        /// 3.2.1.1 维护 pack 多了, 还给 check 库存
        /// 3.2.1.2 维护 pack 少了, 从 check 库存扣除
        /// 3.2.1.2.1 如果库存不够. 从 scrap 中拉回数量, check 库存则直接删除.
        /// 3.2.1.2.1 如果库存够. 直接更新 check bin
        /// 3.2.2 没有 bin check 库存
        /// 3.2.2.1 维护 pack 多了, 还给 check 库存, 创建一条新的记录.
        /// 3.2.2.2 维护 pack 少了, 没库存, 不做处理.
        /// 3.2.3 insert his
        /// 
        /// 
        /// 4. 更新 packing scrap bin history
        /// 4.1 更新 laser user event log , 只有在更新 packing scrap history 的记录才记录下更新动作, 以作记录追踪.
        ///
        /// </summary>
        public bool Update(PackMaintain_Model maintainModel, string sUserID)
        {
            try
            {

                #region 提前 声明 & 处理 一些更新时需要的数据.

                // 给没有 pack 记录, 生成新建时统一的一个 guid.
                string packTrackingID = Guid.NewGuid().ToString();

                string remark = $"Updated by packing maintenance, userID:{sUserID}";

                // 维护后, 增加的总数量
                decimal totalIncreaseQty = maintainModel.MaterialPartList.Sum(p => p.UpdatedQty - p.MaterialQty);

                // 每个 material 以及对应更新前的数量的 dictionary.
                Dictionary<string, decimal> dicMaterialIncreaseQty = new Dictionary<string, decimal>();
                maintainModel.MaterialPartList.ForEach(material =>
                {
                    dicMaterialIncreaseQty.Add(material.MaterialPartNo, material.UpdatedQty - material.MaterialQty);
                });
                 
                // 该 part bom 信息.
                var bom = _baseBLL.GetBomModel(maintainModel.Job.PartNo);

                // 最后一道 check 的工序
                string lastCheckProcess = bom.LastCheckProcess;

                // 获取该记录 check model
                var checkTrackingModel = _baseBLL.GetCheckingList(new Taiyo.SearchParam.PQCParam.PQCOutputParam()
                {
                    JobNo = maintainModel.Job.JobNo
                }).OrderByDescending(p => p.EndTime).First();
          


                List<SqlCommand> cmdList = new List<SqlCommand>();

                #endregion


                
                #region 1. 更新 pack tracking 记录

                var packTrackingModel = _packTrackingBLL.GetModel(maintainModel.Job.TrackingID);

                if (packTrackingModel != null)
                {
                    // 1.1 有 pack tracking,  直接更新                    
                    packTrackingModel.TotalQty += totalIncreaseQty;
                    packTrackingModel.acceptQty += totalIncreaseQty;
                    packTrackingModel.status = "End";
                    packTrackingModel.nextViFlag = "True";
                    packTrackingModel.updatedTime = DateTime.Now;
                    packTrackingModel.remarks = remark;
                    packTrackingModel.lastUpdatedTime = DateTime.Now;
                    cmdList.Add(_packTrackingBLL.UpdateCommand(packTrackingModel));
                }
                else
                {
                    // 1.2 没有 pack tracking 记录, 创建一条新的记录
                    packTrackingModel = new Class.Model.PQCPackTracking_Model();
                    packTrackingModel.id = 0;
                    packTrackingModel.machineID = "";
                    packTrackingModel.dateTime = DateTime.Now;
                    packTrackingModel.partNumber = maintainModel.Job.PartNo;
                    packTrackingModel.jobId = maintainModel.Job.JobNo;
                    packTrackingModel.processes = "PACKING";
                    packTrackingModel.jigNo = bom.JigNo;
                    packTrackingModel.model = bom.Model;
                    packTrackingModel.cavityCount = null;
                    packTrackingModel.cycleTime = null;
                    packTrackingModel.targetQty = null;
                    packTrackingModel.userName = "";
                    packTrackingModel.userID = sUserID;
                    packTrackingModel.TotalQty = maintainModel.MaterialPartList.Sum(p => p.UpdatedQty);
                    packTrackingModel.rejectQty = 0;
                    packTrackingModel.acceptQty = maintainModel.MaterialPartList.Sum(p => p.UpdatedQty);
                    packTrackingModel.startTime = DateTime.Now;
                    packTrackingModel.stopTime = DateTime.Now;
                    packTrackingModel.nextViFlag = (true).ToString();
                    packTrackingModel.day = DateTime.Now.Date;
                    packTrackingModel.shift = Taiyo.Tool.Common.GetCurrentShift().GetDescription();
                    packTrackingModel.status = "End";
                    packTrackingModel.remark_1 = "";
                    packTrackingModel.remark_2 = "";
                    packTrackingModel.refField01 = "";
                    packTrackingModel.refField02 = "";
                    packTrackingModel.refField03 = "";
                    packTrackingModel.refField04 = "";
                    packTrackingModel.refField05 = "";
                    packTrackingModel.refField06 = "";
                    packTrackingModel.refField07 = "";
                    packTrackingModel.refField08 = "";
                    packTrackingModel.refField09 = "";
                    packTrackingModel.refField10 = "";
                    packTrackingModel.refField11 = "";
                    packTrackingModel.refField12 = "";
                    packTrackingModel.customer = bom.Customer;
                    packTrackingModel.lastUpdatedTime = DateTime.Now;
                    packTrackingModel.trackingID = packTrackingID;
                    packTrackingModel.lastTrackingID = "";
                    packTrackingModel.remarks = remark;
                    packTrackingModel.department = "Packing";
                    packTrackingModel.totalRejectQty = null;
                    packTrackingModel.updatedTime = DateTime.Now;
                    packTrackingModel.totalPassQty = null;
                    packTrackingModel.shipTo = bom.ShipTo;
                    packTrackingModel.indexId = 0;

                    cmdList.Add(_packTrackingBLL.AddCommand(packTrackingModel));
                }

                // 1.3 插入 pack tracking history 记录
                cmdList.Add(_packTrackingHisBLL.AddCommand(packTrackingModel));

                #endregion
                

                #region 2. 更新 pack detail tracking 记录

                var packTrackingDetailList = _packTrackingDetailBLL.GetModelList(maintainModel.Job.TrackingID);
             
                if (packTrackingDetailList != null && packTrackingDetailList.Count != 0)
                {
                    // 2.1 有 pack tracking detail , 直接更新
                    packTrackingDetailList.ForEach(packDetail =>
                    {
                        //pack detail 中 totalQty 是这个 job 上一站的总数量, 不需要更新
                        packDetail.passQty += dicMaterialIncreaseQty[packDetail.materialPartNo];
                        packDetail.status = "End";
                        packDetail.lastUpdatedTime = DateTime.Now;
                        packDetail.updatedTime = DateTime.Now;
                        packDetail.remarks = remark;
                        packDetail.userID = sUserID;
                        cmdList.Add(_packTrackingDetailBLL.UpdateCommand(packDetail));


                        // 2.3 插入 pack detail tracking history 记录
                        cmdList.Add(_packTrackingDetailHisBLL.AddCommand(packDetail));
                    });
                }
                else
                {
                    // 2.2 没有 pack detail tracking, 创建新的记录
                    maintainModel.MaterialPartList.ForEach(maintainMaterialModel =>
                    {
                        var packTrackDetialModel = new Class.Model.PQCPackDetailTracking_Model();
                        packTrackDetialModel.id = 0;
                        packTrackDetialModel.trackingID = packTrackingID;
                        packTrackDetialModel.machineID = "";
                        packTrackDetialModel.dateTime = DateTime.Now;
                        packTrackDetialModel.materialPartNo = maintainMaterialModel.MaterialPartNo;
                        packTrackDetialModel.jigNo = bom.JigNo;
                        packTrackDetialModel.model = bom.Model;
                        packTrackDetialModel.cavityCount = null;
                        packTrackDetialModel.userName = "";
                        packTrackDetialModel.userID = sUserID;
                        packTrackDetialModel.startTime = DateTime.Now;
                        packTrackDetialModel.stopTime = DateTime.Now;
                        packTrackDetialModel.day = DateTime.Now.Date;
                        packTrackDetialModel.shift = Taiyo.Tool.Common.GetCurrentShift().GetDescription();
                        packTrackDetialModel.status = "End";
                        packTrackDetialModel.remark_1 = "";
                        packTrackDetialModel.remark_2 = "";
                        packTrackDetialModel.rejectQty = 0;
                        packTrackDetialModel.rejectQtyHour01 = "";
                        packTrackDetialModel.rejectQtyHour02 = "";
                        packTrackDetialModel.rejectQtyHour03 = "";
                        packTrackDetialModel.rejectQtyHour04 = "";
                        packTrackDetialModel.rejectQtyHour05 = "";
                        packTrackDetialModel.rejectQtyHour06 = "";
                        packTrackDetialModel.rejectQtyHour07 = "";
                        packTrackDetialModel.rejectQtyHour08 = "";
                        packTrackDetialModel.rejectQtyHour09 = "";
                        packTrackDetialModel.rejectQtyHour10 = "";
                        packTrackDetialModel.rejectQtyHour11 = "";
                        packTrackDetialModel.rejectQtyHour12 = "";
                        packTrackDetialModel.lastUpdatedTime = DateTime.Now;
                        packTrackDetialModel.remarks = remark;
                        packTrackDetialModel.processes = "PACKING";
                        packTrackDetialModel.jobId = maintainModel.Job.JobNo;
                        packTrackDetialModel.totalQty = maintainModel.MaterialPartList.Sum(p => p.UpdatedQty);
                        packTrackDetialModel.updatedTime = DateTime.Now;
                        packTrackDetialModel.passQty = maintainMaterialModel.UpdatedQty;
                        packTrackDetialModel.totalPassQty = null;
                        packTrackDetialModel.totalRejectQty = null;
                        packTrackDetialModel.color = bom.Color;
                        packTrackDetialModel.materialName = maintainMaterialModel.MaterialName;
                        packTrackDetialModel.outerBoxQty = bom.MaterialPartList.Where(p => p.MaterialPartNo == maintainMaterialModel.MaterialName).FirstOrDefault().OuterBoxQty;
                        packTrackDetialModel.packingTrays = bom.MaterialPartList.Where(p => p.MaterialPartNo == maintainMaterialModel.MaterialName).FirstOrDefault().PackingTrays;
                        packTrackDetialModel.customer = bom.Customer;
                        packTrackDetialModel.shipTo = bom.ShipTo;
                        packTrackDetialModel.module = bom.MaterialPartList.Where(p => p.MaterialPartNo == maintainMaterialModel.MaterialName).FirstOrDefault().Module;
                        packTrackDetialModel.sn = 0;
                        packTrackDetialModel.indexId = 0;

                        cmdList.Add(_packTrackingDetailBLL.AddCommand(packTrackDetialModel));

                        // 2.3 插入 pack detail tracking history 记录
                        cmdList.Add(_packTrackingDetailHisBLL.AddCommand(packTrackDetialModel));
                    });
                }
                #endregion


                // 3. 更新 bin 表      册那 最烦的地方来了
                var binList = _binBLL.GetModelList(null, null, maintainModel.Job.JobNo, "");
                
                #region 3.1 更新 bin pack 记录
                var packList = binList == null ? null : binList.Where(p => p.processes == "PACKING").ToList();
                if (packList != null && packList.Count != 0)
                {
                    // 3.1.1 有 bin pack 记录, 直接更新
                    packList.ForEach(packMaterial =>
                    {
                       packMaterial.materialQty = maintainModel.MaterialPartList.Where(p => p.MaterialPartNo == packMaterial.materialPartNo).First().UpdatedQty;
                       packMaterial.status = "End";
                       packMaterial.nextViFlag = "True";
                       packMaterial.userID = sUserID;
                       packMaterial.remarks = remark;
                       packMaterial.updatedTime = DateTime.Now;
                       cmdList.Add(_binBLL.UpdateCommand(packMaterial));


                       // 3.1.3 插入 pack detail tracking history 记录
                       var packMaterialHisModel = _binHisBLL.CopyModel(packMaterial);
                       packMaterialHisModel.materialFromQty = maintainModel.MaterialPartList.Where(p => p.MaterialPartNo == packMaterial.materialPartNo).First().MaterialQty;
                       cmdList.Add(_binHisBLL.AddCommand(packMaterialHisModel));                       
                    });
                }
                else
                {
                    // 3.1.2 没有 bin pack 记录, 创建一条新的记录
                    maintainModel.MaterialPartList.ForEach(maintainMaterialModel =>
                    {
                        var binModel = new Common.Class.Model.PQCQaViBinning();
                        binModel.id = "";
                        binModel.trackingID = packTrackingID;
                        binModel.processes = "PACKING";
                        binModel.jobId = maintainModel.Job.JobNo;
                        binModel.PartNumber = bom.PartNo;
                        binModel.materialPartNo = maintainMaterialModel.MaterialPartNo;
                        binModel.materialName = maintainMaterialModel.MaterialName;
                        binModel.shipTo = bom.ShipTo;
                        binModel.model = bom.Model;
                        binModel.jigNo = bom.JigNo;
                        binModel.materialQty = maintainMaterialModel.UpdatedQty;
                        binModel.status = "End";
                        binModel.nextViFlag = "True";
                        binModel.dateTime = DateTime.Now;
                        binModel.day = DateTime.Now.Date;
                        binModel.shift = Taiyo.Tool.Common.GetCurrentShift().GetDescription();
                        binModel.userName = "";
                        binModel.userID = sUserID;
                        binModel.remark_1 = "";
                        binModel.remark_2 = "";
                        binModel.remark_3 = "";
                        binModel.remark_4 = "";
                        binModel.remarks = remark;
                        binModel.updatedTime = DateTime.Now;
                        cmdList.Add(_binBLL.AddCommand(binModel));


                        // 3.1.3 insert bin his 
                        var binHisModel = _binHisBLL.CopyModel(binModel);
                        binHisModel.materialFromQty = 0;
                        cmdList.Add(_binHisBLL.AddCommand(binHisModel));
                    });
                }
                #endregion

                #region 3.2 更新 bin check 记录
                
                // 遍历每个 materialpart
                maintainModel.MaterialPartList.ForEach(maintainMaterialPart =>
                {
                    var binCheckMaterial = binList.Where(p => p.processes == lastCheckProcess.ToUpper() && p.materialPartNo == maintainMaterialPart.MaterialPartNo).ToList().FirstOrDefault();
                    if (binCheckMaterial != null)
                    {
                        #region 3.2.1 有 bin check 库存
                        if (dicMaterialIncreaseQty[maintainMaterialPart.MaterialPartNo] < 0)
                        {
                            // 3.2.1.1 维护 pack 多了, 还给 check 库存    --- (维护增加的数量 < 0,  说明原先 pack 数量多了, 还给 check 库存)
                            binCheckMaterial.materialQty += Math.Abs(dicMaterialIncreaseQty[maintainMaterialPart.MaterialPartNo]);
                            binCheckMaterial.status = "LOAD";
                            binCheckMaterial.userID = sUserID;
                            binCheckMaterial.remarks = remark;
                            binCheckMaterial.updatedTime = DateTime.Now;
                            binCheckMaterial.userID = sUserID;
                            cmdList.Add(_binBLL.UpdateCommand(binCheckMaterial));


                            // 3.2.3 insert his
                            var binCheckHisModel = _binHisBLL.CopyModel(binCheckMaterial);
                            binCheckHisModel.materialFromQty -= Math.Abs(dicMaterialIncreaseQty[maintainMaterialPart.MaterialPartNo]);
                            cmdList.Add(_binHisBLL.AddCommand(binCheckHisModel));
                        }
                        else
                        {
                            // 3.2.1.2 维护 pack 少了, 从 check 库存扣除    --- (维护增加的数量 > 0,  说明原先 pack 数量少了, 从 check 库存扣除)
                            if (maintainMaterialPart.UpdatedQty - maintainMaterialPart.MaterialQty >= maintainMaterialPart.InventoryQty)
                            {
                                // 3.2.1.2.1 如果库存不够. 从 scrap 中拉回数量, check 库存则直接删除.
                                cmdList.Add(_binBLL.DeleteCheckCommand(binCheckMaterial.trackingID, binCheckMaterial.materialPartNo));

                                // 3.2.3 insert his
                                var binCheckHisModel = _binHisBLL.CopyModel(binCheckMaterial);
                                binCheckHisModel.materialFromQty = binCheckMaterial.materialQty;
                                binCheckHisModel.status = "DELETE";
                                cmdList.Add(_binHisBLL.AddCommand(binCheckHisModel));
                            }
                            else
                            {
                                // 3.2.1.2.1 如果库存够. 直接更新 check bin
                                binCheckMaterial.materialQty -= Math.Abs(dicMaterialIncreaseQty[maintainMaterialPart.MaterialPartNo]);
                                binCheckMaterial.status = "UNLOAD";
                                binCheckMaterial.userID = sUserID;
                                binCheckMaterial.remarks = remark;
                                binCheckMaterial.updatedTime = DateTime.Now;
                                binCheckMaterial.userID = sUserID;
                                cmdList.Add(_binBLL.UpdateCommand(binCheckMaterial));


                                // 3.2.3 insert his
                                var binCheckHisModel = _binHisBLL.CopyModel(binCheckMaterial);
                                binCheckHisModel.materialFromQty += Math.Abs(dicMaterialIncreaseQty[maintainMaterialPart.MaterialPartNo]);
                                cmdList.Add(_binHisBLL.AddCommand(binCheckHisModel));
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region 3.2.2 没有 bin check 库存
                        if (dicMaterialIncreaseQty[maintainMaterialPart.MaterialPartNo] < 0)
                        {
                            // 3.2.2.1 维护 pack 多了, 还给 check 库存    --- (维护增加的数量 < 0,  说明原先 pack 数量多了, 还给 check 库存)
                            binCheckMaterial = new Common.Class.Model.PQCQaViBinning();
                            binCheckMaterial.PartNumber = maintainModel.Job.PartNo;
                            binCheckMaterial.jobId = maintainModel.Job.JobNo;
                            binCheckMaterial.trackingID = checkTrackingModel.TrackingID;// check tracking 最后一次 check 记录的 tracking id
                            binCheckMaterial.materialPartNo = maintainMaterialPart.MaterialPartNo;
                            binCheckMaterial.materialName = maintainMaterialPart.MaterialName;
                            binCheckMaterial.shipTo = bom.ShipTo;
                            binCheckMaterial.model = bom.Model;
                            binCheckMaterial.jigNo = bom.JigNo;
                            binCheckMaterial.updatedTime = DateTime.Now;
                            binCheckMaterial.status = "LOAD";
                            binCheckMaterial.nextViFlag = "true";
                            binCheckMaterial.remark_1 = "";
                            binCheckMaterial.remark_2 = "";
                            binCheckMaterial.remark_3 = "";
                            binCheckMaterial.remark_4 = "";
                            binCheckMaterial.remarks = remark;
                            binCheckMaterial.processes = lastCheckProcess.ToUpper();
                            binCheckMaterial.day = checkTrackingModel.Day;
                            binCheckMaterial.shift = checkTrackingModel.Shift;
                            binCheckMaterial.userName = checkTrackingModel.Opertor;
                            binCheckMaterial.userID = sUserID;
                            binCheckMaterial.id = "";
                            binCheckMaterial.materialQty = Math.Abs(dicMaterialIncreaseQty[maintainMaterialPart.MaterialPartNo]);
                            binCheckMaterial.dateTime = DateTime.Now;
                            cmdList.Add(_binBLL.AddCommand(binCheckMaterial));


                            // 3.2.3 insert his
                            var binCheckHisModel = _binHisBLL.CopyModel(binCheckMaterial);
                            binCheckHisModel.materialFromQty -= Math.Abs(dicMaterialIncreaseQty[maintainMaterialPart.MaterialPartNo]);
                            cmdList.Add(_binHisBLL.AddCommand(binCheckHisModel));
                        }
                        else
                        {
                            // 3.2.2.2 维护 pack 少了, 从 check 库存扣除    --- (维护增加的数量 > 0,  说明原先 pack 数量少了, 从 check 库存扣除)

                            // 由于本身就没 check 库存信息, 这里不做任何处理
                        }
                        #endregion
                    }
                });
                #endregion

                             
                #region 4. 更新 packing scrap bin history

                // 为了尽量让逻辑清晰, 分离每个表的更新逻辑
                // scrap 的更新不嵌入到步骤3中的分支判断中
                // 而在这里从新判断.


                //获取 bin his中的scrap的数据.
                var hisScrapList = _binHisBLL.GetList(maintainModel.Job.JobNo).Where(p => p.status == "SCRAP").ToList();

                maintainModel.MaterialPartList.ForEach(maintainMaterialPart =>
                {
                    // 只有当维护新增的数量 (updatedqty - materialqty) > check 库存的数量时, 才从 scrap 中拉回数量.
                    if (maintainMaterialPart.UpdatedQty - maintainMaterialPart.MaterialQty > maintainMaterialPart.InventoryQty)
                    {
                        var scrapMaterial = hisScrapList.Where(p => p.materialPartNo == maintainMaterialPart.MaterialPartNo).FirstOrDefault();
                        scrapMaterial.materialFromQty = scrapMaterial.materialQty;// 先保存下更新前的数量.
                        scrapMaterial.materialQty -= Math.Abs(dicMaterialIncreaseQty[maintainMaterialPart.MaterialPartNo]);// 报废数量 - 被维护拉回来的数量
                        scrapMaterial.userID = sUserID;
                        scrapMaterial.updatedTime = DateTime.Now;
                        scrapMaterial.remarks = remark;
                        cmdList.Add(_binHisBLL.UpdateScrapCommand(scrapMaterial));

                        #region 4.1 更新 laser user event log , 只有在更新 packing scrap history 的记录才记录下更新动作, 以作记录追踪.
                        var userEventModel = new Common.Class.Model.LMMSUserEventLog();
                        userEventModel.jobnumber = maintainModel.Job.JobNo;
                        userEventModel.material = maintainMaterialPart.MaterialPartNo;
                        userEventModel.dateTime = DateTime.Now;
                        userEventModel.startTime = DateTime.Now;
                        userEventModel.endTime = DateTime.Now;
                        userEventModel.eventType = "Packing Maintenance";
                        userEventModel.pageName = "Packing Maintenance";
                        userEventModel.action = $"material [{maintainMaterialPart.MaterialPartNo}],  before qty {scrapMaterial.materialFromQty} --> after maintained qty {scrapMaterial.materialQty}";
                        userEventModel.temp1 = "";
                        userEventModel.temp2 = "";
                        userEventModel.userID = sUserID;
                        
                        var eventBLL = new Common.Class.BLL.LMMSUserEventLog_BLL();
                        bool result = eventBLL.AddRollBack(new List<Class.Model.LMMSUserEventLog>() { userEventModel });
                        if (!result)
                        {
                            DBHelp.Reports.LogFile.Log("Packing Maintenance", "Insert event into user event log  fail!");
                        }
                        #endregion
                    }
                });
                #endregion

                

                return DBHelp.SqlDB.SetData_Rollback(cmdList, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }

        


        public bool End(string sTrackingID, string sUserID)
        {
            try
            {
                List<SqlCommand> cmdList = new List<SqlCommand>();

                var packTrackingModel = _packTrackingBLL.GetModel(sTrackingID);
                packTrackingModel.status = "End";
                packTrackingModel.nextViFlag = "True";
                packTrackingModel.updatedTime = DateTime.Now;
                packTrackingModel.remarks = $"Updated by packing maintenance, userID:{sUserID}";
                packTrackingModel.lastUpdatedTime = DateTime.Now;
                cmdList.Add(_packTrackingBLL.UpdateCommand(packTrackingModel));

                cmdList.Add(_packTrackingHisBLL.AddCommand(packTrackingModel));



                var packTrackingDetailList = _packTrackingDetailBLL.GetModelList(sTrackingID);
                packTrackingDetailList.ForEach(packDetail =>
                {
                    packDetail.status = "End";
                    packDetail.lastUpdatedTime = DateTime.Now;
                    packDetail.updatedTime = DateTime.Now;
                    packDetail.remarks = $"Updated by packing maintenance, userID:{sUserID}";
                    packDetail.userID = sUserID;
                    cmdList.Add(_packTrackingDetailBLL.UpdateCommand(packDetail));


                    // 2.3 插入 pack detail tracking history 记录
                    cmdList.Add(_packTrackingDetailHisBLL.AddCommand(packDetail));
                });


                return DBHelp.SqlDB.SetData_Rollback(cmdList, DBHelp.Connection.SqlServer.SqlConn_PQC_Server);
            }
            catch (Exception ee)
            {

                throw ee;
            }
        }



    }
}
