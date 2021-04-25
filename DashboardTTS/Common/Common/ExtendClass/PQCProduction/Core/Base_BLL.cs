using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Taiyo.SearchParam.PQCParam;

namespace Common.ExtendClass.PQCProduction.Core
{
    public class Base_BLL
    {
        private readonly Base_DAL _dal;
        public Base_BLL()
        {
            _dal = new Base_DAL();
        }


        /// <summary>
        /// 包含了 checking 和 packing 的记录
        /// </summary>
        public List<BaseVI_Model> GetViList(PQCOutputParam param)
        {
            var checkViList = _dal.GetCheckViList(param);
            var packViList = _dal.GetPackViList(param);

            List<BaseVI_Model> result = new List<BaseVI_Model>();

            if (checkViList != null)
                result.AddRange(checkViList);

            if (packViList != null)
                result.AddRange(packViList);


            return result;
        }





        public List<BaseVI_Model> GetCheckingList(PQCOutputParam param)
        {
            return _dal.GetCheckViList(param);
        }

        public BaseVI_Model GetCheckingModel(string trackingID)
        {
            return _dal.GetCheckViList(new PQCOutputParam()
            {
                TrackingID = trackingID
            }).FirstOrDefault();
        }

        public List<BaseVIDetail_Model> GetCheckingDetailList(PQCOutputParam param)
        {
            return _dal.GetCheckVIDetailList(param);
        }
        
        public List<BaseDefectSummary_Model> GetDefectList(PQCOutputParam param)
        {
            return _dal.GetDefectList(param);
        }





        public List<BaseVI_Model> GetPackingList(PQCOutputParam param)
        {
            return _dal.GetPackViList(param);
        }
        public BaseVI_Model GetPackingModel(string trackingID)
        {
            return _dal.GetPackViList(new PQCOutputParam()
            {
                TrackingID = trackingID
            }).FirstOrDefault();
        }


        public List<BasePackDetail_Model> GetPackDetailList(PQCOutputParam param)
        {
            return _dal.GetPackDetailList(param);
        }


        public List<BaseLotInfo_Model> GetLotInfoList(PQCOutputParam param)
        {
            return _dal.GetLotInfoList(param);
        }
        public BaseLotInfo_Model GetLotInfoModel(string jobNo)
        {
            return _dal.GetLotInfoList(new PQCOutputParam()
            {
                JobNo = jobNo
            }).FirstOrDefault();
        }



        public List<BaseBin_Model> GetBinList(PQCOutputParam param)
        {
            return _dal.GetBinList(param);
        }

        public List<BaseBin_Model> GetBinHisScrapList(PQCOutputParam param)
        {
            return _dal.GetBinHisScrapList(param);
        }





        public List<Bom_Model> GetBomList()
        {
            DataTable dtBom = _dal.GetBom();
            DataTable dtBomDetail = _dal.GetBomDetail();

            if (dtBom == null || dtBomDetail == null)
                throw new Exception(" PQC BOM Data missing !");

            List<Bom_Model> bomList = new List<Bom_Model>();
            foreach (DataRow dr in dtBom.Rows)
            {
                Bom_Model bom = new Bom_Model();
                bom.PartNo = dr["partNumber"].ToString();
                bom.Customer = dr["customer"].ToString();
                bom.Model = dr["model"].ToString();
                bom.JigNo = dr["jigNo"].ToString();
                bom.Color = dr["color"].ToString();
                bom.Processes = dr["processes"].ToString();
                bom.withLaser = bool.Parse(dr["withLaser"].ToString());
                bom.LastCheckProcess = dr["lastCheckProcess"].ToString();
                bom.Supplier = dr["supplier"].ToString();
                bom.ShipTo = dr["shipTo"].ToString();
                bom.Coating = dr["coating"].ToString();
                bom.Description = dr["description"].ToString();
                bom.Num = dr["num"].ToString();
                bom.UnitCost = decimal.Parse(dr["unitCost"].ToString());



                DataRow[] materialArrs = dtBomDetail.Select($" partNumber = '{bom.PartNo}' ");
                if (materialArrs != null && materialArrs.Count() != 0)
                {
                    foreach (DataRow drMaterial in materialArrs)
                    {
                        Bom_Model.MaterialPart material = new Bom_Model.MaterialPart();
                        material.MaterialName = drMaterial["materialName"].ToString();
                        material.MaterialPartNo = drMaterial["materialPartNo"].ToString();
                        material.PartCount = int.Parse(drMaterial["partCount"].ToString());
                        material.OuterBoxQty = int.Parse(drMaterial["outerBoxQty"].ToString());
                        material.PackingTrays = drMaterial["packingTrays"].ToString();
                        material.Module = drMaterial["module"].ToString();

                        bom.MaterialPartList.Add(material);
                    }
                }

                bomList.Add(bom);                
            }

            return bomList;
        }
        public Bom_Model GetBomModel(string partNo)
        {
            return GetBomList()
                .Where(p => p.PartNo == partNo)
                .FirstOrDefault();
        }

    }
}
