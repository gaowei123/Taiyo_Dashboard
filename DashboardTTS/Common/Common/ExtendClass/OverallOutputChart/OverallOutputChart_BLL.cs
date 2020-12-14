﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Common.ExtendClass.OverallOutputChart
{
    public class OverallOutputChart_BLL
    {
        private readonly OverallOutputChart_DAL _dal = new OverallOutputChart_DAL();


        private OverallOutputChart_Model GetMouldModel(DateTime dDateFrom, DateTime dDateTo, string sShift)
        {

          

            DataTable dt = _dal.GetMouldOutput(dDateFrom,  dDateTo, sShift);
            if (dt == null || dt.Rows.Count == 0)
                return null;


            //所有datatable都只有一行. 
            DataRow dr = dt.Rows[0];


            OverallOutputChart_Model model = new OverallOutputChart_Model();
            model.Department = StaticRes.Global.Department.Moulding;
            

            if (dr["TotalQty"] != null && dr["TotalQty"].ToString() != "")
            {
                model.TotalQty = double.Parse(dr["TotalQty"].ToString());
            }
            if (dr["PassQty"] != null && dr["PassQty"].ToString() != "")
            {
                model.PassQty = double.Parse(dr["PassQty"].ToString());
            }
            if (dr["RejQty"] != null && dr["RejQty"].ToString() != "")
            {
                model.RejQty = double.Parse(dr["RejQty"].ToString());
            }



            if (dr["MouldSetup"] != null && dr["MouldSetup"].ToString() != "")
            {
                model.MouldSetup = double.Parse(dr["MouldSetup"].ToString());
            }
            if (dr["IPQCRej"] != null && dr["IPQCRej"].ToString() != "")
            {
                model.IPQCRej = double.Parse(dr["IPQCRej"].ToString());
            }

            model.RejQty += model.IPQCRej.Value;


           
            return model;
        }


        private OverallOutputChart_Model GetPaintModel(DateTime dDateFrom, DateTime dDateTo, string sShift)
        {

            DataTable dt = _dal.GetPaintOutput(dDateFrom, dDateTo, sShift);
            if (dt == null || dt.Rows.Count == 0)
                return null;

            DataTable dtQaSetup = _dal.GetPaintQASetup(dDateFrom, dDateTo, sShift);
            if (dtQaSetup == null || dtQaSetup.Rows.Count == 0)
                return null;



            //所有datatable都只有一行. 
            DataRow dr = dt.Rows[0];
            DataRow drQaSetup = dtQaSetup.Rows[0];


            OverallOutputChart_Model model = new OverallOutputChart_Model();
            model.Department = StaticRes.Global.Department.Painting;



            if (dr["TotalQty"] != null && dr["TotalQty"].ToString() != "")
            {
                model.TotalQty = double.Parse(dr["TotalQty"].ToString());
            }

            

            if (drQaSetup["PaintSetupRej"] != null && drQaSetup["PaintSetupRej"].ToString() != "")
            {
                model.PaintSetupRej = double.Parse(drQaSetup["PaintSetupRej"].ToString());
            }

            if (drQaSetup["PaintQARej"] != null && drQaSetup["PaintQARej"].ToString() != "")
            {
                model.PaintQARej = double.Parse(drQaSetup["PaintQARej"].ToString());
            }

            model.RejQty = model.PaintSetupRej.Value + model.PaintQARej.Value;

            model.PassQty = model.TotalQty - model.RejQty;



            return model;
        }


        private OverallOutputChart_Model GetLaserModel(DateTime dDateFrom, DateTime dDateTo, string sShift)
        {

            DataTable dt = _dal.GetLaserOutput(dDateFrom, dDateTo, sShift);
            if (dt == null || dt.Rows.Count == 0)
                return null;

           

            DataTable dtSetupBuyoffShortage = _dal.GetLaserSetupBuyoffShortage(dDateFrom, dDateTo, sShift);
            


          
            DataRow dr = dt.Rows[0];
            DataRow drSetupBuyoffShortage = dtSetupBuyoffShortage.Rows[0];



            OverallOutputChart_Model model = new OverallOutputChart_Model();
            model.Department = StaticRes.Global.Department.Laser;



            if (dr["TotalQty"] != null && dr["TotalQty"].ToString() != "")
            {
                model.TotalQty = double.Parse(dr["TotalQty"].ToString());
            }
            if (dr["PassQty"] != null && dr["PassQty"].ToString() != "")
            {
                model.PassQty = double.Parse(dr["PassQty"].ToString());
            }
            if (dr["RejQty"] != null && dr["RejQty"].ToString() != "")
            {
                model.RejQty = double.Parse(dr["RejQty"].ToString());
            }


            if (drSetupBuyoffShortage["LaserBuyoff"] != null && drSetupBuyoffShortage["LaserBuyoff"].ToString() != "")
            {
                model.LaserBuyoff = double.Parse(drSetupBuyoffShortage["LaserBuyoff"].ToString());
            }
            if (drSetupBuyoffShortage["LaserSetup"] != null && drSetupBuyoffShortage["LaserSetup"].ToString() != "")
            {
                model.LaserSetup = double.Parse(drSetupBuyoffShortage["LaserSetup"].ToString());
            }
            if (drSetupBuyoffShortage["LaserShortage"] != null && drSetupBuyoffShortage["LaserShortage"].ToString() != "")
            {
                model.LaserShortage = double.Parse(drSetupBuyoffShortage["LaserShortage"].ToString());
            }

            model.RejQty += (model.LaserSetup.Value + model.LaserBuyoff.Value);




            return model;
        }


        private OverallOutputChart_Model GetCheckModel(DateTime dDateFrom, DateTime dDateTo, string sShift)
        {

            DataTable dt = _dal.GetCheckingOutput(dDateFrom, dDateTo, sShift);
            if (dt == null || dt.Rows.Count == 0)
                return null;

            

            DataRow dr = dt.Rows[0];



            OverallOutputChart_Model model = new OverallOutputChart_Model();
            model.Department = StaticRes.Global.Department.PQC;



            if (dr["TotalQty"] != null && dr["TotalQty"].ToString() != "")
            {
                model.TotalQty = double.Parse(dr["TotalQty"].ToString());
            }
            if (dr["PassQty"] != null && dr["PassQty"].ToString() != "")
            {
                model.PassQty = double.Parse(dr["PassQty"].ToString());
            }
          



            if (dr["TTSMouldRej"] != null && dr["TTSMouldRej"].ToString() != "")
            {
                model.TTSMouldRej = double.Parse(dr["TTSMouldRej"].ToString());
            }
            if (dr["VendorMouldRej"] != null && dr["VendorMouldRej"].ToString() != "")
            {
                model.VendorMouldRej = double.Parse(dr["VendorMouldRej"].ToString());
            }
            if (dr["PaintRej"] != null && dr["PaintRej"].ToString() != "")
            {
                model.PaintRej = double.Parse(dr["PaintRej"].ToString());
            }
            if (dr["LaserRej"] != null && dr["LaserRej"].ToString() != "")
            {
                model.LaserRej = double.Parse(dr["LaserRej"].ToString());
            }
            if (dr["OthersRej"] != null && dr["OthersRej"].ToString() != "")
            {
                model.OthersRej = double.Parse(dr["OthersRej"].ToString());
            }

            model.RejQty = model.TTSMouldRej.Value + model.VendorMouldRej.Value + model.PaintRej.Value + model.LaserRej.Value + model.OthersRej.Value;





            return model;
        }

        private OverallOutputChart_Model GetPackModel(DateTime dDateFrom, DateTime dDateTo, string sShift)
        {

            DataTable dt = _dal.GetPackingOutput(dDateFrom, dDateTo, sShift);
            if (dt == null || dt.Rows.Count == 0)
                return null;



            DataRow dr = dt.Rows[0];



            OverallOutputChart_Model model = new OverallOutputChart_Model();
            model.Department = StaticRes.Global.Department.Packing;



            if (dr["TotalQty"] != null && dr["TotalQty"].ToString() != "")
            {
                model.TotalQty = double.Parse(dr["TotalQty"].ToString());
            }
            if (dr["PassQty"] != null && dr["PassQty"].ToString() != "")
            {
                model.PassQty = double.Parse(dr["PassQty"].ToString());
            }
            if (dr["RejQty"] != null && dr["RejQty"].ToString() != "")
            {
                model.RejQty = double.Parse(dr["RejQty"].ToString());
            }
            



            return model;
        }


        public List<OverallOutputChart_Model> GetDataList(DateTime dDateFrom, DateTime dDateTo, string sShift)
        {
            OverallOutputChart_Model Moulding = GetMouldModel(dDateFrom, dDateTo, sShift);
            OverallOutputChart_Model Painting = GetPaintModel(dDateFrom, dDateTo, sShift);
            OverallOutputChart_Model Laser = GetLaserModel(dDateFrom, dDateTo, sShift);
            OverallOutputChart_Model Checcking = GetCheckModel(dDateFrom, dDateTo, sShift);
            OverallOutputChart_Model Packing = GetPackModel(dDateFrom, dDateTo, sShift);
        


            
            Painting.PaintSetupRej = Laser.LaserShortage;
            Laser.LaserShortage = 0;


            List<OverallOutputChart_Model> modelList = new List<OverallOutputChart_Model>();
            modelList.Add(Moulding);
            modelList.Add(Painting);
            modelList.Add(Laser);
            modelList.Add(Checcking);
            modelList.Add(Packing);


            return modelList;
        }


    }
}