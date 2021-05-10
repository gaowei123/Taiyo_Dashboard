using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ExtendClass.PQCProduction.ScrapReport
{
    public class Scrap_BLL
    {
        private readonly Core.Base_BLL _baseBLL = new Core.Base_BLL();

        // bin history model 和 bin model 表基本一致, 偷懒直接拿来用了.
        public string GetScrapList(Taiyo.SearchParam.PQCParam.PQCOutputParam param)
        {

            var scrapSourceList = _baseBLL.GetBinHisScrapList(param);
            if (scrapSourceList == null)
                return null;


            var bomList = _baseBLL.GetBomList();



 
            // 用来添加 scrap 中缺少的 material 的记录.
            List<Core.BaseBin_Model> tempList = new List<Core.BaseBin_Model>();
            


            // 遍历每条记录, 根据每条记录的 part no, tracking id 和 bom 中的 material part, 判断是否都有, 没有则添加.
            foreach (var item in scrapSourceList)
            {
                
                // 遍历每个 material part 重新从 scrapSourceList 中查一下是否有记录 
                var bom = bomList.Where(p => p.PartNo == item.PartNo).FirstOrDefault();
                if (bom != null  && bom.MaterialPartList.Count != 0)
                {
                    bom.MaterialPartList.ForEach(material =>
                    {
                        var temp = scrapSourceList.Where(p => p.PartNo == item.PartNo && p.TrackingID == item.TrackingID && p.MaterialPartNo == material.MaterialPartNo);
                        if (temp == null || temp.Count() == 0)
                        {
                            // 如果没记录, 并且没有添加到 tempList 中, 则新增一条.

                            var addItem = tempList.Where(p => p.TrackingID == item.TrackingID && p.MaterialPartNo == material.MaterialPartNo);
                            if (addItem == null || addItem.Count() == 0)
                            {
                                tempList.Add(new Core.BaseBin_Model()
                                {
                                    Day = item.Day,
                                    Shift = item.Shift,
                                    TrackingID = item.TrackingID,
                                    PartNo = item.PartNo,
                                    JobNo = item.JobNo,
                                    MaterialName = material.MaterialName,
                                    MaterialPartNo = material.MaterialPartNo,
                                    MaterialQty = 0,
                                    Status = item.Status,
                                    Processes = item.Processes,
                                    ShipTo = item.ShipTo,
                                    PackBundle = item.PackBundle,
                                    UserID = item.UserID
                                });
                            }
                        }
                    });
                }
            }
            

            // 合并 tempList 到 scrapSourceList 中
            scrapSourceList.AddRange(tempList);



            // 再按照 material name 分组, 以一组 material name 中最小的数量为该 material name 的 scrap 数量.
            var result = from a in scrapSourceList
                         group a by new
                         {
                             a.Day,
                             a.Shift,
                             a.PartNo,
                             a.JobNo,
                             a.TrackingID,
                             a.MaterialName,
                             a.UserID
                         }
                         into b
                         select new
                         {
                             Day = b.Key.Day,
                             Shift = b.Key.Shift,
                             PartNo = b.Key.PartNo,
                             JobNo = b.Key.JobNo,
                             TrackingID = b.Key.TrackingID,
                             MaterialName = b.Key.MaterialName,
                             ScrapQty = b.Min(p => p.MaterialQty),
                             UserID = b.Key.UserID
                         } into c
                         where c.ScrapQty != 0
                         select new
                         {
                             Day = c.Day,
                             Shift = c.Shift,
                             PartNo = c.PartNo,
                             JobNo = c.JobNo,
                             TrackingID = c.TrackingID,
                             MaterialName = c.MaterialName,
                             ScrapQty = c.ScrapQty,
                             UserID = c.UserID
                         };


         
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

    }
}
