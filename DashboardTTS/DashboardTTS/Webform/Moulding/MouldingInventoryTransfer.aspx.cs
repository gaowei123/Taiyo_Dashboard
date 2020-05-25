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

namespace DashboardTTS.Webform.Moulding
{
    public partial class MouldingInventoryTransfer : System.Web.UI.Page
    {
        const string Print = "Print";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    DBHelp.Reports.LogFile.Log("MouldingDeliveryDebug", "[Page_Load]====In page ");
                    this.lblUserHeader.Text = "Moulding Delivery Record";
                    this.lb_Lotno.Visible = false;
                    this.txt_LotNo.Visible = false;
                    this.lb_partno.Visible = false;
                    this.lb_InQuantity.Visible = false;
                    this.txt_PartNumber.Visible = false;
                    this.txt_InQuantity.Visible = false;
                    this.btn_add.Visible = false;
                    DataTable dt_Inventory = InitDT();
                    Session["dt_Inventory"] = dt_Inventory;
                    txt_JobID.Focus();
                    DBHelp.Reports.LogFile.Log("MouldingDeliveryDebug", "[Page_Load]====page load end ");
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("Inventory Record Execption", "Page_Load error--" + ee.ToString());
                txt_JobID.Focus();
            }
        }

        protected void txt_JobID_TextChanged(object sender, EventArgs e)
        {
            string jobNumber = this.txt_JobID.Text.ToUpper().Trim();
            DBHelp.Reports.LogFile.Log("MouldingDeliveryDebug", "Receive Text: " + jobNumber);
            #region  Check jobnumber
            if (string.IsNullOrEmpty(jobNumber) || jobNumber.Length < 13)
            {
                return;
            }
            DataTable dt_Inventory = (DataTable)Session["dt_Inventory"];
            if (dt_Inventory == null || dt_Inventory.Rows.Count == 0)
            {
                if (this.dg_AddedInventoryList.Items.Count == 0)
                {
                    dt_Inventory = InitDT();
                    Session["dt_Inventory"] = dt_Inventory;
                }
                else
                {
                    dt_Inventory = ResetDT();
                    Session["dt_Inventory"] = dt_Inventory;
                }
            }

            if (!jobNumber.Contains("JOT"))
            {
                this.txt_JobID.Focus();
                Common.CommFunctions.ShowMessage(Page, "The Job No fomart error, please confirm!");
                return;
            }
            //check dt_Inventory
            int RowsCount = dt_Inventory.Select("jobNumber ='" + jobNumber + "' ").Length;
            if (RowsCount > 0)
            {
                this.txt_JobID.Text = "";
                this.txt_JobID.Focus();
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('This Job No is scaned, Please confirm!');", true);
                return;
            }
      
            //check delivery his 
            Common.Class.BLL.PaintingDeliveryHis_BLL BLL_Painting = new Common.Class.BLL.PaintingDeliveryHis_BLL();
            if (BLL_Painting.ExistJobno(jobNumber))
            {
                this.txt_JobID.Focus();
                this.txt_JobID.Text = "";
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('This Job No is already scanned in, Please confirm! ');", true);
                return;
            }
            #endregion
            #region get data from MRP
            string tempOperation = "Mould";
            LMMS_Webservice.LMMS_WebserviceSoapClient Lmms_service = new LMMS_Webservice.LMMS_WebserviceSoapClient();
            LMMS_Webservice.MRP_SearchResult MRP_Result = new LMMS_Webservice.MRP_SearchResult();
            MRP_Result = Lmms_service.MrpLotInfo("8", "8", txt_JobID.Text.Trim(), tempOperation);
            if (!MRP_Result.isCorrectResult)
            {
                DBHelp.Reports.LogFile.Log("MouldingDeliveryDebug", "[txt_JobID_TextChanged]==== Can't find the job in Mould Process ");
                this.txt_JobID.Text = "";
                this.txt_JobID.Focus();
                Common.CommFunctions.ShowMessage(Page, "Error, no any data found from MRP ! Please check job no and try again !");
                return;
            }
            else
            {
                DBHelp.Reports.LogFile.Log("MouldingDeliveryDebug", "[txt_JobID_TextChanged]==== Laser Process ");
            }
            #endregion
            #region deal with data
            string partnumber = MRP_Result.TravellerData.Item;
            decimal InQuantity = MRP_Result.TravellerData.InQuantity;
            string startTime = MRP_Result.TravellerData.StartOn.ToString();
            DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "dd/MM/yyyy";
            DateTime DateIn = System.Convert.ToDateTime(startTime, dtFormat);
            string description = MRP_Result.TravellerData.Description;
            string lotNo = MRP_Result.TravellerData.Lotno;
            if (string.IsNullOrEmpty(partnumber))
            {
                this.txt_JobID.Text = "";
                this.txt_JobID.Focus();
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('ERROR, PartNumber receive from MRP is Empty! ');", true);
                return;
            }
            if (InQuantity == 0)
            {
                this.txt_JobID.Text = "";
                this.txt_JobID.Focus();
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('ERROR, InQuantity receive from MRP is 0! ');", true);
                return;
            }
            if (string.IsNullOrEmpty(lotNo))
            {
                this.txt_JobID.Text = "";
                this.txt_JobID.Focus();
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('ERROR, lot No receive from MRP is Empty! ');", true);
                return;
            }
            #endregion



            #region Update  


            Common.Class.BLL.MouldingBom_BLL bll = new Common.Class.BLL.MouldingBom_BLL();
            Common.Class.Model.MouldingBom_Model bomModel = bll.GetModel(partnumber,"");


            if (bomModel.suppiller == "Paint")
            {
                #region updated Paint inventory 
                Common.Class.Model.PaintingInventory_Model laserModel = new Common.Class.Model.PaintingInventory_Model();
                laserModel.JobNumber = jobNumber;
                laserModel.partNumber = partnumber;
                laserModel.quantity = double.Parse(InQuantity.ToString());
                laserModel.startOnTime = DateIn;
                laserModel.PQCQuantity = 0;
                laserModel.description = description;
                laserModel.Lotno = lotNo;
                laserModel.ShowFlag = "TRUE";
                laserModel.dateTime = DateTime.Now;
                laserModel.day = DateTime.Now.ToString("dd");
                laserModel.month = DateTime.Now.ToString("MM");
                laserModel.year = DateTime.Now.ToString("yyyy");
                Common.Class.BLL.PaintingInventory_BLL laserInventoryBLL = new Common.Class.BLL.PaintingInventory_BLL();
                if (!laserInventoryBLL.Add(laserModel))
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Add to Painting inventory failed!");
                    return;
                }
                #endregion
            }



            #region updated painting delivery his 

     

            Common.Class.Model.MouldingDeliveryHis_Model paintingDeliveryModel = new Common.Class.Model.MouldingDeliveryHis_Model();
            paintingDeliveryModel.partNumber = partnumber;
            paintingDeliveryModel.jobNumber = jobNumber;
            paintingDeliveryModel.lotNo = lotNo;
            paintingDeliveryModel.sendingTo = bomModel.suppiller;
            paintingDeliveryModel.inQuantity = decimal.Parse(InQuantity.ToString());
            paintingDeliveryModel.dateTime = DateIn;
            paintingDeliveryModel.updatedTime = DateTime.Now;
            paintingDeliveryModel.remark = description;
            paintingDeliveryModel.SignID = "";
            Common.Class.BLL.MouldingDeliveryHis_BLL paintingDeliveryBLL = new Common.Class.BLL.MouldingDeliveryHis_BLL();
            if (!paintingDeliveryBLL.Add(paintingDeliveryModel))
            {
                Common.CommFunctions.ShowMessage(this.Page, "Add to Moulding daily delivery history failed!");
                return;
            }
            #endregion



            #endregion


            #region Reset datagrid
            DataRow newDr = dt_Inventory.NewRow();
            newDr["jobNumber"] = jobNumber;
            newDr["lotNo"] = lotNo;
            newDr["partNumber"] = partnumber;
            newDr["inquantity"] = InQuantity;
            newDr["pqcQuantity"] = 0;
            newDr["startTime"] = DateIn;
            newDr["sendingTo"] = "Painting";
            newDr["description"] = description;
            newDr["updatedTime"] = DateTime.Now;
            dt_Inventory.Rows.Add(newDr);
            Session["dt_Inventory"] = dt_Inventory;
            List_Refresh(dt_Inventory);
            #endregion
            #region Set UI
            foreach (DataGridItem item in this.dg_AddedInventoryList.Items)
            {
                if (item.Cells[1].Text == lotNo)
                {
                    item.BackColor = System.Drawing.Color.Pink;
                }
            }
            this.txt_JobID.Text = "";
            this.txt_JobID.Focus();
            this.lb_AddedCount.Text = dt_Inventory.Rows.Count.ToString();
            this.lb_AddedCount.ForeColor = System.Drawing.Color.Red;
            #endregion

        }

        protected void cb_Print_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cb_Print.Checked)
            {
                this.lb_Lotno.Visible = true;
                this.txt_LotNo.Visible = true;
                this.lb_partno.Visible = true;
                this.lb_InQuantity.Visible = true;
                this.txt_PartNumber.Visible = true;
                this.txt_InQuantity.Visible = true;
                this.btn_add.Visible = true;
            }
            else
            {
                this.lb_Lotno.Visible = false;
                this.txt_LotNo.Visible = false;
                this.lb_partno.Visible = false;
                this.lb_InQuantity.Visible = false;
                this.txt_PartNumber.Visible = false;
                this.txt_InQuantity.Visible = false;
                this.btn_add.Visible = false;
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            #region check value
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
            #endregion
            Common.Class.Model.PaintingDeliveryHis_Model paintingDeliveryModel = new Common.Class.Model.PaintingDeliveryHis_Model();
            paintingDeliveryModel.partNumber = this.txt_PartNumber.Text;
            paintingDeliveryModel.jobNumber = this.txt_JobID.Text;
            paintingDeliveryModel.lotNo = this.txt_LotNo.Text;
            paintingDeliveryModel.sendingTo = Print;
            paintingDeliveryModel.inQuantity = decimal.Parse(this.txt_InQuantity.Text);
            paintingDeliveryModel.updatedTime = DateTime.Now;
            paintingDeliveryModel.SignID = "";
            Common.Class.BLL.PaintingDeliveryHis_BLL paintingDeliveryBLL = new Common.Class.BLL.PaintingDeliveryHis_BLL();
            if (!paintingDeliveryBLL.Add(paintingDeliveryModel))
            {
                Common.CommFunctions.ShowMessage(this.Page, "Add to painting daily delivery history failed!");
                return;
            }
            #region Reset datagrid
            DataTable dt_Inventory = (DataTable)Session["dt_Inventory"];
            if (dt_Inventory == null || dt_Inventory.Rows.Count == 0)
            {
                if (this.dg_AddedInventoryList.Items.Count == 0)
                {
                    dt_Inventory = InitDT();
                    Session["dt_Inventory"] = dt_Inventory;
                }
                else
                {
                    dt_Inventory = ResetDT();
                    Session["dt_Inventory"] = dt_Inventory;
                }
            }
            DataRow newDr = dt_Inventory.NewRow();
            newDr["jobNumber"] = "";
            newDr["lotNo"] = this.txt_LotNo.Text;
            newDr["partNumber"] = this.txt_PartNumber.Text;
            newDr["inquantity"] = this.txt_InQuantity.Text;
            newDr["pqcQuantity"] = 0;
            newDr["startTime"] = "";
            newDr["sendingTo"] = Print;
            newDr["description"] = "";
            newDr["updatedTime"] = DateTime.Now;
            dt_Inventory.Rows.Add(newDr);
            Session["dt_Inventory"] = dt_Inventory;
            List_Refresh(dt_Inventory);
            #endregion

            #region Set UI
            foreach (DataGridItem item in this.dg_AddedInventoryList.Items)
            {
                if (item.Cells[1].Text == this.txt_LotNo.Text)
                {
                    item.BackColor = System.Drawing.Color.Pink;
                }
            }
            this.txt_JobID.Text = "";
            this.txt_JobID.Focus();
            this.lb_AddedCount.Text = dt_Inventory.Rows.Count.ToString();
            this.lb_AddedCount.ForeColor = System.Drawing.Color.Red;
            this.lb_Lotno.Visible = false;
            this.lb_partno.Visible = false;
            this.lb_InQuantity.Visible = false;
            this.txt_LotNo.Visible = false;
            this.txt_LotNo.Text = "";
            this.txt_PartNumber.Visible = false;
            this.txt_PartNumber.Text = "";
            this.txt_InQuantity.Visible = false;
            this.txt_InQuantity.Text = "";
            this.btn_add.Visible = false;
            this.cb_Print.Checked = false;
            #endregion
        }

        private void List_Refresh(DataTable dt)
        {
            dg_AddedInventoryList.DataSource = dt.DefaultView;
            dg_AddedInventoryList.DataBind();
        }

        private DataTable InitDT()
        {
            DataTable dt_Inventory = new DataTable();
            dt_Inventory.Columns.Add("jobNumber");
            dt_Inventory.Columns.Add("lotNo");
            dt_Inventory.Columns.Add("partNumber");
            dt_Inventory.Columns.Add("inquantity");
            dt_Inventory.Columns.Add("pqcQuantity");
            dt_Inventory.Columns.Add("startTime");
            dt_Inventory.Columns.Add("sendingTo");
            dt_Inventory.Columns.Add("description");
            dt_Inventory.Columns.Add("updatedTime");
            return dt_Inventory;
        }

        private DataTable ResetDT()
        {
            DataTable dt_Inventory = new DataTable();
            dt_Inventory.Columns.Add("jobNumber");
            dt_Inventory.Columns.Add("lotNo");
            dt_Inventory.Columns.Add("partNumber");
            dt_Inventory.Columns.Add("inquantity");
            dt_Inventory.Columns.Add("pqcQuantity");
            dt_Inventory.Columns.Add("startTime");
            dt_Inventory.Columns.Add("sendingTo");
            dt_Inventory.Columns.Add("description");
            dt_Inventory.Columns.Add("updatedTime");
            foreach (DataGridItem item in this.dg_AddedInventoryList.Items)
            {
                DataRow dr = dt_Inventory.NewRow();
                dr["jobNumber"] = item.Cells[0].Text;
                dr["lotNo"] = item.Cells[1].Text;
                dr["partNumber"] = item.Cells[2].Text;
                dr["inquantity"] = item.Cells[3].Text;
                dr["pqcQuantity"] = item.Cells[5].Text;
                dr["startTime"] = item.Cells[4].Text;
                dr["sendingTo"] = item.Cells[6].Text;
                dr["description"] = item.Cells[7].Text;
                dr["updatedTime"] = item.Cells[8].Text;
                dt_Inventory.Rows.Add(dr);
            }
            return dt_Inventory;
        }
    }
}