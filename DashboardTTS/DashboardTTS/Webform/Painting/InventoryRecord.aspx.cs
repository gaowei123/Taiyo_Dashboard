using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;
using System.Data.SqlClient;

namespace DashboardTTS.Webform.Painting
{
    public partial class InventoryRecord : System.Web.UI.Page
    {
        const string Print = "Print";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    DBHelp.Reports.LogFile.Log("InventoryRecord_Debug", "In Page");

                    this.lblUserHeader.Text = "Painting Delivery Record";

                    //隐藏 手动填写的lotno, in quantity, partno
                    HidePrint();

             
                    this.txt_JobID.Focus();
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("InventoryRecord_Debug", "Page_Load error--" + ee.ToString());
                txt_JobID.Focus();
            }
        }

       
        protected void txt_JobID_TextChanged(object sender, EventArgs e)
        {
            DBHelp.Reports.LogFile.Log("InventoryRecord_Debug", "In Func txt_JobID_TextChanged");
            
            if (this.cb_Print.Checked)
                return;

            string jobNo = this.txt_JobID.Text.Trim();
            if (jobNo == "")
            {
                this.txt_JobID.Focus();
                return;
            }

            if (!jobNo.ToUpper().Contains("JOT") || jobNo.Length != 13)
            {
                this.txt_JobID.Focus();
                Common.CommFunctions.ShowMessage(Page, "The Job No fomart error, please confirm!");
                return;
            }

          
            DBHelp.Reports.LogFile.Log("InventoryRecord_Debug", string.Format( "Job No: {0}",jobNo));

            string paintProcess = "";

            //查 paint delivery 最近一条记录
            Common.Class.BLL.PaintingDeliveryHis_BLL paintBLL = new Common.Class.BLL.PaintingDeliveryHis_BLL();
            List<Common.Class.Model.PaintingDeliveryHis_Model> paintModels = paintBLL.GetModels(jobNo, DateTime.Now.AddYears(-2), DateTime.Now);
            if (paintModels == null)
            {
                //没有 记录为paint1
                paintProcess = "Paint#1";
            }
            else
            {
                //有paint1, 并且process中有painting#2的, 记录为paint2
                //有paint2, 并且process中有painting#3的, 记录为paint3



                LMMS_Webservice.MRP_SearchResult mrpResult = GetMRPResult(jobNo);
                //LocalTest.MRP_SearchResult mrpResult = GetMRPResult_TEST(); //local testing 
                if (mrpResult == null || !mrpResult.isCorrectResult)
                {
                    this.txt_JobID.Text = "";
                    this.txt_JobID.Focus();
                    Common.CommFunctions.ShowMessage(Page, "Error, Get data from MRP fail ! Please check job no and try again !");
                    return;
                }

                Common.Class.BLL.PQCBom_BLL pqcBomBLL = new Common.Class.BLL.PQCBom_BLL();
                Common.Class.Model.PQCBom_Model pqcBomModel = pqcBomBLL.GetModel(mrpResult.TravellerData.Item);
                if (pqcBomModel == null)
                {
                    this.txt_JobID.Text = "";
                    this.txt_JobID.Focus();
                    Common.CommFunctions.ShowMessage(Page, string.Format("Error, Can not find part no [{0}] in pqc bom, Please check!",mrpResult.TravellerData.Item));
                    return;
                }


             



                Common.Class.Model.PaintingDeliveryHis_Model paintModel = (from a in paintModels orderby a.updatedTime descending select a).FirstOrDefault();
                //旧版本 是不区分process, 该字段为null, 默认都为Paint#1
                if (paintModel.paintProcess == "")
                    paintModel.paintProcess = "Paint#1";



                if (paintModel.paintProcess == "Paint#1")
                {
                    if (!pqcBomModel.processes.Contains("Paint#2"))
                    {
                        Common.CommFunctions.ShowMessage(Page, "Error, This job is already added , can not add again!");
                        return;
                    }
                    else
                    {
                        paintProcess = "Paint#2";
                    }
                    
                }
                else if (paintModel.paintProcess == "Paint#2")
                {
                    if (!pqcBomModel.processes.Contains("Paint#3"))
                    {
                        Common.CommFunctions.ShowMessage(Page, "Error, This job is already added , can not add again!");
                        return;
                    }
                    else
                    {
                        paintProcess = "Paint#3";
                    }
                }
            }
            


            
           
         
           

            DBHelp.Reports.LogFile.Log("InventoryRecord_Debug", string.Format("Paint Process: [{0}]",paintProcess));

            this.lbProcess.Text = paintProcess;


            btn_add_Click(null, null);
        }


        protected void cb_Print_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cb_Print.Checked)
                InitPrint();
            else
                HidePrint();
        }


        protected void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                DBHelp.Reports.LogFile.Log("InventoryRecord_Debug", "In Func btn_add_Click");

                DBHelp.Reports.LogFile.Log("InventoryRecord_Debug", string.Format("cb_Print Checked: {0}", this.cb_Print.Checked.ToString()));
                if (this.cb_Print.Checked)
                {
                    #region 手动填写lotno, qty, partnumber添加
                    if (this.txt_LotNo.Text == "")
                    {
                        Common.CommFunctions.ShowMessage(this.Page, "Please key in Lot No!");
                        return;
                    }
                    if (this.txt_PartNumber.Text == "")
                    {
                        Common.CommFunctions.ShowMessage(this.Page, "Please key in partnumber!");
                        return;
                    }
                    if (this.txt_InQuantity.Text == "")
                    {
                        Common.CommFunctions.ShowMessage(this.Page, "Please key in InQuantity!");
                        return;
                    }
                    else if (!Common.CommFunctions.isNumberic(this.txt_InQuantity.Text))
                    {
                        Common.CommFunctions.ShowMessage(this.Page, "InQuantity must be number!");
                        return;
                    }
                   
                 

                    Common.Class.Model.PaintingDeliveryHis_Model paintingDeliveryModel = new Common.Class.Model.PaintingDeliveryHis_Model();
                    paintingDeliveryModel.partNumber = this.txt_PartNumber.Text;
                    paintingDeliveryModel.jobNumber = this.txt_JobID.Text;
                    paintingDeliveryModel.lotNo = this.txt_LotNo.Text;
                    paintingDeliveryModel.sendingTo = Print;
                    paintingDeliveryModel.inQuantity = decimal.Parse(this.txt_InQuantity.Text);
                    //paintingDeliveryModel.dateTime = DateIn;
                    paintingDeliveryModel.updatedTime = DateTime.Now;
                    //paintingDeliveryModel.remark = description;
                    paintingDeliveryModel.SignID = "";
                    paintingDeliveryModel.paintRejQty = 0;
                    paintingDeliveryModel.paintProcess = "Print";


                    Common.Class.BLL.PaintingDeliveryHis_BLL paintingDeliveryBLL = new Common.Class.BLL.PaintingDeliveryHis_BLL();
                    if (!paintingDeliveryBLL.Add(paintingDeliveryModel))
                    {
                        Common.CommFunctions.ShowMessage(this.Page, "Add to painting daily delivery history failed!");
                        return;
                    }

                    //刷新列表
                    RefreshDataGrid(paintingDeliveryModel);

                    //隐藏框框, 按钮
                    HidePrint();


                    #endregion
                }
                else
                {
                    string jobNumber = this.txt_JobID.Text.ToUpper().Trim().ToUpper();
                    string paintProcess = this.lbProcess.Text;//在txt_JobID_TextChanged中设定好了process赋值给的lbprocess.
                    int paintRejQty = 0;//默认为0

                    DBHelp.Reports.LogFile.Log("InventoryRecord_Debug", string.Format("Job No: {0}, Paint Process: {1}, PaintRejQty: {2}", jobNumber, paintProcess, paintRejQty));

        


                    #region call interface  get data from mrp
                    LMMS_Webservice.MRP_SearchResult mrpResult = GetMRPResult(jobNumber);
                    //LocalTest.MRP_SearchResult mrpResult = GetMRPResult_TEST(); //local testing 

                    if (mrpResult == null || !mrpResult.isCorrectResult)
                    {
                        this.txt_JobID.Text = "";
                        this.txt_JobID.Focus();
                        Common.CommFunctions.ShowMessage(Page, "Error, Get data from MRP fail ! Please check job no and try again !");
                        return;
                    }


                    string partnumber = mrpResult.TravellerData.Item;
                    decimal InQuantity = mrpResult.TravellerData.InQuantity;
                    string description = mrpResult.TravellerData.Description;
                    string lotNo = mrpResult.TravellerData.Lotno;

                    DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
                    dtFormat.ShortDatePattern = "dd/MM/yyyy";
                    DateTime DateIn = System.Convert.ToDateTime(mrpResult.TravellerData.StartOn.ToString(), dtFormat);

                    DBHelp.Reports.LogFile.Log("InventoryRecord_Debug", string.Format("Call MRP Interface success, Part No: {0}, Qty: {1}, lotNo: {2}", partnumber, InQuantity, lotNo));

                    if (string.IsNullOrEmpty(partnumber))
                    {
                        this.txt_JobID.Text = "";
                        this.txt_JobID.Focus();
                        Common.CommFunctions.ShowMessage(this.Page, "ERROR, PartNumber received from MRP is Empty!");
                        return;
                    }

                    if (InQuantity == 0)
                    {
                        this.txt_JobID.Text = "";
                        this.txt_JobID.Focus();
                        Common.CommFunctions.ShowMessage(this.Page, "ERROR, InQuantity received from MRP is 0!");
                        return;
                    }

                    if (string.IsNullOrEmpty(lotNo))
                    {
                        this.txt_JobID.Text = "";
                        this.txt_JobID.Focus();
                        Common.CommFunctions.ShowMessage(this.Page, "ERROR, lot No received from MRP is Empty!");
                        return;
                    }
                    #endregion



                    //Get next process
                    string nextProcess = GetProcess(partnumber, paintProcess);
                    DBHelp.Reports.LogFile.Log("InventoryRecord_Debug", string.Format("Get Next Process, process: [{0}]", nextProcess));

                    if (nextProcess == "NO BOM")
                    {
                        Common.CommFunctions.ShowMessage(this.Page, string.Format("Error, Can not find the part: [{0}] in pqc bom list, please add part first.", partnumber));
                        return;
                    }
                    else if (nextProcess == "NOT FOUND")
                    {
                        Common.CommFunctions.ShowMessage(this.Page, string.Format("Not found the process {0} for the part:[{1}], please check the bom setting.", partnumber, paintProcess));
                        return;
                    }
                    else if (nextProcess == "")
                    {
                        Common.CommFunctions.ShowMessage(this.Page, string.Format("Can not get the next station after finish {0}, please check the bom setting.", paintProcess));
                        return;
                    }

                   

                    //next process是check#2的, 将check#1的ok数量 作为in qty
                    if (nextProcess.Contains("Check#2"))
                    {
                        InQuantity = GetCheck1Qty(jobNumber);
                        DBHelp.Reports.LogFile.Log("InventoryRecord_Debug", string.Format("PQC Check#1 OK Qty: {0}", InQuantity));
                        if (InQuantity == 0)
                        {
                            Common.CommFunctions.ShowMessage(this.Page, "Error, get quantity 0 from previous station PQC, Please confirm whether pqc complete check!");
                            return;
                        }
                    }
                    //next process是check#3的, 将check#2的ok数量 作为in qty
                    else if (nextProcess.Contains("Check#3"))
                    {
                        InQuantity = GetCheck2Qty(jobNumber);
                        DBHelp.Reports.LogFile.Log("InventoryRecord_Debug", string.Format("PQC Check#2 OK Qty: {0}", InQuantity));
                        if (InQuantity == 0)
                        {
                            Common.CommFunctions.ShowMessage(this.Page, "Error, get quantity 0 from previous station PQC, Please confirm whether pqc complete check!");
                            return;
                        }
                    }



                    string sendingTo = "";

                    #region update
                    if (nextProcess.Contains("Laser") && nextProcess.Contains("Check"))
                    {
                        //check repeat add
                        //Common.Class.BLL.LMMSInventoty_BLL laserInventoryBLL = new Common.Class.BLL.LMMSInventoty_BLL();
                        //if (laserInventoryBLL.Exist(txt_JobID.Text.Trim()))
                        //{
                        //    this.txt_JobID.Text = "";
                        //    this.txt_JobID.Focus();
                        //    Common.CommFunctions.ShowMessage(this.Page, "This Job No is already in Laser, Please confirm!");
                        //    return;
                        //}
                        //Common.Class.BLL.PQCInventory_BLL pqcInventoryBLL = new Common.Class.BLL.PQCInventory_BLL();
                        //if (pqcInventoryBLL.Exist(txt_JobID.Text.Trim(), "Check#" + process))
                        //{
                        //    this.txt_JobID.Text = "";
                        //    this.txt_JobID.Focus();
                        //    Common.CommFunctions.ShowMessage(this.Page, "This Job No is already in PQC, Please confirm!");
                        //    return;
                        //}
                        //check repeat add


                        //add to laser & pqc inventory
                        //bool result = AddLaserInventory(jobNumber, partnumber, InQuantity, DateIn, paintRejQty, description, lotNo);
                        //if (!result)
                        //    DBHelp.Reports.LogFile.Log("InventoryRecord_Debug", string.Format("btn_add_Click, next process:{0} ,add laser inventory failed!", nextProcess));

                        //result = AddPQCInventory(jobNumber, partnumber, InQuantity, DateIn, paintRejQty, description, lotNo, "Check#" + process);
                        //if (!result)
                        //    DBHelp.Reports.LogFile.Log("InventoryRecord_Debug", string.Format("btn_add_Click, next process:{0} ,add laser pqc failed!", nextProcess));


                        //sendingTo = "LASER";
                    }
                    else if (nextProcess.Contains("Laser"))
                    {
                        //check repeat add
                        Common.Class.BLL.LMMSInventoty_BLL laserInventoryBLL = new Common.Class.BLL.LMMSInventoty_BLL();
                        if (laserInventoryBLL.Exist(txt_JobID.Text.Trim()))
                        {
                            this.txt_JobID.Text = "";
                            this.txt_JobID.Focus();
                            Common.CommFunctions.ShowMessage(this.Page, "This Job No is already in Laser, Please confirm!");
                            return;
                        }
                     

                        //add to laser inventory
                        bool result = AddLaserInventory(jobNumber, partnumber, InQuantity, DateIn, paintRejQty, description, lotNo);
                        if (!result)
                            DBHelp.Reports.LogFile.Log("InventoryRecord_Debug", string.Format("btn_add_Click, next process:{0} ,add laser inventory failed!", nextProcess));
                        sendingTo = "LASER";
                    }
                    else if (nextProcess.Contains("Check"))
                    {
                        //check repeat add
                        Common.Class.BLL.PQCInventory_BLL pqcInventoryBLL = new Common.Class.BLL.PQCInventory_BLL();
                        if (pqcInventoryBLL.Exist(txt_JobID.Text.Trim(), nextProcess))
                        {
                            this.txt_JobID.Text = "";
                            this.txt_JobID.Focus();
                            Common.CommFunctions.ShowMessage(this.Page, "This Job No is already in PQC, Please confirm!");
                            return;
                        }
                  


                        //add to pqc inventory
                        bool result = AddPQCInventory(jobNumber, partnumber, InQuantity, DateIn, paintRejQty, description, lotNo, nextProcess);
                        if (!result)
                            DBHelp.Reports.LogFile.Log("InventoryRecord_Debug", string.Format("btn_add_Click, next process:{0} ,add laser pqc failed!", nextProcess));

                        sendingTo = "PQC";

                    }
                    else
                    {
                        //Painting后不是check和laser的工序, 不添加到inventory. 
                        DBHelp.Reports.LogFile.Log("InventoryRecord_Debug", string.Format("No Laser&Check process: {0}", nextProcess));
                    }



                    //add into painting delivery history 
                    Common.Class.Model.PaintingDeliveryHis_Model paintingDeliveryModel = new Common.Class.Model.PaintingDeliveryHis_Model();
                    paintingDeliveryModel.partNumber = partnumber;
                    paintingDeliveryModel.jobNumber = jobNumber;
                    paintingDeliveryModel.lotNo = lotNo;
                    paintingDeliveryModel.sendingTo = sendingTo;
                    paintingDeliveryModel.inQuantity = decimal.Parse(InQuantity.ToString());
                    paintingDeliveryModel.dateTime = DateIn;
                    paintingDeliveryModel.updatedTime = DateTime.Now;
                    paintingDeliveryModel.remark = description;
                    paintingDeliveryModel.paintProcess = paintProcess;
                    paintingDeliveryModel.SignID = "";
                    paintingDeliveryModel.paintRejQty = paintRejQty;


                    Common.Class.BLL.PaintingDeliveryHis_BLL paintingDeliveryBLL = new Common.Class.BLL.PaintingDeliveryHis_BLL();
                    paintingDeliveryBLL.Add(paintingDeliveryModel);
                    #endregion


                    RefreshDataGrid(paintingDeliveryModel);


                }


                //reset ui
                this.lb_AddedCount.Text = this.dg_AddedInventoryList.Items.Count.ToString();
                this.lb_AddedCount.ForeColor = System.Drawing.Color.Red;

                this.txt_JobID.Text = "";
                this.txt_JobID.Focus();
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("InventoryRecord_Debug", "btn_add_Click exception : " + ee.ToString());
            }
        }
        

       
        


        private bool AddLaserInventory(string jobNo, string partNo, decimal inQty, DateTime dateIn,int paintRejQty,string description,string lotNo)
        {

            Common.Class.Model.LMMSInventory_Model laserModel = new Common.Class.Model.LMMSInventory_Model();
            laserModel.JobNumber = jobNo;
            laserModel.partNumber = partNo;
            laserModel.quantity = double.Parse(inQty.ToString());
            laserModel.startOnTime = dateIn;
            laserModel.PQCQuantity = paintRejQty;
            laserModel.description = description;
            laserModel.Lotno = lotNo;
            laserModel.ShowFlag = "TRUE";
            laserModel.dateTime = DateTime.Now;
            laserModel.day = DateTime.Now.Day.ToString();
            laserModel.month = DateTime.Now.Month.ToString();
            laserModel.year = DateTime.Now.Year.ToString();

            Common.Class.BLL.LMMSInventoty_BLL bll = new Common.Class.BLL.LMMSInventoty_BLL();
            return bll.Add(laserModel);
        }

        private bool AddPQCInventory(string jobNo, string partNo, decimal inQty, DateTime dateIn, int paintRejQty, string description, string lotNo,string checkProcess)
        {
            Common.Class.Model.PQCInventory_Model pqcModel = new Common.Class.Model.PQCInventory_Model();
            pqcModel.LotNo = lotNo;
            pqcModel.JobNumber = jobNo;
            pqcModel.PartNumber = partNo;
            pqcModel.InMRPQuantity = decimal.Parse(inQty.ToString());
            pqcModel.MFGDate = dateIn;
            pqcModel.UpdatedTime = DateTime.Now;
            pqcModel.InventoryType = StaticRes.Global.Department.Painting;
            pqcModel.CheckProcess = checkProcess;
            pqcModel.shortage = paintRejQty;

            Common.Class.BLL.PQCInventory_BLL bll = new Common.Class.BLL.PQCInventory_BLL();

            return bll.Add(pqcModel);
        }
        
        private int GetCheck1Qty(string jobNo)
        {
            Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();
            return  bll.GetJobCheck1Output(jobNo);
        }

        private int GetCheck2Qty(string jobNo)
        {
            Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();
            return bll.GetJobCheck2Output(jobNo);
        }

        private string GetProcess(string partNo,string paintProcess)
        {
            string nextProcess = "";

            Common.Class.BLL.PQCBom_BLL bll = new Common.Class.BLL.PQCBom_BLL();
            Common.Class.Model.PQCBom_Model model = bll.GetModel(partNo);

            //bom中找不到, 直接返回NO BOM
            if (model == null)
                return "NO BOM";
            

            DBHelp.Reports.LogFile.Log("InventoryRecord_Debug",string.Format( "GetProcess  whole processes:[{0}] , selected paint process:[{1}]" ,model.processes,paintProcess));


            //如果没有这个paint process, 直接返回NOT FOUND
            if (!model.processes.Contains(paintProcess))
                return "NOT FOUND";

            //取出paint后的process, 并split成数组
            string afterProcess = model.processes.Replace(paintProcess, "/").Split('/')[1];
            string[] afterProcessArr = afterProcess.Split('-');

            for (int i = 0; i < afterProcessArr.Length; i++)
            {
                string process = afterProcessArr[i];

                //如果下一站不是lase或者check, 就继续找下一站.
                if (!process.Contains("Laser") && !process.Contains("Check"))
                {
                    continue;
                }
                else if (process.Contains("Check"))
                {
                    //如果下站是check, 并且下下站是laser的特殊情况, 加到2边.
                    if (i + 1 < afterProcessArr.Length && afterProcessArr[i + 1].Contains("Laser"))//防止数组越界, 判断下长度.
                    {
                        nextProcess = afterProcessArr[i] + "," + afterProcessArr[i + 1];
                    }
                    else
                    {
                        nextProcess = process;
                    }

                    break;
                }
                else
                {
                    nextProcess = process;
                    break;
                }
            }


            return nextProcess;
        }
        

        //获取MRP接口数据.
        private LMMS_Webservice.MRP_SearchResult GetMRPResult(string jobNo)
        {
            LMMS_Webservice.LMMS_WebserviceSoapClient Lmms_service = new LMMS_Webservice.LMMS_WebserviceSoapClient();
            LMMS_Webservice.MRP_SearchResult MRP_Result = new LMMS_Webservice.MRP_SearchResult();


            //先查一下laser工序.
            MRP_Result = Lmms_service.MrpLotInfo("8", "8", jobNo, StaticRes.Global.Department.Laser);

            if (!MRP_Result.isCorrectResult)
            {
                //没有的话, 继续查一下PQC工序
                MRP_Result = new LMMS_Webservice.MRP_SearchResult();
                MRP_Result = Lmms_service.MrpLotInfo("8", "8", jobNo, StaticRes.Global.Department.PQC);

                if (!MRP_Result.isCorrectResult)
                    return null;

            }

            return MRP_Result;
        }


        //做本地测试时 用的.
        private LocalTest.MRP_SearchResult GetMRPResult_TEST()
        {
            LocalTest.LMMS_WebserviceSoapClient Lmms_service = new LocalTest.LMMS_WebserviceSoapClient();
            LocalTest.MRP_SearchResult MRP_Result = new LocalTest.MRP_SearchResult();


            //先查一下laser工序.
            MRP_Result = Lmms_service.MrpLotInfo("8", "8", txt_JobID.Text.Trim(), StaticRes.Global.Department.Laser);

            if (!MRP_Result.isCorrectResult)
            {
                //没有的话, 继续查一下PQC工序
                MRP_Result = new LocalTest.MRP_SearchResult();
                MRP_Result = Lmms_service.LocalTest_MrpLotInfo(txt_JobID.Text.Trim());
                if (!MRP_Result.isCorrectResult)
                {

                    return null;
                }
            }

            return MRP_Result;
        }


        //刷新列表. 每次将dataGrid的列表转换成datatable, 在datatable中加新的一行, 再绑定到dataGrid.
        private void RefreshDataGrid(Common.Class.Model.PaintingDeliveryHis_Model paintModel)
        {
            if (paintModel == null )
                return;


            DataTable dt_Inventory = new DataTable();
            dt_Inventory.Columns.Add("jobNumber");
            dt_Inventory.Columns.Add("lotNo");
            dt_Inventory.Columns.Add("partNumber");
            dt_Inventory.Columns.Add("inquantity");
            dt_Inventory.Columns.Add("paintRej");
            dt_Inventory.Columns.Add("pqcQuantity");
            dt_Inventory.Columns.Add("startTime");
            dt_Inventory.Columns.Add("sendingTo");
            dt_Inventory.Columns.Add("paintProcess");
            dt_Inventory.Columns.Add("description");
            dt_Inventory.Columns.Add("updatedTime");
            

            foreach (DataGridItem item in this.dg_AddedInventoryList.Items)
            {
                DataRow dr = dt_Inventory.NewRow();

                dr["jobNumber"] = item.Cells[0].Text.Replace("&nbsp;", "");
                dr["lotNo"] = item.Cells[1].Text.Replace("&nbsp;", "");
                dr["partNumber"] = item.Cells[2].Text.Replace("&nbsp;", "");
                dr["inquantity"] = item.Cells[3].Text.Replace("&nbsp;", "");
                dr["paintRej"] = item.Cells[4].Text.Replace("&nbsp;", "");

                dr["startTime"] = item.Cells[5].Text.Replace("&nbsp;", "");
                dr["pqcQuantity"] = item.Cells[6].Text.Replace("&nbsp;", "");
                dr["sendingTo"] = item.Cells[7].Text.Replace("&nbsp;", "");
                dr["paintProcess"] = item.Cells[8].Text.Replace("&nbsp;", "");
                dr["description"] = item.Cells[9].Text.Replace("&nbsp;", "");
                dr["updatedTime"] = item.Cells[10].Text.Replace("&nbsp;", "");

                dt_Inventory.Rows.Add(dr);
            }

         
            
            //add row for the new job
            DataRow newDr = dt_Inventory.NewRow();
            newDr["jobNumber"] = paintModel.jobNumber;
            newDr["lotNo"] = paintModel.lotNo;
            newDr["partNumber"] = paintModel.partNumber;
            newDr["inquantity"] = paintModel.inQuantity;
            newDr["paintRej"] = paintModel.paintRejQty;
            newDr["pqcQuantity"] = 0;
            newDr["startTime"] = paintModel.dateTime;
            newDr["sendingTo"] = paintModel.sendingTo;
            newDr["paintProcess"] = paintModel.paintProcess;
            newDr["description"] = paintModel.remark;
            newDr["updatedTime"] = DateTime.Now;
            dt_Inventory.Rows.Add(newDr);



            dg_AddedInventoryList.DataSource = dt_Inventory.DefaultView;
            dg_AddedInventoryList.DataBind();
            
            foreach (DataGridItem item in this.dg_AddedInventoryList.Items)
            {
                if (item.Cells[0].Text == paintModel.jobNumber && item.Cells[8].Text == paintModel.paintProcess)
                    item.BackColor = System.Drawing.Color.Pink;//新加的job粉色高亮显示.
            }
        }
     


        void HidePrint()
        {
            this.tbLotNo.Visible = false;
            this.tbPartNo.Visible = false;
            this.tbInQty.Visible = false;

            this.btn_add.Visible = false;
            this.cb_Print.Checked = false;
        }

        void InitPrint()
        {
            this.txt_InQuantity.Text = "";
            this.txt_PartNumber.Text = "";
            this.txt_LotNo.Text = "";

            this.tbLotNo.Visible = true;
            this.tbPartNo.Visible = true;
            this.tbInQty.Visible = true;

            this.btn_add.Visible = true;
        }
        
    }
}