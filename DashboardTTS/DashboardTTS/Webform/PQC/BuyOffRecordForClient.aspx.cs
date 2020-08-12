using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DashboardTTS.Webform.PQC
{
    public partial class BuyOffRecordForClient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    DBHelp.Reports.LogFile.Log("BuyOffRecordForClient", "URL: " + Request.Url.ToString());

                    string sJobNumber = Request.QueryString["JobNumber"] == null ? "" : Request.QueryString["JobNumber"].ToString();
                    string sUserName = Request.QueryString["UserName"] == null ? "" : Request.QueryString["UserName"].ToString(); //2019 07 25 by LiJia, got UserName from Client
                    string sCheckProcess = Request.QueryString["CheckProcess"] == null ? "" : Request.QueryString["CheckProcess"].ToString();


                    DBHelp.Reports.LogFile.Log("BuyOffRecordForClient", string.Format("Received parameters: JobNumber = {0}, Username = {1}, Checkprocess = {2}", sJobNumber, sUserName, sCheckProcess));


                    if (sJobNumber != "")
                    {
                        this.txtJobNumber.Text = sJobNumber;
                        this.txtUserName.Text = sUserName; //2019 07 25 by LiJia, got UserName from Client
                        this.lbCheckProcess.Text = sCheckProcess;

                        this.btn_Confirm.Enabled = true;

                        //check double submit
                        Common.Class.BLL.PaintingTempInfo bll = new Common.Class.BLL.PaintingTempInfo();
                        bool exist = bll.Exist(txtJobNumber.Text.Trim(), sCheckProcess);
                        if (exist)
                        {
                            string strUrl = "../PQC/BuyOffRecordForClient.aspx?Result=TRUE";
                            Response.Redirect(strUrl, false);

                            return;
                        }
                    }
                    else
                    {
                        this.btn_Confirm.Enabled = false;
                    }

                    setMachineDDL();
                    setPICDDL();
                    SetTimeDDL();

                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("BuyOffRecordForClient", "Page_Load Exception " + ee.ToString());
            }
        }



        protected void btn_Confirm_Click(object sender, EventArgs e)
        {
            try
            {

                #region check text value

                if (!Common.CommFunctions.isNumberic(this.txtSetupRejQty.Text) && this.txtSetupRejQty.Text != "")
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please key in number for Setup Rej Qty");
                    this.txtSetupRejQty.Text = "";
                    this.txtSetupRejQty.Focus();
                    return;
                }

                if (!Common.CommFunctions.isNumberic(this.txtQATestQty.Text) && this.txtQATestQty.Text != "")
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please key in number for QA Reliability Test Qty");
                    this.txtQATestQty.Text = "";
                    this.txtQATestQty.Focus();
                    return;
                }


                if (!Common.CommFunctions.isNumberic(this.txtUnderCoatThickness.Text) && this.txtUnderCoatThickness.Text != "")
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please key in number for Thickness");
                    this.txtUnderCoatThickness.Text = "";
                    this.txtUnderCoatThickness.Focus();
                    return;
                }
                if (!Common.CommFunctions.isNumberic(this.txtMiddleCoatThickness.Text) && this.txtMiddleCoatThickness.Text != "")
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please key in number for Thickness");
                    this.txtMiddleCoatThickness.Text = "";
                    this.txtMiddleCoatThickness.Focus();
                    return;
                }

                if (!Common.CommFunctions.isNumberic(this.txtTopCoatThickness.Text) && this.txtTopCoatThickness.Text.Trim() != "")
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please key in number for Thickness");
                    this.txtTopCoatThickness.Text = "";
                    this.txtTopCoatThickness.Focus();
                    return;
                }

                if (this.txtMFGDate.Text.Trim() == "")
                {
                    Common.CommFunctions.ShowMessage(this.Page, "MFG Date can not be empty!");
                    return;
                }

                if (this.txtTempFront.Text.Trim() != "" && !Common.CommFunctions.isNumberic(this.txtTempFront.Text))
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please key in number for temperature front!");
                    return;
                }

                if (this.txtTempRear.Text.Trim() != "" && !Common.CommFunctions.isNumberic(this.txtTempRear.Text))
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please key in number for temperature rear!");
                    return;
                }


                if (this.txtHumidityFront.Text.Trim() != "" && !Common.CommFunctions.isNumberic(this.txtHumidityFront.Text))
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please key in number for humidity front!");
                    return;
                }


                if (this.txtHumidityRear.Text.Trim() != "" && !Common.CommFunctions.isNumberic(this.txtHumidityRear.Text))
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please key in number for humidity rear!");
                    return;
                }




                #endregion



                List<Common.Class.Model.PaintingTempInfo_Model> listModel = new List<Common.Class.Model.PaintingTempInfo_Model>();


                string laserOperator = "";
                string lotNo = "";
                string laserMachine = "";
                string partNumber = "";
                DateTime? laserDate = null;
                DateTime? MFGDate = null;
                double lotQty = 0;







                //get info from laser buyoff list
                Common.Class.BLL.LMMSBUYOFFLIST_BLL buyoffBLL = new Common.Class.BLL.LMMSBUYOFFLIST_BLL();
                DataTable dtBuyoff = buyoffBLL.GetBuyofflist(txtJobNumber.Text, "", "", "", "", "", null, null);
                if (dtBuyoff != null && dtBuyoff.Rows.Count != 0)
                {
                    laserOperator = dtBuyoff.Rows[0]["BUYOFF_BY"].ToString();
                    laserMachine = dtBuyoff.Rows[0]["MACHINE_ID"].ToString();
                    laserDate = DateTime.Parse(dtBuyoff.Rows[0]["DATE_TIME"].ToString()).Date;
                }




                //get info from painting delivery 
                Common.Class.BLL.PaintingDeliveryHis_BLL paintingDeliveryBLL = new Common.Class.BLL.PaintingDeliveryHis_BLL();
                DataTable dtPaint = paintingDeliveryBLL.GetList(DateTime.Parse("2017-1-1"), DateTime.Now.AddDays(1), txtJobNumber.Text, "", "", "");
                if (dtPaint != null && dtPaint.Rows.Count != 0)
                {
                    lotNo = dtPaint.Rows[0]["LotNo"].ToString();
                    lotQty = double.Parse(dtPaint.Rows[0]["InQtySET"].ToString());

                    //MFGDate = DateTime.Parse(dtPaint.Rows[0]["dateTime"].ToString());
                    MFGDate = DateTime.Parse(this.txtMFGDate.Text);

                    
                    partNumber = dtPaint.Rows[0]["partNumber"].ToString();

                    if (partNumber == "")
                    {
                        Common.CommFunctions.ShowMessage(this.Page, "Error, Can not get part no from painting delivery history!");
                        return;
                    }
                }
                else
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Error, Can not get info from painting delivery history. Please check the job no! ");
                    return;
                }



                //get bom detail info
                Common.Class.BLL.PQCBomDetail_BLL bomDetailBLL = new Common.Class.BLL.PQCBomDetail_BLL();
                DataTable dtBomDetail = bomDetailBLL.GetList(partNumber);
                if (dtBomDetail == null || dtBomDetail.Rows.Count == 0)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Error, Can not get material info from bom detail!");
                    return;
                }

                foreach (DataRow dr in dtBomDetail.Rows)
                {
                    string sUnderCoatRunTime = "";
                    if (this.ddlUnderCoatRunTimeHH.SelectedValue != "" && this.ddlUnderCoatRunTimeMM.SelectedValue != "")
                        sUnderCoatRunTime = this.ddlUnderCoatRunTimeHH.SelectedValue + ":" + this.ddlUnderCoatRunTimeMM.SelectedValue;

                    string sUnderCoatOvenTime = "";
                    if (this.ddlUnderCoatOvenTimeHH.SelectedValue != "" && this.ddlUnderCoatOvenTimeMM.SelectedValue != "")
                        sUnderCoatOvenTime = this.ddlUnderCoatOvenTimeHH.SelectedValue + ":" + this.ddlUnderCoatOvenTimeMM.SelectedValue;

                    string sMiddleCoatRunTime = "";
                    if (this.ddlMiddleCoatRunTimeHH.SelectedValue != "" && this.ddlMiddleCoatRunTimeMM.SelectedValue != "")
                        sMiddleCoatRunTime = this.ddlMiddleCoatRunTimeHH.SelectedValue + ":" + this.ddlMiddleCoatRunTimeMM.SelectedValue;

                    string sMiddleCoatOvenTime = "";
                    if (this.ddlMiddleCoatOvenTimeHH.SelectedValue != "" && this.ddlMiddleCoatOvenTimeMM.SelectedValue != "")
                    {
                        sMiddleCoatOvenTime = this.ddlMiddleCoatOvenTimeHH.SelectedValue + ":" + this.ddlMiddleCoatOvenTimeMM.SelectedValue;
                    }

                    string sTopCoatRunTime = "";
                    if (this.ddlTopCoatRunTimeHH.SelectedValue != "" && this.ddlTopCoatRunTimeMM.SelectedValue != "")
                        sTopCoatRunTime = this.ddlTopCoatRunTimeHH.SelectedValue + ":" + this.ddlTopCoatRunTimeMM.SelectedValue;

                    string sTopCoatOvenTime = "";
                    if (this.ddlTopCoatOvenTimeHH.SelectedValue != "" && this.ddlTopCoatOvenTimeMM.SelectedValue != "")
                        sTopCoatOvenTime = this.ddlTopCoatOvenTimeHH.SelectedValue + ":" + this.ddlTopCoatOvenTimeMM.SelectedValue;




                    Common.Class.Model.PaintingTempInfo_Model model = new Common.Class.Model.PaintingTempInfo_Model();

                    model.jobNumber = txtJobNumber.Text;
                    model.recordBy = txtUserName.Text; //2019 07 25 by LiJia, got UserName from Client

                    model.partNumber = partNumber;
                    model.materialName = dr["materialPartNo"].ToString();

                    model.lotNo = lotNo;
                    model.lotQty = decimal.Parse(lotQty.ToString());
                    model.MFGDate = MFGDate;
                    model.laserOperator = laserOperator;
                    model.laserMachine = laserMachine;
                    model.laserDate = laserDate;



                    if (this.txtTempFront.Text.Trim() != "")
                        model.temperateFront = double.Parse(this.txtTempFront.Text.Trim());

                    if (this.txtTempRear.Text.Trim() != "")
                        model.temperateRear = double.Parse(this.txtTempRear.Text.Trim());

                    if (this.txtHumidityFront.Text.Trim() != "")
                        model.humidityFront = double.Parse(this.txtHumidityFront.Text.Trim());

                    if (this.txtHumidityRear.Text.Trim() != "")
                        model.humidityRear = double.Parse(this.txtHumidityRear.Text.Trim());





                    model.setupRejQty = this.txtSetupRejQty.Text.Trim() == "" ? 0 : decimal.Parse(this.txtSetupRejQty.Text.Trim());
                    model.qaTestQty = this.txtQATestQty.Text.Trim() == "" ? 0 : decimal.Parse(this.txtQATestQty.Text.Trim());


                    #region under coat. middle coat, top coat

                    DateTime dUnderCoatDate = DateTime.Today;
                    if (this.txtUnderCoatDate.Text.Trim() == string.Empty ||
                        !DateTime.TryParseExact(this.txtUnderCoatDate.Text.Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dUnderCoatDate))
                        model.paintingDate_1st = null;
                    else
                        model.paintingDate_1st = dUnderCoatDate;

                    model.paintingRunningTime_1st = sUnderCoatRunTime;
                    model.pMachine_1st = this.ddlUnderCoatMachineNo.SelectedValue;
                    //model.coat_1st = this.txtUnderCoat.Text;
                    model.paintLot_1st = this.txtUnderCoatPaintLot.Text;
                    model.thinnersLot_1st = this.txtUnderCoatThinnersLot.Text;
                    if (this.txtUnderCoatThickness.Text == "")
                        model.thickness_1st = null;
                    else
                        model.thickness_1st = decimal.Parse(this.txtUnderCoatThickness.Text);
                    model.paintingPIC_1st = this.ddlUnderCoatPIC.SelectedValue;
                    model.paintingOvenTime_1st = sUnderCoatOvenTime;


                    DateTime dMiddleCoatDate = DateTime.Today;
                    if (this.txtMiddleCoatDate.Text.Trim() == string.Empty ||
                        !DateTime.TryParseExact(this.txtMiddleCoatDate.Text.Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dMiddleCoatDate))
                        model.paintingDate_2nd = null;
                    else
                        model.paintingDate_2nd = dMiddleCoatDate;
                    model.paintingRunningTime_2nd = sMiddleCoatRunTime;
                    model.pMachine_2nd = this.ddlMiddleCoatMachineNo.SelectedValue;
                    //model.coat_2nd = this.txtMiddleCoat.Text;
                    model.paintLot_2nd = this.txtMiddleCoatPaintLot.Text;
                    model.thinnersLot_2nd = this.txtMiddleCoatThinnersLot.Text;
                    if (this.txtMiddleCoatThickness.Text == "")
                        model.thickness_2nd = null;
                    else
                        model.thickness_2nd = decimal.Parse(this.txtMiddleCoatThickness.Text);
                    model.paintingPIC_2nd = this.ddlMiddleCoatPIC.SelectedValue;
                    model.paintingOvenTime_2nd = sMiddleCoatOvenTime;


                    DateTime dTopCoatDate = DateTime.Today;
                    if (this.txtTopCoatDate.Text.Trim() == string.Empty ||
                        !DateTime.TryParseExact(this.txtTopCoatDate.Text.Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dTopCoatDate))
                        model.paintingDate_3rd = null;
                    else
                        model.paintingDate_3rd = dTopCoatDate;

                    model.paintingRunningTime_3rd = sTopCoatRunTime;
                    model.pMachine_3rd = this.ddlTopCoatMachineNo.SelectedValue;
                    //model.coat_3rd = this.txtTopCoat.Text;
                    model.paintLot_3rd = this.txtTopCoatPaintLot.Text;
                    model.thinnersLot_3rd = this.txtTopCoatThinnersLot.Text;
                    if (this.txtTopCoatThickness.Text == "")
                        model.thickness_3rd = null;
                    else
                        model.thickness_3rd = decimal.Parse(this.txtTopCoatThickness.Text);
                    model.paintingPIC_3rd = this.ddlTopCoatPIC.SelectedValue;
                    model.paintingOvenTime_3rd = sTopCoatOvenTime;




                    model.createdTime = DateTime.Now;
                    model.updatedTime = DateTime.Now;

                    model.checkProcess = lbCheckProcess.Text;

                    #endregion


                    listModel.Add(model);
                }



                //从mrp接口获取的datetime不是mfgdate, 只能通过pqc op在buyoff reocord中手动选择mfg date.
                MFGDate = DateTime.Parse(this.txtMFGDate.Text);

                //更新回各个表.
                UpdateMFGDate(txtJobNumber.Text, MFGDate.Value);



                //setup的数量为 painting rej的数量.
                int paintRej = int.Parse(this.txtSetupRejQty.Text.Trim());
                UpdatePaintRejQty(txtJobNumber.Text, paintRej, lbCheckProcess.Text);




                //2019 07 25 by Wei LiJia , update database directly
                string strUrl = "";
                Common.Class.BLL.PaintingTempInfo bll = new Common.Class.BLL.PaintingTempInfo();
                if (bll.AddRollBack(listModel, txtUserName.Text))
                {
                    strUrl = "../PQC/BuyOffRecordForClient.aspx?Result=TRUE";
                }
                else
                {
                    strUrl = "../PQC/BuyOffRecordForClient.aspx?Result=FALSE";
                }
                Response.Redirect(strUrl, false);

            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("BuyOffRecordForClient", "btn_Confirm_Click Exception " + ee.ToString());
            }
        }



        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            //2019 07 30 use url for GUI detection
            string strUrl = "../PQC/BuyOffRecordForClient.aspx?Result=TRUE";
            DBHelp.Reports.LogFile.Log("BuyOffRecordForClient", "btn_Cancel_Click strUrl " + strUrl);

            Response.Redirect(strUrl, false);
        }

        void SetTimeDDL()
        {
            this.ddlUnderCoatRunTimeHH.Items.Clear();
            this.ddlUnderCoatRunTimeMM.Items.Clear();
            this.ddlMiddleCoatRunTimeHH.Items.Clear();
            this.ddlMiddleCoatRunTimeMM.Items.Clear();
            this.ddlTopCoatRunTimeHH.Items.Clear();
            this.ddlTopCoatRunTimeMM.Items.Clear();

            this.ddlUnderCoatOvenTimeHH.Items.Clear();
            this.ddlUnderCoatOvenTimeMM.Items.Clear();
            this.ddlMiddleCoatOvenTimeHH.Items.Clear();
            this.ddlMiddleCoatOvenTimeMM.Items.Clear();
            this.ddlTopCoatOvenTimeHH.Items.Clear();
            this.ddlTopCoatOvenTimeMM.Items.Clear();


            this.ddlUnderCoatRunTimeHH.Items.Add(new ListItem("", ""));
            this.ddlUnderCoatRunTimeMM.Items.Add(new ListItem("", ""));
            this.ddlMiddleCoatRunTimeHH.Items.Add(new ListItem("", ""));
            this.ddlMiddleCoatRunTimeMM.Items.Add(new ListItem("", ""));
            this.ddlTopCoatRunTimeHH.Items.Add(new ListItem("", ""));
            this.ddlTopCoatRunTimeMM.Items.Add(new ListItem("", ""));

            this.ddlUnderCoatOvenTimeHH.Items.Add(new ListItem("", ""));
            this.ddlUnderCoatOvenTimeMM.Items.Add(new ListItem("", ""));
            this.ddlMiddleCoatOvenTimeHH.Items.Add(new ListItem("", ""));
            this.ddlMiddleCoatOvenTimeMM.Items.Add(new ListItem("", ""));
            this.ddlTopCoatOvenTimeHH.Items.Add(new ListItem("", ""));
            this.ddlTopCoatOvenTimeMM.Items.Add(new ListItem("", ""));




            for (int i = 0; i < 60; i++)
            {
                string sNo = i.ToString().Length == 1 ? "0" + i.ToString() : i.ToString();

                if (i < 24)
                {
                    this.ddlUnderCoatRunTimeHH.Items.Add(new ListItem(sNo, sNo));
                    this.ddlMiddleCoatRunTimeHH.Items.Add(new ListItem(sNo, sNo));
                    this.ddlTopCoatRunTimeHH.Items.Add(new ListItem(sNo, sNo));

                    this.ddlUnderCoatOvenTimeHH.Items.Add(new ListItem(sNo, sNo));
                    this.ddlMiddleCoatOvenTimeHH.Items.Add(new ListItem(sNo, sNo));
                    this.ddlTopCoatOvenTimeHH.Items.Add(new ListItem(sNo, sNo));
                }

                this.ddlUnderCoatRunTimeMM.Items.Add(new ListItem(sNo, sNo));
                this.ddlMiddleCoatRunTimeMM.Items.Add(new ListItem(sNo, sNo));
                this.ddlTopCoatRunTimeMM.Items.Add(new ListItem(sNo, sNo));

                this.ddlUnderCoatOvenTimeMM.Items.Add(new ListItem(sNo, sNo));
                this.ddlMiddleCoatOvenTimeMM.Items.Add(new ListItem(sNo, sNo));
                this.ddlTopCoatOvenTimeMM.Items.Add(new ListItem(sNo, sNo));
            }
        }

        void setMachineDDL()
        {
            this.ddlUnderCoatMachineNo.Items.Clear();
            this.ddlMiddleCoatMachineNo.Items.Clear();
            this.ddlTopCoatMachineNo.Items.Clear();


            ListItem liUnderMachine = new ListItem();
            ListItem liMiddleMachine = new ListItem();
            ListItem liTopMachine = new ListItem();

            liUnderMachine.Text = "";
            liUnderMachine.Value = "";
            this.ddlUnderCoatMachineNo.Items.Add(liUnderMachine);

            liMiddleMachine.Text = "";
            liMiddleMachine.Value = "";
            this.ddlMiddleCoatMachineNo.Items.Add(liMiddleMachine);

            liTopMachine.Text = "";
            liTopMachine.Value = "";
            this.ddlTopCoatMachineNo.Items.Add(liTopMachine);



            for (int i = 1; i < 10; i++)
            {

                liUnderMachine = new ListItem();
                liMiddleMachine = new ListItem();
                liTopMachine = new ListItem();


                liUnderMachine.Text = "Machine" + i.ToString();
                liUnderMachine.Value = i.ToString();
                this.ddlUnderCoatMachineNo.Items.Add(liUnderMachine);

                liMiddleMachine.Text = "Machine" + i.ToString();
                liMiddleMachine.Value = i.ToString();
                this.ddlMiddleCoatMachineNo.Items.Add(liMiddleMachine);


                liTopMachine.Text = "Machine" + i.ToString();
                liTopMachine.Value = i.ToString();
                this.ddlTopCoatMachineNo.Items.Add(liTopMachine);
            }
        }


        void setPICDDL()
        {
            Common.Class.BLL.User_DB_BLL bll = new Common.Class.BLL.User_DB_BLL();
            List<Common.Class.Model.User_DB_Model> modelList = bll.GetModelList(StaticRes.Global.Department.Painting, "", "","");



            this.ddlUnderCoatPIC.Items.Clear();
            this.ddlMiddleCoatPIC.Items.Clear();
            this.ddlTopCoatPIC.Items.Clear();

            this.ddlUnderCoatPIC.Items.Add(new ListItem("", ""));
            this.ddlMiddleCoatPIC.Items.Add(new ListItem("", ""));
            this.ddlTopCoatPIC.Items.Add(new ListItem("", ""));


            var sortedList = from a in modelList orderby a.USER_ID ascending select a;

            foreach (var model in sortedList)
            {
                if (model.USER_ID == "")
                    continue;

                this.ddlUnderCoatPIC.Items.Add(new ListItem(model.USER_ID, model.USER_ID));
                this.ddlMiddleCoatPIC.Items.Add(new ListItem(model.USER_ID, model.USER_ID));
                this.ddlTopCoatPIC.Items.Add(new ListItem(model.USER_ID, model.USER_ID));
            }
        }



        void UpdateMFGDate(string jobNumber, DateTime mfgDate)
        {
            //更新 laser inventory表
            Common.Class.BLL.LMMSInventoty_BLL laserInventoryBLL = new Common.Class.BLL.LMMSInventoty_BLL();
            if (!laserInventoryBLL.UpdateMFGDate(jobNumber, mfgDate))
                DBHelp.Reports.LogFile.Log("BuyOffRecordForClient", "Update MFG Date for Laser Inventory Failed! ");

            //更新pqc inventory表
            Common.Class.BLL.PQCInventory_BLL pqcInventoryBLL = new Common.Class.BLL.PQCInventory_BLL();
            if (!pqcInventoryBLL.UpdateMFGDate(jobNumber, mfgDate))
                DBHelp.Reports.LogFile.Log("BuyOffRecordForClient", "Update MFG Date for PQC Inventory Failed! ");

            //更新painting delivery his表
            Common.Class.BLL.PaintingDeliveryHis_BLL paintDeliveryBLL = new Common.Class.BLL.PaintingDeliveryHis_BLL();
            if (!paintDeliveryBLL.UpdateMFGDate(jobNumber, mfgDate))
                DBHelp.Reports.LogFile.Log("BuyOffRecordForClient", "Update MFG Date for Paint Delivery His Failed! ");
        }

        void UpdatePaintRejQty(string jobNumber, int paintRejQty, string process)
        {
            Common.Class.BLL.PaintingDeliveryHis_BLL bll = new Common.Class.BLL.PaintingDeliveryHis_BLL();

            if (!bll.UpdatePaintRej(jobNumber, paintRejQty, process))
            {
                DBHelp.Reports.LogFile.Log("BuyOffRecordForClient", "Update Paint Rej Qty for Paint Delivery His Failed! ");
            }
        }

    }
}