using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.PQC
{
    public partial class PQCRealTime : System.Web.UI.Page
    {
        const string packImgPath = "~/Resources/Images/Packing.jpg";
        const string checkImgPath = "~/Resources/Images/PQCInspector.png";

       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Type"] == null)
                return;


            string type = Request.QueryString["Type"].ToString();
            this.lbHearText.Text = string.Format("PQC {0} Real Time", type);



            ViewModel.PQCRealTime_ViewModel model = GetRealTimeModel(type);



            SetUI(model);


            //set visiable
            SetVisiable(type);
            

        }


        public ViewModel.PQCRealTime_ViewModel GetRealTimeModel(string type)
        {
            ViewModel.PQCRealTime_ViewModel model = new ViewModel.PQCRealTime_ViewModel();
            model.currentInfoList = new List<ViewModel.PQCRealTime_ViewModel.currentInfo>();

            //1-8定死为online
            //11,13-17定死为wip
            //12,21-25定死为packing
            int[] arrStations = new int[] { };
            if (type == "Online")
                arrStations = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            else if (type == "WIP")
                arrStations = new int[] { 11, 13, 14, 15, 16, 17 };
            else if (type == "Packing")
                arrStations = new int[] { 12, 21, 22, 23, 24, 25 };


            //获取源数据
            List<ViewModel.PQCRealTime_ViewModel.baseInfo> baseInfoModelList = GetBaseInfoList(type);
            foreach (int i in arrStations)
            {
                List<ViewModel.PQCRealTime_ViewModel.baseInfo> baseInfo = new List<ViewModel.PQCRealTime_ViewModel.baseInfo>();
                if (baseInfoModelList != null && baseInfoModelList.Count != 0)
                {
                    baseInfo = (from a in baseInfoModelList
                                where a.station == i.ToString()
                                orderby a.dateTime descending
                                select a).ToList();
                }

           

                ViewModel.PQCRealTime_ViewModel.currentInfo curInfoModel = new ViewModel.PQCRealTime_ViewModel.currentInfo();
                
                //没有生产记录, 视为shutdown
                if (baseInfo.Count() == 0)
                {
                    curInfoModel.id = i;
                    curInfoModel.station = GetStationName(i);
                    curInfoModel.status = "Shutdown";

                    curInfoModel.lotNo = "";
                    curInfoModel.jobNo = "";
                    curInfoModel.partNo = "";
                    curInfoModel.mrpTotal = 0;
                    curInfoModel.ok = 0;
                    curInfoModel.ng = 0;
                    curInfoModel.rejRate = "0.00%";
                    curInfoModel.rejPPM = 0;
                    curInfoModel.op = "";
                    curInfoModel.imgPath = type == "Packing" ? packImgPath : checkImgPath;
                }
                else
                {
                    //有生产记录,取最新一条记录
                    ViewModel.PQCRealTime_ViewModel.baseInfo lastestBaseInfoModel = baseInfo.FirstOrDefault();

                    curInfoModel.id = i;
                    curInfoModel.station = GetStationName(i);

                    //最后一条记录complete为true,当前不在check视为no Schedule
                    string nextViFlag = lastestBaseInfoModel.nextViFlag;
                    if (nextViFlag == "True")
                        curInfoModel.status = "No Schedule";
                    else
                        curInfoModel.status = type == "Packing" ? "Packing" : "Checking";


                    curInfoModel.lotNo = lastestBaseInfoModel.lotNo;
                    curInfoModel.jobNo = lastestBaseInfoModel.jobNo;
                    curInfoModel.partNo = lastestBaseInfoModel.partNo;
                    curInfoModel.mrpTotal = lastestBaseInfoModel.mrpTotal;
                    curInfoModel.ok = lastestBaseInfoModel.ok;
                    curInfoModel.ng = lastestBaseInfoModel.ng;
                    curInfoModel.rejRate = Math.Round(curInfoModel.ng / curInfoModel.mrpTotal * 100, 2).ToString("0.00") + "%";
                    curInfoModel.rejPPM = Math.Round(curInfoModel.ng / curInfoModel.mrpTotal * 100 * 10000, 0);
                    curInfoModel.op = lastestBaseInfoModel.op;
                    curInfoModel.imgPath = type == "Packing" ? packImgPath : checkImgPath;
                }


                model.currentInfoList.Add(curInfoModel);
            }

            //base Info Model List 包含当天该type的所有记录
            if (baseInfoModelList == null || baseInfoModelList.Count == 0 )
            {
                model.totalOutput = 0;
                model.totalRejQty = 0;
                model.totalRejRate = "0.00%";
            }
            else
            {
                model.totalOutput = baseInfoModelList.Sum(p => p.ok + p.ng);
                model.totalRejQty = baseInfoModelList.Sum(p => p.ng);
                model.totalRejRate = model.totalOutput == 0 ? "0.00%" : Math.Round(model.totalRejQty / model.totalOutput * 100, 2).ToString("0.00") + "%";
            }

        

            return model;
        }


        





        UserControl.WebUserControlPQCStatus GetControl(int ID)
        {
            UserControl.WebUserControlPQCStatus control = new UserControl.WebUserControlPQCStatus();

            switch (ID)
            {
                case 1:
                    control = this.ucPQC1;
                    break;
                case 2:
                    control = this.ucPQC2;
                    break;
                case 3:
                    control = this.ucPQC3;
                    break;
                case 4:
                    control = this.ucPQC4;
                    break;
                case 5:
                    control = this.ucPQC5;
                    break;
                case 6:
                    control = this.ucPQC6;
                    break;
                case 7:
                    control = this.ucPQC7;
                    break;
                case 8:
                    control = this.ucPQC8;
                    break;
                case 11:
                    control = this.ucPQC11;
                    break;
                case 12:
                    control = this.ucPQC12;
                    break;
                case 13:
                    control = this.ucPQC13;
                    break;
                case 14:
                    control = this.ucPQC14;
                    break;
                case 15:
                    control = this.ucPQC15;
                    break;
                case 16:
                    control = this.ucPQC16;
                    break;
                case 17:
                    control = this.ucPQC17;
                    break;

                case 21:
                    control = this.ucPQC21;
                    break;
                case 22:
                    control = this.ucPQC22;
                    break;
                case 23:
                    control = this.ucPQC23;
                    break;
                case 24:
                    control = this.ucPQC24;
                    break;
                case 25:
                    control = this.ucPQC25;
                    break;
            
                default:
                    break;
            }

            return control;

        }
        
        

        public void SetUI(ViewModel.PQCRealTime_ViewModel model)
        {
            this.lbOutput.Text = model.totalOutput.ToString();
            this.lbRejQty.Text = model.totalRejQty.ToString();
            this.lbRejRate.Text = model.totalRejRate.ToString();

            foreach (var curInfoModel in model.currentInfoList)
            {
                //根据id获取控件.
                var control = GetControl(curInfoModel.id);

                //赋值控件
                control.Station = curInfoModel.station;
                control.Status = curInfoModel.status;
                control.LotNo = curInfoModel.lotNo;
                control.JobID = curInfoModel.jobNo;
                control.PartNo = curInfoModel.partNo;
                control.TotalQtyCurrent = curInfoModel.mrpTotal;
                control.OkQtyCurrent = curInfoModel.ok;
                control.NgQtyCurrent = curInfoModel.ng;
                control.Rejrate = curInfoModel.rejRate;
                control.RejPPM = curInfoModel.rejPPM;
                control.Operator = curInfoModel.op;
                control.ImageUrl = curInfoModel.imgPath;
            }
        }

        //获取源数据 list
        public List<ViewModel.PQCRealTime_ViewModel.baseInfo> GetBaseInfoList(string type)
        {
            Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();
            DataTable dt = bll.GetRealTimeData(type);
            if (dt == null || dt.Rows.Count == 0)
                return null;


            List<ViewModel.PQCRealTime_ViewModel.baseInfo> baseInfoList = new List<ViewModel.PQCRealTime_ViewModel.baseInfo>();

            foreach (DataRow dr in dt.Rows)
            {
                ViewModel.PQCRealTime_ViewModel.baseInfo model = new ViewModel.PQCRealTime_ViewModel.baseInfo();
                model.station = dr["machineID"].ToString();
                model.nextViFlag = dr["nextViFlag"].ToString();
                model.lotNo = dr["lotNo"].ToString();
                model.jobNo = dr["JobId"].ToString();
                model.partNo = dr["partNumber"].ToString();
                model.mrpTotal = double.Parse(dr["mrpTotal"].ToString());
                model.ok = double.Parse(dr["acceptQty"].ToString());
                model.ng = double.Parse(dr["rejectQty"].ToString());
                model.op = dr["userID"].ToString();
                model.dateTime = DateTime.Parse(dr["dateTime"].ToString());


                baseInfoList.Add(model);
            }

            return baseInfoList;
        }


        void SetVisiable(string type)
        {
            if (type == "Online")
            {
                SetOnlineDivVisiable(true);
                SetWipDivVisiable(false);
                SetPackDivVisiable(false);
            }
            else if (type == "WIP")
            {
                SetOnlineDivVisiable(false);
                SetWipDivVisiable(true);
                SetPackDivVisiable(false);
            }
            else if (type == "Packing")
            {
                SetOnlineDivVisiable(false);
                SetWipDivVisiable(false);
                SetPackDivVisiable(true);
            }
        }


        public string GetStationName(int no)
        {
            string stationName = "";

            switch (no)
            {
                case 1:
                    stationName = "Online1";
                    break;
                case 2:
                    stationName = "Online2";
                    break;
                case 3:
                    stationName = "Online3";
                    break;
                case 4:
                    stationName = "Online4";
                    break;
                case 5:
                    stationName = "Online5";
                    break;
                case 6:
                    stationName = "Online6";
                    break;
                case 7:
                    stationName = "Online7";
                    break;
                case 8:
                    stationName = "Online8";
                    break;
              
                case 11:
                    stationName = "WIP5";
                    break;
                case 12:
                    stationName = "Packing6";
                    break;
                case 13:
                    stationName = "WIP6";
                    break;
                case 14:
                    stationName = "WIP3";
                    break;
                case 15:
                    stationName = "WIP4";
                    break;
                case 16:
                    stationName = "WIP1";
                    break;
                case 17:
                    stationName = "WIP2";
                    break;

                case 21:
                    stationName = "Packing4";
                    break;
                case 22:
                    stationName = "Packing3";
                    break;
                case 23:
                    stationName = "Packing2";
                    break;
                case 24:
                    stationName = "Packing5";
                    break;
                case 25:
                    stationName = "Packing1";
                    break;

                default:
                    break;
            }

            return stationName;
        }


        void SetOnlineDivVisiable(bool flag)
        {
            div1.Visible = flag;
            div2.Visible = flag;
            div3.Visible = flag;
            div4.Visible = flag;
            div5.Visible = flag;
            div6.Visible = flag; 
            div7.Visible = flag;
            div8.Visible = flag;
        }

        void SetWipDivVisiable(bool flag)
        {
            div11.Visible = flag;
            div13.Visible = flag;
            div14.Visible = flag;
            div15.Visible = flag;
            div16.Visible = flag;
            div17.Visible = flag;
        }

        void SetPackDivVisiable(bool flag)
        {
            div12.Visible = flag;
            div21.Visible = flag;
            div22.Visible = flag;
            div23.Visible = flag;
            div24.Visible = flag;
            div25.Visible = flag;
        }
        
    }
}